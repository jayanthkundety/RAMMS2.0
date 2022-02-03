using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormHResponseDTO
    {
        [MapTo("FhhPkRefNo")]
        public int No { get; set; }

        [MapTo("FhhFadPkRefNo")]
        public int? FormAId { get; set; }

        [MapTo("FhhFjdPkRefNo")]
        public int? FormJId { get; set; }

        [MapTo("FhhContNo")]
        public string ContNo { get; set; }

        [MapTo("FhhInspDt")]
        public DateTime? InspDt { get; set; }

        [MapTo("FhhRoadCode")]
        public string RoadCode { get; set; }

        [MapTo("FhhRmu")]
        public string Rmu { get; set; }

        [MapTo("FhhDiv")]
        public string Div { get; set; }

        [MapTo("FhhRdName")]
        public string RdName { get; set; }

        [MapTo("FhhAssetId")]
        public string AssetId { get; set; }

        [MapTo("FhhAssetGroupCode")]
        public string AssetGroupCode { get; set; }

        [MapTo("FhhFrmCh")]
        public int? ChinageFrom { get; set; }

        [MapTo("FhhFrmChDeci")]
        public int? ChinageFromDeci { get; set; }

        [MapTo("FhhToCh")]
        public int? ChinageTo { get; set; }

        [MapTo("FhhToChDeci")]
        public int? ChinageToDeci { get; set; }

        [MapTo("FhhDamDtl")]
        public string DamDtl { get; set; }

        [MapTo("FhhDamCausedby")]
        public string DamCausedby { get; set; }

        [MapTo("FhhRemarks")]
        public string Remarks { get; set; }

        [MapTo("FhhCltRemarks")]
        public string CltRemarks { get; set; }

        [MapTo("FhhSignPrp")]
        public string SignPrp { get; set; }

        [MapTo("FhhUseridPrp")]
        public int? UseridPrp { get; set; }

        [MapTo("FhhUsernamePrp")]
        public string UsernamePrp { get; set; }

        [MapTo("FhhDesignationPrp")]
        public string DesignationPrp { get; set; }

        [MapTo("FhhDtPrp")]
        public DateTime? DtPrp { get; set; }

        [MapTo("FhhSignVer")]
        public string SignVer { get; set; }

        [MapTo("FhhUseridVer")]
        public int? UseridVer { get; set; }

        [MapTo("FhhUsernameVer")]
        public string UsernameVer { get; set; }

        [MapTo("FhhDesignationVer")]
        public string DesignationVer { get; set; }

        [MapTo("FhhDtVer")]
        public DateTime? DtVer { get; set; }

        [MapTo("FhhSubAuthSts")]
        public string SubAuthSts { get; set; }

        [MapTo("FhhDtSubAuth")]
        public DateTime? DtSubAuth { get; set; }

        [MapTo("FhhRcvdAuthSts")]
        public string RcvdAuthSts { get; set; }

        [MapTo("FhhDtRcvdAuth")]
        public DateTime? DtRcvdAuth { get; set; }

        [MapTo("FhhAuthRemarks")]
        public string AuthRemarks { get; set; }

        [MapTo("FhhAuthRepNo")]
        public int? AuthRepNo { get; set; }

        [MapTo("FhhAuthRecmd")]
        public string AuthRecmd { get; set; }

        [MapTo("FhhSignRcvdAuth")]
        public string SignRcvdAuth { get; set; }

        [MapTo("FhhUseridRcvdAuth")]
        public int? UseridRcvdAuth { get; set; }

        [MapTo("FhhUsernameRcvdAuth")]
        public string UsernameRcvdAuth { get; set; }

        [MapTo("FhhDesignationRcvdAuth")]
        public string DesignationRcvdAuth { get; set; }

        [MapTo("FhhSignVetAuth")]
        public string SignVetAuth { get; set; }

        [MapTo("FhhUseridVetAuth")]
        public int? UseridVetAuth { get; set; }

        [MapTo("FhhUsernameVetAuth")]
        public string UsernameVetAuth { get; set; }

        [MapTo("FhhDesignationVetAuth")]
        public string DesignationVetAuth { get; set; }
        [MapTo("FhhDtVetAuth")]
        public DateTime? DtVet { get; set; }

        [MapTo("FhhModBy")]
        public string ModBy { get; set; }

        [MapTo("FhhModDt")]
        public DateTime? ModDt { get; set; }

        [MapTo("FhhCrBy")]
        public string CreatedBy { get; set; }

        [MapTo("FhhCrDt")]
        public DateTime? CreatedDt { get; set; }

        [MapTo("FhhSubmitSts")]
        public bool SubmitSts { get; set; }

        [MapTo("FhhActiveYn")]
        public bool? ActiveYn { get; set; }

        [MapTo("FhhRefId")]
        public string ReferenceNo { get; set; }

        [MapTo("FhhSection")]
        public string Section { get; set; }
        
        [MapTo("FhhStatus")]
        public string Status { get; set; }
       
        [MapTo("FhhAuditLog")]
        public string AuditLog { get; set; }

        public string SectionCode { get; set; }
        public string RmuName { get; set; }

        public string ProcessStatus { get; set; }

    }
}
