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
    public interface IFormDLabourRepository : IRepositoryBase<RmFormDLabourDtl>
    {
        int SaveFormDDtl(RmFormDLabourDtl _RmFormDDtl);
        IQueryable<RmFormDLabourDtl> GetGridData(RmFormDLabourDtl _RmFormDDtl);
        IQueryable<RmFormDHdr> GetGridHdrData(RmFormDHdr _RmFormDDtl);
        Task<RmFormDLabourDtl> DetailView(RmFormDLabourDtl rmFormDDtl);

        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id);
        Task<List<RmFormDLabourDtl>> GetFilteredRecordList(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id);

        Task<RmFormDLabourDtl> GetFormWithDetailsByNoAsync(int formNo);

        Task<List<RmFormDLabourDtl>> GetAllLabourById(int headerId);
    }
    public class FormDLabourRepository : RepositoryBase<RmFormDLabourDtl>, IFormDLabourRepository
    {
        public FormDLabourRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }
        public int SaveFormDDtl(RmFormDLabourDtl rmFormDDtl)
        {
            try
            {
                _context.Entry<RmFormDLabourDtl>(rmFormDDtl).State = rmFormDDtl.FdldPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                return 500;
            }
        }
        public IQueryable<RmFormDLabourDtl> GetGridData(RmFormDLabourDtl rmFormDDtl)
        {
            IQueryable<RmFormDLabourDtl> gridData;
            gridData = _context.RmFormDLabourDtl.Where(s => s.FdldActiveYn == true)
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

        public async Task<RmFormDLabourDtl> DetailView(RmFormDLabourDtl rmFormDDtl)
        {
            var editDetail = await _context.Set<RmFormDLabourDtl>().FirstOrDefaultAsync(a => a.FdldPkRefNo == rmFormDDtl.FdldPkRefNo);
            return editDetail;
        }

        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id)
        {
            var query = (from x in _context.RmFormDLabourDtl where x.FdldActiveYn == true select x).Where(x => x.FdldFdhPkRefNo == Convert.ToInt32(id));
            PrepareFilterQuery(filterOptions, ref query);
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<RmFormDLabourDtl>> GetFilteredRecordList(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id)
        {
            List<RmFormDLabourDtl> result = new List<RmFormDLabourDtl>();
            var query = (from x in _context.RmFormDLabourDtl where x.FdldActiveYn == true select x).Where(x => x.FdldFdhPkRefNo == Convert.ToInt32(id));
            PrepareFilterQuery(filterOptions, ref query);
            result = await query.OrderBy(x => x.FdldPkRefNo)
                                .Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            return result;
        }

        private void PrepareFilterQuery(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, ref IQueryable<RmFormDLabourDtl> query)
        {
            query = query.Where(x => x.FdldActiveYn == true);
        }

        public async Task<RmFormDLabourDtl> GetFormWithDetailsByNoAsync(int formNo)
        {
            return await _context.RmFormDLabourDtl
                        .FirstOrDefaultAsync(x => x.FdldPkRefNo == formNo);
        }

        public async Task<List<RmFormDLabourDtl>> GetAllLabourById(int headerId)
        {
            return await _context.RmFormDLabourDtl.Where(x => x.FdldFdhPkRefNo == headerId).ToListAsync();
        }
    }
}
