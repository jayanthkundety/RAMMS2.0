using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RAMMS.Common;
using Microsoft.AspNetCore.Mvc;
using RAMMS.Common.RefNumber;
using RAMMS.DTO.Report;

namespace RAMMS.Repository
{
    public class FormF5Repository : RepositoryBase<RmFormF5InsHdr>, IFormF5Repository
    {
        public FormF5Repository(RAMMSContext context) : base(context)
        {
            _context = context;
        }
        public async Task<GridWrapper<object>> GetFormF5GridHeader(DataTableAjaxPostModel searchData)
        {
            var query = (from hdr in _context.RmFormF5InsHdr
                         from sec in _context.RmRoadMaster.Where(s => s.RdmRdCode == hdr.FvahRoadCode && s.RdmActiveYn == true).DefaultIfEmpty()
                         select new
                         {
                             RefNo = hdr.FvahPkRefNo,
                             RefID = hdr.FvahFormRefId,
                             InspectionDt = hdr.FvahDtInspBy,
                             InspectedBy = hdr.FvahUserNameInspBy,
                             Division = hdr.FvahDivCode,
                             District = hdr.FvahDist,
                             RMU = sec.RdmRmuCode,
                             RMUName = hdr.FvahRmuName,
                             RdCode = hdr.FvahRoadCode,
                             SecCode = sec.RdmSecCode,
                             SecName = sec.RdmSecName,
                             CrewLeaderName = hdr.FvahCrewLeaderName,
                             Year = hdr.FvahYearOfInsp,
                             dtl = hdr.RmFormF5InsDtl,
                             Active = hdr.FvahActiveYn,
                             RdId = sec.RdmRdCdSort,
                             Status = (hdr.FvahSubmitSts == true ? "Submitted" : "Saved")

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
                              || (x.InspectedBy ?? "").Contains(strVal)
                              || (x.Division ?? "").Contains(strVal)
                              || (x.District ?? "").Contains(strVal)
                              || (x.RMU ?? "").Contains(strVal)
                              || (x.RMUName ?? "").Contains(strVal)
                              || (x.SecCode.ToString()).Contains(strVal)
                              || (x.SecName ?? "").Contains(strVal)
                              || (x.RdCode ?? "").Contains(strVal)
                              || (x.CrewLeaderName ?? "").Contains(strVal)
                              || (x.InspectionDt.HasValue && dtSearch.HasValue && x.InspectionDt == dtSearch)
                              || (x.Year.HasValue ? x.Year.Value.ToString() : "").Contains(strVal)
                              );
                            break;
                        case "FromYear":
                            query = query.Where(x => x.Year >= Convert.ToInt32(strVal));
                            break;
                        case "ToYear":
                            query = query.Where(x => x.Year <= Convert.ToInt32(strVal));
                            break;
                        case "AssetType":
                            query = query.Where(x => x.dtl.Any(y => y.FvadStrucCode == strVal && y.FvadActiveYn == true));
                            break;
                        //case "chFromKM":
                        //    string strM = Utility.ToString(searchData.filter["chFromM"]);
                        //    float flKm = Utility.ToFloat(strVal + (strM != "" ? "." + strM : ""));
                        //    query = query.Where(x => x.dtl.Any(dt => Convert.ToDouble(dt.FvadLocChKm.ToString() + '.' + dt.FvadLocChM) >= Convert.ToDouble(flKm)));
                        //    break;
                        //case "chFromM":
                        //    string strKm = Utility.ToString(searchData.filter["chFromKM"]);
                        //    if (strKm == "")
                        //    {
                        //        float flM = Utility.ToFloat("0." + strVal);
                        //        query = query.Where(x => x.dtl.Any(dt => Convert.ToDouble(dt.FvadLocChKm.ToString() + '.' + dt.FvadLocChM) >= Convert.ToDouble(flM)));
                        //    }
                        //    break;
                        //case "chToKm":
                        //    string strTM = Utility.ToString(searchData.filter["chToM"]);
                        //    float flTKm = Utility.ToFloat(strVal + (strTM != "" ? "." + strTM : ""));
                        //    query = query.Where(x => x.dtl.Any(dt => Convert.ToDouble(dt.FvadLocChKm.ToString() + '.' + dt.FvadLocChM) <= Convert.ToDouble(flTKm)));
                        //    break;
                        //case "chToM":
                        //    string strTKm = Utility.ToString(searchData.filter["chToKm"]);
                        //    if (strTKm == "")
                        //    {
                        //        float flTM = Utility.ToFloat("0." + strVal);
                        //        query = query.Where(x => x.dtl.Any(dt => Convert.ToDouble(dt.FvadLocChKm.ToString() + '.' + dt.FvadLocChM) <= Convert.ToDouble(flTM)));
                        //    }
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
            grid.data = await query.Order(searchData, query.OrderByDescending(searchData => searchData.RefNo)).Skip(searchData.start)
                .Take(searchData.length).ToListAsync();
            return grid;
        }

        public async Task<GridWrapper<object>> GetFormF5GridDetail(int headerId, DataTableAjaxPostModel searchData)
        {
            GridWrapper<object> grid = new GridWrapper<object>();
            if (headerId > 0)
            {
                var query = (from dtl in _context.RmFormF5InsDtl.Where(x => x.FvadActiveYn == true && x.FvadFvahPkRefNo == headerId)
                             select new
                             {
                                 RefNo = dtl.FvadPkRefNo,
                                 Length = dtl.FvadLength,
                                 Width = dtl.FvadWidth,
                                 Span = dtl.FvadSpanCnt,
                                 StructureCode = dtl.FvadStrucCode,
                                 Remarks = dtl.FvadRemarks,
                                 River = dtl.FvadRiverName,
                                 CenterLineChainage =Convert.ToDouble(dtl.FvadLocChKm.ToString() + "." + dtl.FvadLocChM),
                                 OverAllCondition = dtl.FvadCondition
                             }
                             );

                grid.recordsTotal = await query.CountAsync();
                grid.recordsFiltered = grid.recordsTotal;
                grid.draw = searchData.draw;
                grid.data = await query.Order(searchData, query.OrderByDescending(searchData => searchData.RefNo)).Skip(searchData.start).Take(searchData.length).ToListAsync();

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

        public async Task<RmFormF5InsHdr> saveFormF5Hdr(RmFormF5InsHdr header, bool updateSubmitSts)
        {
            bool isAdd = false;
            if (header.FvahPkRefNo == 0)
            {
                isAdd = true;
                header.FvahActiveYn = true;
                _context.RmFormF5InsHdr.Add(header);
            }
            else
            {
                _context.RmFormF5InsHdr.Attach(header);
                var entry = _context.Entry(header);
                entry.Property(x => x.FvahSignpathInspBy).IsModified = true;
                entry.Property(x => x.FvahUserDesignationInspBy).IsModified = true;
                entry.Property(x => x.FvahUserIdInspBy).IsModified = true;
                entry.Property(x => x.FvahUserNameInspBy).IsModified = true;
                entry.Property(x => x.FvahDtInspBy).IsModified = true;
                entry.Property(x => x.FvahCrewLeaderId).IsModified = true;
                entry.Property(x => x.FvahCrewLeaderName).IsModified = true;
                entry.Property(x => x.FvahModDt).IsModified = true;
                entry.Property(x => x.FvahModBy).IsModified = true;
                if (updateSubmitSts)
                {
                    entry.Property(x => x.FvahSubmitSts).IsModified = true;
                }
            }
            _context.SaveChanges();
            if (isAdd)
            {
                IDictionary<string, string> lstData = new Dictionary<string, string>();
                lstData.Add("Year", Utility.ToString(header.FvahYearOfInsp));
                lstData.Add("RoadCode", Utility.ToString(header.FvahRoadCode));
                lstData.Add(FormRefNumber.NewRunningNumber, Utility.ToString(header.FvahPkRefNo));
                header.FvahFormRefId = FormRefNumber.GetRefNumber(FormType.FormF5Header, lstData);
                _context.SaveChanges();
            }
            return header;
        }

        public async Task<int> saveDetail(IList<RmFormF5InsDtl> detail)
        {
            _context.RmFormF5InsDtl.AddRange(detail);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteFormF5Hdr(RmFormF5InsHdr header)
        {
            _context.RmFormF5InsHdr.Attach(header);
            var entry = _context.Entry(header);
            entry.Property(x => x.FvahActiveYn).IsModified = true;
            entry.Property(x => x.FvahModBy).IsModified = true;
            entry.Property(x => x.FvahModDt).IsModified = true;
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteFormF5Dtl(List<RmFormF5InsDtl> dtl)
        {
            _context.RmFormF5InsDtl.UpdateRange(dtl);
            return await _context.SaveChangesAsync();
        }
        public async Task<List<RmFormF5InsDtl>> GetDetailList(int id)
        {
            return await _context.RmFormF5InsDtl.Where(d => d.FvadFvahPkRefNo == id && d.FvadActiveYn == true).ToListAsync();
        }
        public async Task<FORMF5Rpt> GetReportData(int headerid)
        {
            FORMF5Rpt result = (from h in _context.RmFormF5InsHdr
                                where h.FvahPkRefNo == headerid
                                select new FORMF5Rpt
                                {
                                    CrewLeader = h.FvahCrewLeaderName,
                                    District = h.FvahDist,
                                    Division = h.FvahDivCode,
                                    RMU = h.FvahRmuName,
                                    InspectedByName = h.FvahUserNameInspBy,
                                    InspectedByDesignation = h.FvahUserDesignationInspBy,
                                    InspectedDate = h.FvahDtInspBy,
                                    RoadCode = h.FvahRoadCode,
                                    RoadName = h.FvahRoadName,
                                    RoadLength = h.FvahRoadLength
                                }).FirstOrDefault();
            result.Details = (from d in _context.RmFormF5InsDtl
                              where d.FvadFvahPkRefNo == headerid && d.FvadActiveYn == true
                              orderby d.FvadPkRefNo descending 
                              select new FORMF5RptDetail
                              {
                                  Code = d.FvadStrucCode,
                                  Remarks = d.FvadRemarks,
                                  StartingChKm = d.FvadLocChKm,
                                  StartingChM = d.FvadLocChM,
                                  BridgeRiverName = d.FvadRiverName,
                                  TOTLength = d.FvadLength,
                                  AvgWidth = d.FvadWidth,
                                  NoOfSpan = d.FvadSpanCnt,
                                  OverAllCondition = d.FvadCondition

                              }).ToArray();
            return result;
        }
    }
}
