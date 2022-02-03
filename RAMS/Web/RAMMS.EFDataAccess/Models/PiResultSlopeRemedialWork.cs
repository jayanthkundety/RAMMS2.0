using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PiResultSlopeRemedialWork
    {
        public int PiSchedulePk { get; set; }
        public int RecordIndex { get; set; }
        public string RwMajorDefect { get; set; }
        public string RwRecommendedAction { get; set; }

        public virtual PiResultSlope PiSchedulePkNavigation { get; set; }
    }
}
