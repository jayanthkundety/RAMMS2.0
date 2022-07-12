using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormTDailyInspection
    {
        public RmFormTDailyInspection()
        {
            RmFormTVechicle = new HashSet<RmFormTVechicle>();
        }

        public int FmtdiPkRefNo { get; set; }
        public int? FmtdiFmtPkRefNo { get; set; }
        public DateTime? FmtdiInspectionDate { get; set; }
        public string FmtdiAuditTimeFrm { get; set; }
        public string FmtdiAuditTimeTo { get; set; }
        public string FmtdiDirectionFrm { get; set; }
        public string FmtdiDirectionTo { get; set; }
        public int? FmtdiHourlyCountPerDay { get; set; }
        public int? FmtdiPcsSubTotal { get; set; }
        public int? FmtdiHvSubTotal { get; set; }
        public int? FmtdiMcSubTotal { get; set; }
        public string FmtdiDescription { get; set; }
        public int? FmtdiModBy { get; set; }
        public DateTime? FmtdiModDt { get; set; }
        public int? FmtdiCrBy { get; set; }
        public DateTime? FmtdiCrDt { get; set; }

        public virtual RmFormTHdr FmtdiFmtPkRefNoNavigation { get; set; }
        public virtual ICollection<RmFormTVechicle> RmFormTVechicle { get; set; }
    }
}
