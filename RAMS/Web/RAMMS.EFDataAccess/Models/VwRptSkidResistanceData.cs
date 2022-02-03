using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptSkidResistanceData
    {
        public int? Year { get; set; }
        public string Road { get; set; }
        public string RoadNo { get; set; }
        public string RecordId { get; set; }
        public double? Direction { get; set; }
        public string Bound { get; set; }
        public double? NoOfLanes { get; set; }
        public double? KmFrom { get; set; }
        public double? KmTo { get; set; }
        public string Description { get; set; }
        public DateTime? SurveyDate { get; set; }
        public double? Gn { get; set; }
        public double? Scrim { get; set; }
        public string LaneNo { get; set; }
    }
}
