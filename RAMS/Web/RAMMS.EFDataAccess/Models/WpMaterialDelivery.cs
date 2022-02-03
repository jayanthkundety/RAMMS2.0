using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpMaterialDelivery
    {
        public WpMaterialDelivery()
        {
            WpMaterialDeliveryItem = new HashSet<WpMaterialDeliveryItem>();
        }

        public int Pk { get; set; }
        public string MixType { get; set; }
        public string Source { get; set; }
        public string Comments { get; set; }

        public virtual WorkProgress PkNavigation { get; set; }
        public virtual ICollection<WpMaterialDeliveryItem> WpMaterialDeliveryItem { get; set; }
    }
}
