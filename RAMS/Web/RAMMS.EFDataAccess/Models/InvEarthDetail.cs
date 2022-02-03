using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvEarthDetail
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
        public string TollPlazaName { get; set; }
        public double? AssetKmlocation { get; set; }
        public string AssetLocation { get; set; }
        public string AssetType { get; set; }
        public double? RecommendedLifetime { get; set; }
        public DateTime? InstallationDate { get; set; }
        public double? Aging { get; set; }
        public string TollCanopyTypeofAirTerminal { get; set; }
        public double? TollCanopyNoofAirTerminal { get; set; }
        public double? TollCanopyHeightofAirTerminal { get; set; }
        public string TollCanopyTypeofConductor { get; set; }
        public double? TollCanopyNoofChamber { get; set; }
        public string SubstationTypeofAirTerminal { get; set; }
        public double? SubstationNoofAirTerminal { get; set; }
        public double? SubstationHeightofAirTerminal { get; set; }
        public string SubstationTypeofConductor { get; set; }
        public double? SubstationNoofChamber { get; set; }
        public string StaggedLineTypeofAirTerminal { get; set; }
        public double? StaggedLineNoofAirTerminal { get; set; }
        public double? StaggedLineHeightofAirTerminal { get; set; }
        public string StaggedLineTypeofConductor { get; set; }
        public double? StaggedLineNoofChamber { get; set; }
        public string BuildingBuildingType { get; set; }
        public double? BuildingTypeofAirTerminal { get; set; }
        public double? BuildingNoofAirTerminal { get; set; }
        public double? BuildingHeightofAirTerminal { get; set; }
        public string BuildingTypeofConductor { get; set; }
        public double? BuildingNoofChamber { get; set; }
        public string HighmastTypeofAirTerminal { get; set; }
        public double? HighmastNoofAirTerminal { get; set; }
        public double? HighmastHeightofAirTerminal { get; set; }
        public string HighmastTypeofConductor { get; set; }
        public double? HighmastNoofChamber { get; set; }
        public string FeederPillarTypeofAirTerminal { get; set; }
        public double? FeederPillarNoofAirTerminal { get; set; }
        public double? FeederPillarHeightofAirTerminal { get; set; }
        public string FeederPillarTypeofConductor { get; set; }
        public double? FeederPillarNoofChamber { get; set; }
    }
}
