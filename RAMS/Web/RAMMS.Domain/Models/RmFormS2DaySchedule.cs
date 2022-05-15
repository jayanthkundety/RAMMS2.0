using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormS2DaySchedule
    {
        public int FsiidsPkRefNo { get; set; }
        public int? FsiidsFsiiqdPkRefNo { get; set; }
        public DateTime? FsiidsScheduledDt { get; set; }
        public int? FsiidsCrBy { get; set; }
        public DateTime? FsiidsCrDt { get; set; }
    }
}
