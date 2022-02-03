using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvFloodWall
    {
        public int Pk { get; set; }
        public double? AssetFromKm { get; set; }
        public double? AssetToKm { get; set; }
        public double? ConstructionYear { get; set; }
        public string FloodWallType { get; set; }
        public double? FloodWallThickness { get; set; }
        public double? FloodWallLength { get; set; }
        public double? FloodWallMinHeight { get; set; }
        public double? FloodWallMaxHeight { get; set; }
        public double? FloodWallDepth { get; set; }
        public string BeddingType { get; set; }
        public double? BeddingWidth { get; set; }
        public double? BeddingThickness { get; set; }
        public string CappingBeamType { get; set; }
        public double? CappingBeamWidth { get; set; }
        public double? CappingBeamThickness { get; set; }
        public string MaintenanceHistory { get; set; }
    }
}
