using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptMasterInspectionProgramDrainage
    {
        public int? WorkProgramYear { get; set; }
        public string SectionNetwork { get; set; }
        public string SectionName { get; set; }
        public int? Frequency { get; set; }
        public string ReportCycle { get; set; }
        public DateTime? TargetWorkOrderSubmissionDate { get; set; }
        public DateTime? ScheduleStartDate { get; set; }
        public DateTime? ScheduleEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public string Remarks { get; set; }
    }
}
