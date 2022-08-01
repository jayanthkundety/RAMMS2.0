using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmB9DesiredService
    {
        public RmB9DesiredService()
        {
            RmB9DesiredServiceHistory = new HashSet<RmB9DesiredServiceHistory>();
        }

        public int B9dsPkRefNo { get; set; }
        public int? B9dsRevisionNo { get; set; }
        public DateTime? B9dsRevisionDate { get; set; }
        public int? B9dsRevisionYear { get; set; }
        public int? B9dsCrBy { get; set; }
        public DateTime? B9dsCrDt { get; set; }

        public virtual ICollection<RmB9DesiredServiceHistory> RmB9DesiredServiceHistory { get; set; }
    }
}
