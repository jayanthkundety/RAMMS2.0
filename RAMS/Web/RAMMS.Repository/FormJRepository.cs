using Microsoft.EntityFrameworkCore;
using RAMMS.Common;
using RAMMS.Common.Extensions;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.RequestBO;
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
    public class FormJRepository : RepositoryBase<RmFormJHdr>, IFormJRepository
    {
        public FormJRepository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> SaveFormAAsync(RmFormJHdr formAHeaderBO)
        {
            int affectedRowCount = 0;
           

            return affectedRowCount;
        }

        public int SaveFormAHdr(RmFormJHdr rmFormJHdr)
        {
            try
            {
                _context.Entry<RmFormJHdr>(rmFormJHdr).State = rmFormJHdr.FjhPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

               
                return rmFormJHdr.FjhPkRefNo;
            }
            catch (Exception)
            {
                return 500;

            }
        }

        public bool SubmittedToFormH(int fhhFjdPkRefNo)
        {
            var detail = _context.RmFormJDtl.FirstOrDefault(s => s.FjdPkRefNo == fhhFjdPkRefNo);
            if (detail != null)
            {
                detail.FjdFormhApp = "Yes";
            }
            return true;
        }

        
        public RmFormJHdr GetRmFormJHdr(RmFormJHdr _RmFormJHdr)
        {
            var usrInst = new RmFormJHdr();
            var userInst = _context.Set<RmFormJHdr>()
                     .AsNoTracking();
            
            return usrInst;
        }

        public async Task<bool> RemoveFormDetail(int detailsId)
        {
            bool isSuccess = true;

            var item = await (from x in _context.RmFormJDtl where x.FjdFjhPkRefNo == detailsId select x).FirstOrDefaultAsync();
            _context.RmFormJDtl.Remove(item);

            return isSuccess;
        }
        public async Task<RmFormJHdr> DetailView(RmFormJHdr RmFormJHdr)
        {
            var editDetail = await _context.Set<RmFormJHdr>().FirstOrDefaultAsync(a => a.FjhPkRefNo == RmFormJHdr.FjhPkRefNo);
            return editDetail;
        }

        public async Task<RmFormJHdr> GetFormWithDetailsByNoAsync(int formNo)
        {
            return await _context.RmFormJHdr
                        .Include(x => x.RmFormJDtl)
                        .FirstOrDefaultAsync(x => x.FjhPkRefNo == formNo);
        }

        public async Task<List<RmFormJHdr>> GetFilteredRecordList(FilteredPagingDefinition<FormJSearchGridDTO> filterOptions)
        {
            List<RmFormJHdr> result = new List<RmFormJHdr>();

            var query = (from x in _context.RmFormJHdr
                         select x);

            var _query = (from x in query
                          let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeDesc == x.FjhRmu || s.DdlTypeCode == x.FjhRmu))
                          let d = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "Section Code" && (s.DdlTypeDesc == x.FjhSection || s.DdlTypeCode == x.FjhSection))
                          select new
                          {
                              x,
                              d,
                              rmu
                          });


            _query = _query.Where(x => x.x.FjhActiveYn == true).OrderByDescending(x => x.x.FjhModDt);

            if (!string.IsNullOrEmpty(filterOptions.Filters.Road_Code))
            {
                _query = _query.Where(x => x.x.FjhRoadCode == filterOptions.Filters.Road_Code);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
            {
                _query = _query.Where(x => x.rmu.DdlTypeCode == filterOptions.Filters.RMU || x.rmu.DdlTypeDesc == filterOptions.Filters.RMU);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.Asset_GroupCode))
            {
                _query = _query.Where(x => x.x.FjhAssetGroupCode == filterOptions.Filters.Asset_GroupCode);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.Section))
            {
                _query = _query.Where(x => x.d.DdlTypeDesc == filterOptions.Filters.Section || x.d.DdlTypeCode == filterOptions.Filters.Section);
            }

            if (filterOptions.Filters.Year.HasValue)
            {
                _query = _query.Where(x => x.x.FjhYear == filterOptions.Filters.Year);
            }

            if (filterOptions.Filters.Month.HasValue)
            {
                _query = _query.Where(x => x.x.FjhMonth == filterOptions.Filters.Month);
            }

            if (filterOptions.Filters.ChinageFromKm.HasValue)
            {
                _query = _query.Where(x => x.x.RmFormJDtl.Any(dt => dt.FjdFrmCh >= filterOptions.Filters.ChinageFromKm && dt.FjdActiveYn == true));
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.ChinageFromM))
            {
                _query = _query.Where(x => x.x.RmFormJDtl.Any(dt => Convert.ToInt32(dt.FjdFrmChDeci) >= Convert.ToInt32(filterOptions.Filters.ChinageFromM) && dt.FjdActiveYn == true));
            }

            if (filterOptions.Filters.ChinageToKm.HasValue)
            {
                _query = _query.Where(x => x.x.RmFormJDtl.Any(dt => dt.FjdToCh <= filterOptions.Filters.ChinageToKm && dt.FjdActiveYn == true));
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.ChinageToM))
            {
                _query = _query.Where(x => x.x.RmFormJDtl.Any(dt => Convert.ToInt32(dt.FjdToChDeci) <= Convert.ToInt32(filterOptions.Filters.ChinageToM) && dt.FjdActiveYn == true));
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
            {
                _query = _query.Where(x =>
                             (x.x.FjhRefId ?? "").Contains(filterOptions.Filters.SmartInputValue)
                             || (x.x.FjhRmu ?? "").Contains(filterOptions.Filters.SmartInputValue)
                             || (x.x.FjhAssetGroupCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                             || (x.x.FjhSection ?? "").Contains(filterOptions.Filters.SmartInputValue)
                             || _context.RmDdLookup.Any(s => s.DdlType == "" && s.DdlTypeDesc == x.x.FjhSection
                                && s.DdlTypeValue.Contains(filterOptions.Filters.SmartInputValue))
                             || (x.x.FjhRoadCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                             || (x.x.FjhRoadName ?? "").Contains(filterOptions.Filters.SmartInputValue)
                             || (x.x.FjhUsernamePrp ?? "").Contains(filterOptions.Filters.SmartInputValue)
                             || (x.x.FjhUsernameVer ?? "").Contains(filterOptions.Filters.SmartInputValue)
                             || (x.x.FjhRoadName ?? "").Contains(filterOptions.Filters.SmartInputValue)
                             || ((x.x.FjhMonth.HasValue ? (x.x.FjhMonth.Value < 10 ? "0" : "") : "")
                             + (x.x.FjhMonth.HasValue ? x.x.FjhMonth.Value.ToString() : "")
                             + "/"
                             + (x.x.FjhYear ?? 0).ToString()).Contains(filterOptions.Filters.SmartInputValue)
                             || (x.x.FjhSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue)
                             );
            }


            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 0)
                    _query = _query.OrderByDescending(s => s.x.FjhModDt);
                if (filterOptions.ColumnIndex == 1)
                    _query = _query.OrderBy(s => s.x.FjhPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    _query = _query.OrderBy(s => s.x.FjhRefId);
                if (filterOptions.ColumnIndex == 3)
                    _query = _query.OrderBy(s => s.x.FjhRmu);
                if (filterOptions.ColumnIndex == 4)
                    _query = _query.OrderBy(s => s.x.FjhRmu);
                if (filterOptions.ColumnIndex == 5)
                    _query = _query.OrderBy(s => s.d.DdlTypeCode);
                if (filterOptions.ColumnIndex == 6)
                    _query = _query.OrderBy(s => s.d.DdlTypeDesc);
                if (filterOptions.ColumnIndex == 7)
                    _query = _query.OrderBy(s => s.x.FjhYear).ThenBy(r => r.x.FjhMonth);
                if (filterOptions.ColumnIndex == 8)
                    _query = _query.OrderBy(s => s.x.FjhRoadCode);
                if (filterOptions.ColumnIndex == 9)
                    _query = _query.OrderBy(s => s.x.FjhAssetGroupCode);
                if (filterOptions.ColumnIndex == 10)
                    _query = _query.OrderBy(s => s.x.FjhSubmitSts);
                if (filterOptions.ColumnIndex == 11)
                    _query = _query.OrderBy(s => s.x.FjhUsernamePrp);
                if (filterOptions.ColumnIndex == 12)
                    _query = _query.OrderBy(s => s.x.FjhUsernameVer);

            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 1)
                    _query = _query.OrderByDescending(s => s.x.FjhPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    _query = _query.OrderByDescending(s => s.x.FjhRefId);
                if (filterOptions.ColumnIndex == 3)
                    _query = _query.OrderByDescending(s => s.x.FjhRmu);
                if (filterOptions.ColumnIndex == 4)
                    _query = _query.OrderByDescending(s => s.x.FjhRmu);
                if (filterOptions.ColumnIndex == 5)
                    _query = _query.OrderByDescending(s => s.d.DdlTypeCode);
                if (filterOptions.ColumnIndex == 6)
                    _query = _query.OrderByDescending(s => s.d.DdlTypeDesc);
                if (filterOptions.ColumnIndex == 7)
                    _query = _query.OrderByDescending(s => s.x.FjhYear).ThenBy(r => r.x.FjhMonth);
                if (filterOptions.ColumnIndex == 8)
                    _query = _query.OrderByDescending(s => s.x.FjhRoadCode);
                if (filterOptions.ColumnIndex == 9)
                    _query = _query.OrderByDescending(s => s.x.FjhAssetGroupCode);
                if (filterOptions.ColumnIndex == 10)
                    _query = _query.OrderByDescending(s => s.x.FjhSubmitSts);
                if (filterOptions.ColumnIndex == 11)
                    _query = _query.OrderByDescending(s => s.x.FjhUsernamePrp);
                if (filterOptions.ColumnIndex == 12)
                    _query = _query.OrderByDescending(s => s.x.FjhUsernameVer);
            }

            result = await _query.Select(s => s.x).Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            return result;
        }

        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormJSearchGridDTO> filterOptions)
        {
            var query = (from x in _context.RmFormJHdr select x);

            var _query = (from x in query
                          let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeDesc == x.FjhRmu || s.DdlTypeCode == x.FjhRmu))
                          let d = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "Section Code" && (s.DdlTypeDesc == x.FjhSection || s.DdlTypeCode == x.FjhSection))
                          select new
                          {
                              x,
                              d,
                              rmu
                          });



            _query = _query.Where(x => x.x.FjhActiveYn == true).OrderByDescending(x => x.x.FjhModDt);

            if (!string.IsNullOrEmpty(filterOptions.Filters.Road_Code))
            {
                _query = _query.Where(x => x.x.FjhRoadCode == filterOptions.Filters.Road_Code);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
            {
                _query = _query.Where(x => x.rmu.DdlTypeCode == filterOptions.Filters.RMU || x.rmu.DdlTypeDesc == filterOptions.Filters.RMU);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.Asset_GroupCode))
            {
                _query = _query.Where(x => x.x.FjhAssetGroupCode == filterOptions.Filters.Asset_GroupCode);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.Section))
            {
                _query = _query.Where(x => x.d.DdlTypeDesc == filterOptions.Filters.Section || x.d.DdlTypeCode == filterOptions.Filters.Section);
            }

            if (filterOptions.Filters.Year.HasValue)
            {
                _query = _query.Where(x => x.x.FjhYear == filterOptions.Filters.Year);
            }

            if (filterOptions.Filters.Month.HasValue)
            {
                _query = _query.Where(x => x.x.FjhMonth == filterOptions.Filters.Month);
            }

            if (filterOptions.Filters.ChinageFromKm.HasValue)
            {
                _query = _query.Where(x => x.x.RmFormJDtl.Any(dt => dt.FjdFrmCh >= filterOptions.Filters.ChinageFromKm && dt.FjdActiveYn==true));
            } 

            if (!string.IsNullOrEmpty(filterOptions.Filters.ChinageFromM))
            {
                _query = _query.Where(x => x.x.RmFormJDtl.Any(dt => Convert.ToInt32(dt.FjdFrmChDeci) >= Convert.ToInt32(filterOptions.Filters.ChinageFromM) && dt.FjdActiveYn == true));
            }

            if (filterOptions.Filters.ChinageToKm.HasValue)
            {
                _query = _query.Where(x => x.x.RmFormJDtl.Any(dt => dt.FjdToCh <= filterOptions.Filters.ChinageToKm && dt.FjdActiveYn==true));
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.ChinageToM))
            {
                _query = _query.Where(x => x.x.RmFormJDtl.Any(dt => Convert.ToInt32(dt.FjdToChDeci) <= Convert.ToInt32(filterOptions.Filters.ChinageToM) && dt.FjdActiveYn == true));
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
            {
                _query = _query.Where(x =>
                             (x.x.FjhRefId ?? "").Contains(filterOptions.Filters.SmartInputValue)
                             || (x.x.FjhRmu ?? "").Contains(filterOptions.Filters.SmartInputValue)
                             || (x.x.FjhAssetGroupCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                             || (x.x.FjhSection ?? "").Contains(filterOptions.Filters.SmartInputValue)
                             || _context.RmDdLookup.Any(s => s.DdlType == "" && s.DdlTypeDesc == x.x.FjhSection
                                && s.DdlTypeValue.Contains(filterOptions.Filters.SmartInputValue))
                             || (x.x.FjhRoadCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                             || (x.x.FjhRoadName ?? "").Contains(filterOptions.Filters.SmartInputValue)
                             || (x.x.FjhUsernamePrp ?? "").Contains(filterOptions.Filters.SmartInputValue)
                             || (x.x.FjhUsernameVer ?? "").Contains(filterOptions.Filters.SmartInputValue)
                             || (x.x.FjhRoadName ?? "").Contains(filterOptions.Filters.SmartInputValue)
                             || ((x.x.FjhMonth.HasValue ? (x.x.FjhMonth.Value < 10 ? "0" : "") : "")
                             + (x.x.FjhMonth.HasValue ? x.x.FjhMonth.Value.ToString() : "")
                             + "/"
                             + (x.x.FjhYear ?? 0).ToString()).Contains(filterOptions.Filters.SmartInputValue)
                             || (x.x.FjhSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue)
                             );
            }

            return await _query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<string>> GetSectionByRMU(string rmu)
        {

            var query = (from section in _context.RmFormJHdr
                         where section.FjhRmu == rmu && section.FjhActiveYn == true
                         select section.FjhSection).ToListAsync().ConfigureAwait(false);
            return await query;
        }

        public async Task<IEnumerable<RmAssetDefectCode>> GetDefectCode(string assetGroup)
        {
            if (assetGroup != "" && assetGroup != null)
            {
                return await _context.RmAssetDefectCode.Where(a => a.AdcAssetGrpCode == assetGroup && a.AdcFormNo == "FORM J").ToListAsync();
            }
            else
            {
                return await _context.RmAssetDefectCode.Where(a => a.AdcFormNo == "FORM J").ToListAsync();
            }
        }

        public async Task<int> CheckWithRef(FormJHeaderRequestDTO formAHeader)
        {
            var data = await _context.RmFormJHdr.Where(x => x.FjhRefId == formAHeader.Id && x.FjhActiveYn == true).FirstOrDefaultAsync();
            if (data != null)
            {
                return data.FjhPkRefNo;
            }
            else
            {
                return 0;
            }

        }

        public async Task<RmFormJHdr> GetFAHRefIDById(int id)
        {
            var data = await _context.RmFormJHdr.FirstOrDefaultAsync(s => s.FjhPkRefNo == id);
            return data;
        }

        public async Task<int?> CreateDtl(RmFormJDtl formADetails)
        {
            try
            {
                var data = _context.RmFormJDtl.Add(formADetails);
                await _context.SaveChangesAsync();
                return formADetails.FjdFjhPkRefNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RmFormJDtl> GetDetailByIdAsync(int detailId)
        {
            return await _context.RmFormJDtl.Where(x => x.FjdPkRefNo == detailId && x.FjdActiveYn == true).FirstOrDefaultAsync();
        }

        public void UpdateDetail(RmFormJDtl request)
        {
            _context.Set<RmFormJDtl>().Attach(request);
            _context.Entry(request).State = EntityState.Modified;
        }

        public async Task<List<RmFormJDtl>> GetDetailRecordList(FilteredPagingDefinition<FormJDetailsRequestDTO> DetailList)
        {
            List<RmFormJDtl> result = new List<RmFormJDtl>();
            var query = (from x in _context.RmFormJDtl select x).Where(x => x.FjdFjhPkRefNo == DetailList.Filters.HeaderNo && x.FjdActiveYn == true);

            result = await query.Skip(DetailList.StartPageNo).Take(DetailList.RecordsPerPage)
                                .ToListAsync();

          
            return result;
        }

        public async Task<int> GetDetailRecordCount(FilteredPagingDefinition<FormJDetailsRequestDTO> filterOptions)
        {
            var query = (from x in _context.RmFormJDtl select x).Where(x => x.FjdFjhPkRefNo == filterOptions.Filters.HeaderNo && x.FjdActiveYn == true);
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<int> GetSrNo(int headerId)
        {
            int result = await _context.RmFormJDtl.Where(x => x.FjdFjhPkRefNo == headerId).CountAsync();
            return result;
        }

        public async Task<(int id, bool exists)> CheckAutoGeneratedReferenceNumber(string fjdRefId)
        {
            var data = await _context.RmFormJDtl.FirstOrDefaultAsync(s => s.FjdRefId == fjdRefId);
            if (data != null)
            {
                var result = _context.RmFormJDtl.Where(r => r.FjdFjhPkRefNo == data.FjdFjhPkRefNo).Count();
                return (result, true);
            }

            return (0, false);
        }

        public async Task<int?> CreateDtlV1(RmFormJDtl domainModelFormA)
        {
            try
            {
                var data = _context.RmFormJDtl.Add(domainModelFormA);
                await _context.SaveChangesAsync();
                return domainModelFormA.FjdPkRefNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string CheckAlreadyExists(string roadCode, int month, int year, string assetGroup)
        {
            var s = _context.RmFormJHdr.FirstOrDefault(s => s.FjhRoadCode == roadCode &&
             s.FjhMonth == month && s.FjhYear == year && s.FjhAssetGroupCode == assetGroup && s.FjhActiveYn == true);
            return s != null ? s.FjhPkRefNo.ToString() : null;
        }

        public string GetAssetCodeByName(string name)
        {
            var dt = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "FormJ_Assets" && s.DdlTypeDesc == name);
            if (dt != null)
                return dt.DdlTypeValue;
            else
                return "";
        }

        public async Task<int> GetLastInsertedHeader()
        {
            try
            {
                var d = await _context.RmFormJHdr.MaxAsync(s => s.FjhPkRefNo);
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
                                   Value = o.DdlTypeDesc,
                                   CValue = o.DdlTypeValue
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

            if (!string.IsNullOrEmpty(request.RMU) &&
                string.IsNullOrEmpty(request.Section) &&
                string.IsNullOrEmpty(request.RoadCode))
            {
                ddl.RoadCode = (from o in _context.RmRoadMaster
                                where o.RdmActiveYn == true
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

        public FORMJRpt GetReportData(int headerId)
        {
            FORMJRpt result = new FORMJRpt();
            result.Header = (from o in _context.RmFormJHdr
                             let s = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (o.FjhRmu == s.DdlTypeDesc || o.FjhRmu == s.DdlTypeCode))
                             where o.FjhPkRefNo == headerId
                             select new FORMJHeaderRpt
                             {
                                 AuditedBy = o.FjhUsernameVet,
                                 AuditedByDesignation = o.FjhDesignationVet,
                                 AuditedDate = o.FjhDtVet,
                                 InspectedBy = o.FjhUsernamePrp,
                                 InspectedByDesignation = o.FjhDesignationPrp,
                                 InspectedDate = o.FjhDtPrp,
                                 CheckedBY = o.FjhUsernameVer,
                                 CheckedByDesignation = o.FjhDesignationVer,
                                 CheckedDate = o.FjhDtVer,
                                 RMUCode = o.FjhRmu,
                                 RMUName = s != null ? s.DdlTypeDesc : "",
                                 RoadCode = o.FjhRoadCode,
                                 RoadName = o.FjhRoadName,
                                 RefId = o.FjhRefId
                             }).FirstOrDefault();

            Func<string, string> GetPriorityCode = (a) =>
            {
                var code = _context.RmDdLookup.FirstOrDefault(g => g.DdlType == "Priority" && g.DdlTypeDesc.ToLower() == a.ToLower());
                if (code != null)
                    return code.DdlTypeValue;
                else
                    return a;
            };
            var _ = (from hd in _context.RmFormJDtl
                          where hd.FjdFjhPkRefNo == headerId
                          && hd.FjdActiveYn == true
                          select new
                          {
                              hd.FjdRefId,
                              hd.FjdDt,
                              hd.FjdSiteRef,
                              hd.FjdFrmCh,
                              hd.FjdFrmChDeci,
                              hd.FjdToCh,
                              hd.FjdToChDeci,
                              hd.FjdDefCode,
                              hd.FjdPrblmDesc,
                              hd.FjdWrkNeed,
                              hd.FjdWidth,
                              hd.FjdLength,
                              hd.FjdHeight,
                              hd.FjdPriority,
                              hd.FjdWi,
                              hd.FjdWs,
                              hd.FjdWtc,
                              hd.FjdWc,
                              hd.FjdRt,
                              hd.FjdRemarks
                              
                          }).ToList();
            var code = _context.RmDdLookup.Where(g => g.DdlType == "Priority").ToArray();
            result.Details = _.Select(s => new FORMJDetailRpt
            {
                Date = s.FjdDt.HasValue ? s.FjdDt.Value.ToString("dd-MM-yyyy") : "",
                RefId = s.FjdRefId ?? "",
                SiteRef = s.FjdSiteRef ?? "",
                LocationFrom = $"{s.FjdFrmCh.GetValueOrDefault()}+{s.FjdFrmChDeci ?? "0"}" ?? "",
                LocationTo = $"{s.FjdToCh.GetValueOrDefault()}+{s.FjdToChDeci ?? "0"}" ?? "",
                Dificiencies = s.FjdPrblmDesc ?? "",
                SACode = s.FjdDefCode ?? "",
                Dimention = $"{s.FjdLength}x{s.FjdWidth}x{s.FjdHeight}" ?? "",
                WorkInstallation = s.FjdWrkNeed ?? "",
                Pr = code.Where(t => t.DdlTypeDesc == s.FjdPriority).Select(s => s.DdlTypeValue).FirstOrDefault(),
                WI = s.FjdWi.HasValue ? s.FjdWi.Value.ToString() : "",
                WTC = s.FjdWtc.HasValue ? s.FjdWtc.Value.ToString() : "",
                WS = s.FjdWs.HasValue ? s.FjdWs.Value.ToString() : "",
                WC = s.FjdWc.HasValue ? s.FjdWc.Value.ToString() : "",
                RT = s.FjdRt.HasValue ? s.FjdRt.Value.ToString() : "",
                Remarks = s.FjdRemarks ?? ""
            }).ToList();
            return result;
        }

        public async Task<int> GetNodActiveRMUCount(string searchobj)
        {
            return await _context.RmFormJHdr.Where(x => x.FjhActiveYn == true && x.FjhRmu == searchobj && x.FjhSubmitSts == false).CountAsync();
        }

        public async Task<int> GetActiveSectionCount(string searchobj)
        {
            return await _context.RmFormJHdr.Where(x => x.FjhActiveYn == true && x.FjhSection == searchobj && x.FjhSubmitSts==false).CountAsync();
        }

        public async Task<int> GetActiveFormJRecord() 
        {
            return await _context.RmFormJHdr.Where(x => x.FjhActiveYn == true && x.FjhSubmitSts == false).CountAsync();
        }
        public async Task<GridWrapper<object>> GetFormJHeaderGrid(DataTableAjaxPostModel searchData)
        {
            var query = (from hdr in _context.RmFormJHdr
                         from rmu in _context.RmDdLookup.Where(rd => rd.DdlType == "RMU" && (rd.DdlTypeCode == hdr.FjhRmu || rd.DdlTypeValue == hdr.FjhRmu)).DefaultIfEmpty()
                         from sec in _context.RmDdLookup.Where(s => s.DdlType == "Section Code" && s.DdlTypeDesc == hdr.FjhSection).DefaultIfEmpty()
                         select new
                         {
                             RefNo = hdr.FjhPkRefNo,
                             RefID = hdr.FjhRefId,
                             RMU = hdr.FjhRmu,
                             RMUCode = rmu.DdlTypeCode,
                             RMUDesc = rmu.DdlTypeDesc,
                             Section = hdr.FjhSection,
                             SecCode = sec.DdlTypeCode,
                             SecDesc = sec.DdlTypeDesc,
                             MonthYear = ((!hdr.FjhMonth.HasValue ? "0" : ((hdr.FjhMonth.Value < 10 ? "0" : "") + hdr.FjhMonth.Value.ToString())) + "/" + (!hdr.FjhYear.HasValue ? "0" : hdr.FjhYear.Value.ToString())),
                             Month = hdr.FjhMonth.GetValueOrDefault(),
                             Year = hdr.FjhYear.GetValueOrDefault(),
                             RoadCode = hdr.FjhRoadCode,
                             RoadName = hdr.FjhRoadName,
                             AssetGroupCode = hdr.FjhAssetGroupCode,
                             Status = (hdr.FjhSubmitSts ? "Submitted" : "Saved"),
                             SubmitSts = hdr.FjhSubmitSts,
                             UsernamePrp = hdr.FjhUsernamePrp,
                             UsernameVer = hdr.FjhUsernameVer,
                             Active = hdr.FjhActiveYn
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
                            query = query.Where(x =>
                                 (x.RefID ?? "").Contains(strVal)
                                 || (x.RMU ?? "").Contains(strVal)
                                 || (x.RMUDesc ?? "").Contains(strVal)
                                 || (x.AssetGroupCode ?? "").Contains(strVal)
                                 || (x.RoadCode ?? "").Contains(strVal)
                                 || (x.RoadName ?? "").Contains(strVal)
                                 || (x.UsernamePrp ?? "").Contains(strVal)
                                 || (x.UsernameVer ?? "").Contains(strVal)
                                 || (x.MonthYear ?? "").Contains(strVal)
                                 || (x.Status ?? "").Contains(strVal)
                                 );
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

        public async Task<List<RmFormJDtl>> GetAllDtlById(int headerId)
        {
            return await _context.RmFormJDtl.Where(x => x.FjdFjhPkRefNo == headerId && x.FjdActiveYn == true).ToListAsync();
        }
    }
}
