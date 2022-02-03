using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvAntiClimbFence
    {
        public int Pk { get; set; }
        public double? AssetKmlocation { get; set; }
        public double? AssetFromKm { get; set; }
        public double? AssetToKm { get; set; }
        public string RampId { get; set; }
        public string MaterialType { get; set; }
        public string FenceType { get; set; }
        public double? Heightfromgroundlevel { get; set; }
        public double? Length { get; set; }
        public double? PostInterval { get; set; }
        public double? EmergencyOpeningLength { get; set; }
        public double? EmergencyOpeningKmlocation { get; set; }
        public string EmergencyOpeningType { get; set; }
        public string AssetLocation { get; set; }
        public double? AssetNumber { get; set; }
        public double? AssetLatitude { get; set; }
        public double? AssetLongitude { get; set; }
    }
}
