using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class McsMeeActivityScorecardDetail
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
        public string McsMeeActivityName { get; set; }
        public string McsMeeActivityCode { get; set; }
        public string SectionName { get; set; }
        public string SectionCode { get; set; }
        public string ContractorCode { get; set; }
        public string ContractorCompanyName { get; set; }
        public string TeamCode { get; set; }
        public string TeamName { get; set; }
        public DateTime? McsCalendarWeekJanStartDate { get; set; }
        public string LostTime { get; set; }
    }
}
