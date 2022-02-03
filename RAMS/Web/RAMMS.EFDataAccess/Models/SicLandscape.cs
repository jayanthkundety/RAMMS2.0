using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class SicLandscape
    {
        public int Pk { get; set; }
        public int SicPk { get; set; }
        public string ActivityName { get; set; }
        public DateTime? ActualTimeFrom { get; set; }
        public DateTime? ActualTimeTo { get; set; }
        public string ActualLocation { get; set; }
        public double? ActualCrewNo { get; set; }
        public string Remarks { get; set; }
        public string PlanLocation { get; set; }
        public double? PlanCrewNo { get; set; }

        public virtual Sic SicPkNavigation { get; set; }
    }
}
