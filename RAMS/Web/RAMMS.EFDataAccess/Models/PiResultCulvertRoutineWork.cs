using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PiResultCulvertRoutineWork
    {
        public int PiSchedulePk { get; set; }
        public int RecordIndex { get; set; }
        public string RmMajorDefect { get; set; }
        public string RmRecommendedAction { get; set; }

        public virtual PiResultCulvert PiSchedulePkNavigation { get; set; }
    }
}
