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
    public interface IFormDMaterialRepository : IRepositoryBase<RmFormDMaterialDtl>
    {
        int SaveFormDMaterial(RmFormDMaterialDtl _RmFormDMaterial);
        IQueryable<RmFormDMaterialDtl> GetGridData(RmFormDMaterialDtl _RmFormDMaterial);
        IQueryable<RmFormDHdr> GetGridHdrData(RmFormDHdr _RmFormDMaterial);
        Task<RmFormDMaterialDtl> DetailView(RmFormDMaterialDtl rmFormDMaterial);

        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id);
        Task<List<RmFormDMaterialDtl>> GetFilteredRecordList(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id);

        Task<RmFormDMaterialDtl> GetFormWithDetailsByNoAsync(int formNo);
        Task<List<RmFormDMaterialDtl>> GetAllMaterialById(int headerId);
    }
    public class FormDMaterialRepository : RepositoryBase<RmFormDMaterialDtl>, IFormDMaterialRepository
    {
        public FormDMaterialRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }
        public int SaveFormDMaterial(RmFormDMaterialDtl rmFormDMaterial)
        {
            try
            {
                _context.Entry<RmFormDMaterialDtl>(rmFormDMaterial).State = rmFormDMaterial.FdmdPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                return 500;
            }
        }
        public IQueryable<RmFormDMaterialDtl> GetGridData(RmFormDMaterialDtl rmFormDMaterial)
        {
            IQueryable<RmFormDMaterialDtl> gridData;
            gridData = _context.RmFormDMaterialDtl.Where(s => s.FdmdActiveYn == true)
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

        public async Task<RmFormDMaterialDtl> DetailView(RmFormDMaterialDtl rmFormDMaterial)
        {
            var editDetail = await _context.Set<RmFormDMaterialDtl>().FirstOrDefaultAsync(a => a.FdmdPkRefNo == rmFormDMaterial.FdmdPkRefNo);
            return editDetail;
        }

        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id)
        {
            var query = (from x in _context.RmFormDMaterialDtl where x.FdmdActiveYn == true select x).Where(x => x.FdmdFdhPkRefNo == Convert.ToInt32(id));
            PrepareFilterQuery(filterOptions, ref query);
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<RmFormDMaterialDtl>> GetFilteredRecordList(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id)
        {
            List<RmFormDMaterialDtl> result = new List<RmFormDMaterialDtl>();
            var query = (from x in _context.RmFormDMaterialDtl where x.FdmdActiveYn == true select x).Where(x => x.FdmdFdhPkRefNo == Convert.ToInt32(id));
            PrepareFilterQuery(filterOptions, ref query);
            result = await query.OrderBy(x => x.FdmdPkRefNo).Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            return result;
        }

        private void PrepareFilterQuery(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, ref IQueryable<RmFormDMaterialDtl> query)
        {
            query = query.Where(x => x.FdmdActiveYn == true);
        }

        public async Task<RmFormDMaterialDtl> GetFormWithDetailsByNoAsync(int formNo)
        {
            return await _context.RmFormDMaterialDtl
                       .FirstOrDefaultAsync(x => x.FdmdPkRefNo == formNo);
        }

        public async Task<List<RmFormDMaterialDtl>> GetAllMaterialById(int headerId)
        {
            return await _context.RmFormDMaterialDtl.Where(x => x.FdmdFdhPkRefNo == headerId).ToListAsync();
        }
    }
}
