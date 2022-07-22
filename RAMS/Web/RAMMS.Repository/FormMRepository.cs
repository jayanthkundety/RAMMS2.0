using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using RAMMS.Common;
using Microsoft.EntityFrameworkCore;
using RAMMS.Common.RefNumber;
using RAMMS.DTO.Report;
using RAMMS.DTO.ResponseBO;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO.RequestBO;

namespace RAMMS.Repository
{
    public class FormMRepository : RepositoryBase<RmFormMHdr>, IFormMRepository
    {
        public FormMRepository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<RmFormMHdr> FindDetails(RmFormMHdr frmR1R2)
        {
            //return await _context.RmFormMHdr.Include(x => x.RmFormMAuditDetails).ThenInclude(x => x.FmadFmhPkRefNoNavigation).Where(x => x.Fr1hAssetId == frmR1R2.Fr1hAssetId && x.Fr1hYearOfInsp == frmR1R2.Fr1hYearOfInsp && x.Fr1hActiveYn == true).FirstOrDefaultAsync();
            return await _context.RmFormMHdr.Include(x => x.RmFormMAuditDetails).ThenInclude(x => x.FmadFmhPkRefNoNavigation).Where(x => x.FmhRmuCode == frmR1R2.FmhRmuCode && x.FmhSecCode == frmR1R2.FmhSecCode && x.FmhRdCode == frmR1R2.FmhRdCode && x.FmhActCode == frmR1R2.FmhActCode && x.FmhAuditedDate == frmR1R2.FmhAuditedDate && x.FmhActiveYn == true).FirstOrDefaultAsync();
        }

        public async Task<RmFormMHdr> FindByHeaderID(int headerId)
        {

            return await _context.RmFormMHdr.Include(x => x.RmFormMAuditDetails).ThenInclude(x => x.FmadFmhPkRefNoNavigation).Where(x => x.FmhPkRefNo == headerId && x.FmhActiveYn == true).FirstOrDefaultAsync();
        }

        public async Task<RmFormMHdr> Save(RmFormMHdr frmR1R2, bool updateSubmit)
        {
            bool isAdd = false;
            if (frmR1R2.FmhPkRefNo == 0)
            {
                isAdd = true;
                frmR1R2.FmhActiveYn = true;
                IDictionary<string, string> lstRef = new Dictionary<string, string>();
                //lstRef.Add("Year", Utility.ToString(frmR1R2.Fr1hYearOfInsp));
                //lstRef.Add("AssetID", Utility.ToString(frmR1R2.Fr1hAssetId));
                //frmR1R2.FmhPkRefNo = Common.RefNumber.FormRefNumber.GetRefNumber(FormType.FormR1R2, lstRef);
                _context.RmFormMHdr.Add(frmR1R2);
            }
            else
            {
                string[] arrNotReqUpdate = new string[] { "FmhPkRefNo",
                    "FMHDivCode", "FMHRMUName", "FMHRdCode","FMHRdname"
                };
                //_context.RmFormS1Dtl.Update(formS1Details);
                //var dtls = frmR1R2.RmFormR2Hdr;
                //frmR1R2.RmFormR2Hdr = null;
                _context.RmFormMHdr.Attach(frmR1R2);

                var entry = _context.Entry(frmR1R2);
                entry.Properties.Where(x => !arrNotReqUpdate.Contains(x.Metadata.Name)).ToList().ForEach((p) =>
                {
                    p.IsModified = true;
                });
                if (updateSubmit)
                {
                    entry.Property(x => x.FmhSubmitSts).IsModified = true;
                }
            }
            await _context.SaveChangesAsync();
            if (isAdd)
            {
                IDictionary<string, string> lstData = new Dictionary<string, string>();
                lstData.Add("RoadCode", frmR1R2.FmhRdCode);
                lstData.Add("ActivityCode", frmR1R2.FmhActCode);
                //lstData.Add("Date", Utility.ToString(frmR1R2.FmhAuditedDate, "YYYYMMDD"));
                lstData.Add("Year", frmR1R2.FmhAuditedDate.Value.Year.ToString());
                lstData.Add("MonthNo", frmR1R2.FmhAuditedDate.Value.Month.ToString());
                lstData.Add("Day", frmR1R2.FmhAuditedDate.Value.Day.ToString());
                lstData.Add(FormRefNumber.NewRunningNumber, Utility.ToString(frmR1R2.FmhPkRefNo));
                frmR1R2.FmhRefId = FormRefNumber.GetRefNumber(FormType.FormM, lstData);
                await _context.SaveChangesAsync();
            }
            return frmR1R2;
        }

        public async Task<RmFormMAuditDetails> SaveAD(RmFormMAuditDetails frmR1R2, bool updateSubmit)
        {

            _context.Entry<RmFormMAuditDetails>(frmR1R2).State = frmR1R2.FmadPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
            await _context.SaveChangesAsync();
            return frmR1R2;

        }

        public async Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData)
        {
            var query = (from hdr in _context.RmFormMHdr.Where(s => s.FmhActiveYn == true)
                         from rmu in _context.RmDdLookup.Where(rd => rd.DdlType == "RMU" && (rd.DdlTypeDesc == hdr.FmhRmuName)).DefaultIfEmpty()
                         let rdcode = _context.RmRoadMaster.Where(r => r.RdmRdCode == hdr.FmhRdCode && r.RdmActiveYn == true).DefaultIfEmpty().FirstOrDefault()
                         select new
                         {
                             RefNo = hdr.FmhPkRefNo,
                             RefID = hdr.FmhRefId,
                             AuditDate = hdr.FmhAuditedDate,
                             RMUCode = hdr.FmhRmuCode,
                             RMUDesc = hdr.FmhRmuName,
                             RoadCode = hdr.FmhRdCode,
                             RoadName = hdr.FmhRdName,
                             RoadId = rdcode.RdmRdCdSort,// asset.AiRdmPkRefNoNavigation.RdmRdCdSort,
                             ActivityCode = hdr.FmhActCode,
                             ClosureType = hdr.FmhType,
                             Method = hdr.FmhMethod,
                             Active = hdr.FmhActiveYn,
                             AuditedBy = hdr.FmhUsernameAudit,
                             WitnessedBy = hdr.FmhUsernameWit,
                             Status = (hdr.FmhSubmitSts ? "Submitted" : "Saved"),
                             ProcessStatus = hdr.Fmhstatus
                         });
            query = query.Where(x => x.Active == true);
            if (searchData.filter != null)
            {
                foreach (var item in searchData.filter.Where(x => !string.IsNullOrEmpty(x.Value)))
                {
                    string strVal = Utility.ToString(item.Value).Trim();
                    switch (item.Key)
                    {
                        case "KeySearch":
                            DateTime? dtSearch = Utility.ToDateTime(strVal);
                            query = query.Where(x =>
                                 (x.RefID ?? "").Contains(strVal)
                                 || (x.RMUDesc ?? "").Contains(strVal)
                                 || (x.RMUCode ?? "").Contains(strVal)
                                 || (x.RoadCode ?? "").Contains(strVal)
                                 || (x.RoadName ?? "").Contains(strVal)
                                 || (x.ActivityCode ?? "").Contains(strVal)
                                 );
                            break;
                        case "FromAuditDate":
                            DateTime? dtFrom = Utility.ToDateTime(strVal);
                            string toDate = Utility.ToString(searchData.filter["ToAuditDate"]);
                            if (toDate == "")
                                query = query.Where(x => x.AuditDate >= dtFrom);
                            else
                            {
                                DateTime? dtTo = Utility.ToDateTime(toDate);
                                query = query.Where(x => x.AuditDate >= dtFrom && x.AuditDate <= dtTo);
                            }
                            break;
                        case "ToAuditDate":
                            string frmDate = Utility.ToString(searchData.filter["FromAuditDate"]);
                            if (frmDate == "")
                            {
                                DateTime? dtTo = Utility.ToDateTime(strVal);
                                query = query.Where(x => x.AuditDate <= dtTo);
                            }
                            break;
                        //case "chFromKM":
                        //    string strM = Utility.ToString(searchData.filter["chFromM"]);
                        //    decimal flKm = Utility.ToDecimal(strVal + (strM != "" ? "." + strM : ""));
                        //    query = query.Where(x => x.LocationCH >= flKm);
                        //    break;
                        //case "chFromM":
                        //    string strKm = Utility.ToString(searchData.filter["chFromKM"]);
                        //    if (strKm == "")
                        //    {
                        //        decimal flM = Utility.ToDecimal("0." + strVal);
                        //        query = query.Where(x => x.LocationCH >= flM);
                        //    }
                        //    break;
                        //case "chToKm":
                        //    string strTM = Utility.ToString(searchData.filter["chToM"]);
                        //    decimal flTKm = Utility.ToDecimal(strVal + (strTM != "" ? "." + strTM : ""));
                        //    query = query.Where(x => x.LocationCH <= flTKm);
                        //    break;
                        //case "chToM":
                        //    string strTKm = Utility.ToString(searchData.filter["chToKm"]);
                        //    if (strTKm == "")
                        //    {
                        //        decimal flTM = Utility.ToDecimal("0." + strVal);
                        //        query = query.Where(x => x.LocationCH <= flTM);
                        //    }
                        //    break;
                        //case "FromYear":
                        //    int iFYr = Utility.ToInt(strVal);
                        //    query = query.Where(x => x.Year.HasValue && x.Year >= iFYr);
                        //    break;
                        //case "ToYear":
                        //    int iTYr = Utility.ToInt(strVal);
                        //    query = query.Where(x => x.Year.HasValue && x.Year <= iTYr);
                        //    break;
                        default:
                            query = query.WhereEquals(item.Key, strVal);
                            break;
                    }
                }

            }
            GridWrapper<object> grid = new GridWrapper<object>();
            grid.recordsTotal = await query.CountAsync();
            grid.recordsFiltered = grid.recordsTotal;
            grid.draw = searchData.draw;
            grid.data = await query.Order(searchData, query.OrderByDescending(s => s.RefNo)).Skip(searchData.start)
                                .Take(searchData.length)
                                .ToListAsync(); ;

            return grid;
        }

        public int DeleteHeader(RmFormMHdr frmR1R2)
        {
            _context.RmFormMHdr.Attach(frmR1R2);
            var entry = _context.Entry(frmR1R2);
            entry.Property(x => x.FmhActiveYn).IsModified = true;
            _context.SaveChanges();
            return frmR1R2.FmhPkRefNo;
        }

        public bool isF1Exist(int id)
        {
            var rmF2dtl = _context.RmFormF1Dtl.FirstOrDefault(x => x.Ff1dR1hPkRefNo == id);
            if (rmF2dtl != null)
                return true;

            return false;
        }

        public List<FormMRpt> GetReportData(int headerid)
        {
            return GetReportDataV2(headerid);
        }

        public List<FormMRpt> GetReportDataV2(int headerid)
        {


            List<FormMRpt> detail = (from o in _context.RmFormMHdr
                                         //where (o.Fr1hAiRdCode == roadcode.Fr1hAiRdCode && o.Fr1hDtOfInsp.HasValue && o.Fr1hDtOfInsp < roadcode.Fr1hDtOfInsp) || o.Fr1hPkRefNo == headerid
                                     where o.FmhPkRefNo == headerid
                                     orderby o.FmhAuditedDate ascending
                                     let formR2 = _context.RmFormMAuditDetails.FirstOrDefault(x => x.FmadFmhPkRefNo == o.FmhPkRefNo)
                                     select new FormMRpt
                                     {
                                         Type = o.FmhType,
                                         Method = o.FmhMethod,
                                         Desc = o.FmhDesc,
                                         RdCode = o.FmhRdCode,
                                         RdName = o.FmhRdName,
                                         RmuName = o.FmhRmuName,
                                         DivCode = o.FmhDivCode,
                                         CrewSup = o.FmhCrewSup,
                                         ActName = o.FmhActName,
                                         AuditTimeFrm = o.FmhAuditTimeFrm,
                                         AuditTimeTo = o.FmhAuditTimeTo,
                                         AuditedDate = o.FmhAuditedDate,
                                         AuditedBy = o.FmhAuditedBy,
                                         AuditType = o.FmhAuditType,
                                         A1tallyBox = formR2.FmadA1tallyBox,
                                         A1total = formR2.FmadA1total,
                                         A2tallyBox = formR2.FmadA2tallyBox,
                                         A2total = formR2.FmadA2total,
                                         A3tallyBox = formR2.FmadA3tallyBox,
                                         A3total = formR2.FmadA3total,
                                         A4tallyBox = formR2.FmadA4tallyBox,
                                         A4total = formR2.FmadA4total,
                                         A5tallyBox = formR2.FmadA5tallyBox,
                                         A5total = formR2.FmadA5total,
                                         A6tallyBox = formR2.FmadA6tallyBox,
                                         A6total = formR2.FmadA6total,
                                         A7tallyBox = formR2.FmadA7tallyBox,
                                         A7total = formR2.FmadA7total,
                                         A8tallyBox = formR2.FmadA8tallyBox,
                                         A8total = formR2.FmadA8total,
                                         B1tallyBox = formR2.FmadB1tallyBox,
                                         B1total = formR2.FmadB1total,
                                         B2tallyBox = formR2.FmadB2tallyBox,
                                         B2total = formR2.FmadB2total,
                                         B3tallyBox = formR2.FmadB3tallyBox,
                                         B3total = formR2.FmadB3total,
                                         B4tallyBox = formR2.FmadB4tallyBox,
                                         B4total = formR2.FmadB4total,
                                         B5tallyBox = formR2.FmadB5tallyBox,
                                         B5total = formR2.FmadB5total,
                                         B6tallyBox = formR2.FmadB6tallyBox,
                                         B6total = formR2.FmadB6total,
                                         B7tallyBox = formR2.FmadB7tallyBox,
                                         B7total = formR2.FmadB7total,
                                         B8tallyBox = formR2.FmadB8tallyBox,
                                         B8total = formR2.FmadB8total,
                                         B9tallyBox = formR2.FmadB9tallyBox,
                                         B9total = formR2.FmadB9total,
                                         C1tallyBox = formR2.FmadC1tallyBox,
                                         C1total = formR2.FmadC1total,
                                         C2tallyBox = formR2.FmadC2tallyBox,
                                         C2total = formR2.FmadC2total,
                                         D1tallyBox = formR2.FmadD1tallyBox,
                                         D1total = formR2.FmadD1total,
                                         D2tallyBox = formR2.FmadD2tallyBox,
                                         D2total = formR2.FmadD2total,
                                         D3tallyBox = formR2.FmadD3tallyBox,
                                         D3total = formR2.FmadD3total,
                                         D4tallyBox = formR2.FmadD4tallyBox,
                                         D4total = formR2.FmadD4total,
                                         D5tallyBox = formR2.FmadD5tallyBox,
                                         D5total = formR2.FmadD5total,
                                         D6tallyBox = formR2.FmadD6tallyBox,
                                         D6total = formR2.FmadD6total,
                                         D7tallyBox = formR2.FmadD7tallyBox,
                                         D7total = formR2.FmadD7total,
                                         D8tallyBox = formR2.FmadD8tallyBox,
                                         D8total = formR2.FmadD8total,
                                         E1tallyBox = formR2.FmadE1tallyBox,
                                         E1total = formR2.FmadE1total,
                                         E2tallyBox = formR2.FmadE2tallyBox,
                                         E2total = formR2.FmadE2total,
                                         F1tallyBox = formR2.FmadF1tallyBox,
                                         F1total = formR2.FmadF1total,
                                         F2tallyBox = formR2.FmadF2tallyBox,
                                         F2total = formR2.FmadF2total,
                                         F3tallyBox = formR2.FmadF3tallyBox,
                                         F3total = formR2.FmadF3total,
                                         F4tallyBox = formR2.FmadF4tallyBox,
                                         F4total = formR2.FmadF4total,
                                         F5tallyBox = formR2.FmadF5tallyBox,
                                         F5total = formR2.FmadF5total,
                                         F6tallyBox = formR2.FmadF6tallyBox,
                                         F6total = formR2.FmadF6total,
                                         F7tallyBox = formR2.FmadF7tallyBox,
                                         F7total = formR2.FmadF7total,
                                         G1tallyBox = formR2.FmadG1tallyBox,
                                         G1total = formR2.FmadG1total,
                                         G2tallyBox = formR2.FmadG2tallyBox,
                                         G2total = formR2.FmadG2total,
                                         G3tallyBox = formR2.FmadG3tallyBox,
                                         G3total = formR2.FmadG3total,
                                         G4tallyBox = formR2.FmadG4tallyBox,
                                         G4total = formR2.FmadG4total,
                                         G5tallyBox = formR2.FmadG5tallyBox,
                                         G5total = formR2.FmadG5total,
                                         G6tallyBox = formR2.FmadG6tallyBox,
                                         G6total = formR2.FmadG6total,
                                         G7tallyBox = formR2.FmadG7tallyBox,
                                         G7total = formR2.FmadG7total,
                                         G8tallyBox = formR2.FmadG8tallyBox,
                                         G8total = formR2.FmadG8total,
                                         G9tallyBox = formR2.FmadG9tallyBox,
                                         G9total = formR2.FmadG9total,
                                         G10tallyBox = formR2.FmadG10tallyBox,
                                         G10total = formR2.FmadG10total,
                                         Total = formR2.FmadTotal,
                                         Findings = o.FmhFindings,
                                         CorrectiveActions = o.FmhCorrectiveActions,
                                         UsernameAudit = o.FmhUsernameAudit,
                                         DesignationAudit = o.FmhDesignationAudit,
                                         DateAudit = o.FmhDateAudit,
                                         UsernameWit = o.FmhUsernameWit,
                                         DesignationWit = o.FmhDesignationWit,
                                         DateWit = o.FmhDateWit,
                                         UsernameWitone = o.FmhUsernameWitone,
                                         DesignationWitone = o.FmhDesignationWitone,
                                         DateWitone = o.FmhDateWitone,
                                         PkRefNo = o.FmhPkRefNo
                                     }).ToList();

            return detail;
        }

    }
}
