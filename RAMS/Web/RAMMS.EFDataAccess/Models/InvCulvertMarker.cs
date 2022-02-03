using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvCulvertMarker
    {
        public int Pk { get; set; }
        public string Cmnumber { get; set; }
        public string InletSignFaceMaterial { get; set; }
        public double? InletSignFaceWidth { get; set; }
        public double? InletSignFaceHeight { get; set; }
        public double? InletSignFaceThickness { get; set; }
        public string InletInstallationType { get; set; }
        public double? InletInstallationYear { get; set; }
        public string InletPostMaterial { get; set; }
        public string InletOverallCondition { get; set; }
        public double? InletAssetLatitude { get; set; }
        public double? InletAssetLongitude { get; set; }
        public string OutletSignFaceMaterial { get; set; }
        public double? OutletSignFaceWidth { get; set; }
        public double? OutletSignFaceHeight { get; set; }
        public double? OutletSignFaceThickness { get; set; }
        public string OutletInstallationType { get; set; }
        public double? OutletInstallationYear { get; set; }
        public string OutletPostMaterial { get; set; }
        public string OutletOverallCondition { get; set; }
    }
}
