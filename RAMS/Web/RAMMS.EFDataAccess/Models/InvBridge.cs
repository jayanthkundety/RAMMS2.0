using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvBridge
    {
        public int Pk { get; set; }
        public string AssetName { get; set; }
        public double? AssetKmlocation { get; set; }
        public string Owner { get; set; }
        public double? SpanCount { get; set; }
        public double? JointCount { get; set; }
        public double? JointLength { get; set; }
        public double? JointLengthTotal { get; set; }
        public double? LengthTotal { get; set; }
        public string ScourEffect { get; set; }
        public double? SkewAngle { get; set; }
        public double? ClearanceHeight { get; set; }
        public double? AssetLatitude { get; set; }
        public double? AssetLongitude { get; set; }
        public string SerialNumber { get; set; }
        public string YearOpen { get; set; }
        public string SourceInfo { get; set; }
        public string DesignAxleLoad { get; set; }
        public string DesignWaterLevel { get; set; }
        public string GeneralStructureType { get; set; }
        public string CrossingFeature { get; set; }
        public string SuperstructureType { get; set; }
        public string DeckDrainageType { get; set; }
        public double? TotalWidth { get; set; }
        public string SlopeRiverBankProtectionType { get; set; }
        public string CarriagewayLayout { get; set; }
        public string DesignFloodDischarge { get; set; }
        public string ParapetType { get; set; }
        public string SpanConfiguration { get; set; }
        public string DesignReturnPeriod { get; set; }

        public virtual InvMaster InvMaster { get; set; }
    }
}
