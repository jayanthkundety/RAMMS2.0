using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpDailyProgressMaterial
    {
        public int WpDailyProgressPk { get; set; }
        public int RecordIndex { get; set; }
        public string MaterialSubject { get; set; }
        public string MaterialQuantity { get; set; }

        public virtual WpDailyProgress WpDailyProgressPkNavigation { get; set; }
    }
}
