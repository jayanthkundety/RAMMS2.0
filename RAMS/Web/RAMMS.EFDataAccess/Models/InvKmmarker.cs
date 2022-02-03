using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvKmmarker
    {
        public int Pk { get; set; }
        public double? AssetKmlocation { get; set; }
        public string RampId { get; set; }
        public string AssetInvType { get; set; }
        public double? GroundLevelHeight { get; set; }
        public double? Reflectivity { get; set; }
        public double? HorizontalClearance { get; set; }
        public double? Interval { get; set; }
        public string SignFace { get; set; }
        public string SignFaceMaterial { get; set; }
        public string SignFaceDimension { get; set; }
        public double? SignFaceDimensionLength { get; set; }
        public double? SignFaceDimensionWidth { get; set; }
        public double? SignFaceDimensionThickness { get; set; }
        public string InstallationType { get; set; }
        public string PostMaterial { get; set; }
        public string AssetLocation { get; set; }
        public double? AssetNumber { get; set; }
        public double? AssetLatitude { get; set; }
        public double? AssetLongitude { get; set; }
    }
}
