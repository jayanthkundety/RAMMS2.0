using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpContractorReview
    {
        public int Pk { get; set; }
        public string Teamwork { get; set; }
        public string Timeliness { get; set; }
        public string Quality { get; set; }
        public string Planning { get; set; }
        public string Knowledge { get; set; }
        public string Professionalism { get; set; }
        public string Analytical { get; set; }
        public string Efficiency { get; set; }
        public string Resources { get; set; }
        public string Compliance { get; set; }
        public double? TotalScore { get; set; }
        public string Overall { get; set; }
        public string SupervisorRem { get; set; }
        public string RmanagerRem { get; set; }
        public string FutureProj { get; set; }

        public virtual WorkProgress PkNavigation { get; set; }
    }
}
