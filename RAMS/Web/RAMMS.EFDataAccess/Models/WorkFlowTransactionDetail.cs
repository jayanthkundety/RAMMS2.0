using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WorkFlowTransactionDetail
    {
        public int Pk { get; set; }
        public int WorkFlowMasterPk { get; set; }
        public int Activity { get; set; }
        public int UserPk { get; set; }
        public bool Answer { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int NodPk { get; set; }
        public string NodId { get; set; }
        public int? WorkFlowAssetGroup { get; set; }
        public string UserName { get; set; }
    }
}
