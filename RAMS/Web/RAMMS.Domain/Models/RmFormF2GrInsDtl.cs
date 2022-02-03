using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormF2GrInsDtl
    {
        public int FgridPkRefNo { get; set; }
        public int? FgridFgrihPkRefNo { get; set; }
        public int? FgrihAiPkRefNo { get; set; }
        public int? FgridStartingChKm { get; set; }
        public string FgridGrCode { get; set; }
        public double? FgridGrCondition1 { get; set; }
        public double? FgridGrCondition2 { get; set; }
        public double? FgridGrCondition3 { get; set; }
        public string FgridRhsMLhs { get; set; }
        public double? FgridPostSpac { get; set; }
        public string FgridRemarks { get; set; }
        public int? FgridModBy { get; set; }
        public DateTime? FgridModDt { get; set; }
        public int? FgridCrBy { get; set; }
        public DateTime? FgridCrDt { get; set; }
        public bool FgridSubmitSts { get; set; }
        public bool? FgridActiveYn { get; set; }
        public string FgridStartingChM { get; set; }
        public double? FgridLength { get; set; }

        public virtual RmFormF2GrInsHdr FgridFgrihPkRefNoNavigation { get; set; }
    }
}
