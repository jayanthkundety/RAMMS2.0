using System;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RAMMS.Repository
{
    public class RMURepository : RepositoryBase<RmRmuMaster>, IRMURepository
    {
        public RMURepository(RAMMSContext context) : base(context) { _context = context ?? throw new ArgumentNullException(nameof(context)); }
        public async Task<long> GetFilteredRecordCount(FilteredPagingDefinition<RMURequestDTO> filterOptions)
        {
            var query = (from s in _context.RmRmuMaster
                         join d in _context.RmDivisionMaster on s.DivCode equals d.DivCode
                         where d.DivIsActive == true && s.RmuIsActive == true
                         select s);
            if (!string.IsNullOrEmpty(filterOptions.Filters.Code))
            {
                query = query.Where(s =>
                s.DivCode.Contains(filterOptions.Filters.Code) ||
                s.RmuName.Contains(filterOptions.Filters.Code) ||
                s.RmuCode.Contains(filterOptions.Filters.Code));
            }
            return await query.LongCountAsync();
        }
        public async Task<List<RMURequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<RMURequestDTO> filterOptions)
        {
            var query = (from s in _context.RmRmuMaster
                         join d in _context.RmDivisionMaster on s.DivCode equals d.DivCode
                         where d.DivIsActive == true && s.RmuIsActive == true
                         select s);

            if (!string.IsNullOrEmpty(filterOptions.Filters.Code))
            {
                query = query.Where(s =>
                s.DivCode.Contains(filterOptions.Filters.Code) ||
                s.RmuName.Contains(filterOptions.Filters.Code) ||
                s.RmuCode.Contains(filterOptions.Filters.Code));
            }
            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 0) { query = query.OrderByDescending(s => s.RmuPkRefNo); }
                if (filterOptions.ColumnIndex == 1) { query = query.OrderBy(s => s.DivCode); }
                if (filterOptions.ColumnIndex == 2) { query = query.OrderBy(s => s.RmuCode); }
                if (filterOptions.ColumnIndex == 3) { query = query.OrderBy(s => s.RmuName); }
                if (filterOptions.ColumnIndex == 4) { query = query.OrderBy(s => s.RmuIsActive); }
            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 0) { query = query.OrderByDescending(s => s.RmuPkRefNo); }
                if (filterOptions.ColumnIndex == 1) { query = query.OrderByDescending(s => s.DivCode); }
                if (filterOptions.ColumnIndex == 2) { query = query.OrderByDescending(s => s.RmuCode); }
                if (filterOptions.ColumnIndex == 3) { query = query.OrderByDescending(s => s.RmuName); }
                if (filterOptions.ColumnIndex == 4) { query = query.OrderByDescending(s => s.RmuIsActive); }
            }
            var lst = await query.Skip(filterOptions.StartPageNo).Take(filterOptions.RecordsPerPage).ToListAsync(); return lst.Select(s => new RMURequestDTO
            {
                PkRefNo = s.RmuPkRefNo,
                DivCode = s.DivCode,
                Code = s.RmuCode,
                Name = s.RmuName,
                IsActive = s.RmuIsActive
            }).ToList();
        }
    }
}
