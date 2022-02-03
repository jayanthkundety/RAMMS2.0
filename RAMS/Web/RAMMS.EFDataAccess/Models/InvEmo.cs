using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvEmo
    {
        public int Pk { get; set; }
        public double? AssetFromKm { get; set; }
        public double? AssetToKm { get; set; }
        public double? AssetLength { get; set; }
        public string AssetComponent { get; set; }
        public double? NumberofPost { get; set; }
        public string PostMaterialType { get; set; }
        public double? PostWidth { get; set; }
        public double? PostBreadth { get; set; }
        public double? PostDiameter { get; set; }
        public double? PostThickness { get; set; }
        public double? PostHeight { get; set; }
        public double? PostSpacing { get; set; }
        public double? NumberofRailing { get; set; }
        public string RailingMaterialType { get; set; }
        public double? RailingWidth { get; set; }
        public double? RailingBreadth { get; set; }
        public double? RailingDiameter { get; set; }
        public double? RailingThickness { get; set; }
        public double? RailingLength { get; set; }
        public double? RailingSpacing { get; set; }
        public string TypeofBedding { get; set; }
        public string TypeofBaseCoverPlate { get; set; }
        public double? NumberofEmergencyJacking { get; set; }
        public string TypeofEmergencyJacking { get; set; }
        public double? NumberofPalletJack { get; set; }
        public string TypeofPalletJack { get; set; }
        public double? NumberofBalanceRoller { get; set; }
        public string TypeofBalanceRoller { get; set; }
        public double? InstallationYear { get; set; }
        public string MaintenanceHistory { get; set; }
    }
}
