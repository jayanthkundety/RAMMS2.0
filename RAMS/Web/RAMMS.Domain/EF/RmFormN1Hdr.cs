using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormN1Hdr
    {
        public RmFormN1Hdr()
        {
            RmFormN2Hdr = new HashSet<RmFormN2Hdr>();
        }

        public int FnihPkRefNo { get; set; }
        public string FnihSourceType { get; set; }
        public int? FnihFqaiidPkRefNo { get; set; }
        public string FnihContNo { get; set; }
        public string FnihRefId { get; set; }
        public string FnihNcnNo { get; set; }
        public string FnihDiv { get; set; }
        public string FnihRmu { get; set; }
        public string FnihSrProvider { get; set; }
        public string FnihUseridAttnTo { get; set; }
        public string FnihUsernameAttnTo { get; set; }
        public string FnihUseridCc { get; set; }
        public string FnihUsernameCc { get; set; }
        public string FnihRoadCode { get; set; }
        public string FnihRoadName { get; set; }
        public int? FnihFrmCh { get; set; }
        public int? FnihFrmChDeci { get; set; }
        public int? FnihToCh { get; set; }
        public int? FnihToChDeci { get; set; }
        public string FnihNcDesc { get; set; }
        public DateTime? FnihDtCorrectTkn { get; set; }
        public string FnihSignIssued { get; set; }
        public int? FnihUseridIssued { get; set; }
        public string FnihUsernameIssued { get; set; }
        public string FnihDesignationIssued { get; set; }
        public string FnihSignRcvd { get; set; }
        public int? FnihUseridRcvd { get; set; }
        public string FnihUsernameRcvd { get; set; }
        public string FnihDesignationRcvd { get; set; }
        public DateTime? FnihDtRcvd { get; set; }
        public string FnihProposedRewrkSpec { get; set; }
        public string FnihSignCorrective { get; set; }
        public int? FnihUseridCorrective { get; set; }
        public string FnihUsernameCorrective { get; set; }
        public string FnihDesignationCorrective { get; set; }
        public DateTime? FnihDtCorrective { get; set; }
        public string FnihSignAccptd { get; set; }
        public int? FnihUseridAccptd { get; set; }
        public string FnihUsernameAccptd { get; set; }
        public string FnihDesignationAccptd { get; set; }
        public DateTime? FnihDtAccptd { get; set; }
        public bool? FnihCorrectionTkn { get; set; }
        public bool? FnihNcrIssue { get; set; }
        public DateTime? FnihIssueDt { get; set; }
        public string FnihOthrFllwAct { get; set; }
        public string FnihRemarks { get; set; }
        public string FnihSignVer { get; set; }
        public int? FnihUseridVer { get; set; }
        public string FnihUsernameVer { get; set; }
        public string FnihDesignationVer { get; set; }
        public DateTime? FnihDtVer { get; set; }
        public string FnihModBy { get; set; }
        public DateTime? FnihModDt { get; set; }
        public string FnihCrBy { get; set; }
        public DateTime? FnihCrDt { get; set; }
        public bool FnihSubmitSts { get; set; }
        public bool? FnihActiveYn { get; set; }
        public string FnihDesignationAttnTo { get; set; }
        public string FnihDesignationCc { get; set; }
        public DateTime? FnihCrctTknBef { get; set; }
        public DateTime? FnihDtIssue { get; set; }
        public int? FnihFsidPkRefNo { get; set; }
        public string FnihStatus { get; set; }
        public string FnihAuditLog { get; set; }

        public virtual RmFormQa2Dtl FnihFqaiidPkRefNoNavigation { get; set; }
        public virtual RmFormS1Dtl FnihFsidPkRefNoNavigation { get; set; }
        public virtual ICollection<RmFormN2Hdr> RmFormN2Hdr { get; set; }
    }
}
