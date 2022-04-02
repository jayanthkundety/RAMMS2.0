using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RAMMS.Repository
{
  public class SectionRepository : 
    RepositoryBase<RmDivRmuSecMaster>,
    ISectionRepository,
    IRepositoryBase<RmDivRmuSecMaster>
  {
    public SectionRepository(RAMMSContext context)
      : base(context)
    {
      this._context = context ?? throw new ArgumentNullException(nameof (context));
    }

    public async Task<long> GetFilteredRecordCount(
      FilteredPagingDefinition<SectionRequestDTO> filterOptions)
    {
      IQueryable<RmDivRmuSecMaster> source = this._context.RmDivRmuSecMaster.Where<RmDivRmuSecMaster>((Expression<Func<RmDivRmuSecMaster, bool>>) (s => s.RdsmActiveYn == (bool?) true));
      if (!string.IsNullOrEmpty(filterOptions.Filters.DivCode))
        source = source.Where<RmDivRmuSecMaster>((Expression<Func<RmDivRmuSecMaster, bool>>) (s => s.RdsmDivCode.Contains(filterOptions.Filters.DivCode) || s.RdsmDivision.Contains(filterOptions.Filters.DivCode) || s.RdsmRmuCode.Contains(filterOptions.Filters.DivCode) || s.RdsmRmuName.Contains(filterOptions.Filters.DivCode) || s.RdsmSectionCode.Contains(filterOptions.Filters.DivCode) || s.RdsmSectionName.Contains(filterOptions.Filters.DivCode)));
      return await source.LongCountAsync<RmDivRmuSecMaster>();
    }

    public async Task<List<SectionRequestDTO>> GetFilteredRecordList(
      FilteredPagingDefinition<SectionRequestDTO> filterOptions)
    {
      IQueryable<RmDivRmuSecMaster> source = this._context.RmDivRmuSecMaster.Where<RmDivRmuSecMaster>((Expression<Func<RmDivRmuSecMaster, bool>>) (s => s.RdsmActiveYn == (bool?) true));
      if (!string.IsNullOrEmpty(filterOptions.Filters.DivCode))
        source = source.Where<RmDivRmuSecMaster>((Expression<Func<RmDivRmuSecMaster, bool>>) (s => s.RdsmDivCode.Contains(filterOptions.Filters.DivCode) || s.RdsmDivision.Contains(filterOptions.Filters.DivCode) || s.RdsmRmuCode.Contains(filterOptions.Filters.DivCode) || s.RdsmRmuName.Contains(filterOptions.Filters.DivCode) || s.RdsmSectionCode.Contains(filterOptions.Filters.DivCode) || s.RdsmSectionName.Contains(filterOptions.Filters.DivCode)));
      if (filterOptions.sortOrder == SortOrder.Ascending)
      {
        if (filterOptions.ColumnIndex == 0)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderByDescending<RmDivRmuSecMaster, int>((Expression<Func<RmDivRmuSecMaster, int>>) (s => s.RdsmPkRefNo));
        if (filterOptions.ColumnIndex == 1)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderBy<RmDivRmuSecMaster, string>((Expression<Func<RmDivRmuSecMaster, string>>) (s => s.RdsmDivCode));
        if (filterOptions.ColumnIndex == 2)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderBy<RmDivRmuSecMaster, string>((Expression<Func<RmDivRmuSecMaster, string>>) (s => s.RdsmDivision));
        if (filterOptions.ColumnIndex == 3)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderBy<RmDivRmuSecMaster, string>((Expression<Func<RmDivRmuSecMaster, string>>) (s => s.RdsmRmuCode));
        if (filterOptions.ColumnIndex == 4)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderBy<RmDivRmuSecMaster, string>((Expression<Func<RmDivRmuSecMaster, string>>) (s => s.RdsmRmuName));
        if (filterOptions.ColumnIndex == 5)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderBy<RmDivRmuSecMaster, string>((Expression<Func<RmDivRmuSecMaster, string>>) (s => s.RdsmSectionCode));
        if (filterOptions.ColumnIndex == 6)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderBy<RmDivRmuSecMaster, string>((Expression<Func<RmDivRmuSecMaster, string>>) (s => s.RdsmSectionName));
        if (filterOptions.ColumnIndex == 7)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderBy<RmDivRmuSecMaster, string>((Expression<Func<RmDivRmuSecMaster, string>>) (s => s.RdsmModBy));
        if (filterOptions.ColumnIndex == 8)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderBy<RmDivRmuSecMaster, DateTime?>((Expression<Func<RmDivRmuSecMaster, DateTime?>>) (s => s.RdsmModDt));
        if (filterOptions.ColumnIndex == 9)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderBy<RmDivRmuSecMaster, string>((Expression<Func<RmDivRmuSecMaster, string>>) (s => s.RdsmCrBy));
        if (filterOptions.ColumnIndex == 10)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderBy<RmDivRmuSecMaster, DateTime?>((Expression<Func<RmDivRmuSecMaster, DateTime?>>) (s => s.RdsmCrDt));
        if (filterOptions.ColumnIndex == 11)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderBy<RmDivRmuSecMaster, bool?>((Expression<Func<RmDivRmuSecMaster, bool?>>) (s => s.RdsmActiveYn));
      }
      else if (filterOptions.sortOrder == SortOrder.Descending)
      {
        if (filterOptions.ColumnIndex == 0)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderByDescending<RmDivRmuSecMaster, int>((Expression<Func<RmDivRmuSecMaster, int>>) (s => s.RdsmPkRefNo));
        if (filterOptions.ColumnIndex == 1)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderByDescending<RmDivRmuSecMaster, string>((Expression<Func<RmDivRmuSecMaster, string>>) (s => s.RdsmDivCode));
        if (filterOptions.ColumnIndex == 2)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderByDescending<RmDivRmuSecMaster, string>((Expression<Func<RmDivRmuSecMaster, string>>) (s => s.RdsmDivision));
        if (filterOptions.ColumnIndex == 3)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderByDescending<RmDivRmuSecMaster, string>((Expression<Func<RmDivRmuSecMaster, string>>) (s => s.RdsmRmuCode));
        if (filterOptions.ColumnIndex == 4)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderByDescending<RmDivRmuSecMaster, string>((Expression<Func<RmDivRmuSecMaster, string>>) (s => s.RdsmRmuName));
        if (filterOptions.ColumnIndex == 5)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderByDescending<RmDivRmuSecMaster, string>((Expression<Func<RmDivRmuSecMaster, string>>) (s => s.RdsmSectionCode));
        if (filterOptions.ColumnIndex == 6)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderByDescending<RmDivRmuSecMaster, string>((Expression<Func<RmDivRmuSecMaster, string>>) (s => s.RdsmSectionName));
        if (filterOptions.ColumnIndex == 7)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderByDescending<RmDivRmuSecMaster, string>((Expression<Func<RmDivRmuSecMaster, string>>) (s => s.RdsmModBy));
        if (filterOptions.ColumnIndex == 8)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderByDescending<RmDivRmuSecMaster, DateTime?>((Expression<Func<RmDivRmuSecMaster, DateTime?>>) (s => s.RdsmModDt));
        if (filterOptions.ColumnIndex == 9)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderByDescending<RmDivRmuSecMaster, string>((Expression<Func<RmDivRmuSecMaster, string>>) (s => s.RdsmCrBy));
        if (filterOptions.ColumnIndex == 10)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderByDescending<RmDivRmuSecMaster, DateTime?>((Expression<Func<RmDivRmuSecMaster, DateTime?>>) (s => s.RdsmCrDt));
        if (filterOptions.ColumnIndex == 11)
          source = (IQueryable<RmDivRmuSecMaster>) source.OrderByDescending<RmDivRmuSecMaster, bool?>((Expression<Func<RmDivRmuSecMaster, bool?>>) (s => s.RdsmActiveYn));
      }
      return (await source.Skip<RmDivRmuSecMaster>(filterOptions.StartPageNo).Take<RmDivRmuSecMaster>(filterOptions.RecordsPerPage).ToListAsync<RmDivRmuSecMaster>()).Select<RmDivRmuSecMaster, SectionRequestDTO>((Func<RmDivRmuSecMaster, SectionRequestDTO>) (s => new SectionRequestDTO()
      {
        PkRefNo = s.RdsmPkRefNo,
        DivCode = s.RdsmDivCode,
        Division = s.RdsmDivision,
        RmuCode = s.RdsmRmuCode,
        RmuName = s.RdsmRmuName,
        SectionCode = s.RdsmSectionCode,
        SectionName = s.RdsmSectionName,
        ModBy = s.RdsmModBy,
        ModDt = s.RdsmModDt,
        CrBy = s.RdsmCrBy,
        CrDt = s.RdsmCrDt,
        ActiveYn = s.RdsmActiveYn
      })).ToList<SectionRequestDTO>();
    }
  }
}
