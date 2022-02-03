using Microsoft.EntityFrameworkCore;
using RAMMS.Common;
using RAMMS.Common.Extensions;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RAMMS.Repository
{
    public class FormS2Repository: RepositoryBase<RmFormS2Hdr>
    {

        public FormS2Repository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int LastHeaderInsertedNo()
        {
           var result = _context.RmFormS2Hdr.OrderByDescending(s => s.FsiihPkRefNo).FirstOrDefault();
            if (result != null)
                return result.FsiihPkRefNo;
            else
                return 0;
        }

        public async Task<List<S2HeaderResponse>> GetFilteredRecordList(FilteredPagingDefinition<S2HeaderSearchRequestDTO> filterOptions)
        {
            var query = (from x in _context.RmFormS2Hdr
                               let rm = _context.RmDdLookup.FirstOrDefault(s => s.DdlTypeCode == x.FsiihRmu && s.DdlType == "RMU")
                               let r = _context.RmDdLookup.FirstOrDefault(s => s.DdlPkRefNo == x.FsiihActId)
                               let gscuser = _context.RmUsers.FirstOrDefault(s => s.UsrPkId == (x.FsiihUseridSchld.HasValue ? x.FsiihUseridSchld.Value : 0))
                               let gvetuser = _context.RmUsers.FirstOrDefault(s => s.UsrPkId == (x.FsiihUseridVet.HasValue ? x.FsiihUseridVet.Value : 0))
                               let gagruser = _context.RmUsers.FirstOrDefault(s => s.UsrPkId == (x.FsiihUseridAgrd.HasValue ? x.FsiihUseridAgrd.Value : 0))
                               let gpuser = _context.RmUsers.FirstOrDefault(s => s.UsrPkId == (x.FsiihUseridPrioritised.HasValue ? x.FsiihUseridPrioritised.Value : 0))
                               let gsubuser = _context.RmUsers.FirstOrDefault(s => s.UsrPkId == (x.FsiihUseridSub.HasValue ? x.FsiihUseridSub.Value : 0))
                               where x.FsiihQuaterId == (filterOptions.Filters.Quarter.HasValue ? filterOptions.Filters.Quarter.Value : x.FsiihQuaterId)
                               && (filterOptions.Filters.ActivityCode.HasValue ? filterOptions.Filters.ActivityCode.Value : x.FsiihActId) == x.FsiihActId
                               && (filterOptions.Filters.Rmu != "" && filterOptions.Filters.Rmu != null ? filterOptions.Filters.Rmu : x.FsiihRmu) == x.FsiihRmu
                               && (filterOptions.Filters.Year.HasValue ? filterOptions.Filters.Year.Value : x.FsiihYear) == x.FsiihYear
                               && x.FsiihActiveYn == true
                               select new S2HeaderResponse
                               {
                                   ScheduledBy = x.FsiihUserNameSchId,
                                   ActivityCode = x.FsiihActCode,
                                   AgreedBy = x.FsiihUserNameAgrd,
                                   Id = x.FsiihPkRefNo,
                                   PrioritizedBy = x.FsiihUserNamePrioritised,
                                   Quarter = x.FsiihQuaterId == 1 ? "Q1" : x.FsiihQuaterId == 2 ? "Q2" : x.FsiihQuaterId == 3 ? "Q3" : x.FsiihQuaterId == 4? "Q4" : "",
                                   ReferenceNo = x.FsiihRefId,
                                   SubmittedBy = x.FsiihUserNameSub,
                                   VettedBy = x.FsiihUserNameVet,
                                   Year = x.FsiihYear,
                                   Rmu = x.FsiihRmu,
                                   RmuName = rm != null ? rm.DdlTypeDesc : "",
                                   SubmitSts = x.FsiihSubmitSts,
                                   Status = x.FsiihSubmitSts ? "Submitted" : "Saved",
                                   ModifiedOn = x.FsiihModDt,
                                   ProcessStatus = x.FsiihStatus
                               });
            if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInput))
            {
                query = query.Where(s => s.ReferenceNo.Contains(filterOptions.Filters.SmartInput)
                 || s.SubmittedBy.Contains(filterOptions.Filters.SmartInput)
                 || s.ScheduledBy.Contains(filterOptions.Filters.SmartInput)
                 || s.VettedBy.Contains(filterOptions.Filters.SmartInput)
                 || s.AgreedBy.Contains(filterOptions.Filters.SmartInput)
                 || s.ActivityCode.Contains(filterOptions.Filters.SmartInput)
                 || s.PrioritizedBy.Contains(filterOptions.Filters.SmartInput)
                 || s.Year.ToString().Contains(filterOptions.Filters.SmartInput)
                 || s.Quarter.Contains(filterOptions.Filters.SmartInput)
                 || s.Rmu.Contains(filterOptions.Filters.SmartInput)
                 || s.Status.Contains(filterOptions.Filters.SmartInput)
                 || s.RmuName.Contains(filterOptions.Filters.SmartInput));
            }
            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(s => s.ReferenceNo);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(s => s.Rmu);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(s => s.RmuName);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(s => s.Quarter);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(s => s.Year);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(s => s.ActivityCode);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderBy(s => s.SubmitSts);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderBy(s => s.PrioritizedBy);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderBy(s => s.ScheduledBy);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderBy(s => s.SubmittedBy);
                if (filterOptions.ColumnIndex == 12)
                    query = query.OrderBy(s => s.VettedBy);
                if (filterOptions.ColumnIndex == 13)
                    query = query.OrderBy(s => s.AgreedBy);
                if (filterOptions.ColumnIndex == 0)
                    query = query.OrderByDescending(s => s.ModifiedOn);
            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(s => s.ReferenceNo);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(s => s.Rmu);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(s => s.RmuName);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(s => s.Quarter);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(s => s.Year);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(s => s.ActivityCode);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderByDescending(s => s.SubmitSts);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderByDescending(s => s.PrioritizedBy);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderByDescending(s => s.ScheduledBy);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderByDescending(s => s.SubmittedBy);
                if (filterOptions.ColumnIndex == 12)
                    query = query.OrderByDescending(s => s.VettedBy);
                if (filterOptions.ColumnIndex == 13)
                    query = query.OrderByDescending(s => s.AgreedBy);
            }

            var result = await query.Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            
            return result;
        }

        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<S2HeaderSearchRequestDTO> filterOptions)
        {
            var query = (from x in _context.RmFormS2Hdr
                         let rm = _context.RmDdLookup.FirstOrDefault(s => s.DdlTypeCode == x.FsiihRmu && s.DdlType == "RMU")
                         let r = _context.RmDdLookup.FirstOrDefault(s => s.DdlPkRefNo == x.FsiihActId)
                         let gscuser = _context.RmUsers.FirstOrDefault(s => s.UsrPkId == (x.FsiihUseridSchld.HasValue ? x.FsiihUseridSchld.Value : 0))
                         let gvetuser = _context.RmUsers.FirstOrDefault(s => s.UsrPkId == (x.FsiihUseridVet.HasValue ? x.FsiihUseridVet.Value : 0))
                         let gagruser = _context.RmUsers.FirstOrDefault(s => s.UsrPkId == (x.FsiihUseridAgrd.HasValue ? x.FsiihUseridAgrd.Value : 0))
                         let gpuser = _context.RmUsers.FirstOrDefault(s => s.UsrPkId == (x.FsiihUseridPrioritised.HasValue ? x.FsiihUseridPrioritised.Value : 0))
                         let gsubuser = _context.RmUsers.FirstOrDefault(s => s.UsrPkId == (x.FsiihUseridSub.HasValue ? x.FsiihUseridSub.Value : 0))
                         where x.FsiihQuaterId == (filterOptions.Filters.Quarter.HasValue ? filterOptions.Filters.Quarter.Value : x.FsiihQuaterId)
                         && (filterOptions.Filters.ActivityCode.HasValue ? filterOptions.Filters.ActivityCode.Value : x.FsiihActId) == x.FsiihActId
                         && (filterOptions.Filters.Rmu != "" && filterOptions.Filters.Rmu != null ? filterOptions.Filters.Rmu : x.FsiihRmu) == x.FsiihRmu
                         && (filterOptions.Filters.Year.HasValue ? filterOptions.Filters.Year.Value : x.FsiihYear) == x.FsiihYear
                         && x.FsiihActiveYn == true
                         select new S2HeaderResponse
                         {
                             ScheduledBy = x.FsiihUserNameSchId,
                             ActivityCode = r != null ? r.DdlTypeCode : "",
                             AgreedBy = x.FsiihUserNameAgrd,
                             Id = x.FsiihPkRefNo,
                             PrioritizedBy = x.FsiihUserNamePrioritised,
                             Quarter = x.FsiihQuaterId == 1 ? "Q1" : x.FsiihQuaterId == 2 ? "Q2" : x.FsiihQuaterId == 3 ? "Q3" : x.FsiihQuaterId == 4 ? "Q4" : "",
                             ReferenceNo = x.FsiihRefId,
                             SubmittedBy = x.FsiihUserNameSub,
                             VettedBy = x.FsiihUserNameVet,
                             Year = x.FsiihYear,
                             Rmu = x.FsiihRmu,
                             RmuName = rm != null ? rm.DdlTypeDesc : "",
                             SubmitSts = x.FsiihSubmitSts,
                             Status = x.FsiihSubmitSts ? "Submitted" : "Saved"
                         });
            if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInput))
            {
                query = query.Where(s => s.ReferenceNo.Contains(filterOptions.Filters.SmartInput)
                 || s.SubmittedBy.Contains(filterOptions.Filters.SmartInput)
                 || s.ScheduledBy.Contains(filterOptions.Filters.SmartInput)
                 || s.VettedBy.Contains(filterOptions.Filters.SmartInput)
                 || s.AgreedBy.Contains(filterOptions.Filters.SmartInput)
                 || s.ActivityCode.Contains(filterOptions.Filters.SmartInput)
                 || s.PrioritizedBy.Contains(filterOptions.Filters.SmartInput)
                 || s.Year.ToString().Contains(filterOptions.Filters.SmartInput)
                 || s.Quarter.Contains(filterOptions.Filters.SmartInput)
                 || s.Rmu.Contains(filterOptions.Filters.SmartInput)
                 || s.Status.Contains(filterOptions.Filters.SmartInput)
                 || s.RmuName.Contains(filterOptions.Filters.SmartInput));
            }
            return await query.CountAsync();
        }

        public FORMS2Rpt GetReportData(int headerId)
        {
            FORMS2Rpt rpt = new FORMS2Rpt();
            rpt.Header = (from x in _context.RmFormS2Hdr
                          let rm = _context.RmDdLookup.FirstOrDefault(s => s.DdlTypeCode == x.FsiihRmu && s.DdlType == "RMU")
                          let r = _context.RmDdLookup.FirstOrDefault(s => s.DdlPkRefNo == x.FsiihActId)
                          let gscuser = _context.RmUsers.FirstOrDefault(s => s.UsrPkId == (x.FsiihUseridSchld.HasValue ? x.FsiihUseridSchld.Value : 0))
                          let gvetuser = _context.RmUsers.FirstOrDefault(s => s.UsrPkId == (x.FsiihUseridVet.HasValue ? x.FsiihUseridVet.Value : 0))
                          let gagruser = _context.RmUsers.FirstOrDefault(s => s.UsrPkId == (x.FsiihUseridAgrd.HasValue ? x.FsiihUseridAgrd.Value : 0))
                          let gpuser = _context.RmUsers.FirstOrDefault(s => s.UsrPkId == (x.FsiihUseridPrioritised.HasValue ? x.FsiihUseridPrioritised.Value : 0))
                          let gsubuser = _context.RmUsers.FirstOrDefault(s => s.UsrPkId == (x.FsiihUseridSub.HasValue ? x.FsiihUseridSub.Value : 0))

                          where x.FsiihPkRefNo == headerId
                          select new FORMS2HeaderRpt
                          {
                              Activity = r != null ? r.DdlTypeValue : "",
                              AgreedBy = x.FsiihUserNameAgrd,
                              AgreedDesignation = x.FsiihUserDesignationAgrd,
                              AgreedDate = x.FsiihDtAgrd,
                              PrioritizedBy = x.FsiihUserNamePrioritised,
                              PrioritizedDate = x.FsiihDtPrioritised,
                              Quarter = x.FsiihQuaterId,
                              RMU = rm != null ? rm.DdlTypeValue : "",
                              ScheduledBy = x.FsiihUserNameSchId,
                              ScheduledDate = x.FsiihDtSchld,
                              SubmittedBy = x.FsiihUserNameSub,
                              SubmittedDate = x.FsiihDtSub,
                              SubmittedDesignation = x.FsiihUserDesignationSub,
                              VettedBy = x.FsiihUserNameVet,
                              VettedDate = x.FsiihDtVet,
                              VettedDesignation = x.FsiihUserDesignationVet,
                              Year = x.FsiihYear
                          }).FirstOrDefault();
            rpt.Header.WeekNo = (from x in _context.RmWeekLookup
                                 where x.ClkQuarter == rpt.Header.Quarter && x.ClkYear == rpt.Header.Year
                                 orderby x.ClkWeekNo
                                 select x.ClkWeekNo.Value).ToArray();

            rpt.Details = (from x in _context.RmFormS2Dtl
                           where x.FsiidFsiihPkRefNo == headerId
                           && x.FsiidActiveYn == true
                           let r = _context.RmRoadMaster.FirstOrDefault(s => s.RdmPkRefNo == x.FsiidRoadId)
                           select new FORMS2DetailRpt
                           {
                               ADP = x.FsiidAdp,
                               CIL = x.FsiidPriority == 320,
                               CrewdayRequired = x.FsiidCrwDaysReq,
                               CrewdaysAllocated = x.FsiidCrwAllwcdQuar,
                               PaveLength = r!=null ? r.RdmLengthPaved : null,
                               PriorityI = x.FsiidPriority == 321,
                               PriorityII = x.FsiidPriority == 322,
                               Remark = x.FsiidRemarks,
                               RoadCode = r != null ? r.RdmRdCode : "",
                               RoadName = r != null ? r.RdmRdName : "",
                               RoadLocationSeq = x.FsiidRdLocSeq,
                               Target = x.FsiidTargetPercent,
                               UnPavedLength = r != null ? r.RdmLengthUnpaved : null,
                               DetailId = x.FsiidPkRefNo,
                               WorkQty = x.FsiidWorkQty
                           }).ToList();
            if (rpt.Details!=null)
            {
                foreach (var q in rpt.Details)
                {
                    q.week = (from cal in _context.RmFormS2QuarDtl
                              join c in _context.RmWeekLookup on cal.FsiiqdClkPkRefNo equals c.ClkPkRefNo
                              where cal.FsiiqdFsiidPkRefNo == q.DetailId
                              select c.ClkWeekNo.Value).ToArray();
                }
            }
            return rpt;
        }
    }
}
