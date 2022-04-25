using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormQa1Wcq
    {
        public int Fqa1wcqPkRefNo { get; set; }
        public int? Fqa1wcqFqa1hPkRefNo { get; set; }
        public bool? Fqa1wcqFlFlushType { get; set; }
        public decimal? Fqa1wcqFlFlush { get; set; }
        public string Fqa1wcqFlFlushRemark { get; set; }
        public bool? Fqa1wcqFlThType { get; set; }
        public decimal? Fqa1wcqFlTh { get; set; }
        public string Fqa1wcqFlThRemark { get; set; }
        public bool? Fqa1wcqFlTlType { get; set; }
        public decimal? Fqa1wcqFlTl { get; set; }
        public string Fqa1wcqFlTlRemark { get; set; }
        public bool? Fqa1wcqFlScType { get; set; }
        public string Fqa1wcqFlScRemark { get; set; }
        public bool? Fqa1wcqFlUcType { get; set; }
        public string Fqa1wcqFlUcRemark { get; set; }
        public bool? Fqa1wcqJnType { get; set; }
        public string Fqa1wcqJnRemark { get; set; }
        public bool? Fqa1wcqJiType { get; set; }
        public string Fqa1wcqJiRemark { get; set; }
        public bool? Fqa1wcqSrevType { get; set; }
        public string Fqa1wcqSrevRemark { get; set; }
        public bool? Fqa1wcqSruevType { get; set; }
        public string Fqa1wcqSruevRemark { get; set; }
        public bool? Fqa1wcqSrprType { get; set; }
        public string Fqa1wcqSrprRemark { get; set; }
        public bool Fqa1wcqActiveYn { get; set; }
        public int? Fqa1wcqModBy { get; set; }
        public DateTime? Fqa1wcqModDt { get; set; }
        public int? Fqa1wcqCrBy { get; set; }
        public DateTime? Fqa1wcqCrDt { get; set; }

        public virtual RmFormQa1Hdr Fqa1wcqFqa1hPkRefNoNavigation { get; set; }
    }
}
