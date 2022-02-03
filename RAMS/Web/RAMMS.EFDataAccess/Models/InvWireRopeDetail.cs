using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvWireRopeDetail
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
        public string RampId { get; set; }
        public string TollPlaza { get; set; }
        public string Type { get; set; }
        public string LinePostDimension { get; set; }
        public double? LinePostPostInterval { get; set; }
        public double? LinePostHeight { get; set; }
        public double? LinePostLength { get; set; }
        public double? LinePostQuantity { get; set; }
        public double? LinePostCap { get; set; }
        public double? LinePostRoller { get; set; }
        public string DeflectionPost { get; set; }
        public double? DeflectionPostPostInterval { get; set; }
        public double? DeflectionPostHeight { get; set; }
        public double? DeflectionPostLength { get; set; }
        public double? DeflectionPostQuantity { get; set; }
        public string DeflectionPostCap { get; set; }
        public double? DeflectionPostRoller { get; set; }
        public double? GroundAnchor { get; set; }
        public double? VerticalAnchor { get; set; }
        public string RiggingScrew { get; set; }
        public string LhsorRhstreadedTerminal { get; set; }
        public string DoubleTreadedTerminal { get; set; }
        public string TensioningSystem { get; set; }
        public string Shackle { get; set; }
        public string DelineatorStripReflectivity { get; set; }
        public string HorizontalClearance { get; set; }
        public string ReflectiveSticker { get; set; }
        public string EmbeddedDepth { get; set; }
        public string AssetLocation { get; set; }
        public double? AssetNumber { get; set; }
        public double? AssetLatitude { get; set; }
        public double? AssetLongitude { get; set; }
    }
}
