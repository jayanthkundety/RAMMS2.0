using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PavementConditionDetail
    {
        public int Pk { get; set; }
        public int? Year { get; set; }
        public int? SectionPk { get; set; }
        public string Route { get; set; }
        public string Bound { get; set; }
        public string DTimssectionId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Iri { get; set; }
        public double? Rut { get; set; }
        public double? Mtd { get; set; }
        public double? RiseFallAvg { get; set; }
        public double? RiseFallNo { get; set; }
        public double? CrossFall { get; set; }
        public double? Curvature { get; set; }
        public double? Gradient { get; set; }
        public double? AllCracks { get; set; }
        public double? WideCracks { get; set; }
        public double? Potholes { get; set; }
        public double? Ravelling { get; set; }
        public double? EdgeBreak { get; set; }
        public double? LatitudeFwd { get; set; }
        public double? LongitudeFwd { get; set; }
        public double? D1 { get; set; }
        public double? D2 { get; set; }
        public double? D3 { get; set; }
        public double? D4 { get; set; }
        public double? D5 { get; set; }
        public double? D6 { get; set; }
        public double? D7 { get; set; }
        public double? E1 { get; set; }
        public double? E2 { get; set; }
        public double? E3 { get; set; }
        public double? Es { get; set; }
        public double? H1 { get; set; }
        public double? H2 { get; set; }
        public double? H3 { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Id { get; set; }
        public double? Kmfrom { get; set; }
        public double? Kmto { get; set; }
        public DateTime? SurveyDateMlp { get; set; }
        public double? Altitude { get; set; }
        public double? Speed { get; set; }
        public double? IristdDev { get; set; }
        public double? RutstdDev { get; set; }
        public double? MtdstdDev { get; set; }
        public double? Ci { get; set; }
        public double? Ri { get; set; }
        public double? Scrim { get; set; }
        public DateTime? SurveyDateFwd { get; set; }
        public double? FwdtestPoint { get; set; }
        public double? FwdcentralDef { get; set; }
        public double? DefDifference { get; set; }
        public double? StructuralNumber { get; set; }
        public double? ResidualLife { get; set; }
        public double? Pci { get; set; }
        public DateTime? SurveyDateGtester { get; set; }
        public double? GripNumber { get; set; }
        public string RoadNo { get; set; }
        public string TeamsrecordId { get; set; }
        public double? Direction { get; set; }
        public double? Source { get; set; }
        public double? Quality { get; set; }
        public double? SurfaceClass { get; set; }
        public string ShoulderType1 { get; set; }
        public bool? Bridge { get; set; }
        public double? PaveAge { get; set; }
        public double? PaveType { get; set; }
        public double? StThick { get; set; }
        public double? SurfStren { get; set; }
        public double? BaseStren { get; set; }
        public double? SubbStren { get; set; }
        public double? RelComp { get; set; }
        public double? BinderContent { get; set; }
        public double? SurfQuality { get; set; }
        public double? BaseQuality { get; set; }
        public string Remark { get; set; }
        public double? Lane { get; set; }
        public double? PatchProtusion { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
    }
}
