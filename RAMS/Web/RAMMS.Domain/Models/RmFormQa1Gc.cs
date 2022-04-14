using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormQa1Gc
    {
        public int Fqa1gcPkRefNo { get; set; }
        public int? Fqa1gcFqa1hPkRefNo { get; set; }
        public string Fqa1gcQualityRating { get; set; }
        public string Fqa1gcQualityRemarks { get; set; }
        public string Fqa1gcUsRatingReason { get; set; }
        public int? Fqa1gcModBy { get; set; }
        public DateTime? Fqa1gcModDt { get; set; }
        public int? Fqa1gcCrBy { get; set; }
        public DateTime? Fqa1gcCrDt { get; set; }

        public virtual RmFormQa1Hdr Fqa1gcFqa1hPkRefNoNavigation { get; set; }
    }
}
