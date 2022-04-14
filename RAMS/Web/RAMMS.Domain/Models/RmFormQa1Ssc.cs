using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormQa1Ssc
    {
        public int Fqa1sscPkRefNo { get; set; }
        public int? Fqa1sscFqa1hPkRefNo { get; set; }
        public string Fqa1sscSp { get; set; }
        public string Fqa1sscSpRemark { get; set; }
        public string Fqa1sscEd { get; set; }
        public string Fqa1sscEdRemark { get; set; }
        public string Fqa1sscWpe { get; set; }
        public string Fqa1sscWpeRemark { get; set; }
        public string Fqa1sscIms { get; set; }
        public string Fqa1sscImsRemark { get; set; }
        public string Fqa1sscAsd { get; set; }
        public string Fqa1sscAsdRemark { get; set; }
        public int? Fqa1sscModBy { get; set; }
        public DateTime? Fqa1sscModDt { get; set; }
        public int? Fqa1sscCrBy { get; set; }
        public DateTime? Fqa1sscCrDt { get; set; }

        public virtual RmFormQa1Hdr Fqa1sscFqa1hPkRefNoNavigation { get; set; }
    }
}
