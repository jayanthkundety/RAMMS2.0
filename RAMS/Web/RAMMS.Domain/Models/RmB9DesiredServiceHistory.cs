using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmB9DesiredServiceHistory
    {
        public int B9dshPkRefNo { get; set; }
        public int? B9dshB9dsPkRefNo { get; set; }
        public string B9dshFeature { get; set; }
        public string B9dshCode { get; set; }
        public string B9dshName { get; set; }
        public decimal? B9dshCond1 { get; set; }
        public decimal? B9dshCond2 { get; set; }
        public decimal? B9dshCond3 { get; set; }
        public int? B9dshUnitOfService { get; set; }
        public string B9dshRemarks { get; set; }
        public int? B9dshRevisionNo { get; set; }
        public DateTime? B9dshRevisionDate { get; set; }
        public int? B9dshUserId { get; set; }
        public string B9dshUserName { get; set; }

        public virtual RmB9DesiredService B9dshB9dsPkRefNoNavigation { get; set; }
    }
}
