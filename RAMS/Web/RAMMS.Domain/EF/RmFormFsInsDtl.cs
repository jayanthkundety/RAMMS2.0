using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormFsInsDtl
    {
        public int FsdPkRefNo { get; set; }
        public int? FsdFshPkRefNo { get; set; }
        public string FsdFeature { get; set; }
        public string FsdGrpType { get; set; }
        public string FsdStrucCode { get; set; }
        public double? FsdWidth { get; set; }
        public double? FsdLength { get; set; }
        public decimal? FsdCondition1 { get; set; }
        public decimal? FsdCondition2 { get; set; }
        public decimal? FsdCondition3 { get; set; }
        public string FsdNeeded { get; set; }
        public string FsdUnit { get; set; }
        public string FsdRemarks { get; set; }
        public int? FsdModBy { get; set; }
        public DateTime? FsdModDt { get; set; }
        public int? FsdCrBy { get; set; }
        public DateTime? FsdCrDt { get; set; }
        public bool FsdSubmitSts { get; set; }
        public bool? FsdActiveYn { get; set; }
        public string FsdGrpCode { get; set; }

        public virtual RmFormFsInsHdr FsdFshPkRefNoNavigation { get; set; }
    }
}
