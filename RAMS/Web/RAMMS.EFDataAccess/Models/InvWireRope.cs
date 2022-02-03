using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvWireRope
    {
        public int Pk { get; set; }
        public string RampId { get; set; }
        public string TollPlaza { get; set; }
        public string Type { get; set; }
        public string LinePostDimension { get; set; }
        public double? LinePostPostInterval { get; set; }
        public double? LinePostHeight { get; set; }
        public double? LinePostLength { get; set; }
        public double? LinePostQuantity { get; set; }
        public double? LinePostCap { get; set; }
        public double? LinePostRoller { get; set; }
        public string DeflectionPost { get; set; }
        public double? DeflectionPostPostInterval { get; set; }
        public double? DeflectionPostHeight { get; set; }
        public double? DeflectionPostLength { get; set; }
        public double? DeflectionPostQuantity { get; set; }
        public string DeflectionPostCap { get; set; }
        public double? DeflectionPostRoller { get; set; }
        public double? GroundAnchor { get; set; }
        public double? VerticalAnchor { get; set; }
        public string RiggingScrew { get; set; }
        public string LhsorRhstreadedTerminal { get; set; }
        public string DoubleTreadedTerminal { get; set; }
        public string TensioningSystem { get; set; }
        public string Shackle { get; set; }
        public string DelineatorStripReflectivity { get; set; }
        public string HorizontalClearance { get; set; }
        public string ReflectiveSticker { get; set; }
        public string EmbeddedDepth { get; set; }
        public string AssetLocation { get; set; }
        public double? AssetNumber { get; set; }
        public double? AssetLatitude { get; set; }
        public double? AssetLongitude { get; set; }
    }
}
