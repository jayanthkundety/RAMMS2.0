using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmIwFormW2Fcem
    {
        public int FcemPkRefNo { get; set; }
        public int? FcemFw2PkRefNo { get; set; }
        public DateTime? FcemDate { get; set; }
        public string FcemSstatus { get; set; }
        public double? FcemProgress { get; set; }
        public bool? FcemIsBq { get; set; }
        public bool? FcemIsDrawing { get; set; }
        public string FcemRemark { get; set; }
        public int? FcemModBy { get; set; }
        public DateTime? FcemModDt { get; set; }
        public int? FcemCrBy { get; set; }
        public DateTime? FcemCrDt { get; set; }
        public bool? FcemSubmitSts { get; set; }
        public bool FcemActiveYn { get; set; }
        public string FcemStatus { get; set; }
        public string FcemAuditLog { get; set; }

        public virtual RmIwFormW2 FcemFw2PkRefNoNavigation { get; set; }
    }
}
