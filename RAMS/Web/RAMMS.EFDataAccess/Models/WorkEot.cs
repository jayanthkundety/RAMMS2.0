using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WorkEot
    {
        public int Pk { get; set; }
        public int? WorkPk { get; set; }
        public string Name { get; set; }
        public DateTime? NewScheduledEndDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? TempId { get; set; }
    }
}
