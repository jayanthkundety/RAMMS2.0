using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvDelPost
    {
        public int Pk { get; set; }
        public double? AssetKmlocation { get; set; }
        public double? AssetFromKm { get; set; }
        public double? AssetToKm { get; set; }
        public string RampId { get; set; }
        public string MaterialType { get; set; }
        public string DelineatorStripReflectivity { get; set; }
        public double? HorizontalClearance { get; set; }
        public string StripType { get; set; }
        public double? StripHeightFrl { get; set; }
        public bool FrontView { get; set; }
        public string StripColorFront { get; set; }
        public bool BackView { get; set; }
        public string StripColorBack { get; set; }
        public string Interval { get; set; }
        public double? PostDimensionHeight { get; set; }
        public double? PostDimensionDiameter { get; set; }
        public double? PostDimensionThickness { get; set; }
        public string InstallationType { get; set; }
        public string InstallationSpacing { get; set; }
        public string AssetLocation { get; set; }
        public double? AssetNumber { get; set; }
        public double? AssetLatitude { get; set; }
        public double? AssetLongitude { get; set; }
    }
}
