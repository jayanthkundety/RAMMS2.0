using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpWorkPermit
    {
        public WpWorkPermit()
        {
            WpWorkPermitForm = new HashSet<WpWorkPermitForm>();
        }

        public int Pk { get; set; }
        public string PermitNo { get; set; }

        public virtual WorkProgress PkNavigation { get; set; }
        public virtual ICollection<WpWorkPermitForm> WpWorkPermitForm { get; set; }
    }
}
