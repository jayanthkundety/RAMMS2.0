using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpLaneClosureItem
    {
        public int WpLaneClosurePk { get; set; }
        public int RecordIndex { get; set; }
        public double? Kmstart { get; set; }
        public double? Kmend { get; set; }
        public string Direction { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public string Purpose { get; set; }

        public virtual WpLaneClosure WpLaneClosurePkNavigation { get; set; }
    }
}
