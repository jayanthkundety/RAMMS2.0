using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpWorkPermitForm
    {
        public int WpWorkPermitPk { get; set; }
        public int RecordIndex { get; set; }
        public string FileName { get; set; }

        public virtual WpWorkPermit WpWorkPermitPkNavigation { get; set; }
    }
}
