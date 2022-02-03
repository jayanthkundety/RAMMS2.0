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
    public interface IFormDEduipmentRepository : IRepositoryBase<RmFormDEquipDtl>
    {
        int SaveFormDDtl(RmFormDEquipDtl _RmFormDDtl);
        IQueryable<RmFormDEquipDtl> GetGridData(RmFormDEquipDtl _RmFormDDtl);
        IQueryable<RmFormDHdr> GetGridHdrData(RmFormDHdr _RmFormDDtl);
        Task<RmFormDEquipDtl> DetailView(RmFormDEquipDtl rmFormDDtl);
        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id);
        Task<List<RmFormDEquipDtl>> GetFilteredRecordList(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id);

        Task<RmFormDEquipDtl> GetFormWithDetailsByNoAsync(int formNo);
        Task<List<RmFormDEquipDtl>> GetAllEquipmentById(int headerId);
    }
    public class FormDEduipmentRepository : RepositoryBase<RmFormDEquipDtl>, IFormDEduipmentRepository
    {
        public FormDEduipmentRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }
        public int SaveFormDDtl(RmFormDEquipDtl rmFormDDtl)
        {
            try
            {
                _context.Entry<RmFormDEquipDtl>(rmFormDDtl).State = rmFormDDtl.FdedPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                return 500;
            }
        }
        public IQueryable<RmFormDEquipDtl> GetGridData(RmFormDEquipDtl rmFormDDtl)
        {
            IQueryable<RmFormDEquipDtl> gridData;
            gridData = _context.RmFormDEquipDtl.Where(s => s.FdedActiveYn == true)
                .AsNoTracking();

            return gridData;
        }

        public IQueryable<RmFormDHdr> GetGridHdrData(RmFormDHdr rmFormDHdr)
        {
            IQueryable<RmFormDHdr> gridData;
            gridData = _context.RmFormDHdr.Where(s => s.FdhActiveYn == true)
                .AsNoTracking();

            return gridData;
        }

        public async Task<RmFormDEquipDtl> DetailView(RmFormDEquipDtl rmFormDDtl)
        {
            var editDetail = await _context.Set<RmFormDEquipDtl>().FirstOrDefaultAsync(a => a.FdedPkRefNo == rmFormDDtl.FdedPkRefNo);
            return editDetail;
        }

        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id)
        {
            var query = (from x in _context.RmFormDEquipDtl where x.FdedActiveYn == true select x).Where(x => x.FdedFdhPkRefNo == Convert.ToInt32(id));
            PrepareFilterQuery(filterOptions, ref query);
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<RmFormDEquipDtl>> GetFilteredRecordList(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id)
        {
            List<RmFormDEquipDtl> result = new List<RmFormDEquipDtl>();
            var query = (from x in _context.RmFormDEquipDtl where x.FdedActiveYn == true select x).Where(x => x.FdedFdhPkRefNo == Convert.ToInt32(id));
            PrepareFilterQuery(filterOptions, ref query);
            result = await query.OrderBy(x => x.FdedPkRefNo)
                                .Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            return result;
        }


        private void PrepareFilterQuery(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, ref IQueryable<RmFormDEquipDtl> query)
        {
            query = query.Where(x => x.FdedActiveYn == true);
        }

        public async Task<RmFormDEquipDtl> GetFormWithDetailsByNoAsync(int formNo)
        {
            return await _context.RmFormDEquipDtl
                       .FirstOrDefaultAsync(x => x.FdedPkRefNo == formNo);
        }

        public async Task<List<RmFormDEquipDtl>> GetAllEquipmentById(int headerId)
        {
            return await _context.RmFormDEquipDtl.Where(x => x.FdedFdhPkRefNo == headerId).ToListAsync();
        }
    }
}
