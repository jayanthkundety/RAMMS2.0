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
                             RMU = r.RdmRmuCode,
                             RoadCode = hdr.FmtRdCode,
                             RoadName = hdr.FmtRdName,
                             TotalPC = vehicle.Where(x => x.FmtvVechicleType == "PC").Sum(y => y.FmtvCount),
                             TotalHV = vehicle.Where(x => x.FmtvVechicleType == "HV").Sum(y => y.FmtvCount),
                             TotalMC = vehicle.Where(x => x.FmtvVechicleType == "MC").Sum(y => y.FmtvCount),
                             Status = hdr.FmtStatus,
                             Recordedby = hdr.FmtUsernameRcd,
                             Headedby = hdr.FmtUsernameHdd
                         });



            query = query.OrderByDescending(x => x.RefNo);
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
                PkRefNo = s.RefNo,
                RefId = s.RefId,
                RMU = s.RMU,
                RoadCode = s.RoadCode,
                RoadName = s.RoadName,
                Status = s.Status,
                TotalHV = s.TotalHV,
                TotalMC = s.TotalMC,
                TotalPC = s.TotalPC

            }).ToList();
        }


        public async Task<List<FormTDtlGridDTO>> GetFormTDtlGridList(FilteredPagingDefinition<FormTDtlResponseDTO> filterOptions)
        {
            List<RmFormTDailyInspection> result = new List<RmFormTDailyInspection>();

            var query = from x in _context.RmFormTDailyInspection
                        let PC = (from p in _context.RmFormTVechicle where p.FmtvFmtdiPkRefNo == x.FmtdiPkRefNo && p.FmtvVechicleType == "PC" select p.FmtvCount ?? 0).DefaultIfEmpty().Sum()
                        let HV = (from p in _context.RmFormTVechicle where p.FmtvFmtdiPkRefNo == x.FmtdiPkRefNo && p.FmtvVechicleType == "HV" select p.FmtvCount ?? 0).DefaultIfEmpty().Sum()
                        let MC = (from p in _context.RmFormTVechicle where p.FmtvFmtdiPkRefNo == x.FmtdiPkRefNo && p.FmtvVechicleType == "MC" select p.FmtvCount ?? 0).DefaultIfEmpty().Sum()
                        where x.FmtdiFmtPkRefNo == filterOptions.Filters.FmtPkRefNo
                        orderby x.FmtdiPkRefNo descending
                        select new { x, PC, HV, MC };


            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderBy(x => x.x.FmtdiPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(x => x.x.FmtdiInspectionDate);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(x => x.x.FmtdiAuditTimeFrm);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(x => x.x.FmtdiAuditTimeTo);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(x => x.x.FmtdiDescription);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(x => x.PC);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(x => x.HV);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderBy(x => x.MC);

            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderByDescending(x => x.x.FmtdiPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(x => x.x.FmtdiInspectionDate);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(x => x.x.FmtdiAuditTimeFrm);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(x => x.x.FmtdiAuditTimeTo);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(x => x.x.FmtdiDescription);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(x => x.PC);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(x => x.HV);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderByDescending(x => x.MC);

            }

            var list = await query.ToListAsync();

            int sl = 1;
            return list.Select(s => new FormTDtlGridDTO
            {
                Date = s.x.FmtdiInspectionDate,
                Description = s.x.FmtdiDescription,
                FromTime = s.x.FmtdiAuditTimeFrm,
                ToTime = s.x.FmtdiAuditTimeTo,
                HV = s.HV,
                MC = s.MC,
                PC = s.PC,
                PkRefNo = s.x.FmtdiPkRefNo,
                SNo = sl++

            }).ToList();

        }



        public RmFormTDailyInspection GetFormTDtlById(int id)
        {
            RmFormTDailyInspection res = (from DI in _context.RmFormTDailyInspection where DI.FmtdiPkRefNo == id select DI).FirstOrDefault();

            res.RmFormTVechicle = (from DI in _context.RmFormTVechicle where DI.FmtvFmtdiPkRefNo == id select DI).ToList();

            return res;
        }


        public int? DeleteFormT(int id)
        {
            try
            {

                IList<RmFormTDailyInspection> child = (from r in _context.RmFormTDailyInspection where r.FmtdiFmtPkRefNo == id select r).ToList();
                foreach (var item in child)
                {
                    IList<RmFormTVechicle> vech = (from r in _context.RmFormTVechicle where r.FmtvFmtdiPkRefNo == item.FmtdiPkRefNo select r).ToList();
                    foreach (var Vitem in vech)
                    {
                        _context.Remove(Vitem);
                        _context.SaveChanges();
                    }
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
            IList<RmFormTVechicle> vech = (from r in _context.RmFormTVechicle where r.FmtvFmtdiPkRefNo == Id select r).ToList();
            foreach (var item in vech)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
            var res = (from r in _context.RmFormTDailyInspection where r.FmtdiPkRefNo == Id select r).SingleOrDefault();
            if (res != null)
            {
                _context.Entry(res).State = EntityState.Deleted;
                _context.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int? SaveFormTDtl(RmFormTDailyInspection FormTDtl, List<RmFormTVechicle> Vechicles)
        {
            try
            {
                _context.Entry<RmFormTDailyInspection>(FormTDtl).State = FormTDtl.FmtdiPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                foreach (var item in Vechicles)
                {
                    item.FmtvFmtdiPkRefNo = FormTDtl.FmtdiPkRefNo;
                    _context.RmFormTVechicle.Add(item);
                    _context.SaveChanges();
                }

                return FormTDtl.FmtdiPkRefNo;
            }
            catch (Exception ex)
            {
                return 500;

            }
        }

        public int? UpdateFormTDtl(RmFormTDailyInspection FormTDtl, List<RmFormTVechicle> Vechicles)
        {
            try
            {
                _context.Set<RmFormTDailyInspection>().Attach(FormTDtl);
                _context.Entry<RmFormTDailyInspection>(FormTDtl).State = EntityState.Modified;
                _context.SaveChanges();

                IList<RmFormTVechicle> child = (from r in _context.RmFormTVechicle where r.FmtvFmtdiPkRefNo == FormTDtl.FmtdiPkRefNo select r).ToList();
                foreach (var item in child)
                {
                    _context.Remove(item);
                    _context.SaveChanges();
                }
                foreach (var item in Vechicles)
                {
                    item.FmtvPkRefNo = 0;
                    _context.RmFormTVechicle.Add(item);
                    _context.SaveChanges();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }


        public async Task<FORMTRpt> GetReportData(int headerid)
        {
            int ? pkrefno =  (from s in _context.RmFormTDailyInspection
                                           where s.FmtdiPkRefNo == headerid
                                           select s.FmtdiFmtPkRefNo).FirstOrDefault();

            FORMTRpt result = (from s in _context.RmFormTHdr
                               where s.FmtPkRefNo == pkrefno
                               select new FORMTRpt
                               {
                                   InspectedDate = s.FmtInspectionDate,
                                   RMU = (from r in _context.RmDdLookup where r.DdlType == "RMU" && r.DdlTypeCode == s.FmtRmuCode select r.DdlTypeDesc).FirstOrDefault(),
                                   RoadCode = s.FmtRdCode,
                                   RoadName = s.FmtRdName,
                                   HdDate = s.FmtDateHdd,
                                   HdName = s.FmtUsernameHdd,
                                   HdDesg = s.FmtDesignationHdd,
                                   RecDate = s.FmtDateRcd,
                                   RecDesg = s.FmtDesignationRcd,
                                   RecName = s.FmtUsernameRcd,
                                   RefId = s.FmtPkRefId,
                                   RefNo = s.FmtReferenceNo,
                               }).FirstOrDefault();


            result.Details = (from d in _context.RmFormTDailyInspection
                              where d.FmtdiPkRefNo == headerid
                              select new FORMTRptDetail
                              {
                                  Day = d.FmtdiDay,
                                  Description = d.FmtdiDescription,
                                  DescriptionHV = d.FmtdiDescriptionHv,
                                  DescriptionMC = d.FmtdiDescriptionMc,
                                  DescriptionPC = d.FmtdiDescriptionPc,
                                  DirectionFrom = d.FmtdiDirectionFrm,
                                  DirectionTo = d.FmtdiDirectionTo,
                                  FromTime = d.FmtdiAuditTimeFrm,
                                  HourlycountPerDay = d.FmtdiHourlyCountPerDay,
                                  TotalDay = d.FmtdiTotalDay,
                                  ToTime = d.FmtdiAuditTimeTo

                              }).FirstOrDefault();


            result.Details.Vechilce = (from v in _context.RmFormTVechicle
                                       where v.FmtvFmtdiPkRefNo == headerid
                                       select new FORMTRptVechicle
                                       {
                                           Axle = v.FmtvAxle,
                                           Count = v.FmtvCount,
                                           Loading = v.FmtvLoading,
                                           Time = Convert.ToInt32(v.FmtvTime),
                                           VechicleType = v.FmtvVechicleType
                                       }).ToArray();
            return result;

        }


    }
}
