using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class ActivityLogColumn
    {
        public int Pk { get; set; }
        public int ActivityLogTablePk { get; set; }
        public string ColumnName { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }

        public virtual ActivityLogTable ActivityLogTablePkNavigation { get; set; }
    }
}
