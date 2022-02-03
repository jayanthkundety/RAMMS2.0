using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class KpiItem
    {
        public KpiItem()
        {
            AuditResult = new HashSet<AuditResult>();
            Complaint = new HashSet<Complaint>();
            KpiItemAuditCondition = new HashSet<KpiItemAuditCondition>();
        }

        public int Pk { get; set; }
        public int KpiGroupL3pk { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Weighting { get; set; }
        public bool IsAudit { get; set; }

        public virtual KpiGroupL3 KpiGroupL3pkNavigation { get; set; }
        public virtual ICollection<AuditResult> AuditResult { get; set; }
        public virtual ICollection<Complaint> Complaint { get; set; }
        public virtual ICollection<KpiItemAuditCondition> KpiItemAuditCondition { get; set; }
    }
}
