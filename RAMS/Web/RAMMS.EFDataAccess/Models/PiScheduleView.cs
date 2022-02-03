using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PiScheduleView
    {
        public string AssetId { get; set; }
        public string AssetBaseType { get; set; }
        public string MainGroupAttributeName { get; set; }
        public string SubGroupAttributeName { get; set; }
        public string MainComponentAttributeName { get; set; }
        public string SubComponentAttributeName { get; set; }
        public int FeaturePk { get; set; }
        public string FeatureId { get; set; }
        public string Route { get; set; }
        public string RouteName { get; set; }
        public int SectionPk { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public DateTime? PreviousInspectionDate { get; set; }
        public int Pk { get; set; }
        public int InvMasterPk { get; set; }
        public DateTime ScheduleInspectionDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Remarks { get; set; }
        public DateTime? InspectionStartDate { get; set; }
        public DateTime? InspectionEndDate { get; set; }
        public string InspectedBy { get; set; }
        public double? InspectionLat { get; set; }
        public double? InspectionLng { get; set; }
        public DateTime? ResultUploadedDate { get; set; }
        public string ResultUploadedBy { get; set; }
        public DateTime? ResultModifiedDate { get; set; }
        public string ResultModifiedBy { get; set; }
    }
}
