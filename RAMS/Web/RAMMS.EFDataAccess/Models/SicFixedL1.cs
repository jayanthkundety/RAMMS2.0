using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class SicFixedL1
    {
        public SicFixedL1()
        {
            SicFixedL2 = new HashSet<SicFixedL2>();
        }

        public int Pk { get; set; }
        public int SicPk { get; set; }
        public string ActivityName { get; set; }
        public DateTime? InspTime { get; set; }
        public string InspOnGoingShift { get; set; }
        public string InspOnGoingActivities { get; set; }
        public string InspRemarks { get; set; }
        public string EodCompleted { get; set; }
        public string EodIncomplete { get; set; }
        public string EodManpower { get; set; }
        public string EodRemarks { get; set; }

        public virtual Sic SicPkNavigation { get; set; }
        public virtual ICollection<SicFixedL2> SicFixedL2 { get; set; }
    }
}
