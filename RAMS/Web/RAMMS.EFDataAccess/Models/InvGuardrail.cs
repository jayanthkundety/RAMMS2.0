using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvGuardrail
    {
        public int Pk { get; set; }
        public double? AssetKmlocation { get; set; }
        public double? AssetFromKm { get; set; }
        public double? AssetToKm { get; set; }
        public string RampId { get; set; }
        public string Type { get; set; }
        public string AssetLocation { get; set; }
        public string Rigidity { get; set; }
        public string PostType { get; set; }
        public string PackerType { get; set; }
        public double? PackerNo { get; set; }
        public double? PostSpacing { get; set; }
        public double? Heightfromroadlevel { get; set; }
        public double? Heightfromgroundlevel { get; set; }
        public double? Length { get; set; }
        public double? DelineatorStripReflectivity { get; set; }
        public double? ZincCoatingThickness { get; set; }
        public double? EmbeddedDepth { get; set; }
        public double? TerminalLength { get; set; }
        public double? TerminalDeflectionLength { get; set; }
        public bool TransitionTreatment { get; set; }
        public double? HorizontalClearance { get; set; }
        public double? AssetNumber { get; set; }
        public double? AssetLatitude { get; set; }
        public double? AssetLongitude { get; set; }
    }
}
