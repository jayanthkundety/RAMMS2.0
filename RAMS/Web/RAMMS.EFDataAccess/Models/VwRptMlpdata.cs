using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptMlpdata
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
        public double? IriMKm { get; set; }
        public double? IriStdDeviation { get; set; }
        public double? RutMm { get; set; }
        public double? RutStdDeviation { get; set; }
        public double? MtdMm { get; set; }
        public double? MtdStdDeviation { get; set; }
        public string LaneNo { get; set; }
    }
}
