using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
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
        public string FxhContNo { get; set; }
        public DateTime? FxhDate { get; set; }
        public TimeSpan? FxhTime { get; set; }
        public string FxhName { get; set; }
        public string FxhHandPh { get; set; }
        public string FxhWorkPh { get; set; }
        public string FxhEmailId { get; set; }
        public string FxhLoc { get; set; }
        public string FxhDesc { get; set; }
        public string FxhRmu { get; set; }
        public string FxhUsernameAttnTo { get; set; }
        public string FxhComments { get; set; }
        public string FxhAssgnWrk { get; set; }
        public int? FxhActMainCode { get; set; }
        public string FxhActMainName { get; set; }
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
        public string FxhModComType { get; set; }
        public string FxhModComDesc { get; set; }
        public string FxhModComUpload { get; set; }
        public string FxhRoadCode { get; set; }
        public string FxhActSubCode { get; set; }
        public string FxhActSubName { get; set; }
        public DateTime? FxhEstDate { get; set; }
        public int? FxhUseridAssgn { get; set; }
        public string FxhUsernameAssgn { get; set; }
        public DateTime? FxhDtAssgn { get; set; }
        public DateTime? FxhDtJkrSent { get; set; }
        public DateTime? FxhDtJkrRcvdFrm { get; set; }
        public string FxhJkrRemarks { get; set; }
        public string FxhStsJkr { get; set; }
        public string FxhSection { get; set; }
        public string FxhRefId { get; set; }
        public string FxhSignSchdVer { get; set; }
        public int? FxhUseridSchdVer { get; set; }
        public string FxhUsernameSchdVer { get; set; }
        public string FxhDesignationSchdVer { get; set; }
        public DateTime? FxhDtSchdVer { get; set; }
        public string FxhRoadName { get; set; }
        public int? FxhUseridAttnTo { get; set; }
        public string FxhLocReportedDesc { get; set; }

        public virtual RmFormDDtl FxhFddPkRefNoNavigation { get; set; }
        public virtual ICollection<RmAccUcuImageDtl> RmAccUcuImageDtl { get; set; }
        public virtual ICollection<RmWarImageDtl> RmWarImageDtl { get; set; }
    }
}
