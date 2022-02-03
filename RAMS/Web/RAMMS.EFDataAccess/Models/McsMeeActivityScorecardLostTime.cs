using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class McsMeeActivityScorecardLostTime
    {
        public int Pk { get; set; }
        public int McsMeeActivityScorecardPk { get; set; }
        public string LostTime { get; set; }
    }
}
