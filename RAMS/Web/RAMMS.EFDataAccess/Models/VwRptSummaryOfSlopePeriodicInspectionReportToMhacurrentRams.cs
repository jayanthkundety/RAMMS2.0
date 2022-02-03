using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptSummaryOfSlopePeriodicInspectionReportToMhacurrentRams
    {
        public string RegionCode { get; set; }
        public string SectionCode { get; set; }
        public string SlopeId { get; set; }
        public double? StartKm { get; set; }
        public double? EndKm { get; set; }
        public string Bound { get; set; }
        public string Type { get; set; }
        public string Feature { get; set; }
        public string Angle { get; set; }
        public double? HeightM { get; set; }
        public double? NoOfBerm { get; set; }
        public DateTime? InspectionDate { get; set; }
        public string SurfaceCondition { get; set; }
        public string DrainageCondition { get; set; }
        public string DistressCondition { get; set; }
        public string ExistingStabWorks { get; set; }
        public string PriorityRating { get; set; }
        public string DefectRating { get; set; }
        public string OverallCond { get; set; }
        public string HazardRiskRating { get; set; }
    }
}
