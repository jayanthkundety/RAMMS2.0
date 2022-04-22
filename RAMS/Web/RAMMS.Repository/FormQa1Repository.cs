using Microsoft.EntityFrameworkCore;
using RAMMS.Common;
using RAMMS.Common.Extensions;
using RAMMS.Common.RefNumber;
using RAMMS.Domain.Models;
using RAMMS.DTO;
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
    public class FormQa1Repository : RepositoryBase<RmFormQa1Hdr>, IFormQa1Repository
    {
        public FormQa1Repository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions)
        {
            var query = (from x in _context.RmFormQa1Hdr select x);
            PrepareFilterQuery(filterOptions, ref query);
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<RmFormQa1Hdr>> GetFilteredRecordList(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions)
        {
            List<RmFormQa1Hdr> result = new List<RmFormQa1Hdr>();
            var query = (from x in _context.RmFormQa1Hdr select x);


            PrepareFilterQuery(filterOptions, ref query);

            result = await query.OrderByDescending(s => s.Fqa1hPkRefNo)
                                .Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            return result;
        }

        private void PrepareFilterQuery(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, ref IQueryable<RmFormQa1Hdr> query)
        {
            query = query.Where(x => x.Fqa1hActiveYn == true);

            if (filterOptions.Filters != null)
            {


                if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
                {
                    query = query.Where(x => x.Fqa1hRmu == filterOptions.Filters.RMU);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.Section))
                {
                    query = query.Where(x => x.Fqa1hSecCode == filterOptions.Filters.Section);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RoadCode))
                {
                    query = query.Where(x => x.Fqa1hRoadCode == filterOptions.Filters.RoadCode);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.Crew))
                {
                    query = query.Where(x => x.Fqa1hCrew.HasValue ? x.Fqa1hCrew == Convert.ToInt32(filterOptions.Filters.Crew) : x.Fqa1hCrew == null);

                }
                if (!string.IsNullOrEmpty(filterOptions.Filters.ActivityCode))
                {
                    query = query.Where(x => x.Fqa1hActCode == filterOptions.Filters.ActivityCode);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.Fqa1hDt.HasValue ? (x.Fqa1hDt.Value.Year == dt.Year && x.Fqa1hDt.Value.Month == dt.Month && x.Fqa1hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && !string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.Fqa1hDt.HasValue ? (x.Fqa1hDt.Value.Year == dt.Year && x.Fqa1hDt.Value.Month == dt.Month && x.Fqa1hDt.Value.Day == dt.Day) : false);
                    }
                }


                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    query = query.Where(x => x.Fqa1hRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.Fqa1hRoadCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.Fqa1hRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.Fqa1hRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.Fqa1hUsernameAssgn.Contains(filterOptions.Filters.SmartInputValue)
                                        || (filterOptions.Filters.SmartInputValue.IsInt() && x.Fqa1hPkRefNo.Equals(filterOptions.Filters.SmartInputValue.AsInt())));

                }
            }
        }

        public async Task<RmFormQa1Hdr> FindSaveFormQa1Hdr(RmFormQa1Hdr formQa1Header, bool updateSubmit)
        {
            bool isAdd = false;
            if (formQa1Header.Fqa1hPkRefNo == 0)
            {
                isAdd = true;
                formQa1Header.Fqa1hActiveYn = true;
                _context.RmFormQa1Hdr.Add(formQa1Header);
            }
            else
            {
                _context.RmFormQa1Hdr.Attach(formQa1Header);
                var entry = _context.Entry(formQa1Header);

                entry.Property(x => x.Fqa1hModBy).IsModified = true;
                entry.Property(x => x.Fqa1hModDt).IsModified = true;

                entry.Property(x => x.Fqa1hUseridAssgn).IsModified = true;
                entry.Property(x => x.Fqa1hInitialAssgn).IsModified = true;
                entry.Property(x => x.Fqa1hUsernameAssgn).IsModified = true;
                entry.Property(x => x.Fqa1hDtAssgn).IsModified = true;

                entry.Property(x => x.Fqa1hUseridExec).IsModified = true;
                entry.Property(x => x.Fqa1hInitialExec).IsModified = true;
                entry.Property(x => x.Fqa1hUsernameExec).IsModified = true;
                entry.Property(x => x.Fqa1hDtExec).IsModified = true;

                entry.Property(x => x.Fqa1hUseridChked).IsModified = true;
                entry.Property(x => x.Fqa1hInitialChked).IsModified = true;
                entry.Property(x => x.Fqa1hUsernameChked).IsModified = true;
                entry.Property(x => x.Fqa1hDtChked).IsModified = true;


                entry.Property(x => x.Fqa1hUseridAudit).IsModified = true;
                entry.Property(x => x.Fqa1hUsernameAudit).IsModified = true;
                entry.Property(x => x.Fqa1hDtAudit).IsModified = true;
                entry.Property(x => x.Fqa1hDesignationAudit).IsModified = true;
                entry.Property(x => x.Fqa1hOfficeAudit).IsModified = true;
                entry.Property(x => x.Fqa1hSignAudit).IsModified = true;

                entry.Property(x => x.Fqa1hUseridWit).IsModified = true;
                entry.Property(x => x.Fqa1hUsernameWit).IsModified = true;
                entry.Property(x => x.Fqa1hDtWit).IsModified = true;
                entry.Property(x => x.Fqa1hDesignationWit).IsModified = true;
                entry.Property(x => x.Fqa1hOfficeWit).IsModified = true;
                entry.Property(x => x.Fqa1hSignWit).IsModified = true;

                if (updateSubmit)
                {
                    entry.Property(x => x.Fqa1hSubmitSts).IsModified = true;
                }
            }
            _context.SaveChanges();
            if (isAdd)
            {
                IDictionary<string, string> lstData = new Dictionary<string, string>();
                lstData.Add("RMU", formQa1Header.Fqa1hRmu.ToString());
                lstData.Add("CrewUnit", formQa1Header.Fqa1hCrew.ToString());
                lstData.Add("ActCode", formQa1Header.Fqa1hActCode.ToString());
                lstData.Add("Year", formQa1Header.Fqa1hDt.Value.Year.ToString());
                lstData.Add("MonthNo", formQa1Header.Fqa1hDt.Value.Month.ToString());
                lstData.Add("Day", formQa1Header.Fqa1hDt.Value.Day.ToString());
                lstData.Add(FormRefNumber.NewRunningNumber, Utility.ToString(formQa1Header.Fqa1hPkRefNo));
                formQa1Header.Fqa1hRefId = FormRefNumber.GetRefNumber(RAMMS.Common.RefNumber.FormType.FormQA1Header, lstData);
                _context.SaveChanges();
            }
            return formQa1Header;
        }

        public async Task<RmFormQa1Lab> SaveLabour(RmFormQa1Lab labour)
        {
            _context.RmFormQa1Lab.Add(labour);
            await _context.SaveChangesAsync();
            return labour;
        }

        public async Task<RmFormQa1Hdr> GetFormQA1(int pkRefNo)
        {
            var result = await _context.RmFormQa1Hdr.Include(m => m.RmFormQa1EqVh)
                .Include(m => m.RmFormQa1Gc)
                .Include(m => m.RmFormQa1Gen)
                .Include(m => m.RmFormQa1Lab)
                .Include(m => m.RmFormQa1Mat)
                .Include(m => m.RmFormQa1Ssc)
                .Include(m => m.RmFormQa1Tes)
                .Include(m => m.RmFormQa1Wcq)
                .Include(m => m.RmFormQa1We)
                .FirstOrDefaultAsync(m => m.Fqa1hPkRefNo == pkRefNo);
            return result;
        }

        #region Equipment
        public async Task<int> GetFilteredEqpRecordCount(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id)
        {
            var query = (from x in _context.RmFormQa1EqVh.Where(x => x.Fqa1evFqa1hPkRefNo == id) select x);
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<RmFormQa1EqVh>> GetFilteredEqpRecordList(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id)
        {
            List<RmFormQa1EqVh> result = new List<RmFormQa1EqVh>();
            var query = (from x in _context.RmFormQa1EqVh.Where(x => x.Fqa1evFqa1hPkRefNo == id )    select x);

            result = await query.OrderByDescending(s => s.Fqa1evPkRefNo)
                                .Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            return result;
        }

        public async Task<RmFormQa1EqVh> GetEquipDetails(int pkRefNo)
        {
            var result = await _context.RmFormQa1EqVh.FirstOrDefaultAsync(m => m.Fqa1evFqa1hPkRefNo == pkRefNo);
            return result;
        }        
        #endregion

        #region Material
        public async Task<int> GetFilteredMatRecordCount(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id)
        {
            var query = (from x in _context.RmFormQa1Mat.Where(x => x.Fqa1mFqa1hPkRefNo == id) select x);
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<RmFormQa1Mat>> GetFilteredMatRecordList(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id)
        {
            List<RmFormQa1Mat> result = new List<RmFormQa1Mat>();
            var query = (from x in _context.RmFormQa1Mat.Where(x => x.Fqa1mFqa1hPkRefNo == id) select x);

            result = await query.OrderByDescending(s => s.Fqa1mPkRefNo)
                                .Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            return result;
        }

        public async Task<RmFormQa1Mat> GetMatDetails(int pkRefNo)
        {
            var result = await _context.RmFormQa1Mat.FirstOrDefaultAsync(m => m.Fqa1mFqa1hPkRefNo == pkRefNo);
            return result;
        }
        #endregion


        #region General 
        public async Task<int> GetFilteredGenRecordCount(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id)
        {
            var query = (from x in _context.RmFormQa1Gen.Where(x => x.Fqa1genFqa1hPkRefNo == id) select x);
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<RmFormQa1Gen>> GetFilteredGenRecordList(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id)
        {
            List<RmFormQa1Gen> result = new List<RmFormQa1Gen>();
            var query = (from x in _context.RmFormQa1Gen.Where(x => x.Fqa1genFqa1hPkRefNo == id) select x);

            result = await query.OrderByDescending(s => s.Fqa1genFqa1hPkRefNo)
                                .Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            return result;
        }

        public async Task<RmFormQa1Gen> GetGenDetails(int pkRefNo)
        {
            var result = await  _context.RmFormQa1Gen.FirstOrDefaultAsync(m => m.Fqa1genPkRefNo == pkRefNo);
            return result;

        }

        #endregion


        #region Labour
        public async Task<RmFormQa1Lab> GetLabourDetails(int pkRefNo)
        {
            var result = await _context.RmFormQa1Lab.FirstOrDefaultAsync(m => m.Fqa1lPkRefNo == pkRefNo);
            return result;
        }
        #endregion
    }


}
