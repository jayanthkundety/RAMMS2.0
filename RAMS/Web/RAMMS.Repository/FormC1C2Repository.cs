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
    public class FormC1C2Repository : RepositoryBase<RmFormCvInsHdr>, IFormC1C2Repository
    {
        public FormC1C2Repository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<RmFormCvInsHdr> FindDetails(RmFormCvInsHdr frmC1C2)
        {
            return await _context.RmFormCvInsHdr.Include(x => x.RmFormCvInsDtl).ThenInclude(x => x.FcvidIimPkRefNoNavigation).Where(x => x.FcvihAiAssetId == frmC1C2.FcvihAiAssetId && x.FcvihYearOfInsp == frmC1C2.FcvihYearOfInsp && x.FcvihActiveYn == true).FirstOrDefaultAsync();
        }
        public async Task<RmFormCvInsHdr> FindByHeaderID(int headerId)
        {

            return await _context.RmFormCvInsHdr.Include(x => x.RmFormCvInsDtl).ThenInclude(x => x.FcvidIimPkRefNoNavigation).Where(x => x.FcvihPkRefNo == headerId && x.FcvihActiveYn == true).FirstOrDefaultAsync();
        }
        public async Task<RmFormCvInsHdr> Save(RmFormCvInsHdr frmC1C2, bool updateSubmit)
        {
            //bool isAdd = false;
            if (frmC1C2.FcvihPkRefNo == 0)
            {
                //isAdd = true;
                frmC1C2.FcvihActiveYn = true;
                IDictionary<string, string> lstRef = new Dictionary<string, string>();
                lstRef.Add("Year", Utility.ToString(frmC1C2.FcvihYearOfInsp));
                lstRef.Add("AssetID", Utility.ToString(frmC1C2.FcvihAiAssetId));
                frmC1C2.FcvihCInspRefNo = Common.RefNumber.FormRefNumber.GetRefNumber(FormType.FormC1C2, lstRef);
                _context.RmFormCvInsHdr.Add(frmC1C2);
            }
            else
            {
                string[] arrNotReqUpdate = new string[] { "FcvihPkRefNo", "FcvihCInspRefNo", "FcvihAiPkRefNo", "FcvihAiAssetId",
                    "FcvihAiDivCode", "FcvihAiRmuName", "FcvihAiRdCode","FcvihAiRdName","FcvihAiLocChKm","FcvihAiLocChM","FcvihAiFinRdLevel","FcvihAiStrucCode","FcvihAiCatchArea","FcvihAiSkew",
                    "FcvihAiDesignFlow","FcvihAiLength","FcvihAiPrecastSitu","FcvihAiGrpType","FcvihAiBarrelNo","FcvihAiGpsEasting","FcvihAiGpsNorthing","FcvihAiMaterial","FcvihAiIntelLevel","FcvihAiOutletLevel",
                    "FcvihAiIntelStruc","FcvihAiOutletStruc","FcvihYearOfInsp","FcvihCrBy","FcvihCrDt"
                };
                //_context.RmFormS1Dtl.Update(formS1Details);
                //var dtls = frmC1C2.RmFormCvInsDtl;
                //frmC1C2.RmFormCvInsDtl = null;
                _context.RmFormCvInsHdr.Attach(frmC1C2);
                var entry = _context.Entry(frmC1C2);
                entry.Properties.Where(x => !arrNotReqUpdate.Contains(x.Metadata.Name)).ToList().ForEach((p) =>
                {
                    p.IsModified = true;
                });
                if (updateSubmit)
                {
                    entry.Property(x => x.FcvihSubmitSts).IsModified = true;
                }
                string[] arrDtlReqUpdate = new string[] { "FcvidDistress", "FcvidSeverity", "FcvidDistressOthers" };
                foreach (var dtl in frmC1C2.RmFormCvInsDtl)
                {
                    if (dtl.FcvidPkRefNo > 0)
                    {
                        _context.RmFormCvInsDtl.Attach(dtl);
                        var dtlentry = _context.Entry(dtl);
                        dtlentry.Properties.Where(x => arrDtlReqUpdate.Contains(x.Metadata.Name)).ToList().ForEach((p) =>
                        {
                            p.IsModified = true;
                        });
                    }
                }
            }
            await _context.SaveChangesAsync();
            return frmC1C2;
        }
        public async Task<List<FormC1C2PhotoTypeDTO>> GetExitingPhotoType(int headerId)
        {
            return await _context.RmFormCvInsImage.Where(x => x.FcviFcvihPkRefNo == headerId).GroupBy(x => x.FcviImageTypeCode).Select(x => new FormC1C2PhotoTypeDTO()
            {
                SNO = x.Max(y => y.FcviImageSrno.Value),
                Type = x.Key
            }).ToListAsync();
        }
        public async Task<RmFormCvInsImage> AddImage(RmFormCvInsImage image)
        {
            _context.RmFormCvInsImage.Add(image);
            await _context.SaveChangesAsync();
            return image;
        }
        public async Task<IList<RmFormCvInsImage>> AddMultiImage(IList<RmFormCvInsImage> images)
        {
            _context.RmFormCvInsImage.AddRange(images);
            await _context.SaveChangesAsync();
            return images;
        }

        public async Task<int> ImageCount(string type, long headerId)
        {
            return await _context.RmFormCvInsImage.Where(s => s.FcviImageTypeCode == type && s.FcviFcvihPkRefNo == headerId).CountAsync();

        }
        public async Task<List<RmFormCvInsImage>> ImageList(int headerId)
        {
            return await _context.RmFormCvInsImage.Where(x => x.FcviFcvihPkRefNo == headerId && x.FcviActiveYn == true).ToListAsync();
        }
        public async Task<int> DeleteImage(RmFormCvInsImage img)
        {
            _context.RmFormCvInsImage.Attach(img);
            var entry = _context.Entry(img);
            entry.Property(x => x.FcviActiveYn).IsModified = true;
            await _context.SaveChangesAsync();
            return img.FcviPkRefNo;
        }

        public async Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData)
        {
            var query = (from hdr in _context.RmFormCvInsHdr.Where(s => s.FcvihActiveYn == true)
                         from rmu in _context.RmDdLookup.Where(rd => rd.DdlType == "RMU" && (rd.DdlTypeDesc == hdr.FcvihAiRmuName)).DefaultIfEmpty()
                         from asset in _context.RmAllassetInventory.Where(a => a.AiPkRefNo == hdr.FcvihAiPkRefNo).DefaultIfEmpty()
                         let rdcode = _context.RmRoadMaster.Where(r => r.RdmRdCode == hdr.FcvihAiRdCode && r.RdmActiveYn == true).DefaultIfEmpty().FirstOrDefault()
                         select new
                         {
                             RefNo = hdr.FcvihPkRefNo,
                             RefID = hdr.FcvihCInspRefNo,
                             AssetID = hdr.FcvihAiPkRefNo,
                             AssetRefId = hdr.FcvihAiAssetId,
                             InsDate = hdr.FcvihDtOfInsp,
                             Year = hdr.FcvihYearOfInsp,
                             RMUCode = rmu.DdlTypeCode,
                             RMUDesc = hdr.FcvihAiRmuName,
                             SecCode = asset.AiSecCode,
                             SecName = asset.AiSecName,
                             Bound = asset.AiBound,
                             AssetType = hdr.FcvihAiStrucCode,
                             RoadCode = hdr.FcvihAiRdCode,
                             RoadName = hdr.FcvihAiRdName,
                             RoadId = rdcode.RdmRdCdSort,// asset.AiRdmPkRefNoNavigation.RdmRdCdSort,
                             LocationCH = Convert.ToDecimal((hdr.FcvihAiLocChKm.HasValue ? hdr.FcvihAiLocChKm.Value.ToString() : "") + "." + hdr.FcvihAiLocChM),
                             InspectedBy = hdr.FcvihSerProviderUserName,
                             AuditedBy = hdr.FcvihUserNameAud,
                             CDia = asset.AiDiameter,
                             CULWidth = asset.AiWidth,
                             Active = hdr.FcvihActiveYn,
                             Status = (hdr.FcvihSubmitSts ? "Submitted" : "Saved")
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
                                 || (x.AssetRefId ?? "").Contains(strVal)
                                 || (x.SecCode ?? "").Contains(strVal)
                                 || (x.SecName ?? "").Contains(strVal)
                                 || (x.RoadCode ?? "").Contains(strVal)
                                 || (x.RoadName ?? "").Contains(strVal)
                                 || (x.LocationCH.ToString() ?? "").Contains(strVal)
                                 || (x.AssetType ?? "").Contains(strVal)
                                 || (x.InspectedBy ?? "").Contains(strVal)
                                 || (x.AuditedBy ?? "").Contains(strVal)
                                 || (x.Year.HasValue ? x.Year.Value.ToString() : "").Contains(strVal)
                                 || (!x.CDia.HasValue ? "" : x.CDia.Value.ToString()).Contains(strVal)
                                 || (!x.CULWidth.HasValue ? "" : x.CULWidth.Value.ToString()).Contains(strVal)
                                 || (x.InsDate.HasValue && ((x.InsDate.Value.ToString().Contains(strVal)) || (dtSearch.HasValue && x.InsDate == dtSearch)))
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
                        case "chFromKM":
                            string strM = Utility.ToString(searchData.filter["chFromM"]);
                            decimal flKm = Utility.ToDecimal(strVal + (strM != "" ? "." + strM : ""));
                            query = query.Where(x => x.LocationCH >= flKm);
                            break;
                        case "chFromM":
                            string strKm = Utility.ToString(searchData.filter["chFromKM"]);
                            if (strKm == "")
                            {
                                decimal flM = Utility.ToDecimal("0." + strVal);
                                query = query.Where(x => x.LocationCH >= flM);
                            }
                            break;
                        case "chToKm":
                            string strTM = Utility.ToString(searchData.filter["chToM"]);
                            decimal flTKm = Utility.ToDecimal(strVal + (strTM != "" ? "." + strTM : ""));
                            query = query.Where(x => x.LocationCH <= flTKm);
                            break;
                        case "chToM":
                            string strTKm = Utility.ToString(searchData.filter["chToKm"]);
                            if (strTKm == "")
                            {
                                decimal flTM = Utility.ToDecimal("0." + strVal);
                                query = query.Where(x => x.LocationCH <= flTM);
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

        public async Task<List<RmInspItemMas>> GetInspItemMaster()
        {
            return await _context.RmInspItemMas.Include(x => x.RmInspItemMasDtl).Where(x => x.IimActiveYn == true).ToListAsync();
        }
        public int DeleteHeader(RmFormCvInsHdr frmC1C2)
        {
            _context.RmFormCvInsHdr.Attach(frmC1C2);
            var entry = _context.Entry(frmC1C2);
            entry.Property(x => x.FcvihActiveYn).IsModified = true;
            _context.SaveChanges();
            return frmC1C2.FcvihPkRefNo;
        }

        public List<FormC1C2Rpt> GetReportData(int headerid)
        {
            return GetReportDataV2(headerid);
        }


        public List<FormC1C2Rpt> GetReportDataV2(int headerid)
        {
            Func<string, int, (string distress, int? severity)> distressSeverity = (code, headerid) =>
            {
                var ddl = _context.RmFormCvInsDtl.Where(s => s.FcvidFcvihPkRefNo == headerid && s.FcvidInspCode == code).FirstOrDefault();
                if (ddl != null)
                {
                    return (ddl.FcvidDistress, ddl.FcvidSeverity);
                }
                else
                    return ("", null);
            };

            var type = (from ty in _context.RmDdLookup
                        where ty.DdlType == "Photo Type" && ty.DdlTypeCode == "CV"
                        orderby ty.DdlTypeRemarks ascending
                        select ty).ToList();
            var roadcode = (from o in _context.RmFormCvInsHdr
                            where o.FcvihPkRefNo == headerid
                            select new { o.FcvihAiRdCode, o.FcvihDtOfInsp }).FirstOrDefault();

            List<FormC1C2Rpt> detail = (from o in _context.RmFormCvInsHdr
                                        where (o.FcvihAiRdCode == roadcode.FcvihAiRdCode && o.FcvihDtOfInsp.HasValue && o.FcvihDtOfInsp < roadcode.FcvihDtOfInsp) || o.FcvihPkRefNo == headerid
                                        select new FormC1C2Rpt
                                        {
                                            RefernceNo = o.FcvihCInspRefNo,
                                            RMU = o.FcvihAiRmuName,
                                            RoadCode = o.FcvihAiRdCode,
                                            RoadName = o.FcvihAiRdName,
                                            StructureCode = o.FcvihAiStrucCode,
                                            ParkingPosition = o.FcvihPrkPosition.HasValue ? o.FcvihPrkPosition.Value ? "Yes" : "No" : "No",
                                            PotentialHazards = o.FcvihPotentialHazards.HasValue ? o.FcvihPotentialHazards.Value ? "Yes" : "No" : "No",
                                            Accessiblity = o.FcvihAccessibility.HasValue ? o.FcvihAccessibility.Value ? "Yes" : "No" : "No",
                                            AuditedByDate = o.FcvihDtAud,
                                            ReportforYear = o.FcvihYearOfInsp,
                                            AuditedByDesignation = o.FcvihUserDesignationAud,
                                            AuditedByName = o.FcvihUserNameAud,
                                            AssetRefNO = o.FcvihAiAssetId,
                                            BarrelNumber = o.FcvihAiBarrelNo,
                                            CatchmentArea = o.FcvihAiCatchArea,
                                            CulverConditionRate = o.FcvihCulvertConditionRat,
                                            CulverSkew = o.FcvihAiSkew,
                                            CulvertLength = o.FcvihAiLength,
                                            Culvertmaterial = o.FcvihAiMaterial,
                                            CulvertReference = o.FcvihCInspRefNo,
                                            CulvertType = o.FcvihAiGrpType,
                                            DesignFlow = o.FcvihAiDesignFlow,
                                            Division = o.FcvihAiDivCode,
                                            InspectedByDate = o.FcvihSerProviderInsDt,
                                            InspectedByDesignation = o.FcvihSerProviderUserDesignation,
                                            InspectedByName = o.FcvihSerProviderUserName,
                                            FinishedRoadLevel = o.FcvihAiFinRdLevel,
                                            GPSEasting = o.FcvihAiGpsEasting,
                                            GPSNorthing = o.FcvihAiGpsNorthing,
                                            HaveIssueFound = o.FcvihReqFurtherInv.HasValue ? o.FcvihReqFurtherInv.Value ? "Yes" : "No" : "No",
                                            Day = o.FcvihDtOfInsp.Value.Day,
                                            InletLevel = o.FcvihAiIntelLevel,
                                            InletStructure = o.FcvihAiIntelStruc,
                                            LocationChainageKm = o.FcvihAiLocChKm,
                                            LocationChainageM = o.FcvihAiLocChM,
                                            Month = o.FcvihDtOfInsp.Value.Month,
                                            Year = o.FcvihDtOfInsp.Value.Year,
                                            OutletLevel = o.FcvihAiOutletLevel,
                                            OutletStructure = o.FcvihAiOutletStruc,
                                            PartB2ServiceProvider = o.FcvihSerProviderDefObs,
                                            PartB2ServicePrvdrCons = o.FcvihAuthDefObs,
                                            PartCGeneralComments = o.FcvihSerProviderDefGenCom,
                                            PartCGeneralCommentsCons = o.FcvihAuthDefGenCom,
                                            PartDFeedback = o.FcvihSerProviderDefFeedback,
                                            PartDFeedbackCons = o.FcvihAuthDefFeedback,
                                            Precast = o.FcvihAiPrecastSitu,
                                            PkRefNo = o.FcvihPkRefNo
                                        }).ToList();

            string[] str = type.Select(s => s.DdlTypeDesc).ToArray();
            foreach (var rpt in detail)
            {
                rpt.CulvertDistress = distressSeverity("1A", rpt.PkRefNo).distress;
                rpt.CulvertSeverity = distressSeverity("1A", rpt.PkRefNo).severity;
                rpt.WaterwayDistress = distressSeverity("2A", rpt.PkRefNo).distress;
                rpt.WaterwaySeverity = distressSeverity("2A", rpt.PkRefNo).severity;
                rpt.EmbankmentDistress = distressSeverity("2B", rpt.PkRefNo).distress;
                rpt.EmbankmentSeverity = distressSeverity("2B", rpt.PkRefNo).severity;
                rpt.HeadwallInletDistress = distressSeverity("3A", rpt.PkRefNo).distress;
                rpt.HeadwallInletSeverity = distressSeverity("3A", rpt.PkRefNo).severity;
                rpt.WingwallInletDistress = distressSeverity("3B", rpt.PkRefNo).distress;
                rpt.WingwalInletSeverity = distressSeverity("3B", rpt.PkRefNo).severity;
                rpt.ApronInletDistress = distressSeverity("3C", rpt.PkRefNo).distress;
                rpt.ApronInletSeverity = distressSeverity("3C", rpt.PkRefNo).severity;
                rpt.RiprapInletDistress = distressSeverity("3D", rpt.PkRefNo).distress;
                rpt.RiprapInletSeverity = distressSeverity("3D", rpt.PkRefNo).severity;
                rpt.HeadwallOutletDistress = distressSeverity("3E", rpt.PkRefNo).distress;
                rpt.HeadwallOutletSeverity = distressSeverity("3E", rpt.PkRefNo).severity;
                rpt.WingwallOutletDistress = distressSeverity("3F", rpt.PkRefNo).distress;
                rpt.WingwallOutletSeverity = distressSeverity("3F", rpt.PkRefNo).severity;
                rpt.ApronOutletDistress = distressSeverity("3G", rpt.PkRefNo).distress;
                rpt.ApronOutletSeverity = distressSeverity("3G", rpt.PkRefNo).severity;
                rpt.RiprapOutletDistress = distressSeverity("3H", rpt.PkRefNo).distress;
                rpt.RiprapOutletSeverity = distressSeverity("3H", rpt.PkRefNo).severity;
                rpt.Barrel_1_Distress = distressSeverity("4A", rpt.PkRefNo).distress;
                rpt.Barrel_1_Severity = distressSeverity("4A", rpt.PkRefNo).severity;
                rpt.Barrel_2_Distress = distressSeverity("4B", rpt.PkRefNo).distress;
                rpt.Barrel_2_Severity = distressSeverity("4B", rpt.PkRefNo).severity;
                rpt.Barrel_3_Distress = distressSeverity("4C", rpt.PkRefNo).distress;
                rpt.Barrel_3_Severity = distressSeverity("4C", rpt.PkRefNo).severity;
                rpt.Barrel_4_Distress = distressSeverity("4D", rpt.PkRefNo).distress;
                rpt.Barrel_4_Severity = distressSeverity("4D", rpt.PkRefNo).severity;

                rpt.BarrelList = (from d in _context.RmFormCvInsDtl
                                  where d.FcvidFcvihPkRefNo == rpt.PkRefNo
                                  && d.FcvidInspCodeDesc.Contains("Barrel") && d.FcvidActiveYn == true
                                  && d.FcvidInspCode != "4A"
                                   && d.FcvidInspCode != "4B"
                                    && d.FcvidInspCode != "4C"
                                     && d.FcvidInspCode != "4D"
                                  orderby d.FcvidInspCodeDesc ascending
                                  select new BarrelDistressSeverity
                                  {
                                      Code = d.FcvidInspCode,
                                      Description = d.FcvidInspCodeDesc,
                                      Distress = d.FcvidDistress,
                                      Severity = d.FcvidSeverity
                                  }).ToList();


                var p = (from o in _context.RmFormCvInsImage
                         where o.FcviFcvihPkRefNo == rpt.PkRefNo && o.FcviActiveYn == true
                         && str.Contains(o.FcviImageTypeCode)
                         select new Pictures
                         {
                             ImageUrl = o.FcviImageUserFilePath,
                             Type = o.FcviImageTypeCode
                         }).ToList();
                rpt.Pictures = new List<Pictures>();
                int i = 1;
                foreach (var t in type)
                {
                    var picktures = p.Where(s => s.Type == t.DdlTypeDesc).ToList();
                    if (picktures == null || (picktures != null && picktures.Count == 0))
                    {
                        rpt.Pictures.Add(new Pictures { Type = t.DdlTypeValue != "P1" ? $"{t.DdlTypeValue}: {t.DdlTypeDesc}" : "" });
                        rpt.Pictures.Add(new Pictures { Type = $"{t.DdlTypeValue}: {t.DdlTypeDesc}" });
                    }
                    else if (picktures.Count < 2)
                    {
                        foreach (var pi in picktures)
                        {
                            pi.Type = $"{t.DdlTypeValue}: {t.DdlTypeDesc}";
                        }
                        rpt.Pictures.AddRange(picktures);
                        rpt.Pictures.Add(new Pictures { Type = t.DdlTypeValue != "P1" ? $"{t.DdlTypeValue}: {t.DdlTypeDesc}" : "" });
                    }
                    else
                    {
                        foreach (var pi in picktures)
                        {
                            pi.Type = $"{t.DdlTypeValue}: {t.DdlTypeDesc}";
                        }
                        rpt.Pictures.AddRange(picktures);
                    }
                }
            }
            return detail;
        }

        public async Task<IEnumerable<SelectListItem>> GetCVId(AssetDDLRequestDTO request)
        {
            var lst = _context.RmAllassetInventory.Where(s => s.AiAssetGrpCode == "CV" && (request.IncludeInActive ? true : s.AiActiveYn == true));
            if (!string.IsNullOrEmpty(request.RMU))
                lst = lst.Where(s => (s.AiRmuCode == request.RMU || s.AiRmuName == request.RMU));
            if (!string.IsNullOrEmpty(request.RdCode))
                lst = lst.Where(s => s.AiRdCode == request.RdCode);
            if (request.SectionCode > 0)
            {
                string code = request.SectionCode.ToString();
                lst = lst.Where(s => s.AiSecCode == code);
            }

            var resultlst = lst.ToArray().OrderBy(x => x.AiLocChKm).ThenBy(x => x.AiLocChM)
                .Select(s => new SelectListItem
                {
                    Value = s.AiPkRefNo.ToString(),
                    Text = s.AiAssetId
                });
            return resultlst;
        }
    }
}
