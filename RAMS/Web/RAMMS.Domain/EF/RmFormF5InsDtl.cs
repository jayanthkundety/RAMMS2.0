using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormF5InsDtl
    {
        public int FvadPkRefNo { get; set; }
        public int? FvadFvahPkRefNo { get; set; }
        public int? FvadFbrihPkRefNo { get; set; }
        public int? FvadLocChKm { get; set; }
        public string FvadLocChM { get; set; }
        public string FvadStrucCode { get; set; }
        public string FvadRiverName { get; set; }
        public double? FvadLength { get; set; }
        public double? FvadWidth { get; set; }
        public int? FvadSpanCnt { get; set; }
        public int? FvadCondition { get; set; }
        public string FvadRemarks { get; set; }
        public int? FvadModBy { get; set; }
        public DateTime? FvadModDt { get; set; }
        public int? FvadCrBy { get; set; }
        public DateTime? FvadCrDt { get; set; }
        public bool FvadSubmitSts { get; set; }
        public bool? FvadActiveYn { get; set; }

        public virtual RmFormB1b2BrInsHdr FvadFbrihPkRefNoNavigation { get; set; }
        public virtual RmFormF5InsHdr FvadFvahPkRefNoNavigation { get; set; }
    }
}
