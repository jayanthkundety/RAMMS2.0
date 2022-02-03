using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PiResultSlopeFurtherInvestigation
    {
        public int PiSchedulePk { get; set; }
        public int RecordIndex { get; set; }
        public string FiMajorDefect { get; set; }
        public string FiRecommendedAction { get; set; }

        public virtual PiResultSlope PiSchedulePkNavigation { get; set; }
    }
}
