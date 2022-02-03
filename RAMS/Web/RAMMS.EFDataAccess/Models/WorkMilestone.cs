using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WorkMilestone
    {
        public int Pk { get; set; }
        public int? WorkPk { get; set; }
        public string Description { get; set; }
        public DateTime? PlannedDate { get; set; }
        public DateTime? ActualDate { get; set; }
        public string Remarks { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? TempId { get; set; }

        public virtual Work WorkPkNavigation { get; set; }
    }
}
