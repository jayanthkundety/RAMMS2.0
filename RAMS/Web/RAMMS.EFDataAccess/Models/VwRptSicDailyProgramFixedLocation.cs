using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptSicDailyProgramFixedLocation
    {
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public double? PlannedCrew { get; set; }
        public double? PlannedQuantity { get; set; }
        public DateTime ActivityDate { get; set; }
        public int Pk { get; set; }
        public string Shift { get; set; }
        public int McsCategoryPk { get; set; }
        public int McsActivityScorecardPk { get; set; }
    }
}
