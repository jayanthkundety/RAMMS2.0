using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpInspection
    {
        public WpInspection()
        {
            WpInspectionDefect = new HashSet<WpInspectionDefect>();
        }

        public int Pk { get; set; }
        public string Dummy { get; set; }

        public virtual WorkProgress PkNavigation { get; set; }
        public virtual ICollection<WpInspectionDefect> WpInspectionDefect { get; set; }
    }
}
