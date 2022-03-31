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
    public interface IFormV2MaterialRepository : IRepositoryBase<RmFormV2Mat>
    {
        int SaveFormV2Material(RmFormV2Mat _RmFormV2Mat);
        IQueryable<RmFormV2Mat> GetGridData(RmFormV2Mat _RmFormV2Material);
        IQueryable<RmFormV2Hdr> GetGridHdrData(RmFormV2Hdr _RmFormV2Material);
        Task<RmFormV2Mat> DetailView(RmFormV2Mat rmFormV2Material);

        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions, string id);
        Task<List<RmFormV2Mat>> GetFilteredRecordList(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions, string id);

        Task<RmFormV2Mat> GetFormWithDetailsByNoAsync(int formNo);
        Task<List<RmFormV2Mat>> GetAllMaterialById(int headerId);
    }
    public class FormV2MaterialRepository : RepositoryBase<RmFormV2Mat>, IFormV2MaterialRepository
    {
        public FormV2MaterialRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }
        public int SaveFormV2Material(RmFormV2Mat rmFormV2Material)
        {
            try
            {
                _context.Entry<RmFormV2Mat>(rmFormV2Material).State = rmFormV2Material.Fv2mPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                return 500;
            }
        }
        public IQueryable<RmFormV2Mat> GetGridData(RmFormV2Mat rmFormV2Material)
        {
            IQueryable<RmFormV2Mat> gridData;
            gridData = _context.RmFormV2Mat.Where(s => s.Fv2mActiveYn == true)
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

        public async Task<RmFormV2Mat> DetailView(RmFormV2Mat rmFormV2Material)
        {
            var editDetail = await _context.Set<RmFormV2Mat>().FirstOrDefaultAsync(a => a.Fv2mFv2hPkRefNo == rmFormV2Material.Fv2mFv2hPkRefNo);
            return editDetail;
        }

        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions, string id)
        {
            var query = (from x in _context.RmFormV2Mat where x.Fv2mActiveYn == true select x).Where(x => x.Fv2mFv2hPkRefNo == Convert.ToInt32(id));
            PrepareFilterQuery(filterOptions, ref query);
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<RmFormV2Mat>> GetFilteredRecordList(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions, string id)
        {
            List<RmFormV2Mat> result = new List<RmFormV2Mat>();
            var query = (from x in _context.RmFormV2Mat where x.Fv2mActiveYn == true select x).Where(x => x.Fv2mFv2hPkRefNo == Convert.ToInt32(id));
            PrepareFilterQuery(filterOptions, ref query);
            result = await query.OrderBy(x => x.Fv2mPkRefNo).Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            return result;
        }

        private void PrepareFilterQuery(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions, ref IQueryable<RmFormV2Mat> query)
        {
            query = query.Where(x => x.Fv2mActiveYn == true);
        }

        public async Task<RmFormV2Mat> GetFormWithDetailsByNoAsync(int formNo)
        {
            return await _context.RmFormV2Mat
                       .FirstOrDefaultAsync(x => x.Fv2mPkRefNo == formNo);
        }

        public async Task<List<RmFormV2Mat>> GetAllMaterialById(int headerId)
        {
            return await _context.RmFormV2Mat.Where(x => x.Fv2mFv2hPkRefNo == headerId).ToListAsync();
        }
    }
}
