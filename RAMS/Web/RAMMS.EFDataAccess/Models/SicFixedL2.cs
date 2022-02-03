using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class SicFixedL2
    {
        public int Pk { get; set; }
        public int SicFixedL1pk { get; set; }
        public string L2activityName { get; set; }
        public string L3activityName { get; set; }
        public bool? ActualCompleted { get; set; }
        public int AssetQty { get; set; }

        public virtual SicFixedL1 SicFixedL1pkNavigation { get; set; }
    }
}
