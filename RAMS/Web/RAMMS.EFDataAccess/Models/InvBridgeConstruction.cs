using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvBridgeConstruction
    {
        public int Pk { get; set; }
        public string ContractNumber { get; set; }
        public string Owner { get; set; }
        public string ContractTitle { get; set; }
        public string ContractStatus { get; set; }
        public DateTime? ContractDate { get; set; }
        public string PackageNumber { get; set; }
        public int InvMasterPk { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
