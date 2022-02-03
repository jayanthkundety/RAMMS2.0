using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptSicDailyProgramGeneralRoadCleaning
    {
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string ActivityName { get; set; }
        public double? Kmfrom { get; set; }
        public double? Kmto { get; set; }
        public string Bound { get; set; }
        public string PlanLocation { get; set; }
        public double? PlanCrewNo { get; set; }
        public DateTime ActivityDate { get; set; }
        public int Pk { get; set; }
        public int McsActivityScorecardPk { get; set; }
    }
}
