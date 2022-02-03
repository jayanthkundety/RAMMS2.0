using System;
using RAMMS.Domain.Models;
using RAMS.Repository;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using RAMMS.DTO.RequestBO;
using Microsoft.EntityFrameworkCore;

namespace RAMMS.Repository
{
    public class FormS2DetailRepository : RepositoryBase<RmFormS2Dtl>
    {

        public FormS2DetailRepository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int LastDetailInsertedNo(int headerId)
        {
            var result = _context.RmFormS2Dtl.Where(s => s.FsiidFsiihPkRefNo == headerId).Count();
            return result;
        }

        public async Task<List<FormS2DetailResponseDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormS2DetailSearchDto> filterOptions)
        {
            var query = await (from x in _context.RmFormS2Dtl
                               let r = _context.RmRoadMaster.FirstOrDefault(s => s.RdmPkRefNo == x.FsiidRoadId)
                               let p = _context.RmDdLookup.FirstOrDefault(s => s.DdlPkRefNo == (x.FsiidPriority.HasValue ? x.FsiidPriority.Value : 0))
                               where
                               x.FsiidFsiihPkRefNo == filterOptions.Filters.HeaderId && x.FsiidActiveYn == true
                               select new FormS2DetailResponseDTO
                               {
                                   Adp = x.FsiidAdp,
                                   CrewDayRequired = x.FsiidCrwDaysReq,
                                   Id = x.FsiidPkRefNo,
                                   PavedLength = r != null ? r.RdmLengthPaved : null,
                                   Priority = p != null ? p.DdlTypeValue : "",
                                   WorkQty = x.FsiidWorkQty,
                                   Remarks = x.FsiidRemarks,
                                   RoadCode = r != null ? r.RdmRdCode : "",
                                   RoadLocationSequence = x.FsiidRdLocSeq,
                                   RoadName = r != null ? r.RdmRdName : "",
                                   Target = x.FsiidTargetPercent,
                                   UnPavedLength = r != null ? r.RdmLengthUnpaved : null,
                                   HeaderId = x.FsiidFsiihPkRefNo,
                                   ReferenceNo = x.FsiidRefId
                               }).Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            foreach (var q in query)
            {
                q.Weeks = (from cal in _context.RmFormS2QuarDtl
                           join c in _context.RmWeekLookup on cal.FsiiqdClkPkRefNo equals c.ClkPkRefNo
                           where cal.FsiiqdFsiidPkRefNo == q.Id
                           select c.ClkWeekNo.Value).ToArray();
            }
            return query;
        }

        public async Task<long> GetFilteredRecordCount(FilteredPagingDefinition<FormS2DetailSearchDto> filterOptions)
        {
            var query = await (from x in _context.RmFormS2Dtl
                               where
                               x.FsiidFsiihPkRefNo == filterOptions.Filters.HeaderId && x.FsiidActiveYn == true
                               select x.FsiidPkRefNo).CountAsync();
            return query;
        }
        public async Task<IEnumerable<object>> GetActiveRefId(int activityCode, int roadCodeId)
        {
            return await _context.RmFormS2Dtl.Include(x => x.FsiidFsiihPkRefNoNavigation).Where(x => x.FsiidActiveYn == true && x.FsiidRoadId == roadCodeId && x.FsiidFsiihPkRefNoNavigation.FsiihActId == activityCode).Select(x => new { FsiidPkRefNo = x.FsiidPkRefNo, FsiidRefId = x.FsiidRefId }).ToListAsync();
        }

        public async Task<bool> CheckExistance(int headerId, int rdCode)
        {
            return await _context.RmFormS2Dtl.AnyAsync(x => x.FsiidRoadId == rdCode && x.FsiidFsiihPkRefNo == headerId
                                                        && x.FsiidActiveYn == true);
        }
    }
}
