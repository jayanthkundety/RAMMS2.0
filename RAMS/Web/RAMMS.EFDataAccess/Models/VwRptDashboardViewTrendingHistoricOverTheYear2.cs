using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptDashboardViewTrendingHistoricOverTheYear2
    {
        public int? Year { get; set; }
        public int? Frequency { get; set; }
        public int Value { get; set; }
        public string Network { get; set; }
    }
}
