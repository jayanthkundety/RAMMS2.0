using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvTunnelConstruction
    {
        public int Pk { get; set; }
        public string ContractNumber { get; set; }
        public string ContractStatus { get; set; }
        public DateTime? ContractDate { get; set; }
        public string PackageNumber { get; set; }
        public int InvMasterPk { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Description { get; set; }
        public string ChainageStartKm { get; set; }
        public string ChainageEndKm { get; set; }
        public string Remark { get; set; }
    }
}
