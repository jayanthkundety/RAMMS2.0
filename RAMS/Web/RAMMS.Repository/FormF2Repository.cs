using System;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System.Linq;
using RAMMS.Common;
using Microsoft.EntityFrameworkCore;
using RAMMS.DTO.Wrappers;
using RAMMS.DTO.RequestBO;
using System.Collections.Generic;
using AutoMapper;
using RAMMS.DTO;
using RAMMS.DTO.Report;

namespace RAMMS.Repository
{
    public class FormF2Repository : RepositoryBase<RmFormF2GrInsHdr>, IFormF2Repository
    {
        private readonly IMapper _mapper;
        public FormF2Repository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<decimal?> TotalLength(string roadcode)
        {
            //double total = await (from s in _context.RmAllassetInventory
            //                      where s.AiRdCode == roadcode
            //                      && s.AiAssetGrpCode == "GR"
            //                      select s.AiLength.HasValue ? s.AiLength.Value : 0).SumAsync();
            //return total;
            var total = await (from s in _context.RmRoadMaster
                               where s.RdmRdCode == roadcode
                               select new { s.RdmLengthPaved, s.RdmLengthUnpaved }).FirstOrDefaultAsync();
            decimal? t = total.RdmLengthPaved + total.RdmLengthUnpaved;
            return t;
        }

        //public async Task<decimal?> HdrTotalLength(string roadcode)
        //{
        //    var total = await (from s in _context.RmRoadMaster
        //                          where s.RdmRdCode == roadcode
        //                          select new { s.RdmLengthPaved, s.RdmLengthUnpaved }).FirstOrDefaultAsync();
        //    decimal? t = total.RdmLengthPaved + total.RdmLengthUnpaved;
        //    return t;
        //}

        public async Task<long> BulkDetailInsert(int headerid, int createdBy)
        {
            var mst = await _context.RmFormF2GrInsHdr.FirstOrDefaultAsync(s => s.FgrihPkRefNo == headerid);

            if (mst != null)
            {

                var detail = (from s in _context.RmAllassetInventory
                              where s.AiRdCode == mst.FgrihRoadCode
                              && s.AiAssetGrpCode == "GR" && s.AiActiveYn==true
                              select new RmFormF2GrInsDtl
                              {
                                  FgridFgrihPkRefNo = headerid,
                                  FgridActiveYn = true,
                                  FgridCrBy = createdBy,
                                  FgridCrDt = DateTime.UtcNow,
                                  FgridGrCode = s.AiStrucCode,
                                  FgridGrCondition1 = null,
                                  FgridGrCondition2 = null,
                                  FgridGrCondition3 = null,
                                  FgridModBy = createdBy,
                                  FgridModDt = DateTime.UtcNow,
                                  FgridPostSpac = s.AiPostSpacing,
                                  FgridRemarks = "",
                                  FgridRhsMLhs = s.AiBound,
                                  FgridStartingChKm = s.AiLocChKm,
                                  FgridStartingChM = s.AiLocChM,
                                  FgridSubmitSts = false,
                                  FgrihAiPkRefNo = s.AiPkRefNo,
                                  FgridLength=s.AiLength
                              }).ToArray();
                var existingIds = _context.RmFormF2GrInsDtl.Where(d => d.FgridFgrihPkRefNo == headerid).Select(d => d.FgrihAiPkRefNo).ToArray();
                detail = detail.Where(s => !existingIds.Contains(s.FgrihAiPkRefNo)).ToArray();
                if (detail.Length > 0)
                {
                    _context.RmFormF2GrInsDtl.AddRange(detail);
                    return await _context.SaveChangesAsync();
                }
                return 0;
            }
            else
            {
                return 0;
            }
        }

        public async Task<long> GetFilteredRecordCount(FilteredPagingDefinition<FormF2SearchGridDTO> filterOptions)
        {
            var roads = (from a in _context.RmAllassetInventory
                         where filterOptions.Filters.AssertType != "" ? a.AiGrpType == filterOptions.Filters.AssertType : a.AiGrpType.Contains(filterOptions.Filters.SmartSearch) && a.AiActiveYn == true
                         select a.AiRdCode).ToArray();

            var query = (from s in _context.RmFormF2GrInsHdr
                         join d in _context.RmRoadMaster on s.FgrihRoadId equals d.RdmPkRefNo
                         where s.FgrihActiveYn == true
                         select new { s, d });
            var search = filterOptions.Filters;
            if (search.SecCode.HasValue)
            {
                query = query.Where(s => s.d.RdmSecCode == search.SecCode);
            }
            if (!string.IsNullOrEmpty(search.AssertType))
            {
                //query = query.Where(s => roads.Contains(s.d.RdmRdCode));
                query = query.Where(s => s.s.RmFormF2GrInsDtl.Any(x=> x.FgridGrCode==search.AssertType && x.FgridActiveYn==true));
            }
            if (!string.IsNullOrEmpty(search.RmuCode))
            {
                query = query.Where(s => s.d.RdmRmuCode == search.RmuCode);
            }
            if (!string.IsNullOrEmpty(search.RoadCode))
            {
                query = query.Where(s => s.s.FgrihRoadCode == search.RoadCode);
            }
            if (search.Year.HasValue)
            {
                query = query.Where(s => s.s.FgrihYearOfInsp == search.Year.Value);
            }

            if (search.FromYear.HasValue)
            {
                query = query.Where(s => s.s.FgrihYearOfInsp >= search.FromYear);
            }
            if (search.ToYear.HasValue)
            {
                query = query.Where(s => s.s.FgrihYearOfInsp <= search.ToYear);
            }

            if (search.FromChKM.HasValue || !string.IsNullOrEmpty(search.FromChM))
            {
                query = query.Where(s => s.s.RmFormF2GrInsDtl.Any(x => Convert.ToDouble(x.FgridStartingChKm.ToString() + '.' + x.FgridStartingChM) >= Convert.ToDouble(search.FromChKM.ToString() + '.' + search.FromChM)));
            }

            if (search.ToChKM.HasValue || !string.IsNullOrEmpty(search.ToChM))
            {
                query = query.Where(s => s.s.RmFormF2GrInsDtl.Any(x => Convert.ToDouble(x.FgridStartingChKm.ToString() + '.' + x.FgridStartingChM) <= Convert.ToDouble(search.ToChKM.ToString() + '.' + search.ToChM)));
            }

            if (!string.IsNullOrEmpty(search.Bound))
            {
                query = query.Where(s => s.s.RmFormF2GrInsDtl.Any(x => x.FgridRhsMLhs == search.Bound));
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
                    (s.s.FgrihFormRefId.Contains(search.SmartSearch) ||
                    s.s.FgrihRoadCode.Contains(search.SmartSearch) ||
                    s.d.RdmRmuCode.Contains(search.SmartSearch) ||
                    s.d.RdmRmuName.Contains(search.SmartSearch) ||
                    s.d.RdmDivCode.Contains(search.SmartSearch) ||
                    s.s.FgrihRoadName.Contains(search.SmartSearch)) ||
                    s.d.RdmSecName.Contains(search.SmartSearch) ||
                    s.s.FgrihDist.Contains(search.SmartSearch) ||
                    //roads.Contains(s.d.RdmRdCode) ||
                    s.s.FgrihUserNameInspBy.Contains(search.SmartSearch) ||
                    s.s.FgrihCrewLeaderName.Contains(search.SmartSearch) ||
                    (s.s.FgrihDtOfInsp.HasValue ? (s.s.FgrihDtOfInsp.Value.Year == dt.Year && s.s.FgrihDtOfInsp.Value.Month == dt.Month && s.s.FgrihDtOfInsp.Value.Day == dt.Day) : true) && s.s.FgrihDtOfInsp != null);
                }
                else
                {
                    query = query.Where(s =>
                     (s.s.FgrihFormRefId.Contains(search.SmartSearch) ||
                     s.s.FgrihRoadCode.Contains(search.SmartSearch) ||
                     s.d.RdmRmuCode.Contains(search.SmartSearch) ||
                     s.d.RdmRmuName.Contains(search.SmartSearch) ||
                     s.d.RdmDivCode.Contains(search.SmartSearch) ||
                     s.s.FgrihRoadName.Contains(search.SmartSearch)) ||
                     s.d.RdmSecName.Contains(search.SmartSearch) ||
                     s.s.FgrihDist.Contains(search.SmartSearch) ||
                     //roads.Contains(s.d.RdmRdCode) ||
                     s.s.FgrihUserNameInspBy.Contains(search.SmartSearch) ||
                     s.s.FgrihCrewLeaderName.Contains(search.SmartSearch)
                     );
                }
            }

            return await query.CountAsync();
        }

        public async Task<List<FormF2HeaderRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormF2SearchGridDTO> filterOptions)
        {

            var roads = (from a in _context.RmAllassetInventory
                         where filterOptions.Filters.AssertType != "" ? a.AiGrpType == filterOptions.Filters.AssertType : a.AiGrpType.Contains(filterOptions.Filters.SmartSearch) && a.AiActiveYn==true
                         select a.AiRdCode).ToArray();

            var query = (from s in _context.RmFormF2GrInsHdr
                         join d in _context.RmRoadMaster on s.FgrihRoadId equals d.RdmPkRefNo
                         where s.FgrihActiveYn == true
                         select new { s, d });
            query = query.OrderByDescending(x => x.s.FgrihModDt);
            var search = filterOptions.Filters;
            if (search.SecCode.HasValue)
            {
                query = query.Where(s => s.d.RdmSecCode == search.SecCode);
            }
            if (!string.IsNullOrEmpty(search.AssertType))
            {
                //query = query.Where(s => roads.Contains(s.d.RdmRdCode));
                //query = query.Where(s => s.s.RmFormF2GrInsDtl.Any(x => x.FgridGrCode == search.AssertType));
            }
            if (!string.IsNullOrEmpty(search.RmuCode))
            {
                query = query.Where(s => s.d.RdmRmuCode == search.RmuCode);
            }
            if (!string.IsNullOrEmpty(search.RoadCode))
            {
                query = query.Where(s => s.s.FgrihRoadCode == search.RoadCode);
            }
            if (search.Year.HasValue)
            {
                query = query.Where(s => s.s.FgrihYearOfInsp == search.Year.Value);
            }

            if (search.FromYear.HasValue)
            {
                query = query.Where(s => s.s.FgrihYearOfInsp >= search.FromYear);
            }
            if (search.ToYear.HasValue)
            {
                query = query.Where(s => s.s.FgrihYearOfInsp <= search.ToYear);
            }
            if (!string.IsNullOrEmpty(search.AssertType))
            {
                query = query.Where(s => s.s.RmFormF2GrInsDtl.Any(x => x.FgridGrCode == search.AssertType && x.FgridActiveYn == true));
            }

            if (search.FromChKM.HasValue ||(!string.IsNullOrEmpty(search.FromChM)))
            {
                query = query.Where(s => s.s.RmFormF2GrInsDtl.Any(x=>Convert.ToDouble(x.FgridStartingChKm.ToString()+'.'+ x.FgridStartingChM) >=Convert.ToDouble(search.FromChKM.ToString() +'.'+search.FromChM)));
            }

            if (search.ToChKM.HasValue || (!string.IsNullOrEmpty(search.ToChM)))
            {
                query = query.Where(s => s.s.RmFormF2GrInsDtl.Any(x => Convert.ToDouble(x.FgridStartingChKm.ToString() + '.' + x.FgridStartingChM) <= Convert.ToDouble(search.ToChKM.ToString() + '.' + search.ToChM)));
            }

            if (!string.IsNullOrEmpty(search.Bound))
            {
                query = query.Where(s => s.s.RmFormF2GrInsDtl.Any(x => x.FgridRhsMLhs == search.Bound));
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
                    (s.s.FgrihFormRefId.Contains(search.SmartSearch) ||
                    s.s.FgrihRoadCode.Contains(search.SmartSearch) ||
                    s.d.RdmRmuCode.Contains(search.SmartSearch) ||
                    s.d.RdmRmuName.Contains(search.SmartSearch) ||
                    s.d.RdmDivCode.Contains(search.SmartSearch) ||
                    s.s.FgrihRoadName.Contains(search.SmartSearch)) ||
                    s.d.RdmSecName.Contains(search.SmartSearch) ||
                    s.s.FgrihDist.Contains(search.SmartSearch) ||
                    //roads.Contains(s.d.RdmRdCode) ||
                    s.s.FgrihUserNameInspBy.Contains(search.SmartSearch) ||
                    s.s.FgrihCrewLeaderName.Contains(search.SmartSearch) ||
                    (s.s.FgrihDtOfInsp.HasValue ? (s.s.FgrihDtOfInsp.Value.Year == dt.Year && s.s.FgrihDtOfInsp.Value.Month == dt.Month && s.s.FgrihDtOfInsp.Value.Day == dt.Day) : true) && s.s.FgrihDtOfInsp!=null);
                }
                else
                {
                    query = query.Where(s =>
                     (s.s.FgrihFormRefId.Contains(search.SmartSearch) ||
                     s.s.FgrihRoadCode.Contains(search.SmartSearch) ||
                     s.d.RdmRmuCode.Contains(search.SmartSearch) ||
                     s.d.RdmRmuName.Contains(search.SmartSearch) ||
                     s.d.RdmDivCode.Contains(search.SmartSearch) ||
                     s.s.FgrihRoadName.Contains(search.SmartSearch)) ||
                     s.d.RdmSecName.Contains(search.SmartSearch) ||
                     s.s.FgrihDist.Contains(search.SmartSearch) ||
                     //roads.Contains(s.d.RdmRdCode) ||
                     s.s.FgrihUserNameInspBy.Contains(search.SmartSearch) ||
                     s.s.FgrihCrewLeaderName.Contains(search.SmartSearch)
                     );
                }
                   
            }

            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(s => s.d.RdmRdCdSort);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(s => s.s.FgrihDtOfInsp);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(s => s.s.FgrihUserNameInspBy);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(s => s.s.FgrihDivCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(s => s.s.FgrihDist);
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
                    query = query.OrderBy(s => s.s.FgrihCrewLeaderName);
                //if (filterOptions.ColumnIndex == 0)
                //    query = query.OrderByDescending(s => s.s.FgrihPkRefNo);
            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(s => s.d.RdmRdCdSort);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(s => s.s.FgrihDtOfInsp);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(s => s.s.FgrihUserNameInspBy);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(s => s.s.FgrihDivCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(s => s.s.FgrihDist);
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
                    query = query.OrderByDescending(s => s.s.FgrihCrewLeaderName);
                //if (filterOptions.ColumnIndex == 0)
                //    query = query.OrderByDescending(s => s.s.FgrihPkRefNo);
            }

            var list = await query.Skip(filterOptions.StartPageNo)
               .Take(filterOptions.RecordsPerPage)
               .ToListAsync();

            return list.Select(s => new FormF2HeaderRequestDTO
            {
                ActiveYn = s.s.FgrihActiveYn.Value,
                CrBy = s.s.FgrihCrBy,
                CrDt = s.s.FgrihCrDt,
                CrewLeaderId = s.s.FgrihCrewLeaderId,
                CrewLeaderName = s.s.FgrihCrewLeaderName,
                Dist = s.s.FgrihDist,
                DivCode = s.d.RdmDivCode,
                DtInspBy = s.s.FgrihDtInspBy,
                DtOfInsp = s.s.FgrihDtOfInsp,
                FormRefId = s.s.FgrihFormRefId,
                ModBy = s.s.FgrihModBy,
                PkRefNo = s.s.FgrihPkRefNo,
                RoadCode = s.s.FgrihRoadCode,
                RoadName = s.s.FgrihRoadName,
                RmuCode = s.d.RdmRmuCode,
                RmuName = s.d.RdmRmuName,
                SectionCode = s.d.RdmSecCode,
                SectionName = s.d.RdmSecName,
                RoadLength = s.s.FgrihRoadLength,
                SignpathInspBy = s.s.FgrihSignpathInspBy,
                SubmitSts = s.s.FgrihSubmitSts,
                UserDesignationInspBy = s.s.FgrihUserDesignationInspBy,
                UserIdInspBy = s.s.FgrihUserIdInspBy,
                YearOfInsp = s.s.FgrihYearOfInsp,
                UserNameInspBy = s.s.FgrihUserNameInspBy

            }).ToList();
        }

        public RmFormF2GrInsHdr IsExists(string st)
        {
            var context = _context.RmFormF2GrInsHdr.FirstOrDefault(s => s.FgrihFormRefId == st && s.FgrihActiveYn==true);
            if (context != null)
            {
                return context;
            }
            else
                return null;
        }

        public FORMF2Rpt GetReportData(int headerid)
        {
            FORMF2Rpt result = (from s in _context.RmFormF2GrInsHdr
                                join rm in _context.RmRoadMaster on s.FgrihRoadId equals rm.RdmPkRefNo
                                where s.FgrihPkRefNo == headerid && s.FgrihActiveYn == true
                                select new FORMF2Rpt
                                {
                                    CrewLeader = s.FgrihCrewLeaderName,
                                    District = s.FgrihDist,
                                    InspectedByDesignation = s.FgrihUserDesignationInspBy,
                                    InspectedByName = s.FgrihUserNameInspBy,
                                    InspectedDate = s.FgrihDtOfInsp,
                                    Division = s.FgrihDivCode,
                                    RMU = rm.RdmRmuName,
                                    RoadCode = s.FgrihRoadCode,
                                    RoadName = s.FgrihRoadName,
                                    RoadLength = s.FgrihRoadLength
                                }).FirstOrDefault();

            result.Details = (from d in _context.RmFormF2GrInsDtl
                              where d.FgridFgrihPkRefNo == headerid && d.FgridActiveYn == true
                              select new FORMF2RptDetail
                              {
                                  Code = d.FgridGrCode,
                                  Condition1 = d.FgridGrCondition1,
                                  Condition2 = d.FgridGrCondition2,
                                  Condition3 = d.FgridGrCondition3,
                                  PostSpacing = d.FgridPostSpac,
                                  Remarks = d.FgridRemarks,
                                  RML = d.FgridRhsMLhs,
                                  StartingChKm = d.FgridStartingChKm,
                                  StartingChM = d.FgridStartingChM

                              }).OrderBy(s=>s.StartingChKm).ThenBy(s=>s.StartingChM) .ToArray();
            return result;

        }
    }
}
