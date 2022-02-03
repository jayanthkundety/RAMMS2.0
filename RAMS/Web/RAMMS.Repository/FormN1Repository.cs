 using Microsoft.EntityFrameworkCore;
using RAMMS.Common.Extensions;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
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
    public class FormN1Repository : RepositoryBase<RmFormN1Hdr>, IFormN1Repository
    {
        public FormN1Repository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<bool> CheckHdrRefereceId(string id)
        {
            var obj = await _context.RmFormN1Hdr.Where(x => x.FnihNcnNo == id && x.FnihActiveYn == true).ToListAsync();
            return obj.Count >= 1 ? true : false;
        }
        public async Task<bool> CheckHdrRefereceNo(string id)
        {
            var obj = await _context.RmFormN1Hdr.Where(x => x.FnihRefId == id && x.FnihActiveYn == true).ToListAsync();
            return obj.Count >= 1 ? true : false;
        }
        public async Task<int> CheckwithRef(FormN1HeaderRequestDTO formNiHeader)
        {
            var data = await _context.RmFormN1Hdr.Where(x => x.FnihPkRefNo == formNiHeader.No).FirstOrDefaultAsync();
            if (data != null)
            {
                return data.FnihPkRefNo;
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

        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormN1SearchGridDTO> filterOptions)
        {
            var query = (from x in _context.RmFormN1Hdr select x);
            PrepareFilterQuery(filterOptions, ref query);
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<RmFormN1Hdr>> GetFilteredRecordList(FilteredPagingDefinition<FormN1SearchGridDTO> filterOptions)
        {
            List<RmFormN1Hdr> result = new List<RmFormN1Hdr>();
            var query = (from x in _context.RmFormN1Hdr select x);

            PrepareFilterQuery(filterOptions, ref query);

            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderBy(s => s.FnihNcnNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(s => s.FnihRmu);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(s => s.FnihRoadCode);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(s => s.FnihRoadName);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(s => s.FnihFrmCh);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(s => s.FnihCorrectionTkn);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(s => s.FnihNcrIssue);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderBy(s => s.FnihUsernameVer);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderBy(s => s.FnihSubmitSts);
            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderByDescending(s => s.FnihNcnNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(s => s.FnihRmu);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(s => s.FnihRoadCode);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(s => s.FnihRoadName);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(s => s.FnihFrmCh);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(s => s.FnihCorrectionTkn);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(s => s.FnihNcrIssue);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderByDescending(s => s.FnihUsernameVer);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderByDescending(s => s.FnihSubmitSts);
            }

            result = await query
                            .Skip(filterOptions.StartPageNo)
                            .Take(filterOptions.RecordsPerPage)
                            .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<RmFormQa2Dtl>> GetFormQA2ReferenceId(string rodeCode)
        {
            return await _context.RmFormQa2Dtl.Where(x=>x.FqaiidActiveYn==true).OrderByDescending(x =>x.FqaiidPkRefNo).ToListAsync();
        }
        public async Task<IEnumerable<RmFormS1Dtl>> GetFormS1ReferenceId(string rodeCode)
        {
            if (rodeCode == "" || rodeCode == null)
                return await _context.RmFormS1Dtl.Where(x=>x.FsidActiveYn==true).OrderByDescending(x => x.FsidPkRefNo).ToListAsync();
            else
                return await _context.RmFormS1Dtl.Where(x => x.FsiidRoadCode == rodeCode && x.FsidActiveYn == true).ToListAsync();
        }

        public async Task<RmFormS1Hdr> GetFormS1Data(int id)
        {
            var s1Hdr = await (from x in _context.RmFormS1Dtl
                              join h in _context.RmFormS1Hdr on x.FsidFsihPkRefNo equals h.FsihPkRefNo
                              where x.FsidPkRefNo == id && x.FsidActiveYn==true 
                              select new RmFormS1Hdr
                              {
                                  FsihRmu = h.FsihRmu

                              }).FirstOrDefaultAsync();
            var s1Dtl = await (from x in _context.RmFormS1Dtl
                               where x.FsidPkRefNo == id && x.FsidActiveYn == true
                               select new RmFormS1Dtl
                               {
                                   FsiidRoadCode=x.FsiidRoadCode,
                                   FsiidRoadName=x.FsiidRoadName,
                                   FsidFrmChKm=x.FsidFrmChKm,
                                   FsidFrmChM=x.FsidFrmChM,
                                   FsidToChKm=x.FsidToChKm,
                                   FsidToChM=x.FsidToChM
                               }).ToListAsync();
            s1Hdr.RmFormS1Dtl = s1Dtl;
            return s1Hdr;
        }

        public async Task<FormQa2HeaderResponseDTO> GetFormQa2Data(int id)
        {
            var data = await (from x in _context.RmFormQa2Dtl
                              join h in _context.RmFormQa2Hdr on x.FqaiidFqaiihPkRefNo equals h.FqaiihPkRefNo
                              where x.FqaiidPkRefNo == id
                              select new FormQa2HeaderResponseDTO
                              {
                                  Rmu = h.FqaiihRmu,
                                  RoadCode = h.FqaiihRoadCode,
                                  RoadName = h.FqaiihRoadName
                              }).FirstOrDefaultAsync();

            var data2 = await (from x in _context.RmFormQa2Dtl
                               where x.FqaiidPkRefNo == id
                               select new FormQa2DtlResponseDTO
                               {
                                   FrmCh = x.FqaiidFrmCh,
                                   FrmChDeci = x.FqaiidFrmChDeci,
                                   ToCh = x.FqaiidToCh,
                                   ToChDeci = x.FqaiidToChDeci
                               }).ToListAsync();
            data.FormQa2Details = data2;

            return data;
        }

        public RmFormN1Hdr GetRmFormN1Hdr(RmFormN1Hdr rmFormN1Hdr)
        {
            var frmInst = new RmFormN1Hdr();
            var userInst = _context.Set<RmFormN1Hdr>()
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

        public int SaveFormN1Hdr(RmFormN1Hdr rmFormN1Hdr)
        {
            try
            {
                _context.Entry<RmFormN1Hdr>(rmFormN1Hdr).State = rmFormN1Hdr.FnihPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return rmFormN1Hdr.FnihPkRefNo;
            }
            catch (Exception)
            {
                return 500;

            }
        }

        public async Task<RmFormN1Hdr> GetFormWithDetailsByNoAsync(int formNo)
        {
            return await _context.RmFormN1Hdr
                        .FirstOrDefaultAsync(x => x.FnihPkRefNo == formNo);
        }

        private void PrepareFilterQuery(FilteredPagingDefinition<FormN1SearchGridDTO> filterOptions, ref IQueryable<RmFormN1Hdr> query)
        {
            query = query.Where(x => x.FnihActiveYn == true);
            if (filterOptions.Filters != null)
            {
                if (!string.IsNullOrEmpty(filterOptions.Filters.Road_Code))
                {
                    query = query.Where(x => x.FnihRoadCode == filterOptions.Filters.Road_Code);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
                {
                    query = query.Where(x => x.FnihRmu == filterOptions.Filters.RMU);
                }

                if (filterOptions.Filters.IssueMonth.HasValue)
                {
                    query = query.Where(x => x.FnihDtIssue.Value.Month == filterOptions.Filters.IssueMonth);
                }

                if (filterOptions.Filters.IssueFrom.HasValue || filterOptions.Filters.IssueTo.HasValue)
                {
                    query = query.Where(x => (x.FnihDtIssue >= (filterOptions.Filters.IssueFrom ?? x.FnihDtIssue) && x.FnihDtIssue <= (filterOptions.Filters.IssueTo ?? x.FnihDtIssue)));
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    query = query.Where(x => x.FnihRmu.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FnihRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FnihNcnNo.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FnihNcrIssue == true && (filterOptions.Filters.SmartInputValue).ToLower() == "yes"
                                        || x.FnihNcrIssue == false && (filterOptions.Filters.SmartInputValue).ToLower() == "no"
                                        || x.FnihCorrectionTkn == true && (filterOptions.Filters.SmartInputValue).ToLower() == "yes"
                                        || x.FnihCorrectionTkn == false && (filterOptions.Filters.SmartInputValue).ToLower() == "no"
                                        || x.FnihRmu.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FnihRoadCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FnihRoadName.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FnihUsernameIssued.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FnihUsernameVer.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FnihUsernameAccptd.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FnihUsernameCc.Contains(filterOptions.Filters.SmartInputValue)
                                        || ((x.FnihFrmCh.HasValue ? x.FnihFrmCh.Value : 0).ToString() +
                                        "."
                                        + x.FnihFrmChDeci)
                                        .Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.FnihSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue)
                                        || (filterOptions.Filters.SmartInputValue.IsInt() && x.FnihPkRefNo.Equals(filterOptions.Filters.SmartInputValue.AsInt())));

                }
            }
        }

        public (int id, bool aleadyExists) CheckExistence(string rdCode, int month, int year)
        {
            var result = _context.RmFormN1Hdr
                .FirstOrDefault(s =>
            s.FnihRoadCode == rdCode &&
           (s.FnihDtIssue.HasValue ? s.FnihDtIssue.Value.Month : 0) == month &&
           (s.FnihDtIssue.HasValue ? s.FnihDtIssue.Value.Year : 0) == year);
            if (result != null)
            {
                return (result.FnihPkRefNo, true);
            }
            else
            {
                var d = _context.RmFormN1Hdr.OrderByDescending(s => s.FnihPkRefNo).FirstOrDefault();
                if (d != null)
                    return (d.FnihPkRefNo + 1, false);

                else
                {
                    return (1, false);
                }
            }
        }

        public async Task<int> GetMaxCount()
        {
            var count = await _context.RmFormN1Hdr.Select(m => m.FnihPkRefNo).CountAsync();
            if (count > 0)
            {
                var query = (from x in _context.RmFormN1Hdr select x);
                return await query.MaxAsync(m => m.FnihPkRefNo);
            }
            return 0;
        }

        public FORMN1Rpt GetReportData(int headerId, int pageIndex, int pageCount)
        {
            FORMN1Rpt result = (from o in _context.RmFormN1Hdr.AsEnumerable()
                                let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeCode == o.FnihRmu || s.DdlTypeValue == o.FnihRmu))
                                let road = _context.RmRoadMaster.FirstOrDefault(s => s.RdmRdCode == o.FnihRoadCode)
                                where o.FnihPkRefNo == headerId
                                select new FORMN1Rpt
                                {
                                    AcceptedBy = o.FnihUsernameAccptd,
                                    AcceptedDate = o.FnihDtAccptd.HasValue ? o.FnihDtAccptd.Value.ToString("dd-MM-yyyy") : "",
                                    Attn = o.FnihUsernameAttnTo,
                                    CC = o.FnihUsernameCc,
                                    Chainage = $"{o.FnihFrmCh.GetValueOrDefault()}+{o.FnihFrmChDeci.GetValueOrDefault()} TO {o.FnihToCh.GetValueOrDefault()}+{o.FnihToChDeci.GetValueOrDefault()}",
                                    CompletedBy = o.FnihUsernameCorrective,
                                    CompletedDate = o.FnihDtCorrective.HasValue ? o.FnihDtCorrective.Value.ToString("dd-MM-yyyy") : "",
                                    Deduction = o.FnihOthrFllwAct == "Deduction" ? "✔" : "",
                                    DescriptionOfNC = o.FnihNcDesc,
                                    Division = road != null ? road.RdmDivCode : o.FnihDiv,
                                    IsCorrectionTaken = o.FnihCorrectionTkn.GetValueOrDefault() ? "✔" : "✖",
                                    CorrectToBeTakenBefore = o.FnihCrctTknBef.HasValue ? o.FnihCrctTknBef.Value.ToString("dd-MM-yyyy") : "",
                                    IsNCRIssue = o.FnihNcrIssue.GetValueOrDefault() ? "✔" : "✖",
                                    IssueDate = o.FnihDtIssue.HasValue ? o.FnihDtIssue.Value.ToString("dd-MM-yyyy") : "",
                                    IssuedBy = o.FnihUsernameIssued,
                                    NCNNo = o.FnihRefId,
                                    NCRIssueDate = o.FnihNcrIssue.GetValueOrDefault() ? (o.FnihIssueDt.HasValue ? o.FnihIssueDt.Value.ToString("dd-MM-yyyy") : "") : "",
                                    Penalty = o.FnihOthrFllwAct == "Penalty" ? "✔" : "",
                                    ReceivedBy = o.FnihUsernameRcvd,
                                    Remarks = o.FnihRemarks,
                                    ReworkSpecification = o.FnihProposedRewrkSpec,
                                    RMU = rmu.DdlTypeDesc.ToUpper(),
                                    RoadCode = o.FnihRoadCode,
                                    RoadName = o.FnihRoadName,
                                    ServiceProvider = o.FnihSrProvider,
                                    VerifiedBy = o.FnihUsernameVer,
                                    WarningLetter = o.FnihOthrFllwAct == "Warning Letter" ? "✔" : ""
                                }).FirstOrDefault();
            return result;
        }

        public async Task<int> GetActiveCount()
        {
            return await _context.RmFormN1Hdr.Where(x => x.FnihActiveYn == true).CountAsync();
        }

        public async Task<int> GetActiveRmuBasedCount(string rmu)
        {
            return await _context.RmFormN1Hdr.Where(x => x.FnihRmu == rmu && x.FnihActiveYn == true).CountAsync();
        }

        public async Task<int> GetActiveRdCodeCount(List<string> rdCode)
        {
            int NcNCount = 0;
            try
            {
                foreach (var code in rdCode)
                {
                    int ct = await _context.RmFormN1Hdr.Where(x => x.FnihRoadCode == code && x.FnihActiveYn == true).CountAsync();
                    NcNCount += ct;
                }
                return NcNCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
