using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvBridgeRailing
    {
        public int Pk { get; set; }
        public double? AssetFromKm { get; set; }
        public double? AssetToKm { get; set; }
        public string RailingType { get; set; }
        public string Grade { get; set; }
        public double? RailingHeight { get; set; }
        public double? RailingLength { get; set; }
        public string LongitudinalSize { get; set; }
        public string LongitudinalLayer { get; set; }
        public double? LongitudinalVerticalSpacingUpper { get; set; }
        public double? LongitudinalVerticalSpacingMedian { get; set; }
        public double? LongitudinalVerticalSpacingLower { get; set; }
        public string PostSize { get; set; }
        public double? PostSpacing { get; set; }
        public double? BasePlate { get; set; }
        public bool? ParapetEndTreatment { get; set; }
        public string DelineatorStripReflectivity { get; set; }
        public double? HorizontalClearance { get; set; }
        public double? EmbeddedDepth { get; set; }
        public double? AssetLongitude { get; set; }
        public double? AssetLatitude { get; set; }
        public string Quantity { get; set; }
    }
}
