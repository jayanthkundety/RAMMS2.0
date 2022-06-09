using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.Models;
using RAMMS.DTO;
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
    public class FormF3Repository : RepositoryBase<RmFormF3Hdr>, IFormF3Repository
    {
        public FormF3Repository(RAMMSContext context) : base(context)
        {
            _context = context;
        }



        //public async Task<long> GetFilteredRecordCount(FilteredPagingDefinition<FormF2SearchGridDTO> filterOptions)
        //{
        //    var roads = (from a in _context.RmAllassetInventory
        //                 where filterOptions.Filters.AssertType != "" ? a.AiGrpType == filterOptions.Filters.AssertType : a.AiGrpType.Contains(filterOptions.Filters.SmartSearch) && a.AiActiveYn == true
        //                 select a.AiRdCode).ToArray();

        //    var query = (from s in _context.RmFormF3Hdr
        //                 join d in _context.RmRoadMaster on s.Ff3hRdCode equals d.RdmRdCode
        //                 where s.Ff3hActiveYn == true
        //                 select new { s, d });
        //    var search = filterOptions.Filters;
        //    if (search.SecCode.HasValue)
        //    {
        //        query = query.Where(s => s.d.RdmSecCode == search.SecCode);
        //    }
        //    if (!string.IsNullOrEmpty(search.AssertType))
        //    {

        //       // query = query.Where(s => s.s.RmFormF3Dtl.Any(x => x.aser == search.AssertType ));
        //    }
        //    if (!string.IsNullOrEmpty(search.RmuCode))
        //    {
        //        query = query.Where(s => s.d.RdmRmuCode == search.RmuCode);
        //    }
        //    if (!string.IsNullOrEmpty(search.RoadCode))
        //    {
        //        query = query.Where(s => s.s.Ff3hRdCode == search.RoadCode);
        //    }
        //    if (search.Year.HasValue)
        //    {
        //        query = query.Where(s => s.s.FgrihYearOfInsp == search.Year.Value);
        //    }

        //    if (search.FromYear.HasValue)
        //    {
        //        query = query.Where(s => s.s.FgrihYearOfInsp >= search.FromYear);
        //    }
        //    if (search.ToYear.HasValue)
        //    {
        //        query = query.Where(s => s.s.FgrihYearOfInsp <= search.ToYear);
        //    }

        //    if (search.FromChKM.HasValue || !string.IsNullOrEmpty(search.FromChM))
        //    {
        //        query = query.Where(s => s.s.RmFormF3Dtl.Any(x => Convert.ToDouble(x.RdmFrmCh.ToString() + '.' + x.RdmFrmChDeci) >= Convert.ToDouble(search.FromChKM.ToString() + '.' + search.FromChM)));
        //    }

        //    if (search.ToChKM.HasValue || !string.IsNullOrEmpty(search.ToChM))
        //    {
        //        query = query.Where(s => s.s.RmFormF3Dtl.Any(x => Convert.ToDouble(x.RdmToCh.ToString() + '.' + x.RdmToCh) <= Convert.ToDouble(search.ToChKM.ToString() + '.' + search.ToChM)));
        //    }

        //    if (!string.IsNullOrEmpty(search.Bound))
        //    {
        //        query = query.Where(s => s.s.RmFormF3Dtl.Any(x => x.Ff3dSide == search.Bound));
        //    }

        //    if (!string.IsNullOrEmpty(search.SmartSearch))
        //    {
        //        if (int.TryParse(search.SmartSearch, out int seccode))
        //        {
        //            query = query.Where(s => s.d.RdmSecCode == seccode);
        //        }

        //        DateTime dt;
        //        if (DateTime.TryParseExact(search.SmartSearch, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
        //        {
        //            query = query.Where(s =>
        //            (s.s.FgrihFormRefId.Contains(search.SmartSearch) ||
        //            s.s.Ff3hRdCode.Contains(search.SmartSearch) ||
        //            s.d.RdmRmuCode.Contains(search.SmartSearch) ||
        //            s.d.RdmRmuName.Contains(search.SmartSearch) ||
        //            s.d.RdmDivCode.Contains(search.SmartSearch) ||
        //            s.s.Ff3hRdName.Contains(search.SmartSearch)) ||
        //            s.d.RdmSecName.Contains(search.SmartSearch) ||
        //            s.s.Ff3hDist.Contains(search.SmartSearch) ||
        //            //roads.Contains(s.d.RdmRdCode) ||
        //            s.s.Ff3hInspectedName.Contains(search.SmartSearch) ||
        //            s.s.FgrihCrewLeaderName.Contains(search.SmartSearch) ||
        //            (s.s.Ff3hInspectedDate.HasValue ? (s.s.Ff3hInspectedDate.Value.Year == dt.Year && s.s.Ff3hInspectedDate.Value.Month == dt.Month && s.s.Ff3hInspectedDate.Value.Day == dt.Day) : true) && s.s.Ff3hInspectedDate != null);
        //        }
        //        else
        //        {
        //            query = query.Where(s =>
        //             (s.s.FgrihFormRefId.Contains(search.SmartSearch) ||
        //             s.s.Ff3hRdCode.Contains(search.SmartSearch) ||
        //             s.d.RdmRmuCode.Contains(search.SmartSearch) ||
        //             s.d.RdmRmuName.Contains(search.SmartSearch) ||
        //             s.d.RdmDivCode.Contains(search.SmartSearch) ||
        //             s.s.Ff3hRdName.Contains(search.SmartSearch)) ||
        //             s.d.RdmSecName.Contains(search.SmartSearch) ||
        //             s.s.Ff3hDist.Contains(search.SmartSearch) ||
        //             //roads.Contains(s.d.RdmRdCode) ||
        //             s.s.Ff3hInspectedName.Contains(search.SmartSearch) ||
        //             s.s.FgrihCrewLeaderName.Contains(search.SmartSearch)
        //             );
        //        }
        //    }

        //    return await query.CountAsync();
        //}

        //public async Task<List<FormF2HeaderRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormF2SearchGridDTO> filterOptions)
        //{

        //    var roads = (from a in _context.RmAllassetInventory
        //                 where filterOptions.Filters.AssertType != "" ? a.AiGrpType == filterOptions.Filters.AssertType : a.AiGrpType.Contains(filterOptions.Filters.SmartSearch) && a.AiActiveYn == true
        //                 select a.AiRdCode).ToArray();

        //    var query = (from s in _context.RmFormF2GrInsHdr
        //                 join d in _context.RmRoadMaster on s.FgrihRoadId equals d.RdmPkRefNo
        //                 where s.FgrihActiveYn == true
        //                 select new { s, d });
        //    query = query.OrderByDescending(x => x.s.FgrihModDt);
        //    var search = filterOptions.Filters;
        //    if (search.SecCode.HasValue)
        //    {
        //        query = query.Where(s => s.d.RdmSecCode == search.SecCode);
        //    }
        //    if (!string.IsNullOrEmpty(search.AssertType))
        //    {
        //        //query = query.Where(s => roads.Contains(s.d.RdmRdCode));
        //        //query = query.Where(s => s.s.RmFormF2GrInsDtl.Any(x => x.FgridGrCode == search.AssertType));
        //    }
        //    if (!string.IsNullOrEmpty(search.RmuCode))
        //    {
        //        query = query.Where(s => s.d.RdmRmuCode == search.RmuCode);
        //    }
        //    if (!string.IsNullOrEmpty(search.RoadCode))
        //    {
        //        query = query.Where(s => s.s.FgrihRoadCode == search.RoadCode);
        //    }
        //    if (search.Year.HasValue)
        //    {
        //        query = query.Where(s => s.s.FgrihYearOfInsp == search.Year.Value);
        //    }

        //    if (search.FromYear.HasValue)
        //    {
        //        query = query.Where(s => s.s.FgrihYearOfInsp >= search.FromYear);
        //    }
        //    if (search.ToYear.HasValue)
        //    {
        //        query = query.Where(s => s.s.FgrihYearOfInsp <= search.ToYear);
        //    }
        //    if (!string.IsNullOrEmpty(search.AssertType))
        //    {
        //        query = query.Where(s => s.s.RmFormF2GrInsDtl.Any(x => x.FgridGrCode == search.AssertType && x.FgridActiveYn == true));
        //    }

        //    if (search.FromChKM.HasValue || (!string.IsNullOrEmpty(search.FromChM)))
        //    {
        //        query = query.Where(s => s.s.RmFormF2GrInsDtl.Any(x => Convert.ToDouble(x.FgridStartingChKm.ToString() + '.' + x.FgridStartingChM) >= Convert.ToDouble(search.FromChKM.ToString() + '.' + search.FromChM)));
        //    }

        //    if (search.ToChKM.HasValue || (!string.IsNullOrEmpty(search.ToChM)))
        //    {
        //        query = query.Where(s => s.s.RmFormF2GrInsDtl.Any(x => Convert.ToDouble(x.FgridStartingChKm.ToString() + '.' + x.FgridStartingChM) <= Convert.ToDouble(search.ToChKM.ToString() + '.' + search.ToChM)));
        //    }

        //    if (!string.IsNullOrEmpty(search.Bound))
        //    {
        //        query = query.Where(s => s.s.RmFormF2GrInsDtl.Any(x => x.FgridRhsMLhs == search.Bound));
        //    }

        //    if (!string.IsNullOrEmpty(search.SmartSearch))
        //    {
        //        if (int.TryParse(search.SmartSearch, out int seccode))
        //        {
        //            query = query.Where(s => s.d.RdmSecCode == seccode);
        //        }
        //        DateTime dt;
        //        if (DateTime.TryParseExact(search.SmartSearch, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
        //        {
        //            query = query.Where(s =>
        //            (s.s.FgrihFormRefId.Contains(search.SmartSearch) ||
        //            s.s.FgrihRoadCode.Contains(search.SmartSearch) ||
        //            s.d.RdmRmuCode.Contains(search.SmartSearch) ||
        //            s.d.RdmRmuName.Contains(search.SmartSearch) ||
        //            s.d.RdmDivCode.Contains(search.SmartSearch) ||
        //            s.s.FgrihRoadName.Contains(search.SmartSearch)) ||
        //            s.d.RdmSecName.Contains(search.SmartSearch) ||
        //            s.s.FgrihDist.Contains(search.SmartSearch) ||
        //            //roads.Contains(s.d.RdmRdCode) ||
        //            s.s.FgrihUserNameInspBy.Contains(search.SmartSearch) ||
        //            s.s.FgrihCrewLeaderName.Contains(search.SmartSearch) ||
        //            (s.s.FgrihDtOfInsp.HasValue ? (s.s.FgrihDtOfInsp.Value.Year == dt.Year && s.s.FgrihDtOfInsp.Value.Month == dt.Month && s.s.FgrihDtOfInsp.Value.Day == dt.Day) : true) && s.s.FgrihDtOfInsp != null);
        //        }
        //        else
        //        {
        //            query = query.Where(s =>
        //             (s.s.FgrihFormRefId.Contains(search.SmartSearch) ||
        //             s.s.FgrihRoadCode.Contains(search.SmartSearch) ||
        //             s.d.RdmRmuCode.Contains(search.SmartSearch) ||
        //             s.d.RdmRmuName.Contains(search.SmartSearch) ||
        //             s.d.RdmDivCode.Contains(search.SmartSearch) ||
        //             s.s.FgrihRoadName.Contains(search.SmartSearch)) ||
        //             s.d.RdmSecName.Contains(search.SmartSearch) ||
        //             s.s.FgrihDist.Contains(search.SmartSearch) ||
        //             //roads.Contains(s.d.RdmRdCode) ||
        //             s.s.FgrihUserNameInspBy.Contains(search.SmartSearch) ||
        //             s.s.FgrihCrewLeaderName.Contains(search.SmartSearch)
        //             );
        //        }

        //    }

        //    if (filterOptions.sortOrder == SortOrder.Ascending)
        //    {
        //        if (filterOptions.ColumnIndex == 2)
        //            query = query.OrderBy(s => s.d.RdmRdCdSort);
        //        if (filterOptions.ColumnIndex == 3)
        //            query = query.OrderBy(s => s.s.FgrihDtOfInsp);
        //        if (filterOptions.ColumnIndex == 4)
        //            query = query.OrderBy(s => s.s.FgrihUserNameInspBy);
        //        if (filterOptions.ColumnIndex == 5)
        //            query = query.OrderBy(s => s.s.FgrihDivCode);
        //        if (filterOptions.ColumnIndex == 6)
        //            query = query.OrderBy(s => s.s.FgrihDist);
        //        if (filterOptions.ColumnIndex == 7)
        //            query = query.OrderBy(s => s.d.RdmRmuCode);
        //        if (filterOptions.ColumnIndex == 8)
        //            query = query.OrderBy(s => s.d.RdmRmuName);
        //        if (filterOptions.ColumnIndex == 9)
        //            query = query.OrderBy(s => s.d.RdmSecCode);
        //        if (filterOptions.ColumnIndex == 10)
        //            query = query.OrderBy(s => s.d.RdmSecName);
        //        if (filterOptions.ColumnIndex == 11)
        //            query = query.OrderBy(s => s.d.RdmRdCdSort);
        //        //if (filterOptions.ColumnIndex == 12)
        //        //    query = query.OrderBy(s => s.s.FgrihRoadName);
        //        //if (filterOptions.ColumnIndex == 13)
        //        //    query = query.OrderBy(s => s.s.FgrihYearOfInsp);
        //        if (filterOptions.ColumnIndex == 12)
        //            query = query.OrderBy(s => s.s.FgrihCrewLeaderName);
        //        //if (filterOptions.ColumnIndex == 0)
        //        //    query = query.OrderByDescending(s => s.s.FgrihPkRefNo);
        //    }
        //    else if (filterOptions.sortOrder == SortOrder.Descending)
        //    {
        //        if (filterOptions.ColumnIndex == 2)
        //            query = query.OrderByDescending(s => s.d.RdmRdCdSort);
        //        if (filterOptions.ColumnIndex == 3)
        //            query = query.OrderByDescending(s => s.s.FgrihDtOfInsp);
        //        if (filterOptions.ColumnIndex == 4)
        //            query = query.OrderByDescending(s => s.s.FgrihUserNameInspBy);
        //        if (filterOptions.ColumnIndex == 5)
        //            query = query.OrderByDescending(s => s.s.FgrihDivCode);
        //        if (filterOptions.ColumnIndex == 6)
        //            query = query.OrderByDescending(s => s.s.FgrihDist);
        //        if (filterOptions.ColumnIndex == 7)
        //            query = query.OrderByDescending(s => s.d.RdmRmuCode);
        //        if (filterOptions.ColumnIndex == 8)
        //            query = query.OrderByDescending(s => s.d.RdmRmuName);
        //        if (filterOptions.ColumnIndex == 9)
        //            query = query.OrderByDescending(s => s.d.RdmSecCode);
        //        if (filterOptions.ColumnIndex == 10)
        //            query = query.OrderByDescending(s => s.d.RdmSecName);
        //        if (filterOptions.ColumnIndex == 11)
        //            query = query.OrderByDescending(s => s.d.RdmRdCdSort);
        //        //if (filterOptions.ColumnIndex == 12)
        //        //    query = query.OrderByDescending(s => s.s.FgrihRoadName);
        //        //if (filterOptions.ColumnIndex == 13)
        //        //    query = query.OrderByDescending(s => s.s.FgrihYearOfInsp);
        //        if (filterOptions.ColumnIndex == 12)
        //            query = query.OrderByDescending(s => s.s.FgrihCrewLeaderName);
        //        //if (filterOptions.ColumnIndex == 0)
        //        //    query = query.OrderByDescending(s => s.s.FgrihPkRefNo);
        //    }

        //    var list = await query.Skip(filterOptions.StartPageNo)
        //       .Take(filterOptions.RecordsPerPage)
        //       .ToListAsync();

        //    return list.Select(s => new FormF2HeaderRequestDTO
        //    {
        //        ActiveYn = s.s.FgrihActiveYn.Value,
        //        CrBy = s.s.FgrihCrBy,
        //        CrDt = s.s.FgrihCrDt,
        //        CrewLeaderId = s.s.FgrihCrewLeaderId,
        //        CrewLeaderName = s.s.FgrihCrewLeaderName,
        //        Dist = s.s.FgrihDist,
        //        DivCode = s.d.RdmDivCode,
        //        DtInspBy = s.s.FgrihDtInspBy,
        //        DtOfInsp = s.s.FgrihDtOfInsp,
        //        FormRefId = s.s.FgrihFormRefId,
        //        ModBy = s.s.FgrihModBy,
        //        PkRefNo = s.s.FgrihPkRefNo,
        //        RoadCode = s.s.FgrihRoadCode,
        //        RoadName = s.s.FgrihRoadName,
        //        RmuCode = s.d.RdmRmuCode,
        //        RmuName = s.d.RdmRmuName,
        //        SectionCode = s.d.RdmSecCode,
        //        SectionName = s.d.RdmSecName,
        //        RoadLength = s.s.FgrihRoadLength,
        //        SignpathInspBy = s.s.FgrihSignpathInspBy,
        //        SubmitSts = s.s.FgrihSubmitSts,
        //        UserDesignationInspBy = s.s.FgrihUserDesignationInspBy,
        //        UserIdInspBy = s.s.FgrihUserIdInspBy,
        //        YearOfInsp = s.s.FgrihYearOfInsp,
        //        UserNameInspBy = s.s.FgrihUserNameInspBy

        //    }).ToList();
        //}


        public async Task<List<FormF3DtlGridDTO>> GetFormF3DtlGridList(FilteredPagingDefinition<FormF3DtlResponseDTO> filterOptions)
        {
            List<RmFormF3Dtl> result = new List<RmFormF3Dtl>();

            var query = from x in _context.RmFormF3Dtl
                        join a in _context.RmAllassetInventory on Convert.ToInt32(x.Ff3dAssetId) equals a.AiPkRefNo
                        where x.Ff3dFf3hPkRefNo == filterOptions.Filters.PkRefNo

                        select new { x, a };


            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderBy(x => x.x.Ff3dPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(x => x.x.RdmFrmCh);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(x => x.x.Ff3dCode);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(x => x.x.Ff3dConditionI);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(x => x.a.AiBound);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(x => x.a.AiWidth);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(x => x.a.AiHeight);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderBy(x => x.x.Ff3dDescription);

            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderByDescending(x => x.x.Ff3dPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(x => x.x.RdmFrmCh);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(x => x.x.Ff3dCode);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(x => x.x.Ff3dConditionI);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(x => x.a.AiBound);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(x => x.a.AiWidth);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(x => x.a.AiHeight);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderByDescending(x => x.x.Ff3dDescription);

            }

            var list = await query.Skip(filterOptions.StartPageNo)
            .Take(filterOptions.RecordsPerPage)
            .ToListAsync();

            return list.Select(s => new FormF3DtlGridDTO
            {
                Bound = s.a.AiBound,
                Ch = s.x.RdmFrmCh + "+" + s.x.RdmFrmChDeci,
                ConditionRating = s.x.Ff3dConditionI,
                Description = s.x.Ff3dDescription,
                FrmCh = s.x.RdmFrmCh,
                FrmChDec = s.x.RdmFrmChDeci,
                Height = s.a.AiHeight,
                PkRefNo = s.x.Ff3dPkRefNo,
                StructureCode = s.a.AiStrucCode,
                Width = s.a.AiWidth

            }).ToList();


        }

 

        public int SaveFormF3(RmFormF3Hdr FormF3)
        {
            try
            {
                _context.Entry<RmFormF3Hdr>(FormF3).State = FormF3.Ff3hPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return FormF3.Ff3hPkRefNo;
            }
            catch (Exception)
            {
                return 500;

            }
        }

        public int? DeleteFormF3Dtl(int Id)
        {
            var res = (from r in _context.RmFormF3Dtl where r.Ff3dPkRefNo == Id select r).SingleOrDefault();
            if (res != null)
            {
                _context.Entry(res).State = EntityState.Deleted;
                _context.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int? SaveFormF3Dtl(RmFormF3Dtl FormF3Dtl)
        {
            try
            {


                _context.Entry<RmFormF3Dtl>(FormF3Dtl).State = FormF3Dtl.Ff3dPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return FormF3Dtl.Ff3dPkRefNo;
            }
            catch (Exception ex)
            {
                return 500;

            }
        }


        //public async Task<RmFormF3Hdr> FindF3Byw1ID(int Id)
        //{
        //    return await _context.RmFormF3Hdr.Where(x => x.FF3Fw1PkRefNo == Id && x.FF3ActiveYn == true).FirstOrDefaultAsync();
        //}

        //public async Task<RmFormF3Hdr> FindFormF3ByID(int Id)
        //{
        //    return await _context.RmFormF3Hdr.Where(x => x.FF3PkRefNo == Id && x.FF3ActiveYn == true).FirstOrDefaultAsync();
        //}

        //public async Task<IEnumerable<RmFormF3HdrDtl>> FindFormF3DtlByID(int Id)
        //{
        //    return await _context.RmFormF3HdrDtl.Where(x => x.FF3dFF3PkRefNo == Id).ToListAsync();
        //}



    }
}
