using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PiResultBridgeFurtherInvestigation
    {
        public int PiSchedulePk { get; set; }
        public int RecordIndex { get; set; }
        public string FiMajorDefect { get; set; }
        public string FiRecommendedAction { get; set; }

        public virtual PiResultBridge PiSchedulePkNavigation { get; set; }
    }
}
