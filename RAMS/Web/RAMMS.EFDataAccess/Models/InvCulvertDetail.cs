using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvCulvertDetail
    {
        public int Pk { get; set; }
        public string Id { get; set; }
        public string BaseType { get; set; }
        public int FeaturePk { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Remark { get; set; }
        public int GroupStructurePk { get; set; }
        public int? ParentPk { get; set; }
        public string ParentId { get; set; }
        public string MainGroupAttributeName { get; set; }
        public string MainGroupAttributeType { get; set; }
        public string MainGroupAttributeCode { get; set; }
        public string SubGroupAttributeName { get; set; }
        public string SubGroupAttributeCode { get; set; }
        public string SubGroupAttributeType { get; set; }
        public string SubComponentAttributeName { get; set; }
        public string SubComponentAttributeType { get; set; }
        public string SubComponentAttributeCode { get; set; }
        public string MainComponentAttributeName { get; set; }
        public string MainComponentAttributeCode { get; set; }
        public string MainComponentAttributeType { get; set; }
        public string FeatureId { get; set; }
        public string Name { get; set; }
        public string Route { get; set; }
        public double? Kmlocation { get; set; }
        public int SectionPk { get; set; }
        public string LocalityOfFeature { get; set; }
        public string Bound { get; set; }
        public string Mhaoffice { get; set; }
        public DateTime? OpeningDate { get; set; }
        public double? SpeedLimit { get; set; }
        public string RouteName { get; set; }
        public string LocalityOfFeatureName { get; set; }
        public string BoundName { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public string TemanId { get; set; }
        public bool IsActive { get; set; }
        public string LofCode { get; set; }
        public string FeatureOwner { get; set; }
        public string RegionName { get; set; }
        public string RegionCode { get; set; }
        public double? RegionKmfrom { get; set; }
        public double? RegionKmto { get; set; }
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
        public DateTime? DateInactive { get; set; }
        public string MaintainedBy { get; set; }
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
        public string OutletKm { get; set; }
        public string InletKm { get; set; }
    }
}
