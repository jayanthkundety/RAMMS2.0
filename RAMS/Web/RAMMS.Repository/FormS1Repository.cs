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
using RAMMS.DTO.RequestBO;
using static RAMMS.DTO.RequestBO.FormS1DetailDTO;

namespace RAMMS.Repository
{
    public class FormS1Repository : RepositoryBase<RmFormS1Hdr>, IFormS1Repository
    {
        public FormS1Repository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public int DeleteFormS1Hdr(RmFormS1Hdr formS1Header)
        {
            _context.RmFormS1Hdr.Attach(formS1Header);
            var entry = _context.Entry(formS1Header);
            entry.Property(x => x.FsihActiveYn).IsModified = true;
            _context.SaveChanges();
            return formS1Header.FsihPkRefNo;
        }
        public int DeleteDetail(RmFormS1Dtl formS1Dtl)
        {
            _context.RmFormS1Dtl.Attach(formS1Dtl);
            var entry = _context.Entry(formS1Dtl);
            entry.Property(x => x.FsidActiveYn).IsModified = true;
            _context.SaveChanges();
            return formS1Dtl.FsidPkRefNo;
        }

        public RmFormS1Hdr SaveFormS1Hdr(RmFormS1Hdr formS1Header, bool updateSubmit)
        {
            bool isAdd = false;
            if (formS1Header.FsihPkRefNo == 0)
            {
                isAdd = true;
                formS1Header.FsihActiveYn = true;
                _context.RmFormS1Hdr.Add(formS1Header);
            }
            else
            {
                _context.RmFormS1Hdr.Attach(formS1Header);
                var entry = _context.Entry(formS1Header);
                entry.Property(x => x.FsiihDtPlan).IsModified = true;
                entry.Property(x => x.FsiihUseridPlan).IsModified = true;
                entry.Property(x => x.FsiihDtVet).IsModified = true;
                entry.Property(x => x.FsiihUseridVet).IsModified = true;
                entry.Property(x => x.FsiihDtAgrd).IsModified = true;
                entry.Property(x => x.FsiihUseridAgrd).IsModified = true;
                entry.Property(x => x.FsihModBy).IsModified = true;
                entry.Property(x => x.FsihModDt).IsModified = true;
                entry.Property(x => x.FsihRemarks).IsModified = true;

                entry.Property(x => x.FsiihUserNamePlan).IsModified = true;
                entry.Property(x => x.FsiihUserNameVet).IsModified = true;
                entry.Property(x => x.FsiihUserNameAgrd).IsModified = true;
                entry.Property(x => x.FsiihUserDesignationPlan).IsModified = true;
                entry.Property(x => x.FsiihUserDesignationVet).IsModified = true;
                entry.Property(x => x.FsiihUserDesignationAgrd).IsModified = true;
                if (updateSubmit)
                {
                    entry.Property(x => x.FsihSubmitSts).IsModified = true;
                }
            }
            _context.SaveChanges();
            if (isAdd)
            {
                IDictionary<string, string> lstData = new Dictionary<string, string>();
                lstData.Add("RMU", formS1Header.FsihRmu);
                lstData.Add("Date", Utility.ToString(formS1Header.FsihDt, "yyyy-MMM-dd"));
                lstData.Add("WeekNo", Utility.ToString(formS1Header.FsihWeekNo));
                lstData.Add(FormRefNumber.NewRunningNumber, Utility.ToString(formS1Header.FsihPkRefNo));
                formS1Header.FsihRefId = FormRefNumber.GetRefNumber(FormType.FormS1Header, lstData);
                _context.SaveChanges();
            }
            return formS1Header;
        }
        public async Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData)
        {
            var query = (from hdr in _context.RmFormS1Hdr
                         from rmu in _context.RmDdLookup.Where(rd => rd.DdlType == "RMU" && (rd.DdlTypeCode == hdr.FsihRmu)).DefaultIfEmpty()
                         from plan in _context.RmUsers.Where(x => x.UsrPkId == hdr.FsiihUseridPlan).DefaultIfEmpty()
                         from vet in _context.RmUsers.Where(x => x.UsrPkId == hdr.FsiihUseridVet).DefaultIfEmpty()
                         from agr in _context.RmUsers.Where(x => x.UsrPkId == hdr.FsiihUseridAgrd).DefaultIfEmpty()
                         select new
                         {
                             RefNo = hdr.FsihPkRefNo,
                             RefID = hdr.FsihRefId,
                             RMU = hdr.FsihRmu,
                             RMUCode = rmu.DdlTypeCode,
                             RMUDesc = rmu.DdlTypeDesc,
                             Active = hdr.FsihActiveYn,
                             DateOfEntry = hdr.FsihDt,
                             FromDate = hdr.FsihFromDt,
                             ToDate = hdr.FsihToDt,
                             WeekNo = (hdr.FsihWeekNo.HasValue ? hdr.FsihWeekNo.Value : 0),
                             PlanByID = hdr.FsiihUseridPlan,
                             PlanByName = plan.UsrUserName,
                             VetByID = hdr.FsiihUseridVet,
                             VetByName = vet.UsrUserName,
                             AgrByID = hdr.FsiihUseridAgrd,
                             AgrByName = agr.UsrUserName,
                             Status = (hdr.FsihSubmitSts.HasValue && hdr.FsihSubmitSts.Value ? "Submitted" : "Saved"),
                             ProcessStatus = hdr.FsihStatus
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
                                 || (x.RMU ?? "").Contains(strVal)
                                 || (x.RMUCode ?? "").Contains(strVal)
                                 || (x.RMUDesc ?? "").Contains(strVal)
                                 || (x.WeekNo.ToString()).Contains(strVal)
                                 || (x.PlanByName ?? "").Contains(strVal)
                                 || (x.VetByName ?? "").Contains(strVal)
                                 || (x.AgrByName ?? "").Contains(strVal)
                                 || (x.Status ?? "").Contains(strVal)
                                 || (x.DateOfEntry.HasValue && dtSearch.HasValue && x.DateOfEntry == dtSearch)
                                 );
                            break;
                        case "fromDate":
                            query = query.Where(x => x.DateOfEntry >= Convert.ToDateTime(strVal));
                            break;
                        case "toDate":
                            query = query.Where(x => x.DateOfEntry <= Convert.ToDateTime(strVal));
                            break;
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


        public RmFormS1Dtl SaveDetails(RmFormS1Dtl formS1Details)
        {
            bool isAdd = false;

            if (formS1Details.FsidPkRefNo == 0)
            {

                isAdd = true;
                _context.RmFormS1Dtl.Add(formS1Details);
            }
            else
            {
                string[] arrNotReqUpdate = new string[] { "FsidPkRefNo", "FsidFsihPkRefNo", "FsidFsihPkRefNoNavigation", "FsidSubmitSts", "FsidCrBy", "FsidCrDt", "FsidActiveYn" };
                _context.RmFormS1Dtl.Attach(formS1Details);
                var entry = _context.Entry(formS1Details);
                entry.Properties.Where(x => !arrNotReqUpdate.Contains(x.Metadata.Name)).ToList().ForEach((p) =>
                {
                    p.IsModified = true;
                });
                if (formS1Details.RmFormS1WkDtl != null && formS1Details.RmFormS1WkDtl.Count > 0)
                {
                    string[] arrNotReWD = new string[] { "FsiwdPkRefNo", "FsiwdFsidPkRefNo", "FsiwdFsidPkRefNoNavigation" };
                    formS1Details.RmFormS1WkDtl.Where(x => x.FsiwdPkRefNo > 0).ToList().ForEach((wkDtl) =>
                    {
                        _context.RmFormS1WkDtl.Attach(wkDtl);
                        var entryWkDtl = _context.Entry(wkDtl);
                        entryWkDtl.Properties.Where(x => !arrNotReWD.Contains(x.Metadata.Name)).ToList().ForEach((p) =>
                        {
                            p.IsModified = true;
                        });
                    });
                }

            }
            _context.SaveChanges();
            if (isAdd)
            {
                int seqNum = _context.RmFormS1Dtl.CountAsync(x => x.FsidFsihPkRefNo == formS1Details.FsidFsihPkRefNo).Result;
                seqNum = seqNum == 0 ? 1 : seqNum;
                var header = FindAsync(x => x.FsihPkRefNo == formS1Details.FsidFsihPkRefNo).Result;
                IDictionary<string, string> lstData = new Dictionary<string, string>();
                lstData.Add("RMU", header.FsihRmu);
                lstData.Add("Date", Utility.ToString(header.FsihDt, "yyyy-MMM-dd"));
                lstData.Add("WeekNo", Utility.ToString(header.FsihWeekNo));
                lstData.Add("S1PKID", Utility.ToString(header.FsihPkRefNo));
                lstData.Add(FormRefNumber.NewRunningNumber, Utility.ToString(seqNum));
                formS1Details.FsidRefId = FormRefNumber.GetRefNumber(Common.RefNumber.FormType.FormS1Details, lstData);
                _context.Entry(formS1Details).Property(x => x.FsidRefId).IsModified = true;
                _context.SaveChanges();
                formS1Details.FsidFsihPkRefNoNavigation = null;
            }
            else
            {

                if (formS1Details.RmFormS1WkDtl.Count() > 0)
                {
                    int[] delIds = formS1Details.RmFormS1WkDtl.Select(x => x.FsiwdPkRefNo).ToArray();
                    _context.RmFormS1WkDtl.RemoveRange(_context.RmFormS1WkDtl.Where(x => x.FsiwdFsidPkRefNo == formS1Details.FsidPkRefNo && !delIds.Contains(x.FsiwdPkRefNo)));
                    _context.SaveChanges();
                }
                else
                {
                    _context.RmFormS1WkDtl.RemoveRange(_context.RmFormS1WkDtl.Where(x => x.FsiwdFsidPkRefNo == formS1Details.FsidPkRefNo));
                    _context.SaveChanges();
                }
            }
            return formS1Details;
        }
        public async Task<RmFormS1Dtl> FindDetailsById(int detailPKId)
        {
            return await _context.RmFormS1Dtl.Include(x => x.RmFormS1WkDtl).Where(x => x.FsidPkRefNo == detailPKId).FirstOrDefaultAsync();
        }

        public async Task<GridWrapper<object>> GetDetailsGrid(int headerId, DataTableAjaxPostModel searchData)
        {
            GridWrapper<object> grid = new GridWrapper<object>();
            if (headerId > 0)
            {
                var query = (from dtl in _context.RmFormS1Dtl.Include(x => x.FsidFsihPkRefNoNavigation).Where(x => x.FsidFsihPkRefNo == headerId)
                             from rd in _context.RmRoadMaster.Where(x => x.RdmPkRefNo == dtl.FsiidRoadId).DefaultIfEmpty()
                             from d1 in _context.RmFormS1WkDtl.Where(x => x.FsiwdFsidPkRefNo == dtl.FsidPkRefNo && x.FsiwdSchldDate == dtl.FsidFsihPkRefNoNavigation.FsihFromDt.Value).DefaultIfEmpty()
                             from d2 in _context.RmFormS1WkDtl.Where(x => x.FsiwdFsidPkRefNo == dtl.FsidPkRefNo && x.FsiwdSchldDate == dtl.FsidFsihPkRefNoNavigation.FsihFromDt.Value.AddDays(1)).DefaultIfEmpty()
                             from d3 in _context.RmFormS1WkDtl.Where(x => x.FsiwdFsidPkRefNo == dtl.FsidPkRefNo && x.FsiwdSchldDate == dtl.FsidFsihPkRefNoNavigation.FsihFromDt.Value.AddDays(2)).DefaultIfEmpty()
                             from d4 in _context.RmFormS1WkDtl.Where(x => x.FsiwdFsidPkRefNo == dtl.FsidPkRefNo && x.FsiwdSchldDate == dtl.FsidFsihPkRefNoNavigation.FsihFromDt.Value.AddDays(3)).DefaultIfEmpty()
                             from d5 in _context.RmFormS1WkDtl.Where(x => x.FsiwdFsidPkRefNo == dtl.FsidPkRefNo && x.FsiwdSchldDate == dtl.FsidFsihPkRefNoNavigation.FsihFromDt.Value.AddDays(4)).DefaultIfEmpty()
                             from d6 in _context.RmFormS1WkDtl.Where(x => x.FsiwdFsidPkRefNo == dtl.FsidPkRefNo && x.FsiwdSchldDate == dtl.FsidFsihPkRefNoNavigation.FsihFromDt.Value.AddDays(5)).DefaultIfEmpty()
                             from d7 in _context.RmFormS1WkDtl.Where(x => x.FsiwdFsidPkRefNo == dtl.FsidPkRefNo && x.FsiwdSchldDate == dtl.FsidFsihPkRefNoNavigation.FsihFromDt.Value.AddDays(6)).DefaultIfEmpty()
                             from crew in _context.RmUsers.Where(x => x.UsrPkId == dtl.FsidCrewSupervisor).DefaultIfEmpty()
                             select new
                             {
                                 RefNo = dtl.FsidPkRefNo,
                                 RefID = dtl.FsidRefId,
                                 HeaderId = dtl.FsidFsihPkRefNo,
                                 ACode = dtl.FsidActCode,
                                 RCode = rd.RdmRdCode,
                                 RName = rd.RdmRdName,
                                 CHFrom = (dtl.FsidFrmChKm.HasValue ? dtl.FsidFrmChKm.Value.ToString() : "") + "." + (dtl.FsidFrmChM == null ? "0" : dtl.FsidFrmChM),
                                 CHTo = (dtl.FsidToChKm.HasValue ? dtl.FsidToChKm.Value.ToString() : "") + "." + (dtl.FsidToChM == null ? "0" : dtl.FsidToChM),
                                 Active = dtl.FsidActiveYn,
                                 Status = (dtl.FsidSubmitSts ? "Submitted" : "Saved"),
                                 SiteRef = dtl.FsidFormASiteRef,
                                 Priority = dtl.FsidFormAPriority,
                                 WorkQty = dtl.FsidFormAWorkQty,
                                 CDR = dtl.FsidFormACdr,
                                 CrewSupID = dtl.FsidCrewSupervisor,
                                 CrewSupName = crew.UsrUserName,
                                 Mon = d1.FsiwdActual,
                                 Tue = d2.FsiwdActual,
                                 Wed = d3.FsiwdActual,
                                 Thu = d4.FsiwdActual,
                                 Fri = d5.FsiwdActual,
                                 Sat = d6.FsiwdActual,
                                 Sun = d7.FsiwdActual,
                                 Remarks = dtl.FsidRemarks
                             }); ;
                query = query.Where(x => x.Active == true);


                grid.recordsTotal = await query.CountAsync();
                grid.recordsFiltered = grid.recordsTotal;
                grid.draw = searchData.draw;
                grid.data = await query.Order(searchData, query.OrderBy(s => s.RefNo)).Skip(searchData.start)
                                    .Take(searchData.length)
                                    .ToListAsync();
            }
            else
            {
                grid.recordsTotal = 0;
                grid.recordsFiltered = 0;
                grid.draw = searchData.draw;
                grid.data = new List<string>();
            }
            return grid;
        }

        public FORMS1Rpt GetReportData(int headerId)
        {
            FORMS1Rpt report = new FORMS1Rpt();
            report.Header = (from h in _context.RmFormS1Hdr
                             where h.FsihPkRefNo == headerId && h.FsihActiveYn == true
                             select new FORMS1HeaderRpt
                             {
                                 AgreedBy = h.FsiihUserNameAgrd,
                                 Date = h.FsihDt,
                                 From = h.FsihFromDt,
                                 To = h.FsihToDt,
                                 PlannedBy = h.FsiihUserNamePlan,
                                 Remarks = h.FsihRemarks,
                                 RMU = h.FsihRmu,
                                 VettedBy = h.FsiihUserNameVet,
                                 WeekNo = h.FsihWeekNo.HasValue ? h.FsihWeekNo.Value : 0
                             }).FirstOrDefault();
            var week = (from w in _context.RmFormS1WkDtl
                        join d in _context.RmFormS1Dtl on w.FsiwdFsidPkRefNo equals d.FsidPkRefNo
                        where d.FsidFsihPkRefNo == headerId && d.FsidActiveYn == true
                        select w).ToArray();
            report.Details = (from d in _context.RmFormS1Dtl
                              where d.FsidFsihPkRefNo == headerId && d.FsidActiveYn == true
                              select new FORMS1DetailRpt
                              {
                                  Id = d.FsidPkRefNo,
                                  ActivityCode = d.FsidActCode,
                                  CDR = d.FsidFormACdr,
                                  ChainageFromKm = d.FsidFrmChKm == null ? 0 : d.FsidFrmChKm,
                                  ChainageFromM = d.FsidFrmChM == null ? "0" : d.FsidFrmChM,
                                  ChainageToKm = d.FsidToChKm == null ? 0 : d.FsidToChKm,
                                  ChainageToM = d.FsidToChM == null ? "0" : d.FsidToChM,
                                  CrewSupervisor = d.FsidCrewSupervisorName,
                                  IsFriday = false,
                                  IsMonday = false,
                                  IsTuesday = false,
                                  IsWednesday = false,
                                  IsThursday = false,
                                  IsMT = d.FsidFapMt,
                                  IsQA1 = d.FsidFapQa1,
                                  IsQA2 = d.FsidFapQa2,
                                  IsRA = d.FsidFapRa,
                                  IsSA = d.FsidFapSa,
                                  IsSaturday = false,
                                  IsSunday = false,
                                  N1 = d.FsidFapN1,
                                  N2 = d.FsidFapN2,
                                  Priority = d.FsidFormAPriority,
                                  Qty = d.FsidFormAWorkQty,
                                  Remarks = d.FsidRemarks,
                                  RoadCode = d.FsiidRoadCode,
                                  RoadName = d.FsiidRoadName,
                                  SiteRef = d.FsidFormASiteRef,
                              }).ToList();
            foreach (var detail in report.Details)
            {
                var planned = week.Where(s => s.FsiwdFsidPkRefNo == detail.Id && s.FsiwdActual.HasValue).ToArray();
                if (planned != null)
                {
                    detail.Planned = planned.Select(s => new Planned
                    {
                        DayOfTheWeek = s.FsiwdSchldDayOfWeek.Value,
                        Value = s.FsiwdActual.Value
                    });
                }
                var scheduled = week.Where(s => s.FsiwdFsidPkRefNo == detail.Id && s.FsiwdPlanned.HasValue).ToArray();
                if (scheduled != null)
                {
                    detail.Scheduled = scheduled.Select(s => new Planned
                    {
                        DayOfTheWeek = s.FsiwdSchldDayOfWeek.Value,
                        Value = s.FsiwdPlanned.Value
                    });
                }
            }
            return report;
        }

        public async Task<RmFormS1Dtl> GetDetailsById(int dtlId)
        {
            return await _context.RmFormS1Dtl.Where(x => x.FsidPkRefNo == dtlId).FirstOrDefaultAsync();
        }

        public async Task<List<ActWeekDtl>> GetFormDDtls(string roadCode, string actCode, string frmCh, string frmChDeci, string toCh, string toChDeci, string crewSupervisor, string weekNo)
        {
            var result = await (from a in _context.RmFormDHdr
                                join b in _context.RmFormDDtl on a.FdhPkRefNo equals b.FddFdhPkRefNo
                                where a.FdhCrewUnit == crewSupervisor && a.FdhWeekNo == Convert.ToInt16(weekNo) && a.FdhActiveYn == true
                                && b.FddRoadCode == roadCode && b.FddActCode == Convert.ToInt32(actCode) && b.FddFrmCh == Convert.ToInt32(frmCh)
                                && b.FddFrmChDeci == Convert.ToInt32(frmChDeci) && b.FddToCh == Convert.ToInt32(toCh) && b.FddToChDeci == Convert.ToInt32(toChDeci)
                                && b.FddActiveYn == true
                                select new FormS1DetailDTO.ActWeekDtl
                                {
                                    FormDFddWorkSts = b.FddWorkSts,
                                    FormDFdhDay = a.FdhDay
                                }).ToListAsync();
            return result;
        }

        public void CreateWkdtl(RmFormS1WkDtl s1WkDtl)
        {
            _context.RmFormS1WkDtl.Add(s1WkDtl);
        }

        public void UpdateWkdtl(RmFormS1WkDtl s1WkDtl)
        {
            _context.Set<RmFormS1WkDtl>().Attach(s1WkDtl);
            _context.Entry(s1WkDtl).State = EntityState.Modified;
        }

        public (int id, bool alreadyExists) CheckAlreadyExists(string roadCode, string activityCode, int fromChKm, string fromChM, int toChKm, string toChM, int weekNo)
        {

            var isExists = (from a in _context.RmFormS1Hdr
                            join b in _context.RmFormS1Dtl on a.FsihPkRefNo equals b.FsidFsihPkRefNo
                            where b.FsiidRoadCode == roadCode && b.FsidActCode == activityCode &&
                                 b.FsidFrmChKm == fromChKm && b.FsidFrmChM == fromChM &&
                                 b.FsidToChKm == toChKm && b.FsidToChM == toChM && a.FsihWeekNo == weekNo
                            select b.FsidPkRefNo).FirstOrDefault();
            if (isExists > 0)
            {
                return (isExists, true);
            }
            else
            {
                return (0, false);
            }
        }
    }
}
