using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class McsMeeActivityScorecard
    {
        public int Pk { get; set; }
        public int McsMeeActivityPk { get; set; }
        public int SectionPk { get; set; }
        public DateTime ActivityDate { get; set; }
        public string Location { get; set; }
        public string Bound { get; set; }
        public double? PlannedActivity { get; set; }
        public double? ActualActivity { get; set; }
        public double? PlannedCrew { get; set; }
        public double? ActualCrew { get; set; }
        public int ContractorPk { get; set; }
        public string Issue { get; set; }
        public string CorrectiveAction { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public DateTime? ActualEndTime { get; set; }
        public int? TeamPk { get; set; }
    }
}
