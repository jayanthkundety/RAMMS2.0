using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PbContractYear
    {
        public PbContractYear()
        {
            PbContractMonth = new HashSet<PbContractMonth>();
        }

        public int Pk { get; set; }
        public int PbContractPk { get; set; }
        public int YearNo { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Etc { get; set; }

        public virtual PbContract PbContractPkNavigation { get; set; }
        public virtual ICollection<PbContractMonth> PbContractMonth { get; set; }
    }
}
