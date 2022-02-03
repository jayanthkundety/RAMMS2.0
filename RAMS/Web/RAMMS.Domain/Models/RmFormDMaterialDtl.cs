using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormDMaterialDtl
    {
        public int FdmdPkRefNo { get; set; }
        public int? FdmdFdhPkRefNo { get; set; }
        public int? FdmdSrno { get; set; }
        public string FdmdMtCode { get; set; }
        public string FdmdMtDesc { get; set; }
        public decimal? FdmdMtQty { get; set; }
        public string FdmdMtUnit { get; set; }
        public string FdmdModBy { get; set; }
        public DateTime? FdmdModDt { get; set; }
        public string FdmdCrBy { get; set; }
        public DateTime? FdmdCrDt { get; set; }
        public bool FdmdSubmitSts { get; set; }
        public bool? FdmdActiveYn { get; set; }
        public string FdmdCodeDesc { get; set; }

        public virtual RmFormDHdr FdmdFdhPkRefNoNavigation { get; set; }
    }
}
