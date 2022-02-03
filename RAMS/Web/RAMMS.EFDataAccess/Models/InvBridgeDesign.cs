using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvBridgeDesign
    {
        public int Pk { get; set; }
        public int InvMasterPk { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string ContractNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public string Title { get; set; }
        public DateTime? DesignDate { get; set; }
        public string PreparedBy { get; set; }
        public string FillingLocation { get; set; }
        public string FillingReference { get; set; }
    }
}
