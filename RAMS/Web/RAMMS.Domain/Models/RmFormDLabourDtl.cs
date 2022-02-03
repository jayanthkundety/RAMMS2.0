using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormDLabourDtl
    {
        public int FdldPkRefNo { get; set; }
        public int? FdldFdhPkRefNo { get; set; }
        public int? FdldSrno { get; set; }
        public string FdldLabCode { get; set; }
        public string FdldLabDesc { get; set; }
        public int? FdldLabQty { get; set; }
        public string FdldLabUnit { get; set; }
        public string FdldModBy { get; set; }
        public DateTime? FdldModDt { get; set; }
        public string FdldCrBy { get; set; }
        public DateTime? FdldCrDt { get; set; }
        public bool FdldSubmitSts { get; set; }
        public bool? FdldActiveYn { get; set; }
        public string FdldCodeDesc { get; set; }

        public virtual RmFormDHdr FdldFdhPkRefNoNavigation { get; set; }
    }
}
