using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormS2QuarDtl
    {
        public int FsiiqdPkRefNo { get; set; }
        public int? FsiiqdFsiidPkRefNo { get; set; }
        public int? FsiiqdClkPkRefNo { get; set; }
        public int? FsiiqdCrBy { get; set; }
        public DateTime? FsiiqdCrDt { get; set; }

        public virtual RmWeekLookup FsiiqdClkPkRefNoNavigation { get; set; }
        public virtual RmFormS2Dtl FsiiqdFsiidPkRefNoNavigation { get; set; }
    }
}
