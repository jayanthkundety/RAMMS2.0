using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvCulvertConstruction
    {
        public int Pk { get; set; }
        public int InvMasterPk { get; set; }
        public string ContractNumber { get; set; }
        public string DeveloperName { get; set; }
        public string ContractTitle { get; set; }
        public string ContractStatus { get; set; }
        public string PackageNumber { get; set; }
        public string Chainage { get; set; }
        public string YearBuilt { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
    }
}
