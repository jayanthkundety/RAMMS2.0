using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmAuditLogAction
    {
        public RmAuditLogAction()
        {
            RmAuditLogTransaction = new HashSet<RmAuditLogTransaction>();
        }

        public long AlaPkRefNo { get; set; }
        public string AlaRequestIp { get; set; }
        public string AlaRequester { get; set; }
        public string AlaActionName { get; set; }
        public DateTime AlaCrDt { get; set; }
        public int? AlaCrBy { get; set; }

        public virtual ICollection<RmAuditLogTransaction> RmAuditLogTransaction { get; set; }
    }
}
