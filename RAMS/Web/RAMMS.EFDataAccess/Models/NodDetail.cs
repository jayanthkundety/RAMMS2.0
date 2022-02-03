using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class NodDetail
    {
        public int Pk { get; set; }
        public string Id { get; set; }
        public DateTime? ResponseDateTime { get; set; }
        public DateTime? IssuedDateTime { get; set; }
        public string Category { get; set; }
        public double? KmStart { get; set; }
        public double? KmEnd { get; set; }
        public int? FeaturePk { get; set; }
        public int? GroupStructurePk { get; set; }
        public string Status { get; set; }
        public int? InvMasterPk { get; set; }
        public string AttendedBy { get; set; }
        public double? CreatedAtLat { get; set; }
        public double? CreatedAtLng { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UploadedBy { get; set; }
        public DateTime? UploadedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DescriptionOfDefect { get; set; }
        public bool? NonConformance { get; set; }
        public int? KpiItemPk { get; set; }
        public bool? SentToTechnical { get; set; }
        public DateTime? SentDateToTechnical { get; set; }
        public DateTime? TargetDesignCompletionDate { get; set; }
        public DateTime? NotificationToEmployerDateTime { get; set; }
        public DateTime? NotificationDateTimeToMha { get; set; }
        public DateTime? TreatmentPlanSubmissionDate { get; set; }
        public DateTime? SafetyMeasuresInPlaceDateTime { get; set; }
        public string DefectRating { get; set; }
        public string PossibleCausesOfDefect { get; set; }
        public string Remark { get; set; }
        public string EnvironmentalAspectAndImpact { get; set; }
        public string SafetyMeasureDesc { get; set; }
        public DateTime? RectificationWorksCommencementDateTime { get; set; }
        public string ActualCausesOfDefect { get; set; }
        public string ActionTaken { get; set; }
        public string ReplacementItem { get; set; }
        public bool? HasActionLog { get; set; }
        public string AlogId { get; set; }
        public string AlogSubject { get; set; }
        public string AlogDescription { get; set; }
        public string AlogCategory { get; set; }
        public bool? AlogIsClaimable { get; set; }
        public string AlogReference { get; set; }
        public string AlogOwner { get; set; }
        public int? AlogAgram { get; set; }
        public DateTime? AlogDueDate { get; set; }
        public DateTime? AlogCloseDate { get; set; }
        public string AlogErrRef { get; set; }
        public string AlogNcrNumber { get; set; }
        public string AlogWorkType { get; set; }
        public bool? EnableWorkFlowCascading { get; set; }
        public bool? IsEmergency { get; set; }
        public string Lane { get; set; }
        public string Kmlocation { get; set; }
        public bool? IsRecuring { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public int? RegionPk { get; set; }
        public int? SectionPk { get; set; }
        public string GroupStructureDetailMainGroup { get; set; }
        public string GroupStructureDetailMainGroupAttributeName { get; set; }
        public string GroupStructureDetailSubGroup { get; set; }
        public string GroupStructureDetailSubGroupAttributeName { get; set; }
        public string GroupStructureDetailMainComponent { get; set; }
        public string GroupStructureDetailMainComponentAttributeName { get; set; }
        public string GroupStructureDetailSubComponent { get; set; }
        public string GroupStructureDetailSubComponentAttributeName { get; set; }
        public string FeatureBound { get; set; }
        public string InvMasterId { get; set; }
        public string CategoryName { get; set; }
        public string LaneName { get; set; }
        public string PitTaskId { get; set; }
    }
}
