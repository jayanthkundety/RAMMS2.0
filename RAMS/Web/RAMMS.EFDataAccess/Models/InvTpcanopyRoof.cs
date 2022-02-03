using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvTpcanopyRoof
    {
        public int Pk { get; set; }
        public string TollPlazaId { get; set; }
        public string TypeofRoof { get; set; }
        public double? TotalArea { get; set; }
        public string TypeofGutter { get; set; }
        public int? GutterQty { get; set; }
        public double? GutterWidth { get; set; }
        public double? GutterDepth { get; set; }
        public double? GutterLength { get; set; }
        public string GutterRemarks { get; set; }
        public int? RainwaterDownPipeQty { get; set; }
        public string Rwdplocation { get; set; }
        public string TypeofRwdp { get; set; }
        public double? Rwdpdiameter { get; set; }
        public double? Rwdplength { get; set; }
        public string Rwdpremarks { get; set; }
    }
}
