using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.Models;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAMMS.DTO.Wrappers;
using RAMMS.DTO;

namespace RAMMS.Repository
{
    public interface IFormV2EquipmentRepository : IRepositoryBase<RmFormV2Eqp>
    {
        int SaveFormV2Dtl(RmFormV2Eqp _RmFormV2Dtl);
        IQueryable<RmFormV2Eqp> GetGridData(RmFormV2Eqp _RmFormV2Dtl);
        IQueryable<RmFormV2Hdr> GetGridHdrData(RmFormV2Hdr _RmFormV2Dtl);
        Task<RmFormV2Eqp> DetailView(RmFormV2Eqp rmFormV2Dtl);
        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions, string id);
        Task<List<RmFormV2Eqp>> GetFilteredRecordList(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions, string id);

        Task<RmFormV2Eqp> GetFormWithDetailsByNoAsync(int formNo);
        Task<List<RmFormV2Eqp>> GetAllEquipmentById(int headerId);
    }
    public class FormV2EquipmentRepository : RepositoryBase<RmFormV2Eqp>, IFormV2EquipmentRepository
    {
        public FormV2EquipmentRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }
        public int SaveFormV2Dtl(RmFormV2Eqp rmFormV2Dtl)
        {
            try
            {
                _context.Entry<RmFormV2Eqp>(rmFormV2Dtl).State = rmFormV2Dtl.Fv2ePkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                return 500;
            }
        }
        public IQueryable<RmFormV2Eqp> GetGridData(RmFormV2Eqp rmFormV2Dtl)
        {
            IQueryable<RmFormV2Eqp> gridData;
            gridData = _context.RmFormV2Eqp.Where(s => s.Fv2eActiveYn == true)
                .AsNoTracking();

            return gridData;
        }

        public IQueryable<RmFormV2Hdr> GetGridHdrData(RmFormV2Hdr rmFormV2Hdr)
        {
            IQueryable<RmFormV2Hdr> gridData;
            gridData = _context.RmFormV2Hdr.Where(s => s.Fv2hActiveYn == true)
                .AsNoTracking();

            return gridData;
        }

        public async Task<RmFormV2Eqp> DetailView(RmFormV2Eqp rmFormV2Dtl)
        {
            var editDetail = await _context.Set<RmFormV2Eqp>().FirstOrDefaultAsync(a => a.Fv2ePkRefNo == rmFormV2Dtl.Fv2ePkRefNo);
            return editDetail;
        }

        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions, string id)
        {
            var query = (from x in _context.RmFormV2Eqp where x.Fv2eActiveYn == true select x).Where(x => x.Fv2eFv2hPkRefNo == Convert.ToInt32(id));
            PrepareFilterQuery(filterOptions, ref query);
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<RmFormV2Eqp>> GetFilteredRecordList(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions, string id)
        {
            List<RmFormV2Eqp> result = new List<RmFormV2Eqp>();
            var query = (from x in _context.RmFormV2Eqp where x.Fv2eActiveYn == true select x).Where(x => x.Fv2eFv2hPkRefNo == Convert.ToInt32(id));
            PrepareFilterQuery(filterOptions, ref query);
            result = await query.OrderBy(x => x.Fv2ePkRefNo)
                                .Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            return result;
        }


        private void PrepareFilterQuery(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions, ref IQueryable<RmFormV2Eqp> query)
        {
            query = query.Where(x => x.Fv2eActiveYn == true);
        }

        public async Task<RmFormV2Eqp> GetFormWithDetailsByNoAsync(int formNo)
        {
            return await _context.RmFormV2Eqp
                       .FirstOrDefaultAsync(x => x.Fv2ePkRefNo == formNo);
        }

        public async Task<List<RmFormV2Eqp>> GetAllEquipmentById(int headerId)
        {
            return await _context.RmFormV2Eqp.Where(x => x.Fv2eFv2hPkRefNo == headerId).ToListAsync();
        }
    }
}
