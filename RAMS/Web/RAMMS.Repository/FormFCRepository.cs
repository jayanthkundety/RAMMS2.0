using Microsoft.EntityFrameworkCore;
using RAMMS.Common;
using RAMMS.Common.RefNumber;
using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using RAMMS.DTO.ResponseBO;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository
{
    public class FormFCRepository : RepositoryBase<RmFormFcInsHdr>, IFormFCRepository
    {
        public FormFCRepository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<RmFormFcInsHdr> FindDetails(RmFormFcInsHdr frmFC)
        {
            return await _context.RmFormFcInsHdr.Include(x => x.RmFormFcInsDtl).Where(x => x.FcihRoadCode == frmFC.FcihRoadCode && x.FcihYearOfInsp == frmFC.FcihYearOfInsp && x.FcihActiveYn == true).FirstOrDefaultAsync();
        }
        public async Task<RmFormFcInsHdr> FindByHeaderID(int headerId)
        {
            return await _context.RmFormFcInsHdr.Include(x => x.RmFormFcInsDtl).Where(x => x.FcihPkRefNo == headerId && x.FcihActiveYn == true).FirstOrDefaultAsync();
        }
        public async Task<RmFormFcInsHdr> Save(RmFormFcInsHdr frmFC, bool updateSubmit)
        {
            //bool isAdd = false;
            if (frmFC.FcihPkRefNo == 0)
            {
                //isAdd = true;
                frmFC.FcihActiveYn = true;
                IDictionary<string, string> lstRef = new Dictionary<string, string>();
                lstRef.Add("Year", Utility.ToString(frmFC.FcihYearOfInsp));
                lstRef.Add("RoadCode", Utility.ToString(frmFC.FcihRoadCode));
                frmFC.FcihFormRefId = Common.RefNumber.FormRefNumber.GetRefNumber(FormType.FormFCHeader, lstRef);
                _context.RmFormFcInsHdr.Add(frmFC);
            }
            else
            {
                string[] arrNotReqUpdate = new string[] {
                    "FcihPkRefNo","FcihDivCode","FcihDist","FcihRmuName","FcihRoadId","FcihRoadCode","FcihRoadName","FcihRoadLength","FcihYearOfInsp","FcihFormRefId","FcihCrBy","FcihCrDt","FcihSubmitSts","FcihActiveYn"
                };
                //_context.RmFormS1Dtl.Update(formS1Details);
                //var dtls = frmC1C2.RmFormCvInsDtl;
                //frmC1C2.RmFormCvInsDtl = null;
                _context.RmFormFcInsHdr.Attach(frmFC);
                var entry = _context.Entry(frmFC);
                entry.Properties.Where(x => !arrNotReqUpdate.Contains(x.Metadata.Name)).ToList().ForEach((p) =>
                {
                    p.IsModified = true;
                });
                if (updateSubmit)
                {
                    entry.Property(x => x.FcihSubmitSts).IsModified = true;
                }
                string[] arrDtlReqUpdate = new string[] { "FcidCondition", "FcidModBy", "FcidModDt", "FcidRemarks" };
                foreach (var dtl in frmFC.RmFormFcInsDtl)
                {
                    if (dtl.FcidPkRefNo > 0)
                    {
                        _context.RmFormFcInsDtl.Attach(dtl);
                        var dtlentry = _context.Entry(dtl);
                        dtlentry.Properties.Where(x => arrDtlReqUpdate.Contains(x.Metadata.Name)).ToList().ForEach((p) =>
                        {
                            p.IsModified = true;
                        });
                    }
                }
            }
            await _context.SaveChangesAsync();
            return frmFC;
        }

        public async Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData)
        {
            var query = (from hdr in _context.RmFormFcInsHdr
                         from rd in _context.RmRoadMaster.Where(rd => rd.RdmPkRefNo == hdr.FcihRoadId).DefaultIfEmpty()
                         select new
                         {
                             RefNo = hdr.FcihPkRefNo,
                             RefID = hdr.FcihFormRefId,
                             InsDate = hdr.FcihDtInspBy,
                             Year = hdr.FcihYearOfInsp,
                             RMUCode = rd.RdmRmuCode,
                             RMUDesc = hdr.FcihRmuName,
                             SecCode = rd.RdmSecCode,
                             SecName = rd.RdmSecName,
                             RoadCode = hdr.FcihRoadCode,
                             RoadName = hdr.FcihRoadName,
                             RoadId = hdr.FcihRoadCode != null ? Convert.ToDecimal(hdr.FcihRoadCode.ToLower().Replace("q", "").Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", ".").Replace("a", "1").Replace("b", "2").Replace("c", "3").Replace("d", "4").Replace("e", "5").Replace("f", "6")) : 0,
                             InspectedByID = hdr.FcihUserIdInspBy.HasValue ? hdr.FcihUserIdInspBy.Value : 0,
                             InspectedBy = hdr.FcihUserNameInspBy,
                             CrewLeaderID = hdr.FcihCrewLeaderId.HasValue ? hdr.FcihCrewLeaderId.Value : 0,
                             CrewLeader = hdr.FcihCrewLeaderName,
                             Active = hdr.FcihActiveYn,
                             Status = (hdr.FcihSubmitSts ? "Submitted" : "Saved")
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
                                 || (x.InsDate.HasValue && dtSearch.HasValue && (x.InsDate.ToString().Contains(strVal) || x.InsDate == dtSearch))
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
            grid.data = await query.Order(searchData, query.OrderByDescending(s => s.RefNo)).Skip(searchData.start)
                                .Take(searchData.length)
                                .ToListAsync(); ;

            return grid;
        }
        public int DeleteHeader(RmFormFcInsHdr frmFC)
        {
            _context.RmFormFcInsHdr.Attach(frmFC);
            var entry = _context.Entry(frmFC);
            entry.Property(x => x.FcihActiveYn).IsModified = true;
            _context.SaveChanges();
            return frmFC.FcihPkRefNo;
        }

        public FormFCRpt GetReportData(int headerid)
        {
            FormFCRpt rpt = (from h in _context.RmFormFcInsHdr
                             join rd in _context.RmRoadMaster on h.FcihRoadId equals rd.RdmPkRefNo
                             where h.FcihPkRefNo == headerid
                             select new FormFCRpt
                             {
                                 CrewLeader = h.FcihCrewLeaderName,
                                 District = h.FcihDist,
                                 Division = h.FcihDivCode,
                                 InspectedByName = h.FcihUserNameInspBy,
                                 InspectedDate = h.FcihDtInspBy,
                                 Remarks = h.FcihRemarks,
                                 RMU = h.FcihRmuName,
                                 RoadCode = h.FcihRoadCode,
                                 RoadLength = rd.RdmLengthPaved,
                                 RoadName = h.FcihRoadName,
                                 AssetTypes = h.FcihAssetTypes,
                                 FromCH = h.FcihFrmCh,
                                 ToCH = h.FcihToCh
                             }).FirstOrDefault();
            if (!string.IsNullOrEmpty(rpt.AssetTypes))
            {
                var AvgWidth = Common.Utility.JDeSerialize<FormAssetTypesDTO>(rpt.AssetTypes ?? "");

                if (AvgWidth.ContainsKey("RS"))
                {
                    var cw = AvgWidth["RS"];
                    foreach (var c in cw)
                    {
                        if (c.ContainsValue("Left"))
                        {
                            if (c.ContainsKey("LAvgWidth"))
                            {
                                rpt.L_R = c["LAvgWidth"];
                            }
                        }
                        if (c.ContainsValue("Centre"))
                        {
                            if (c.ContainsKey("AvgWidth"))
                            {
                                rpt.C_R = c["AvgWidth"];
                            }
                        }
                        if (c.ContainsValue("Right"))
                        {
                            if (c.ContainsKey("RAvgWidth"))
                            {
                                rpt.R_R = c["RAvgWidth"];
                            }
                        }
                    }
                }
                if (AvgWidth.ContainsKey("ELM"))
                {
                    var cw = AvgWidth["ELM"];
                    foreach (var c in cw)
                    {
                        if (c.ContainsValue("Paint"))
                        {
                            if (c.ContainsKey("LAvgWidth"))
                            {
                                rpt.L_E_P = c["LAvgWidth"];
                            }
                            if (c.ContainsKey("RAvgWidth"))
                            {
                                rpt.R_E_P = c["RAvgWidth"];
                            }
                        }

                        if (c.ContainsValue("Thermoplastic"))
                        {
                            if (c.ContainsKey("LAvgWidth"))
                            {
                                rpt.L_E_T = c["LAvgWidth"];
                            }
                            if (c.ContainsKey("RAvgWidth"))
                            {
                                rpt.R_E_T = c["RAvgWidth"];
                            }
                        }
                    }
                }

                if (AvgWidth.ContainsKey("CLM"))
                {
                    var cw = AvgWidth["CLM"];
                    foreach (var c in cw)
                    {
                        if (c.ContainsValue("Paint"))
                        {
                            if (c.ContainsKey("AvgWidth"))
                            {
                                rpt.C_C_P = c["AvgWidth"];
                            }
                        }
                        else if (c.ContainsValue("Thermoplastic"))
                        {
                            if (c.ContainsKey("AvgWidth"))
                            {
                                rpt.C_C_T = c["AvgWidth"];
                            }
                        }

                    }
                }

                if (AvgWidth.ContainsKey("CW"))
                {
                    var cw = AvgWidth["CW"];
                    foreach (var c in cw)
                    {
                        if (c.ContainsValue("Asphalt"))
                        {
                            if (c.ContainsKey("AvgWidth"))
                            {
                                rpt.C_P_A = c["AvgWidth"];
                            }
                        }
                        else if (c.ContainsValue("Surface Dressed"))
                        {
                            if (c.ContainsKey("AvgWidth"))
                            {
                                rpt.C_P_D = c["AvgWidth"];
                            }
                        }
                        else if (c.ContainsValue("Gravel"))
                        {
                            if (c.ContainsKey("AvgWidth"))
                            {
                                rpt.C_P_G = c["AvgWidth"];
                            }
                        }
                        else if (c.ContainsValue("Earth"))
                        {
                            if (c.ContainsKey("AvgWidth"))
                            {
                                rpt.C_P_E = c["AvgWidth"];
                            }
                        }
                        else if (c.ContainsValue("Concrete"))
                        {
                            if (c.ContainsKey("AvgWidth"))
                            {
                                rpt.C_P_C = c["AvgWidth"];
                            }
                        }
                        else if (c.ContainsValue("Sand"))
                        {
                            if (c.ContainsKey("AvgWidth"))
                            {
                                rpt.C_P_S = c["AvgWidth"];
                            }
                        }

                    }
                }
            }

            decimal MinFromCH = rpt.FromCH.GetValueOrDefault();
            decimal MaxFromCH = rpt.ToCH.GetValueOrDefault();
            List<FormFCDetail> lst = new List<FormFCDetail>();
            decimal tempMinFromCH = MinFromCH;
            var _data = _context.RmFormFcInsDtl
                               .Where(o => o.FcidFcihPkRefNo == headerid)
                               .Select(o => o).ToList();
            decimal MAX = _data.Max(x => Convert.ToDecimal(x.FcidAiFrmCh.Value.ToString() + "." + x.FcidAiFrmChDeci));
            if (MAX > MaxFromCH)
            {
                MaxFromCH = MAX;
            }
            while (tempMinFromCH <= MaxFromCH)
            {

                int i = 0;
                int iteration = 99;
                int max = 1000;
                string str = tempMinFromCH.ToString();
                str = str.Substring(0, str.IndexOf('.'));
                //while (i < max)
                //{
                FormFCDetail fc = new FormFCDetail();
                fc.KMTitle = $"KM {str}+{tempMinFromCH.ToString().Substring(tempMinFromCH.ToString().IndexOf('.') + 1, (tempMinFromCH.ToString().Length - 1 - tempMinFromCH.ToString().IndexOf('.')))}";
                var from = tempMinFromCH;
                var to = tempMinFromCH + (decimal)0.100;
                var data = _data.Where(x => x.FcidFcihPkRefNo == headerid
                            && ((Convert.ToDecimal(x.FcidAiFrmCh.Value.ToString() + "." + x.FcidAiFrmChDeci) == from))
                            )
                            .Select(o => o).ToList();
                fc.FromCh = from;
                if (data.Count > 0)
                {
                    var e = data.Where(s => s.FcidAiAssetGrpCode == "ELM" && s.FcidAiGrpType == "Paint" && (s.FcidAiBound == "L" || s.FcidAiBound == "Left")).FirstOrDefault();

                    fc.Left_EdgeLine_Paint = e != null ? e.FcidCondition : null;

                    e = data.Where(s => s.FcidAiAssetGrpCode == "ELM" && s.FcidAiGrpType == "Thermoplastic" && (s.FcidAiBound == "L" || s.FcidAiBound == "Left")).FirstOrDefault();

                    fc.Left_EdgeLine_Thermoplastic = e != null ? e.FcidCondition : null;

                    e = data.Where(s => s.FcidAiAssetGrpCode == "RS" && (s.FcidAiBound == "L" || s.FcidAiBound == "Left")).FirstOrDefault();
                    fc.Left_RoadStuds = e != null ? e.FcidCondition : null;

                    e = data.Where(s => s.FcidAiAssetGrpCode == "CW" && s.FcidAiGrpType.Contains("Asphalt")).FirstOrDefault();
                    fc.CarriageWay_Pavment_Asphalt = e != null ? e.FcidCondition : null;

                    e = data.Where(s => s.FcidAiAssetGrpCode == "CW" && s.FcidAiGrpType == "Concrete").FirstOrDefault();
                    fc.CarriageWay_Pavment_Concrete = e != null ? e.FcidCondition : null;

                    e = data.Where(s => s.FcidAiAssetGrpCode == "CW" && s.FcidAiGrpType == "Earth").FirstOrDefault();
                    fc.CarriageWay_Pavement_Earth = e != null ? e.FcidCondition : (int?)null;

                    e = data.Where(s => s.FcidAiAssetGrpCode == "CW" && s.FcidAiGrpType == "Gravel").FirstOrDefault();
                    fc.CarriageWay_Pavment_Gravel = e != null ? e.FcidCondition : null;

                    e = data.Where(s => s.FcidAiAssetGrpCode == "CW" && s.FcidAiGrpType == "Sand").FirstOrDefault();
                    fc.CarriageWay_Pavment_Sand = e != null ? e.FcidCondition : null;

                    e = data.Where(s => s.FcidAiAssetGrpCode == "CW" && s.FcidAiGrpType == "Surface Dressed").FirstOrDefault();
                    fc.CarriageWay_Pavment_SurfaceDressed = e != null ? e.FcidCondition : null;

                    e = data.Where(s => s.FcidAiAssetGrpCode == "RS" && (s.FcidAiBound == "M")).FirstOrDefault();
                    fc.CarriageWay_CentreRoadStuds = e != null ? e.FcidCondition : null;

                    e = data.Where(s => s.FcidAiAssetGrpCode == "CLM" && s.FcidAiGrpType == "Paint").FirstOrDefault();
                    fc.CarriageWay_CentreLine_Paint = e != null ? e.FcidCondition : null;

                    e = data.Where(s => s.FcidAiAssetGrpCode == "CLM" && s.FcidAiGrpType == "Thermoplastic").FirstOrDefault();
                    fc.CarriageWay_CentreLine_Thermoplastic = e != null ? e.FcidCondition : null;

                    e = data.Where(s => s.FcidAiAssetGrpCode == "RS" && (s.FcidAiBound == "Right" || s.FcidAiBound == "R")).FirstOrDefault();
                    fc.Right_RoadStuds = e != null ? e.FcidCondition : null;

                    e = data.Where(s => s.FcidAiAssetGrpCode == "ELM" && s.FcidAiGrpType == "Paint" && (s.FcidAiBound == "R" || s.FcidAiBound == "Right")).FirstOrDefault();
                    fc.Right_EdgeLine_Paint = e != null ? e.FcidCondition : null;

                    e = data.Where(s => s.FcidAiAssetGrpCode == "ELM" && s.FcidAiGrpType == "Thermoplastic" && (s.FcidAiBound == "R" || s.FcidAiBound == "Right")).FirstOrDefault();
                    fc.Right_Thermoplastic = e != null ? e.FcidCondition : null;
                }
                tempMinFromCH = tempMinFromCH + (decimal)0.100;
                lst.Add(fc);

                //}
                //tempMinFromCH += 1;


            }

            rpt.Details = lst.ToArray();
            return rpt;
        }
    }
}
