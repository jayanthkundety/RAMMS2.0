using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository
{
    public interface IFormV2LabourRepository : IRepositoryBase<RmFormV2Lab>
    {
        int SaveFormV2Dtl(RmFormV2Lab _RmFormV2Dtl);
        IQueryable<RmFormV2Lab> GetGridData(RmFormV2Lab _RmFormV2Dtl);
        IQueryable<RmFormV2Hdr> GetGridHdrData(RmFormV2Hdr _RmFormV2Dtl);
        Task<RmFormV2Lab> DetailView(RmFormV2Lab rmFormV2Dtl);

        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions, string id);
        Task<List<RmFormV2Lab>> GetFilteredRecordList(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions, string id);

        Task<RmFormV2Lab> GetFormWithDetailsByNoAsync(int formNo);

        Task<List<RmFormV2Lab>> GetAllLabourById(int headerId);
    }
    public class FormV2LabourRepository : RepositoryBase<RmFormV2Lab>, IFormV2LabourRepository
    {
        public FormV2LabourRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }
        public int SaveFormV2Dtl(RmFormV2Lab rmFormV2Dtl)
        {
            try
            {
                _context.Entry<RmFormV2Lab>(rmFormV2Dtl).State = rmFormV2Dtl.Fv2lPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                return 500;
            }
        }
        public IQueryable<RmFormV2Lab> GetGridData(RmFormV2Lab rmFormV2Dtl)
        {
            IQueryable<RmFormV2Lab> gridData;
            gridData = _context.RmFormV2Lab.Where(s => s.Fv2lActiveYn == true)
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

        public async Task<RmFormV2Lab> DetailView(RmFormV2Lab rmFormV2Dtl)
        {
            var editDetail = await _context.Set<RmFormV2Lab>().FirstOrDefaultAsync(a => a.Fv2lPkRefNo == rmFormV2Dtl.Fv2lPkRefNo);
            return editDetail;
        }

        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions, string id)
        {
            var query = (from x in _context.RmFormV2Lab where x.Fv2lActiveYn == true select x).Where(x => x.Fv2lFv2hPkRefNo == Convert.ToInt32(id));
            PrepareFilterQuery(filterOptions, ref query);
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<RmFormV2Lab>> GetFilteredRecordList(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions, string id)
        {
            List<RmFormV2Lab> result = new List<RmFormV2Lab>();
            var query = (from x in _context.RmFormV2Lab where x.Fv2lActiveYn == true select x).Where(x => x.Fv2lFv2hPkRefNo == Convert.ToInt32(id));
            PrepareFilterQuery(filterOptions, ref query);
            result = await query.OrderBy(x => x.Fv2lPkRefNo)
                                .Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            return result;
        }

        private void PrepareFilterQuery(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions, ref IQueryable<RmFormV2Lab> query)
        {
            query = query.Where(x => x.Fv2lActiveYn == true);
        }

        public async Task<RmFormV2Lab> GetFormWithDetailsByNoAsync(int formNo)
        {
            return await _context.RmFormV2Lab
                        .FirstOrDefaultAsync(x => x.Fv2lPkRefNo == formNo);
        }

        public async Task<List<RmFormV2Lab>> GetAllLabourById(int headerId)
        {
            return await _context.RmFormV2Lab.Where(x => x.Fv2lFv2hPkRefNo == headerId).ToListAsync();
        }
    }
}
