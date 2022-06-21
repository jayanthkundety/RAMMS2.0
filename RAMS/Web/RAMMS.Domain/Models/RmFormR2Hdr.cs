using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormR2Hdr
    {
        public int Fr2hPkRefNo { get; set; }
        public int? Fr2hFr1hPkRefNo { get; set; }
        public string Fr2hDistressSp { get; set; }
        public string Fr2hDistressEc { get; set; }
        public string Fr2hGeneralSp { get; set; }
        public string Fr2hGeneralEc { get; set; }
        public string Fr2hFeedbackSp { get; set; }
        public string Fr2hFeedbackEc { get; set; }
        public string Fr2hSpName { get; set; }
        public string Fr2hSpDesignation { get; set; }
        public DateTime? Fr2hSpInspDate { get; set; }
        public string Fr2hInspectedBy { get; set; }
        public string Fr2hInspectedBySign { get; set; }
        public string Fr2hEcName { get; set; }
        public string Fr2hEcDesignation { get; set; }
        public DateTime? Fr2hEcInspDate { get; set; }
        public int? Fr2hRating { get; set; }
        public bool? Fr2hIssuesFound { get; set; }
        public int? Fr2hModBy { get; set; }
        public DateTime? Fr2hModDt { get; set; }
        public int? Fr2hCrBy { get; set; }
        public DateTime? Fr2hCrDt { get; set; }
        public bool Fr2hSubmitSts { get; set; }
        public bool Fr2hActiveYn { get; set; }

        public virtual RmFormR1Hdr Fr2hFr1hPkRefNoNavigation { get; set; }
    }
}
