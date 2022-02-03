using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class SicMobile
    {
        public int Pk { get; set; }
        public int SicPk { get; set; }
        public string InspectionNo { get; set; }
        public string Km { get; set; }
        public string Bound { get; set; }
        public bool? Shoulder { get; set; }
        public bool? Median { get; set; }
        public DateTime? Dt { get; set; }
        public double? ActualQty { get; set; }

        public virtual Sic SicPkNavigation { get; set; }
    }
}
