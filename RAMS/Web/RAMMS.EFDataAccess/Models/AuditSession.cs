using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class AuditSession
    {
        public AuditSession()
        {
            AuditResult = new HashSet<AuditResult>();
        }

        public int Pk { get; set; }
        public string Id { get; set; }
        public int PbContractMonthPk { get; set; }
        public int SectionPk { get; set; }
        public string Type { get; set; }
        public double Kmfrom { get; set; }
        public double Kmto { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual PbContractMonth PbContractMonthPkNavigation { get; set; }
        public virtual Section SectionPkNavigation { get; set; }
        public virtual ICollection<AuditResult> AuditResult { get; set; }
    }
}
