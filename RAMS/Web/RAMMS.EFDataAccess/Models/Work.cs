using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class Work
    {
        public Work()
        {
            WorkMilestone = new HashSet<WorkMilestone>();
            WorkProgress = new HashSet<WorkProgress>();
            WorkSorItem = new HashSet<WorkSorItem>();
        }

        public int Pk { get; set; }
        public string Id { get; set; }
        public int? InvMasterPk { get; set; }
        public int? GroupStructurePk { get; set; }
        public int? SectionPk { get; set; }
        public string Status { get; set; }
        public string WorkType { get; set; }
        public string WorkOrderNo { get; set; }
        public int? ContractorPk { get; set; }
        public string Issuer { get; set; }
        public int? OverallProgress { get; set; }
        public string WorkDescription { get; set; }
        public DateTime? ScheduleStartDate { get; set; }
        public DateTime? ScheduleEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public string AttdendedBy { get; set; }
        public double? GeoLat { get; set; }
        public double? GeoLong { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? RequireTrafficManagementPlan { get; set; }
        public DateTime? TrafficManagementPlanSubmissionDate { get; set; }
        public bool? BreachOfTrafficManagementPlan { get; set; }
        public bool? RequireWorkPermit { get; set; }
        public DateTime? WorkPermitGainedDate { get; set; }
        public bool? RequirePostConstructionReport { get; set; }
        public DateTime? PostContructionReportDate { get; set; }
        public string Remark { get; set; }
        public bool? SentToDr { get; set; }
        public string WorkClass { get; set; }
        public string ContractorRemark { get; set; }
        public string WorkSubType { get; set; }
        public string Drnumber { get; set; }
        public DateTime? Drdate { get; set; }
        public DateTime? NotificationDate { get; set; }
        public string RossworkCategory { get; set; }
        public string PlusworkOrderNo { get; set; }
        public DateTime? PlusworkOrderDate { get; set; }
        public string WorkMaintenanceType { get; set; }
        public double? Kmfrom { get; set; }
        public double? Kmto { get; set; }
        public string Bound { get; set; }
        public string InitialBq { get; set; }
        public string FinalBq { get; set; }
        public DateTime? WorkCloseDate { get; set; }
        public bool? WorkCompletion { get; set; }
        public DateTime? LastSentToRossdate { get; set; }
        public string JobTitle { get; set; }
        public int? ScheduledProgress { get; set; }

        public virtual Contractor ContractorPkNavigation { get; set; }
        public virtual Section SectionPkNavigation { get; set; }
        public virtual ICollection<WorkMilestone> WorkMilestone { get; set; }
        public virtual ICollection<WorkProgress> WorkProgress { get; set; }
        public virtual ICollection<WorkSorItem> WorkSorItem { get; set; }
    }
}
