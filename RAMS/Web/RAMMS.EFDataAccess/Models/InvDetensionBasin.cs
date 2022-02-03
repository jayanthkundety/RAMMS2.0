using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvDetensionBasin
    {
        public int Pk { get; set; }
        public double? AssetFromKm { get; set; }
        public double? AssetToKm { get; set; }
        public string TypeofOsd { get; set; }
        public double? YearBuilt { get; set; }
        public string Shape { get; set; }
        public double? Diameter { get; set; }
        public double? Width { get; set; }
        public double? Depth { get; set; }
        public double? Length { get; set; }
        public double? NoofCell { get; set; }
        public string TypeofLiningBase { get; set; }
        public string TypeofLiningWall { get; set; }
        public double? ThicknessBase { get; set; }
        public double? ThicknessWall { get; set; }
        public double? PondInvertLevel { get; set; }
        public string OutletType { get; set; }
        public double? OutletNoofCell { get; set; }
        public double? OutletDiameter { get; set; }
        public double? OutletLength { get; set; }
        public string SpillwayType { get; set; }
        public double? SpillwayThickness { get; set; }
        public double? SpillwayWidth { get; set; }
        public double? SpillwayDepth { get; set; }
        public string PumpType { get; set; }
        public double? PumpNo { get; set; }
        public double? PumpCapacity { get; set; }
        public double? PumpInstallYear { get; set; }
        public string SafetyBarrierType { get; set; }
        public double? SafetyBarrierPostNo { get; set; }
        public string SafetyBarrierPostType { get; set; }
        public double? SafetyBarrierPostSpacing { get; set; }
        public double? SafetyBarrierDiameter { get; set; }
        public double? SafetyBarrierHeight { get; set; }
        public double? SafetyBarrierLength { get; set; }
        public double? SafetyBarrierDepth { get; set; }
        public string TrashScreenType { get; set; }
        public double? TrashScreenWidth { get; set; }
        public double? TrashScreenHeight { get; set; }
        public string MaintenanceHistory { get; set; }
    }
}
