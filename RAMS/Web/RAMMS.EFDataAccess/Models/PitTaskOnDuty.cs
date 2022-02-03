using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PitTaskOnDuty
    {
        public int PitTaskPk { get; set; }
        public int RecordIndex { get; set; }
        public string OnDutyStaffName { get; set; }

        public virtual PitTask PitTaskPkNavigation { get; set; }
    }
}
