using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvSlopeDetail
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
        public double? AssetKmfrom { get; set; }
        public double? AssetKmto { get; set; }
        public double? LatitudeStart { get; set; }
        public double? LongitudeStart { get; set; }
        public double? LatitudeEnd { get; set; }
        public double? LongitudeEnd { get; set; }
        public double? Height { get; set; }
        public double? Length { get; set; }
        public double? BermCount { get; set; }
        public string SlopeAngle { get; set; }
        public string SlopeType { get; set; }
        public string HazardRiskRanking { get; set; }
        public string SlopeMaterial { get; set; }
        public string Owner { get; set; }
        public string SlopeNumber { get; set; }
        public double? AssetKmlocation { get; set; }
        public string AssetName { get; set; }
        public string Alias { get; set; }
        public string MaintainedBy { get; set; }
        public string Abbreviation { get; set; }
        public string LithologyRockType { get; set; }
        public string OtherLithologyRockType { get; set; }
        public string WeatheringGrade { get; set; }
        public string AdverseGeologicalStructure { get; set; }
        public string RockSoilInterface { get; set; }
        public string MajorGeologicalStructure { get; set; }
        public string OtherMajorGeologicalStructure { get; set; }
        public string NoOfDiscontinuitySet { get; set; }
    }
}
