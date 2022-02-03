using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvNjb
    {
        public int Pk { get; set; }
        public double? AssetKmlocation { get; set; }
        public double? AssetFromKm { get; set; }
        public double? AssetToKm { get; set; }
        public string RampId { get; set; }
        public string AssetType { get; set; }
        public string AssetLocation { get; set; }
        public double? Height { get; set; }
        public double? Length { get; set; }
        public string DelineatorStripReflectivity { get; set; }
        public string HorizontalClearance { get; set; }
        public string EmbeddedDepth { get; set; }
        public string EndTreatment { get; set; }
        public double? AssetNumber { get; set; }
        public double? AssetLatitude { get; set; }
        public double? AssetLongitude { get; set; }
    }
}
