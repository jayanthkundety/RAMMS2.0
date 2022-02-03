using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PiResultCulvertPhoto
    {
        public int PiSchedulePk { get; set; }
        public int RecordIndex { get; set; }
        public string PhoName { get; set; }
        public string PhoPhoto { get; set; }
        public string PhoRemark { get; set; }

        public virtual PiResultCulvert PiSchedulePkNavigation { get; set; }
    }
}
