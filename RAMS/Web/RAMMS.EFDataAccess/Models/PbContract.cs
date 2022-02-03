using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PbContract
    {
        public PbContract()
        {
            PbContractYear = new HashSet<PbContractYear>();
        }

        public int Pk { get; set; }
        public DateTime ContractSignDate { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public string Etc { get; set; }

        public virtual ICollection<PbContractYear> PbContractYear { get; set; }
    }
}
