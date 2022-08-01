using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository
{
    //public class FormB9Repository : RepositoryBase<RmB9DesiredService>, IFormB9Repository
    //{
    //    public FormB9Repository(RAMMSContext context) : base(context)
    //    {
    //        _context = context;
    //    }



    //    public async Task<long> GetFilteredRecordCount(FilteredPagingDefinition<FormB9SearchGridDTO> filterOptions)
    //    {

    //        var query = (from s in _context.RmFormB9Hdr
    //                     join d in _context.RmRoadMaster on s.Ff1hRdCode equals d.RdmRdCode
    //                     from a in _context.RmAllassetInventory.Where(a => a.AiPkRefNo == 0).DefaultIfEmpty()
    //                     where s.Ff1hActiveYn == true
    //                     select new { s, d, a });


    //        var search = filterOptions.Filters;
    //        if (search.SecCode.HasValue)
    //        {
    //            query = query.Where(s => s.d.RdmSecCode == search.SecCode);
    //        }
    //        if (!string.IsNullOrEmpty(search.AssertType))
    //        {
    //            query = (from s in _context.RmFormB9Hdr
    //                     join dtl in _context.RmFormB9Dtl on s.Ff1hPkRefNo equals dtl.Ff1dFf1hPkRefNo
    //                     join a in _context.RmAllassetInventory on dtl.Ff1dAssetId equals a.AiAssetId
    //                     join d in _context.RmRoadMaster on s.Ff1hRdCode equals d.RdmRdCode
    //                     select new { s, d, a });
    //            query = query.Where(s => s.a.AiStrucCode == search.AssertType);
    //        }
    //        if (!string.IsNullOrEmpty(search.RmuCode))
    //        {
    //            query = query.Where(s => s.d.RdmRmuCode == search.RmuCode);
    //        }
    //        if (!string.IsNullOrEmpty(search.RoadCode))
    //        {
    //            query = query.Where(s => s.s.Ff1hRdCode == search.RoadCode);
    //        }
    //        if (search.Year.HasValue)
    //        {
    //            query = query.Where(s => s.s.Ff1hInspectedYear == search.Year.Value);
    //        }

    //        if (search.FromYear.HasValue)
    //        {
    //            query = query.Where(s => s.s.Ff1hInspectedYear >= search.FromYear);
    //        }
    //        if (search.ToYear.HasValue)
    //        {
    //            query = query.Where(s => s.s.Ff1hInspectedYear <= search.ToYear);
    //        }
    //        if (!string.IsNullOrEmpty(search.SmartSearch))
    //        {
    //            if (int.TryParse(search.SmartSearch, out int seccode))
    //            {
    //                query = query.Where(s => s.d.RdmSecCode == seccode);
    //            }

    //            DateTime dt;
    //            if (DateTime.TryParseExact(search.SmartSearch, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
    //            {
    //                query = query.Where(s =>
    //                (s.s.Ff1hPkRefId.Contains(search.SmartSearch) ||
    //                s.s.Ff1hRdCode.Contains(search.SmartSearch) ||
    //                s.d.RdmRmuCode.Contains(search.SmartSearch) ||
    //                s.d.RdmRmuName.Contains(search.SmartSearch) ||
    //                s.d.RdmDivCode.Contains(search.SmartSearch) ||
    //                s.s.Ff1hRdName.Contains(search.SmartSearch)) ||
    //                s.d.RdmSecName.Contains(search.SmartSearch) ||
    //                s.s.Ff1hDist.Contains(search.SmartSearch) ||
    //                //roads.Contains(s.d.RdmRdCode) ||
    //                s.s.Ff1hInspectedName.Contains(search.SmartSearch) ||

    //                (s.s.Ff1hInspectedDate.HasValue ? (s.s.Ff1hInspectedDate.Value.Year == dt.Year && s.s.Ff1hInspectedDate.Value.Month == dt.Month && s.s.Ff1hInspectedDate.Value.Day == dt.Day) : true) && s.s.Ff1hInspectedDate != null);
    //            }
    //            else
    //            {
    //                query = query.Where(s =>
    //                 (s.s.Ff1hPkRefId.Contains(search.SmartSearch) ||
    //                 s.s.Ff1hRdCode.Contains(search.SmartSearch) ||
    //                 s.d.RdmRmuCode.Contains(search.SmartSearch) ||
    //                 s.d.RdmRmuName.Contains(search.SmartSearch) ||
    //                 s.d.RdmDivCode.Contains(search.SmartSearch) ||
    //                 s.s.Ff1hRdName.Contains(search.SmartSearch)) ||
    //                 s.d.RdmSecName.Contains(search.SmartSearch) ||
    //                 s.s.Ff1hDist.Contains(search.SmartSearch) ||
    //                 //roads.Contains(s.d.RdmRdCode) ||
    //                 s.s.Ff1hInspectedName.Contains(search.SmartSearch)

    //                 );
    //            }
    //        }

    //        return await query.CountAsync();
    //    }

    //    public async Task<List<FormB9ResponseDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormB9SearchGridDTO> filterOptions)
    //    {


    //        var query = (from x in _context.RmB9DesiredService
                         
    //                     select new { x });



    //        query = query.OrderByDescending(x => x.x.B9dsPkRefNo);
    //        var search = filterOptions.Filters;
    //        if (search.Year.HasValue)
    //        {
    //            query = query.Where(s => s.x.B9dsRevisionYear == search.Year);
    //        }

    //        if (!string.IsNullOrEmpty(filterOptions.Filters.FromDate) && string.IsNullOrEmpty(filterOptions.Filters.ToDate))
    //        {
    //            DateTime dt;
    //            if (DateTime.TryParseExact(filterOptions.Filters.FromDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
    //            {
    //                query = query.Where(x => x.x.B9dsRevisionDate.HasValue ? (x.x.B9dsRevisionDate.Value.Year == dt.Year && x.x.B9dsRevisionDate.Value.Month == dt.Month && x.x.B9dsRevisionDate.Value.Day == dt.Day) : false);
    //            }
    //        }

    //        if (string.IsNullOrEmpty(filterOptions.Filters.FromDate) && !string.IsNullOrEmpty(filterOptions.Filters.ToDate))
    //        {
    //            DateTime dt;
    //            if (DateTime.TryParseExact(filterOptions.Filters.FromDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
    //            {
    //                query = query.Where(x => x.x.B9dsRevisionDate.HasValue ? (x.x.B9dsRevisionDate.Value.Year == dt.Year && x.x.B9dsRevisionDate.Value.Month == dt.Month && x.x.B9dsRevisionDate.Value.Day == dt.Day) : false);
    //            }
    //        }

    //        if (!string.IsNullOrEmpty(search.SmartSearch))
    //        {
    //            if (int.TryParse(search.SmartSearch, out int Year))
    //            {
    //                query = query.Where(s => s.x.B9dsRevisionYear == Year);
    //            }
    //            DateTime dt;
    //            if (DateTime.TryParseExact(search.SmartSearch, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
    //            {
    //                query = query.Where(s =>
    //                (s.x.B9dsRevisionDate.HasValue ? (s.x.B9dsRevisionDate.Value.Year == dt.Year && s.x.B9dsRevisionDate.Value.Month == dt.Month && s.x.B9dsRevisionDate.Value.Day == dt.Day) : true) && s.x.B9dsRevisionDate != null);
    //            }
                

    //        }

    //        if (filterOptions.sortOrder == SortOrder.Ascending)
    //        {
    //            if (filterOptions.ColumnIndex == 2)
    //                query = query.OrderBy(s => s.d.RdmRdCdSort);
    //            if (filterOptions.ColumnIndex == 3)
    //                query = query.OrderBy(s => s.s.Ff1hInspectedDate);
    //            if (filterOptions.ColumnIndex == 4)
    //                query = query.OrderBy(s => s.s.Ff1hInspectedName);
    //            if (filterOptions.ColumnIndex == 5)
    //                query = query.OrderBy(s => s.s.Ff1hDivCode);
    //            if (filterOptions.ColumnIndex == 6)
    //                query = query.OrderBy(s => s.s.Ff1hDist);
    //            if (filterOptions.ColumnIndex == 7)
    //                query = query.OrderBy(s => s.d.RdmRmuCode);
    //            if (filterOptions.ColumnIndex == 8)
    //                query = query.OrderBy(s => s.d.RdmRmuName);
    //            if (filterOptions.ColumnIndex == 9)
    //                query = query.OrderBy(s => s.d.RdmSecCode);
    //            if (filterOptions.ColumnIndex == 10)
    //                query = query.OrderBy(s => s.d.RdmSecName);
    //            if (filterOptions.ColumnIndex == 11)
    //                query = query.OrderBy(s => s.d.RdmRdCdSort);

    //        }
    //        else if (filterOptions.sortOrder == SortOrder.Descending)
    //        {
    //            if (filterOptions.ColumnIndex == 2)
    //                query = query.OrderByDescending(s => s.d.RdmRdCdSort);
    //            if (filterOptions.ColumnIndex == 3)
    //                query = query.OrderByDescending(s => s.s.Ff1hInspectedDate);
    //            if (filterOptions.ColumnIndex == 4)
    //                query = query.OrderByDescending(s => s.s.Ff1hInspectedName);
    //            if (filterOptions.ColumnIndex == 5)
    //                query = query.OrderByDescending(s => s.s.Ff1hDivCode);
    //            if (filterOptions.ColumnIndex == 6)
    //                query = query.OrderByDescending(s => s.s.Ff1hDist);
    //            if (filterOptions.ColumnIndex == 7)
    //                query = query.OrderByDescending(s => s.d.RdmRmuCode);
    //            if (filterOptions.ColumnIndex == 8)
    //                query = query.OrderByDescending(s => s.d.RdmRmuName);
    //            if (filterOptions.ColumnIndex == 9)
    //                query = query.OrderByDescending(s => s.d.RdmSecCode);
    //            if (filterOptions.ColumnIndex == 10)
    //                query = query.OrderByDescending(s => s.d.RdmSecName);
    //            if (filterOptions.ColumnIndex == 11)
    //                query = query.OrderByDescending(s => s.d.RdmRdCdSort);

    //        }

    //        var list = await query.ToListAsync();
    //        var Dlist = list.GroupBy(x => x.s.Ff1hPkRefNo).Select(g => g.First()).AsEnumerable();
    //        var Flist = Dlist.Skip(filterOptions.StartPageNo).Take(filterOptions.RecordsPerPage).ToList();


    //        return Flist.Select(s => new FormB9HeaderRequestDTO
    //        {
    //            ActiveYn = s.s.Ff1hActiveYn,
    //            CrBy = s.s.Ff1hCrBy,
    //            CrDt = s.s.Ff1hCrDt,
    //            CrewLeaderName = s.s.Ff1hCrewName,
    //            Dist = s.s.Ff1hDist,
    //            DivCode = s.d.RdmDivCode,
    //            DtInspBy = s.s.Ff1hInspectedDate,
    //            DtOfInsp = s.s.Ff1hInspectedDate,
    //            FormRefId = s.s.Ff1hPkRefId,
    //            ModBy = s.s.Ff1hModBy,
    //            PkRefNo = s.s.Ff1hPkRefNo,
    //            RoadCode = s.s.Ff1hRdCode,
    //            RoadName = s.s.Ff1hRdName,
    //            RmuCode = s.d.RdmRmuCode,
    //            RmuName = s.d.RdmRmuName,
    //            SectionCode = s.d.RdmSecCode,
    //            SectionName = s.d.RdmSecName,
    //            RoadLength = s.s.Ff1hRoadLength,
    //            SignpathInspBy = "",// s.s.FgrihSignpathInspBy,
    //            SubmitSts = s.s.Ff1hSubmitSts,
    //            UserDesignationInspBy = "",//s.s.in,
    //            UserIdInspBy = s.s.Ff1hInspectedBy,
    //            YearOfInsp = s.s.Ff1hInspectedYear,
    //            UserNameInspBy = s.s.Ff1hInspectedName,
    //            Status = s.s.Ff1hStatus

    //        }).ToList();

           
    //    }


    //    public async Task<List<FormB9DtlGridDTO>> GetFormB9DtlGridList(FilteredPagingDefinition<FormB9DtlResponseDTO> filterOptions)
    //    {
    //        List<RmFormB9Dtl> result = new List<RmFormB9Dtl>();

    //        var query = from x in _context.RmFormB9Dtl
    //                    join a in _context.RmAllassetInventory on x.Ff1dAssetId equals a.AiAssetId
    //                    where x.Ff1dFf1hPkRefNo == filterOptions.Filters.Ff1hPkRefNo
    //                    orderby x.Ff1dPkRefNo descending
    //                    select new { x, a };


    //        if (filterOptions.sortOrder == SortOrder.Ascending)
    //        {
    //            if (filterOptions.ColumnIndex == 1)
    //                query = query.OrderBy(x => x.x.Ff1dPkRefNo);
    //            if (filterOptions.ColumnIndex == 2)
    //                query = query.OrderBy(x => x.x.Ff1dLocCh);
    //            if (filterOptions.ColumnIndex == 3)
    //                query = query.OrderBy(x => x.x.Ff1dCode);
    //            if (filterOptions.ColumnIndex == 4)
    //                query = query.OrderBy(x => x.x.Ff1dOverallCondition);
    //            if (filterOptions.ColumnIndex == 5)
    //                query = query.OrderBy(x => x.a.AiBound);
    //            if (filterOptions.ColumnIndex == 6)
    //                query = query.OrderBy(x => x.a.AiWidth);
    //            if (filterOptions.ColumnIndex == 7)
    //                query = query.OrderBy(x => x.a.AiHeight);
    //            if (filterOptions.ColumnIndex == 8)
    //                query = query.OrderBy(x => x.x.Ff1dDescription);

    //        }
    //        else if (filterOptions.sortOrder == SortOrder.Descending)
    //        {
    //            if (filterOptions.ColumnIndex == 1)
    //                query = query.OrderByDescending(x => x.x.Ff1dPkRefNo);
    //            if (filterOptions.ColumnIndex == 2)
    //                query = query.OrderByDescending(x => x.x.Ff1dLocCh);
    //            if (filterOptions.ColumnIndex == 3)
    //                query = query.OrderByDescending(x => x.x.Ff1dCode);
    //            if (filterOptions.ColumnIndex == 4)
    //                query = query.OrderByDescending(x => x.x.Ff1dOverallCondition);
    //            if (filterOptions.ColumnIndex == 5)
    //                query = query.OrderByDescending(x => x.a.AiBound);
    //            if (filterOptions.ColumnIndex == 6)
    //                query = query.OrderByDescending(x => x.a.AiWidth);
    //            if (filterOptions.ColumnIndex == 7)
    //                query = query.OrderByDescending(x => x.a.AiHeight);
    //            if (filterOptions.ColumnIndex == 8)
    //                query = query.OrderByDescending(x => x.x.Ff1dDescription);

    //        }

    //        var list = await query.ToListAsync();


    //        return list.Select(s => new FormB9DtlGridDTO
    //        {

    //            AssetId = s.a.AiAssetId,
    //            Ch = s.x.Ff1dLocCh + "+" + s.x.Ff1dLocChDeci,
    //            OverallCondition = Convert.ToInt32(s.x.Ff1dOverallCondition),
    //            Description = s.x.Ff1dDescription,
    //            FrmCh = s.x.Ff1dLocCh,
    //            FrmChDec = s.x.Ff1dLocChDeci,
    //            Height = s.a.AiHeight,
    //            PkRefNo = s.x.Ff1dPkRefNo,
    //            StructureCode = s.a.AiStrucCode,
    //            Length = s.a.AiLength,
    //            Width = s.a.AiWidth,
    //            BottomWidth = s.a.AiBotWidth,
    //            Tier = s.a.AiTier

    //        }).ToList();


    //    }


    //    public List<RmAllassetInventory> GetAssetDetails(FormB9ResponseDTO FormB9)
    //    {


    //        return (from r in _context.RmAllassetInventory.Where(s => s.AiStrucCode == "Y" && s.AiRmuCode == FormB9.RmuCode && s.AiDivCode == FormB9.DivCode && s.AiRdCode == FormB9.RdCode) select r).ToList();


    //    }

       

    //    public async Task<RmFormB9Hdr> SaveFormB9(RmFormB9Hdr FormB9)
    //    {
    //        try
    //        {

    //            var search = (from r1 in _context.RmFormR1Hdr
    //                          where r1.Fr1hAiDivCode == FormB9.Ff1hDivCode && r1.Fr1hAiRmuCode == FormB9.Ff1hRmuCode && r1.Fr1hYearOfInsp == FormB9.Ff1hInspectedYear && r1.Fr1hAiRdCode == FormB9.Ff1hRdCode && r1.Fr1hActiveYn == true
    //                          select r1);

    //            if (search.Count() > 0)
    //            {
    //                _context.RmFormB9Hdr.Add(FormB9);
    //                _context.SaveChanges();

    //                var res = (from r1 in _context.RmFormR1Hdr
    //                           join a in _context.RmAllassetInventory on r1.Fr1hAidPkRefNo equals a.AiPkRefNo
    //                           where r1.Fr1hAiDivCode == FormB9.Ff1hDivCode && r1.Fr1hAiRmuCode == FormB9.Ff1hRmuCode && r1.Fr1hYearOfInsp == FormB9.Ff1hInspectedYear && r1.Fr1hAiRdCode == FormB9.Ff1hRdCode && r1.Fr1hActiveYn == true
    //                           select new RmFormB9Dtl
    //                           {
    //                               Ff1dFf1hPkRefNo = FormB9.Ff1hPkRefNo,
    //                               Ff1dAssetId = r1.Fr1hAssetId,
    //                               Ff1dR1hPkRefNo = r1.Fr1hPkRefNo,
    //                               Ff1dTier = Convert.ToInt32(a.AiTier),
    //                               Ff1dCode = a.AiStrucCode,
    //                               Ff1dTotalLength = Convert.ToDecimal(a.AiLength),
    //                               Ff1dHeight = Convert.ToDecimal(a.AiHeight),
    //                               Ff1dTopWidth = Convert.ToDecimal(a.AiWidth),
    //                               Ff1dBottomWidth  = Convert.ToDecimal(a.AiBotWidth),
    //                               Ff1dOverallCondition = r1.Fr1hCondRating,
    //                               Ff1dLocCh = a.AiLocChKm,
    //                               Ff1dLocChDeci = a.AiLocChM == "" ? 0 : Convert.ToInt32(a.AiLocChM)

    //                           }).ToList();

    //                foreach (var item in res)
    //                {
    //                    _context.RmFormB9Dtl.Add(item);
    //                    _context.SaveChanges();
    //                }

    //            }

    //            return FormB9;
    //        }
    //        catch (Exception ex)
    //        {
    //            return null;
    //        }
    //    }
    //    public int? DeleteFormB9(int id)
    //    {
    //        try
    //        {
    //            IList<RmFormB9Dtl> child = (from r in _context.RmFormB9Dtl where r.Ff1dFf1hPkRefNo == id select r).ToList();
    //            foreach (var item in child)
    //            {
    //                _context.Remove(item);
    //                _context.SaveChanges();
    //            }

    //            var res = _context.Set<RmFormB9Hdr>().FindAsync(id);
    //            res.Result.Ff1hActiveYn = false;
    //            _context.Set<RmFormB9Hdr>().Attach(res.Result);
    //            _context.Entry<RmFormB9Hdr>(res.Result).State = EntityState.Modified;
    //            _context.SaveChanges();
    //            return 1;

    //        }
    //        catch (Exception ex)
    //        {
    //            return 500;
    //        }
    //    }


    //    public int? DeleteFormB9Dtl(int Id)
    //    {
    //        var res = (from r in _context.RmFormB9Dtl where r.Ff1dPkRefNo == Id select r).SingleOrDefault();
    //        if (res != null)
    //        {
    //            _context.Entry(res).State = EntityState.Deleted;
    //            _context.SaveChanges();
    //            return 1;
    //        }
    //        return 0;
    //    }

    //    public int? SaveFormB9Dtl(RmFormB9Dtl FormB9Dtl)
    //    {
    //        try
    //        {


    //            _context.Entry<RmFormB9Dtl>(FormB9Dtl).State = FormB9Dtl.Ff1dPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
    //            _context.SaveChanges();

    //            return FormB9Dtl.Ff1dPkRefNo;
    //        }
    //        catch (Exception ex)
    //        {
    //            return 500;

    //        }
    //    }

    //    public int? UpdateFormB9Dtl(RmFormB9Dtl FormB9Dtl)
    //    {
    //        try
    //        {
    //            _context.Set<RmFormB9Dtl>().Attach(FormB9Dtl);
    //            _context.Entry<RmFormB9Dtl>(FormB9Dtl).State = EntityState.Modified;
    //            _context.SaveChanges();
    //            return 1;
    //        }
    //        catch (Exception ex)
    //        {
    //            return 500;
    //        }
    //    }


    //    public async Task<FORMB9Rpt> GetReportData(int headerid)
    //    {
    //        FORMB9Rpt result = (from s in _context.RmFormB9Hdr
    //                            where s.Ff1hPkRefNo == headerid && s.Ff1hActiveYn == true
    //                            select new FORMB9Rpt
    //                            {
    //                                CrewLeader = s.Ff1hCrewName,
    //                                District = s.Ff1hDist,
    //                                InspectedByDesignation = s.Ff1hInspectedDesg,
    //                                InspectedByName = s.Ff1hInspectedName,
    //                                InspectedDate = s.Ff1hInspectedDate,
    //                                Division = s.Ff1hDivCode,
    //                                RMU = (from r in _context.RmDdLookup where r.DdlType == "RMU" && r.DdlTypeCode == s.Ff1hRmuCode select r.DdlTypeDesc).FirstOrDefault(),
    //                                RoadCode = s.Ff1hRdCode,
    //                                RoadName = s.Ff1hRdName,
    //                                RoadLength = s.Ff1hRoadLength
    //                            }).FirstOrDefault();


    //        result.Details = (from d in _context.RmFormB9Dtl
    //                          join a in _context.RmAllassetInventory on d.Ff1dAssetId equals a.AiAssetId
    //                          where d.Ff1dFf1hPkRefNo == headerid
    //                          orderby d.Ff1dPkRefNo descending
    //                          select new FORMB9RptDetail
    //                          {
                                  
    //                              Descriptions = d.Ff1dDescription,
    //                              LocationChKm = a.AiLocChKm,
    //                              LocationChM = a.AiLocChM == "" ? 0 : Convert.ToInt32(a.AiLocChM),
    //                              Length = Convert.ToDecimal(a.AiLength),
    //                              Width = Convert.ToDecimal(a.AiWidth),
    //                              BottomWidth = Convert.ToDecimal(a.AiBotWidth),
    //                              Height = Convert.ToDecimal(a.AiHeight),
    //                              Condition = d.Ff1dOverallCondition,
    //                              Tier = Convert.ToInt32(a.AiTier),
    //                              StructCode = a.AiStrucCode
    //                          }).ToArray();
    //        return result;

    //    }


    //}
}
