using Microsoft.EntityFrameworkCore;
using RAMMS.Common.Extensions;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RAMS.Repository
{
    public class FormARepository : RepositoryBase<RmFormAHdr>, IFormARepository
    {
        public FormARepository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> SaveFormAAsync(RmFormAHdr formAHeaderBO)
        {
            int affectedRowCount = 0;
         

            return affectedRowCount;
        }

        public int SaveFormAHdr(RmFormAHdr rmFormAHdr)
        {
            try
            {
                _context.Entry<RmFormAHdr>(rmFormAHdr).State = rmFormAHdr.FahPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                
                return rmFormAHdr.FahPkRefNo;
            }
            catch (Exception)
            {
                return 500;

            }
        }
        
        public RmFormAHdr GetRmFormAHdr(RmFormAHdr _rmFormAHdr)
        {
            var usrInst = new RmFormAHdr();
            var userInst = _context.Set<RmFormAHdr>()
                     .AsNoTracking();
            
            return usrInst;
        }

        public async Task<bool> RemoveFormDetail(int detailsId)
        {
            bool isSuccess = true;

            var item = await (from x in _context.RmFormADtl where x.FadFahPkRefNo == detailsId select x).FirstOrDefaultAsync();
            _context.RmFormADtl.Remove(item);

            return isSuccess;
        }
        public async Task<RmFormAHdr> DetailView(RmFormAHdr rmFormAHdr)
        {
            var editDetail = await _context.Set<RmFormAHdr>().FirstOrDefaultAsync(a => a.FahPkRefNo == rmFormAHdr.FahPkRefNo);
            return editDetail;
        }

        public async Task<RmFormAHdr> GetFormWithDetailsByNoAsync(int formNo)
        {
            return await _context.RmFormAHdr
                        .Include(x => x.RmFormADtl)
                        .FirstOrDefaultAsync(x => x.FahPkRefNo == formNo);
        }

        public async Task<List<RmFormAHdr>> GetFilteredRecordList(FilteredPagingDefinition<FormASearchGridDTO> filterOptions)
        {
            List<RmFormAHdr> result = new List<RmFormAHdr>();
            var query = (from x in _context.RmFormAHdr
                         let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeCode == x.FahRmu || s.DdlTypeDesc == x.FahRmu))
                         let sec = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "Section Code" && (s.DdlTypeDesc == x.FahSection || s.DdlTypeCode == x.FahSection))
                         select new { rmu, sec, x });
            query = query.Where(x => x.x.FahActiveYn == true).OrderByDescending(x => x.x.FahModDt);

            if (!string.IsNullOrEmpty(filterOptions.Filters.Road_Code))
            {
                query = query.Where(x => x.x.FahRoadCode == filterOptions.Filters.Road_Code);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
            {
                query = query.Where(x => x.rmu.DdlTypeCode == filterOptions.Filters.RMU || x.rmu.DdlTypeDesc == filterOptions.Filters.RMU);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.Asset_GroupCode))
            {
                query = query.Where(x => x.x.FahAssetGroupCode == filterOptions.Filters.Asset_GroupCode);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.Section))
            {
                query = query.Where(x => x.sec.DdlTypeDesc == filterOptions.Filters.Section || x.sec.DdlTypeCode == filterOptions.Filters.Section);
            }

            if (filterOptions.Filters.Year.HasValue)
            {
                query = query.Where(x => x.x.FahYear == filterOptions.Filters.Year);
            }

            if (filterOptions.Filters.Month.HasValue)
            {
                query = query.Where(x => x.x.FahMonth == filterOptions.Filters.Month);
            }



            //if (filterOptions.Filters.ChinageFromKm.HasValue || filterOptions.Filters.ChinageFromM != null)

            //{
            //    query = query.Where(x =>x.x.RmFormADtl.Any(dt=> Convert.ToDouble(dt.FadFrmCh.ToString() + '.' + dt.FadFrmChDeci) >= Convert.ToDouble(filterOptions.Filters.ChinageFromKm.ToString() + '.' + filterOptions.Filters.ChinageFromM)));
            //}
            //if (filterOptions.Filters.ChinageFromKm.HasValue)
            //{

            //    query = query.Where(x => x.x.RmFormADtl.Any(dt => dt.FadFrmCh >= filterOptions.Filters.ChinageFromKm && dt.FadActiveYn == true));
            //}

            //if (filterOptions.Filters.ChinageFromM != null)
            //{
            //    var ts = Convert.ToDouble(filterOptions.Filters.ChinageFromM);
            //    query = query.Where(x => x.x.RmFormADtl.Any(dt => Convert.ToDouble(dt.FadFrmChDeci) >= ts && dt.FadActiveYn == true));
            //}

            if (filterOptions.Filters.ChinageFromKm.HasValue || filterOptions.Filters.ChinageFromM != null)
            {
                query = query.Where(x => x.x.RmFormADtl.Any(dt => Convert.ToDouble(dt.FadFrmCh.ToString() + "." + dt.FadFrmChDeci) >= Convert.ToDouble(filterOptions.Filters.ChinageFromKm.ToString() + "." + filterOptions.Filters.ChinageFromM) && dt.FadActiveYn == true));
            }

            if (filterOptions.Filters.ChinageToKm.HasValue || filterOptions.Filters.ChinageToM != null)
            {
                query = query.Where(x => x.x.RmFormADtl.Any(dt => Convert.ToDouble(dt.FadToCh.ToString() + "." + dt.FadToChDeci) <= Convert.ToDouble(filterOptions.Filters.ChinageToKm + "." + filterOptions.Filters.ChinageToM) && dt.FadActiveYn == true));
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
            {

                query = query.Where(x =>
                        (x.sec.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue)) 
                         || (x.x.FahRefId ?? "").Contains(filterOptions.Filters.SmartInputValue)
                         || (x.x.FahRmu ?? "").Contains(filterOptions.Filters.SmartInputValue)
                         || (x.x.FahAssetGroupCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                         || (x.x.FahRoadCode ?? "").Contains(filterOptions.Filters.SmartInputValue)

                         || (x.x.FahUsernamePrp ?? "").Contains(filterOptions.Filters.SmartInputValue)
                         || (x.x.FahUsernameVer ?? "").Contains(filterOptions.Filters.SmartInputValue)
                         || (x.x.FahSection ?? "").Contains(filterOptions.Filters.SmartInputValue)
                         || ((x.x.FahMonth.HasValue ? (x.x.FahMonth.Value < 10 ? "0" : "") : "")
                         + (x.x.FahMonth.HasValue ? x.x.FahMonth.Value.ToString() : "")
                         + "/"
                         + (x.x.FahYear ?? 0).ToString()).Contains(filterOptions.Filters.SmartInputValue)
                         || (x.x.FahSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue)
                         );
            }

            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(s => s.x.FahRefId);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(s => s.x.FahRmu);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(s => s.x.FahRmu);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(s => s.sec.DdlTypeCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(s => s.x.FahSection);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(s => s.x.FahYear).ThenBy(r => r.x.FahMonth);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderBy(s => s.x.FahRoadCode);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderBy(s => s.x.FahAssetGroupCode);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderBy(s => s.x.FahSubmitSts);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderBy(s => s.x.FahUsernamePrp);
                if (filterOptions.ColumnIndex == 12)
                    query = query.OrderBy(s => s.x.FahUsernameVer);
                if (filterOptions.ColumnIndex == 0)
                    query = query.OrderByDescending(s => s.x.FahModDt);
            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(s => s.x.FahRefId);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(s => s.x.FahRmu);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(s => s.x.FahRmu);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(s => s.sec.DdlTypeCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(s => s.x.FahSection);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(s => s.x.FahYear).ThenByDescending(r => r.x.FahMonth);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderByDescending(s => s.x.FahRoadCode);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderByDescending(s => s.x.FahAssetGroupCode);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderByDescending(s => s.x.FahSubmitSts);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderByDescending(s => s.x.FahUsernamePrp);
                if (filterOptions.ColumnIndex == 12)
                    query = query.OrderByDescending(s => s.x.FahUsernameVer);

            }

            result = await query.Select(s => s.x).Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            return result;
        }

        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormASearchGridDTO> filterOptions)
        {
            var query = (from x in _context.RmFormAHdr
                         let sec = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "Section Code" && (s.DdlTypeDesc == x.FahSection || s.DdlTypeCode == x.FahSection))
                         let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeDesc == x.FahRmu || s.DdlTypeCode == x.FahRmu))
                         select new { rmu, sec, x });
            query = query.Where(x => x.x.FahActiveYn == true);

            if (!string.IsNullOrEmpty(filterOptions.Filters.Road_Code))
            {
                query = query.Where(x => x.x.FahRoadCode == filterOptions.Filters.Road_Code);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
            {
                query = query.Where(x => x.rmu.DdlTypeCode == filterOptions.Filters.RMU || x.rmu.DdlTypeDesc == filterOptions.Filters.RMU);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.Asset_GroupCode))
            {
                query = query.Where(x => x.x.FahAssetGroupCode == filterOptions.Filters.Asset_GroupCode);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.Section))
            {
                query = query.Where(x => x.sec.DdlTypeDesc == filterOptions.Filters.Section || x.sec.DdlTypeCode == filterOptions.Filters.Section);
            }

            if (filterOptions.Filters.Year.HasValue)
            {
                query = query.Where(x => x.x.FahYear == filterOptions.Filters.Year);
            }

            if (filterOptions.Filters.Month.HasValue)
            {
                query = query.Where(x => x.x.FahMonth == filterOptions.Filters.Month);
            }


            if (filterOptions.Filters.ChinageFromKm.HasValue)
            {
                query = query.Where(x => x.x.RmFormADtl.Any(dt => dt.FadFrmCh >= filterOptions.Filters.ChinageFromKm && dt.FadActiveYn == true));
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.ChinageFromM))
            {
                query = query.Where(x => x.x.RmFormADtl.Any(dt => Convert.ToInt32(dt.FadFrmChDeci) >= Convert.ToInt32(filterOptions.Filters.ChinageFromM) && dt.FadActiveYn == true));

            }

            if (filterOptions.Filters.ChinageToKm.HasValue)
            {
                query = query.Where(x => x.x.RmFormADtl.Any(dt => dt.FadToCh <= filterOptions.Filters.ChinageToKm && dt.FadActiveYn == true));
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.ChinageToM))
            {
                query = query.Where(x => x.x.RmFormADtl.Any(dt => Convert.ToInt32(dt.FadToChDeci) <= Convert.ToInt32(filterOptions.Filters.ChinageToM) && dt.FadActiveYn == true));
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
            {

                query = query.Where(x =>
                        (x.sec.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))  
                            ||
                         (x.x.FahRefId ?? "").Contains(filterOptions.Filters.SmartInputValue)
                         || (x.x.FahRmu ?? "").Contains(filterOptions.Filters.SmartInputValue)
                         || (x.x.FahAssetGroupCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                         || (x.x.FahRoadCode ?? "").Contains(filterOptions.Filters.SmartInputValue)

                         || (x.x.FahUsernamePrp ?? "").Contains(filterOptions.Filters.SmartInputValue)
                         || (x.x.FahUsernameVer ?? "").Contains(filterOptions.Filters.SmartInputValue)
                         || (x.x.FahSection ?? "").Contains(filterOptions.Filters.SmartInputValue)
                         || ((x.x.FahMonth.HasValue ? (x.x.FahMonth.Value < 10 ? "0" : "") : "")
                         + (x.x.FahMonth.HasValue ? x.x.FahMonth.Value.ToString() : "")
                         + "/"
                         + (x.x.FahYear ?? 0).ToString()).Contains(filterOptions.Filters.SmartInputValue)
                         || (x.x.FahSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue)
                         );
            }
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<string>> GetSectionByRMU(string rmu)
        {

            var query = (from section in _context.RmFormAHdr
                         where section.FahRmu == rmu && section.FahActiveYn == true
                         select section.FahSection).ToListAsync().ConfigureAwait(false);
            return await query;
        }

        public async Task<IEnumerable<RmAssetDefectCode>> GetDefectCode(string assetGroup)
        {
            return await _context.RmAssetDefectCode.Where(a => a.AdcAssetGrpCode == assetGroup && a.AdcFormNo == "Form A").ToListAsync();

        }

        public async Task<int> CheckwithRef(FormAHeaderRequestDTO formAHeader)
        {
            var data = await _context.RmFormAHdr.Where(x => x.FahRefId == formAHeader.Id && x.FahActiveYn == true).FirstOrDefaultAsync();
            if (data != null)
            {
                return data.FahPkRefNo;
            }
            else
            {
                return 0;
            }

        }

        public async Task<(int, bool)> CheckAutoGeneratedReferenceNumber(string exists)
        {
            var data = await _context.RmFormADtl.FirstOrDefaultAsync(s => s.FadRefId == exists);
            if (data != null)
            {
                var result = _context.RmFormADtl.Where(r => r.FadFahPkRefNo == data.FadFahPkRefNo).OrderByDescending(s => s.FadSrno).FirstOrDefault();
                if (result != null)
                {
                    return (result.FadSrno.GetValueOrDefault(), true);
                }
            }

            return (0, false);
        }

        public async Task<RmFormAHdr> GetFAHRefIDById(int id)
        {
            var data = await _context.RmFormAHdr.FirstOrDefaultAsync(s => s.FahPkRefNo == id);
            return data;
        }

        public async Task<int?> CreateDtl(RmFormADtl formADetails)
        {
            try
            {
                var data = _context.RmFormADtl.Add(formADetails);
                await _context.SaveChangesAsync();
                return formADetails.FadFahPkRefNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RmFormADtl> GetDetailByIdAsync(int detailId)
        {
            return await _context.RmFormADtl.Where(x => x.FadPkRefNo == detailId && x.FadActiveYn == true).FirstOrDefaultAsync();
        }

        public void UpdateDetail(RmFormADtl request)
        {
            _context.Set<RmFormADtl>().Attach(request);
            _context.Entry(request).State = EntityState.Modified;
        }

        public async Task<List<RmFormADtl>> GetDetailRecordList(FilteredPagingDefinition<FormADetailsRequestDTO> DetailList)
        {
            List<RmFormADtl> result = new List<RmFormADtl>();
            var query = (from x in _context.RmFormADtl select x).Where(x => x.FadFahPkRefNo == DetailList.Filters.HeaderNo && x.FadActiveYn == true);

            result = await query.Skip(DetailList.StartPageNo).Take(DetailList.RecordsPerPage)
                                .ToListAsync();

            
            return result;
        }

        public async Task<int> GetDetailRecordCount(FilteredPagingDefinition<FormADetailsRequestDTO> filterOptions)
        {
            var query = (from x in _context.RmFormADtl select x).Where(x => x.FadFahPkRefNo == filterOptions.Filters.HeaderNo && x.FadActiveYn == true);
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<int> GetSrNo(int headerId)
        {
            int result = await _context.RmFormADtl.Where(x => x.FadFahPkRefNo == headerId).CountAsync();
            return result;
        }

        public async Task<int?> CreateDtlV1(RmFormADtl domainModelFormA)
        {
            try
            {
                var data = _context.RmFormADtl.Add(domainModelFormA);
                await _context.SaveChangesAsync();
                return domainModelFormA.FadPkRefNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> GetLastInsertedHeader()
        {
            try
            {
                var d = await _context.RmFormAHdr.MaxAsync(s => s.FahPkRefNo);
                return d + 1;
            }
            catch (Exception e)
            {
                return 1;
            }
        }

        public FormASearchDropdown GetDropdown(RequestDropdownFormA request)
        {
            FormASearchDropdown ddl = new FormASearchDropdown();
            if (!string.IsNullOrEmpty(request.RMU))
            {
                ddl.Section = (from o in _context.RmDdLookup
                               where (o.DdlTypeValue == request.RMU
                                            || o.DdlTypeValue == request.RMU)
                                            && o.DdlType == "Section Code" && o.DdlActiveYn == true
                               select new DropDown
                               {
                                   Text = o.DdlTypeCode + "-" + o.DdlTypeDesc,
                                   Value = o.DdlTypeDesc
                               }).ToList();
                ddl.RoadCode = (from o in _context.RmRoadMaster
                                where (o.RdmRmuCode == request.RMU ||
                                o.RdmRmuName == request.RMU) && o.RdmActiveYn == true
                                select new DropDown
                                {
                                    Text = o.RdmRdCode + "-" + o.RdmRdName,
                                    Value = o.RdmRdCode
                                }).Distinct().ToList();
            }


            if (!string.IsNullOrEmpty(request.RMU) &&
                !string.IsNullOrEmpty(request.Section) &&
                string.IsNullOrEmpty(request.RoadCode))
            {
                ddl.RoadCode = (from o in _context.RmRoadMaster
                                where o.RdmSecName == request.Section && o.RdmActiveYn == true
                                select new DropDown
                                {
                                    Text = o.RdmRdCode + "-" + o.RdmRdName,
                                    Value = o.RdmRdCode
                                }).Distinct().ToList();
            }

            if (string.IsNullOrEmpty(request.RMU) &&
                string.IsNullOrEmpty(request.Section) &&
                string.IsNullOrEmpty(request.RoadCode))
            {
                ddl.RMU = (from o in _context.RmDdLookup
                           where o.DdlType == "RMU" && o.DdlActiveYn == true
                           select new DropDown
                           {
                               Text = o.DdlTypeDesc,
                               Value = o.DdlTypeCode
                           }).ToList();
                ddl.Section = (from o in _context.RmDdLookup
                               where o.DdlType == "Section Code" && o.DdlActiveYn == true
                               select new DropDown
                               {
                                   Text = o.DdlTypeCode + "-" + o.DdlTypeDesc,
                                   Value = o.DdlTypeDesc
                               }).ToList();
                ddl.RoadCode = (from o in _context.RmRoadMaster
                                where o.RdmActiveYn == true
                                select new DropDown
                                {
                                    Text = o.RdmRdCode + "-" + o.RdmRdName,
                                    Value = o.RdmRdCode
                                }).Distinct().ToList();
            }

            return ddl;

        }

        public string CheckAlreadyExists(string roadCode, int month, int year, string assetGroup)
        {
            var s = _context.RmFormAHdr.FirstOrDefault(s => s.FahRoadCode == roadCode &&
             s.FahMonth == month && s.FahYear == year && s.FahAssetGroupCode == assetGroup && s.FahActiveYn == true);
            return s != null ? s.FahPkRefNo.ToString() : null;
        }

        public string GetAssetCodeByName(string name)
        {
            var dt = _context.RmDdLookup.FirstOrDefault(s => s.DdlTypeCode == "FormA" && s.DdlTypeDesc == name);
            if (dt != null)
                return dt.DdlTypeValue;
            else
                return "";
        }

        public FORMARpt GetReportData(int headerId, int pageIndex, int pageCount)
        {
            FORMARpt obj = new FORMARpt();
            obj.Header = (from o in _context.RmFormAHdr
                          let s = _context.RmDdLookup.FirstOrDefault(s => (s.DdlTypeCode == o.FahRmu || s.DdlTypeDesc == o.FahRmu) && s.DdlType == "RMU")
                          where o.FahPkRefNo == headerId
                          select new FORMAHeaderRpt
                          {
                              RoadCode = o.FahRoadCode,
                              RoadName = o.FahRoadName,
                              RMUCode = o.FahRmu,
                              RMUName = s.DdlTypeDesc,
                              VerifiedBY = o.FahUsernameVer,
                              VerifiedDate = o.FahDtVer,
                              VerifiedByDesignation = o.FahDesignationVer,
                              InspectedBy = o.FahUsernamePrp,
                              InspectedDate = o.FahDtPrp,
                              InspectedByDesignation = o.FahDesignationPrp,
                              RefId = o.FahRefId
                          }).FirstOrDefault();
            var result = (from hd in _context.RmFormADtl
                          join h in _context.RmFormAHdr on hd.FadFahPkRefNo equals h.FahPkRefNo
                          where hd.FadFahPkRefNo == headerId
                          && hd.FadActiveYn == true
                          select new
                          {
                              h.FahAssetGroupCode,
                              hd.FadDefCode,
                              hd.FadDt,
                              hd.FadRefId,
                              hd.FadSiteRef,
                              hd.FadFrmCh,
                              hd.FadToCh,
                              hd.FadFrmChDeci,
                              hd.FadToChDeci,
                              hd.FadDesc,
                              hd.FadUnit,
                              hd.FadLength,
                              hd.FadHeight,
                              hd.FadWidth,
                              hd.FadAdp,
                              hd.FadCdr,
                              hd.FadPriority,
                              hd.FadWi,
                              hd.FadWtc,
                              hd.FadWc,
                              hd.FadWs,
                              hd.FadSftPs,
                              hd.FadSftWis,
                              hd.FadRt,
                              hd.FadRemarks,
                              hd.FadActCode
                          }).ToList();

            Func<string, string, string> GetActivity = (a, group) =>
            {
                var gcode = _context.RmDdLookup.FirstOrDefault(g => g.DdlTypeDesc == group && g.DdlType == "FormA_Assets");
                if (gcode != null)
                {
                    var dd = _context.RmDdLookup.FirstOrDefault(r => r.DdlTypeCode == a && r.DdlType == "ACT-" + gcode.DdlTypeValue);
                    if (dd != null)
                        return dd.DdlTypeValue;
                }
                return "";
            };

            var code = _context.RmDdLookup.Where(g => g.DdlType == "Priority").ToArray();


            obj.Detail = result.Select(s => new FORMADetailRpt
            {
                Date = s.FadDt.HasValue ? s.FadDt.Value.ToString("dd-MM-yyyy") : "",
                RefId = s.FadRefId ?? "",
                SiteRef = s.FadSiteRef ?? "",
                LocationFrom = $"{s.FadFrmCh.GetValueOrDefault()}+{s.FadFrmChDeci ?? "0"}" ?? "",
                LocationTo = $"{s.FadToCh.GetValueOrDefault()}+{s.FadToChDeci ?? "0"}" ?? "",
                Description = s.FadDesc ?? "",
                ActivityCode = s.FadActCode ?? "",
                Unit = s.FadUnit ?? "",
                Dimention = $"{(s.FadLength.HasValue ? s.FadLength : 0)}x{(s.FadWidth.HasValue ? s.FadWidth : 0)}x{(s.FadHeight.HasValue ? s.FadHeight : 0)}" ?? "",
                ADP = s.FadAdp ?? "",
                L_P = (s.FadSiteRef ?? "").Contains("LHS") && "CWU,CWR,CWF,Carriageway- Rigid Pavement,Carriageway- Unpaved Road,Carriageway- Flexible Pavement".Contains(s.FahAssetGroupCode ?? "") ? s.FadDefCode : "",
                L_EL = (s.FadSiteRef ?? "").Contains("LHS") && "ELM,Edge Line Marking".Contains(s.FahAssetGroupCode ?? "") ? s.FadDefCode : "",
                L_D = (s.FadSiteRef ?? "").Contains("LHS") && "DR,Drain,DI,Ditches".Contains(s.FahAssetGroupCode ?? "") ? s.FadDefCode : "",
                L_R = (s.FadSiteRef ?? "").Contains("LHS") && "RR,Area within Reserve,Road Line Marking".Contains(s.FahAssetGroupCode ?? "") ? s.FadDefCode : "",
                L_Sh = (s.FadSiteRef ?? "").Contains("LHS") && "SH,Shoulder".Contains(s.FahAssetGroupCode ?? "") ? s.FadDefCode : "",
                CL = (s.FadSiteRef ?? "").Contains("CL") ? s.FadDefCode : "",
                R_P = (s.FadSiteRef ?? "").Contains("RHS") && "CWU,CWR,CWF,Carriageway- Rigid Pavement,Carriageway- Unpaved Road,Carriageway- Flexible Pavement".Contains(s.FahAssetGroupCode ?? "") ? s.FadDefCode : "",
                R_D = (s.FadSiteRef ?? "").Contains("RHS") && "DR,Drain,DI,Ditches".Contains(s.FahAssetGroupCode ?? "") ? s.FadDefCode : "",
                R_EL = (s.FadSiteRef ?? "").Contains("RHS") && "ELM,Edge Line Marking".Contains(s.FahAssetGroupCode ?? "") ? s.FadDefCode : "",
                R_R = (s.FadSiteRef ?? "").Contains("RHS") && "RR,Area within Reserve,Road Line Marking".Contains(s.FahAssetGroupCode ?? "") ? s.FadDefCode : "",
                R_Sh = (s.FadSiteRef ?? "").Contains("RHS") && "SH,Shoulder".Contains(s.FahAssetGroupCode ?? "") ? s.FadDefCode : "",
                Cul = (s.FadSiteRef ?? "").Contains("") && "CV,Culvert".Contains(s.FahAssetGroupCode ?? "") ? s.FadDefCode : "",
                Br = (s.FadSiteRef ?? "").Contains("") && "BR,Bridge".Contains(s.FahAssetGroupCode ?? "") ? s.FadDefCode : "",
                Pr = s.FadPriority ?? "", 
                WI = s.FadWi.HasValue && s.FadWi != 0 ? s.FadWi.Value.ToString() : "",
                CDR = s.FadCdr ?? "",
                WTC = s.FadWtc.HasValue ? s.FadWtc.Value != 0 ? s.FadWtc.Value.ToString() : "" : "",
                WS = s.FadWs.HasValue ? s.FadWs.Value != 0 ? s.FadWs.Value.ToString() : "" : "",
                WC = s.FadWc.HasValue ? s.FadWc.Value != 0 ? s.FadWc.Value.ToString() : "" : "",
                PS = s.FadSftPs ?? "",
                WIS = s.FadSftWis.HasValue ? s.FadSftWis.Value != 0 ? s.FadSftWis.Value.ToString() : "" : "",
                RT = s.FadRt.HasValue ? s.FadRt.Value != 0 ? s.FadRt.Value.ToString() : "" : "",
                Remarks = s.FadRemarks ?? "",
                DefCode = s.FadDefCode ?? ""
            }).ToList();
            return obj;
        }
        public async Task<int> GetNodActiveRMUCount(string searchObj)
        {
            return await _context.RmFormAHdr.Where(x => x.FahActiveYn == true && x.FahRmu == searchObj && x.FahSubmitSts == false).CountAsync();
        }
        public async Task<int> GetNodActiveSectionCount(string searchObj)
        {
            return await _context.RmFormAHdr.Where(x => x.FahActiveYn == true && x.FahSection == searchObj && x.FahSubmitSts==false).CountAsync();
        }

        public async Task<int> GetActiveFormARecord() 
        {
            return await _context.RmFormAHdr.Where(x => x.FahActiveYn == true && x.FahSubmitSts == false).CountAsync();
        }
    }
}
