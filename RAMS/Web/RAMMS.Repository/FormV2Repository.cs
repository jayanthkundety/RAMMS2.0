using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RAMMS.Common;
using RAMMS.Common.Extensions;
using RAMMS.Common.RefNumber;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.Report;
using RAMMS.DTO.ResponseBO;
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
    public class FormV2Repository : RepositoryBase<RmFormV2Hdr>, IFormV2Repository
    {
        public FormV2Repository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public int SaveFormV2Hdr(RmFormV2Hdr rmFormV2Hdr)
        {
            try
            {
                _context.Entry<RmFormV2Hdr>(rmFormV2Hdr).State = rmFormV2Hdr.Fv2hPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return rmFormV2Hdr.Fv2hPkRefNo;
            }
            catch (Exception)
            {
                return 500;

            }
        }

        public int SaveFormV2Labour(RmFormV2Lab rmFormV2Hdr)
        {
            try
            {
                _context.Entry<RmFormV2Lab>(rmFormV2Hdr).State = rmFormV2Hdr.Fv2lPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return rmFormV2Hdr.Fv2lPkRefNo;
            }
            catch (Exception)
            {
                return 500;

            }
        }

        public int SaveFormV2Material(RmFormV2Mat rmFormV2Hdr)
        {
            try
            {
                _context.Entry<RmFormV2Mat>(rmFormV2Hdr).State = rmFormV2Hdr.Fv2mPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return rmFormV2Hdr.Fv2mPkRefNo;
            }
            catch (Exception)
            {
                return 500;

            }
        }

        public int SaveFormV2Equipment(RmFormV2Eqp rmFormV2Hdr)
        {
            try
            {
                _context.Entry<RmFormV2Eqp>(rmFormV2Hdr).State = rmFormV2Hdr.Fv2ePkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return rmFormV2Hdr.Fv2ePkRefNo;
            }
            catch (Exception)
            {
                return 500;

            }
        }

        public RmFormV2Hdr GetRmFormV2Hdr(RmFormV2Hdr rmFormV2Hdr)
        {
            var usrInst = new RmFormV2Hdr();
            var userInst = _context.Set<RmFormV2Hdr>()
                     .AsNoTracking();

            return usrInst;
        }

        public async Task<RmFormV2Hdr> DetailView(RmFormV2Hdr rmFormV2Hdr)
        {
            var editDetail = await _context.Set<RmFormV2Hdr>().FirstOrDefaultAsync(a => a.Fv2hPkRefNo == rmFormV2Hdr.Fv2hPkRefNo);
            return editDetail;
        }

        public async Task<RmFormV2Hdr> GetFormWithDetailsByNoAsync(int formNo)
        {
            return await _context.RmFormV2Hdr
                        .FirstOrDefaultAsync(x => x.Fv2hPkRefNo == formNo);
        }

        public async Task<List<RmFormV2Hdr>> GetFilteredRecordList(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions)
        {
            List<RmFormV2Hdr> result = new List<RmFormV2Hdr>();
            var query = (from x in _context.RmFormV2Hdr
                         let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeCode == x.Fv2hRmu || s.DdlTypeDesc == x.Fv2hRmu))
                         let sec = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "Section Code" && (s.DdlTypeDesc == x.Fv2hSecCode || s.DdlTypeCode == x.Fv2hSecCode))
                         let div = _context.RmDivRmuSecMaster.FirstOrDefault(s => s.RdsmSectionCode == x.Fv2hSecCode)
                         select new { rmu, sec, x, div });



            query = query.Where(x => x.x.Fv2hActiveYn == true).OrderByDescending(x => x.x.Fv2hPkRefNo);
            if (filterOptions.Filters != null)
            {

                if (!string.IsNullOrEmpty(filterOptions.Filters.Section))
                {
                    query = query.Where(x => x.sec.DdlTypeDesc == filterOptions.Filters.Section || x.sec.DdlTypeCode == filterOptions.Filters.Section);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
                {
                    query = query.Where(x => x.rmu.DdlTypeCode == filterOptions.Filters.RMU || x.rmu.DdlTypeDesc == filterOptions.Filters.RMU);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.Crew))
                {
                    query = query.Where(x => x.x.Fv2hCrewname == filterOptions.Filters.Crew || (x.x.Fv2hCrew.HasValue ? x.x.Fv2hCrew.ToString() == filterOptions.Filters.Crew : x.x.Fv2hCrew.ToString() == ""));
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ActivityCode))
                {
                    query = query.Where(x => x.x.Fv2hActCode  == filterOptions.Filters.ActivityCode );
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.x.Fv2hDt.HasValue ? (x.x.Fv2hDt.Value.Year == dt.Year && x.x.Fv2hDt.Value.Month == dt.Month && x.x.Fv2hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && !string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.x.Fv2hDt.HasValue ? (x.x.Fv2hDt.Value.Year == dt.Year && x.x.Fv2hDt.Value.Month == dt.Month && x.x.Fv2hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    query = query.Where(x => x.x.Fv2hRmu.Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.rmu.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || (x.sec.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || x.x.Fv2hCrewname.Contains(filterOptions.Filters.SmartInputValue)
                                        || ((bool)x.x.Fv2hSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv2hRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv2hSecCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv2hUsernameSch.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv2hUsernameAgr.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv2hUsernameAck.Contains(filterOptions.Filters.SmartInputValue)
                                        || (filterOptions.Filters.SmartInputValue.IsInt() && x.x.Fv2hPkRefNo.Equals(filterOptions.Filters.SmartInputValue.AsInt())));

                }
            }


            if (filterOptions.sortOrder == SortOrder.Ascending)
            {

                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderBy(x => x.x.Fv2hPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(x => x.x.Fv2hRmu);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(x => x.div.RdsmDivCode);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(x => x.x.Fv2hActCode);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(x => x.x.Fv2hSecCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(x => x.x.Fv2hCrewname);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(x => x.x.Fv2hDt);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderBy(x => x.x.Fv2hUsernameAck);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderBy(x => x.x.Fv2hUsernameAgr);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderBy(x => x.x.Fv2hUsernameSch);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderBy(x => x.x.Fv2hSubmitSts);


            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderByDescending(x => x.x.Fv2hPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(x => x.x.Fv2hRmu);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(x => x.div.RdsmDivCode);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(x => x.x.Fv2hActCode);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(x => x.x.Fv2hSecCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(x => x.x.Fv2hCrewname);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(x => x.x.Fv2hDt);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderByDescending(x => x.x.Fv2hUsernameAck);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderByDescending(x => x.x.Fv2hUsernameAgr);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderByDescending(x => x.x.Fv2hUsernameSch);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderByDescending(x => x.x.Fv2hSubmitSts);
            }


            result = await query.Select(s => s.x)
                    .Skip(filterOptions.StartPageNo)
                    .Take(filterOptions.RecordsPerPage)
                    .ToListAsync();
            return result;
        }


        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions)
        {
            List<RmFormV2Hdr> result = new List<RmFormV2Hdr>();
            var query = (from x in _context.RmFormV2Hdr
                         let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeCode == x.Fv2hRmu || s.DdlTypeDesc == x.Fv2hRmu))
                         let sec = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "Section Code" && (s.DdlTypeDesc == x.Fv2hSecCode || s.DdlTypeCode == x.Fv2hSecCode))
                         let div = _context.RmDivRmuSecMaster.FirstOrDefault(s => s.RdsmSectionCode == x.Fv2hSecCode)
                         select new { rmu, sec, x, div });



            query = query.Where(x => x.x.Fv2hActiveYn == true).OrderByDescending(x => x.x.Fv2hPkRefNo);
            if (filterOptions.Filters != null)
            {

                if (!string.IsNullOrEmpty(filterOptions.Filters.Section))
                {
                    query = query.Where(x => x.sec.DdlTypeDesc == filterOptions.Filters.Section || x.sec.DdlTypeCode == filterOptions.Filters.Section);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
                {
                    query = query.Where(x => x.rmu.DdlTypeCode == filterOptions.Filters.RMU || x.rmu.DdlTypeDesc == filterOptions.Filters.RMU);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.Crew))
                {
                    query = query.Where(x => x.x.Fv2hCrewname == filterOptions.Filters.Crew || (x.x.Fv2hCrew.HasValue ? x.x.Fv2hCrew.ToString() == filterOptions.Filters.Crew : x.x.Fv2hCrew.ToString() == ""));
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ActivityCode))
                {
                    query = query.Where(x => x.x.Fv2hActCode == filterOptions.Filters.ActivityCode  );
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.x.Fv2hDt.HasValue ? (x.x.Fv2hDt.Value.Year == dt.Year && x.x.Fv2hDt.Value.Month == dt.Month && x.x.Fv2hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && !string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.x.Fv2hDt.HasValue ? (x.x.Fv2hDt.Value.Year == dt.Year && x.x.Fv2hDt.Value.Month == dt.Month && x.x.Fv2hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    query = query.Where(x => x.x.Fv2hRmu.Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.rmu.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || (x.sec.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || x.x.Fv2hCrewname.Contains(filterOptions.Filters.SmartInputValue)
                                        || ((bool)x.x.Fv2hSubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv2hRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv2hSecCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv2hUsernameSch.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv2hUsernameAgr.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv2hUsernameAck.Contains(filterOptions.Filters.SmartInputValue)
                                        || (filterOptions.Filters.SmartInputValue.IsInt() && x.x.Fv2hPkRefNo.Equals(filterOptions.Filters.SmartInputValue.AsInt())));

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

        public async Task<int> CheckwithRef(FormV2HeaderResponseDTO formV2Header)
        {
            var data = await _context.RmFormV2Hdr.Where(x => x.Fv2hPkRefNo == formV2Header.PkRefNo).FirstOrDefaultAsync();
            if (data != null)
            {
                return data.Fv2hPkRefNo;
            }
            else
            {
                return 0;
            }

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
            return await _context.RmDdLookup.Where(x => x.DdlActiveYn == true && x.DdlType == "Act-FormV2").ToListAsync();
        }

        public async Task<bool> CheckHdrRefereceId(string id)
        {
            var obj = await _context.RmFormV2Hdr.Where(x => x.Fv2hRefId == id).ToListAsync();
            return obj.Count >= 1 ? true : false;
        }

        public async Task<string> CheckAlreadyExists(DateTime? date, string crewUnit, string day, string rmu, string secCode)
        {
            var s = await _context.RmFormV2Hdr.FirstOrDefaultAsync(s => s.Fv2hRmu == rmu &&
             s.Fv2hActiveYn == true &&
             s.Fv2hSecCode == secCode && s.Fv2hDt == date && s.Fv2hCrew.ToString() == crewUnit);
            return s != null ? s.Fv2hPkRefNo.ToString() : null;
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
            var count = await _context.RmFormV2Hdr.Select(m => m.Fv2hPkRefNo).CountAsync();
            if (count > 0)
            {
                var data = await _context.RmFormV2Hdr.OrderByDescending(s => s.Fv2hPkRefNo).ToListAsync();
                return Convert.ToString(data.FirstOrDefault().Fv2hPkRefNo);
            }
            return "1";
        }

        public async Task<IEnumerable<RmRoadMaster>> GetRoadCodeBySectionCode(string secCode)
        {
            return await _context.RmRoadMaster.Where(x => x.RdmActiveYn == true && (string.IsNullOrEmpty(secCode) || x.RdmSecCode == Convert.ToInt32(secCode))).ToListAsync();
        }


        public async Task<RmFormV2Hdr> FindSaveFormV2Hdr(RmFormV2Hdr formV2Header, bool updateSubmit)
        {
            bool isAdd = false;
            if (formV2Header.Fv2hPkRefNo == 0)
            {
                isAdd = true;
                formV2Header.Fv2hActiveYn = true;
                _context.RmFormV2Hdr.Add(formV2Header);
            }
            else
            {
                _context.RmFormV2Hdr.Attach(formV2Header);
                var entry = _context.Entry(formV2Header);
                entry.Property(x => x.Fv2hSignAck).IsModified = true;
                entry.Property(x => x.Fv2hSignAgr).IsModified = true;
                entry.Property(x => x.Fv2hSignSch).IsModified = true;                
                entry.Property(x => x.Fv2hModBy).IsModified = true;
                entry.Property(x => x.Fv2hModDt).IsModified = true;


                entry.Property(x => x.Fv2hUseridAck).IsModified = true;
                entry.Property(x => x.Fv2hUsernameAck).IsModified = true;
                entry.Property(x => x.Fv2hDtAck).IsModified = true;
                entry.Property(x => x.Fv2hDesignationAck).IsModified = true;

                entry.Property(x => x.Fv2hUseridAgr).IsModified = true;
                entry.Property(x => x.Fv2hUsernameAgr).IsModified = true;
                entry.Property(x => x.Fv2hDtAgr).IsModified = true;
                entry.Property(x => x.Fv2hDesignationAgr).IsModified = true;

                entry.Property(x => x.Fv2hUseridSch).IsModified = true;
                entry.Property(x => x.Fv2hUsernameSch).IsModified = true;
                entry.Property(x => x.Fv2hDtSch).IsModified = true;
                entry.Property(x => x.Fv2hDesignationSch).IsModified = true;

                if (updateSubmit)
                {
                    entry.Property(x => x.Fv2hSubmitSts).IsModified = true;
                }
            }
            _context.SaveChanges();
            if (isAdd)
            {
                IDictionary<string, string> lstData = new Dictionary<string, string>();
                lstData.Add("CrewUnit", formV2Header.Fv2hCrew.ToString());
                lstData.Add(FormRefNumber.NewRunningNumber, Utility.ToString(formV2Header.Fv2hPkRefNo));
                formV2Header.Fv2hRefId = FormRefNumber.GetRefNumber(RAMMS.Common.RefNumber.FormType.FormV2Header, lstData);
                _context.SaveChanges();
            }
            return formV2Header;
        }


    }
}
