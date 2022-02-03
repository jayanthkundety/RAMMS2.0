using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptMcsbacklogPerformanceMarkerReport
    {
        public int Pk { get; set; }
        public string Section { get; set; }
        public string Category { get; set; }
        public string Activity { get; set; }
        public double? LocationOutstandingFromKm { get; set; }
        public double? LocationOutstandingToKm { get; set; }
        public double? _ { get; set; }
        public string PhotoBefore { get; set; }
        public string PhotoAfter { get; set; }
        public string Remark { get; set; }
        public DateTime ScheduleStartDate { get; set; }
        public DateTime ScheduleEndDate { get; set; }
        public int? McsCategoryBacklogPk { get; set; }
        public int? McsActivityBacklogPk { get; set; }
        public int? RegionPk { get; set; }
    }
}
