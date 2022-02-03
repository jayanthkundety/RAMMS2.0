using System;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RAMMS.Repository
{
    public class DivisonRepository : RepositoryBase<RmDivisionMaster>, IDivisonRepository
    {
        public DivisonRepository(RAMMSContext context) : base(context) { _context = context ?? throw new ArgumentNullException(nameof(context)); }
        public async Task<long> GetFilteredRecordCount(FilteredPagingDefinition<DivisionRequestDTO> filterOptions)
        {
            return await (from s in _context.RmDivisionMaster where s.DivIsActive == true select s).LongCountAsync();
        }

       

        public async Task<List<DivisionRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<DivisionRequestDTO> filterOptions)
        {
            var query = (from s in _context.RmDivisionMaster where s.DivIsActive == true select s);
            if (!string.IsNullOrEmpty(filterOptions.Filters.Code))
            {
                query = query.Where(s =>
                s.DivCode.Contains(filterOptions.Filters.Code)
                || s.DivName.Contains(filterOptions.Filters.Name));
            }

            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 0)
                { query = query.OrderByDescending(s => s.DivPkRefNo); }
                if (filterOptions.ColumnIndex == 1) { query = query.OrderBy(s => s.DivCode); }
                if (filterOptions.ColumnIndex == 2) { query = query.OrderBy(s => s.DivName); }
                if (filterOptions.ColumnIndex == 3) { query = query.OrderBy(s => s.DivIsActive); }
            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 0)
                { query = query.OrderByDescending(s => s.DivPkRefNo); }
                if (filterOptions.ColumnIndex == 1) { query = query.OrderByDescending(s => s.DivCode); }
                if (filterOptions.ColumnIndex == 2) { query = query.OrderByDescending(s => s.DivName); }
                if (filterOptions.ColumnIndex == 3) { query = query.OrderByDescending(s => s.DivIsActive); }
            }
            var lst = await query.Skip(filterOptions.StartPageNo).Take(filterOptions.RecordsPerPage).ToListAsync();
            return lst.Select(s => new DivisionRequestDTO
            {
                PkRefNo = s.DivPkRefNo,
                Code = s.DivCode,
                Name = s.DivName,
                Isactive = s.DivIsActive
            }).ToList();
        }

       
    }
}
