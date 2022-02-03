using Microsoft.EntityFrameworkCore;
using RAMMS.Common.Extensions;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
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
    public class FormN2Repository : RepositoryBase<RmFormN2Hdr>, IFormN2Repository
    {
        public FormN2Repository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public (int id, bool aleadyExists) CheckExistence(string rdCode, int month, int year)
        {
            var result = _context.RmFormN2Hdr
                .FirstOrDefault(s =>
           (s.FnihCrDt.HasValue ? s.FnihCrDt.Value.Month : 0) == month &&
           (s.FnihCrDt.HasValue ? s.FnihCrDt.Value.Year : 0) == year);
            if (result != null)
            {
                return (result.FnthPkRefNo, true);
            }
            else
            {
                var d = _context.RmFormN2Hdr.OrderByDescending(s => s.FnthPkRefNo).FirstOrDefault();
                if (d != null)
                    return (d.FnthPkRefNo + 1, false);
                else
                {
                    return (1, false);
                }
            }
        }

        public async Task<bool> CheckHdrRefereceId(string id)
        {
            var obj = await _context.RmFormN2Hdr.Where(x => x.FnthNcrNo == id && x.FnihActiveYn == true).ToListAsync();
            return obj.Count >= 1 ? true : false;
        }

        public async Task<int> CheckWithRef(FormN2HeaderRequestDTO formNiHeader)
        {
            var data = await _context.RmFormN2Hdr.Where(x => x.FnthPkRefNo == formNiHeader.No).FirstOrDefaultAsync();
            if (data != null)
            {
                return data.FnthPkRefNo;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<RmDdLookup>> GetDivisions()
        {
            return await _context.RmDdLookup.Where(x => x.DdlType == "Division").ToListAsync();
        }

        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormN2SearchGridDTO> filterOptions)
        {
            var query = (from x in _context.RmFormN2Hdr select x);
            PrepareFilterQuery(filterOptions, ref query);
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<RmFormN2Hdr>> GetFilteredRecordList(FilteredPagingDefinition<FormN2SearchGridDTO> filterOptions)
        {
            List<RmFormN2Hdr> result = new List<RmFormN2Hdr>();
            var query = (from x in _context.RmFormN2Hdr select x);

            PrepareFilterQuery(filterOptions, ref query);

            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderBy(s => s.FnthNcrNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(s => s.FnthRmu);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(s => s.FnthFnihPkRefNoNavigation.FnihRoadCode);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(s => s.FnthFnihPkRefNoNavigation.FnihRoadName);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(s => s.FnthFnihPkRefNoNavigation.FnihFrmCh).ThenBy(s => s.FnthFnihPkRefNoNavigation.FnihFrmChDeci);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(s => s.FnthUsernameVer);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(s => s.FnihSubmitSts);
            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderByDescending(s => s.FnthNcrNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(s => s.FnthRmu);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(s => s.FnthFnihPkRefNoNavigation.FnihRoadCode);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(s => s.FnthFnihPkRefNoNavigation.FnihRoadName);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(s => s.FnthFnihPkRefNoNavigation.FnihFrmCh).ThenBy(s => s.FnthFnihPkRefNoNavigation.FnihFrmChDeci);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(s => s.FnthUsernameVer);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(s => s.FnihSubmitSts);
            }

            result = await query
                               .Include(x => x.FnthFnihPkRefNoNavigation)
                               .Skip(filterOptions.StartPageNo)
                               .Take(filterOptions.RecordsPerPage)
                               .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<RmFormN1Hdr>> GetFormN1ReferenceId(bool active)
        {
            return await _context.RmFormN1Hdr.Where(m => m.FnihActiveYn == active && m.FnihNcrIssue == true).ToListAsync();
        }

        public async Task<IEnumerable<RmFormN2Hdr>> GetFormN2ReferenceId(bool active)
        {
            return await _context.RmFormN2Hdr.Where(x => x.FnihActiveYn == active).ToListAsync();  
        }

        public async Task<RmFormN2Hdr> GetFormWithDetailsByNoAsync(int formNo)
        {
            return await _context.RmFormN2Hdr
                         .FirstOrDefaultAsync(x => x.FnthPkRefNo == formNo);
        }

        public RmFormN2Hdr GetRmFormN2Hdr(RmFormN2Hdr rmFormN2Hdr)
        {
            var frmInst = new RmFormN2Hdr();
            var userInst = _context.Set<RmFormN2Hdr>()
                     .AsNoTracking();
            return frmInst;
        }

        public async Task<IEnumerable<RmDdLookup>> GetRMU()
        {
            return await _context.RmDdLookup.Where(x => x.DdlType == "RMU").ToListAsync();
        }

        public async Task<IEnumerable<RmRoadMaster>> GetRoadCodes()
        {
            return await _context.RmRoadMaster.Where(x => x.RdmActiveYn == true).ToListAsync();
        }

        public async Task<IEnumerable<RmRoadMaster>> GetRoadCodesByRMU(string rmu)
        {
            if (rmu == "" || rmu == null)
                return await _context.RmRoadMaster.Where(x => x.RdmActiveYn == true).ToListAsync();
            else
                return await _context.RmRoadMaster.Where(x => x.RdmActiveYn == true && x.RdmRmuCode == rmu).ToListAsync();
        }

        public async Task<List<string>> GetSectionByRMU(string rmu)
        {

            var query = (from section in _context.RmFormAHdr
                         where section.FahRmu == rmu && section.FahActiveYn == true
                         select section.FahSection).ToListAsync().ConfigureAwait(false);
            return await query;
        }

        public async Task<IEnumerable<RmDdLookup>> GetSectionCode()
        {
            return await _context.RmDdLookup.Where(x => x.DdlType == "Section Code").ToListAsync();
        }

        public int SaveFormN2Hdr(RmFormN2Hdr rmFormN2Hdr)
        {
            try
            {
                _context.Entry<RmFormN2Hdr>(rmFormN2Hdr).State = rmFormN2Hdr.FnthPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return rmFormN2Hdr.FnthPkRefNo;
            }
            catch (Exception)
            {
                return 500;

            }
        }

        private void PrepareFilterQuery(FilteredPagingDefinition<FormN2SearchGridDTO> filterOptions, ref IQueryable<RmFormN2Hdr> query)
        {
            query = query.Where(x => x.FnihActiveYn == true);
            if (filterOptions.Filters != null)
            {
                if (!string.IsNullOrEmpty(filterOptions.Filters.Road_Code))
                {
                    query = query.Where(x => x.FnthFnihPkRefNoNavigation.FnihRoadCode == filterOptions.Filters.Road_Code);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RoadName))
                {
                    query = query.Where(x => x.FnthFnihPkRefNoNavigation.FnihRoadName == filterOptions.Filters.RoadName);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
                {
                    query = query.Where(x => x.FnthRmu == filterOptions.Filters.RMU);
                }


                if (filterOptions.Filters.IssueMonth.HasValue)
                {
                    query = query.Where(x => x.FnthIssueDt.Value.Month == filterOptions.Filters.IssueMonth);
                }

                if (filterOptions.Filters.IssueFrom.HasValue || filterOptions.Filters.IssueTo.HasValue)
                {
                    query = query.Where(x => (x.FnthIssueDt >= (filterOptions.Filters.IssueFrom ?? x.FnthIssueDt) && x.FnthIssueDt <= (filterOptions.Filters.IssueTo ?? x.FnthIssueDt)));
                }



                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    query = query.Where(x => x.FnthRmu.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FnthFnihPkRefNoNavigation.FnihRoadCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FnthFnihPkRefNoNavigation.FnihRoadName.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FnthRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FnthNcrNo.Contains(filterOptions.Filters.SmartInputValue)
                                        || ((x.FnthFnihPkRefNoNavigation.FnihFrmCh.HasValue ? x.FnthFnihPkRefNoNavigation.FnihFrmCh.Value : 0).ToString() +
                                        "."
                                        + x.FnthFnihPkRefNoNavigation.FnihFrmChDeci)
                                        .Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.FnihSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FnthUsernameVer.Contains(filterOptions.Filters.SmartInputValue)
                                        || (filterOptions.Filters.SmartInputValue.IsInt() && x.FnthPkRefNo.Equals(filterOptions.Filters.SmartInputValue.AsInt())));

                }
            }
        }

        public async Task<int> GetMaxCount()
        {

            var count = await _context.RmFormN2Hdr.Select(m => m.FnthPkRefNo).CountAsync();
            if (count > 0)
            {
                var query = (from x in _context.RmFormN2Hdr select x);
                return await query.MaxAsync(m => m.FnthPkRefNo);
            }
            return 0;
        }

        public FORMN2Rpt GetReportData(int headerId, int pageIndex, int pageCount)
        {
            FORMN2Rpt result = (from o in _context.RmFormN2Hdr.AsEnumerable()
                                let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeCode == o.FnthRmu || s.DdlTypeValue == o.FnthRmu))
                                where o.FnthPkRefNo == headerId
                                select new FORMN2Rpt
                                {
                                    AcceptedBy = o.FnthUsernameAccptd,
                                    AcceptedDate = o.FnthDtAccptd.HasValue ? o.FnthDtAccptd.Value.ToString("dd-MM-yyyy") : "",
                                    ActionCompletedBy = o.FnthUsernamePreventive,
                                    ActionCompletedDate = o.FnthDtPreventive.HasValue ? o.FnthDtPreventive.Value.ToString("dd-MM-yyyy") : "",
                                    ActionToBeTaken = o.FnthPreventiveAct,
                                    Attn = o.FnthUsernameAttnTo,
                                    CallMeeting = o.FnohOthrFllwAct == "Call Meeting" ? "✔" : "",
                                    CC = o.FnthUsernameCc,
                                    CloseOut = o.FnthCloseOutDt.HasValue ? o.FnthCloseOutDt.Value.ToString("dd/MM") : "",
                                    CloseOutRemarks = o.FnthCloseOutRemarks,
                                    CompletedBy = o.FnthUsernameCorrective,
                                    CompletedDate = o.FnthDtCorrective.HasValue ? o.FnthDtCorrective.Value.ToString("dd-MM-yyyy") : "",
                                    Date = o.FnthCloseOutDt.HasValue ? o.FnthCloseOutDt.Value.ToString("dd-MM-yyyy") : "",
                                    Deduction = o.FnohOthrFllwAct == "Deduction" ? "✔" : "",
                                    Division = o.FnthDiv,
                                    IssueDate = o.FnthIssueDt.HasValue ? o.FnthIssueDt.Value.ToString("dd-MM-yyyy") : "",
                                    IssuedBy = o.FnthUsernameIssued,
                                    NCRNo = o.FnthNcrNo,
                                    NonConferenceRemarks = o.FnthNonConfDtl,
                                    Penalty = o.FnohOthrFllwAct == "Penalty" ? "✔" : "",
                                    ProposedCorrectiveAction = o.FnthProposedCrctAct,
                                    ReceivedBy = o.FnthUsernameRcvd,
                                    ReferenceNo = o.FnthRefId,
                                    Region = o.FnthRegion,
                                    ReIssueNCR = o.FnohOthrFllwAct == "Reissue NCR" ? "✔" : "",
                                    ReportInRTC_NTC = o.FnohOthrFllwAct == "Report In RTC/STC" ? "✔" : "",
                                    RMU = rmu.DdlTypeDesc,
                                    ServiceProvider = o.FnthSrProvider,
                                    Subject = o.FnthSubject,
                                    VerifiedBy = o.FnthUsernameVer,
                                    WarningLetter = o.FnohOthrFllwAct == "Warning Letter" ? "✔" : "",
                                    Close=o.FnohOthrFllwAct== "Close out" ? "✔" : ""
                                }).FirstOrDefault();
            return result;
        }

        public async Task<int> GetActiveCount()
        {
            return await _context.RmFormN2Hdr.Where(x => x.FnihActiveYn == true).CountAsync();
        }

        public async Task<int> GetActiveRmuBasedCount(string rmu)
        {
            return await _context.RmFormN2Hdr.Where(x => x.FnthRmu == rmu && x.FnihActiveYn == true).CountAsync();
        }

        public async Task<int> GetActiveRdCodeCount(List<string> rdCode)
        {
            int NcRCount = 0;
            try
            {
                foreach (var code in rdCode)
                {
                    var ct = await (from a in _context.RmFormN2Hdr
                                    where a.FnihActiveYn == true
                                    from b in _context.RmFormN1Hdr.Where(rd => rd.FnihPkRefNo == a.FnthFnihPkRefNo
                                    && rd.FnihRoadCode == code)
                                    select new
                                    {
                                        a.FnthPkRefNo
                                    }).CountAsync();
                    NcRCount += ct;
                }
                return NcRCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
