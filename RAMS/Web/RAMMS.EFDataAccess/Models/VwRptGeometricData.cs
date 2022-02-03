using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptGeometricData
    {
        public int? Year { get; set; }
        public string Road { get; set; }
        public string Bound { get; set; }
        public string RoadNo { get; set; }
        public string RecordId { get; set; }
        public double? NoOfLanes { get; set; }
        public double? KmFrom { get; set; }
        public double? KmTo { get; set; }
        public string Description { get; set; }
        public DateTime? SurveyDate { get; set; }
        public double? AltitudeM { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? AverageRiseFall { get; set; }
        public double? RiseFallNo { get; set; }
        public double? Crossfall { get; set; }
        public double? CurvatureDegKm { get; set; }
        public double? Gradient { get; set; }
        public double? AverageSpeed { get; set; }
        public string LaneNo { get; set; }
    }
}
