using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpVehicle
    {
        public WpVehicle()
        {
            WpVehicleItem = new HashSet<WpVehicleItem>();
        }

        public int Pk { get; set; }
        public string InCharge { get; set; }
        public string Designation { get; set; }
        public string Company { get; set; }
        public string ContactNo { get; set; }
        public string PlusPermitNo { get; set; }
        public string RefNo { get; set; }

        public virtual WorkProgress PkNavigation { get; set; }
        public virtual ICollection<WpVehicleItem> WpVehicleItem { get; set; }
    }
}
