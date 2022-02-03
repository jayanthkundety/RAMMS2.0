using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvEarth
    {
        public int Pk { get; set; }
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
