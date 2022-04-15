using Microsoft.EntityFrameworkCore;
using RAMMS.Common.Extensions;
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

    }
}
