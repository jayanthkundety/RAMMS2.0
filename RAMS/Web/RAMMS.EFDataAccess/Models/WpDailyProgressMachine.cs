using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpDailyProgressMachine
    {
        public int RecordIndex { get; set; }
        public int WpDailyProgressPk { get; set; }
        public string MachineSubject { get; set; }
        public string MachineQuantity { get; set; }

        public virtual WpDailyProgress WpDailyProgressPkNavigation { get; set; }
    }
}
