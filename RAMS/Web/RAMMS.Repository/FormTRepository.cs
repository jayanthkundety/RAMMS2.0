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
    public class FormTRepository : RepositoryBase<RmFormTHdr>, IFormTRepository
    {
        public FormTRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }



       
        public async Task<List<FormTHeaderRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormTSearchGridDTO> filterOptions)
        {



            var query = (from hdr in _context.RmFormTHdr.Where(s => s.FmtActiveYn == true)
                         join r in _context.RmRoadMaster on hdr.FmtRdCode equals r.RdmRdCode
                         let vehicle = _context.RmFormTVechicle.Where(r => r.FmtvFmtdiPkRefNo == hdr.FmtPkRefNo).DefaultIfEmpty()
                        
                         select new
                         {
                             RefNo = hdr.FmtPkRefNo,
                             RefId = hdr.FmtPkRefId,
                             Date = hdr.FmtInspectionDate,
                             RMU = r.RdmRdName,
                             RoadCode = hdr.FmtRdCode,
                             RoadName = hdr.FmtRdName,
                             TotalPC = vehicle.Where(x => x.FmtvVechicleType == "PC").Sum(y => y.FmtvCount),
                             TotalHV = vehicle.Where(x => x.FmtvVechicleType == "HV").Sum(y => y.FmtvCount),
                             TotalMC = vehicle.Where(x => x.FmtvVechicleType == "MC").Sum(y => y.FmtvCount),
                             Status = hdr.FmtStatus,
                             Recordedby = hdr.FmtUsernameRcd,
                             Headedby = hdr.FmtUsernameHdd
                         });



            query = query.OrderByDescending(x => x.RefId);
            var search = filterOptions.Filters;

            if (!string.IsNullOrEmpty(search.RmuCode))
            {
                query = query.Where(s => s.RMU == search.RmuCode);
            }
            if (!string.IsNullOrEmpty(search.RoadCode))
            {
                query = query.Where(s => s.RoadCode == search.RoadCode);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.FromInspectionDate) && !string.IsNullOrEmpty(filterOptions.Filters.ToInspectionDate))
            {
                DateTime dtFrom, dtTo;
                DateTime.TryParseExact(filterOptions.Filters.FromInspectionDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtFrom);
                DateTime.TryParseExact(filterOptions.Filters.ToInspectionDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtTo);

                {
                    query = query.Where(x => x.Date.HasValue ? x.Date >= dtFrom && x.Date <= dtTo : false);
                }
            }


            if (!string.IsNullOrEmpty(filterOptions.Filters.FromInspectionDate) && string.IsNullOrEmpty(filterOptions.Filters.ToInspectionDate))
            {
                DateTime dt;
                if (DateTime.TryParseExact(filterOptions.Filters.ToInspectionDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                {
                    query = query.Where(x => x.Date.HasValue ? (x.Date.Value.Year == dt.Year && x.Date.Value.Month == dt.Month && x.Date.Value.Day == dt.Day) : false);
                }
            }

            if (string.IsNullOrEmpty(filterOptions.Filters.FromInspectionDate) && !string.IsNullOrEmpty(filterOptions.Filters.ToInspectionDate))
            {
                DateTime dt;
                if (DateTime.TryParseExact(filterOptions.Filters.ToInspectionDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                {
                    query = query.Where(x => x.Date.HasValue ? (x.Date.Value.Year == dt.Year && x.Date.Value.Month == dt.Month && x.Date.Value.Day == dt.Day) : false);
                }
            }

            if (!string.IsNullOrEmpty(search.SmartSearch))
            {

                DateTime dt;
                if (DateTime.TryParseExact(search.SmartSearch, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                {
                    query = query.Where(s =>
                    (s.RefId.Contains(search.SmartSearch) ||
                    s.RMU.Contains(search.SmartSearch) ||
                    s.RoadCode.Contains(search.SmartSearch) ||
                    s.RoadName.Contains(search.SmartSearch) ||
                    s.Recordedby.Contains(search.SmartSearch) ||
                    s.Headedby.Contains(search.SmartSearch)) ||
                    s.Status.Contains(search.SmartSearch) ||

                    (s.Date.HasValue ? (s.Date.Value.Year == dt.Year && s.Date.Value.Month == dt.Month && s.Date.Value.Day == dt.Day) : true) && s.Date != null);
                }
                else
                {
                    query = query.Where(s =>
                     (s.RefId.Contains(search.SmartSearch) ||
                    s.RMU.Contains(search.SmartSearch) ||
                    s.RoadCode.Contains(search.SmartSearch) ||
                    s.RoadName.Contains(search.SmartSearch) ||
                    s.Recordedby.Contains(search.SmartSearch) ||
                    s.Headedby.Contains(search.SmartSearch)) ||
                    s.Status.Contains(search.SmartSearch)

                     );
                }

            }

            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(s => s.RefNo);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(s => s.Date);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(s => s.RMU);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(s => s.RoadCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(s => s.RoadName);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(s => s.TotalPC);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderBy(s => s.TotalHV);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderBy(s => s.TotalMC);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderBy(s => s.Status);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderBy(s => s.Recordedby);
                if (filterOptions.ColumnIndex == 12)
                    query = query.OrderBy(s => s.Headedby);

            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(s => s.RefNo);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(s => s.Date);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(s => s.RMU);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(s => s.RoadCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(s => s.RoadName);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(s => s.TotalPC);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderByDescending(s => s.TotalHV);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderByDescending(s => s.TotalMC);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderByDescending(s => s.Status);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderByDescending(s => s.Recordedby);
                if (filterOptions.ColumnIndex == 12)
                    query = query.OrderByDescending(s => s.Headedby);

            }




            var list = await query.Skip(filterOptions.StartPageNo)
  .Take(filterOptions.RecordsPerPage)
  .ToListAsync();

            return list.Select(s => new FormTHeaderRequestDTO
            {
                Date = s.Date,
                Headedby = s.Headedby,
                Recordedby = s.Headedby,
                RefNo = s.RefNo,
                RefId = s.RefId,
                RMU = s.RMU,
                RoadCode = s.RoadCode,
                RoadName = s.RoadCode,
                Status = s.Status,
                TotalHV = s.TotalHV,
                TotalMC = s.TotalMC,
                TotalPC = s.TotalPC

            }).ToList();
        }


        public async Task<List<FormTDtlGridDTO>> GetFormTDtlGridList(FilteredPagingDefinition<FormTDtlResponseDTO> filterOptions)
        {
            List<RmFormTDailyInspection> result = new List<RmFormTDailyInspection>();

            //var query = from x in _context.RmFormTDailyInspection
            //            join a in _context.RmAllassetInventory on x.Ff1dAssetId equals a.AiAssetId
            //            where x.Ff1dFmtPkRefNo == filterOptions.Filters.FmtPkRefNo
            //            orderby x.FmtdiPkRefNo descending
            //            select new { x, a };


            //if (filterOptions.sortOrder == SortOrder.Ascending)
            //{
            //    if (filterOptions.ColumnIndex == 1)
            //        query = query.OrderBy(x => x.x.FmtdiPkRefNo);
            //    if (filterOptions.ColumnIndex == 2)
            //        query = query.OrderBy(x => x.x.Ff1dLocCh);
            //    if (filterOptions.ColumnIndex == 3)
            //        query = query.OrderBy(x => x.x.Ff1dCode);
            //    if (filterOptions.ColumnIndex == 4)
            //        query = query.OrderBy(x => x.x.Ff1dOverallCondition);
            //    if (filterOptions.ColumnIndex == 5)
            //        query = query.OrderBy(x => x.a.AiBound);
            //    if (filterOptions.ColumnIndex == 6)
            //        query = query.OrderBy(x => x.a.AiWidth);
            //    if (filterOptions.ColumnIndex == 7)
            //        query = query.OrderBy(x => x.a.AiHeight);
            //    if (filterOptions.ColumnIndex == 8)
            //        query = query.OrderBy(x => x.x.Ff1dDescription);

            //}
            //else if (filterOptions.sortOrder == SortOrder.Descending)
            //{
            //    if (filterOptions.ColumnIndex == 1)
            //        query = query.OrderByDescending(x => x.x.FmtdiPkRefNo);
            //    if (filterOptions.ColumnIndex == 2)
            //        query = query.OrderByDescending(x => x.x.Ff1dLocCh);
            //    if (filterOptions.ColumnIndex == 3)
            //        query = query.OrderByDescending(x => x.x.Ff1dCode);
            //    if (filterOptions.ColumnIndex == 4)
            //        query = query.OrderByDescending(x => x.x.Ff1dOverallCondition);
            //    if (filterOptions.ColumnIndex == 5)
            //        query = query.OrderByDescending(x => x.a.AiBound);
            //    if (filterOptions.ColumnIndex == 6)
            //        query = query.OrderByDescending(x => x.a.AiWidth);
            //    if (filterOptions.ColumnIndex == 7)
            //        query = query.OrderByDescending(x => x.a.AiHeight);
            //    if (filterOptions.ColumnIndex == 8)
            //        query = query.OrderByDescending(x => x.x.Ff1dDescription);

            //}

            //var list = await query.ToListAsync();


            //return list.Select(s => new FormTDtlGridDTO
            //{

            //    AssetId = s.a.AiAssetId,
            //    Ch = s.x.Ff1dLocCh + "+" + s.x.Ff1dLocChDeci,
            //    OverallCondition = Convert.ToInt32(s.x.Ff1dOverallCondition),
            //    Description = s.x.Ff1dDescription,
            //    FrmCh = s.x.Ff1dLocCh,
            //    FrmChDec = s.x.Ff1dLocChDeci,
            //    Height = s.a.AiHeight,
            //    PkRefNo = s.x.FmtdiPkRefNo,
            //    StructureCode = s.a.AiStrucCode,
            //    Length = s.a.AiLength,
            //    Width = s.a.AiWidth,
            //    BottomWidth = s.a.AiBotWidth,
            //    Tier = s.a.AiTier

            //}).ToList();

            return null;
        }


        public List<RmAllassetInventory> GetAssetDetails(FormTResponseDTO FormT)
        {


            return (from r in _context.RmAllassetInventory.Where(s => s.AiStrucCode == "Y" && s.AiRmuCode == FormT.RmuCode  && s.AiRdCode == FormT.RdCode) select r).ToList();


        }

        public int LoadR1Data(FormTResponseDTO FormT)
        {
            try
            {

                var res = (from r1 in _context.RmFormR1Hdr
                           join a in _context.RmAllassetInventory on r1.Fr1hAidPkRefNo equals a.AiPkRefNo
                           where    r1.Fr1hAiRdCode == FormT.RdCode && r1.Fr1hActiveYn == true
                           select new RmFormTDailyInspection
                           {
                               //Ff1dFmtPkRefNo = FormT.PkRefNo,
                               //Ff1dAssetId = a.AiAssetId,
                               FmtdiPkRefNo = r1.Fr1hPkRefNo,
                               //Ff1dTier = Convert.ToInt32(a.AiTier),
                               //Ff1dCode = a.AiStrucCode,
                               //Ff1dHeight = Convert.ToDecimal(a.AiHeight),
                               //Ff1dTopWidth = Convert.ToDecimal(a.AiWidth),
                               //Ff1dOverallCondition = r1.Fr1hCondRating,
                               //Ff1dLocCh = a.AiLocChKm,
                               //Ff1dLocChDeci = a.AiLocChM == "" ? 0 : Convert.ToInt32(a.AiLocChM)

                           }).ToList();

                foreach (var item in res)
                {
                    _context.RmFormTDailyInspection.Add(item);
                    _context.SaveChanges();
                }

                return 1;
            }
            catch (Exception)
            {
                return 500;

            }
        }


        public async Task<RmFormTHdr> SaveFormT(RmFormTHdr FormT)
        {
            try
            {

                //var search = (from r1 in _context.RmFormR1Hdr
                //              where r1.Fr1hAiDivCode == FormT.Ff1hDivCode && r1.Fr1hAiRmuCode == FormT.Ff1hRmuCode && r1.Fr1hYearOfInsp == FormT.Ff1hInspectedYear && r1.Fr1hAiRdCode == FormT.Ff1hRdCode && r1.Fr1hActiveYn == true
                //              select r1);

                //if (search.Count() > 0)
                //{
                //    _context.RmFormTHdr.Add(FormT);
                //    _context.SaveChanges();

                //    var res = (from r1 in _context.RmFormR1Hdr
                //               join a in _context.RmAllassetInventory on r1.Fr1hAidPkRefNo equals a.AiPkRefNo
                //               where r1.Fr1hAiDivCode == FormT.Ff1hDivCode && r1.Fr1hAiRmuCode == FormT.Ff1hRmuCode && r1.Fr1hYearOfInsp == FormT.Ff1hInspectedYear && r1.Fr1hAiRdCode == FormT.Ff1hRdCode && r1.Fr1hActiveYn == true
                //               select new RmFormTDailyInspection
                //               {
                //                   Ff1dFmtPkRefNo = FormT.FmtPkRefNo,
                //                   Ff1dAssetId =r1.Fr1hAssetId,
                //                   Ff1dR1hPkRefNo = r1.Fr1hPkRefNo,
                //                   Ff1dTier = Convert.ToInt32(a.AiTier),
                //                   Ff1dCode = a.AiStrucCode,
                //                   Ff1dHeight = Convert.ToDecimal(a.AiHeight),
                //                   Ff1dTopWidth = Convert.ToDecimal(a.AiWidth),
                //                   Ff1dOverallCondition = r1.Fr1hCondRating,
                //                   Ff1dLocCh = a.AiLocChKm,
                //                   Ff1dLocChDeci = a.AiLocChM == "" ? 0 : Convert.ToInt32(a.AiLocChM)

                //               }).ToList();

                //    foreach (var item in res)
                //    {
                //        _context.RmFormTDailyInspection.Add(item);
                //        _context.SaveChanges();
                //    }

                //}

                return FormT;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int? DeleteFormT(int id)
        {
            try
            {
                IList<RmFormTDailyInspection> child = (from r in _context.RmFormTDailyInspection where r.FmtdiPkRefNo == id select r).ToList();
                foreach (var item in child)
                {
                    _context.Remove(item);
                    _context.SaveChanges();
                }

                var res = _context.Set<RmFormTHdr>().FindAsync(id);
                res.Result.FmtActiveYn = false;
                _context.Set<RmFormTHdr>().Attach(res.Result);
                _context.Entry<RmFormTHdr>(res.Result).State = EntityState.Modified;
                _context.SaveChanges();
                return 1;

            }
            catch (Exception ex)
            {
                return 500;
            }
        }


        public int? DeleteFormTDtl(int Id)
        {
            var res = (from r in _context.RmFormTDailyInspection where r.FmtdiPkRefNo == Id select r).SingleOrDefault();
            if (res != null)
            {
                _context.Entry(res).State = EntityState.Deleted;
                _context.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int? SaveFormTDtl(RmFormTDailyInspection FormTDtl)
        {
            try
            {


                _context.Entry<RmFormTDailyInspection>(FormTDtl).State = FormTDtl.FmtdiPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return FormTDtl.FmtdiPkRefNo;
            }
            catch (Exception ex)
            {
                return 500;

            }
        }

        public int? UpdateFormTDtl(RmFormTDailyInspection FormTDtl)
        {
            try
            {
                _context.Set<RmFormTDailyInspection>().Attach(FormTDtl);
                _context.Entry<RmFormTDailyInspection>(FormTDtl).State = EntityState.Modified;
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }


        //public async Task<FORMTRpt> GetReportData(int headerid)
        //{
        //    FORMTRpt result = (from s in _context.RmFormTHdr
        //                        where s.FmtPkRefNo == headerid && s.Ff1hActiveYn == true
        //                        select new FORMTRpt
        //                        {
        //                            CrewLeader = s.Ff1hCrewName,
        //                            District = s.Ff1hDist,
        //                            InspectedByDesignation = s.Ff1hInspectedDesg,
        //                            InspectedByName = s.Ff1hInspectedName,
        //                            InspectedDate = s.Ff1hInspectedDate,
        //                            Division = s.Ff1hDivCode,
        //                            RMU = (from r in _context.RmDdLookup where r.DdlType == "RMU" && r.DdlTypeCode == s.Ff1hRmuCode select r.DdlTypeDesc).FirstOrDefault(),
        //                            RoadCode = s.Ff1hRdCode,
        //                            RoadName = s.Ff1hRdName,
        //                            RoadLength = s.Ff1hRoadLength
        //                        }).FirstOrDefault();


        //    result.Details = (from d in _context.RmFormTDailyInspection
        //                      where d.Ff1dFmtPkRefNo == headerid
        //                      orderby d.FmtdiPkRefNo descending
        //                      select new FORMTRptDetail
        //                      {
        //                          Descriptions = d.Ff1dDescription,
        //                          LocationChKm = d.Ff1dLocCh,
        //                          LocationChM = d.Ff1dLocChDeci,
        //                          Width = d.Ff1dTopWidth,
        //                          BottomWidth = d.Ff1dBottomWidth,
        //                          Height = d.Ff1dHeight,
        //                          Condition = d.Ff1dOverallCondition,
        //                          Tier = d.Ff1dTier,
        //                          StructCode = d.Ff1dCode
        //                      }).ToArray();
        //    return result;

        //}


    }
}
