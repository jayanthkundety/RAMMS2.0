using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormTVechicle
    {
        public int FmtvPkRefNo { get; set; }
        public int? FmtvFmtdiPkRefNo { get; set; }
        public string FmtvVechicleType { get; set; }
        public string FmtvAxle { get; set; }
        public string FmtvLoading { get; set; }
        public string FmtvTime { get; set; }
        public int? FmtvCount { get; set; }

        public virtual RmFormTDailyInspection FmtvFmtdiPkRefNoNavigation { get; set; }
    }
}
