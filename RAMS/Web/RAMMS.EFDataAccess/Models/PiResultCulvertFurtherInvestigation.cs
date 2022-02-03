using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PiResultCulvertFurtherInvestigation
    {
        public int PiSchedulePk { get; set; }
        public int RecordIndex { get; set; }
        public string FiMajorDefect { get; set; }
        public string FiRecommendedAction { get; set; }

        public virtual PiResultCulvert PiSchedulePkNavigation { get; set; }
    }
}
