using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class ActivityLogAction
    {
        public ActivityLogAction()
        {
            ActivityLogTable = new HashSet<ActivityLogTable>();
        }

        public int Pk { get; set; }
        public string UserName { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public string Ipaddress { get; set; }

        public virtual ICollection<ActivityLogTable> ActivityLogTable { get; set; }
    }
}
