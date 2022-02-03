using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvFlexPost
    {
        public int Pk { get; set; }
        public double? AssetKmlocation { get; set; }
        public double? AssetFromKm { get; set; }
        public double? AssetToKm { get; set; }
        public string RampId { get; set; }
        public string MaterialType { get; set; }
        public string Brand { get; set; }
        public string PoleDimension { get; set; }
        public double? PoleDimensionHeight { get; set; }
        public double? PoleDimensionBaseDiameter { get; set; }
        public double? PoleDimensionPoleDiameter { get; set; }
        public string Arrangement { get; set; }
        public double? Quantity { get; set; }
        public double? Spacing { get; set; }
        public string InstallationType { get; set; }
        public string AssetColor { get; set; }
        public string AssetLocation { get; set; }
        public double? AssetNumber { get; set; }
        public double? AssetLatitude { get; set; }
        public double? AssetLongitude { get; set; }
    }
}
