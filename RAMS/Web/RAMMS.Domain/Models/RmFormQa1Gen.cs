using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormQa1Gen
    {
        public int Fqa1genPkRefNo { get; set; }
        public int? Fqa1genFqa1hPkRefNo { get; set; }
        public string Fqa1genItem { get; set; }
        public string Fqa1genAttTo { get; set; }
        public string Fqa1genAttRemarks { get; set; }
        public int? Fqa1genModBy { get; set; }
        public DateTime? Fqa1genModDt { get; set; }
        public int? Fqa1genCrBy { get; set; }
        public DateTime? Fqa1genCrDt { get; set; }

        public virtual RmFormQa1Hdr Fqa1genFqa1hPkRefNoNavigation { get; set; }
    }
}
