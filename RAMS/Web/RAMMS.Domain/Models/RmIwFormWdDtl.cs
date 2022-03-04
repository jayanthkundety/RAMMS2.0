using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmIwFormWdDtl
    {
        public int FwddPkRefNo { get; set; }
        public int? FwddFwdPkRefNo { get; set; }
        public string FwddReason { get; set; }
        public string FwddClause { get; set; }
        public double? FwddExtnPrd { get; set; }
        public int? FwddModBy { get; set; }
        public DateTime? FwddModDt { get; set; }
        public int? FwddCrBy { get; set; }
        public DateTime? FwddCrDt { get; set; }

        public virtual RmIwFormWd FwddFwdPkRefNoNavigation { get; set; }
    }
}
