using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvRoadMarking
    {
        public int Pk { get; set; }
        public double? AssetKmlocation { get; set; }
        public double? AssetFromKm { get; set; }
        public double? AssetToKm { get; set; }
        public string RampId { get; set; }
        public string MaterialType { get; set; }
        public double? NoofLane { get; set; }
        public string TypeofMarking { get; set; }
        public string TypeofMarkingLongitudinal { get; set; }
        public string TypeofMarkingTraverse { get; set; }
        public string TypeofMarkingArrow { get; set; }
        public string TypeofMarkingOthers { get; set; }
        public string DimensionGap { get; set; }
        public double? DimensionLength { get; set; }
        public double? DimensionWidth { get; set; }
        public double? DimensionThickness { get; set; }
        public double? Reflectivity { get; set; }
        public string Color { get; set; }
        public string AssetLocation { get; set; }
        public double? Number { get; set; }
        public double? AssetLatitude { get; set; }
        public double? AssetLongitude { get; set; }
    }
}
