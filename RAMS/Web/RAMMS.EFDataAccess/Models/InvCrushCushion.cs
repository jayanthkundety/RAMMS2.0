using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvCrushCushion
    {
        public int Pk { get; set; }
        public double? AssetKmlocation { get; set; }
        public double? AssetFromKm { get; set; }
        public double? AssetToKm { get; set; }
        public string RampId { get; set; }
        public string DesignType { get; set; }
        public double? Height { get; set; }
        public string SpeedLimit { get; set; }
        public string CatridgeType { get; set; }
        public string CatridgeNos { get; set; }
        public string ReserveGoreAreaLength { get; set; }
        public string ReserveGoreAreaWidth { get; set; }
        public string ReserveGoreAreaLhs { get; set; }
        public string ReserveGoreAreaRhs { get; set; }
        public DateTime? DateofInstallation { get; set; }
        public string BaseType { get; set; }
        public string BeamPanel { get; set; }
        public string HighSpeedCatridge { get; set; }
        public string Monorail { get; set; }
        public string YellowNose { get; set; }
        public string SteelBackup { get; set; }
        public string Diaphragm { get; set; }
        public string HighSpeedDiaphragm { get; set; }
        public string AssetLocation { get; set; }
        public double? AssetNumber { get; set; }
        public double? AssetLatitude { get; set; }
        public double? AssetLongitude { get; set; }
    }
}
