using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptSicDailyProgramMobile
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string TypeOfArea { get; set; }
        public string CompanyName { get; set; }
        public double? Kmfrom { get; set; }
        public double? Kmto { get; set; }
        public string Location { get; set; }
        public string Interchange { get; set; }
        public string Bound { get; set; }
        public double? PlannedQuantity { get; set; }
        public double? PlannedCrew { get; set; }
        public DateTime ActivityDate { get; set; }
        public int Pk { get; set; }
    }
}
