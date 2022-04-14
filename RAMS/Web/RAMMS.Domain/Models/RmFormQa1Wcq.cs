using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormQa1Wcq
    {
        public int Fqa1wcqPkRefNo { get; set; }
        public int? Fqa1wcqFqa1hPkRefNo { get; set; }
        public string Fqa1wcqFlType { get; set; }
        public decimal? Fqa1wcqFl { get; set; }
        public int? Fqa1wcqFlUnit { get; set; }
        public string Fqa1wcqFlRemark { get; set; }
        public string Fqa1wcqJType { get; set; }
        public string Fqa1wcqJRemark { get; set; }
        public string Fqa1wcqSrType { get; set; }
        public string Fqa1wcqSrRemark { get; set; }
        public int? Fqa1wcqModBy { get; set; }
        public DateTime? Fqa1wcqModDt { get; set; }
        public int? Fqa1wcqCrBy { get; set; }
        public DateTime? Fqa1wcqCrDt { get; set; }

        public virtual RmFormQa1Hdr Fqa1wcqFqa1hPkRefNoNavigation { get; set; }
    }
}
