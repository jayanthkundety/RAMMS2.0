using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvBrParapet
    {
        public int Pk { get; set; }
        public string BridgeId { get; set; }
        public double? AssetKmlocation { get; set; }
        public double? AssetFromKm { get; set; }
        public double? AssetToKm { get; set; }
        public string RampId { get; set; }
        public string AssetType { get; set; }
        public string Grade { get; set; }
        public double? Height { get; set; }
        public double? Length { get; set; }
        public string LongitudinalBarSize { get; set; }
        public string LongitudinalBarLayer { get; set; }
        public double? LongitudinalBarVsupper { get; set; }
        public double? LongitudinalBarVsmedian { get; set; }
        public string LongitudinalBarVslower { get; set; }
        public string PostSize { get; set; }
        public double? PostSpacing { get; set; }
        public double? BasePlate { get; set; }
        public bool ParapetEndTreatment { get; set; }
        public double? DelineatorStripReflectivity { get; set; }
        public double? HorizontalClearance { get; set; }
        public double? EmbeddedDepth { get; set; }
        public string AssetLocation { get; set; }
        public double? AssetNumber { get; set; }
        public double? AssetLatitude { get; set; }
        public double? AssetLongitude { get; set; }
    }
}
