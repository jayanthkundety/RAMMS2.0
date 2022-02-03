using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvLandscapeDetail
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
        public string AssetName { get; set; }
        public string AssetLocation { get; set; }
        public string ZoneCode { get; set; }
        public string SoftLandscaping { get; set; }
        public string HardLandscaping { get; set; }
        public string AssetNumbering { get; set; }
        public string AssetType { get; set; }
        public int? AssetQty { get; set; }
        public double? AssetDiameter { get; set; }
        public double? AssetSize { get; set; }
        public string Aging { get; set; }
        public string DrawingNo { get; set; }
        public string DrawingTitle { get; set; }
        public string DrawingFile { get; set; }
        public string ContractorVendorNo { get; set; }
        public string ContractorCompanyName { get; set; }
        public string ContractorRegNo { get; set; }
        public string ContractorRptno { get; set; }
        public string ConsultantVendorNo { get; set; }
        public string ConsultantCompanyName { get; set; }
        public string ConsultantRegNo { get; set; }
        public string ConsultantRptno { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
