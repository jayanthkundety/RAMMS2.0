using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormG2Hdr
    {
        public int Fg2hPkRefNo { get; set; }
        public int? Fg2hFg1hPkRefNo { get; set; }
        public string Fg2hDistressSp { get; set; }
        public string Fg2hDistressEc { get; set; }
        public string Fg2hGeneralSp { get; set; }
        public string Fg2hGeneralEc { get; set; }
        public string Fg2hFeedbackSp { get; set; }
        public string Fg2hFeedbackEc { get; set; }
        public int? Fg2hModBy { get; set; }
        public DateTime? Fg2hModDt { get; set; }
        public int? Fg2hCrBy { get; set; }
        public DateTime? Fg2hCrDt { get; set; }
        public bool Fg2hSubmitSts { get; set; }
        public bool Fg2hActiveYn { get; set; }

        public virtual RmFormG1Hdr Fg2hFg1hPkRefNoNavigation { get; set; }
    }
}
