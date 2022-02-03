using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PbContractMonth
    {
        public PbContractMonth()
        {
            AuditSession = new HashSet<AuditSession>();
            KpiBucketSizeParam = new HashSet<KpiBucketSizeParam>();
        }

        public int Pk { get; set; }
        public int PbContractYearPk { get; set; }
        public int MonthNo { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Etc { get; set; }

        public virtual PbContractYear PbContractYearPkNavigation { get; set; }
        public virtual ICollection<AuditSession> AuditSession { get; set; }
        public virtual ICollection<KpiBucketSizeParam> KpiBucketSizeParam { get; set; }
    }
}
