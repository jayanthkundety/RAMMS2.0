using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptSummaryOfSlopeDefectsRecommendedWorksAndStatusCurrent
    {
        public string SlopeId { get; set; }
        public double? StartKm { get; set; }
        public double? EndKm { get; set; }
        public string Bound { get; set; }
        public string Type { get; set; }
        public DateTime? InspectionDate { get; set; }
        public string Defect { get; set; }
        public string RecommendedAction { get; set; }
        public string RefNo { get; set; }
        public string ActionBy { get; set; }
        public string Status { get; set; }
        public string OverallCond { get; set; }
        public string PriorityRating { get; set; }
        public string Rank { get; set; }
    }
}
