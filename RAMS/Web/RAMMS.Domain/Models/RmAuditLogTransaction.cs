using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmAuditLogTransaction
    {
        public long AltPkRefNo { get; set; }
        public long AltAlaPkRefNo { get; set; }
        public string AltTransactionName { get; set; }
        public string AltTableName { get; set; }
        public string AltTransactinDetails { get; set; }

        public virtual RmAuditLogAction AltAlaPkRefNoNavigation { get; set; }
    }
}
