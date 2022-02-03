using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WorkProgram
    {
        public int Pk { get; set; }
        public string ScheduleType { get; set; }
        public string WorkProgramType { get; set; }
        public string WorkProgramCategory { get; set; }
        public int? InvMasterPk { get; set; }
        public DateTime? ScheduleStartDate { get; set; }
        public DateTime? ScheduleEndDate { get; set; }
        public string ScheduleDescriptionOfWork { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public string ActualDescriptionOfWork { get; set; }
        public string WorkOrderNumber { get; set; }
        public double? WorkCostEstimate { get; set; }
        public string ProposedBy { get; set; }
        public DateTime? TargetWorkOrderSubmissionDate { get; set; }
        public string Contractor { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UploadedBy { get; set; }
        public DateTime? UploadedDate { get; set; }
        public int? SectionPk { get; set; }
        public double? AssetToKm { get; set; }
        public double? AssetFromKm { get; set; }
        public string LocalityOfFeature { get; set; }
        public string Bound { get; set; }
        public int? WorkProgramYear { get; set; }
        public string OtherProgramType { get; set; }
        public int? Frequency { get; set; }
        public string ReportCycle { get; set; }
        public string ScheduleTreatmentCode { get; set; }
        public string ActualTreatmentCode { get; set; }

        public virtual InvMaster InvMasterPkNavigation { get; set; }
    }
}
