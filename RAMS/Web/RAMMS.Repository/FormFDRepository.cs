using RAMMS.Repository.Interfaces;
using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using RAMS.Repository;
using System.Threading.Tasks;
using RAMMS.DTO.JQueryModel;
using System.Linq;
using RAMMS.Common;
using Microsoft.EntityFrameworkCore;
using RAMMS.Common.RefNumber;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;

namespace RAMMS.Repository
{
    public class FormFDRepository : RepositoryBase<RmFormFdInsHdr>, IFormFDRepository
    {
        public FormFDRepository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<RmFormFdInsHdr> FindDetails(RmFormFdInsHdr frmFd)
        {
            return await _context.RmFormFdInsHdr.Include(x => x.RmFormFdInsDtl).Where(x => x.FdihRoadCode == frmFd.FdihRoadCode && x.FdihYearOfInsp == frmFd.FdihYearOfInsp && x.FdihActiveYn == true).FirstOrDefaultAsync();
        }
        public async Task<RmFormFdInsHdr> FindByHeaderID(int headerId)
        {
            return await _context.RmFormFdInsHdr.Include(x => x.RmFormFdInsDtl).Where(x => x.FdihPkRefNo == headerId && x.FdihActiveYn == true).FirstOrDefaultAsync();
        }
        public async Task<RmFormFdInsHdr> Save(RmFormFdInsHdr frmFd, bool updateSubmit)
        {
            //bool isAdd = false;
            if (frmFd.FdihPkRefNo == 0)
            {
                //isAdd = true;
                frmFd.FdihActiveYn = true;
                IDictionary<string, string> lstRef = new Dictionary<string, string>();
                lstRef.Add("Year", Utility.ToString(frmFd.FdihYearOfInsp));
                lstRef.Add("RoadCode", Utility.ToString(frmFd.FdihRoadCode));
                frmFd.FdihFormRefId = Common.RefNumber.FormRefNumber.GetRefNumber(FormType.FormFDHeader, lstRef);
                _context.RmFormFdInsHdr.Add(frmFd);
            }
            else
            {
                string[] arrNotReqUpdate = new string[] {
                    "FdihPkRefNo","FdihDivCode","FdihDist","FdihRmuName","FdihRoadId","FdihRoadCode","FdihRoadName","FdihRoadLength","FdihYearOfInsp","FdihFormRefId","FdihCrBy","FdihCrDt","FdihSubmitSts","FdihActiveYn"
                };
                _context.RmFormFdInsHdr.Attach(frmFd);
                var entry = _context.Entry(frmFd);
                entry.Properties.Where(x => !arrNotReqUpdate.Contains(x.Metadata.Name)).ToList().ForEach((p) =>
                {
                    p.IsModified = true;
                });
                if (updateSubmit)
                {
                    entry.Property(x => x.FdihSubmitSts).IsModified = true;
                }
                string[] arrDtlReqUpdate = new string[] { "FdidCondition", "FdidModBy", "FdidModDt", "FdidRemarks" };
                foreach (var dtl in frmFd.RmFormFdInsDtl)
                {
                    if (dtl.FdidPkRefNo > 0)
                    {
                        _context.RmFormFdInsDtl.Attach(dtl);
                        var dtlentry = _context.Entry(dtl);
                        dtlentry.Properties.Where(x => arrDtlReqUpdate.Contains(x.Metadata.Name)).ToList().ForEach((p) =>
                        {
                            p.IsModified = true;
                        });
                    }
                }
            }
            await _context.SaveChangesAsync();
            return frmFd;
        }
        public async Task<GridWrapper<object>> GetFormFDGridHeader(DataTableAjaxPostModel searchData)
        {
            var query = (from hdr in _context.RmFormFdInsHdr
                         from rd in _context.RmRoadMaster.Where(rd => rd.RdmPkRefNo == hdr.FdihRoadId).DefaultIfEmpty()
                             //from rmu in _context.RmDdLookup.Where(rd => rd.DdlType == "RMU" && (rd.DdlTypeValue == hdr.FdihRmuName) && rd.DdlActiveYn == true).DefaultIfEmpty()
                             //from sec in _context.RmDdLookup.Where(s => s.DdlType == "Section Code" && s.DdlTypeDesc == hdr.FdihSecName && s.DdlActiveYn == true).DefaultIfEmpty()
                         select new
                         {
                             RefNo = hdr.FdihPkRefNo,
                             RefID = hdr.FdihFormRefId,
                             InsDate = hdr.FdihDtInsBy,
                             Year = hdr.FdihYearOfInsp,
                             RMUCode = rd.RdmRmuCode,
                             RMUDesc = hdr.FdihRmuName,
                             SecCode = rd.RdmSecCode,
                             SecName = rd.RdmSecName,
                             RoadCode = hdr.FdihRoadCode,
                             RoadName = hdr.FdihRoadName,
                             RoadId = hdr.FdihRoadCode != null ? Convert.ToDecimal(hdr.FdihRoadCode.ToLower().Replace("q", "").Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", ".").Replace("a", "1").Replace("b", "2").Replace("c", "3").Replace("d", "4").Replace("e", "5").Replace("f", "6")) : 0,
                             InspectedByID = hdr.FdihUserIdInspBy.HasValue ? hdr.FdihUserIdInspBy.Value : 0,
                             InspectedBy = hdr.FdihUserNameInspBy,
                             CrewLeaderID = hdr.FdihCrewLeaderId.HasValue ? hdr.FdihCrewLeaderId.Value : 0,
                             CrewLeader = hdr.FdihCrewLeaderName,
                             Active = hdr.FdihActiveYn,
                             Status = (hdr.FdihSubmitSts ? "Submitted" : "Saved")
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
                                 || (x.SecCode.HasValue ? x.SecCode.Value.ToString() : "").Contains(strVal)
                                 || (x.SecName ?? "").Contains(strVal)
                                 || (x.RoadCode ?? "").Contains(strVal)
                                 || (x.RoadName ?? "").Contains(strVal)
                                 || (x.InspectedBy ?? "").Contains(strVal)
                                 || (x.CrewLeader ?? "").Contains(strVal)
                                 || (x.InspectedBy ?? "").Contains(strVal)
                                 || (x.Year.HasValue ? x.Year.Value.ToString() : "").Contains(strVal)
                                 || (x.InsDate.HasValue && dtSearch.HasValue && (x.InsDate == dtSearch || x.InsDate.ToString().Contains(strVal)))
                                 );
                            break;
                        case "fromInsDate":
                            DateTime? dtFrom = Utility.ToDateTime(strVal);
                            string toDate = Utility.ToString(searchData.filter["toInsDate"]);
                            if (toDate == "")
                                query = query.Where(x => x.InsDate >= dtFrom);
                            else
                            {
                                DateTime? dtTo = Utility.ToDateTime(toDate);
                                query = query.Where(x => x.InsDate >= dtFrom && x.InsDate <= dtTo);
                            }
                            break;
                        case "toInsDate":
                            string frmDate = Utility.ToString(searchData.filter["fromInsDate"]);
                            if (frmDate == "")
                            {
                                DateTime? dtTo = Utility.ToDateTime(strVal);
                                query = query.Where(x => x.InsDate <= dtTo);
                            }
                            break;
                        case "FromYear":
                            int iFYr = Utility.ToInt(strVal);
                            query = query.Where(x => x.Year.HasValue && x.Year >= iFYr);
                            break;
                        case "ToYear":
                            int iTYr = Utility.ToInt(strVal);
                            query = query.Where(x => x.Year.HasValue && x.Year <= iTYr);
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
            grid.data = await query.Order(searchData, query.OrderByDescending(searchData => searchData.RefNo)).Skip(searchData.start)
                .Take(searchData.length).ToListAsync();
            return grid;
        }
        public int DeleteHeader(RmFormFdInsHdr frmFD)
        {
            _context.RmFormFdInsHdr.Attach(frmFD);
            var entry = _context.Entry(frmFD);
            entry.Property(x => x.FdihActiveYn).IsModified = true;
            _context.SaveChanges();
            return frmFD.FdihPkRefNo;
        }

        public FormFDRpt GetReportData(int headerid)
        {
            FormFDRpt rpt = (from h in _context.RmFormFdInsHdr
                             join rd in _context.RmRoadMaster on h.FdihRoadId equals rd.RdmPkRefNo
                             where h.FdihPkRefNo == headerid
                             select new FormFDRpt
                             {
                                 CrewLeader = h.FdihCrewLeaderName,
                                 District = h.FdihDist,
                                 Division = rd.RdmDivCode,
                                 InspectedByName = h.FdihUserNameInspBy,
                                 InspectedDate = h.FdihDtInsBy,
                                 Remarks = h.FdihRemarks,
                                 RMU = h.FdihRmuName,
                                 RoadCode = h.FdihRoadCode,
                                 RoadLength = rd.RdmLengthPaved,
                                 RoadName = h.FdihRoadName,
                                 AssetTypes = h.FdihAssetTypes,
                                 FromCH = h.FdihFrmCh,
                                 ToCH = h.FdihToCh
                             }).FirstOrDefault();

            if (!string.IsNullOrEmpty(rpt.AssetTypes))
            {
                var AvgWidth = Common.Utility.JDeSerialize<FormAssetTypesDTO>(rpt.AssetTypes ?? "");

                if (AvgWidth.ContainsKey("DR"))
                {
                    var cw = AvgWidth["DR"];
                    foreach (var c in cw)
                    {
                        if (c.ContainsValue("Earth"))
                        {
                            if (c.ContainsKey("LAvgWidth"))
                            {
                                rpt.L_DR_E = c["LAvgWidth"];
                            }
                            if (c.ContainsKey("RAvgWidth"))
                            {
                                rpt.R_DR_E = c["RAvgWidth"];
                            }
                        }
                        else if (c.ContainsValue("Concrete"))
                        {
                            if (c.ContainsKey("LAvgWidth"))
                            {
                                rpt.L_DR_C = c["LAvgWidth"];
                            }
                            if (c.ContainsKey("RAvgWidth"))
                            {
                                rpt.R_DR_C = c["RAvgWidth"];
                            }
                        }
                        else if (c.ContainsValue("Block Stone"))
                        {
                            if (c.ContainsKey("LAvgWidth"))
                            {
                                rpt.L_DR_B = c["LAvgWidth"];
                            }
                            if (c.ContainsKey("RAvgWidth"))
                            {
                                rpt.R_DR_B = c["RAvgWidth"];
                            }
                        }
                    }
                }
                if (AvgWidth.ContainsKey("DI"))
                {
                    var cw = AvgWidth["DI"];
                    foreach (var c in cw)
                    {
                        if (c.ContainsValue("Gravel/Sand/Earth"))
                        {
                            if (c.ContainsKey("LAvgWidth"))
                            {
                                rpt.L_DI_G = c["LAvgWidth"];
                            }
                            if (c.ContainsKey("RAvgWidth"))
                            {
                                rpt.R_DI_G = c["RAvgWidth"];
                            }
                        }
                    }
                }
                if (AvgWidth.ContainsKey("SH"))
                {
                    var cw = AvgWidth["SH"];
                    foreach (var c in cw)
                    {
                        if (c.ContainsValue("Asphalt"))
                        {
                            if (c.ContainsKey("LAvgWidth"))
                            {
                                rpt.L_SH_A = c["LAvgWidth"];
                            }
                            if (c.ContainsKey("RAvgWidth"))
                            {
                                rpt.R_SH_A = c["RAvgWidth"];
                            }
                        }
                        else if (c.ContainsValue("Gravel"))
                        {
                            if (c.ContainsKey("LAvgWidth"))
                            {
                                rpt.L_SH_G = c["LAvgWidth"];
                            }
                            if (c.ContainsKey("RAvgWidth"))
                            {
                                rpt.R_SH_G = c["RAvgWidth"];
                            }
                        }
                        else if (c.ContainsValue("Earth"))
                        {
                            if (c.ContainsKey("LAvgWidth"))
                            {
                                rpt.L_SH_E = c["LAvgWidth"];
                            }
                            if (c.ContainsKey("RAvgWidth"))
                            {
                                rpt.R_SH_E = c["RAvgWidth"];
                            }
                        }
                        else if (c.ContainsValue("Concrete"))
                        {
                            if (c.ContainsKey("LAvgWidth"))
                            {
                                rpt.L_SH_C = c["LAvgWidth"];
                            }
                            if (c.ContainsKey("RAvgWidth"))
                            {
                                rpt.R_SH_C = c["RAvgWidth"];
                            }
                        }
                        else if (c.ContainsValue("Gravel/Sand/Earth"))
                        {
                            if (c.ContainsKey("LAvgWidth"))
                            {
                                rpt.L_SH_G = c["LAvgWidth"];
                            }
                            if (c.ContainsKey("RAvgWidth"))
                            {
                                rpt.R_SH_G = c["RAvgWidth"];
                            }
                        }
                        else if (c.ContainsValue("Footpath/Kerb"))
                        {
                            if (c.ContainsKey("LAvgWidth"))
                            {
                                rpt.L_SH_F = c["LAvgWidth"];
                            }
                            if (c.ContainsKey("RAvgWidth"))
                            {
                                rpt.R_SH_F = c["RAvgWidth"];
                            }
                        }
                    }
                }
            }

            decimal MinFromCH = rpt.FromCH.GetValueOrDefault();
            decimal MaxFromCH = rpt.ToCH.GetValueOrDefault();
            List<FormFDDetail> lst = new List<FormFDDetail>();
            var _data = _context.RmFormFdInsDtl
                                .Where(o => o.FdidFdihPkRefNo == headerid)
                                .Select(o => o).ToList();
            decimal MAX = _data.Max(x => Convert.ToDecimal(x.FdidAiFrmCh.Value.ToString() + "." + x.FdidAiFrmChDeci));
            if (MAX > MaxFromCH)
            {
                MaxFromCH = MAX;
            }
            decimal tempMinFromCH = MinFromCH;
            while (tempMinFromCH <= MaxFromCH)
            {
                string str = tempMinFromCH.ToString();
                str = str.Substring(0, str.IndexOf('.'));
                //while (i < max)
                //{

                FormFDDetail fc = new FormFDDetail();
                fc.KMTitle = $"KM {str}+{tempMinFromCH.ToString().Substring(tempMinFromCH.ToString().IndexOf('.') + 1, (tempMinFromCH.ToString().Length - 1 - tempMinFromCH.ToString().IndexOf('.')))}";

                var from = tempMinFromCH;
                var to = tempMinFromCH + (decimal)0.100;

                var data = _data.Where(x => x.FdidAiFrmCh.HasValue && x.FdidAiFrmChDeci != null
                && x.FdidAiToCh.HasValue && x.FdidAiToChDeci != null &&
                     ((Convert.ToDecimal(x.FdidAiFrmCh.Value.ToString() + "." + x.FdidAiFrmChDeci) == from)
                            ||
                            (Convert.ToDecimal(x.FdidAiToCh.Value.ToString() + "." + x.FdidAiToChDeci) == from)))
                            .Select(o => o).ToList();
                fc.FromCh = from;
                if (data.Count > 0)
                {
                    var e = data.Where(s => s.FdidAiAssetGrpCode == "DI"
                    && s.FdidAiGrpType == "Gravel/Sand/Earth"
                    && s.FdidAiBound == "L").FirstOrDefault();
                    fc.Left_Ditch_GravelSandEarth = e != null ? e.FdidCondition : null;

                    e = data.Where(s => s.FdidAiAssetGrpCode == "DR"
                     && s.FdidAiGrpType == "Earth"
                     && s.FdidAiBound == "L"
                     ).FirstOrDefault();
                    fc.Left_Drain_Earth = e != null ? e.FdidCondition : null;

                    e = data.Where(s => s.FdidAiAssetGrpCode == "DR"
                    && s.FdidAiGrpType == "Block Stone"
                    && s.FdidAiBound == "L"
                    ).FirstOrDefault();
                    fc.Left_Drain_Blockstone = e != null ? e.FdidCondition : null;

                    e = data.Where(s => s.FdidAiAssetGrpCode == "DR"
                   && s.FdidAiGrpType == "Concrete"
                   && s.FdidAiBound == "L"
                   ).FirstOrDefault();
                    fc.Left_Drain_Concrete = e != null ? e.FdidCondition : null;

                    e = data.Where(s => s.FdidAiAssetGrpCode == "SH"
                  && s.FdidAiGrpType == "Asphalt"
                  && s.FdidAiBound == "L"
                  ).FirstOrDefault();
                    fc.Left_Shoulder_Asphalt = e != null ? e.FdidCondition : null;

                    e = data.Where(s => s.FdidAiAssetGrpCode == "SH"
                 && s.FdidAiGrpType == "Concrete"
                 && s.FdidAiBound == "L"
                 ).FirstOrDefault();
                    fc.Left_Shoulder_Concrete = e != null ? e.FdidCondition : null;

                    e = data.Where(s => s.FdidAiAssetGrpCode == "SH"
                && s.FdidAiGrpType == "Earth"
                && s.FdidAiBound == "L"
                ).FirstOrDefault();
                    fc.Left_Shoulder_Earth = e != null ? e.FdidCondition : null;

                    e = data.Where(s => s.FdidAiAssetGrpCode == "SH"
                && s.FdidAiGrpType == "Gravel"
                && s.FdidAiBound == "L"
                ).FirstOrDefault();
                    fc.Left_Shoulder_Gravel = e != null ? e.FdidCondition : null;


                    e = data.Where(s => s.FdidAiAssetGrpCode == "SH"
                && s.FdidAiGrpType == "Footpath/Kerb"
                && s.FdidAiBound == "L"
                ).FirstOrDefault();
                    fc.Left_Shoulder_FootpathKerb = e != null ? e.FdidCondition : null;

                    //
                    e = data.Where(s => s.FdidAiAssetGrpCode == "DI"
                    && s.FdidAiGrpType == "Gravel/Sand/Earth"
                    && s.FdidAiBound == "R").FirstOrDefault();
                    fc.Right_Ditch_GravelSandEarth = e != null ? e.FdidCondition : null;

                    e = data.Where(s => s.FdidAiAssetGrpCode == "DR"
                     && s.FdidAiGrpType == "Earth"
                     && s.FdidAiBound == "R"
                     ).FirstOrDefault();
                    fc.Right_Drain_Earth = e != null ? e.FdidCondition : null;

                    e = data.Where(s => s.FdidAiAssetGrpCode == "DR"
                    && s.FdidAiGrpType == "Block Stone"
                    && s.FdidAiBound == "R"
                    ).FirstOrDefault();
                    fc.Right_Drain_Blockstone = e != null ? e.FdidCondition : null;

                    e = data.Where(s => s.FdidAiAssetGrpCode == "DR"
                   && s.FdidAiGrpType == "Concrete"
                   && s.FdidAiBound == "R"
                   ).FirstOrDefault();
                    fc.Right_Drain_Concrete = e != null ? e.FdidCondition : null;

                    e = data.Where(s => s.FdidAiAssetGrpCode == "SH"
                  && s.FdidAiGrpType == "Asphalt"
                  && s.FdidAiBound == "R"
                  ).FirstOrDefault();
                    fc.Right_Shoulder_Asphalt = e != null ? e.FdidCondition : null;

                    e = data.Where(s => s.FdidAiAssetGrpCode == "SH"
                 && s.FdidAiGrpType == "Concrete"
                 && s.FdidAiBound == "R"
                 ).FirstOrDefault();
                    fc.Right_Shoulder_Concrete = e != null ? e.FdidCondition : null;

                    e = data.Where(s => s.FdidAiAssetGrpCode == "SH"
                && s.FdidAiGrpType == "Earth"
                && s.FdidAiBound == "R"
                ).FirstOrDefault();
                    fc.Right_Shoulder_Earth = e != null ? e.FdidCondition : null;

                    e = data.Where(s => s.FdidAiAssetGrpCode == "SH"
                && s.FdidAiGrpType == "Gravel"
                && s.FdidAiBound == "R"
                ).FirstOrDefault();
                    fc.Right_Shoulder_Gravel = e != null ? e.FdidCondition : null;


                    e = data.Where(s => s.FdidAiAssetGrpCode == "SH"
                && s.FdidAiGrpType == "Footpath/Kerb"
                && s.FdidAiBound == "R"
                ).FirstOrDefault();
                    fc.Right_Shoulder_FootpathKerb = e != null ? e.FdidCondition : null;


                }

                tempMinFromCH = tempMinFromCH + (decimal)0.100;
                lst.Add(fc);
            }

            rpt.Details = lst.ToArray();
            return rpt;
        }
    }
}
