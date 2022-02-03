using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WorkFlowNotification
    {
        public int Pk { get; set; }
        public int WorkFlowMasterPk { get; set; }
        public int Activity { get; set; }
        public int UserPk { get; set; }
        public DateTime DueDate { get; set; }
        public int Level { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
