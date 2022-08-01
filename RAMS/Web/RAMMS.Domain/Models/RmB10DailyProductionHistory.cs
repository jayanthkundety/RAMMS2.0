using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmB10DailyProductionHistory
    {
        public RmB10DailyProductionHistory()
        {
            InverseB10dphB10dpPkRefNoNavigation = new HashSet<RmB10DailyProductionHistory>();
        }

        public int B10dphPkRefNo { get; set; }
        public int? B10dphB10dpPkRefNo { get; set; }
        public string B10dphFeature { get; set; }
        public string B10dphCode { get; set; }
        public string B10dphName { get; set; }
        public decimal? B10dphCond1 { get; set; }
        public decimal? B10dphCond2 { get; set; }
        public decimal? B10dphCond3 { get; set; }
        public int? B10dphUnitOfService { get; set; }
        public string B10dphRemarks { get; set; }
        public int? B10dphRevisionNo { get; set; }
        public DateTime? B10dphRevisionDate { get; set; }
        public int? B10dphUserId { get; set; }
        public string B10dphUserName { get; set; }

        public virtual RmB10DailyProductionHistory B10dphB10dpPkRefNoNavigation { get; set; }
        public virtual ICollection<RmB10DailyProductionHistory> InverseB10dphB10dpPkRefNoNavigation { get; set; }
    }
}
