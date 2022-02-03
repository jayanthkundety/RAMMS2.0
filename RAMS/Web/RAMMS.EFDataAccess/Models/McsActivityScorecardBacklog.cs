using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class McsActivityScorecardBacklog
    {
        public int Pk { get; set; }
        public int McsActivityBacklogPk { get; set; }
        public int SectionPk { get; set; }
        public string Location { get; set; }
        public string Interchange { get; set; }
        public double? Kmfrom { get; set; }
        public double? Kmto { get; set; }
        public string Bound { get; set; }
        public double? PlannedQuantity { get; set; }
        public double? ActualQuantity { get; set; }
        public double? PlannedCrew { get; set; }
        public double? ActualCrew { get; set; }
        public int ContractorPk { get; set; }
        public double? AvailableManhourPerManPerDay { get; set; }
        public string Issue { get; set; }
        public string CorrectiveAction { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ScheduleStartDate { get; set; }
        public DateTime? ScheduleEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
    }
}
