using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PiResultBridgeRemedialWork
    {
        public int PiSchedulePk { get; set; }
        public int RecordIndex { get; set; }
        public string RwMajorDefect { get; set; }
        public string RwRecommendedAction { get; set; }

        public virtual PiResultBridge PiSchedulePkNavigation { get; set; }
    }
}
