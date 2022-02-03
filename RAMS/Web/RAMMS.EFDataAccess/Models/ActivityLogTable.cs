using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class ActivityLogTable
    {
        public ActivityLogTable()
        {
            ActivityLogColumn = new HashSet<ActivityLogColumn>();
        }

        public int Pk { get; set; }
        public int ActivityLogActionPk { get; set; }
        public string Command { get; set; }
        public string TableName { get; set; }
        public string BulkOperation { get; set; }

        public virtual ActivityLogAction ActivityLogActionPkNavigation { get; set; }
        public virtual ICollection<ActivityLogColumn> ActivityLogColumn { get; set; }
    }
}
