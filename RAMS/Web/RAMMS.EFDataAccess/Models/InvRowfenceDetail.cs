using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvRowfenceDetail
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
        public double? AssetFromKm { get; set; }
        public double? AssetToKm { get; set; }
        public string RampId { get; set; }
        public string AssetInvType { get; set; }
        public double? Height { get; set; }
        public string Layerofbarbwire { get; set; }
        public string LayerofStrainingWire { get; set; }
        public double? Length { get; set; }
        public string PostType { get; set; }
        public double? Quantity { get; set; }
        public double? QuantityofCornerPost { get; set; }
        public double? QuantityofIntermediatePost { get; set; }
        public double? QuantityofStrainingPost { get; set; }
        public string PostMaterial { get; set; }
        public string PostSpacing { get; set; }
        public double? PostSize { get; set; }
        public string PostSizeThickness { get; set; }
        public double? PostSizeWidth { get; set; }
        public double? PostSizeLength { get; set; }
        public double? ConcretePlinth { get; set; }
        public string SizeofConcretePlinth { get; set; }
        public string MaintenanceGate { get; set; }
        public string MaintenanceGateCattletrap { get; set; }
        public double? MaintenanceGateQuantity { get; set; }
        public double? MaintenanceGateSize { get; set; }
        public double? MaintenanceGateHeight { get; set; }
        public double? MaintenanceGateWidth { get; set; }
        public string MaintenanceGateLocation { get; set; }
        public string AssetLocation { get; set; }
        public double? AssetNumber { get; set; }
        public double? AssetLatitude { get; set; }
        public double? AssetLongitude { get; set; }
    }
}
