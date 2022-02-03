using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvCulvertView
    {
        public string AssetId { get; set; }
        public int GroupStructurePk { get; set; }
        public string BaseType { get; set; }
        public int FeaturePk { get; set; }
        public string FeatureId { get; set; }
        public string FeatureName { get; set; }
        public int SectionPk { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public int Pk { get; set; }
        public double? AssetKmlocation { get; set; }
        public string RampId { get; set; }
        public string SerialNumber { get; set; }
        public double? CellCount { get; set; }
        public double? Size { get; set; }
        public string StructureType { get; set; }
        public double? Skew { get; set; }
        public double? Length { get; set; }
        public string Marker { get; set; }
        public string Purpose { get; set; }
        public string Inlet { get; set; }
        public string Outlet { get; set; }
        public double? BuildYear { get; set; }
        public string Owner { get; set; }
        public double? AssetLongitude { get; set; }
        public double? AssetLatitude { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        public string FillHeight { get; set; }
        public double? Diameter { get; set; }
        public string InletStructure { get; set; }
        public string OutletStructure { get; set; }
        public double? InletInvertLevel { get; set; }
        public double? OutletInvertLevel { get; set; }
        public string InletPosition { get; set; }
        public string OutletPosition { get; set; }
        public string Description { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ModificationBy { get; set; }
        public string InformationSource { get; set; }
        public DateTime? DateInactive { get; set; }
        public string MaintainedBy { get; set; }
        public string OutletKm { get; set; }
        public string InletKm { get; set; }
        public int? PiScheduleMasterPk { get; set; }
        public int? Frequency { get; set; }
        public DateTime? BaseDate { get; set; }
    }
}
