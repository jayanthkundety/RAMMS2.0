using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvDrainIo
    {
        public int Pk { get; set; }
        public double? AssetFromKm { get; set; }
        public double? AssetToKm { get; set; }
        public double? AssetLength { get; set; }
        public string TypeofInletOutlet { get; set; }
        public string TypeofGrating { get; set; }
        public double? GratingWidth { get; set; }
        public double? GratingBreadth { get; set; }
        public string TypeofHinge { get; set; }
        public string TypeofGratingFrame { get; set; }
        public string MaintenanceHistory { get; set; }
    }
}
