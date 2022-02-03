using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;

namespace RAMMS.DTO.ResponseBO
{
    public class FormN1HeaderResponseDTO
    {
        public FormN1HeaderResponseDTO()
        {
         
        }
        [MapTo("FnihPkRefNo")]
        public int No { get; set; }
        [MapTo("FnihSourceType")]
        public string SourceType { get; set; }
        [MapTo("FnihFqaiihPkRefNo")]
        public int? QA2RefNo { get; set; }
        [MapTo("FnihFsihPkRefNo")]
        public int? S1RefNo { get; set; }
        [MapTo("FnihContNo")]
        public string ContNo { get; set; }
        [MapTo("FnihRefId")]
        public string ReferenceID { get; set; }
        [MapTo("FnihNcnNo")]
        public string NCNo { get; set; }
        [MapTo("FnihDiv")]
        public string DivisonName { get; set; }
        [MapTo("FnihRmu")]
        public string RMU { get; set; }
        [MapTo("FnihSrProvider")]
        public string ServiceProvider { get; set; }
        [MapTo("FnihUseridAttnTo")]
        public string AttentionToUserid { get; set; }
        [MapTo("FnihUsernameAttnTo")]
        public string AttnToUsername { get; set; }
        [MapTo("FnihUseridCc")]
        public string CcUserid { get; set; }
        [MapTo("FnihUsernameCc")]
        public string CcUsername { get; set; }
        [MapTo("FnihRoadCode")]
        public string RoadCode { get; set; }
        [MapTo("FnihRoadName")]
        public string RoadName { get; set; }
        [MapTo("FnihFrmCh")]
        public int? FromChainage { get; set; }
        [MapTo("FnihFrmChDeci")]
        public int? FromChainageDeci { get; set; }
        [MapTo("FnihToCh")]
        public int? ToChainage { get; set; }
        [MapTo("FnihToChDeci")]
        public int? ToChainageDeci { get; set; }
        [MapTo("FnihNcDesc")]
        public string NcDescription { get; set; }
        [MapTo("FnihDtCorrectTkn")]
        public DateTime? CorrectActionTakenDate { get; set; }
        [MapTo("FnihSignIssued")]
        public string IssuedBySignature { get; set; }
        [MapTo("FnihUseridIssued")]
        public int? IssuedByUserId { get; set; }
        [MapTo("FnihUsernameIssued")]
        public string IssuedByUsername { get; set; }
        [MapTo("FnihDesignationIssued")]
        public string IssuedByDesignation { get; set; }
        [MapTo("FnihSignRcvd")]
        public string RecievedBySignature { get; set; }
        [MapTo("FnihUseridRcvd")]
        public int? RecievedByUserId { get; set; }
        [MapTo("FnihUsernameRcvd")]
        public string RecievedByUsername { get; set; }
        [MapTo("FnihDesignationRcvd")]
        public string RecievedByDesignation { get; set; }
        [MapTo("FnihDtRcvd")]
        public DateTime? RecievedByDate { get; set; }
        [MapTo("FnihProposedRewrkSpec")]
        public string ProposedReworkSpecification { get; set; }
        [MapTo("FnihSignCorrective")]
        public string CorrectiveSignature { get; set; }
        [MapTo("FnihUseridCorrective")]
        public int? CorrectiveUserid { get; set; }
        [MapTo("FnihUsernameCorrective")]
        public string CorrectiveUsername { get; set; }
        [MapTo("FnihDesignationCorrective")]
        public string CorrectiveDesignation { get; set; }
        [MapTo("FnihDtCorrective")]
        public DateTime? CorrectiveDate { get; set; }
        [MapTo("FnihSignAccptd")]
        public string AcceptedBySignature { get; set; }
        [MapTo("FnihUsernameAccptd")]
        public int? AcceptedByUserId { get; set; }
        [MapTo("FnihUsernameAccptd")]
        public string AcceptedByUsername { get; set; }
        [MapTo("FnihDesignationAccptd")]
        public string AcceptedByDesignation { get; set; }
        [MapTo("FnihDtAccptd")]
        public DateTime? AcceptedByDate { get; set; }
        [MapTo("FnihCorrectionTkn")]
        public bool? IsCorrectionTaken { get; set; }
        [MapTo("FnihNcrIssue")]
        public bool? ISNcrIssued { get; set; }
        [MapTo("FnihIssueDt")]
        public DateTime? IssuedDate { get; set; }
        [MapTo("FnihOthrFllwAct")]
        public string OtherFollowAction { get; set; }
        [MapTo("FnihRemarks")]
        public string Remarks { get; set; }
        [MapTo("FnihSignVer")]
        public string VerifiedBySignature { get; set; }
        [MapTo("FnihUseridVer")]
        public int? VerifiedByUserId { get; set; }
        [MapTo("FnihUsernameVer")]
        public string VerifiedByUsername { get; set; }
        [MapTo("FnihDesignationVer")]
        public string VerifiedByDesignation { get; set; }
        [MapTo("FnihDtVer")]
        public DateTime? VerifiedByDate { get; set; }
        [MapTo("FnihModBy")]
        public string ModifiedBy { get; set; }
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

        [MapTo("FnihDesignationAttnTo")]
        public string AttentionToDesignation { get; set; }

        [MapTo("FnihDesignationCc")]
        public string CCDesignation { get; set; }

        [MapTo("FnihCrctTknBef")]
        public DateTime? CorrectionTakenBefore { get; set; }

        [MapTo("FnihDtIssue")]
        public DateTime? IssueDateHdr { get; set; }
        public string sortOrder { get; set; }
        public string currentFilter { get; set; }
        public string searchString { get; set; }
        public int? Page_No { get; set; }
        public int? pageSize { get; set; }
       
    }
}
