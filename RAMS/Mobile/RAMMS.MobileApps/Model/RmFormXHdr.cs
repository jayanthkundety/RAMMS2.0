using System;
using System.Collections.Generic;

namespace RAMMS.MobileApps
{
    public partial class RmFormXHdr
    {
        public RmFormXHdr()
        {
            RmAccUcuImageDtl = new HashSet<RmAccUcuImageDtl>();
            RmWarImageDtl = new HashSet<RmWarImageDtl>();
        }

        public int FxhPkRefNo { get; set; }
        public int? FxhFddPkRefNo { get; set; }
        public int? FxhContNo { get; set; }
        public DateTime? FxhDate { get; set; }
        public TimeSpan? FxhTime { get; set; }
        public string FxhName { get; set; }
        public int? FxhHandPh { get; set; }
        public int? FxhWorkPh { get; set; }
        public string FxhEmailId { get; set; }
        public string FxhLoc { get; set; }
        public string FxhDesc { get; set; }
        public string FxhRmu { get; set; }
        public string FxhAttnTo { get; set; }
        public string FxhComments { get; set; }
        public string FxhAssgnWrk { get; set; }
        public int? FxhActCode { get; set; }
        public string FxhActName { get; set; }
        public string FxhEstDays { get; set; }
        public DateTime? FxhWrkSc { get; set; }
        public DateTime? FxhWrkCmpld { get; set; }
        public DateTime? FxhClsd { get; set; }
        public string FxhSignPrp { get; set; }
        public int? FxhUseridPrp { get; set; }
        public string FxhUsernamePrp { get; set; }
        public string FxhDesignationPrp { get; set; }
        public DateTime? FxhDtPrp { get; set; }
        public string FxhSignVer { get; set; }
        public int? FxhUseridVer { get; set; }
        public string FxhUsernameVer { get; set; }
        public string FxhDesignationVer { get; set; }
        public DateTime? FxhDtVer { get; set; }
        public string FxhSignVet { get; set; }
        public int? FxhUseridVet { get; set; }
        public string FxhUsernameVet { get; set; }
        public string FxhDesignationVet { get; set; }
        public DateTime? FxhDtVet { get; set; }
        public string FxhRemarks { get; set; }
        public string FxhModBy { get; set; }
        public DateTime? FxhModDt { get; set; }
        public string FxhCrBy { get; set; }
        public DateTime? FxhCrDt { get; set; }
        public bool FxhSubmitSts { get; set; }
        public bool? FxhActiveYn { get; set; }

        public virtual RmFormDDtl FxhFddPkRefNoNavigation { get; set; }
        public virtual ICollection<RmAccUcuImageDtl> RmAccUcuImageDtl { get; set; }
        public virtual ICollection<RmWarImageDtl> RmWarImageDtl { get; set; }
    }
}
