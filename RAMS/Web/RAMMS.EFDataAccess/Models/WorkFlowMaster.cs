using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WorkFlowMaster
    {
        public int Pk { get; set; }
        public int? NodPk { get; set; }
        public int? WorkPk { get; set; }
        public int? WorkFlowAssetGroup { get; set; }
        public int? CurrentActivity { get; set; }
        public DateTime? CurrentActivityNotificationTime { get; set; }
        public int? CurrentLevel { get; set; }
        public bool? IsCompleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
