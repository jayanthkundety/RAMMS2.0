using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class McsActivityScorecardBacklogLostTime
    {
        public int Pk { get; set; }
        public int McsActivityScorecardBacklogPk { get; set; }
        public string LostTime { get; set; }
    }
}
