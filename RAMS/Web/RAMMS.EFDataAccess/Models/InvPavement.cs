using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvPavement
    {
        public int Pk { get; set; }
        public string LaneNumber { get; set; }
        public double? AssetFromKm { get; set; }
        public double? AssetToKm { get; set; }
        public double? NumberofLane { get; set; }
        public double? LaneWidth { get; set; }
        public double? CarriagewayWidth { get; set; }
        public double? ShoulderWidth { get; set; }
        public double? MedianWidth { get; set; }
        public string MedianTypeofSurfacing { get; set; }
        public bool IsFlexiblePavement { get; set; }
        public double? FlexiblePavementLength { get; set; }
        public bool IsFlexibleComposite { get; set; }
        public double? FlexibleCompositeLength { get; set; }
        public bool IsBridge { get; set; }
        public string BridgeId { get; set; }
        public double? BridgeLength { get; set; }
        public bool IsJrcp { get; set; }
        public double? Jrcplength { get; set; }
        public bool IsCrcp { get; set; }
        public double? Crcplength { get; set; }
        public double? LeftMarginalStrip { get; set; }
        public double? RightMarginalStrip { get; set; }
        public string BridgeName { get; set; }
    }
}
