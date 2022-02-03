using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvTplaneDetail
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
        public string TollPlazaId { get; set; }
        public string LaneCode { get; set; }
        public string LaneType { get; set; }
        public string LaneDirection { get; set; }
        public string Reversible { get; set; }
        public string VehicleType { get; set; }
        public double? LaneWidth { get; set; }
        public string TransactionMode { get; set; }
        public int? HeightLimitBarQty { get; set; }
        public string Hlblocation { get; set; }
        public string Hlbtype { get; set; }
        public double? Hlbwidth { get; set; }
        public double? Hlbheight { get; set; }
        public string Hlbremarks { get; set; }
        public string SignagesCategory { get; set; }
        public int? GantrySignboardQty { get; set; }
        public string GantrySignboardType { get; set; }
        public int? GantrySignagesQty { get; set; }
        public string SignagesType { get; set; }
        public string SignagesLocation { get; set; }
        public double? SignagesWidth { get; set; }
        public double? SignagesHeight { get; set; }
        public string SignagesRemarks { get; set; }
        public string GantryBarType { get; set; }
        public int? GantryBarQty { get; set; }
        public double? GantryBarDiameter { get; set; }
        public double? GantryBarLength { get; set; }
        public double? GantryBarHeight { get; set; }
    }
}
