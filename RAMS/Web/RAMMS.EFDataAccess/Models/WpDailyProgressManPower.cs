using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpDailyProgressManPower
    {
        public int WpDailyProgressPk { get; set; }
        public int RecordIndex { get; set; }
        public string ManPowerSubject { get; set; }
        public string ManPowerQuantity { get; set; }

        public virtual WpDailyProgress WpDailyProgressPkNavigation { get; set; }
    }
}
