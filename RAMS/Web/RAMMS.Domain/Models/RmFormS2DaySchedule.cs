using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormS2DaySchedule
    {
        public int FsiidsPkRefNo { get; set; }
        public int? FsiidsFsiidPkRefNo { get; set; }
        public int? FsiidsFsiiqdPkRefNo { get; set; }
        public int? FsiidsFsiiqdClkPkRefNo { get; set; }
        public DateTime? FsiidsScheduledDt { get; set; }
        public int? FsiidsCrBy { get; set; }
        public DateTime? FsiidsCrDt { get; set; }

        public virtual RmFormS2Dtl FsiidsFsiidPkRefNoNavigation { get; set; }
        public virtual RmFormS2QuarDtl FsiidsFsiiqdPkRefNoNavigation { get; set; }
    }
}
