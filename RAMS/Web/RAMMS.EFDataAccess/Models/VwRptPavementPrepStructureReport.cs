using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptPavementPrepStructureReport
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
        public double? TestKm { get; set; }
        public double? EndKm { get; set; }
        public double? Source { get; set; }
        public double? Quality { get; set; }
        public double? SurfaceClass { get; set; }
        public double? PaveAge { get; set; }
        public double? PaveType { get; set; }
        public double? StThick { get; set; }
        public double? SurfStren { get; set; }
        public string BaseThick { get; set; }
        public double? BaseStren { get; set; }
        public string SubbThick { get; set; }
        public double? SubbStren { get; set; }
        public string SubgStren { get; set; }
        public double? RelComp { get; set; }
        public double? FwdcentralDef { get; set; }
        public double? BinderContent { get; set; }
        public double? SurfQuality { get; set; }
        public double? BaseQuality { get; set; }
        public double? DeflectionD300 { get; set; }
        public double? ResidualLife { get; set; }
        public string Remark { get; set; }
        public string LaneNo { get; set; }
    }
}
