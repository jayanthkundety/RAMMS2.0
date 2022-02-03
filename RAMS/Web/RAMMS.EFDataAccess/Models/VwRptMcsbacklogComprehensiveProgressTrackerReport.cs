using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptMcsbacklogComprehensiveProgressTrackerReport
    {
        public int Pk { get; set; }
        public string Section { get; set; }
        public string Category { get; set; }
        public string Activity { get; set; }
        public double? LocationBacklogFromKm { get; set; }
        public double? LocationBacklogToKm { get; set; }
        public double? TotalQuantityBacklog { get; set; }
        public string Unit { get; set; }
        public DateTime PlanDateStart { get; set; }
        public DateTime PlanDateEnd { get; set; }
        public DateTime? ActualDateStart { get; set; }
        public DateTime? ActualDateEnd { get; set; }
        public double? ProgressTrackingTodate { get; set; }
        public double? ProgressTrackingTodate1 { get; set; }
        public double? ProgressTrackingOutstanding { get; set; }
        public double? ProgressTrackingOutstanding1 { get; set; }
        public string Remark { get; set; }
        public int? McsCategoryBacklogPk { get; set; }
        public int? McsActivityBacklogPk { get; set; }
        public int? RegionPk { get; set; }
    }
}
