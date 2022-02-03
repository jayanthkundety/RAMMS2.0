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
    public class FormF4Repository : RepositoryBase<RmFormF4InsHdr>, IFormF4Repository
    {
        public FormF4Repository(RAMMSContext context) : base(context)
        {
            _context = context;
        }
        public async Task<GridWrapper<object>> GetFormF4GridHeader(DataTableAjaxPostModel searchData)
        {
            var query = (from hdr in _context.RmFormF4InsHdr
                         from sec in _context.RmRoadMaster.Where(s => s.RdmRdCode == hdr.FivahRoadCode && s.RdmActiveYn == true).DefaultIfEmpty()
                         select new
                         {
                             RefNo = hdr.FivahPkRefNo,
                             RefID = hdr.FivahFormRefId,
                             InspectionDt = hdr.FivahDtInspBy,
                             InspectedBy = hdr.FivahUserNameInspBy,
                             Division = hdr.FivahDivCode,
                             District = hdr.FivahDist,
                             RMU = sec.RdmRmuCode,
                             RMUName = hdr.FivahRmuName,
                             RdCode = hdr.FivahRoadCode,
                             SecCode = sec.RdmSecCode,
                             SecName = sec.RdmSecName,
                             CrewLeaderName = hdr.FivahCrewLeaderName,
                             Year = hdr.FivahYearOfInsp,
                             dtl = hdr.RmFormF4InsDtl,
                             Active = hdr.FivahActiveYn,
                             RdId = sec.RdmRdCdSort,
                             Status = (hdr.FivahSubmitSts == true ? "Submitted" : "Saved")

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
                            query = query.Where(x => x.dtl.Any(y => y.FivadStrucCode == strVal && y.FivadActiveYn == true));
                            break;
                        //case "chFromKM":
                        //    string strM = Utility.ToString(searchData.filter["chFromM"]);
                        //    float flKm = Utility.ToFloat(strVal + (strM != "" ? "." + strM : ""));
                        //    query = query.Where(x => x.dtl.Any(dt => Convert.ToDouble(dt.FivadLocChKm.ToString() + '.' + dt.FivadLocChM) >= Convert.ToDouble(flKm)));
                        //    break;
                        //case "chFromM":
                        //    string strKm = Utility.ToString(searchData.filter["chFromKM"]);
                        //    if (strKm == "")
                        //    {
                        //        float flM = Utility.ToFloat("0." + strVal);
                        //        query = query.Where(x => x.dtl.Any(dt => Convert.ToDouble(dt.FivadLocChKm.ToString() + '.' + dt.FivadLocChM) >= Convert.ToDouble(flM)));
                        //    }
                        //    break;
                        //case "chToKm":
                        //    string strTM = Utility.ToString(searchData.filter["chToM"]);
                        //    float flTKm = Utility.ToFloat(strVal + (strTM != "" ? "." + strTM : ""));
                        //    query = query.Where(x => x.dtl.Any(dt => Convert.ToDouble(dt.FivadLocChKm.ToString() + '.' + dt.FivadLocChM) <= Convert.ToDouble(flTKm)));
                        //    break;
                        //case "chToM":
                        //    string strTKm = Utility.ToString(searchData.filter["chToKm"]);
                        //    if (strTKm == "")
                        //    {
                        //        float flTM = Utility.ToFloat("0." + strVal);
                        //        query = query.Where(x => x.dtl.Any(dt => Convert.ToDouble(dt.FivadLocChKm.ToString() + '.' + dt.FivadLocChM) <= Convert.ToDouble(flTM)));
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

        public async Task<GridWrapper<object>> GetFormF4GridDetail(int headerId, DataTableAjaxPostModel searchData)
        {
            GridWrapper<object> grid = new GridWrapper<object>();
            if (headerId > 0)
            {
                var query = (from dtl in _context.RmFormF4InsDtl.Where(x => x.FivadActiveYn == true && x.FivadFivahPkRefNo == headerId)
                             select new
                             {
                                 RefNo = dtl.FivadPkRefNo,
                                 Length = dtl.FivadLength,
                                 Width = dtl.FivadWidth,
                                 Height = dtl.FivadHeight,
                                 StructureCode = dtl.FivadStrucCode,
                                 Remarks = dtl.FivadRemarks,
                                 InletStructure = dtl.FivadIntelStruc == true ? "Y" : "N",
                                 OutletStructure = dtl.FivadOutletStruc == true ? "Y" : "N",
                                 CenterLineChainage = Convert.ToDouble(dtl.FivadLocChKm + "." + dtl.FivadLocChM),
                                 NoOfCell = dtl.FivadBarrelNo,
                                 OverAllCondition = dtl.FivadCondition
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

        public async Task<RmFormF4InsHdr> saveFormF4Hdr(RmFormF4InsHdr header, bool updateSubmitSts)
        {
            bool isAdd = false;
            if (header.FivahPkRefNo == 0)
            {
                isAdd = true;
                header.FivahActiveYn = true;
                _context.RmFormF4InsHdr.Add(header);
            }
            else
            {
                _context.RmFormF4InsHdr.Attach(header);
                var entry = _context.Entry(header);
                entry.Property(x => x.FivahSignpathInspBy).IsModified = true;
                entry.Property(x => x.FivahUserDesignationInspBy).IsModified = true;
                entry.Property(x => x.FivahUserIdInspBy).IsModified = true;
                entry.Property(x => x.FivahUserNameInspBy).IsModified = true;
                entry.Property(x => x.FivahDtInspBy).IsModified = true;
                entry.Property(x => x.FivahCrewLeaderId).IsModified = true;
                entry.Property(x => x.FivahCrewLeaderName).IsModified = true;
                entry.Property(x => x.FivahModDt).IsModified = true;
                entry.Property(x => x.FivahModBy).IsModified = true;
                if (updateSubmitSts)
                {
                    entry.Property(x => x.FivahSubmitSts).IsModified = true;
                }
            }
            _context.SaveChanges();
            if (isAdd)
            {
                IDictionary<string, string> lstData = new Dictionary<string, string>();
                lstData.Add("Year", Utility.ToString(header.FivahYearOfInsp));
                lstData.Add("RoadCode", Utility.ToString(header.FivahRoadCode));
                lstData.Add(FormRefNumber.NewRunningNumber, Utility.ToString(header.FivahPkRefNo));
                header.FivahFormRefId = FormRefNumber.GetRefNumber(FormType.FormF4Header, lstData);
                _context.SaveChanges();
            }
            return header;
        }

        public async Task<int> saveDetail(IList<RmFormF4InsDtl> detail)
        {
            _context.RmFormF4InsDtl.AddRange(detail);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteFormF4Hdr(RmFormF4InsHdr header)
        {
            _context.RmFormF4InsHdr.Attach(header);
            var entry = _context.Entry(header);
            entry.Property(x => x.FivahActiveYn).IsModified = true;
            entry.Property(x => x.FivahModBy).IsModified = true;
            entry.Property(x => x.FivahModDt).IsModified = true;
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteFormF4Dtl(List<RmFormF4InsDtl> dtl)
        {
            _context.RmFormF4InsDtl.UpdateRange(dtl);
            return await _context.SaveChangesAsync();
        }
        public async Task<List<RmFormF4InsDtl>> GetDetailList(int id)
        {
            return await _context.RmFormF4InsDtl.Where(d => d.FivadFivahPkRefNo == id && d.FivadActiveYn == true).ToListAsync();
        }
        public async Task<FORMF4Rpt> GetReportData(int headerid)
        {
            FORMF4Rpt result = (from s in _context.RmFormF4InsHdr
                                where s.FivahPkRefNo == headerid && s.FivahActiveYn == true
                                select new FORMF4Rpt
                                {
                                    CrewLeader = s.FivahCrewLeaderName,
                                    District = s.FivahDist,
                                    InspectedByDesignation = s.FivahUserDesignationInspBy,
                                    InspectedByName = s.FivahUserNameInspBy,
                                    InspectedDate = s.FivahDtInspBy,
                                    Division = s.FivahDivCode,
                                    RMU = s.FivahRmuName,
                                    RoadCode = s.FivahRoadCode,
                                    RoadName = s.FivahRoadName,
                                    RoadLength = s.FivahRoadLength
                                }).FirstOrDefault();

            result.Details = (from d in _context.RmFormF4InsDtl
                              where d.FivadFivahPkRefNo == headerid && d.FivadActiveYn == true
                              orderby d.FivadPkRefNo descending
                              select new FORMF4RptDetail
                              {
                                  Code = d.FivadStrucCode,
                                  Remarks = d.FivadRemarks,
                                  CentreLineChKm = d.FivadLocChKm,
                                  CentreLineChM = d.FivadLocChM,
                                  InletHeadWall = d.FivadIntelStruc == true ? "Y" : "N",
                                  OutletHeadWall = d.FivadOutletStruc == true ? "Y" : "N",
                                  Length = d.FivadLength,
                                  NoOfCell = d.FivadBarrelNo,
                                  Width = d.FivadWidth,
                                  Height = d.FivadHeight,
                                  OverAllCondition = d.FivadCondition
                              }).ToArray();
            return result;

        }

    }
}
