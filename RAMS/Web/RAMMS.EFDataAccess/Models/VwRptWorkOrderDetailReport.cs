using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptWorkOrderDetailReport
    {
        public string Region { get; set; }
        public string Section { get; set; }
        public double SectionKmFrom { get; set; }
        public double SectionKmTo { get; set; }
        public string Bound { get; set; }
        public string WorkType { get; set; }
        public string WorkOrderNo { get; set; }
        public string AssetId { get; set; }
        public string NodNo { get; set; }
        public string IssuedBy { get; set; }
        public DateTime? WoCreatedDate { get; set; }
        public string WoTitle { get; set; }
        public DateTime? ScheduleStartDate { get; set; }
        public DateTime? ScheduleEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public int? Progress { get; set; }
        public string Status { get; set; }
        public string AssignedToContractor { get; set; }
        public string IssuerPlusPropel { get; set; }
    }
}
