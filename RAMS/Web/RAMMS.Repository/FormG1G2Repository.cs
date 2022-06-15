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
    public class FormG1Repository : RepositoryBase<RmFormG1Hdr> , IFormG1Repository
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
                frmG1G2.Fg1hRefNo  = Common.RefNumber.FormRefNumber.GetRefNumber(FormType.FormG1G2, lstRef);
                _context.RmFormG1Hdr.Add(frmG1G2);
            }
            else
            {
                string[] arrNotReqUpdate = new string[] { "Fg1hPkRefNo", "Fg1hCInspRefNo", "Fg1hPkRefNo", "Fg1hAssetId",
                    "Fg1hDivCode", "Fg1hRmuName", "Fg1hRdCode","Fg1hRdName","Fg1hLocChKm","Fg1hLocChM","Fg1hFinRdLevel","Fg1hStrucCode","Fg1hAiCatchArea","Fg1hAiSkew",
                    "Fg1hAiDesignFlow","Fg1hAiLength","Fg1hAiPrecastSitu","Fg1hAiGrpType","Fg1hAiBarrelNo","Fg1hAiGpsEasting","Fg1hAiGpsNorthing","Fg1hAiMaterial","Fg1hAiIntelLevel","Fg1hAiOutletLevel",
                    "Fg1hAiIntelStruc","Fg1hAiOutletStruc","Fg1hYearOfInsp","Fg1hCrBy","Fg1hCrDt"
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
                string[] arrDtlReqUpdate = new string[] { "Fg1dDistress", "Fg1dSeverity", "Fg1dDistressOthers" };
                foreach (var dtl in frmG1G2.RmFormG2Hdr)
                {
                    if (dtl.Fg2hPkRefNo > 0)
                    {
                        _context.RmFormG2Hdr.Attach(dtl);
                        var dtlentry = _context.Entry(dtl);
                        dtlentry.Properties.Where(x => arrDtlReqUpdate.Contains(x.Metadata.Name)).ToList().ForEach((p) =>
                        {
                            p.IsModified = true;
                        });
                    }
                }
            }
            await _context.SaveChangesAsync();
            return frmG1G2;
        }
        public async Task<List<FormG1G2PhotoTypeDTO>> GetExitingPhotoType(int headerId)
        {
            return await _context.RmFormGImages.Where(x => x.FgiFg1hPkRefNo == headerId).GroupBy(x => x.FgiImageTypeCode).Select(x => new FormG1G2PhotoTypeDTO()
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
                         from asset in _context.RmAllassetInventory.Where(a => a.AiPkRefNo == hdr.Fg1hPkRefNo).DefaultIfEmpty()
                         let rdcode = _context.RmRoadMaster.Where(r => r.RdmRdCode == hdr.Fg1hRdCode && r.RdmActiveYn == true).DefaultIfEmpty().FirstOrDefault()
                         select new
                         {
                             RefNo = hdr.Fg1hPkRefNo,
                             RefID = hdr.Fg1hRefNo,
                             Year = hdr.Fg1hYearOfInsp,
                             InsDate = hdr.Fg1hDtOfInsp,
                             AssetRefId = hdr.Fg1hAssetId,
                             RMUCode = hdr.Fg1hRmuCode,
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
        public int DeleteHeader(RmFormG1Hdr frmG1G2)
        {
            _context.RmFormG1Hdr.Attach(frmG1G2);
            var entry = _context.Entry(frmG1G2);
            entry.Property(x => x.Fg1hActiveYn).IsModified = true;
            _context.SaveChanges();
            return frmG1G2.Fg1hPkRefNo;
        }

        //public List<FormC1C2Rpt> GetReportData(int headerid)
        //{
        //    return GetReportDataV2(headerid);
        //}


        //public List<FormC1C2Rpt> GetReportDataV2(int headerid)
        //{
        //    return null;
        //}

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
