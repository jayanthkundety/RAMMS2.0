using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RAMMS.Common;
using RAMMS.Common.Extensions;
using RAMMS.Common.RefNumber;
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
        private readonly IMapper _mapper;


        public FormV1Repository(RAMMSContext context, IMapper mapper = null) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        #region FormV1
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

                if (!string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && !string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dtFrom, dtTo;
                    DateTime.TryParseExact(filterOptions.Filters.ByFromdate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtFrom);
                    DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtTo);

                    {
                        query = query.Where(x => x.x.Fv1hDt.HasValue ? x.x.Fv1hDt >= dtFrom && x.x.Fv1hDt <= dtTo : false);
                    }
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

                if (!string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && !string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dtFrom, dtTo;
                    DateTime.TryParseExact(filterOptions.Filters.ByFromdate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtFrom);
                    DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtTo);

                    {
                        query = query.Where(x => x.x.Fv1hDt.HasValue ? x.x.Fv1hDt >= dtFrom && x.x.Fv1hDt <= dtTo : false);
                    }
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
                       join wdtl in _context.RmFormS1WkDtl on dtl.FsidPkRefNo equals wdtl.FsiwdFsidPkRefNo
                       where wdtl.FsiwdSchldDate == FormV1.Dt && hdr.FsihRmu == FormV1.Rmu && Convert.ToInt32(dtl.FsidActCode) == Convert.ToInt32(FormV1.ActCode) && dtl.FsidActiveYn == true && hdr.FsihActiveYn == true
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

                var dependency = _context.RmFormV2Hdr.Where(x => x.Fv2hFv1hPkRefNo == id && x.Fv2hActiveYn == true).FirstOrDefault();
                if (dependency == null)
                {

                    IList<RmFormV1Dtl> child = (from r in _context.RmFormV1Dtl where r.Fv1dFv1hPkRefNo == id select r).ToList();
                    foreach (var item in child)
                    {
                        _context.Remove(item);
                        _context.SaveChanges();
                    }

                    var res = _context.Set<RmFormV1Hdr>().FindAsync(id);
                    res.Result.Fv1hActiveYn = false;
                    _context.Set<RmFormV1Hdr>().Attach(res.Result);
                    _context.Entry<RmFormV1Hdr>(res.Result).State = EntityState.Modified;
                    _context.SaveChanges();
                    return 1;
                }
                else
                {
                    return -1;
                }
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

        #endregion

        #region FormV3

        public async Task<List<RmFormV3Hdr>> GetFilteredV3RecordList(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions)
        {
            List<RmFormV3Hdr> result = new List<RmFormV3Hdr>();
            var query = (from x in _context.RmFormV3Hdr
                         let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeCode == x.Fv3hRmu || s.DdlTypeDesc == x.Fv3hRmu))
                         let sec = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "Section Code" && (s.DdlTypeDesc == x.Fv3hSecCode || s.DdlTypeCode == x.Fv3hSecCode))
                         let div = _context.RmDivRmuSecMaster.FirstOrDefault(s => s.RdsmSectionCode == x.Fv3hSecCode)
                         select new { rmu, sec, x, div });



            query = query.Where(x => x.x.Fv3hActiveYn == true).OrderByDescending(x => x.x.Fv3hPkRefNo);
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
                    query = query.Where(x => x.x.Fv3hCrewname == filterOptions.Filters.Crew || (x.x.Fv3hCrew.HasValue ? x.x.Fv3hCrew.ToString() == filterOptions.Filters.Crew : x.x.Fv3hCrew.ToString() == ""));
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ActivityCode))
                {
                    query = query.Where(x => x.x.Fv3hActCode == filterOptions.Filters.ActivityCode);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && !string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dtFrom, dtTo;
                    DateTime.TryParseExact(filterOptions.Filters.ByFromdate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtFrom);
                    DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtTo);

                    {
                        query = query.Where(x => x.x.Fv3hDt.HasValue ? x.x.Fv3hDt >= dtFrom && x.x.Fv3hDt <= dtTo : false);
                    }
                }


                if (!string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.x.Fv3hDt.HasValue ? (x.x.Fv3hDt.Value.Year == dt.Year && x.x.Fv3hDt.Value.Month == dt.Month && x.x.Fv3hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && !string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.x.Fv3hDt.HasValue ? (x.x.Fv3hDt.Value.Year == dt.Year && x.x.Fv3hDt.Value.Month == dt.Month && x.x.Fv3hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    query = query.Where(x => x.x.Fv3hRmu.Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.rmu.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || (x.sec.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || x.x.Fv3hCrewname.Contains(filterOptions.Filters.SmartInputValue)
                                        || ((bool)x.x.Fv3hSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv3hRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv3hSecCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv3hUsernameRec.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv3hUsernameAgr.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv3hUsernameFac.Contains(filterOptions.Filters.SmartInputValue)
                                        || (filterOptions.Filters.SmartInputValue.IsInt() && x.x.Fv3hPkRefNo.Equals(filterOptions.Filters.SmartInputValue.AsInt())));

                }
            }


            if (filterOptions.sortOrder == SortOrder.Ascending)
            {

                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderBy(x => x.x.Fv3hPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(x => x.x.Fv3hRmu);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(x => x.div.RdsmDivCode);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(x => x.x.Fv3hActCode);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(x => x.x.Fv3hSecCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(x => x.x.Fv3hCrewname);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(x => x.x.Fv3hDt);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderBy(x => x.x.Fv3hUsernameRec);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderBy(x => x.x.Fv3hUsernameAgr);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderBy(x => x.x.Fv3hUsernameFac);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderBy(x => x.x.Fv3hSubmitSts);


            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderByDescending(x => x.x.Fv3hPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(x => x.x.Fv3hRmu);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(x => x.div.RdsmDivCode);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(x => x.x.Fv3hActCode);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(x => x.x.Fv3hSecCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(x => x.x.Fv3hCrewname);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(x => x.x.Fv3hDt);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderByDescending(x => x.x.Fv3hUsernameRec);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderByDescending(x => x.x.Fv3hUsernameAgr);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderByDescending(x => x.x.Fv3hUsernameFac);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderByDescending(x => x.x.Fv3hSubmitSts);
            }


            result = await query.Select(s => s.x)
                    .Skip(filterOptions.StartPageNo)
                    .Take(filterOptions.RecordsPerPage)
                    .ToListAsync();
            return result;
        }

        public async Task<int> GetFilteredV3RecordCount(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions)
        {
            List<RmFormV3Hdr> result = new List<RmFormV3Hdr>();
            var query = (from x in _context.RmFormV3Hdr
                         let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeCode == x.Fv3hRmu || s.DdlTypeDesc == x.Fv3hRmu))
                         let sec = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "Section Code" && (s.DdlTypeDesc == x.Fv3hSecCode || s.DdlTypeCode == x.Fv3hSecCode))
                         let div = _context.RmDivRmuSecMaster.FirstOrDefault(s => s.RdsmSectionCode == x.Fv3hSecCode)
                         select new { rmu, sec, x, div });



            query = query.Where(x => x.x.Fv3hActiveYn == true).OrderByDescending(x => x.x.Fv3hPkRefNo);
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
                    query = query.Where(x => x.x.Fv3hCrewname == filterOptions.Filters.Crew || (x.x.Fv3hCrew.HasValue ? x.x.Fv3hCrew.ToString() == filterOptions.Filters.Crew : x.x.Fv3hCrew.ToString() == ""));
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ActivityCode))
                {
                    query = query.Where(x => x.x.Fv3hActCode == filterOptions.Filters.ActivityCode);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && !string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dtFrom, dtTo;
                    DateTime.TryParseExact(filterOptions.Filters.ByFromdate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtFrom);
                    DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtTo);

                    {
                        query = query.Where(x => x.x.Fv3hDt.HasValue ? x.x.Fv3hDt >= dtFrom && x.x.Fv3hDt <= dtTo : false);
                    }
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.x.Fv3hDt.HasValue ? (x.x.Fv3hDt.Value.Year == dt.Year && x.x.Fv3hDt.Value.Month == dt.Month && x.x.Fv3hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && !string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.x.Fv3hDt.HasValue ? (x.x.Fv3hDt.Value.Year == dt.Year && x.x.Fv3hDt.Value.Month == dt.Month && x.x.Fv3hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    query = query.Where(x => x.x.Fv3hRmu.Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.rmu.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || (x.sec.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || x.x.Fv3hCrewname.Contains(filterOptions.Filters.SmartInputValue)
                                        || ((bool)x.x.Fv3hSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv3hRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv3hSecCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv3hUsernameRec.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv3hUsernameAgr.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv3hUsernameFac.Contains(filterOptions.Filters.SmartInputValue)
                                        || (filterOptions.Filters.SmartInputValue.IsInt() && x.x.Fv3hPkRefNo.Equals(filterOptions.Filters.SmartInputValue.AsInt())));

                }
            }

            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<RmFormV3Dtl>> GetFormv3DtlGridList(FilteredPagingDefinition<FormV3DtlGridDTO> filterOptions, int V3PkRefNo)
        {
            List<RmFormV3Dtl> result = new List<RmFormV3Dtl>();
            var query = (from x in _context.RmFormV3Dtl
                         where x.Fv3dFv3hPkRefNo == V3PkRefNo && x.Fv3dActiveYn == true
                         select new { x }).OrderByDescending(x => x.x.Fv3dPkRefNo);


            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderBy(x => x.x.Fv3dRoadCode);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(x => x.x.Fv3dRoadName);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(x => x.x.Fv3dFrmCh);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(x => x.x.Fv3dToCh);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(x => x.x.Fv3dLength);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(x => x.x.Fv3dWidth);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(x => x.x.Fv3dTimetakenFrm);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderBy(x => x.x.Fv3dTimeTakenTo);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderBy(x => x.x.Fv3dTimeTakenTotal);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderBy(x => x.x.Fv3dAdp);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderBy(x => x.x.Fv3dTransitTimeFrm);
                if (filterOptions.ColumnIndex == 12)
                    query = query.OrderBy(x => x.x.Fv3dTransitTimeTo);
                if (filterOptions.ColumnIndex == 13)
                    query = query.OrderBy(x => x.x.Fv3dTransitTimeTotal);


            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderByDescending(x => x.x.Fv3dRoadCode);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(x => x.x.Fv3dRoadName);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(x => x.x.Fv3dFrmCh);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(x => x.x.Fv3dToCh);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(x => x.x.Fv3dLength);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(x => x.x.Fv3dWidth);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(x => x.x.Fv3dTimetakenFrm);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderByDescending(x => x.x.Fv3dTimeTakenTo);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderByDescending(x => x.x.Fv3dTimeTakenTotal);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderByDescending(x => x.x.Fv3dAdp);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderByDescending(x => x.x.Fv3dTransitTimeFrm);
                if (filterOptions.ColumnIndex == 12)
                    query = query.OrderByDescending(x => x.x.Fv3dTransitTimeTo);
                if (filterOptions.ColumnIndex == 13)
                    query = query.OrderByDescending(x => x.x.Fv3dTransitTimeTotal);
            }


            result = await query.Select(s => s.x)
                    .Skip(filterOptions.StartPageNo)
                    .Take(filterOptions.RecordsPerPage)
                    .ToListAsync();

            return result;
        }


        public async Task<RmFormV3Hdr> FindFormV3ByID(int id)
        {
            return await _context.RmFormV3Hdr.Where(x => x.Fv3hPkRefNo == id && x.Fv3hActiveYn == true).FirstOrDefaultAsync();
        }

        public async Task<FormV3ResponseDTO> SaveFormV3(FormV3ResponseDTO Formv3)
        {
            try
            {
                var domainModelFormv3 = _mapper.Map<RmFormV3Hdr>(Formv3);
                domainModelFormv3.Fv3hPkRefNo = 0;

                //var obj = _context.RmFormV3Hdr.Where(x => x.Fv3hRmu == domainModelFormv3.Fv3hRmu && x.Fv3hActCode == domainModelFormv3.Fv3hActCode && x.Fv3hSecCode == domainModelFormv3.Fv3hSecCode && x.Fv3hCrew == domainModelFormv3.Fv3hCrew && x.Fv3hDt == domainModelFormv3.Fv3hDt && x.Fv3hActiveYn == true).ToList();
                var obj = _context.RmFormV3Hdr.Where(x => x.Fv3hRmu == domainModelFormv3.Fv3hRmu && x.Fv3hActCode == domainModelFormv3.Fv3hActCode && x.Fv3hDt == domainModelFormv3.Fv3hDt && x.Fv3hCrew == domainModelFormv3.Fv3hCrew && x.Fv3hActiveYn == true).FirstOrDefault();
                if (obj != null)
                {
                    var res = _mapper.Map<FormV3ResponseDTO>(obj);
                    res.FormExist = true;
                    return res;
                }
                var objV2 = _context.RmFormV2Hdr.Where(x => x.Fv2hRmu == domainModelFormv3.Fv3hRmu && x.Fv2hActCode == domainModelFormv3.Fv3hActCode && x.Fv2hDt == domainModelFormv3.Fv3hDt && x.Fv2hCrew == domainModelFormv3.Fv3hCrew && x.Fv2hActiveYn == true).ToList();
                if (objV2.Count == 0)
                {
                    Formv3.PkRefNo = -2;
                    return Formv3;
                }
                else
                {
                    var objV1 = _context.RmFormV1Hdr.Where(x => x.Fv1hRmu == domainModelFormv3.Fv3hRmu && x.Fv1hActCode == domainModelFormv3.Fv3hActCode && x.Fv1hDt == domainModelFormv3.Fv3hDt && x.Fv1hActiveYn == true).ToList();
                    if (objV1.Count > 0)
                    {
                        domainModelFormv3.Fv3hFv1PkRefId = objV1.Single().Fv1hRefId;
                        domainModelFormv3.Fv3hFv1PkRefNo = objV1.Single().Fv1hPkRefNo;
                        _context.Set<RmFormV3Hdr>().Add(domainModelFormv3);
                        _context.SaveChanges();

                        IDictionary<string, string> lstData = new Dictionary<string, string>();
                        lstData.Add("YYYYMMDD", Utility.ToString(DateTime.Today.ToString("yyyyMMdd")));
                        lstData.Add("Crew", domainModelFormv3.Fv3hCrew.ToString());
                        lstData.Add("ActivityCode", domainModelFormv3.Fv3hActCode);
                        lstData.Add("RMU", domainModelFormv3.Fv3hRmu.ToString());
                        lstData.Add(FormRefNumber.NewRunningNumber, Utility.ToString(domainModelFormv3.Fv3hPkRefNo));
                        domainModelFormv3.Fv3hRefId = FormRefNumber.GetRefNumber(RAMMS.Common.RefNumber.FormType.FormV3Header, lstData);
                        _context.SaveChanges();
                        Formv3.PkRefNo = _mapper.Map<FormV3ResponseDTO>(domainModelFormv3).PkRefNo;
                        Formv3.RefId = domainModelFormv3.Fv3hRefId;
                        Formv3.Status = domainModelFormv3.Fv3hStatus;
                        Formv3.FV1PKRefId = domainModelFormv3.Fv3hFv1PkRefId;

                        var res = (from dtl in _context.RmFormV1Dtl
                                   where dtl.Fv1dFv1hPkRefNo == domainModelFormv3.Fv3hFv1PkRefNo && dtl.Fv1dActiveYn == true
                                   orderby dtl.Fv1dPkRefNo descending
                                   select new RmFormV3Dtl
                                   {
                                       Fv3dFv3hPkRefNo = Formv3.PkRefNo,
                                       Fv3dFv1dPkRefNo = dtl.Fv1dPkRefNo,
                                       Fv3dActiveYn = true,
                                       Fv3dFrmChDeci = dtl.Fv1dFrmChDeci,
                                       Fv3dFrmCh = dtl.Fv1dFrmCh,
                                       Fv3dToChDeci = dtl.Fv1dToChDeci,
                                       Fv3dToCh = dtl.Fv1dToCh,
                                       Fv3dRoadCode = dtl.Fv1dRoadCode,
                                       Fv3dRoadName = dtl.Fv1dRoadName,

                                   }).ToList();


                        foreach (var item in res)
                        {
                            _context.RmFormV3Dtl.Add(item);
                            _context.SaveChanges();
                        }


                        return Formv3;
                    }
                    else
                    {
                        Formv3.PkRefNo = -1;
                        return Formv3;
                    }

                }
            }
            catch (Exception ex)
            {
                await _context.DisposeAsync();
                throw ex;
            }
        }


        public async Task<int> UpdateFormV3(RmFormV3Hdr FormV3)
        {
            try
            {
                _context.Set<RmFormV3Hdr>().Attach(FormV3);
                _context.Entry<RmFormV3Hdr>(FormV3).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        public async Task<int> UpdateFormV3Dtl(RmFormV3Dtl FormV3Dtl)
        {
            try
            {
                _context.Set<RmFormV3Dtl>().Attach(FormV3Dtl);
                _context.Entry<RmFormV3Dtl>(FormV3Dtl).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        public int? DeleteFormV3(int id)
        {
            try
            {
                var dependency = _context.RmFormV4Hdr.Where(x => x.Fv4hFv3PkRefNo == id && x.Fv4hActiveYn == true).FirstOrDefault();
                if (dependency == null)
                {
                    IList<RmFormV3Dtl> child = (from r in _context.RmFormV3Dtl where r.Fv3dFv3hPkRefNo == id select r).ToList();
                    foreach (var item in child)
                    {
                        _context.Remove(item);
                        _context.SaveChanges();
                    }

                    var res = _context.Set<RmFormV3Hdr>().FindAsync(id);
                    res.Result.Fv3hActiveYn = false;
                    _context.Set<RmFormV3Hdr>().Attach(res.Result);
                    _context.Entry<RmFormV3Hdr>(res.Result).State = EntityState.Modified;
                    _context.SaveChanges();
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return 500;
            }
        }


        public int? DeleteFormV3Dtl(int id)
        {
            try
            {
                var res = _context.Set<RmFormV3Dtl>().FindAsync(id);
                res.Result.Fv3dActiveYn = false;
                _context.Set<RmFormV3Dtl>().Attach(res.Result);
                _context.Entry<RmFormV3Dtl>(res.Result).State = EntityState.Modified;
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        #endregion

        #region FormV4

        public async Task<List<RmFormV4Hdr>> GetFilteredV4RecordList(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions)
        {
            List<RmFormV4Hdr> result = new List<RmFormV4Hdr>();
            var query = (from x in _context.RmFormV4Hdr
                         let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeCode == x.Fv4hRmu || s.DdlTypeDesc == x.Fv4hRmu))
                         let sec = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "Section Code" && (s.DdlTypeDesc == x.Fv4hSecCode || s.DdlTypeCode == x.Fv4hSecCode))
                         let div = _context.RmDivRmuSecMaster.FirstOrDefault(s => s.RdsmSectionCode == x.Fv4hSecCode)
                         select new { rmu, sec, x, div });



            query = query.Where(x => x.x.Fv4hActiveYn == true).OrderByDescending(x => x.x.Fv4hPkRefNo);
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
                    query = query.Where(x => x.x.Fv4hCrewname == filterOptions.Filters.Crew || (x.x.Fv4hCrew.HasValue ? x.x.Fv4hCrew.ToString() == filterOptions.Filters.Crew : x.x.Fv4hCrew.ToString() == ""));
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ActivityCode))
                {
                    query = query.Where(x => x.x.Fv4hActCode == filterOptions.Filters.ActivityCode);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && !string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dtFrom, dtTo;
                    DateTime.TryParseExact(filterOptions.Filters.ByFromdate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtFrom);
                    DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtTo);

                    {
                        query = query.Where(x => x.x.Fv4hDt.HasValue ? x.x.Fv4hDt >= dtFrom && x.x.Fv4hDt <= dtTo : false);
                    }
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.x.Fv4hDt.HasValue ? (x.x.Fv4hDt.Value.Year == dt.Year && x.x.Fv4hDt.Value.Month == dt.Month && x.x.Fv4hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && !string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.x.Fv4hDt.HasValue ? (x.x.Fv4hDt.Value.Year == dt.Year && x.x.Fv4hDt.Value.Month == dt.Month && x.x.Fv4hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    query = query.Where(x => x.x.Fv4hRmu.Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.rmu.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || (x.sec.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || x.x.Fv4hCrewname.Contains(filterOptions.Filters.SmartInputValue)
                                        || ((bool)x.x.Fv4hSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv4hRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv4hSecCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv4hUsernameVet.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv4hUsernameAgr.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv4hUsernameFac.Contains(filterOptions.Filters.SmartInputValue)
                                        || (filterOptions.Filters.SmartInputValue.IsInt() && x.x.Fv4hPkRefNo.Equals(filterOptions.Filters.SmartInputValue.AsInt())));

                }
            }


            if (filterOptions.sortOrder == SortOrder.Ascending)
            {

                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderBy(x => x.x.Fv4hPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(x => x.x.Fv4hRmu);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(x => x.div.RdsmDivCode);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(x => x.x.Fv4hActCode);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(x => x.x.Fv4hSecCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(x => x.x.Fv4hCrewname);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(x => x.x.Fv4hDt);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderBy(x => x.x.Fv4hUsernameVet);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderBy(x => x.x.Fv4hUsernameAgr);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderBy(x => x.x.Fv4hUsernameFac);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderBy(x => x.x.Fv4hSubmitSts);


            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderByDescending(x => x.x.Fv4hPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(x => x.x.Fv4hRmu);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(x => x.div.RdsmDivCode);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(x => x.x.Fv4hActCode);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(x => x.x.Fv4hSecCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(x => x.x.Fv4hCrewname);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(x => x.x.Fv4hDt);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderByDescending(x => x.x.Fv4hUsernameVet);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderByDescending(x => x.x.Fv4hUsernameAgr);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderByDescending(x => x.x.Fv4hUsernameFac);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderByDescending(x => x.x.Fv4hSubmitSts);
            }


            result = await query.Select(s => s.x)
                    .Skip(filterOptions.StartPageNo)
                    .Take(filterOptions.RecordsPerPage)
                    .ToListAsync();
            return result;
        }

        public async Task<int> GetFilteredV4RecordCount(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions)
        {
            List<RmFormV4Hdr> result = new List<RmFormV4Hdr>();
            var query = (from x in _context.RmFormV4Hdr
                         let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeCode == x.Fv4hRmu || s.DdlTypeDesc == x.Fv4hRmu))
                         let sec = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "Section Code" && (s.DdlTypeDesc == x.Fv4hSecCode || s.DdlTypeCode == x.Fv4hSecCode))
                         let div = _context.RmDivRmuSecMaster.FirstOrDefault(s => s.RdsmSectionCode == x.Fv4hSecCode)
                         select new { rmu, sec, x, div });



            query = query.Where(x => x.x.Fv4hActiveYn == true).OrderByDescending(x => x.x.Fv4hPkRefNo);
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
                    query = query.Where(x => x.x.Fv4hCrewname == filterOptions.Filters.Crew || (x.x.Fv4hCrew.HasValue ? x.x.Fv4hCrew.ToString() == filterOptions.Filters.Crew : x.x.Fv4hCrew.ToString() == ""));
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ActivityCode))
                {
                    query = query.Where(x => x.x.Fv4hActCode == filterOptions.Filters.ActivityCode);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && !string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dtFrom, dtTo;
                    DateTime.TryParseExact(filterOptions.Filters.ByFromdate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtFrom);
                    DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtTo);

                    {
                        query = query.Where(x => x.x.Fv4hDt.HasValue ? x.x.Fv4hDt >= dtFrom && x.x.Fv4hDt <= dtTo : false);
                    }
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.x.Fv4hDt.HasValue ? (x.x.Fv4hDt.Value.Year == dt.Year && x.x.Fv4hDt.Value.Month == dt.Month && x.x.Fv4hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && !string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.x.Fv4hDt.HasValue ? (x.x.Fv4hDt.Value.Year == dt.Year && x.x.Fv4hDt.Value.Month == dt.Month && x.x.Fv4hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    query = query.Where(x => x.x.Fv4hRmu.Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.rmu.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || (x.sec.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || x.x.Fv4hCrewname.Contains(filterOptions.Filters.SmartInputValue)
                                        || ((bool)x.x.Fv4hSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv4hRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv4hSecCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv4hUsernameVet.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv4hUsernameAgr.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv4hUsernameFac.Contains(filterOptions.Filters.SmartInputValue)
                                        || (filterOptions.Filters.SmartInputValue.IsInt() && x.x.Fv4hPkRefNo.Equals(filterOptions.Filters.SmartInputValue.AsInt())));

                }
            }

            return await query.CountAsync().ConfigureAwait(false);
        }



        public async Task<RmFormV4Hdr> FindFormV4ByID(int id)
        {
            return await _context.RmFormV4Hdr.Where(x => x.Fv4hPkRefNo == id && x.Fv4hActiveYn == true).FirstOrDefaultAsync();
        }

        public async Task<FormV4ResponseDTO> SaveFormV4(FormV4ResponseDTO Formv4)
        {
            try
            {
                var domainModelFormv4 = _mapper.Map<RmFormV4Hdr>(Formv4);
                domainModelFormv4.Fv4hPkRefNo = 0;

                //var obj = _context.RmFormV4Hdr.Where(x => x.Fv4hRmu == domainModelFormv4.Fv4hRmu && x.Fv4hActCode == domainModelFormv4.Fv4hActCode && x.Fv4hSecCode == domainModelFormv4.Fv4hSecCode && x.Fv4hCrew == domainModelFormv4.Fv4hCrew && x.Fv4hDt == domainModelFormv4.Fv4hDt && x.Fv4hActiveYn == true).ToList();
                var obj = _context.RmFormV4Hdr.Where(x => x.Fv4hRmu == domainModelFormv4.Fv4hRmu && x.Fv4hActCode == domainModelFormv4.Fv4hActCode && x.Fv4hDt == domainModelFormv4.Fv4hDt && x.Fv4hCrew == domainModelFormv4.Fv4hCrew && x.Fv4hActiveYn == true).FirstOrDefault();
                if (obj != null)
                {
                    var res = _mapper.Map<FormV4ResponseDTO>(obj);
                    res.FormExist = true;
                    return res;
                }
                var objV2 = _context.RmFormV3Hdr.Where(x => x.Fv3hRmu == domainModelFormv4.Fv4hRmu && x.Fv3hActCode == domainModelFormv4.Fv4hActCode && x.Fv3hDt == domainModelFormv4.Fv4hDt && x.Fv3hCrew == domainModelFormv4.Fv4hCrew && x.Fv3hActiveYn == true).ToList();
                if (objV2.Count == 0)
                {
                    Formv4.PkRefNo = -1;
                    return Formv4;
                }
                else
                {


                    domainModelFormv4.Fv4hFv3PkRefNo = objV2.Single().Fv3hPkRefNo;
                    domainModelFormv4.Fv4hFv3PkRefId = objV2.Single().Fv3hRefId;
                    domainModelFormv4.Fv4hTotalProduction = _context.RmFormV3Dtl.Where(x => x.Fv3dFv3hPkRefNo == objV2.Single().Fv3hPkRefNo).Select(x => x.Fv3dAdp).Sum();
                    _context.Set<RmFormV4Hdr>().Add(domainModelFormv4);
                    _context.SaveChanges();

                    IDictionary<string, string> lstData = new Dictionary<string, string>();
                    lstData.Add("YYYYMMDD", Utility.ToString(DateTime.Today.ToString("yyyyMMdd")));
                    lstData.Add("Crew", domainModelFormv4.Fv4hCrew.ToString());
                    lstData.Add("ActivityCode", domainModelFormv4.Fv4hActCode);
                    lstData.Add("RMU", domainModelFormv4.Fv4hRmu.ToString());
                    lstData.Add(FormRefNumber.NewRunningNumber, Utility.ToString(domainModelFormv4.Fv4hPkRefNo));
                    domainModelFormv4.Fv4hRefId = FormRefNumber.GetRefNumber(RAMMS.Common.RefNumber.FormType.FormV4Header, lstData);
                    _context.SaveChanges();
                    Formv4.PkRefNo = _mapper.Map<FormV4ResponseDTO>(domainModelFormv4).PkRefNo;
                    Formv4.RefId = domainModelFormv4.Fv4hRefId;
                    Formv4.Status = domainModelFormv4.Fv4hStatus;
                    Formv4.FV3PKRefID = domainModelFormv4.Fv4hFv3PkRefId;
                    Formv4.TotalProduction = domainModelFormv4.Fv4hTotalProduction;



                    return Formv4;


                }
            }
            catch (Exception ex)
            {
                await _context.DisposeAsync();
                throw ex;
            }
        }


        public async Task<int> UpdateFormV4(RmFormV4Hdr FormV4)
        {
            try
            {
                _context.Set<RmFormV4Hdr>().Attach(FormV4);
                _context.Entry<RmFormV4Hdr>(FormV4).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        public int? DeleteFormV4(int id)
        {
            try
            {
                var dependency = _context.RmFormV5Hdr.Where(x => x.Fv5hFv4PkRefNo == id && x.Fv5hActiveYn == true).FirstOrDefault();
                if (dependency == null)
                {

                    var res = _context.Set<RmFormV4Hdr>().FindAsync(id);
                    res.Result.Fv4hActiveYn = false;
                    _context.Set<RmFormV4Hdr>().Attach(res.Result);
                    _context.Entry<RmFormV4Hdr>(res.Result).State = EntityState.Modified;
                    _context.SaveChanges();
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return 500;
            }
        }


        #endregion


        #region FormV5

        public async Task<List<RmFormV5Hdr>> GetFilteredV5RecordList(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions)
        {
            List<RmFormV5Hdr> result = new List<RmFormV5Hdr>();
            var query = (from x in _context.RmFormV5Hdr
                         let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeCode == x.Fv5hRmu || s.DdlTypeDesc == x.Fv5hRmu))
                         let sec = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "Section Code" && (s.DdlTypeDesc == x.Fv5hSecCode || s.DdlTypeCode == x.Fv5hSecCode))
                         let div = _context.RmDivRmuSecMaster.FirstOrDefault(s => s.RdsmSectionCode == x.Fv5hSecCode)
                         select new { rmu, sec, x, div });



            query = query.Where(x => x.x.Fv5hActiveYn == true).OrderByDescending(x => x.x.Fv5hPkRefNo);
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
                    query = query.Where(x => x.x.Fv5hCrewname == filterOptions.Filters.Crew || (x.x.Fv5hCrew.HasValue ? x.x.Fv5hCrew.ToString() == filterOptions.Filters.Crew : x.x.Fv5hCrew.ToString() == ""));
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ActivityCode))
                {
                    query = query.Where(x => x.x.Fv5hActCode == filterOptions.Filters.ActivityCode);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && !string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dtFrom, dtTo;
                    DateTime.TryParseExact(filterOptions.Filters.ByFromdate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtFrom);
                    DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtTo);

                    {
                        query = query.Where(x => x.x.Fv5hDt.HasValue ? x.x.Fv5hDt >= dtFrom && x.x.Fv5hDt <= dtTo : false);
                    }
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.x.Fv5hDt.HasValue ? (x.x.Fv5hDt.Value.Year == dt.Year && x.x.Fv5hDt.Value.Month == dt.Month && x.x.Fv5hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && !string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.x.Fv5hDt.HasValue ? (x.x.Fv5hDt.Value.Year == dt.Year && x.x.Fv5hDt.Value.Month == dt.Month && x.x.Fv5hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    query = query.Where(x => x.x.Fv5hRmu.Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.rmu.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || (x.sec.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || x.x.Fv5hCrewname.Contains(filterOptions.Filters.SmartInputValue)
                                        || ((bool)x.x.Fv5hSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv5hRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv5hSecCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv5hUsernameRec.Contains(filterOptions.Filters.SmartInputValue)
                                        || (filterOptions.Filters.SmartInputValue.IsInt() && x.x.Fv5hPkRefNo.Equals(filterOptions.Filters.SmartInputValue.AsInt())));

                }
            }


            if (filterOptions.sortOrder == SortOrder.Ascending)
            {

                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderBy(x => x.x.Fv5hPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(x => x.x.Fv5hRmu);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(x => x.div.RdsmDivCode);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(x => x.x.Fv5hActCode);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(x => x.x.Fv5hSecCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(x => x.x.Fv5hCrewname);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(x => x.x.Fv5hDt);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderBy(x => x.x.Fv5hUsernameRec);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderBy(x => x.x.Fv5hSubmitSts);


            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderByDescending(x => x.x.Fv5hPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(x => x.x.Fv5hRmu);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(x => x.div.RdsmDivCode);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(x => x.x.Fv5hActCode);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(x => x.x.Fv5hSecCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(x => x.x.Fv5hCrewname);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(x => x.x.Fv5hDt);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderByDescending(x => x.x.Fv5hUsernameRec);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderByDescending(x => x.x.Fv5hSubmitSts);
            }


            result = await query.Select(s => s.x)
                    .Skip(filterOptions.StartPageNo)
                    .Take(filterOptions.RecordsPerPage)
                    .ToListAsync();
            return result;
        }

        public async Task<int> GetFilteredV5RecordCount(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions)
        {
            List<RmFormV5Hdr> result = new List<RmFormV5Hdr>();
            var query = (from x in _context.RmFormV5Hdr
                         let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeCode == x.Fv5hRmu || s.DdlTypeDesc == x.Fv5hRmu))
                         let sec = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "Section Code" && (s.DdlTypeDesc == x.Fv5hSecCode || s.DdlTypeCode == x.Fv5hSecCode))
                         let div = _context.RmDivRmuSecMaster.FirstOrDefault(s => s.RdsmSectionCode == x.Fv5hSecCode)
                         select new { rmu, sec, x, div });



            query = query.Where(x => x.x.Fv5hActiveYn == true).OrderByDescending(x => x.x.Fv5hPkRefNo);
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
                    query = query.Where(x => x.x.Fv5hCrewname == filterOptions.Filters.Crew || (x.x.Fv5hCrew.HasValue ? x.x.Fv5hCrew.ToString() == filterOptions.Filters.Crew : x.x.Fv5hCrew.ToString() == ""));
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ActivityCode))
                {
                    query = query.Where(x => x.x.Fv5hActCode == filterOptions.Filters.ActivityCode);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && !string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dtFrom, dtTo;
                    DateTime.TryParseExact(filterOptions.Filters.ByFromdate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtFrom);
                    DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtTo);

                    {
                        query = query.Where(x => x.x.Fv5hDt.HasValue ? x.x.Fv5hDt >= dtFrom && x.x.Fv5hDt <= dtTo : false);
                    }
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.x.Fv5hDt.HasValue ? (x.x.Fv5hDt.Value.Year == dt.Year && x.x.Fv5hDt.Value.Month == dt.Month && x.x.Fv5hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && !string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.x.Fv5hDt.HasValue ? (x.x.Fv5hDt.Value.Year == dt.Year && x.x.Fv5hDt.Value.Month == dt.Month && x.x.Fv5hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    query = query.Where(x => x.x.Fv5hRmu.Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.rmu.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || (x.sec.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || x.x.Fv5hCrewname.Contains(filterOptions.Filters.SmartInputValue)
                                        || ((bool)x.x.Fv5hSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv5hRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv5hSecCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv5hUsernameRec.Contains(filterOptions.Filters.SmartInputValue)
                                        || (filterOptions.Filters.SmartInputValue.IsInt() && x.x.Fv5hPkRefNo.Equals(filterOptions.Filters.SmartInputValue.AsInt())));

                }
            }

            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<RmFormV5Dtl>> GetFormv5DtlGridList(FilteredPagingDefinition<FormV5DtlResponseDTO> filterOptions, int V5PkRefNo)
        {
            List<RmFormV5Dtl> result = new List<RmFormV5Dtl>();
            var query = (from x in _context.RmFormV5Dtl
                         where x.Fv5dFv5hPkRefNo == V5PkRefNo && x.Fv5dActiveYn == true
                         select new { x }).OrderByDescending(x => x.x.Fv5dPkRefNo);


            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderBy(x => x.x.Fv5dFileNameFrm);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(x => x.x.Fv5dFileNameTo);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(x => x.x.Fv5dDesc);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(x => x.x.Fv5dImageFilenameSys);



            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderByDescending(x => x.x.Fv5dFileNameFrm);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(x => x.x.Fv5dFileNameTo);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(x => x.x.Fv5dDesc);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(x => x.x.Fv5dImageFilenameSys);
            }


            result = await query.Select(s => s.x)
                    .Skip(filterOptions.StartPageNo)
                    .Take(filterOptions.RecordsPerPage)
                    .ToListAsync();

            return result;
        }


        public async Task<RmFormV5Hdr> FindFormV5ByID(int id)
        {
            return await _context.RmFormV5Hdr.Where(x => x.Fv5hPkRefNo == id && x.Fv5hActiveYn == true).FirstOrDefaultAsync();
        }

        public async Task<FormV5ResponseDTO> SaveFormV5(FormV5ResponseDTO Formv5)
        {
            try
            {
                var domainModelFormv5 = _mapper.Map<RmFormV5Hdr>(Formv5);
                domainModelFormv5.Fv5hPkRefNo = 0;


                var obj = _context.RmFormV5Hdr.Where(x => x.Fv5hRmu == domainModelFormv5.Fv5hRmu && x.Fv5hActCode == domainModelFormv5.Fv5hActCode && x.Fv5hDt == domainModelFormv5.Fv5hDt && x.Fv5hCrew == domainModelFormv5.Fv5hCrew && x.Fv5hActiveYn == true).FirstOrDefault();
                if (obj != null)
                {
                    var res = _mapper.Map<FormV5ResponseDTO>(obj);
                    res.FormExist = true;
                    return res;
                }
                var objV4 = _context.RmFormV4Hdr.Where(x => x.Fv4hRmu == domainModelFormv5.Fv5hRmu && x.Fv4hActCode == domainModelFormv5.Fv5hActCode && x.Fv4hDt == domainModelFormv5.Fv5hDt && x.Fv4hCrew == domainModelFormv5.Fv5hCrew && x.Fv4hActiveYn == true).ToList();
                if (objV4.Count == 0)
                {
                    Formv5.PkRefNo = -2;
                    return Formv5;
                }
                else
                {
                    _context.Set<RmFormV5Hdr>().Add(domainModelFormv5);
                    _context.SaveChanges();

                    IDictionary<string, string> lstData = new Dictionary<string, string>();
                    lstData.Add("YYYYMMDD", Utility.ToString(DateTime.Today.ToString("yyyyMMdd")));
                    lstData.Add("Crew", domainModelFormv5.Fv5hCrew.ToString());
                    lstData.Add("ActivityCode", domainModelFormv5.Fv5hActCode);
                    lstData.Add("RMU", domainModelFormv5.Fv5hRmu.ToString());
                    lstData.Add(FormRefNumber.NewRunningNumber, Utility.ToString(domainModelFormv5.Fv5hPkRefNo));
                    domainModelFormv5.Fv5hRefId = FormRefNumber.GetRefNumber(RAMMS.Common.RefNumber.FormType.FormV5Header, lstData);
                    _context.SaveChanges();
                    Formv5.PkRefNo = _mapper.Map<FormV5ResponseDTO>(domainModelFormv5).PkRefNo;
                    Formv5.RefId = domainModelFormv5.Fv5hRefId;
                    Formv5.Status = domainModelFormv5.Fv5hStatus;

                    return Formv5;

                }
            }
            catch (Exception ex)
            {
                await _context.DisposeAsync();
                throw ex;
            }
        }


        public async Task<int> UpdateFormV5(RmFormV5Hdr FormV5)
        {
            try
            {
                _context.Set<RmFormV5Hdr>().Attach(FormV5);
                _context.Entry<RmFormV5Hdr>(FormV5).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        public int? SaveFormV5Dtl(RmFormV5Dtl FormV5Dtl)
        {
            try
            {
                _context.Entry<RmFormV5Dtl>(FormV5Dtl).State = FormV5Dtl.Fv5dPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return FormV5Dtl.Fv5dPkRefNo;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }



        public async Task<int> UpdateFormV5Dtl(RmFormV5Dtl FormV5Dtl)
        {
            try
            {
                _context.Set<RmFormV5Dtl>().Attach(FormV5Dtl);
                _context.Entry<RmFormV5Dtl>(FormV5Dtl).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        public int? DeleteFormV5(int id)
        {
            try
            {
                // var Dependency = from r in _context.RmFormV4Hdr where r.Fv4hPkRefNo == id  select r;

                IList<RmFormV5Dtl> child = (from r in _context.RmFormV5Dtl where r.Fv5dFv5hPkRefNo == id select r).ToList();
                foreach (var item in child)
                {
                    _context.Remove(item);
                    _context.SaveChanges();
                }

                var res = _context.Set<RmFormV5Hdr>().FindAsync(id);
                res.Result.Fv5hActiveYn = false;
                _context.Set<RmFormV5Hdr>().Attach(res.Result);
                _context.Entry<RmFormV5Hdr>(res.Result).State = EntityState.Modified;
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }


        public int? DeleteFormV5Dtl(int id)
        {
            try
            {
                var res = _context.Set<RmFormV5Dtl>().FindAsync(id);
                res.Result.Fv5dActiveYn = false;
                _context.Set<RmFormV5Dtl>().Attach(res.Result);
                _context.Entry<RmFormV5Dtl>(res.Result).State = EntityState.Modified;
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        #endregion

    }
}
