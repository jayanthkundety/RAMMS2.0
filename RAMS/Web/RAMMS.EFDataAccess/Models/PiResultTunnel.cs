using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PiResultTunnel
    {
        public PiResultTunnel()
        {
            PiResultTunnelPanel = new HashSet<PiResultTunnelPanel>();
        }

        public int PiSchedulePk { get; set; }
        public string GenInfoTrafficControl { get; set; }

        public virtual PiSchedule PiSchedulePkNavigation { get; set; }
        public virtual ICollection<PiResultTunnelPanel> PiResultTunnelPanel { get; set; }
    }
}
