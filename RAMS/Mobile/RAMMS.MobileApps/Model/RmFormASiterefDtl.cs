using System;
using System.Collections.Generic;

namespace RAMMS.MobileApps
{
    public partial class RmFormASiterefDtl
    {
        public int FsdPkRefId { get; set; }
        public int? FsdFadPkRefNo { get; set; }
        public int? FsdFadSrno { get; set; }
        public int? FsdSiteRefId { get; set; }
        public string FsdFadSiteRef { get; set; }
        public string FsdDefCode { get; set; }
        public string FsdDefDesc { get; set; }
        public string AioModBy { get; set; }
        public DateTime? AioModDt { get; set; }
        public string AioCrBy { get; set; }
        public DateTime? AioCrDt { get; set; }
        public bool AioSubmitSts { get; set; }
        public bool? AioActiveYn { get; set; }

        public virtual RmFormADtl FsdFadPkRefNoNavigation { get; set; }
    }
}
