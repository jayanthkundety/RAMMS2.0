using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PiResultBridgePhoto
    {
        public int PiSchedulePk { get; set; }
        public int RecordIndex { get; set; }
        public string PhoName { get; set; }
        public string PhoPhoto { get; set; }
        public string PhoRemark { get; set; }

        public virtual PiResultBridge PiSchedulePkNavigation { get; set; }
    }
}
