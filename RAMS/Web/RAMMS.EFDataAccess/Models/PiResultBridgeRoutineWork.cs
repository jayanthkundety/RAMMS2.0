using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PiResultBridgeRoutineWork
    {
        public int PiSchedulePk { get; set; }
        public int RecordIndex { get; set; }
        public string RmMajorDefect { get; set; }
        public string RmRecommendedAction { get; set; }

        public virtual PiResultBridge PiSchedulePkNavigation { get; set; }
    }
}
