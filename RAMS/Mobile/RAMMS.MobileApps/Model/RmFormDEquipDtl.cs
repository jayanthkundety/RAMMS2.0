using System;
using System.Collections.Generic;

namespace RAMMS.MobileApps
{
    public partial class RmFormDEquipDtl
    {
        public int FdedPkRefNo { get; set; }
        public int? FdedFdedPkRefNo { get; set; }
        public int? FdedSrno { get; set; }
        public string FdedEqpCode { get; set; }
        public string FdedEqpDesc { get; set; }
        public decimal? FdedEqpQty { get; set; }
        public string FdedEqpUnit { get; set; }
        public string FdedModBy { get; set; }
        public DateTime? FdedModDt { get; set; }
        public string FdedCrBy { get; set; }
        public DateTime? FdedCrDt { get; set; }
        public bool FdedSubmitSts { get; set; }
        public bool? FdedActiveYn { get; set; }

        public virtual RmFormDHdr FdedFdedPkRefNoNavigation { get; set; }
    }
}
