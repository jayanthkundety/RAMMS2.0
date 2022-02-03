using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptPavementAnnualWorkProgramAwp
    {
        public int? Year { get; set; }
        public string Section { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string Treatment { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? Cost { get; set; }
    }
}
