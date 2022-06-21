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
    public class FormF3Repository : RepositoryBase<RmFormF3Hdr>, IFormF3Repository
    {
        public FormF3Repository(RAMMSContext context) : base(context)
        {
            _context = context;
        }



        public async Task<long> GetFilteredRecordCount(FilteredPagingDefinition<FormF2SearchGridDTO> filterOptions)
        {
            //var roads = (from a in _context.RmAllassetInventory
            //             where filterOptions.Filters.AssertType != "" ? a.AiGrpType == filterOptions.Filters.AssertType : a.AiGrpType.Contains(filterOptions.Filters.SmartSearch) && a.AiActiveYn == true
            //             select a.AiRdCode).ToArray();



            var query = (from s in _context.RmFormF3Hdr
                         join d in _context.RmRoadMaster on s.Ff3hRdCode equals d.RdmRdCode
                         from a in _context.RmAllassetInventory.Where(a => a.AiPkRefNo == 0).DefaultIfEmpty()
                         where s.Ff3hActiveYn == true
                         select new { s, d, a });


            var search = filterOptions.Filters;
            if (search.SecCode.HasValue)
            {
                query = query.Where(s => s.d.RdmSecCode == search.SecCode);
            }
            if (!string.IsNullOrEmpty(search.AssertType))
            {
                query = (from s in _context.RmFormF3Hdr
                         join dtl in _context.RmFormF3Dtl on s.Ff3hPkRefNo equals dtl.Ff3dFf3hPkRefNo
                         join a in _context.RmAllassetInventory on dtl.Ff3dAssetId equals Convert.ToString(a.AiPkRefNo)
                         join d in _context.RmRoadMaster on s.Ff3hRdCode equals d.RdmRdCode
                         select new { s, d, a });
                query = query.Where(s => s.a.AiStrucCode == search.AssertType);
            }
            if (!string.IsNullOrEmpty(search.RmuCode))
            {
                query = query.Where(s => s.d.RdmRmuCode == search.RmuCode);
            }
            if (!string.IsNullOrEmpty(search.RoadCode))
            {
                query = query.Where(s => s.s.Ff3hRdCode == search.RoadCode);
            }
            if (search.Year.HasValue)
            {
                query = query.Where(s => s.s.Ff3hInspectedYear == search.Year.Value);
            }

            if (search.FromYear.HasValue)
            {
                query = query.Where(s => s.s.Ff3hInspectedYear >= search.FromYear);
            }
            if (search.ToYear.HasValue)
            {
                query = query.Where(s => s.s.Ff3hInspectedYear <= search.ToYear);
            }

            if (search.FromChKM.HasValue || !string.IsNullOrEmpty(search.FromChM))
            {
                query = query.Where(s => s.s.RmFormF3Dtl.Any(x => Convert.ToDouble(x.Ff3dLocCh.ToString() + '.' + x.Ff3dLocChDeci) >= Convert.ToDouble(search.FromChKM.ToString() + '.' + search.FromChM)));
            }

            if (search.ToChKM.HasValue || !string.IsNullOrEmpty(search.ToChM))
            {
                query = query.Where(s => s.s.RmFormF3Dtl.Any(x => Convert.ToDouble(x.Ff3dLocCh.ToString() + '.' + x.Ff3dLocChDeci) <= Convert.ToDouble(search.ToChKM.ToString() + '.' + search.ToChM)));
            }

            if (!string.IsNullOrEmpty(search.Bound))
            {
                query = query.Where(s => s.s.RmFormF3Dtl.Any(x => x.Ff3dSide == search.Bound));
            }

            if (!string.IsNullOrEmpty(search.SmartSearch))
            {
                if (int.TryParse(search.SmartSearch, out int seccode))
                {
                    query = query.Where(s => s.d.RdmSecCode == seccode);
                }

                DateTime dt;
                if (DateTime.TryParseExact(search.SmartSearch, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                {
                    query = query.Where(s =>
                    (s.s.Ff3hPkRefId.Contains(search.SmartSearch) ||
                    s.s.Ff3hRdCode.Contains(search.SmartSearch) ||
                    s.d.RdmRmuCode.Contains(search.SmartSearch) ||
                    s.d.RdmRmuName.Contains(search.SmartSearch) ||
                    s.d.RdmDivCode.Contains(search.SmartSearch) ||
                    s.s.Ff3hRdName.Contains(search.SmartSearch)) ||
                    s.d.RdmSecName.Contains(search.SmartSearch) ||
                    s.s.Ff3hDist.Contains(search.SmartSearch) ||
                    //roads.Contains(s.d.RdmRdCode) ||
                    s.s.Ff3hInspectedName.Contains(search.SmartSearch) ||
                    s.s.Ff3hCrewName.Contains(search.SmartSearch) ||
                    (s.s.Ff3hInspectedDate.HasValue ? (s.s.Ff3hInspectedDate.Value.Year == dt.Year && s.s.Ff3hInspectedDate.Value.Month == dt.Month && s.s.Ff3hInspectedDate.Value.Day == dt.Day) : true) && s.s.Ff3hInspectedDate != null);
                }
                else
                {
                    query = query.Where(s =>
                     (s.s.Ff3hPkRefId.Contains(search.SmartSearch) ||
                     s.s.Ff3hRdCode.Contains(search.SmartSearch) ||
                     s.d.RdmRmuCode.Contains(search.SmartSearch) ||
                     s.d.RdmRmuName.Contains(search.SmartSearch) ||
                     s.d.RdmDivCode.Contains(search.SmartSearch) ||
                     s.s.Ff3hRdName.Contains(search.SmartSearch)) ||
                     s.d.RdmSecName.Contains(search.SmartSearch) ||
                     s.s.Ff3hDist.Contains(search.SmartSearch) ||
                     //roads.Contains(s.d.RdmRdCode) ||
                     s.s.Ff3hInspectedName.Contains(search.SmartSearch) ||
                     s.s.Ff3hCrewName.Contains(search.SmartSearch)
                     );
                }
            }

            return await query.CountAsync();
        }

        public async Task<List<FormF2HeaderRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormF2SearchGridDTO> filterOptions)
        {



            var query = (from s in _context.RmFormF3Hdr
                         join d in _context.RmRoadMaster on s.Ff3hRdCode equals d.RdmRdCode
                         from a in _context.RmAllassetInventory.Where(a => a.AiPkRefNo == 0).DefaultIfEmpty()
                         select new { s, d, a });



            query = query.Where(x => x.s.Ff3hActiveYn == true).OrderByDescending(x => x.s.Ff3hPkRefNo);
            var search = filterOptions.Filters;
            if (search.SecCode.HasValue)
            {
                query = query.Where(s => s.d.RdmSecCode == search.SecCode);
            }
            if (!string.IsNullOrEmpty(search.AssertType))
            {

                query = (from s in _context.RmFormF3Hdr
                         join dtl in _context.RmFormF3Dtl on s.Ff3hPkRefNo equals dtl.Ff3dFf3hPkRefNo
                         join a in _context.RmAllassetInventory on dtl.Ff3dAssetId equals Convert.ToString(a.AiPkRefNo)
                         join d in _context.RmRoadMaster on s.Ff3hRdCode equals d.RdmRdCode
                         select new { s, d, a });
                query = query.Where(s => s.a.AiStrucCode == search.AssertType);
            }
            if (!string.IsNullOrEmpty(search.RmuCode))
            {
                query = query.Where(s => s.d.RdmRmuCode == search.RmuCode);
            }
            if (!string.IsNullOrEmpty(search.RoadCode))
            {
                query = query.Where(s => s.s.Ff3hRdCode == search.RoadCode);
            }
            if (search.Year.HasValue)
            {
                query = query.Where(s => s.s.Ff3hInspectedYear == search.Year.Value);
            }

            if (search.FromYear.HasValue)
            {
                query = query.Where(s => s.s.Ff3hInspectedYear >= search.FromYear);
            }
            if (search.ToYear.HasValue)
            {
                query = query.Where(s => s.s.Ff3hInspectedYear <= search.ToYear);
            }
            if (!string.IsNullOrEmpty(search.AssertType))
            {
                //  query = query.Where(s => s.r.AiGrpType == search.AssertType);
            }

            if (search.FromChKM.HasValue || (!string.IsNullOrEmpty(search.FromChM)))
            {
                query = query.Where(s => s.s.RmFormF3Dtl.Any(x => Convert.ToDouble(x.Ff3dLocCh.ToString() + '.' + x.Ff3dLocChDeci) >= Convert.ToDouble(search.FromChKM.ToString() + '.' + search.FromChM)));
            }

            if (search.ToChKM.HasValue || (!string.IsNullOrEmpty(search.ToChM)))
            {
                query = query.Where(s => s.s.RmFormF3Dtl.Any(x => Convert.ToDouble(x.Ff3dLocCh.ToString() + '.' + x.Ff3dLocChDeci) <= Convert.ToDouble(search.ToChKM.ToString() + '.' + search.ToChM)));
            }

            if (!string.IsNullOrEmpty(search.Bound))
            {
                query = query.Where(s => s.s.RmFormF3Dtl.Any(x => x.Ff3dBound == search.Bound));
            }

            if (!string.IsNullOrEmpty(search.SmartSearch))
            {
                if (int.TryParse(search.SmartSearch, out int seccode))
                {
                    query = query.Where(s => s.d.RdmSecCode == seccode);
                }
                DateTime dt;
                if (DateTime.TryParseExact(search.SmartSearch, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                {
                    query = query.Where(s =>
                    (s.s.Ff3hPkRefId.Contains(search.SmartSearch) ||
                    s.s.Ff3hRdCode.Contains(search.SmartSearch) ||
                    s.d.RdmRmuCode.Contains(search.SmartSearch) ||
                    s.d.RdmRmuName.Contains(search.SmartSearch) ||
                    s.d.RdmDivCode.Contains(search.SmartSearch) ||
                    s.s.Ff3hRdName.Contains(search.SmartSearch)) ||
                    s.d.RdmSecName.Contains(search.SmartSearch) ||
                    s.s.Ff3hDist.Contains(search.SmartSearch) ||
                    //roads.Contains(s.d.RdmRdCode) ||
                    s.s.Ff3hInspectedName.Contains(search.SmartSearch) ||
                    s.s.Ff3hCrewName.Contains(search.SmartSearch) ||
                    (s.s.Ff3hInspectedDate.HasValue ? (s.s.Ff3hInspectedDate.Value.Year == dt.Year && s.s.Ff3hInspectedDate.Value.Month == dt.Month && s.s.Ff3hInspectedDate.Value.Day == dt.Day) : true) && s.s.Ff3hInspectedDate != null);
                }
                else
                {
                    query = query.Where(s =>
                     (s.s.Ff3hPkRefId.Contains(search.SmartSearch) ||
                     s.s.Ff3hRdCode.Contains(search.SmartSearch) ||
                     s.d.RdmRmuCode.Contains(search.SmartSearch) ||
                     s.d.RdmRmuName.Contains(search.SmartSearch) ||
                     s.d.RdmDivCode.Contains(search.SmartSearch) ||
                     s.s.Ff3hRdName.Contains(search.SmartSearch)) ||
                     s.d.RdmSecName.Contains(search.SmartSearch) ||
                     s.s.Ff3hDist.Contains(search.SmartSearch) ||
                     //roads.Contains(s.d.RdmRdCode) ||
                     s.s.Ff3hInspectedName.Contains(search.SmartSearch) ||
                     s.s.Ff3hCrewName.Contains(search.SmartSearch)
                     );
                }

            }

            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(s => s.d.RdmRdCdSort);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(s => s.s.Ff3hInspectedDate);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(s => s.s.Ff3hInspectedName);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(s => s.s.Ff3hDivCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(s => s.s.Ff3hDist);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(s => s.d.RdmRmuCode);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderBy(s => s.d.RdmRmuName);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderBy(s => s.d.RdmSecCode);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderBy(s => s.d.RdmSecName);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderBy(s => s.d.RdmRdCdSort);
                //if (filterOptions.ColumnIndex == 12)
                //    query = query.OrderBy(s => s.s.FgrihRoadName);
                //if (filterOptions.ColumnIndex == 13)
                //    query = query.OrderBy(s => s.s.FgrihYearOfInsp);
                if (filterOptions.ColumnIndex == 12)
                    query = query.OrderBy(s => s.s.Ff3hCrewName);
                //if (filterOptions.ColumnIndex == 0)
                //    query = query.OrderByDescending(s => s.s.FgrihPkRefNo);
            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(s => s.d.RdmRdCdSort);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(s => s.s.Ff3hInspectedDate);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(s => s.s.Ff3hInspectedName);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(s => s.s.Ff3hDivCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(s => s.s.Ff3hDist);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(s => s.d.RdmRmuCode);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderByDescending(s => s.d.RdmRmuName);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderByDescending(s => s.d.RdmSecCode);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderByDescending(s => s.d.RdmSecName);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderByDescending(s => s.d.RdmRdCdSort);
                //if (filterOptions.ColumnIndex == 12)
                //    query = query.OrderByDescending(s => s.s.FgrihRoadName);
                //if (filterOptions.ColumnIndex == 13)
                //    query = query.OrderByDescending(s => s.s.FgrihYearOfInsp);
                if (filterOptions.ColumnIndex == 12)
                    query = query.OrderByDescending(s => s.s.Ff3hCrewName);
                //if (filterOptions.ColumnIndex == 0)
                //    query = query.OrderByDescending(s => s.s.FgrihPkRefNo);
            }




            var list = await query.Skip(filterOptions.StartPageNo)
  .Take(filterOptions.RecordsPerPage)
  .ToListAsync();

            return list.Select(s => new FormF2HeaderRequestDTO
            {
                ActiveYn = s.s.Ff3hActiveYn,
                CrBy = s.s.Ff3hCrBy,
                CrDt = s.s.Ff3hCrDt,
                CrewLeaderId = 0,
                CrewLeaderName = s.s.Ff3hCrewName,
                Dist = s.s.Ff3hDist,
                DivCode = s.d.RdmDivCode,
                DtInspBy = s.s.Ff3hInspectedDate,
                DtOfInsp = s.s.Ff3hInspectedDate,
                FormRefId = s.s.Ff3hPkRefId,
                ModBy = s.s.Ff3hModBy,
                PkRefNo = s.s.Ff3hPkRefNo,
                RoadCode = s.s.Ff3hRdCode,
                RoadName = s.s.Ff3hRdName,
                RmuCode = s.d.RdmRmuCode,
                RmuName = s.d.RdmRmuName,
                SectionCode = s.d.RdmSecCode,
                SectionName = s.d.RdmSecName,
                RoadLength = s.s.Ff3hRoadLength,
                SignpathInspBy = "",// s.s.FgrihSignpathInspBy,
                SubmitSts = s.s.Ff3hSubmitSts,
                UserDesignationInspBy = "",//s.s.in,
                UserIdInspBy = s.s.Ff3hInspectedBy,
                YearOfInsp = s.s.Ff3hInspectedYear,
                UserNameInspBy = s.s.Ff3hInspectedName

            }).ToList();
        }


        public async Task<List<FormF3DtlGridDTO>> GetFormF3DtlGridList(FilteredPagingDefinition<FormF3DtlResponseDTO> filterOptions)
        {
            List<RmFormF3Dtl> result = new List<RmFormF3Dtl>();

            var query = from x in _context.RmFormF3Dtl
                        join a in _context.RmAllassetInventory on Convert.ToInt32(x.Ff3dAssetId) equals a.AiPkRefNo
                        where x.Ff3dFf3hPkRefNo == filterOptions.Filters.Ff3hPkRefNo

                        select new { x, a };


            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderBy(x => x.x.Ff3dPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(x => x.x.Ff3dLocCh);
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
                    query = query.OrderByDescending(x => x.x.Ff3dLocCh);
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
            int i = 1;

            return list.Select(s => new FormF3DtlGridDTO
            {
                AssetId = s.x.Ff3dAssetId,
                Bound = s.a.AiBound,
                Ch = s.x.Ff3dLocCh + "+" + s.x.Ff3dLocChDeci,
                ConditionRating = Convert.ToInt32(s.x.Ff3dConditionI),
                Description = s.x.Ff3dDescription,
                FrmCh = s.x.Ff3dLocCh,
                FrmChDec = s.x.Ff3dLocChDeci,
                Height = s.a.AiHeight,
                PkRefNo = i++,
                StructureCode = s.a.AiStrucCode,
                Width = s.a.AiWidth

            }).ToList();


        }


        public List<RmAllassetInventory> GetAssetDetails(FormF3ResponseDTO FormF3)
        {

            if (FormF3.Source == "New")
            {
                string[] StructCode = { "W", "GS", "DEL" };
                return (from r in _context.RmAllassetInventory.Where(s => StructCode.Contains(s.AiStrucCode) && s.AiRmuCode == FormF3.RmuCode && s.AiDivCode == FormF3.DivCode && s.AiSecCode == FormF3.SecCode && s.AiRdCode == FormF3.RdCode) select r).ToList();
            }
            else
            {
                return (from r in _context.RmAllassetInventory.Where(s => s.AiStrucCode == "Y" && s.AiRmuCode == FormF3.RmuCode && s.AiDivCode == FormF3.DivCode && s.AiSecCode == FormF3.SecCode && s.AiRdCode == FormF3.RdCode) select r).ToList();
            }

        }

        public int LoadG1G2Data(FormF3ResponseDTO FormF3)
        {
            try
            {
                var res = (from g1 in _context.RmFormG1Hdr
                           where g1.Fg1hDivCode == FormF3.DivCode && g1.Fg1hRmuCode == FormF3.RmuCode && g1.Fg1hYearOfInsp == FormF3.InspectedYear && g1.Fg1hRdCode == FormF3.RdCode && g1.Fg1hActiveYn == true
                           select new RmFormF3Dtl
                           {
                               Ff3dFf3hPkRefNo = FormF3.PkRefNo,
                               Ff3dAssetId = Convert.ToString(g1.Fg1hAiPkRefNo),
                               Ff3G1hPkRefNo = g1.Fg1hPkRefNo
                           }).ToList();

                foreach (var item in res)
                {
                    _context.RmFormF3Dtl.Add(item);
                    _context.SaveChanges();
                }

                return 1;
            }
            catch (Exception)
            {
                return 500;

            }
        }


        public int? DeleteFormF3(int id)
        {
            try
            {
                IList<RmFormF3Dtl> child = (from r in _context.RmFormF3Dtl where r.Ff3dFf3hPkRefNo == id select r).ToList();
                foreach (var item in child)
                {
                    _context.Remove(item);
                    _context.SaveChanges();
                }

                var res = _context.Set<RmFormF3Hdr>().FindAsync(id);
                res.Result.Ff3hActiveYn = false;
                _context.Set<RmFormF3Hdr>().Attach(res.Result);
                _context.Entry<RmFormF3Hdr>(res.Result).State = EntityState.Modified;
                _context.SaveChanges();
                return 1;

            }
            catch (Exception ex)
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

        public int? UpdateFormF3Dtl(RmFormF3Dtl FormF3Dtl)
        {
            try
            {
                _context.Set<RmFormF3Dtl>().Attach(FormF3Dtl);
                _context.Entry<RmFormF3Dtl>(FormF3Dtl).State = EntityState.Modified;
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }


        public async Task<FORMF3Rpt> GetReportData(int headerid)
        {
            FORMF3Rpt result = (from s in _context.RmFormF3Hdr
                                where s.Ff3hPkRefNo == headerid && s.Ff3hActiveYn == true
                                select new FORMF3Rpt
                                {
                                    CrewLeader = s.Ff3hCrewName,
                                    District = s.Ff3hDist,
                                    InspectedByDesignation = s.Ff3hInspectedDesig,
                                    InspectedByName = s.Ff3hInspectedName,
                                    InspectedDate = s.Ff3hInspectedDate,
                                    Division = s.Ff3hDivCode,
                                    RMU = s.Ff3hRmuCode,
                                    RoadCode = s.Ff3hRdCode,
                                    RoadName = s.Ff3hRdName,
                                    RoadLength = s.Ff3hRoadLength
                                }).FirstOrDefault();


            result.Details = (from d in _context.RmFormF3Dtl
                              where d.Ff3dFf3hPkRefNo == headerid
                              orderby d.Ff3dPkRefNo descending
                              select new FORMF3RptDetail
                              {
                                  Descriptions = d.Ff3dDescription,
                                  LocationChKm = d.Ff3dLocCh,
                                  LocationChM = d.Ff3dLocChDeci,
                                  Width = d.Ff3dWidth,
                                  Height = d.Ff3dHeight,
                                  Condition = d.Ff3dConditionI
                              }).ToArray();
            return result;

        }


    }
}
