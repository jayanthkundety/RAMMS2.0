using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvPavMarker
    {
        public int Pk { get; set; }
        public string RampId { get; set; }
        public string MaterialType { get; set; }
        public double? Brand { get; set; }
        public string Position { get; set; }
        public double? Quantity { get; set; }
        public double? Interval { get; set; }
        public string Color { get; set; }
        public string Arrangement { get; set; }
        public string DimensionLength { get; set; }
        public double? DimensionWidth { get; set; }
        public double? DimensionThickness { get; set; }
        public double? DimensionDiameter { get; set; }
        public string AssetLocation { get; set; }
        public double? AssetNumber { get; set; }
        public double? AssetLatitude { get; set; }
        public double? AssetLongitude { get; set; }
    }
}
