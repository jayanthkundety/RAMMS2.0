using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class McsActivityScorecardLostTime
    {
        public int Pk { get; set; }
        public int McsActivityScorecardPk { get; set; }
        public string LostTime { get; set; }
    }
}
