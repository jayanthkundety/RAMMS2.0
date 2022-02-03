using System;
using System.Collections.Generic;

namespace RAMMS.DTO.Report
{
    public class FORMS2Rpt
    {
        public FORMS2HeaderRpt Header { get; set; }
        public List<FORMS2DetailRpt> Details { get; set; }
    }

    public class FORMS2HeaderRpt
    {
        public string RMU { get; set; }
        public string Activity { get; set; }
        public string PrioritizedBy { get; set; }
        public DateTime? PrioritizedDate { get; set; }
        public string ScheduledBy { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public string VettedBy { get; set; }
        public DateTime? VettedDate { get; set; }
        public string AgreedBy { get; set; }
        public DateTime? AgreedDate { get; set; }
        public int[] WeekNo { get; set; }
        public int? Quarter { get; set; }
        public int? Year { get; set; }
        public string SubmittedDesignation { get; set; }
        public string VettedDesignation { get; set; }
        public string AgreedDesignation { get; set; }
    }

    public class FORMS2DetailRpt
    {
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public decimal? PaveLength { get; set; }
        public decimal? UnPavedLength { get; set; }
        public string RoadLocationSeq { get; set; }
        public bool? CIL { get; set; }
        public bool? PriorityI { get; set; }
        public bool? PriorityII { get; set; }
        public int? ADP { get; set; }
        public int? CrewdayRequired { get; set; }
        public int? CrewdaysAllocated { get; set; }
        public int[] week { get; set; }
        public decimal? Target { get; set; }
        public string Remark { get; set; }
        public int DetailId { get; set; }
        public string WorkQty { get; set; }
    }
}
