using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvTplane
    {
        public int Pk { get; set; }
        public string TollPlazaId { get; set; }
        public string LaneCode { get; set; }
        public string LaneType { get; set; }
        public string LaneDirection { get; set; }
        public string Reversible { get; set; }
        public string VehicleType { get; set; }
        public double? LaneWidth { get; set; }
        public string TransactionMode { get; set; }
        public int? HeightLimitBarQty { get; set; }
        public string Hlblocation { get; set; }
        public string Hlbtype { get; set; }
        public double? Hlbwidth { get; set; }
        public double? Hlbheight { get; set; }
        public string Hlbremarks { get; set; }
        public string SignagesCategory { get; set; }
        public int? GantrySignboardQty { get; set; }
        public string GantrySignboardType { get; set; }
        public int? GantrySignagesQty { get; set; }
        public string SignagesType { get; set; }
        public string SignagesLocation { get; set; }
        public double? SignagesWidth { get; set; }
        public double? SignagesHeight { get; set; }
        public string SignagesRemarks { get; set; }
        public string GantryBarType { get; set; }
        public int? GantryBarQty { get; set; }
        public double? GantryBarDiameter { get; set; }
        public double? GantryBarLength { get; set; }
        public double? GantryBarHeight { get; set; }
    }
}
