using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvDrains
    {
        public int Pk { get; set; }
        public double? AssetFromKm { get; set; }
        public double? AssetToKm { get; set; }
        public double? AssetLength { get; set; }
        public string DrainType { get; set; }
        public string DrainShape { get; set; }
        public double? BottomWidth { get; set; }
        public double? TopWidth { get; set; }
        public double? Thickness { get; set; }
        public double? Height { get; set; }
        public string TypeofGrating { get; set; }
        public double? GratingWidth { get; set; }
        public double? GratingBreadth { get; set; }
        public string InletStructureType { get; set; }
        public string InletId { get; set; }
        public string OutletStructureType { get; set; }
        public string OutletId { get; set; }
        public string MaintenanceHistory { get; set; }
    }
}
