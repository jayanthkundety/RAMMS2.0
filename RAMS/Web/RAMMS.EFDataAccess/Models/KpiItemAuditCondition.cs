using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class KpiItemAuditCondition
    {
        public int KpiItemPk { get; set; }
        public string RoadClass { get; set; }
        public string Condition { get; set; }

        public virtual KpiItem KpiItemPkNavigation { get; set; }
    }
}
