using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class RptBiNod
    {
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string Status { get; set; }
        public string SectionCode { get; set; }
        public DateTime? ReportDate { get; set; }
    }
}
