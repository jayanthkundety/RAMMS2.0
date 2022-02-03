using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormFcInsDtl
    {
        public int FcidPkRefNo { get; set; }
        public int? FcidFcihPkRefNo { get; set; }
        public int? FcidAiPkRefNo { get; set; }
        public string FcidAiAssetGrpCode { get; set; }
        public int? FcidAiFrmCh { get; set; }
        public string FcidAiFrmChDeci { get; set; }
        public int? FcidAiToCh { get; set; }
        public string FcidAiToChDeci { get; set; }
        public string FcidAiBound { get; set; }
        public string FcidAiGrpType { get; set; }
        public int? FcidCondition { get; set; }
        public string FcidRemarks { get; set; }
        public int? FcidModBy { get; set; }
        public DateTime? FcidModDt { get; set; }
        public int? FcidCrBy { get; set; }
        public DateTime? FcidCrDt { get; set; }
        public bool FcidSubmitSts { get; set; }
        public bool? FcidActiveYn { get; set; }
        public double? FcidLength { get; set; }
        public double? FcidWidth { get; set; }

        public virtual RmAllassetInventory FcidAiPkRefNoNavigation { get; set; }
        public virtual RmFormFcInsHdr FcidFcihPkRefNoNavigation { get; set; }
    }
}
