using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RAMMS.Common.Extensions;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.Report;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.SearchBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository
{
    public class FormHRepository : RepositoryBase<RmFormHHdr>, IFormHRepository
    {
        public FormHRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }

        public void SaveImageList(IEnumerable<RmFormhImageDtl> imageDtl)
        {
            _context.RmFormhImageDtl.AddRange(imageDtl);
        }

        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormHSearchDTO> filterOptions)
        {
            var query = (from x in _context.RmFormHHdr
                         let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeCode == x.FhhRmu || s.DdlTypeDesc == x.FhhRmu))
                         let sec = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "Section Code" && (s.DdlTypeDesc == x.FhhSection || s.DdlTypeCode == x.FhhSection))
                         select new { rmu, sec, x });



            query = query.Where(x => x.x.FhhActiveYn == true).OrderByDescending(x => x.x.FhhModDt);

            if (!string.IsNullOrEmpty(filterOptions.Filters.RoadCode))
            {
                query = query.Where(x => x.x.FhhRoadCode == filterOptions.Filters.RoadCode);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.Section))
            {
                query = query.Where(x => x.sec.DdlTypeDesc == filterOptions.Filters.Section || x.sec.DdlTypeCode == filterOptions.Filters.Section);

            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
            {
                query = query.Where(x => x.rmu.DdlTypeCode == filterOptions.Filters.RMU || x.rmu.DdlTypeDesc == filterOptions.Filters.RMU);

            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.AssetGroupCode))
            {
                query = query.Where(x => x.x.FhhAssetGroupCode == filterOptions.Filters.AssetGroupCode);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.RoadCode))
            {
                query = query.Where(x => x.x.FhhRoadCode == filterOptions.Filters.RoadCode);
            }

            if (filterOptions.Filters.FromChKM.HasValue)
            {
                query = query.Where(x => x.x.FhhFrmCh >= filterOptions.Filters.FromChKM);
            }

            if (filterOptions.Filters.FromChM.HasValue)
            {
                query = query.Where(x => x.x.FhhFrmChDeci >= filterOptions.Filters.FromChM);
            }

            if (filterOptions.Filters.ToChKM.HasValue)
            {
                query = query.Where(x => x.x.FhhToCh <= filterOptions.Filters.ToChKM);
            }

            if (filterOptions.Filters.ToChM.HasValue)
            {
                query = query.Where(x => x.x.FhhToChDeci <= filterOptions.Filters.ToChM);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.InspectionDate))
            {
                DateTime dt;
                if (DateTime.TryParseExact(filterOptions.Filters.InspectionDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                {
                    query = query.Where(x => x.x.FhhInspDt.HasValue ? (x.x.FhhInspDt.Value.Year == dt.Year && x.x.FhhInspDt.Value.Month == dt.Month && x.x.FhhInspDt.Value.Day == dt.Day) : false);
                }
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
            {

                DateTime dt;
                if (DateTime.TryParseExact(filterOptions.Filters.SmartInputValue, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                {
                    query = query.Where(x =>
                                   (x.sec.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                    || (x.x.FhhRefId ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhRmu ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhAssetGroupCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhRoadCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhUsernamePrp ?? "").Contains(filterOptions.Filters.SmartInputValue)

                                    || (x.x.FhhUsernameRcvdAuth ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhUsernameVetAuth ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhInspDt.HasValue ? (x.x.FhhInspDt.Value.Year == dt.Year && x.x.FhhInspDt.Value.Month == dt.Month && x.x.FhhInspDt.Value.Day == dt.Day) : true)
                                    || (x.x.FhhSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue));
                }
                else
                {
                    query = query.Where(x =>
                                    (x.sec.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                    || (x.x.FhhRefId ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhRmu ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhAssetGroupCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhRoadCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhUsernamePrp ?? "").Contains(filterOptions.Filters.SmartInputValue)

                                    || (x.x.FhhUsernameRcvdAuth ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhUsernameVetAuth ?? "").Contains(filterOptions.Filters.SmartInputValue)

                                    || (x.x.FhhSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue));
                }
            }



            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<RmFormHHdr>> GetFilteredRecordList(FilteredPagingDefinition<FormHSearchDTO> filterOptions)
        {
            List<RmFormHHdr> result = new List<RmFormHHdr>();
            var query = (from x in _context.RmFormHHdr
                         let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeCode == x.FhhRmu || s.DdlTypeDesc == x.FhhRmu))
                         let sec = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "Section Code" && (s.DdlTypeDesc == x.FhhSection || s.DdlTypeCode == x.FhhSection))
                         select new { rmu, sec, x });

            query = query.Where(x => x.x.FhhActiveYn == true).OrderByDescending(x => x.x.FhhModDt);

            if (!string.IsNullOrEmpty(filterOptions.Filters.RoadCode))
            {
                query = query.Where(x => x.x.FhhRoadCode == filterOptions.Filters.RoadCode);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.Section))
            {
                query = query.Where(x => x.sec.DdlTypeDesc == filterOptions.Filters.Section || x.sec.DdlTypeCode == filterOptions.Filters.Section);

            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
            {
                query = query.Where(x => x.rmu.DdlTypeCode == filterOptions.Filters.RMU || x.rmu.DdlTypeDesc == filterOptions.Filters.RMU);

            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.AssetGroupCode))
            {
                query = query.Where(x => x.x.FhhAssetGroupCode == filterOptions.Filters.AssetGroupCode);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.RoadCode))
            {
                query = query.Where(x => x.x.FhhRoadCode == filterOptions.Filters.RoadCode);
            }

            if (filterOptions.Filters.FromChKM.HasValue)
            {
                query = query.Where(x => x.x.FhhFrmCh >= filterOptions.Filters.FromChKM);
            }

            if (filterOptions.Filters.FromChM.HasValue)
            {
                query = query.Where(x => x.x.FhhFrmChDeci >= filterOptions.Filters.FromChM);
            }

            if (filterOptions.Filters.ToChKM.HasValue)
            {
                query = query.Where(x => x.x.FhhToCh <= filterOptions.Filters.ToChKM);
            }

            if (filterOptions.Filters.ToChM.HasValue)
            {
                query = query.Where(x => x.x.FhhToChDeci <= filterOptions.Filters.ToChM);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.InspectionDate))
            {
                DateTime dt;
                if (DateTime.TryParseExact(filterOptions.Filters.InspectionDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                {
                    query = query.Where(x => x.x.FhhInspDt.HasValue ? (x.x.FhhInspDt.Value.Year == dt.Year && x.x.FhhInspDt.Value.Month == dt.Month && x.x.FhhInspDt.Value.Day == dt.Day) : false);
                }
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
            {

                DateTime dt;
                if (DateTime.TryParseExact(filterOptions.Filters.SmartInputValue, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                {
                    query = query.Where(x =>
                                    (x.sec.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                    ||(x.x.FhhRefId ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhRmu ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhAssetGroupCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhRoadCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhUsernamePrp ?? "").Contains(filterOptions.Filters.SmartInputValue)

                                    || (x.x.FhhUsernameRcvdAuth ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhUsernameVetAuth ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhInspDt.HasValue ? (x.x.FhhInspDt.Value.Year == dt.Year && x.x.FhhInspDt.Value.Month == dt.Month && x.x.FhhInspDt.Value.Day == dt.Day) : true)
                                    || (x.x.FhhSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue));
                }
                else
                {
                    query = query.Where(x =>
                                   (x.sec.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                   ||
                                    (x.x.FhhRefId ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhRmu ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhAssetGroupCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhRoadCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhUsernamePrp ?? "").Contains(filterOptions.Filters.SmartInputValue)

                                    || (x.x.FhhUsernameRcvdAuth ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.FhhUsernameVetAuth ?? "").Contains(filterOptions.Filters.SmartInputValue)

                                    || (x.x.FhhSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue));
                }
            }

            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(s => s.x.FhhRefId);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(s => s.x.FhhRmu);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(s => s.x.FhhRmu);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(s => s.sec.DdlTypeCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(s => s.x.FhhSection);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(s => s.x.FhhRoadCode);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderBy(s => s.x.FhhInspDt);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderBy(s => s.x.FhhAssetGroupCode);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderBy(s => s.x.FhhSubmitSts);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderBy(s => s.x.FhhUsernameVer);
                if (filterOptions.ColumnIndex == 12)
                    query = query.OrderBy(s => s.x.FhhUsernameRcvdAuth);
                if (filterOptions.ColumnIndex == 13)
                    query = query.OrderBy(s => s.x.FhhUsernameVetAuth);
                
            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(s => s.x.FhhRefId);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(s => s.x.FhhRmu);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(s => s.x.FhhRmu);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(s => s.sec.DdlTypeCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(s => s.x.FhhSection);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(s => s.x.FhhRoadCode);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderByDescending(s => s.x.FhhInspDt);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderByDescending(s => s.x.FhhAssetGroupCode);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderByDescending(s => s.x.FhhSubmitSts);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderByDescending(s => s.x.FhhUsernameVer);
                if (filterOptions.ColumnIndex == 12)
                    query = query.OrderByDescending(s => s.x.FhhUsernameRcvdAuth);
                if (filterOptions.ColumnIndex == 13)
                    query = query.OrderByDescending(s => s.x.FhhUsernameVetAuth);
                
            }
            result = await query.Select(s => s.x).Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync().ConfigureAwait(false);
            return result;
        }


        public async Task<RmFormhImageDtl> GetFormHImageByIdAsync(int imageId)
        {
            return await _context.RmFormhImageDtl.Where(x => x.FhiPkRefNo == imageId && x.FhiActiveYn == true).FirstOrDefaultAsync();
        }

        public void UpdateImage(RmFormhImageDtl imageDtl)
        {
            _context.Set<RmFormhImageDtl>().Attach(imageDtl);
            _context.Entry(imageDtl).State = EntityState.Modified;
        }

        public async Task<List<RmFormhImageDtl>> GetFormHImageListAsync(int HeaderId)
        {
            return await _context.RmFormhImageDtl.Where(x => x.FhiActiveYn == true && x.FhiFhhPkRefNo == HeaderId).ToListAsync();
        }

        public async Task<int> GetSrNo(int headerId)
        {
            int? result = await _context.RmFormhImageDtl.Where(x => x.FhiFhhPkRefNo == headerId).Select(x => x.FhiImageSrno).MaxAsync();
            return result.HasValue ? result.Value : 0;
        }

        public async Task<int> GetNodActiveRMUCount(string searchObj) 
        {
            return await _context.RmFormHHdr.Where(x => x.FhhActiveYn == true && x.FhhRmu == searchObj && x.FhhSubmitSts == false).CountAsync();
        }

        public async Task<int> GetNodActiveSectionCount(string searchObj) 
        {
            return await _context.RmFormHHdr.Where(x => x.FhhActiveYn == true && x.FhhSection == searchObj && x.FhhSubmitSts==false).CountAsync();
        }

        public async Task<int> GetActiveFormHRecord() 
        {
            return await _context.RmFormHHdr.Where(x => x.FhhActiveYn == true && x.FhhSubmitSts == false).CountAsync();
        }

        public List<SelectListItem> GetReferenceNoByFormType(RequestFormReference lookUp)
        {
            if (lookUp.FormType == DTO.FormType.FormA)
            {
                var formA = (from h in _context.RmFormAHdr
                             from d in _context.RmFormADtl.Where(x => x.FadFahPkRefNo == h.FahPkRefNo)
                             where h.FahRoadCode == lookUp.RoadCode && d.FadFrmCh == lookUp.LocationFrom
                             && d.FadToCh == lookUp.LocationTo
                             && d.FadDt.Value.Month == lookUp.DateOfInspection.Value.Month
                             && d.FadDt.Value.Day == lookUp.DateOfInspection.Value.Day
                             && d.FadDt.Value.Year == lookUp.DateOfInspection.Value.Year
                             && d.FadActiveYn == true
                             select new SelectListItem
                             {
                                 Text = d.FadRefId,
                                 Value = d.FadPkRefNo.ToString()
                             }).ToList();
                return formA;
            }

            if (lookUp.FormType == DTO.FormType.FormJ)
            {
                var formJ = (from h in _context.RmFormJHdr
                             from d in _context.RmFormJDtl.Where(x => x.FjdFjhPkRefNo == h.FjhPkRefNo)
                             where
                             h.FjhRoadCode == lookUp.RoadCode
                             && h.FjhAssetGroupCode == lookUp.AssetGroup && d.FjdFrmCh == lookUp.LocationFrom
                             && Convert.ToInt32(d.FjdFrmChDeci) == lookUp.LocationFromDec && d.FjdToCh == lookUp.LocationTo
                             && Convert.ToInt32(d.FjdToChDeci) == lookUp.LocationToDec
                             && d.FjdDt.Value.Year == lookUp.DateOfInspection.Value.Year
                             && d.FjdDt.Value.Month == lookUp.DateOfInspection.Value.Month
                             && d.FjdDt.Value.Day == lookUp.DateOfInspection.Value.Day
                             && d.FjdActiveYn == true
                             select new SelectListItem
                             {
                                 Text = d.FjdRefId,
                                 Value = d.FjdPkRefNo.ToString()
                             }).ToList();


                return formJ;
            }
            return new List<SelectListItem>();
        }

        public FORMHRpt GetReportData(int headerId, int pageIndex, int pageCount)
        {
            FORMHRpt formH = (from o in _context.RmFormHHdr.AsEnumerable()
                              where headerId == o.FhhPkRefNo
                              let road = _context.RmRoadMaster.FirstOrDefault(s => s.RdmRdCode == o.FhhRoadCode)
                              select new FORMHRpt
                              {
                                  InspectionDate = o.FhhInspDt.HasValue ? o.FhhInspDt.Value.ToString("dd-MM-yyyy") : "",
                                  Chainage = $"{o.FhhFrmCh.GetValueOrDefault()}+{o.FhhFrmChDeci.GetValueOrDefault()} To {o.FhhToCh.GetValueOrDefault()}+{o.FhhToChDeci.GetValueOrDefault()}",
                                  DamageCausedBy = o.FhhDamCausedby,
                                  DamageDetail = o.FhhDamDtl,
                                  Division = o.FhhDiv,
                                  GeneralComments = o.FhhCltRemarks,
                                  ReceivedBy = o.FhhUsernameRcvdAuth,
                                  ReceivedByDesignation = o.FhhDesignationRcvdAuth,
                                  ReferenceNumber = o.FhhRefId,
                                  Remarks = o.FhhRemarks,
                                  ReportedByDate = o.FhhDtPrp.HasValue ? o.FhhDtPrp.Value.ToString("dd-MM-yyyy") : "",
                                  ReportedByDesignation = o.FhhDesignationPrp,
                                  ReportedByName = o.FhhUsernamePrp,
                                  RMU = o.FhhRmu,
                                  ReportNumber = o.FhhAuthRemarks,
                                  Recommendation = o.FhhAuthRecmd,
                                  RoadCode = o.FhhRoadCode,
                                  RoadName = road.RdmRdName,
                                  VerifiedBy = o.FhhUsernameVer,
                                  VerifiedByDesignation = o.FhhDesignationVer,
                                  VerifiedByDate = o.FhhDtVer.HasValue ? o.FhhDtVer.Value.ToString("dd-MM-yyyy") : "",
                                  VettedBy = o.FhhUsernameVetAuth,
                                  VettedByDesignation = o.FhhDesignationVetAuth
                              }).FirstOrDefault();
            return formH;
        }

        public (int id, bool alreadyExists) CheckAlreadyExists(FormType form, string roadCode, DateTime inspectionDate, string assetGroup, int locationFrom, int locationTo, int sourceRefNo)
        {
            var f = form == FormType.FormA ? "A" : form == FormType.FormJ ? "J" : "N";
            
            if(f=="A")
            {
                var isExists = _context.RmFormHHdr.AsEnumerable().FirstOrDefault(s =>
                s.FhhRoadCode == roadCode &&
                s.FhhInspDt.Value.ToString("yyyyMMdd") == inspectionDate.ToString("yyyyMMdd") &&
                s.FhhAssetGroupCode == assetGroup &&
                s.FhhFrmCh == locationFrom &&
                s.FhhToCh == locationTo && s.FhhFadPkRefNo == sourceRefNo);
                if (isExists != null)
                    return (isExists.FhhPkRefNo, true);
                else
                    return (_context.RmFormHHdr.Count(), false);
            }
            else if (f == "J")
            {
                var isExists = _context.RmFormHHdr.AsEnumerable().FirstOrDefault(s =>
                s.FhhRoadCode == roadCode &&
                s.FhhInspDt.Value.ToString("yyyyMMdd") == inspectionDate.ToString("yyyyMMdd") &&
                s.FhhAssetGroupCode == assetGroup &&
                s.FhhFrmCh == locationFrom &&
                s.FhhToCh == locationTo && s.FhhFjdPkRefNo == sourceRefNo);
                if (isExists != null)
                    return (isExists.FhhPkRefNo, true);
                else
                    return (_context.RmFormHHdr.Count(), false);
            }
            else
            {
                var isExists = _context.RmFormHHdr.AsEnumerable().FirstOrDefault(s =>
                    s.FhhRoadCode == roadCode &&
                    s.FhhInspDt.Value.ToString("yyyyMMdd") == inspectionDate.ToString("yyyyMMdd") &&
                    s.FhhAssetGroupCode == assetGroup &&
                    s.FhhFrmCh == locationFrom &&
                    s.FhhToCh == locationTo && s.FhhFadPkRefNo==null && s.FhhFjdPkRefNo==null);
                if (isExists != null)
                    return (isExists.FhhPkRefNo, true);
                else
                    return (_context.RmFormHHdr.Count(), false);
            }

        }

        public async Task<int> GetLastInsertedHeader()
        {
            return await _context.RmFormHHdr.CountAsync();
        }

        public async Task<RmFormHHdr> GetByReferenceID(string formHId)
        {
            return await _context.RmFormHHdr.FirstOrDefaultAsync(s => s.FhhRefId == formHId);
        }
    }
}
