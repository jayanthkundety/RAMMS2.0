using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class RmScheduleView
    {
        public string SectionCode { get; set; }
        public string ContractorName { get; set; }
        public string RmFormCode { get; set; }
        public string RmFormName { get; set; }
        public int? RmFormCategoryPk { get; set; }
        public string RmFormCategoryName { get; set; }
        public string BoundName { get; set; }
        public string FeatureName { get; set; }
        public string AssetId { get; set; }
        public int? UserPk { get; set; }
        public string Username { get; set; }
        public int Pk { get; set; }
        public string PlanningType { get; set; }
        public string Task { get; set; }
        public DateTime? ScheduleStartDateTime { get; set; }
        public DateTime? ScheduleEndDateTime { get; set; }
        public string DescriptionOfWork { get; set; }
        public string Category { get; set; }
        public int? RmFormPk { get; set; }
        public int? AssetPk { get; set; }
        public int? GroupStructurePk { get; set; }
        public string AssetName { get; set; }
        public int? FeaturePk { get; set; }
        public int SectionPk { get; set; }
        public double? ChainageStartKm { get; set; }
        public double? ChainageEndKm { get; set; }
        public string Location { get; set; }
        public int? ContractorPk { get; set; }
        public string ContractorRemark { get; set; }
        public string Status { get; set; }
        public string ReviewBy { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string ReviewRemark { get; set; }
        public string Remark { get; set; }
        public int? RescheduleFromRmSchedulePk { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? InspectionStartDate { get; set; }
        public DateTime? InspectionEndDate { get; set; }
        public string InspectedBy { get; set; }
        public double? InspectionLat { get; set; }
        public double? InspectionLng { get; set; }
        public DateTime? ResultUploadedDate { get; set; }
        public string ResultUploadedBy { get; set; }
        public string Bound { get; set; }
        public string RescheduleReason { get; set; }
        public DateTime? ResultModifiedDate { get; set; }
        public string ResultModifiedBy { get; set; }
    }
}
