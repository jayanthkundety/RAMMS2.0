using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptSummaryOfTollPlazaByNetwork
    {
        public string Network { get; set; }
        public int? Open { get; set; }
        public int? Closed { get; set; }
    }
}
