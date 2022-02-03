using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RAMMS.Common;
using RAMMS.Common.Extensions;
using RAMMS.Common.RefNumber;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RAMS.Repository
{
    public class FormDRepository : RepositoryBase<RmFormDHdr>, IFormDRepository
    {
        public FormDRepository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public int SaveFormDHdr(RmFormDHdr rmFormDHdr)
        {
            try
            {
                _context.Entry<RmFormDHdr>(rmFormDHdr).State = rmFormDHdr.FdhPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return rmFormDHdr.FdhPkRefNo;
            }
            catch (Exception)
            {
                return 500;

            }
        }

        public int SaveFormDLabour(RmFormDLabourDtl rmFormDHdr)
        {
            try
            {
                _context.Entry<RmFormDLabourDtl>(rmFormDHdr).State = rmFormDHdr.FdldPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return rmFormDHdr.FdldPkRefNo;
            }
            catch (Exception)
            {
                return 500;

            }
        }

        public int SaveFormDMaterial(RmFormDMaterialDtl rmFormDHdr)
        {
            try
            {
                _context.Entry<RmFormDMaterialDtl>(rmFormDHdr).State = rmFormDHdr.FdmdPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return rmFormDHdr.FdmdPkRefNo;
            }
            catch (Exception)
            {
                return 500;

            }
        }

        public int SaveFormDEquipment(RmFormDEquipDtl rmFormDHdr)
        {
            try
            {
                _context.Entry<RmFormDEquipDtl>(rmFormDHdr).State = rmFormDHdr.FdedPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return rmFormDHdr.FdedPkRefNo;
            }
            catch (Exception)
            {
                return 500;

            }
        }

        public int SaveFormDDetails(RmFormDDtl rmFormDHdr)
        {
            try
            {
                _context.Entry<RmFormDDtl>(rmFormDHdr).State = rmFormDHdr.FddPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return rmFormDHdr.FddPkRefNo;
            }
            catch (Exception)
            {
                return 500;

            }
        }
        public RmFormDHdr GetRmFormDHdr(RmFormDHdr rmFormDHdr)
        {
            var usrInst = new RmFormDHdr();
            var userInst = _context.Set<RmFormDHdr>()
                     .AsNoTracking();

            return usrInst;
        }

        public async Task<RmFormDHdr> DetailView(RmFormDHdr rmFormDHdr)
        {
            var editDetail = await _context.Set<RmFormDHdr>().FirstOrDefaultAsync(a => a.FdhPkRefNo == rmFormDHdr.FdhPkRefNo);
            return editDetail;
        }

        public async Task<RmFormDHdr> GetFormWithDetailsByNoAsync(int formNo)
        {
            return await _context.RmFormDHdr
                        .FirstOrDefaultAsync(x => x.FdhPkRefNo == formNo);
        }

        public async Task<List<RmFormDHdr>> GetFilteredRecordList(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions)
        {
            List<RmFormDHdr> result = new List<RmFormDHdr>();
            var query = (from x in _context.RmFormDHdr
                         let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeCode == x.FdhRmu || s.DdlTypeDesc == x.FdhRmu))
                         let sec = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "Section Code" && (s.DdlTypeDesc == x.FdhRoadCode || s.DdlTypeCode == x.FdhRoadCode))
                         select new { rmu, sec, x });



            query = query.Where(x => x.x.FdhActiveYn == true).OrderByDescending(x => x.x.FdhPkRefNo);
            if (filterOptions.Filters != null)
            {

                if (!string.IsNullOrEmpty(filterOptions.Filters.Road_Code))
                {
                    query = query.Where(x => x.sec.DdlTypeDesc == filterOptions.Filters.Road_Code || x.sec.DdlTypeCode == filterOptions.Filters.Road_Code);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
                {
                    query = query.Where(x => x.rmu.DdlTypeCode == filterOptions.Filters.RMU || x.rmu.DdlTypeDesc == filterOptions.Filters.RMU);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.WeekNo))
                {
                    query = query.Where(x => x.x.FdhWeekNo == Convert.ToInt32(filterOptions.Filters.WeekNo));
                }

                if (filterOptions.Filters.Year.HasValue)
                {
                    query = query.Where(x => x.x.FdhYear == filterOptions.Filters.Year);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.WeekDay))
                {
                    query = query.Where(x => x.x.FdhDay == filterOptions.Filters.WeekDay);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    query = query.Where(x => x.x.FdhRmu.Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.rmu.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || (x.sec.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || x.x.FdhCrewSupName.Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.x.FdhSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.FdhRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.FdhRoadCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.FdhUsernamePrp.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.FdhUsernameVer.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.FdhUsernamePrp.Contains(filterOptions.Filters.SmartInputValue)
                                        || (filterOptions.Filters.SmartInputValue.IsInt() && x.x.FdhPkRefNo.Equals(filterOptions.Filters.SmartInputValue.AsInt())));

                }
            }


            if (filterOptions.sortOrder == SortOrder.Ascending)
            {

                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderBy(x => x.x.FdhPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(x => x.x.FdhRmu);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(x => x.x.FdhDivName);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(x => x.sec.DdlTypeCode);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(x => x.x.FdhCrewSupName);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(x => x.x.FdhWeekNo);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(x => x.x.FdhYear);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderBy(x => x.x.FdhUsernamePrp);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderBy(x => x.x.FdhUsernameVer);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderBy(x => x.x.FdhSubmitSts);


            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderByDescending(x => x.x.FdhPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(x => x.x.FdhRmu);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(x => x.x.FdhDivName);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(x => x.sec.DdlTypeCode);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(x => x.x.FdhCrewSupName);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(x => x.x.FdhWeekNo);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(x => x.x.FdhYear);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderByDescending(x => x.x.FdhUsernamePrp);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderByDescending(x => x.x.FdhUsernameVer);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderByDescending(x => x.x.FdhSubmitSts);
            }


            result = await query.Select(s => s.x)
                    .Skip(filterOptions.StartPageNo)
                    .Take(filterOptions.RecordsPerPage)
                    .ToListAsync();
            return result;
        }

 
        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions)
        {
            var query = (from x in _context.RmFormDHdr
                         let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeCode == x.FdhRmu || s.DdlTypeDesc == x.FdhRmu))
                         let sec = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "Section Code" && (s.DdlTypeDesc == x.FdhRoadCode || s.DdlTypeCode == x.FdhRoadCode))
                         select new { rmu, sec, x });


            query = query.Where(x => x.x.FdhActiveYn == true);
            if (filterOptions.Filters != null)
            {

                if (!string.IsNullOrEmpty(filterOptions.Filters.Road_Code))
                {
                    query = query.Where(x => x.sec.DdlTypeDesc == filterOptions.Filters.Road_Code || x.sec.DdlTypeCode == filterOptions.Filters.Road_Code);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
                {
                    query = query.Where(x => x.rmu.DdlTypeCode == filterOptions.Filters.RMU || x.rmu.DdlTypeDesc == filterOptions.Filters.RMU);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.WeekNo))
                {
                    query = query.Where(x => x.x.FdhWeekNo == Convert.ToInt32(filterOptions.Filters.WeekNo));
                }

                if (filterOptions.Filters.Year.HasValue)
                {
                    query = query.Where(x => x.x.FdhYear == filterOptions.Filters.Year);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.WeekDay))
                {
                    query = query.Where(x => x.x.FdhDay == filterOptions.Filters.WeekDay);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    query = query.Where(x => x.x.FdhRmu.Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.rmu.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || (x.sec.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || (x.x.FdhSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.FdhCrewSupName.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.FdhRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.FdhRoadCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.FdhUsernamePrp.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.FdhUsernameVer.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.FdhUsernamePrp.Contains(filterOptions.Filters.SmartInputValue)
                                        || (filterOptions.Filters.SmartInputValue.IsInt() && x.x.FdhPkRefNo.Equals(filterOptions.Filters.SmartInputValue.AsInt())));
                }
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
            return await _context.RmAssetDefectCode.Where(a => a.AdcAssetGrpCode == assetGroup).ToListAsync();
        }

        public async Task<int> CheckwithRef(FormDHeaderRequestDTO formDHeader)
        {
            var data = await _context.RmFormDHdr.Where(x => x.FdhPkRefNo == formDHeader.No).FirstOrDefaultAsync();
            if (data != null)
            {
                return data.FdhPkRefNo;
            }
            else
            {
                return 0;
            }

        }

        public void SaveWarImage(IEnumerable<RmWarImageDtl> rmWarImage)
        {
            _context.RmWarImageDtl.AddRange(rmWarImage);
        }

        public void SaveAccUccImage(IEnumerable<RmAccUcuImageDtl> rmAccUcuImage)
        {
            _context.RmAccUcuImageDtl.AddRange(rmAccUcuImage);
        }

        public void UpdateWar(RmWarImageDtl rmWarImage)
        {
            _context.Set<RmWarImageDtl>().Attach(rmWarImage);
            _context.Entry(rmWarImage).State = EntityState.Modified;
        }

        public void UpdateAccUcu(RmAccUcuImageDtl rmAccUcuImage)
        {
            _context.Set<RmAccUcuImageDtl>().Attach(rmAccUcuImage);
            _context.Entry(rmAccUcuImage).State = EntityState.Modified;
        }

        public Task<RmWarImageDtl> GetWarImageByIdAsync(int warId)
        {
            return _context.RmWarImageDtl.Where(x => x.FwarPkRefNo == warId).FirstOrDefaultAsync();
        }

        public Task<RmAccUcuImageDtl> GetAccUccImageById(int accUccId)
        {
            return _context.RmAccUcuImageDtl.Where(x => x.FauPkRefNo == accUccId).FirstOrDefaultAsync();
        }

        public Task<List<RmWarImageDtl>> GetWarImagelist(int formDId)
        {
            return _context.RmWarImageDtl.Where(x => x.FwarFddPkRefNo == formDId && x.FwarActiveYn == true).ToListAsync();
        }

        public Task<List<RmAccUcuImageDtl>> GetAccUccImagelist(int formDId)
        {
            return _context.RmAccUcuImageDtl.Where(x => x.FauFddPkRefNo == formDId && x.FauActiveYn == true).ToListAsync();
        }
        public async Task<int> GetWARId(int headerId, string type)
        {
            int? result = await _context.RmWarImageDtl.Where(x => x.FwarFddPkRefNo == headerId && x.FwarImageTypeCode == type).Select(x => x.FwarImageSrno).MaxAsync();
            return result.HasValue ? result.Value : 0;
        }
        public async Task<int> GetUCUId(int headerId, string type)
        {
            int? result = await _context.RmAccUcuImageDtl.Where(x => x.FauFddPkRefNo == headerId && x.FauAccUcu == type).Select(x => x.FauImageSrno).MaxAsync();
            return result.HasValue ? result.Value : 0;
        }
  
        public async Task<IEnumerable<RmDdLookup>> GetDivisions()
        {
            return await _context.RmDdLookup.Where(x => x.DdlActiveYn == true && x.DdlType == "Division").ToListAsync();
        }

        public async Task<IEnumerable<RmDdLookup>> GetActivityMainTask()
        {
            return await _context.RmDdLookup.Where(x => x.DdlActiveYn == true && x.DdlType == "ACT-Main_Task").ToListAsync();
        }

        public async Task<IEnumerable<RmDdLookup>> GetActivitySubTask()
        {
            return await _context.RmDdLookup.Where(x => x.DdlActiveYn == true && x.DdlType == "ACT-Sub_Task").ToListAsync();
        }

        public async Task<IEnumerable<RmDdLookup>> GetSectionCode()
        {
            return await _context.RmDdLookup.Where(x => x.DdlActiveYn == true && x.DdlType == "Section Code").ToListAsync();
        }

        public async Task<IEnumerable<RmDdLookup>> GetLabourCode()
        {
            return await _context.RmDdLookup.Where(x => x.DdlActiveYn == true && x.DdlType == "Labour_Code").ToListAsync();
            
        }

        public async Task<IEnumerable<RmDdLookup>> GetMaterialCode()
        {
            return await _context.RmDdLookup.Where(x => x.DdlActiveYn == true && x.DdlType == "Material_Code").ToListAsync();
        }

        public async Task<IEnumerable<RmDdLookup>> GetEquipmentCode()
        {
            return await _context.RmDdLookup.Where(x => x.DdlType == "Equipment_Code").ToListAsync();
        }

        public async Task<IEnumerable<RmDdLookup>> GetRMU()
        {
            return await _context.RmDdLookup.Where(x => x.DdlActiveYn == true && x.DdlType == "RMU").ToListAsync();
        }
        public async Task<IEnumerable<RmRoadMaster>> GetRoadCodes()
        {
            return await _context.RmRoadMaster.Where(x => x.RdmActiveYn == true).ToListAsync();
        }

        public async Task<IEnumerable<RmDdLookup>> GetERTActivityCode()
        {
            return await _context.RmDdLookup.Where(x => x.DdlActiveYn == true && x.DdlType == "Act-FormD").ToListAsync();
        }

        public async Task<bool> CheckHdrRefereceId(string id)
        {
            var obj = await _context.RmFormDHdr.Where(x => x.FdhRefId == id).ToListAsync();
            return obj.Count >= 1 ? true : false;
        }

        public async Task<string> CheckAlreadyExists(int? WeekNo, int? year, string crewUnit, string day, string rmu, string secCode)
        {
            var s = await _context.RmFormDHdr.FirstOrDefaultAsync(s => s.FdhRmu == rmu &&
             s.FdhActiveYn == true &&
             s.FdhRoadCode == secCode && s.FdhWeekNo == WeekNo && s.FdhDay == day && s.FdhYear == year && s.FdhCrewUnit == crewUnit);
            return s != null ? s.FdhPkRefNo.ToString() : null;
        }


        public async Task<bool> CheckDetailsRefereceId(string id)
        {
            var obj = await _context.RmFormDDtl.Where(x => x.FddRefId == id && x.FddActiveYn == true).ToListAsync();
            return obj.Count > 1 ? true : false;
        }

        public async Task<IEnumerable<RmDivRmuSecMaster>> GetSectionCodesByRMU(string rmu)
        {
            if (rmu != "" && rmu != null)
                return await _context.RmDivRmuSecMaster.Where(x => x.RdsmActiveYn == true && x.RdsmRmuCode == rmu).ToListAsync();
            else
                return await _context.RmDivRmuSecMaster.Where(x => x.RdsmActiveYn == true).ToListAsync();
        }

        public async Task<IEnumerable<RmRoadMaster>> GetRoadCodesByRMU(string rmu)
        {
            return await _context.RmRoadMaster.Where(x => x.RdmActiveYn == true && x.RdmRmuCode == rmu).ToListAsync();
        }

        public async Task<IEnumerable<RmFormXHdr>> GetFormXReferenceId(string rodeCode)
        {
            return await _context.RmFormXHdr.Where(x => x.FxhActiveYn == true && x.FxhRoadCode == rodeCode).ToListAsync();
        }

        public async Task<string> GetMaxIdLength()
        {
            var count = await _context.RmFormDHdr.Select(m => m.FdhPkRefNo).CountAsync();
            if (count > 0)
            {
                var data = await _context.RmFormDHdr.OrderByDescending(s => s.FdhPkRefNo).ToListAsync();
                return Convert.ToString(data.FirstOrDefault().FdhPkRefNo);
            }
            return "1";
        }

        public async Task<IEnumerable<RmRoadMaster>> GetRoadCodeBySectionCode(string secCode)
        {
            return await _context.RmRoadMaster.Where(x => x.RdmActiveYn == true && (string.IsNullOrEmpty(secCode) || x.RdmSecCode == Convert.ToInt32(secCode))).ToListAsync();
        }

        public async Task<int?> GetLabourSRNO(int id)
        {
            var count = await _context.RmFormDLabourDtl.Where(x => x.FdldFdhPkRefNo == id).Select(m => m.FdldSrno).CountAsync();
            if (count > 0)
            {
                var query = (from x in _context.RmFormDLabourDtl where x.FdldFdhPkRefNo == id select x);
                return await query.MaxAsync(m => m.FdldSrno);
            }
            return 0;
        }

        public async Task<int?> GetEqpSRNO(int id)
        {
            var count = await _context.RmFormDEquipDtl.Where(x => x.FdedFdhPkRefNo == id).Select(m => m.FdedSrno).CountAsync();
            if (count > 0)
            {
                var query = (from x in _context.RmFormDEquipDtl where x.FdedFdhPkRefNo == id select x);
                return await query.MaxAsync(m => m.FdedSrno);
            }
            return 0;
        }

        public async Task<int?> GetMatSrno(int id)
        {
            var count = await _context.RmFormDMaterialDtl.Where(x => x.FdmdFdhPkRefNo == id).Select(m => m.FdmdSrno).CountAsync();
            if (count > 0)
            {
                var query = (from x in _context.RmFormDMaterialDtl where x.FdmdFdhPkRefNo == id select x);
                return await query.MaxAsync(m => m.FdmdSrno);
            }
            return 0;
        }

        public async Task<int?> GetDtlSrno(int? id)
        {
            var count = await _context.RmFormDDtl.Where(x => x.FddFdhPkRefNo == id).Select(m => m.FddSrno).CountAsync();
            if (count > 0)
            {
                var query = (from x in _context.RmFormDDtl where x.FddFdhPkRefNo == id select x);
                return await query.MaxAsync(m => m.FddSrno);
            }
            return 0;
        }

        public async Task<RmFormDHdr> FindSaveFormDHdr(RmFormDHdr formDHeader, bool updateSubmit)
        {
            bool isAdd = false;
            if (formDHeader.FdhPkRefNo == 0)
            {
                isAdd = true;
                formDHeader.FdhActiveYn = true;
                _context.RmFormDHdr.Add(formDHeader);
            }
            else
            {
                _context.RmFormDHdr.Attach(formDHeader);
                var entry = _context.Entry(formDHeader);
                entry.Property(x => x.FdhSignPrp).IsModified = true;
                entry.Property(x => x.FdhSignVer).IsModified = true;
                entry.Property(x => x.FdhSignVerSo).IsModified = true;
                entry.Property(x => x.FdhSignVet).IsModified = true;
                entry.Property(x => x.FdhSignPrcdSo).IsModified = true;
                entry.Property(x => x.FdhSignAgrdSo).IsModified = true;
                entry.Property(x => x.FdhModBy).IsModified = true;
                entry.Property(x => x.FdhModDt).IsModified = true;


                entry.Property(x => x.FdhUseridPrp).IsModified = true;
                entry.Property(x => x.FdhUsernamePrp).IsModified = true;
                entry.Property(x => x.FdhDtPrp).IsModified = true;
                entry.Property(x => x.FdhDesignationPrp).IsModified = true;

                entry.Property(x => x.FdhUseridVer).IsModified = true;
                entry.Property(x => x.FdhUsernameVer).IsModified = true;
                entry.Property(x => x.FdhDtVer).IsModified = true;
                entry.Property(x => x.FdhDesignationVer).IsModified = true;

                entry.Property(x => x.FdhUseridVerSo).IsModified = true;
                entry.Property(x => x.FdhUsernameVerSo).IsModified = true;
                entry.Property(x => x.FdhDtVerSo).IsModified = true;
                entry.Property(x => x.FdhDesignationVerSo).IsModified = true;

                entry.Property(x => x.FdhUseridVet).IsModified = true;
                entry.Property(x => x.FdhUsernameVet).IsModified = true;
                entry.Property(x => x.FdhDtVet).IsModified = true;
                entry.Property(x => x.FdhDesignationVet).IsModified = true;

                entry.Property(x => x.FdhUseridPrcdSo).IsModified = true;
                entry.Property(x => x.FdhUsernamePrcdSo).IsModified = true;
                entry.Property(x => x.FdhDtPrcdSo).IsModified = true;
                entry.Property(x => x.FdhDesignationPrcdSo).IsModified = true;

                entry.Property(x => x.FdhUseridAgrdSo).IsModified = true;
                entry.Property(x => x.FdhUsernameAgrdSo).IsModified = true;
                entry.Property(x => x.FdhDtAgrdSo).IsModified = true;
                entry.Property(x => x.FdhDesignationAgrdSo).IsModified = true;

                if (updateSubmit)
                {
                    entry.Property(x => x.FdhSubmitSts).IsModified = true;
                }
            }
            _context.SaveChanges();
            if (isAdd)
            {
                IDictionary<string, string> lstData = new Dictionary<string, string>();
                lstData.Add("Year", Utility.ToString(formDHeader.FdhYear));
                lstData.Add("MonthNo", Utility.ToString(formDHeader.FdhMonth));
                lstData.Add("WeekNo", Utility.ToString(formDHeader.FdhWeekNo));
                lstData.Add("CrewUnit", formDHeader.FdhCrewUnit);
                lstData.Add(FormRefNumber.NewRunningNumber, Utility.ToString(formDHeader.FdhPkRefNo));
                formDHeader.FdhRefId = FormRefNumber.GetRefNumber(RAMMS.Common.RefNumber.FormType.FormDHeader, lstData);
                _context.SaveChanges();
            }
            return formDHeader;
        }


    }
}
