using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpLaneClosure
    {
        public WpLaneClosure()
        {
            WpLaneClosureItem = new HashSet<WpLaneClosureItem>();
        }

        public int Pk { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string PermitNo { get; set; }

        public virtual WorkProgress PkNavigation { get; set; }
        public virtual ICollection<WpLaneClosureItem> WpLaneClosureItem { get; set; }
    }
}
