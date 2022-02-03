using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormFdInsDtl
    {
        public int FdidPkRefNo { get; set; }
        public int? FdidFdihPkRefNo { get; set; }
        public int? FdidAiPkRefNo { get; set; }
        public string FdidAiAssetGrpCode { get; set; }
        public int? FdidAiFrmCh { get; set; }
        public string FdidAiFrmChDeci { get; set; }
        public int? FdidAiToCh { get; set; }
        public string FdidAiToChDeci { get; set; }
        public string FdidAiBound { get; set; }
        public string FdidAiGrpType { get; set; }
        public int? FdidCondition { get; set; }
        public string FdidRemarks { get; set; }
        public int? FdidModBy { get; set; }
        public DateTime? FdidModDt { get; set; }
        public int? FdidCrBy { get; set; }
        public DateTime? FdidCrDt { get; set; }
        public bool FdidSubmitSts { get; set; }
        public bool? FdidActiveYn { get; set; }
        public double? FdidLength { get; set; }
        public double? FdidWidth { get; set; }

        public virtual RmAllassetInventory FdidAiPkRefNoNavigation { get; set; }
        public virtual RmFormFdInsHdr FdidFdihPkRefNoNavigation { get; set; }
    }
}
