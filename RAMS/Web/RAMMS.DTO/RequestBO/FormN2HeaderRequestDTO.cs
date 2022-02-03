using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;

namespace RAMMS.DTO.RequestBO
{
    public class FormN2HeaderRequestDTO
    {
        public FormN2HeaderRequestDTO()
        {
         
        }
        [MapTo("FnthPkRefNo")]
        public int No { get; set; }
        [MapTo("FnthSourceType")]
        public string SourceType { get; set; }
        [MapTo("FnthContNo")]
        public string ContNo { get; set; }
        [MapTo("FnthRefId")]
        public string ReferenceID { get; set; }
        [MapTo("FnthNcrNo")]
        public string NcrNo { get; set; }
        [MapTo("FnthIssueDt")]
        public DateTime? IssuedDate { get; set; }
        [MapTo("FnthRegion")]
        public string Region { get; set; }
        [MapTo("FnthDiv")]
        public string Division { get; set; }
        [MapTo("FnthRmu")]
        public string RMU { get; set; }
        [MapTo("FnthSrProvider")]
        public string ServiceProvider { get; set; }
        [MapTo("FnthAttnTo")]
        public string AttentionTo { get; set; }
        [MapTo("FnthCc")]
        public string Cc { get; set; }
        [MapTo("FnthSubject")]
        public string Subject { get; set; }
        [MapTo("FnthNonConfDtl")]
        public string NonConfDetail { get; set; }
        [MapTo("FnthSignIssued")]
        public string IssuedBySignature { get; set; }
        [MapTo("FnthUseridIssued")]
        public int? IssuedByUserId { get; set; }
        [MapTo("FnthUsernameIssued")]
        public string IssuedByUsername { get; set; }
        [MapTo("FnthDesignationIssued")]
        public string IssuedByDesignation { get; set; }
        [MapTo("FnthSignRcvd")]
        public string RcvdBySign { get; set; }
        [MapTo("FnthUseridRcvd")]
        public int? RcvdByUserid { get; set; }
        [MapTo("FnthUsernameRcvd")]
        public string RcvdByUsername { get; set; }
        [MapTo("FnthDesignationRcvd")]
        public string RcvdByDesignation { get; set; }
        [MapTo("FnthDtRcvd")]
        public DateTime? RcvdByDate { get; set; }
        [MapTo("FnthProposedCrctAct")]
        public string ProposedCrctAct { get; set; }
        [MapTo("FnthSignCorrective")]
        public string CorrectiveSignature { get; set; }
        [MapTo("FnthUseridCorrective")]
        public int? CorrectiveUserId { get; set; }
        [MapTo("FnthUsernameCorrective")]
        public string CorrectiveUsername { get; set; }
        [MapTo("FnthDesignationCorrective")]
        public string CorrectiveDesignation { get; set; }
        [MapTo("FnthDtCorrective")]
        public DateTime? CorrectiveDate { get; set; }
        [MapTo("FnthSignAccptd")]
        public string AccptdBySignature { get; set; }
        [MapTo("FnthUseridAccptd")]
        public int? AccptdByUserId { get; set; }
        [MapTo("FnthUsernameAccptd")]
        public string AccptdByUsername { get; set; }
        [MapTo("FnthDesignationAccptd")]
        public string AccptdByDesignation { get; set; }
        [MapTo("FnthDtAccptd")]
        public DateTime? AccptdByDate { get; set; }
        [MapTo("FnthPreventiveAct")]
        public string PreventiveAction { get; set; }
        [MapTo("FnthSignPreventive")]
        public string PreventiveSignature { get; set; }
        [MapTo("FnthUseridPreventive")]
        public int? PreventiveUserId { get; set; }
        [MapTo("FnthUsernamePreventive")]
        public string PreventiveUsername { get; set; }
        [MapTo("FnthDesignationPreventive")]
        public string PreventiveDesignation{ get; set; }
        [MapTo("FnthDtPreventive")]
        public DateTime? PreventiveDate { get; set; }
        [MapTo("FnohOthrFllwAct")]
        public string OthrrFollowUpAction { get; set; }
        [MapTo("FnthCloseOutDt")]
        public DateTime? CloseOutDate { get; set; }
        [MapTo("FnthCloseOutRemarks")]
        public string CloseOutRemarks { get; set; }
        [MapTo("FnthSignVer")]
        public string VerifiedBySign { get; set; }
        [MapTo("FnthUseridVer")]
        public int? VerifiedByUserid { get; set; }
        [MapTo("FnthUsernameVer")]
        public string VerifiedByUsername { get; set; }
        [MapTo("FnthDesignationVer")]
        public string VerifiedByDesignation { get; set; }
        [MapTo("FnthDtVer")]
        public DateTime? VerifiedByDate { get; set; }
        [MapTo("FnihModBy")]
        public string ModefiedBy { get; set; }
        [MapTo("FnihModDt")]
        public DateTime? ModifiedDate { get; set; }
        [MapTo("FnihCrBy")]
        public string CreatedBy { get; set; }
        [MapTo("FnihCrDt")]
        public DateTime? CreatedDate { get; set; }
        [MapTo("FnihSubmitSts")]
        public bool SubmitStatus { get; set; }
        [MapTo("FnihActiveYn")]
        public bool? isActive { get; set; }

        [MapTo("FnthFnihPkRefNo")]
        public int? FormN1RefNo { get; set; }
        
        [MapTo("FnthSignPrvAccptd")]
        public string PreventiveAcceptedSign{ get; set; }
        
        [MapTo("FnthUseridPrvAccptd")]
        public int? PreventiveAcceptedUserId { get; set; }
        
        [MapTo("FnthUsernamePrvAccptd")]
        public string PreventiveAcceptedUsername { get; set; }
        
        [MapTo("FnthDesignationPrvAccptd")]
        public string PreventiveAcceptedDessig { get; set; }
        
        [MapTo("FnthDtPrvAccptd")]
        public DateTime? PreventiveAcceptedDate { get; set; }
       
        [MapTo("FnthUsernameAttnTo")]
        public string AttentionToUsername { get; set; }
       
        [MapTo("FnthUsernameCc")]
        public string CCUsername { get; set; }

        [MapTo("FnihDesignationAttnTo")]
        public string AttentionToDesignation { get; set; }

        [MapTo("FnihDesignationCc")]
        public string CCDesignation { get; set; }

        [MapTo("FnthFnihPkRefNoNavigation")]
        public FormN1HeaderRequestDTO  N1HeaderDetails { get; set; }

        public string sortOrder { get; set; }
        public string currentFilter { get; set; }
        public string searchString { get; set; }
        public int? Page_No { get; set; }
        public int? pageSize { get; set; }
        [MapTo("FnthStatus")]
        public string Status { get; set; }
        [MapTo("FnthAuditLog")]
        public string AuditLog { get; set; }
    }
}
