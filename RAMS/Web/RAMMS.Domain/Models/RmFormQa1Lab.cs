using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormQa1Lab
    {
        public int Fqa1lPkRefNo { get; set; }
        public int? Fqa1lFqa1hPkRefNo { get; set; }
        public string Fqa1lLabour { get; set; }
        public int? Fqa1lLabourOnSite { get; set; }
        public int? Fqa1lLabourOnLeave { get; set; }
        public bool? Fqa1lLabourPerformanceStd { get; set; }
        public string Fqa1lLabourRemark { get; set; }
        public int? Fqa1lModBy { get; set; }
        public DateTime? Fqa1lModDt { get; set; }
        public int? Fqa1lCrBy { get; set; }
        public DateTime? Fqa1lCrDt { get; set; }

        public virtual RmFormQa1Hdr Fqa1lFqa1hPkRefNoNavigation { get; set; }
    }
}
