using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpDipping
    {
        public WpDipping()
        {
            WpDippingChainage = new HashSet<WpDippingChainage>();
        }

        public int Pk { get; set; }
        public double? ChainageFrom { get; set; }
        public double? ChainageTo { get; set; }
        public string TypeOfDepth { get; set; }
        public string Comments { get; set; }

        public virtual WorkProgress PkNavigation { get; set; }
        public virtual ICollection<WpDippingChainage> WpDippingChainage { get; set; }
    }
}
