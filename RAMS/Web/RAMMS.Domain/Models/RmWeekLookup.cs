using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmWeekLookup
    {
        public RmWeekLookup()
        {
            RmFormS2QuarDtl = new HashSet<RmFormS2QuarDtl>();
        }

        public int ClkPkRefNo { get; set; }
        public int? ClkYear { get; set; }
        public int? ClkQuarter { get; set; }
        public int? ClkMonth { get; set; }
        public int? ClkWeekNo { get; set; }

        public virtual ICollection<RmFormS2QuarDtl> RmFormS2QuarDtl { get; set; }
    }
}
