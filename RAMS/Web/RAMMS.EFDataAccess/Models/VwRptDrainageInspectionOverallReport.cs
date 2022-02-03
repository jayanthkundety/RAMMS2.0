using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptDrainageInspectionOverallReport
    {
        public double? KmLoc { get; set; }
        public string CulvertId { get; set; }
        public string FeatureName { get; set; }
        public string RampId { get; set; }
        public string Bound { get; set; }
        public string Type { get; set; }
        public DateTime? InspectionDate { get; set; }
        public string OverallService { get; set; }
        public string OverallCondition { get; set; }
        public string Defect { get; set; }
        public string RecommendedAction { get; set; }
        public string PriorityRating { get; set; }
        public bool? Fi { get; set; }
        public bool? Rw { get; set; }
        public bool? Rm { get; set; }
        public string Remarks { get; set; }
    }
}
