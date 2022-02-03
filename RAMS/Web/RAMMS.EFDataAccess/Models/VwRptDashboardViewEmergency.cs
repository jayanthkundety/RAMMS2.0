using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptDashboardViewEmergency
    {
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string Status { get; set; }
        public string SectionCode { get; set; }
        public int? Year { get; set; }
    }
}
