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
    public class DivRmuSectionRepository : RepositoryBase<RmDivRmuSecMaster>, IDivRmuSectionRepository
    {
        public DivRmuSectionRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }

        public async Task<long> GetFilteredRecordCount(FilteredPagingDefinition<DivRmuSectionRequestDTO> filterOptions)
        {
            return await (from s in _context.RmDivRmuSecMaster where s.RdsmActiveYn == true select s).LongCountAsync();
        }

        public async Task<List<DivRmuSectionRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<DivRmuSectionRequestDTO> filterOptions)
        {
            var query = (from s in _context.RmDivRmuSecMaster where s.RdsmActiveYn == true select s);
            if (!string.IsNullOrEmpty(filterOptions.Filters.DivCode))
            {
                query = query.Where(s =>
                s.RdsmDivCode.Contains(filterOptions.Filters.DivCode)
                || s.RdsmDivision.Contains(filterOptions.Filters.DivCode));
            }

            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 0)
                { query = query.OrderByDescending(s => s.RdsmPkRefNo); }
                if (filterOptions.ColumnIndex == 1) { query = query.OrderBy(s => s.RdsmDivCode); }
                if (filterOptions.ColumnIndex == 2) { query = query.OrderBy(s => s.RdsmDivision); }
                if (filterOptions.ColumnIndex == 3) { query = query.OrderBy(s => s.RdsmRmuCode); }
                if (filterOptions.ColumnIndex == 4) { query = query.OrderBy(s => s.RdsmRmuName); }
                if (filterOptions.ColumnIndex == 5) { query = query.OrderBy(s => s.RdsmSectionCode); }
                if (filterOptions.ColumnIndex == 6) { query = query.OrderBy(s => s.RdsmSectionName); }
            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 0)
                { query = query.OrderByDescending(s => s.RdsmPkRefNo); }
                if (filterOptions.ColumnIndex == 1) { query = query.OrderBy(s => s.RdsmDivCode); }
                if (filterOptions.ColumnIndex == 2) { query = query.OrderBy(s => s.RdsmDivision); }
                if (filterOptions.ColumnIndex == 3) { query = query.OrderBy(s => s.RdsmRmuCode); }
                if (filterOptions.ColumnIndex == 4) { query = query.OrderBy(s => s.RdsmRmuName); }
                if (filterOptions.ColumnIndex == 5) { query = query.OrderBy(s => s.RdsmSectionCode); }
                if (filterOptions.ColumnIndex == 6) { query = query.OrderBy(s => s.RdsmSectionName); }
            }
            var lst = await query.Skip(filterOptions.StartPageNo).Take(filterOptions.RecordsPerPage).ToListAsync();
            return lst.Select(s => new DivRmuSectionRequestDTO
            {
                RefNo = s.RdsmPkRefNo,
                DivCode = s.RdsmDivCode,
                Division = s.RdsmDivision,
                RmuCode = s.RdsmRmuCode,
                RmuName = s.RdsmRmuName,
                SectionCode = s.RdsmSectionCode,
                SectionName = s.RdsmSectionName,
            }).ToList();
        }
    }
}
