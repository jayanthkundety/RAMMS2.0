using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormS2DayScheduleDTO
    {

        public int PkRefNo { get; set; }
        public int? FsiidPkRefNo { get; set; }
        public int? FsiiqdPkRefNo { get; set; }
        public int? FsiiqdClkPkRefNo { get; set; }
        public DateTime? ScheduledDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }

        public virtual FormS2DetailResponseDTO FsiidPkRefNoNavigation { get; set; }

    }
}
