using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RAMMS.Common.Extensions;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RAMMS.Repository
{
    public class FormV1Repository : RepositoryBase<RmFormV1Hdr>, IFormV1Repository
    {
        public FormV1Repository(RAMMSContext context) : base(context)
        {
            _context = context;
        }


        public async Task<List<RmFormV1Hdr>> GetFilteredRecordList(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions)
        {
            List<RmFormV1Hdr> result = new List<RmFormV1Hdr>();
            var query = (from x in _context.RmFormV1Hdr
                         let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeCode == x.Fv1hRmu || s.DdlTypeDesc == x.Fv1hRmu))
                         let sec = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "Section Code" && (s.DdlTypeDesc == x.Fv1hSecCode || s.DdlTypeCode == x.Fv1hSecCode))
                         let div = _context.RmDivRmuSecMaster.FirstOrDefault(s => s.RdsmSectionCode == x.Fv1hSecCode)
                         select new { rmu, sec, x, div });



            query = query.Where(x => x.x.Fv1hActiveYn == true).OrderByDescending(x => x.x.Fv1hPkRefNo);
            if (filterOptions.Filters != null)
            {

                if (!string.IsNullOrEmpty(filterOptions.Filters.Section))
                {
                    query = query.Where(x => x.sec.DdlTypeDesc == filterOptions.Filters.Section || x.sec.DdlTypeCode == filterOptions.Filters.Section);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
                {
                    query = query.Where(x => x.rmu.DdlTypeCode == filterOptions.Filters.RMU || x.rmu.DdlTypeDesc == filterOptions.Filters.RMU);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.Crew))
                {
                    query = query.Where(x => x.x.Fv1hCrewname == filterOptions.Filters.Crew || (x.x.Fv1hCrew.HasValue ? x.x.Fv1hCrew.ToString() == filterOptions.Filters.Crew : x.x.Fv1hCrew.ToString() == ""));
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ActivityCode))
                {
                    query = query.Where(x => x.x.Fv1hActCode == filterOptions.Filters.ActivityCode);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.x.Fv1hDt.HasValue ? (x.x.Fv1hDt.Value.Year == dt.Year && x.x.Fv1hDt.Value.Month == dt.Month && x.x.Fv1hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && !string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.x.Fv1hDt.HasValue ? (x.x.Fv1hDt.Value.Year == dt.Year && x.x.Fv1hDt.Value.Month == dt.Month && x.x.Fv1hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    query = query.Where(x => x.x.Fv1hRmu.Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.rmu.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || (x.sec.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || x.x.Fv1hCrewname.Contains(filterOptions.Filters.SmartInputValue)
                                        || ((bool)x.x.Fv1hSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv1hRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv1hSecCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv1hUsernameSch.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv1hUsernameAgr.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv1hUsernameAck.Contains(filterOptions.Filters.SmartInputValue)
                                        || (filterOptions.Filters.SmartInputValue.IsInt() && x.x.Fv1hPkRefNo.Equals(filterOptions.Filters.SmartInputValue.AsInt())));

                }
            }


            if (filterOptions.sortOrder == SortOrder.Ascending)
            {

                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderBy(x => x.x.Fv1hPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(x => x.x.Fv1hRmu);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(x => x.div.RdsmDivCode);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(x => x.x.Fv1hActCode);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(x => x.x.Fv1hSecCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(x => x.x.Fv1hCrewname);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(x => x.x.Fv1hDt);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderBy(x => x.x.Fv1hUsernameAck);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderBy(x => x.x.Fv1hUsernameAgr);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderBy(x => x.x.Fv1hUsernameSch);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderBy(x => x.x.Fv1hSubmitSts);


            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderByDescending(x => x.x.Fv1hPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(x => x.x.Fv1hRmu);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(x => x.div.RdsmDivCode);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(x => x.x.Fv1hActCode);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(x => x.x.Fv1hSecCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(x => x.x.Fv1hCrewname);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(x => x.x.Fv1hDt);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderByDescending(x => x.x.Fv1hUsernameAck);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderByDescending(x => x.x.Fv1hUsernameAgr);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderByDescending(x => x.x.Fv1hUsernameSch);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderByDescending(x => x.x.Fv1hSubmitSts);
            }


            result = await query.Select(s => s.x)
                    .Skip(filterOptions.StartPageNo)
                    .Take(filterOptions.RecordsPerPage)
                    .ToListAsync();
            return result;
        }

        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions)
        {
            List<RmFormV1Hdr> result = new List<RmFormV1Hdr>();
            var query = (from x in _context.RmFormV1Hdr
                         let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeCode == x.Fv1hRmu || s.DdlTypeDesc == x.Fv1hRmu))
                         let sec = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "Section Code" && (s.DdlTypeDesc == x.Fv1hSecCode || s.DdlTypeCode == x.Fv1hSecCode))
                         let div = _context.RmDivRmuSecMaster.FirstOrDefault(s => s.RdsmSectionCode == x.Fv1hSecCode)
                         select new { rmu, sec, x, div });



            query = query.Where(x => x.x.Fv1hActiveYn == true).OrderByDescending(x => x.x.Fv1hPkRefNo);
            if (filterOptions.Filters != null)
            {

                if (!string.IsNullOrEmpty(filterOptions.Filters.Section))
                {
                    query = query.Where(x => x.sec.DdlTypeDesc == filterOptions.Filters.Section || x.sec.DdlTypeCode == filterOptions.Filters.Section);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
                {
                    query = query.Where(x => x.rmu.DdlTypeCode == filterOptions.Filters.RMU || x.rmu.DdlTypeDesc == filterOptions.Filters.RMU);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.Crew))
                {
                    query = query.Where(x => x.x.Fv1hCrewname == filterOptions.Filters.Crew || (x.x.Fv1hCrew.HasValue ? x.x.Fv1hCrew.ToString() == filterOptions.Filters.Crew : x.x.Fv1hCrew.ToString() == ""));
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ActivityCode))
                {
                    query = query.Where(x => x.x.Fv1hActCode == filterOptions.Filters.ActivityCode);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.x.Fv1hDt.HasValue ? (x.x.Fv1hDt.Value.Year == dt.Year && x.x.Fv1hDt.Value.Month == dt.Month && x.x.Fv1hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && !string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.x.Fv1hDt.HasValue ? (x.x.Fv1hDt.Value.Year == dt.Year && x.x.Fv1hDt.Value.Month == dt.Month && x.x.Fv1hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    query = query.Where(x => x.x.Fv1hRmu.Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.rmu.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || (x.sec.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || x.x.Fv1hCrewname.Contains(filterOptions.Filters.SmartInputValue)
                                        || ((bool)x.x.Fv1hSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv1hRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv1hSecCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv1hUsernameSch.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv1hUsernameAgr.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv1hUsernameAck.Contains(filterOptions.Filters.SmartInputValue)
                                        || (filterOptions.Filters.SmartInputValue.IsInt() && x.x.Fv1hPkRefNo.Equals(filterOptions.Filters.SmartInputValue.AsInt())));

                }
            }

            return await query.CountAsync().ConfigureAwait(false);
        }


        public async Task<List<RmFormV1Dtl>> GetFormV1WorkScheduleGridList(FilteredPagingDefinition<FormV1WorkScheduleGridDTO> filterOptions, int V1PkRefNo)
        {
            List<RmFormV1Dtl> result = new List<RmFormV1Dtl>();
            var query = (from x in _context.RmFormV1Dtl
                         where x.Fv1dFv1hPkRefNo == V1PkRefNo && x.Fv1dActiveYn == true
                         select new { x }).OrderByDescending(x => x.x.Fv1dPkRefNo);


            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderBy(x => x.x.Fv1dRoadCode);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(x => x.x.Fv1dRoadName);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(x => x.x.Fv1dFrmCh);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(x => x.x.Fv1dSiteRef);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(x => x.x.Fv1dStartTime);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(x => x.x.Fv1dRemarks);

            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderByDescending(x => x.x.Fv1dRoadCode);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(x => x.x.Fv1dRoadName);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(x => x.x.Fv1dFrmCh);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(x => x.x.Fv1dSiteRef);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(x => x.x.Fv1dStartTime);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(x => x.x.Fv1dRemarks);
            }


            result = await query.Select(s => s.x)
                    .Skip(filterOptions.StartPageNo)
                    .Take(filterOptions.RecordsPerPage)
                    .ToListAsync();

            return result;
        }


        public async Task<RmFormV1Hdr> FindFormV1ByID(int id)
        {
            return await _context.RmFormV1Hdr.Where(x => x.Fv1hPkRefNo == id && x.Fv1hActiveYn == true).FirstOrDefaultAsync();
        }


        public List<SelectListItem> FindRefNoFromS1(FormV1ResponseDTO FormV1)
        {



            var res = (from hdr in _context.RmFormS1Hdr
                       join dtl in _context.RmFormS1Dtl on hdr.FsihPkRefNo equals dtl.FsidFsihPkRefNo
                       where hdr.FsihDt == FormV1.Dt && hdr.FsihRmu == FormV1.Rmu && Convert.ToInt32(dtl.FsidActCode) == Convert.ToInt32(FormV1.ActCode) && dtl.FsidActiveYn == true && hdr.FsihActiveYn == true
                       orderby hdr.FsihPkRefNo descending
                       select new
                       {
                           hdr.FsihRefId,
                           hdr.FsihPkRefNo
                       }).Distinct();

            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in res)
            {
                list.Add(new SelectListItem { Text = item.FsihRefId.ToString(), Value = item.FsihPkRefNo.ToString() });
            }

            return list;
        }


        public int LoadS1Data(int PKRefNo, int S1PKRefNo, string ActCode)
        {

            IList<RmFormV1Dtl> del = (from r in _context.RmFormV1Dtl where r.Fv1dFv1hPkRefNo == PKRefNo && r.Fv1dS1dPkRefNo != 0 select r).ToList();

            foreach (var item in del)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }

            if (S1PKRefNo != 0)
            {
                var res = (from dtl in _context.RmFormS1Dtl
                           where dtl.FsidFsihPkRefNo == S1PKRefNo && dtl.FsidActCode == ActCode && dtl.FsidActiveYn == true
                           orderby dtl.FsidPkRefNo descending
                           select new RmFormV1Dtl
                           {
                               Fv1dFv1hPkRefNo = PKRefNo,
                               Fv1dS1dPkRefNo = dtl.FsidPkRefNo,
                               Fv1dActiveYn = true,
                               Fv1dCrDt = dtl.FsidCrDt,
                               Fv1dFrmCh = dtl.FsidFrmChKm,
                               Fv1dRemarks = dtl.FsidRemarks,
                               Fv1dRoadCode = dtl.FsiidRoadCode,
                               Fv1dRoadName = dtl.FsiidRoadName,
                               Fv1dFrmChDeci = dtl.FsidFrmChM == "" ? 0 : Convert.ToInt32(dtl.FsidFrmChM),
                               Fv1dSiteRef = dtl.FsidFormASiteRef,
                               Fv1dStartTime = "",
                               Fv1dToCh = dtl.FsidToChKm,
                               Fv1dToChDeci = dtl.FsidToChM == "" ? 0 : Convert.ToInt32(dtl.FsidToChM),
                           }).ToList();
              

                foreach (var item in res)
                {
                    _context.RmFormV1Dtl.Add(item);
                    _context.SaveChanges();
                }
            }

            return 1;
        }

        public int PullS1Data(int PKRefNo, int S1PKRefNo, string ActCode)
        {

            var Exist = from r in _context.RmFormV1Dtl where r.Fv1dFv1hPkRefNo == PKRefNo && r.Fv1dS1dPkRefNo != 0 select r.Fv1dS1dPkRefNo;
            var FromS1 = (from dtl in _context.RmFormS1Dtl
                          where dtl.FsidFsihPkRefNo == S1PKRefNo && dtl.FsidActCode == ActCode && dtl.FsidActiveYn == true && !Exist.Contains(dtl.FsidPkRefNo)
                          orderby dtl.FsidPkRefNo descending
                          select new RmFormV1Dtl
                          {
                              Fv1dFv1hPkRefNo = PKRefNo,
                              Fv1dS1dPkRefNo = dtl.FsidPkRefNo,
                              Fv1dActiveYn = true,
                              Fv1dCrDt = dtl.FsidCrDt,
                              Fv1dFrmCh = dtl.FsidFrmChKm,
                              Fv1dRemarks = dtl.FsidRemarks,
                              Fv1dRoadCode = dtl.FsiidRoadCode,
                              Fv1dRoadName = dtl.FsiidRoadName,
                              Fv1dFrmChDeci = dtl.FsidFrmChM == "" ? 0 : Convert.ToInt32(dtl.FsidFrmChM),
                              Fv1dSiteRef = dtl.FsidFormASiteRef,
                              Fv1dStartTime = "",
                              Fv1dToCh = dtl.FsidToChKm,
                              Fv1dToChDeci = dtl.FsidToChM == "" ? 0 : Convert.ToInt32(dtl.FsidToChM),
                          }).ToList();
                        

            foreach (var item in FromS1)
            {
                _context.RmFormV1Dtl.Add(item);
                _context.SaveChanges();
            }

            return 1;
        }


        public int? SaveFormV1WorkSchedule(RmFormV1Dtl FormV1Dtl)
        {
            try
            {
                _context.Entry<RmFormV1Dtl>(FormV1Dtl).State = FormV1Dtl.Fv1dPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return FormV1Dtl.Fv1dPkRefNo;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        public int? UpdateFormV1WorkSchedule(RmFormV1Dtl FormV1Dtl)
        {
            try
            {
                _context.Set<RmFormV1Dtl>().Attach(FormV1Dtl);
                _context.Entry<RmFormV1Dtl>(FormV1Dtl).State = EntityState.Modified;
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }


        public int? DeleteFormV1(int id)
        {
            try
            {
                var res = _context.Set<RmFormV1Hdr>().FindAsync(id);
                res.Result.Fv1hActiveYn = false;
                _context.Set<RmFormV1Hdr>().Attach(res.Result);
                _context.Entry<RmFormV1Hdr>(res.Result).State = EntityState.Modified;
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }


        public int? DeleteFormV1WorkSchedule(int id)
        {
            try
            {
                var res = _context.Set<RmFormV1Dtl>().FindAsync(id);
                res.Result.Fv1dActiveYn = false;
                _context.Set<RmFormV1Dtl>().Attach(res.Result);
                _context.Entry<RmFormV1Dtl>(res.Result).State = EntityState.Modified;
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }


    }
}
