using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.Models;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository
{
    public interface IFormADtlRepository : IRepositoryBase<RmFormADtl>
    {
        int SaveFormADtl(RmFormADtl rmFormADtl);
        IQueryable<RmFormADtl> GetGridData(RmFormADtl rmFormADtl);
        IQueryable<RmFormAHdr> GetGridHdrData(RmFormAHdr rmFormADtl);
        Task<RmFormADtl> DetailView(RmFormADtl rmFormADtl);

        Task<List<RmFormADtl>> GetAllDtlById(int headerId);
        Task<IEnumerable<object>> GetActiveRefIDs(string activityCode, string roadCode, int fromCHKM, string fromCHM, int toCHKM, string toCHM);


    }
    public class FormADtlRepository : RepositoryBase<RmFormADtl>, IFormADtlRepository
    {
        public FormADtlRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }
        public int SaveFormADtl(RmFormADtl rmFormADtl)
        {
            try
            {
                _context.Entry<RmFormADtl>(rmFormADtl).State = rmFormADtl.FadPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                return 500;
            }
        }
        public IQueryable<RmFormADtl> GetGridData(RmFormADtl rmFormADtl)
        {
            IQueryable<RmFormADtl> gridData;
            gridData = _context.RmFormADtl
                .AsNoTracking();

            return gridData;
        }

        public IQueryable<RmFormAHdr> GetGridHdrData(RmFormAHdr rmFormAHdr)
        {
            IQueryable<RmFormAHdr> gridData;
            gridData = _context.RmFormAHdr
                .AsNoTracking();

            return gridData;
        }

        public async Task<RmFormADtl> DetailView(RmFormADtl rmFormADtl)
        {
            var editDetail = await _context.Set<RmFormADtl>().FirstOrDefaultAsync(a => a.FadPkRefNo == rmFormADtl.FadPkRefNo);
            return editDetail;
        }

        public bool SubmitToFormH(int fhhFadPkRefNo)
        {
            var detail = _context.RmFormADtl.FirstOrDefault(s => s.FadPkRefNo == fhhFadPkRefNo);
            if (detail != null)
            {
                detail.FadFormhApp = "Yes";
            }
            return true;
        }
        public async Task<IEnumerable<object>> GetActiveRefIDs(string activityCode, string roadCode, int fromCHKM, string fromCHM, int toCHKM, string toCHM)
        {
            return await _context.RmFormADtl.Include(x=> x.FadFahPkRefNoNavigation).Where(x => x.FadActiveYn == true && x.FadActCode == activityCode && x.FadFrmCh == fromCHKM && x.FadFrmChDeci == fromCHM && x.FadToCh == toCHKM && x.FadToChDeci == toCHM && x.FadFahPkRefNoNavigation.FahRoadCode==roadCode).Select(x => new { FadPkRefNo = x.FadPkRefNo, FadRefId = x.FadRefId, FadSiteRef = x.FadSiteRef, FadPriority = x.FadPriority, FadCdr = x.FadCdr, FadAdp = x.FadAdp }).ToListAsync();
        }

        public async Task<List<RmFormADtl>> GetAllDtlById(int headerId)
        {
            return await _context.RmFormADtl.Where(x => x.FadFahPkRefNo == headerId && x.FadActiveYn == true).ToListAsync();
        }
    }
}
