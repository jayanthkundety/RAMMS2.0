using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvTunnelJoint
    {
        public int Pk { get; set; }
        public string AssetName { get; set; }
        public string JointType { get; set; }
        public string FinishTreatment { get; set; }
    }
}
