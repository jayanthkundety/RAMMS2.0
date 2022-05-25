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
        public string Fg2hSpName { get; set; }
        public string Fg2hSpDesignation { get; set; }
        public DateTime? Fg2hSpInspDate { get; set; }
        public int? Fg2hInspectedBy { get; set; }
        public bool? Fg2hInspectedBySign { get; set; }
        public string Fg2hEcName { get; set; }
        public string Fg2hEcDesignation { get; set; }
        public DateTime? Fg2hEcInspDate { get; set; }
        public int? Fg2hRating { get; set; }
        public bool? Fg2hIssuesFound { get; set; }
        public int? Fg2hModBy { get; set; }
        public DateTime? Fg2hModDt { get; set; }
        public int? Fg2hCrBy { get; set; }
        public DateTime? Fg2hCrDt { get; set; }
        public bool Fg2hSubmitSts { get; set; }
        public bool Fg2hActiveYn { get; set; }
        public string Fg2hStatus { get; set; }
        public string Fg2hAuditLog { get; set; }

        public virtual RmFormG1Hdr Fg2hFg1hPkRefNoNavigation { get; set; }
    }
}
