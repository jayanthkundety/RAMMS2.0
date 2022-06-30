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
    public class FormF1Repository : RepositoryBase<RmFormF1Hdr>, IFormF1Repository
    {
        public FormF1Repository(RAMMSContext context) : base(context)
        {
            _context = context;
        }



        public async Task<long> GetFilteredRecordCount(FilteredPagingDefinition<FormF1SearchGridDTO> filterOptions)
        {



            var query = (from s in _context.RmFormF1Hdr
                         join d in _context.RmRoadMaster on s.Ff1hRdCode equals d.RdmRdCode
                         from a in _context.RmAllassetInventory.Where(a => a.AiPkRefNo == 0).DefaultIfEmpty()
                         where s.Ff1hActiveYn == true
                         select new { s, d, a });


            var search = filterOptions.Filters;
            if (search.SecCode.HasValue)
            {
                query = query.Where(s => s.d.RdmSecCode == search.SecCode);
            }
            if (!string.IsNullOrEmpty(search.AssertType))
            {
                query = (from s in _context.RmFormF1Hdr
                         join dtl in _context.RmFormF1Dtl on s.Ff1hPkRefNo equals dtl.Ff1dFf1hPkRefNo
                         join a in _context.RmAllassetInventory on dtl.Ff1dDescription equals Convert.ToString(a.AiPkRefNo)
                         join d in _context.RmRoadMaster on s.Ff1hRdCode equals d.RdmRdCode
                         select new { s, d, a });
                query = query.Where(s => s.a.AiStrucCode == search.AssertType);
            }
            if (!string.IsNullOrEmpty(search.RmuCode))
            {
                query = query.Where(s => s.d.RdmRmuCode == search.RmuCode);
            }
            if (!string.IsNullOrEmpty(search.RoadCode))
            {
                query = query.Where(s => s.s.Ff1hRdCode == search.RoadCode);
            }
            if (search.Year.HasValue)
            {
                query = query.Where(s => s.s.Ff1hInspectedYear == search.Year.Value);
            }

            if (search.FromYear.HasValue)
            {
                query = query.Where(s => s.s.Ff1hInspectedYear >= search.FromYear);
            }
            if (search.ToYear.HasValue)
            {
                query = query.Where(s => s.s.Ff1hInspectedYear <= search.ToYear);
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
                    (s.s.Ff1hPkRefId.Contains(search.SmartSearch) ||
                    s.s.Ff1hRdCode.Contains(search.SmartSearch) ||
                    s.d.RdmRmuCode.Contains(search.SmartSearch) ||
                    s.d.RdmRmuName.Contains(search.SmartSearch) ||
                    s.d.RdmDivCode.Contains(search.SmartSearch) ||
                    s.s.Ff1hRdName.Contains(search.SmartSearch)) ||
                    s.d.RdmSecName.Contains(search.SmartSearch) ||
                    s.s.Ff1hDist.Contains(search.SmartSearch) ||
                    //roads.Contains(s.d.RdmRdCode) ||
                    s.s.Ff1hInspectedName.Contains(search.SmartSearch) ||

                    (s.s.Ff1hInspectedDate.HasValue ? (s.s.Ff1hInspectedDate.Value.Year == dt.Year && s.s.Ff1hInspectedDate.Value.Month == dt.Month && s.s.Ff1hInspectedDate.Value.Day == dt.Day) : true) && s.s.Ff1hInspectedDate != null);
                }
                else
                {
                    query = query.Where(s =>
                     (s.s.Ff1hPkRefId.Contains(search.SmartSearch) ||
                     s.s.Ff1hRdCode.Contains(search.SmartSearch) ||
                     s.d.RdmRmuCode.Contains(search.SmartSearch) ||
                     s.d.RdmRmuName.Contains(search.SmartSearch) ||
                     s.d.RdmDivCode.Contains(search.SmartSearch) ||
                     s.s.Ff1hRdName.Contains(search.SmartSearch)) ||
                     s.d.RdmSecName.Contains(search.SmartSearch) ||
                     s.s.Ff1hDist.Contains(search.SmartSearch) ||
                     //roads.Contains(s.d.RdmRdCode) ||
                     s.s.Ff1hInspectedName.Contains(search.SmartSearch)

                     );
                }
            }

            return await query.CountAsync();
        }

        public async Task<List<FormF1HeaderRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormF1SearchGridDTO> filterOptions)
        {



            var query = (from s in _context.RmFormF1Hdr
                         join d in _context.RmRoadMaster on s.Ff1hRdCode equals d.RdmRdCode
                         from a in _context.RmAllassetInventory.Where(a => a.AiPkRefNo == 0).DefaultIfEmpty()
                         select new { s, d, a });



            query = query.Where(x => x.s.Ff1hActiveYn == true).OrderByDescending(x => x.s.Ff1hPkRefNo);
            var search = filterOptions.Filters;
            if (search.SecCode.HasValue)
            {
                query = query.Where(s => s.d.RdmSecCode == search.SecCode);
            }
            if (!string.IsNullOrEmpty(search.AssertType))
            {

                query = (from s in _context.RmFormF1Hdr
                         join dtl in _context.RmFormF1Dtl on s.Ff1hPkRefNo equals dtl.Ff1dFf1hPkRefNo
                         join a in _context.RmAllassetInventory on dtl.Ff1dDescription equals Convert.ToString(a.AiPkRefNo)
                         join d in _context.RmRoadMaster on s.Ff1hRdCode equals d.RdmRdCode
                         select new { s, d, a });
                query = query.Where(s => s.a.AiStrucCode == search.AssertType);
            }
            if (!string.IsNullOrEmpty(search.RmuCode))
            {
                query = query.Where(s => s.d.RdmRmuCode == search.RmuCode);
            }
            if (!string.IsNullOrEmpty(search.RoadCode))
            {
                query = query.Where(s => s.s.Ff1hRdCode == search.RoadCode);
            }
            if (search.Year.HasValue)
            {
                query = query.Where(s => s.s.Ff1hInspectedYear == search.Year.Value);
            }

            if (search.FromYear.HasValue)
            {
                query = query.Where(s => s.s.Ff1hInspectedYear >= search.FromYear);
            }
            if (search.ToYear.HasValue)
            {
                query = query.Where(s => s.s.Ff1hInspectedYear <= search.ToYear);
            }
            if (!string.IsNullOrEmpty(search.AssertType))
            {
                //  query = query.Where(s => s.r.AiGrpType == search.AssertType);
            }

            if (search.FromChKM.HasValue || (!string.IsNullOrEmpty(search.FromChM)))
            {
                query = query.Where(s => s.s.RmFormF1Dtl.Any(x => Convert.ToDouble(x.Ff1dLocCh.ToString() + '.' + x.Ff1dLocChDeci) >= Convert.ToDouble(search.FromChKM.ToString() + '.' + search.FromChM)));
            }

            if (search.ToChKM.HasValue || (!string.IsNullOrEmpty(search.ToChM)))
            {
                query = query.Where(s => s.s.RmFormF1Dtl.Any(x => Convert.ToDouble(x.Ff1dLocCh.ToString() + '.' + x.Ff1dLocChDeci) <= Convert.ToDouble(search.ToChKM.ToString() + '.' + search.ToChM)));
            }

            if (search.ToChKM.HasValue)
            {
                query = query.Where(s => s.s.RmFormF1Dtl.Any(x => x.Ff1dTier == search.Tier));
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
                    (s.s.Ff1hPkRefId.Contains(search.SmartSearch) ||
                    s.s.Ff1hRdCode.Contains(search.SmartSearch) ||
                    s.d.RdmRmuCode.Contains(search.SmartSearch) ||
                    s.d.RdmRmuName.Contains(search.SmartSearch) ||
                    s.d.RdmDivCode.Contains(search.SmartSearch) ||
                    s.s.Ff1hRdName.Contains(search.SmartSearch)) ||
                    s.d.RdmSecName.Contains(search.SmartSearch) ||
                    s.s.Ff1hDist.Contains(search.SmartSearch) ||
                    //roads.Contains(s.d.RdmRdCode) ||
                    s.s.Ff1hInspectedName.Contains(search.SmartSearch) ||

                    (s.s.Ff1hInspectedDate.HasValue ? (s.s.Ff1hInspectedDate.Value.Year == dt.Year && s.s.Ff1hInspectedDate.Value.Month == dt.Month && s.s.Ff1hInspectedDate.Value.Day == dt.Day) : true) && s.s.Ff1hInspectedDate != null);
                }
                else
                {
                    query = query.Where(s =>
                     (s.s.Ff1hPkRefId.Contains(search.SmartSearch) ||
                     s.s.Ff1hRdCode.Contains(search.SmartSearch) ||
                     s.d.RdmRmuCode.Contains(search.SmartSearch) ||
                     s.d.RdmRmuName.Contains(search.SmartSearch) ||
                     s.d.RdmDivCode.Contains(search.SmartSearch) ||
                     s.s.Ff1hRdName.Contains(search.SmartSearch)) ||
                     s.d.RdmSecName.Contains(search.SmartSearch) ||
                     s.s.Ff1hDist.Contains(search.SmartSearch) ||
                     //roads.Contains(s.d.RdmRdCode) ||
                     s.s.Ff1hInspectedName.Contains(search.SmartSearch)

                     );
                }

            }

            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(s => s.d.RdmRdCdSort);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(s => s.s.Ff1hInspectedDate);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(s => s.s.Ff1hInspectedName);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(s => s.s.Ff1hDivCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(s => s.s.Ff1hDist);
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

            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(s => s.d.RdmRdCdSort);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(s => s.s.Ff1hInspectedDate);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(s => s.s.Ff1hInspectedName);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(s => s.s.Ff1hDivCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(s => s.s.Ff1hDist);
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

            }




            var list = await query.Skip(filterOptions.StartPageNo)
  .Take(filterOptions.RecordsPerPage)
  .ToListAsync();

            return list.Select(s => new FormF1HeaderRequestDTO
            {
                ActiveYn = s.s.Ff1hActiveYn,
                CrBy = s.s.Ff1hCrBy,
                CrDt = s.s.Ff1hCrDt,
                CrewLeaderName = s.s.Ff1hCrewName,
                Dist = s.s.Ff1hDist,
                DivCode = s.d.RdmDivCode,
                DtInspBy = s.s.Ff1hInspectedDate,
                DtOfInsp = s.s.Ff1hInspectedDate,
                FormRefId = s.s.Ff1hPkRefId,
                ModBy = s.s.Ff1hModBy,
                PkRefNo = s.s.Ff1hPkRefNo,
                RoadCode = s.s.Ff1hRdCode,
                RoadName = s.s.Ff1hRdName,
                RmuCode = s.d.RdmRmuCode,
                RmuName = s.d.RdmRmuName,
                SectionCode = s.d.RdmSecCode,
                SectionName = s.d.RdmSecName,
                RoadLength = s.s.Ff1hRoadLength,
                SignpathInspBy = "",// s.s.FgrihSignpathInspBy,
                SubmitSts = s.s.Ff1hSubmitSts,
                UserDesignationInspBy = "",//s.s.in,
                UserIdInspBy = s.s.Ff1hInspectedBy,
                YearOfInsp = s.s.Ff1hInspectedYear,
                UserNameInspBy = s.s.Ff1hInspectedName,
                Status = s.s.Ff1hStatus

            }).ToList();
        }


        public async Task<List<FormF1DtlGridDTO>> GetFormF1DtlGridList(FilteredPagingDefinition<FormF1DtlResponseDTO> filterOptions)
        {
            List<RmFormF1Dtl> result = new List<RmFormF1Dtl>();

            var query = from x in _context.RmFormF1Dtl
                        join a in _context.RmAllassetInventory on x.Ff1dAssetId equals a.AiAssetId
                        where x.Ff1dFf1hPkRefNo == filterOptions.Filters.Ff1hPkRefNo
                        orderby x.Ff1dPkRefNo descending
                        select new { x, a };


            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderBy(x => x.x.Ff1dPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(x => x.x.Ff1dLocCh);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(x => x.x.Ff1dCode);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(x => x.x.Ff1dOverallCondition);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(x => x.a.AiBound);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(x => x.a.AiWidth);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(x => x.a.AiHeight);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderBy(x => x.x.Ff1dDescription);

            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderByDescending(x => x.x.Ff1dPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(x => x.x.Ff1dLocCh);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(x => x.x.Ff1dCode);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(x => x.x.Ff1dOverallCondition);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(x => x.a.AiBound);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(x => x.a.AiWidth);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(x => x.a.AiHeight);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderByDescending(x => x.x.Ff1dDescription);

            }

            var list = await query.ToListAsync();
            

            return list.Select(s => new FormF1DtlGridDTO
            {

                AssetId = s.a.AiAssetId,
                Ch = s.x.Ff1dLocCh + "+" + s.x.Ff1dLocChDeci,
                OverallCondition = Convert.ToInt32(s.x.Ff1dOverallCondition),
                Description = s.x.Ff1dDescription,
                FrmCh = s.x.Ff1dLocCh,
                FrmChDec = s.x.Ff1dLocChDeci,
                Height = s.a.AiHeight,
                PkRefNo = s.x.Ff1dPkRefNo,
                StructureCode = s.a.AiStrucCode,
                Length = s.a.AiLength,
                Width = s.a.AiWidth,
                BottomWidth = s.a.AiBotWidth,
                Tier = s.a.AiTier

            }).ToList();


        }


        public List<RmAllassetInventory> GetAssetDetails(FormF1ResponseDTO FormF1)
        {


            return (from r in _context.RmAllassetInventory.Where(s => s.AiStrucCode == "Y" && s.AiRmuCode == FormF1.RmuCode && s.AiDivCode == FormF1.DivCode && s.AiRdCode == FormF1.RdCode) select r).ToList();


        }

        public int LoadR1Data(FormF1ResponseDTO FormF1)
        {
            try
            {

                var res = (from r1 in _context.RmFormR1Hdr
                           join a in _context.RmAllassetInventory on r1.Fr1hAidPkRefNo equals a.AiPkRefNo
                           where r1.Fr1hAiDivCode == FormF1.DivCode && r1.Fr1hAiRmuCode == FormF1.RmuCode && r1.Fr1hYearOfInsp == FormF1.InspectedYear && r1.Fr1hAiRdCode == FormF1.RdCode && r1.Fr1hActiveYn == true
                           select new RmFormF1Dtl
                           {
                               Ff1dFf1hPkRefNo = FormF1.PkRefNo,
                               //Ff1dAssetId = Convert.ToString(r1.Fr1hAiPkRefNo),
                               Ff1dPkRefNo = r1.Fr1hPkRefNo,
                               Ff1dTier = Convert.ToInt32(a.AiTier),
                               Ff1dCode = a.AiStrucCode,
                               Ff1dHeight = Convert.ToDecimal(a.AiHeight),
                               Ff1dTopWidth = Convert.ToDecimal(a.AiWidth),
                               Ff1dOverallCondition = r1.Fr1hCondRating,
                               Ff1dLocCh = a.AiLocChKm,
                               Ff1dLocChDeci = a.AiLocChM == "" ? 0 : Convert.ToInt32(a.AiLocChM)

                           }).ToList();

                foreach (var item in res)
                {
                    _context.RmFormF1Dtl.Add(item);
                    _context.SaveChanges();
                }

                return 1;
            }
            catch (Exception)
            {
                return 500;

            }
        }


        public async Task<RmFormF1Hdr> SaveFormF1(RmFormF1Hdr FormF1)
        {
            try
            {

                var search = (from r1 in _context.RmFormR1Hdr
                              where r1.Fr1hAiDivCode == FormF1.Ff1hDivCode && r1.Fr1hAiRmuCode == FormF1.Ff1hRmuCode && r1.Fr1hYearOfInsp == FormF1.Ff1hInspectedYear && r1.Fr1hAiRdCode == FormF1.Ff1hRdCode && r1.Fr1hActiveYn == true
                              select r1);

                if (search.Count() > 0)
                {
                    _context.RmFormF1Hdr.Add(FormF1);
                    _context.SaveChanges();

                    var res = (from r1 in _context.RmFormR1Hdr
                               join a in _context.RmAllassetInventory on r1.Fr1hAidPkRefNo equals a.AiPkRefNo
                               where r1.Fr1hAiDivCode == FormF1.Ff1hDivCode && r1.Fr1hAiRmuCode == FormF1.Ff1hRmuCode && r1.Fr1hYearOfInsp == FormF1.Ff1hInspectedYear && r1.Fr1hAiRdCode == FormF1.Ff1hRdCode && r1.Fr1hActiveYn == true
                               select new RmFormF1Dtl
                               {
                                   Ff1dFf1hPkRefNo = FormF1.Ff1hPkRefNo,
                                   Ff1dAssetId =r1.Fr1hAssetId,
                                   Ff1dR1hPkRefNo = r1.Fr1hPkRefNo,
                                   Ff1dTier = Convert.ToInt32(a.AiTier),
                                   Ff1dCode = a.AiStrucCode,
                                   Ff1dHeight = Convert.ToDecimal(a.AiHeight),
                                   Ff1dTopWidth = Convert.ToDecimal(a.AiWidth),
                                   Ff1dOverallCondition = r1.Fr1hCondRating,
                                   Ff1dLocCh = a.AiLocChKm,
                                   Ff1dLocChDeci = a.AiLocChM == "" ? 0 : Convert.ToInt32(a.AiLocChM)

                               }).ToList();

                    foreach (var item in res)
                    {
                        _context.RmFormF1Dtl.Add(item);
                        _context.SaveChanges();
                    }

                }

                return FormF1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int? DeleteFormF1(int id)
        {
            try
            {
                IList<RmFormF1Dtl> child = (from r in _context.RmFormF1Dtl where r.Ff1dFf1hPkRefNo == id select r).ToList();
                foreach (var item in child)
                {
                    _context.Remove(item);
                    _context.SaveChanges();
                }

                var res = _context.Set<RmFormF1Hdr>().FindAsync(id);
                res.Result.Ff1hActiveYn = false;
                _context.Set<RmFormF1Hdr>().Attach(res.Result);
                _context.Entry<RmFormF1Hdr>(res.Result).State = EntityState.Modified;
                _context.SaveChanges();
                return 1;

            }
            catch (Exception ex)
            {
                return 500;
            }
        }


        public int? DeleteFormF1Dtl(int Id)
        {
            var res = (from r in _context.RmFormF1Dtl where r.Ff1dPkRefNo == Id select r).SingleOrDefault();
            if (res != null)
            {
                _context.Entry(res).State = EntityState.Deleted;
                _context.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int? SaveFormF1Dtl(RmFormF1Dtl FormF1Dtl)
        {
            try
            {


                _context.Entry<RmFormF1Dtl>(FormF1Dtl).State = FormF1Dtl.Ff1dPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return FormF1Dtl.Ff1dPkRefNo;
            }
            catch (Exception ex)
            {
                return 500;

            }
        }

        public int? UpdateFormF1Dtl(RmFormF1Dtl FormF1Dtl)
        {
            try
            {
                _context.Set<RmFormF1Dtl>().Attach(FormF1Dtl);
                _context.Entry<RmFormF1Dtl>(FormF1Dtl).State = EntityState.Modified;
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }


        public async Task<FORMF1Rpt> GetReportData(int headerid)
        {
            FORMF1Rpt result = (from s in _context.RmFormF1Hdr
                                where s.Ff1hPkRefNo == headerid && s.Ff1hActiveYn == true
                                select new FORMF1Rpt
                                {
                                    //CrewLeader = s.Ff1hCrewName,
                                    //District = s.Ff1hDist,
                                    //InspectedByDesignation = s.Ff1hInspectedDesig,
                                    InspectedByName = s.Ff1hInspectedName,
                                    InspectedDate = s.Ff1hInspectedDate,
                                    Division = s.Ff1hDivCode,
                                    RMU = (from r in _context.RmDdLookup where r.DdlType == "RMU" && r.DdlTypeCode == s.Ff1hRmuCode select r.DdlTypeDesc).FirstOrDefault(),
                                    RoadCode = s.Ff1hRdCode,
                                    RoadName = s.Ff1hRdName,
                                    RoadLength = s.Ff1hRoadLength
                                }).FirstOrDefault();


            result.Details = (from d in _context.RmFormF1Dtl
                              where d.Ff1dFf1hPkRefNo == headerid
                              orderby d.Ff1dPkRefNo descending
                              select new FORMF1RptDetail
                              {
                                  Descriptions = d.Ff1dDescription,
                                  LocationChKm = d.Ff1dLocCh,
                                  LocationChM = d.Ff1dLocChDeci,
                                  //Width = d.Ff1dWidth,
                                  //Height = d.Ff1dHeight,
                                  //Condition = d.Ff1dConditionI,
                                  //Bound = d.Ff1dBound,
                                  StructCode = d.Ff1dCode
                              }).ToArray();
            return result;

        }


    }
}
