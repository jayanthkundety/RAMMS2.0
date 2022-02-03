using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptPavementPrepConditionReport
    {
        public int? Year { get; set; }
        public string Road { get; set; }
        public string Bound { get; set; }
        public string RoadNo { get; set; }
        public string RecordId { get; set; }
        public double? Direction { get; set; }
        public double? Lane { get; set; }
        public double? StartKm { get; set; }
        public DateTime? SurveyDate { get; set; }
        public double? EndKm { get; set; }
        public double? Source { get; set; }
        public double? Quality { get; set; }
        public double? SurfaceClass { get; set; }
        public double? AllCracks { get; set; }
        public double? WideCracks { get; set; }
        public double? Potholes { get; set; }
        public double? Ravelling { get; set; }
        public double? Roughness { get; set; }
        public double? Rutting { get; set; }
        public double? EdgeBreak { get; set; }
        public double? TextureDepth { get; set; }
        public double? SkidResistance { get; set; }
        public double? PatchProtusion { get; set; }
        public double? Pci { get; set; }
        public string Remark { get; set; }
        public string LaneNo { get; set; }
    }
}
