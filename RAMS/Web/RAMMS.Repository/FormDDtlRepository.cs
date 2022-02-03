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
    public interface IFormDDtlRepository : IRepositoryBase<RmFormDDtl>
    {
        int SaveFormDDtl(RmFormDDtl _RmFormDDtl);
        IQueryable<RmFormDDtl> GetGridData(RmFormDDtl _RmFormDDtl);
        IQueryable<RmFormDHdr> GetGridHdrData(RmFormDHdr _RmFormDDtl);
        Task<RmFormDDtl> DetailView(RmFormDDtl rmFormDDtl);

        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id);
        Task<List<RmFormDDtl>> GetFilteredRecordList(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id);

        Task<RmFormDDtl> GetFormWithDetailsByNoAsync(int formNo);
        Task<List<RmFormDDtl>> GetAllDtlById(int headerId);
        Task<(int detId, int wkDtlId, int wkDtlPlanned, bool alreadyExists)> CheckAlreadyExistsS1Det(string roadCode, string actCode, int frmCh, string frmChDeci, int toCh, string toChDeci, int crewUnit, int weekNo, int dayOfWeek);
    }
    public class FormDDtlRepository : RepositoryBase<RmFormDDtl>, IFormDDtlRepository
    {
        public FormDDtlRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }
        public int SaveFormDDtl(RmFormDDtl rmFormDDtl)
        {
            try
            {
                _context.Entry<RmFormDDtl>(rmFormDDtl).State = rmFormDDtl.FddPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                return 500;
            }
        }
        public IQueryable<RmFormDDtl> GetGridData(RmFormDDtl rmFormDDtl)
        {
            IQueryable<RmFormDDtl> gridData;
            gridData = _context.RmFormDDtl.Where(s => s.FddActiveYn == true)
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

        public async Task<RmFormDDtl> DetailView(RmFormDDtl rmFormDDtl)
        {
            var editDetail = await _context.Set<RmFormDDtl>().FirstOrDefaultAsync(a => a.FddPkRefNo == rmFormDDtl.FddPkRefNo);
            return editDetail;
        }

        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id)
        {
            var query = (from x in _context.RmFormDDtl where x.FddActiveYn == true select x).Where(x => x.FddFdhPkRefNo == Convert.ToInt32(id));
            PrepareFilterQuery(filterOptions, ref query);
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<RmFormDDtl>> GetFilteredRecordList(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id)
        {
            List<RmFormDDtl> result = new List<RmFormDDtl>();
            var query = (from x in _context.RmFormDDtl where x.FddActiveYn == true select x).Where(x => x.FddFdhPkRefNo == Convert.ToInt32(id));
            PrepareFilterQuery(filterOptions, ref query);
            result = await query.OrderBy(x => x.FddPkRefNo)
                                .Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            return result;
        }

        private void PrepareFilterQuery(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, ref IQueryable<RmFormDDtl> query)
        {
            query = query.Where(x => x.FddActiveYn == true);
        }

        public async Task<RmFormDDtl> GetFormWithDetailsByNoAsync(int formNo)
        {
            return await _context.RmFormDDtl
                      .FirstOrDefaultAsync(x => x.FddPkRefNo == formNo);
        }

        public RmFormDDtl GetTop()
        {
            return _context.RmFormDDtl.Where(s => s.FddActiveYn == true).FirstOrDefault();
        }

        public async Task<List<RmFormDDtl>> GetAllDtlById(int headerId)
        {
            return await _context.RmFormDDtl.Where(x => x.FddActiveYn == true && x.FddFdhPkRefNo == headerId).ToListAsync();
        }

        public async Task<(int detId, int wkDtlId, int wkDtlPlanned, bool alreadyExists)> CheckAlreadyExistsS1Det(string roadCode, string actCode, int frmCh, string frmChDeci, int toCh, string toChDeci, int crewUnit, int weekNo, int dayOfWeek)
        {

            var isexistsDtlId = await (from a in _context.RmFormS1Hdr
                                       join b in _context.RmFormS1Dtl on a.FsihPkRefNo equals b.FsidFsihPkRefNo
                                       where b.FsidCrewSupervisor == crewUnit && a.FsihWeekNo == weekNo && a.FsihActiveYn == true
                                       && b.FsiidRoadCode == roadCode && b.FsidActCode == actCode && b.FsidFrmChKm == frmCh
                                       && b.FsidFrmChM == frmChDeci && b.FsidToChKm == toCh && b.FsidToChM == toChDeci
                                       && b.FsidActiveYn == true
                                       select b.FsidPkRefNo).FirstOrDefaultAsync();
            if (isexistsDtlId > 0)
            {
                var isexistsWkdtl = await (from c in _context.RmFormS1WkDtl.Where(x => x.FsiwdFsidPkRefNo == isexistsDtlId && x.FsiwdSchldDayOfWeek == dayOfWeek)
                                           select new { c.FsiwdPkRefNo, c.FsiwdPlanned }).FirstOrDefaultAsync();

                if (isexistsWkdtl != null)
                {
                    return (isexistsDtlId, isexistsWkdtl.FsiwdPkRefNo, Convert.ToInt32(isexistsWkdtl.FsiwdPlanned), true);
                }
                else
                {
                    return (isexistsDtlId, 0, 0, false);
                }
            }
            return (0, 0, 0, false);
            
        }
        public async Task<RmFormDHdr> getFromDhdr(int hdrRefNo)
        {
            var result = await _context.RmFormDHdr.Where(x => x.FdhPkRefNo == hdrRefNo).FirstOrDefaultAsync();
            return result;
        }


    }
}
