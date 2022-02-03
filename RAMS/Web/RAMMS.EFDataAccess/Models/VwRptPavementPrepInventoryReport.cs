using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptPavementPrepInventoryReport
    {
        public int? Year { get; set; }
        public string Road { get; set; }
        public string Bound { get; set; }
        public string RoadNo { get; set; }
        public string RecordId { get; set; }
        public double? Direction { get; set; }
        public double? StartKm { get; set; }
        public DateTime? SurveyDate { get; set; }
        public double? EndKm { get; set; }
        public double? Source { get; set; }
        public double? Quality { get; set; }
        public double? SurfaceClass { get; set; }
        public double? CwayWidth { get; set; }
        public double? NumberLanes { get; set; }
        public double? ShoulderWidth1 { get; set; }
        public string ShoulderWidth2 { get; set; }
        public string ShouldType1 { get; set; }
        public string ShouldType2 { get; set; }
        public double? Altitude { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? RiseFallAverage { get; set; }
        public double? RiseFallNumber { get; set; }
        public double? Crossfall { get; set; }
        public double? Curvature { get; set; }
        public string Comment { get; set; }
        public bool? Bridge { get; set; }
        public string LaneNo { get; set; }
    }
}
