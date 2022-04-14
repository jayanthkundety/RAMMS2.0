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
                if (!string.IsNullOrEmpty(filterOptions.Filters.Road_Code))
                {
                    query = query.Where(x => x.Fqa1hRoadCode == filterOptions.Filters.Road_Code);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RoadName))
                {
                    query = query.Where(x => x.Fqa1hRoadName == filterOptions.Filters.RoadName);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
                {
                    query = query.Where(x => x.Fqa1hRmu == filterOptions.Filters.RMU);
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
