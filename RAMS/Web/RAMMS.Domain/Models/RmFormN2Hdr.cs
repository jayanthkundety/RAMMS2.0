using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormN2Hdr
    {
        public int FnthPkRefNo { get; set; }
        public string FnthSourceType { get; set; }
        public string FnthContNo { get; set; }
        public string FnthRefId { get; set; }
        public string FnthNcrNo { get; set; }
        public DateTime? FnthIssueDt { get; set; }
        public string FnthRegion { get; set; }
        public string FnthDiv { get; set; }
        public string FnthRmu { get; set; }
        public string FnthSrProvider { get; set; }
        public string FnthAttnTo { get; set; }
        public string FnthCc { get; set; }
        public string FnthSubject { get; set; }
        public string FnthNonConfDtl { get; set; }
        public string FnthSignIssued { get; set; }
        public int? FnthUseridIssued { get; set; }
        public string FnthUsernameIssued { get; set; }
        public string FnthDesignationIssued { get; set; }
        public string FnthSignRcvd { get; set; }
        public int? FnthUseridRcvd { get; set; }
        public string FnthUsernameRcvd { get; set; }
        public string FnthDesignationRcvd { get; set; }
        public DateTime? FnthDtRcvd { get; set; }
        public string FnthProposedCrctAct { get; set; }
        public string FnthSignCorrective { get; set; }
        public int? FnthUseridCorrective { get; set; }
        public string FnthUsernameCorrective { get; set; }
        public string FnthDesignationCorrective { get; set; }
        public DateTime? FnthDtCorrective { get; set; }
        public string FnthSignAccptd { get; set; }
        public int? FnthUseridAccptd { get; set; }
        public string FnthUsernameAccptd { get; set; }
        public string FnthDesignationAccptd { get; set; }
        public DateTime? FnthDtAccptd { get; set; }
        public string FnthPreventiveAct { get; set; }
        public string FnthSignPreventive { get; set; }
        public int? FnthUseridPreventive { get; set; }
        public string FnthUsernamePreventive { get; set; }
        public string FnthDesignationPreventive { get; set; }
        public DateTime? FnthDtPreventive { get; set; }
        public string FnohOthrFllwAct { get; set; }
        public DateTime? FnthCloseOutDt { get; set; }
        public string FnthCloseOutRemarks { get; set; }
        public string FnthSignVer { get; set; }
        public int? FnthUseridVer { get; set; }
        public string FnthUsernameVer { get; set; }
        public string FnthDesignationVer { get; set; }
        public DateTime? FnthDtVer { get; set; }
        public string FnihModBy { get; set; }
        public DateTime? FnihModDt { get; set; }
        public string FnihCrBy { get; set; }
        public DateTime? FnihCrDt { get; set; }
        public bool FnihSubmitSts { get; set; }
        public bool? FnihActiveYn { get; set; }
        public int? FnthFnihPkRefNo { get; set; }
        public string FnthSignPrvAccptd { get; set; }
        public int? FnthUseridPrvAccptd { get; set; }
        public string FnthUsernamePrvAccptd { get; set; }
        public string FnthDesignationPrvAccptd { get; set; }
        public DateTime? FnthDtPrvAccptd { get; set; }
        public string FnthUsernameAttnTo { get; set; }
        public string FnthUsernameCc { get; set; }
        public string FnihDesignationAttnTo { get; set; }
        public string FnihDesignationCc { get; set; }
        public string FnthStatus { get; set; }
        public string FnthAuditLog { get; set; }

        public virtual RmFormN1Hdr FnthFnihPkRefNoNavigation { get; set; }
    }
}
