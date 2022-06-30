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
    public class FormG1Repository : RepositoryBase<RmFormG1Hdr>, IFormG1Repository
    {
        public FormG1Repository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<RmFormG1Hdr> FindDetails(RmFormG1Hdr frmG1G2)
        {
            return await _context.RmFormG1Hdr.Include(x => x.RmFormG2Hdr).ThenInclude(x => x.Fg2hFg1hPkRefNoNavigation).Where(x => x.Fg1hAssetId == frmG1G2.Fg1hAssetId && x.Fg1hYearOfInsp == frmG1G2.Fg1hYearOfInsp && x.Fg1hActiveYn == true).FirstOrDefaultAsync();
        }
        public async Task<RmFormG1Hdr> FindByHeaderID(int headerId)
        {

            return await _context.RmFormG1Hdr.Include(x => x.RmFormG2Hdr).ThenInclude(x => x.Fg2hFg1hPkRefNoNavigation).Where(x => x.Fg1hPkRefNo == headerId && x.Fg1hActiveYn == true).FirstOrDefaultAsync();
        }
        public async Task<RmFormG1Hdr> Save(RmFormG1Hdr frmG1G2, bool updateSubmit)
        {
            //bool isAdd = false;
            if (frmG1G2.Fg1hPkRefNo == 0)
            {
                //isAdd = true;
                frmG1G2.Fg1hActiveYn = true;
                IDictionary<string, string> lstRef = new Dictionary<string, string>();
                lstRef.Add("Year", Utility.ToString(frmG1G2.Fg1hYearOfInsp));
                lstRef.Add("AssetID", Utility.ToString(frmG1G2.Fg1hAssetId));
                frmG1G2.Fg1hRefNo = Common.RefNumber.FormRefNumber.GetRefNumber(FormType.FormG1G2, lstRef);
                _context.RmFormG1Hdr.Add(frmG1G2);
            }
            else
            {
                string[] arrNotReqUpdate = new string[] { "Fg1hPkRefNo", "Fg1hCInspRefNo", "Fg1hPkRefNo", "Fg1hAssetId",
                    "Fg1hDivCode", "Fg1hRmuName", "Fg1hRdCode","Fg1hRdName","Fg1hLocChKm","Fg1hLocChM"
                };
                //_context.RmFormS1Dtl.Update(formS1Details);
                //var dtls = frmG1G2.RmFormG2Hdr;
                //frmG1G2.RmFormG2Hdr = null;
                _context.RmFormG1Hdr.Attach(frmG1G2);
               
                var entry = _context.Entry(frmG1G2);
                entry.Properties.Where(x => !arrNotReqUpdate.Contains(x.Metadata.Name)).ToList().ForEach((p) =>
                {
                    p.IsModified = true;
                });
                if (updateSubmit)
                {
                    entry.Property(x => x.Fg1hSubmitSts).IsModified = true;
                }
                //string[] arrDtlReqUpdate = new string[] { "Fg1dDistress", "Fg1dSeverity", "Fg1dDistressOthers" };
                //foreach (var dtl in frmG1G2.RmFormG2Hdr)
                //{
                //    if (dtl.Fg2hPkRefNo > 0)
                //    {
                //        _context.RmFormG2Hdr.Attach(dtl);
                //        var dtlentry = _context.Entry(dtl);
                //        dtlentry.Properties.Where(x => arrDtlReqUpdate.Contains(x.Metadata.Name)).ToList().ForEach((p) =>
                //        {
                //            p.IsModified = true;
                //        });
                //    }
                //}
            }
            await _context.SaveChangesAsync();
            return frmG1G2;
        }

        public  async Task<RmFormG2Hdr> SaveG2(RmFormG2Hdr frmG1G2, bool updateSubmit)
        {

            _context.Entry<RmFormG2Hdr>(frmG1G2).State = frmG1G2.Fg2hPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
            await _context.SaveChangesAsync();
            return frmG1G2;


        }

        public async Task<List<FormG1G2PhotoTypeDTO>> GetExitingPhotoType(int headerId)
        {
            return await _context.RmFormGImages.Where(x => x.FgiFg1hPkRefNo == headerId && x.FgiActiveYn == true).GroupBy(x => x.FgiImageTypeCode).Select(x => new FormG1G2PhotoTypeDTO()
            {
                SNO = x.Max(y => y.FgiImageSrno.Value),
                Type = x.Key
            }).ToListAsync();
        }
        public async Task<RmFormGImages> AddImage(RmFormGImages image)
        {
            _context.RmFormGImages.Add(image);
            await _context.SaveChangesAsync();
            return image;
        }
        public async Task<IList<RmFormGImages>> AddMultiImage(IList<RmFormGImages> images)
        {
            _context.RmFormGImages.AddRange(images);
            await _context.SaveChangesAsync();
            return images;
        }

        public async Task<int> ImageCount(string type, long headerId)
        {
            return await _context.RmFormGImages.Where(s => s.FgiImageTypeCode == type && s.FgiFg1hPkRefNo == headerId).CountAsync();

        }
        public async Task<List<RmFormGImages>> ImageList(int headerId)
        {
            return await _context.RmFormGImages.Where(x => x.FgiFg1hPkRefNo == headerId && x.FgiActiveYn == true).ToListAsync();
        }
        public async Task<int> DeleteImage(RmFormGImages img)
        {
            _context.RmFormGImages.Attach(img);
            var entry = _context.Entry(img);
            entry.Property(x => x.FgiActiveYn).IsModified = true;
            await _context.SaveChangesAsync();
            return img.FgiPkRefNo;
        }

        public async Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData)
        {
            var query = (from hdr in _context.RmFormG1Hdr.Where(s => s.Fg1hActiveYn == true)
                         from rmu in _context.RmDdLookup.Where(rd => rd.DdlType == "RMU" && (rd.DdlTypeDesc == hdr.Fg1hRmuName)).DefaultIfEmpty()
                         from asset in _context.RmAllassetInventory.Where(a => a.AiPkRefNo == hdr.Fg1hAiPkRefNo).DefaultIfEmpty()
                         let rdcode = _context.RmRoadMaster.Where(r => r.RdmRdCode == hdr.Fg1hRdCode && r.RdmActiveYn == true).DefaultIfEmpty().FirstOrDefault()
                         select new
                         {
                             RefNo = hdr.Fg1hPkRefNo,
                             RefID = hdr.Fg1hRefNo,
                             Year = hdr.Fg1hYearOfInsp,
                             InsDate = hdr.Fg1hDtOfInsp,
                             AssetRefId = hdr.Fg1hAssetId,
                             RMUCode = rmu.DdlTypeCode,
                             RMUDesc = hdr.Fg1hRmuName,
                             SecCode = asset.AiSecCode,
                             SecName = asset.AiSecName,
                             Bound = asset.AiBound,
                             AssetType = hdr.Fg1hStrucCode,
                             RoadCode = hdr.Fg1hRdCode,
                             RoadName = hdr.Fg1hRdName,
                             RoadId = rdcode.RdmRdCdSort,// asset.AiRdmPkRefNoNavigation.RdmRdCdSort,
                             LocationCH = Convert.ToDecimal((hdr.Fg1hLocChKm.HasValue ? hdr.Fg1hLocChKm.Value.ToString() : "") + "." + hdr.Fg1hLocChM),
                             InspectedBy = hdr.Fg1hInspectedName,
                             AuditedBy = hdr.Fg1hAuditedName,
                             Active = hdr.Fg1hActiveYn,
                             Status = (hdr.Fg1hSubmitSts ? "Submitted" : "Saved"),
                             ProcessStatus = hdr.Fg1hStatus
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
                                 || (x.Year.HasValue ? x.Year.Value.ToString() : "").Contains(strVal)
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
                            string strM = Utility.ToString(searchData.filter["chFromKM"]);
                            decimal flKm = Utility.ToDecimal(strVal + (strM != "" ? "." + strM : ""));
                            query = query.Where(x => x.LocationCH >= flKm);
                            break;
                        case "chFromM":
                            string strKm = Utility.ToString(searchData.filter["chFromM"]);
                            if (strKm == "")
                            {
                                decimal flM = Utility.ToDecimal("0." + strVal);
                                query = query.Where(x => x.LocationCH >= flM);
                            }
                            break;
                        case "chToKM":
                            string strTM = Utility.ToString(searchData.filter["chToKM"]);
                            decimal flTKm = Utility.ToDecimal(strVal + (strTM != "" ? "." + strTM : ""));
                            query = query.Where(x => x.LocationCH <= flTKm);
                            break;
                        case "chToM":
                            string strTKm = Utility.ToString(searchData.filter["chToM"]);
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

        //public async Task<List<RmInspItemMas>> GetInspItemMaster()
        //{
        //    return await _context.RmInspItemMas.Include(x => x.RmInspItemMasDtl).Where(x => x.IimActiveYn == true).ToListAsync();
        //}
        public int DeleteHeader(RmFormG1Hdr frmG1G2)
        {
            _context.RmFormG1Hdr.Attach(frmG1G2);
            var entry = _context.Entry(frmG1G2);
            entry.Property(x => x.Fg1hActiveYn).IsModified = true;
            _context.SaveChanges();
            return frmG1G2.Fg1hPkRefNo;
        }

        public bool isF3Exist(int id)
        {
           var rmF2dtl =  _context.RmFormF3Dtl.FirstOrDefault(x => x.Ff3dG1hPkRefNo == id);
            if (rmF2dtl != null)
                return true;

            return false;
        }

        public List<FormG1G2Rpt> GetReportData(int headerid)
        {
            return GetReportDataV2(headerid);
        }


        public List<FormG1G2Rpt> GetReportDataV2(int headerid)
        {
            var type = (from ty in _context.RmDdLookup
                        where ty.DdlType == "Photo Type" && ty.DdlTypeCode == "SG"
                        orderby ty.DdlTypeRemarks ascending
                        select ty).ToList();
            var roadcode = (from o in _context.RmFormG1Hdr
                            where o.Fg1hPkRefNo == headerid && o.Fg1hActiveYn == true
                            select new { o.Fg1hRdCode, o.Fg1hDtOfInsp }).FirstOrDefault();

            var AssetId = (from ast in _context.RmFormG1Hdr
                           where ast.Fg1hPkRefNo == headerid
                           select ast.Fg1hAiPkRefNo).First();

            List<FormG1G2Rpt> detail = (from o in _context.RmFormG1Hdr
                                        where (o.Fg1hRdCode == roadcode.Fg1hRdCode && o.Fg1hDtOfInsp.HasValue && o.Fg1hDtOfInsp < roadcode.Fg1hDtOfInsp && o.Fg1hAiPkRefNo == AssetId && o.Fg1hActiveYn == true) || o.Fg1hPkRefNo == headerid
                                        orderby o.Fg1hYearOfInsp ascending
                                        let formG2 = _context.RmFormG2Hdr.FirstOrDefault(x => x.Fg2hFg1hPkRefNo == o.Fg1hPkRefNo)
                                        select new FormG1G2Rpt
                                        {
                                            RefernceNo = o.Fg1hRefNo,
                                            RMU = o.Fg1hRmuName,
                                            RoadCode = o.Fg1hRdCode,
                                            RoadName = o.Fg1hRdName,
                                            StructureCode = o.Fg1hStrucCode,
                                            ParkingPosition = o.Fg1hPrkPosition.HasValue ? o.Fg1hPrkPosition.Value ? "Yes" : "No" : "No",
                                            PotentialHazards = o.Fg1hPotentialHazards.HasValue ? o.Fg1hPotentialHazards.Value ? "Yes" : "No" : "No",
                                            Accessiblity = o.Fg1hAccessibility.HasValue ? o.Fg1hAccessibility.Value ? "Yes" : "No" : "No",
                                            AuditedByDate = o.Fg1hAuditedDt,
                                            ReportforYear = o.Fg1hYearOfInsp,
                                            AuditedByDesignation = o.Fg1hAuditedDesig,
                                            AuditedByName = o.Fg1hAuditedName,
                                            AssetRefNO = o.Fg1hAssetId,
                                            BarriersYes = o.Fg1hInspBarrier == "Yes" ? 1 : 0 ,
                                            BarriersNo = o.Fg1hInspBarrier == "No" ? 1 : 0,
                                            BarriersCritical = o.Fg1hInspBarrier == "Critical" ? 1 : 0,
                                            BarriersClosed = o.Fg1hInspBarrier == "Closed" ? 1 : 0,
                                            GantryBeamsYes = o.Fg1hInspGBeam == "Yes" ? 1 : 0,
                                            GantryBeamsNo = o.Fg1hInspGBeam == "No" ? 1 : 0,
                                            GantryBeamsCritical = o.Fg1hInspGBeam == "Critical" ? 1 : 0,
                                            GantryBeamsClosed = o.Fg1hInspGBeam == "Closed" ? 1 : 0,
                                            GantryColsYes = o.Fg1hInspGColumn == "Yes" ? 1 : 0,
                                            GantryColsNo = o.Fg1hInspGColumn == "No" ? 1 : 0,
                                            GantryColsCritical = o.Fg1hInspGColumn == "Critical" ? 1 : 0,
                                            GantryColsClosed = o.Fg1hInspGColumn == "Closed" ? 1 : 0,
                                            FootingYes = o.Fg1hInspFootings == "Yes" ? 1 : 0,
                                            FootingNo = o.Fg1hInspFootings == "No" ? 1 : 0,
                                            FootingCritical = o.Fg1hInspFootings == "Critical" ? 1 : 0,
                                            FootingClosed = o.Fg1hInspFootings == "Closed" ? 1 : 0,
                                            AnchorYes = o.Fg1hInspGPads == "Yes" ? 1 : 0,
                                            AnchorNo = o.Fg1hInspGPads == "No" ? 1 : 0,
                                            AnchorCritical = o.Fg1hInspGPads == "Critical" ? 1 : 0,
                                            AnchorClosed = o.Fg1hInspGPads == "Closed" ? 1 : 0,
                                            MaintenanceAccessYes = o.Fg1hInspMaintenance == "Yes" ? 1 : 0,
                                            MaintenanceAccessNo = o.Fg1hInspMaintenance == "No" ? 1 : 0,
                                            MaintenanceAccessCritical = o.Fg1hInspMaintenance == "Critical" ? 1 : 0,
                                            MaintenanceAccessClosed = o.Fg1hInspMaintenance == "Closed" ? 1 : 0,
                                            StaticSignsYes = o.Fg1hInspStaticSigns == "Yes" ? 1 : 0,
                                            StaticSignsNo = o.Fg1hInspStaticSigns == "No" ? 1 : 0,
                                            StaticSignsCritical = o.Fg1hInspStaticSigns == "Critical" ? 1 : 0,
                                            StaticSignsClosed = o.Fg1hInspStaticSigns == "Closed" ? 1 : 0,
                                            VariableMessagYes = o.Fg1hInspVms == "Yes" ? 1 : 0,
                                            VariableMessagNo = o.Fg1hInspVms == "No" ? 1 : 0,
                                            VariableMessagCritical = o.Fg1hInspVms == "Critical" ? 1 : 0,
                                            VariableMessagClosed = o.Fg1hInspVms == "Closed" ? 1 : 0,
                                            Division = o.Fg1hDivCode,
                                            InspectedByDate = o.Fg1hInspectedDt,
                                            InspectedByDesignation = o.Fg1hInspectedDesig,
                                            InspectedByName = o.Fg1hInspectedName,
                                            GPSEasting = o.Fg1hGpsEasting,
                                            GPSNorthing = o.Fg1hGpsNorthing,
                                            GantrySignConditionRate = o.Fg1hCondRating,
                                            HaveIssueFound = o.Fg1hIssuesFound.HasValue ? o.Fg1hIssuesFound.Value ? "Yes" : "No" : "No",
                                            Day = o.Fg1hDtOfInsp.Value.Day,
                                            RatingRecordNo = o.Fg1hRecordNo ,
                                            LocationChainageKm = o.Fg1hLocChKm,
                                            LocationChainageM = o.Fg1hLocChM    ,
                                            Month = o.Fg1hDtOfInsp.Value.Month,
                                            Year = o.Fg1hDtOfInsp.Value.Year,
                                            
                                            PartB2ServiceProvider = formG2.Fg2hDistressSp ,
                                            PartB2ServicePrvdrCons = formG2.Fg2hDistressEc,
                                            PartCGeneralComments = formG2.Fg2hGeneralSp,
                                            PartCGeneralCommentsCons = formG2.Fg2hGeneralSp ,
                                            PartDFeedback = formG2.Fg2hFeedbackSp, 
                                            PartDFeedbackCons = formG2.Fg2hFeedbackEc,
                                            PkRefNo = o.Fg1hPkRefNo
                                        }).ToList();

            string[] str = type.Select(s => s.DdlTypeDesc).ToArray();
            foreach (var rpt in detail)
            { 
                var p = (from o in _context.RmFormGImages
                         where o.FgiFg1hPkRefNo == rpt.PkRefNo && o.FgiActiveYn == true
                         && str.Contains(o.FgiImageTypeCode)
                         select new Pictures
                         {
                             ImageUrl = o.FgiImageUserFilePath,
                             Type = o.FgiImageTypeCode,
                             FileName = o.FgiImageFilenameUpload 
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

        //public async Task<IEnumerable<SelectListItem>> GetCVId(AssetDDLRequestDTO request)
        //{
        //    var lst = _context.RmAllassetInventory.Where(s => s.AiAssetGrpCode == "CV" && (request.IncludeInActive ? true : s.AiActiveYn == true));
        //    if (!string.IsNullOrEmpty(request.RMU))
        //        lst = lst.Where(s => (s.AiRmuCode == request.RMU || s.AiRmuName == request.RMU));
        //    if (!string.IsNullOrEmpty(request.RdCode))
        //        lst = lst.Where(s => s.AiRdCode == request.RdCode);
        //    if (request.SectionCode > 0)
        //    {
        //        string code = request.SectionCode.ToString();
        //        lst = lst.Where(s => s.AiSecCode == code);
        //    }

        //    var resultlst = lst.ToArray().OrderBy(x => x.AiLocChKm).ThenBy(x => x.AiLocChM)
        //        .Select(s => new SelectListItem
        //        {
        //            Value = s.AiPkRefNo.ToString(),
        //            Text = s.AiAssetId
        //        });
        //    return resultlst;
        //}

    }
}
