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
    public class FormR1R2Repository : RepositoryBase<RmFormR1Hdr>, IFormR1Repository
    {
        public FormR1R2Repository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<RmFormR1Hdr> FindDetails(RmFormR1Hdr frmR1R2)
        {
            return await _context.RmFormR1Hdr.Include(x => x.RmFormR2Hdr).ThenInclude(x => x.Fr2hFr1hPkRefNoNavigation).Where(x => x.Fr1hAssetId == frmR1R2.Fr1hAssetId && x.Fr1hYearOfInsp == frmR1R2.Fr1hYearOfInsp && x.Fr1hActiveYn == true).FirstOrDefaultAsync();
        }
        public async Task<RmFormR1Hdr> FindByHeaderID(int headerId)
        {

            return await _context.RmFormR1Hdr.Include(x => x.RmFormR2Hdr).ThenInclude(x => x.Fr2hFr1hPkRefNoNavigation).Where(x => x.Fr1hPkRefNo == headerId && x.Fr1hActiveYn == true).FirstOrDefaultAsync();
        }
        public async Task<RmFormR1Hdr> Save(RmFormR1Hdr frmR1R2, bool updateSubmit)
        {
            //bool isAdd = false;
            if (frmR1R2.Fr1hPkRefNo == 0)
            {
                //isAdd = true;
                frmR1R2.Fr1hActiveYn = true;
                IDictionary<string, string> lstRef = new Dictionary<string, string>();
                lstRef.Add("Year", Utility.ToString(frmR1R2.Fr1hYearOfInsp));
                lstRef.Add("AssetID", Utility.ToString(frmR1R2.Fr1hAssetId));
                frmR1R2.Fr1hRefNo = Common.RefNumber.FormRefNumber.GetRefNumber(FormType.FormR1R2, lstRef);
                _context.RmFormR1Hdr.Add(frmR1R2);
            }
            else
            {
                string[] arrNotReqUpdate = new string[] { "Fr1hPkRefNo", "Fr1hCInspRefNo", "Fr1hPkRefNo", "Fr1hAssetId",
                    "Fr1hDivCode", "Fr1hRmuName", "Fr1hRdCode","Fr1hRdName","Fr1hLocChKm","Fr1hLocChM"
                };
                //_context.RmFormS1Dtl.Update(formS1Details);
                //var dtls = frmR1R2.RmFormR2Hdr;
                //frmR1R2.RmFormR2Hdr = null;
                _context.RmFormR1Hdr.Attach(frmR1R2);

                var entry = _context.Entry(frmR1R2);
                entry.Properties.Where(x => !arrNotReqUpdate.Contains(x.Metadata.Name)).ToList().ForEach((p) =>
                {
                    p.IsModified = true;
                });
                if (updateSubmit)
                {
                    entry.Property(x => x.Fr1hSubmitSts).IsModified = true;
                }
                //string[] arrDtlReqUpdate = new string[] { "Fg1dDistress", "Fg1dSeverity", "Fg1dDistressOthers" };
                //foreach (var dtl in frmR1R2.RmFormR2Hdr)
                //{
                //    if (dtl.Fg2hPkRefNo > 0)
                //    {
                //        _context.RmFormR2Hdr.Attach(dtl);
                //        var dtlentry = _context.Entry(dtl);
                //        dtlentry.Properties.Where(x => arrDtlReqUpdate.Contains(x.Metadata.Name)).ToList().ForEach((p) =>
                //        {
                //            p.IsModified = true;
                //        });
                //    }
                //}
            }
            await _context.SaveChangesAsync();
            return frmR1R2;
        }

        public async Task<RmFormR2Hdr> SaveR2(RmFormR2Hdr frmR1R2, bool updateSubmit)
        {

            _context.Entry<RmFormR2Hdr>(frmR1R2).State = frmR1R2.Fr2hPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
            await _context.SaveChangesAsync();
            return frmR1R2;


        }

        public async Task<List<FormR1R2PhotoTypeDTO>> GetExitingPhotoType(int headerId)
        {
            return await _context.RmFormRImages.Where(x => x.FriFr1hPkRefNo == headerId).GroupBy(x => x.FriImageTypeCode).Select(x => new FormR1R2PhotoTypeDTO()
            {
                SNO = x.Max(y => y.FriImageSrno.Value),
                Type = x.Key
            }).ToListAsync();
        }
        public async Task<RmFormRImages> AddImage(RmFormRImages image)
        {
            _context.RmFormRImages.Add(image);
            await _context.SaveChangesAsync();
            return image;
        }
        public async Task<IList<RmFormRImages>> AddMultiImage(IList<RmFormRImages> images)
        {
            _context.RmFormRImages.AddRange(images);
            await _context.SaveChangesAsync();
            return images;
        }

        public async Task<int> ImageCount(string type, long headerId)
        {
            return await _context.RmFormRImages.Where(s => s.FriImageTypeCode == type && s.FriFr1hPkRefNo == headerId).CountAsync();

        }
        public async Task<List<RmFormRImages>> ImageList(int headerId)
        {
            return await _context.RmFormRImages.Where(x => x.FriFr1hPkRefNo == headerId && x.FriActiveYn == true).ToListAsync();
        }
        public async Task<int> DeleteImage(RmFormRImages img)
        {
            _context.RmFormRImages.Attach(img);
            var entry = _context.Entry(img);
            entry.Property(x => x.FriActiveYn).IsModified = true;
            await _context.SaveChangesAsync();
            return img.FriPkRefNo;
        }

        public async Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData)
        {
            var query = (from hdr in _context.RmFormR1Hdr.Where(s => s.Fr1hActiveYn == true)
                         from rmu in _context.RmDdLookup.Where(rd => rd.DdlType == "RMU" && (rd.DdlTypeDesc == hdr.Fr1hAiRmuName)).DefaultIfEmpty()
                         from asset in _context.RmAllassetInventory.Where(a => a.AiPkRefNo == hdr.Fr1hAidPkRefNo).DefaultIfEmpty()
                         let rdcode = _context.RmRoadMaster.Where(r => r.RdmRdCode == hdr.Fr1hAiRdCode && r.RdmActiveYn == true).DefaultIfEmpty().FirstOrDefault()
                         select new
                         {
                             RefNo = hdr.Fr1hPkRefNo,
                             RefID = hdr.Fr1hRefNo,
                             Year = hdr.Fr1hYearOfInsp,
                             InsDate = hdr.Fr1hDtOfInsp,
                             AssetRefId = hdr.Fr1hAssetId,
                             RMUCode = rmu.DdlTypeCode,
                             RMUDesc = hdr.Fr1hAiRmuName,
                             SecCode = asset.AiSecCode,
                             SecName = asset.AiSecName,
                             Bound = asset.AiBound,
                             AssetType = hdr.Fr1hAiStrucCode,
                             RoadCode = hdr.Fr1hAiRdCode,
                             RoadName = hdr.Fr1hAiRdName,
                             RoadId = rdcode.RdmRdCdSort,// asset.AiRdmPkRefNoNavigation.RdmRdCdSort,
                             LocationCH = Convert.ToDecimal((hdr.Fr1hAiLocChKm.HasValue ? hdr.Fr1hAiLocChKm.Value.ToString() : "") + "." + hdr.Fr1hAiLocChM),
                             InspectedBy = hdr.Fr1hInspectedName,
                             AuditedBy = hdr.Fr1hAuditedName,
                             Active = hdr.Fr1hActiveYn,
                             Status = (hdr.Fr1hSubmitSts ? "Submitted" : "Saved"),
                             ProcessStatus = hdr.Fr1hStatus
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

        //public async Task<List<RmInspItemMas>> GetInspItemMaster()
        //{
        //    return await _context.RmInspItemMas.Include(x => x.RmInspItemMasDtl).Where(x => x.IimActiveYn == true).ToListAsync();
        //}
        public int DeleteHeader(RmFormR1Hdr frmR1R2)
        {
            _context.RmFormR1Hdr.Attach(frmR1R2);
            var entry = _context.Entry(frmR1R2);
            entry.Property(x => x.Fr1hActiveYn).IsModified = true;
            _context.SaveChanges();
            return frmR1R2.Fr1hPkRefNo;
        }

        public List<FormR1R2Rpt> GetReportData(int headerid)
        {
            return GetReportDataV2(headerid);
        }

        public List<FormR1R2Rpt> GetReportDataV2(int headerid)
        {
            var type = (from ty in _context.RmDdLookup
                        where ty.DdlType == "Photo Type" && ty.DdlTypeCode == "RWG"
                        orderby ty.DdlTypeRemarks ascending
                        select ty).ToList();
            var roadcode = (from o in _context.RmFormR1Hdr
                            where o.Fr1hPkRefNo == headerid
                            select new { o.Fr1hAiRdCode, o.Fr1hDtOfInsp }).FirstOrDefault();

            List<FormR1R2Rpt> detail = (from o in _context.RmFormR1Hdr
                                        where (o.Fr1hAiRdCode == roadcode.Fr1hAiRdCode && o.Fr1hDtOfInsp.HasValue && o.Fr1hDtOfInsp < roadcode.Fr1hDtOfInsp) || o.Fr1hPkRefNo == headerid
                                        orderby o.Fr1hYearOfInsp ascending
                                        let formR2 = _context.RmFormR2Hdr.FirstOrDefault(x => x.Fr2hFr1hPkRefNo == o.Fr1hPkRefNo)                                        
                                        select new FormR1R2Rpt
                                        {
                                            RefernceNo = o.Fr1hRefNo,
                                            RMU = o.Fr1hAiRmuName,
                                            RoadCode = o.Fr1hAiRdCode,
                                            RoadName = o.Fr1hAiRdName,
                                            StructureCode = o.Fr1hAiStrucCode,
                                            //ParkingPosition = o.Fg1hPrkPosition.HasValue ? o.Fg1hPrkPosition.Value ? "Yes" : "No" : "No",
                                            //PotentialHazards = o.Fg1hPotentialHazards.HasValue ? o.Fg1hPotentialHazards.Value ? "Yes" : "No" : "No",
                                            //Accessiblity = o.Fg1hAccessibility.HasValue ? o.Fg1hAccessibility.Value ? "Yes" : "No" : "No",
                                            AuditedByDate = o.Fr1hAuditedDt,
                                            ReportforYear = o.Fr1hYearOfInsp,
                                            AuditedByDesignation = o.Fr1hAuditedDesig,
                                            AuditedByName = o.Fr1hAuditedName,
                                            AssetRefNO = o.Fr1hAssetId,
                                            WallFunction = o.Fr1hWallFunction,
                                            WallMember = o.Fr1hWallMember,
                                            FacingType = o.Fr1hFacingType,
                                            Year = o.Fr1hDtOfInsp.Value.Year,
                                            Month = o.Fr1hDtOfInsp.Value.Month,
                                            Day = o.Fr1hDtOfInsp.Value.Day,
                                            DistressObserved = o.Fr1hDistressObserved1,
                                            Severity = o.Fr1hSeverity,                                           
                                            Division = o.Fr1hAiDivCode,
                                            InspectedByDate = o.Fr1hInspectedDt,
                                            InspectedByDesignation = o.Fr1hInspectedDesig,
                                            InspectedByName = o.Fr1hInspectedName,
                                            GPSEasting = o.Fr1hAiGpsEasting,
                                            GPSNorthing = o.Fr1hAiGpsNorthing,
                                            RatingWallConditionRate = o.Fr1hCondRating,
                                            HaveIssueFound = o.Fr1hIssuesFound.HasValue ? o.Fr1hIssuesFound.Value ? "Yes" : "No" : "No",                                            
                                            RatingRecordNo = o.Fr1hRecordNo,
                                            LocationChainageKm = o.Fr1hAiLocChKm,
                                            LocationChainageM = o.Fr1hAiLocChM,

                                            PartB2ServiceProvider = formR2.Fr2hDistressSp,
                                            PartB2ServicePrvdrCons = formR2.Fr2hDistressEc,
                                            PartCGeneralComments = formR2.Fr2hGeneralSp,
                                            PartCGeneralCommentsCons = formR2.Fr2hGeneralSp,
                                            PartDFeedback = formR2.Fr2hFeedbackSp,
                                            PartDFeedbackCons = formR2.Fr2hFeedbackEc,
                                            PkRefNo = o.Fr1hPkRefNo
                                        }).ToList();

            string[] str = type.Select(s => s.DdlTypeDesc).ToArray();
            foreach (var rpt in detail)
            {
                var p = (from o in _context.RmFormRImages
                         where o.FriFr1hPkRefNo == rpt.PkRefNo && o.FriActiveYn == true
                         && str.Contains(o.FriImageTypeCode)
                         select new Pictures
                         {
                             ImageUrl = o.FriImageUserFilePath,
                             Type = o.FriImageTypeCode,
                             FileName = o.FriImageFilenameUpload
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
    }
}
