using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpPermitToWorkPtw
    {
        public int WpPermitToWorkPk { get; set; }
        public int RecordIndex { get; set; }
        public DateTime? PtwTimeIn { get; set; }
        public string PtwName { get; set; }
        public string PtwPosition { get; set; }

        public virtual WpPermitToWork WpPermitToWorkPkNavigation { get; set; }
    }
}
