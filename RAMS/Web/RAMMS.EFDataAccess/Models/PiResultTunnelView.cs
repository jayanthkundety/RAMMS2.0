using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PiResultTunnelView
    {
        public string SectionCode { get; set; }
        public string FeatureBound { get; set; }
        public string FeatureRoute { get; set; }
        public string TunnelName { get; set; }
        public double? TunnelKmlocation { get; set; }
        public double? TunnelLength { get; set; }
        public double? TunnelPanelCount { get; set; }
        public double? TunnelClearance { get; set; }
        public double? TunnelLatitude { get; set; }
        public double? TunnelLongitude { get; set; }
        public int Pk { get; set; }
        public int? PiSchedulePk { get; set; }
        public string GenInfoTrafficControl { get; set; }
    }
}
