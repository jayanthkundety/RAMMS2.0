using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvFlashLight
    {
        public int Pk { get; set; }
        public string AssetLocation { get; set; }
        public double? AssetKmlocation { get; set; }
        public double? AssetNumber { get; set; }
        public string AssetType { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }
        public string LanternType { get; set; }
        public double? LanternWattage { get; set; }
        public double? Quantity { get; set; }
        public string MaintenanceHistory { get; set; }
        public double? RecommendedLifetime { get; set; }
        public DateTime? InstallationDate { get; set; }
        public double? Aging { get; set; }
        public double? AssetLatitude { get; set; }
        public double? AssetLongitude { get; set; }
    }
}
