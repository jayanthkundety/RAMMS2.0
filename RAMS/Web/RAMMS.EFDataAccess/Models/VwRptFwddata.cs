using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptFwddata
    {
        public int? Year { get; set; }
        public string Road { get; set; }
        public string Bound { get; set; }
        public string RoadNo { get; set; }
        public string RecordId { get; set; }
        public double? NoOfLanes { get; set; }
        public double? KmFrom { get; set; }
        public double? KmTo { get; set; }
        public double? LengthM { get; set; }
        public string Description { get; set; }
        public DateTime? SurveyDate { get; set; }
        public double? FwdTestPoint { get; set; }
        public double? D0 { get; set; }
        public double? D0D300 { get; set; }
        public double? StructNo { get; set; }
        public double? E1 { get; set; }
        public double? E2 { get; set; }
        public double? E3 { get; set; }
        public double? Es { get; set; }
        public double? H1 { get; set; }
        public double? H2 { get; set; }
        public double? H3 { get; set; }
        public string LaneNo { get; set; }
    }
}
