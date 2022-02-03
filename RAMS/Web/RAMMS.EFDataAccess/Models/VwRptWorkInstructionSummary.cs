using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptWorkInstructionSummary
    {
        public string MonthDate { get; set; }
        public string Section { get; set; }
        public string Region { get; set; }
        public int? TotalNew { get; set; }
        public int? TotalInProgress { get; set; }
        public int? TotalClosed { get; set; }
        public int? GrandTotal { get; set; }
        public int? TotalNod { get; set; }
    }
}
