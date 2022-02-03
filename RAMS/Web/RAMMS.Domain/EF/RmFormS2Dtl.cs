using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormS2Dtl
    {
        public RmFormS2Dtl()
        {
            RmFormS2QuarDtl = new HashSet<RmFormS2QuarDtl>();
        }

        public int FsiidPkRefNo { get; set; }
        public int? FsiidFsiihPkRefNo { get; set; }
        public string FsiidRefId { get; set; }
        public int? FsiidRoadId { get; set; }
        public string FsiidRdLocSeq { get; set; }
        public int? FsiidCil { get; set; }
        public int? FsiidPriorityI { get; set; }
        public int? FsiidPriorityIi { get; set; }
        public int? FsiidAdp { get; set; }
        public int? FsiidCrwDaysReq { get; set; }
        public int? FsiidCrwAllwcdQuar { get; set; }
        public decimal? FsiidTargetPercent { get; set; }
        public string FsiidRemarks { get; set; }
        public int? FsiidModBy { get; set; }
        public DateTime? FsiidModDt { get; set; }
        public int? FsiidCrBy { get; set; }
        public DateTime? FsiidCrDt { get; set; }
        public bool FsiidSubmitSts { get; set; }
        public bool? FsiidActiveYn { get; set; }
        public int? FsiidPriority { get; set; }
        public string FsiidRoadCode { get; set; }
        public string FsiidRoadName { get; set; }
        public decimal? FsiidRoadPavedLength { get; set; }
        public decimal? FsiidRoadUnPavedLength { get; set; }
        public string FsiidWorkQty { get; set; }

        public virtual RmFormS2Hdr FsiidFsiihPkRefNoNavigation { get; set; }
        public virtual ICollection<RmFormS2QuarDtl> RmFormS2QuarDtl { get; set; }
    }
}
