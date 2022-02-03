using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpToolboxTalk
    {
        public WpToolboxTalk()
        {
            WpToolboxTalkAttendance = new HashSet<WpToolboxTalkAttendance>();
        }

        public int Pk { get; set; }
        public DateTime? Time { get; set; }
        public string Location { get; set; }

        public virtual WorkProgress PkNavigation { get; set; }
        public virtual ICollection<WpToolboxTalkAttendance> WpToolboxTalkAttendance { get; set; }
    }
}
