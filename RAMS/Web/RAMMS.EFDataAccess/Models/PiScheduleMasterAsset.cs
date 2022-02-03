using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PiScheduleMasterAsset
    {
        public int Pk { get; set; }
        public int InvMasterPk { get; set; }
        public int PiScheduleMasterPk { get; set; }

        public virtual InvMaster InvMasterPkNavigation { get; set; }
        public virtual PiScheduleMaster PiScheduleMasterPkNavigation { get; set; }
    }
}
