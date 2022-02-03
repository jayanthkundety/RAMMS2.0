using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormDEquipDtl
    {
        public int FdedPkRefNo { get; set; }
        public int? FdedFdhPkRefNo { get; set; }
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
        public string FdedCodeDesc { get; set; }

        public virtual RmFormDHdr FdedFdhPkRefNoNavigation { get; set; }
    }
}
