using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormHHdr
    {
        public RmFormHHdr()
        {
            RmFormhImageDtl = new HashSet<RmFormhImageDtl>();
        }

        public int FhhPkRefNo { get; set; }
        public int? FhhFadPkRefNo { get; set; }
        public int? FhhFjdPkRefNo { get; set; }
        public string FhhContNo { get; set; }
        public DateTime? FhhInspDt { get; set; }
        public string FhhRoadCode { get; set; }
        public string FhhRmu { get; set; }
        public string FhhDiv { get; set; }
        public string FhhRdName { get; set; }
        public string FhhAssetId { get; set; }
        public string FhhAssetGroupCode { get; set; }
        public int? FhhFrmCh { get; set; }
        public int? FhhFrmChDeci { get; set; }
        public int? FhhToCh { get; set; }
        public int? FhhToChDeci { get; set; }
        public string FhhDamDtl { get; set; }
        public string FhhDamCausedby { get; set; }
        public string FhhRemarks { get; set; }
        public string FhhCltRemarks { get; set; }
        public string FhhSignPrp { get; set; }
        public int? FhhUseridPrp { get; set; }
        public string FhhUsernamePrp { get; set; }
        public string FhhDesignationPrp { get; set; }
        public DateTime? FhhDtPrp { get; set; }
        public string FhhSignVer { get; set; }
        public int? FhhUseridVer { get; set; }
        public string FhhUsernameVer { get; set; }
        public string FhhDesignationVer { get; set; }
        public DateTime? FhhDtVer { get; set; }
        public string FhhSubAuthSts { get; set; }
        public DateTime? FhhStsDtSubAuth { get; set; }
        public string FhhRcvdAuthSts { get; set; }
        public DateTime? FhhStsDtRcvdAuth { get; set; }
        public string FhhAuthRemarks { get; set; }
        public int? FhhAuthRepNo { get; set; }
        public string FhhAuthRecmd { get; set; }
        public string FhhSignRcvdAuth { get; set; }
        public int? FhhUseridRcvdAuth { get; set; }
        public string FhhUsernameRcvdAuth { get; set; }
        public string FhhDesignationRcvdAuth { get; set; }
        public string FhhSignVetAuth { get; set; }
        public int? FhhUseridVetAuth { get; set; }
        public string FhhUsernameVetAuth { get; set; }
        public string FhhDesignationVetAuth { get; set; }
        public string FhhModBy { get; set; }
        public DateTime? FhhModDt { get; set; }
        public string FhhCrBy { get; set; }
        public DateTime? FhhCrDt { get; set; }
        public bool FhhSubmitSts { get; set; }
        public bool? FhhActiveYn { get; set; }
        public string FhhRefId { get; set; }
        public string FhhSection { get; set; }
        public DateTime? FhhDtRcvdAuth { get; set; }
        public DateTime? FhhDtVetAuth { get; set; }
        public string FhhStatus { get; set; }
        public string FhhAuditLog { get; set; }

        public virtual RmFormADtl FhhFadPkRefNoNavigation { get; set; }
        public virtual RmFormJDtl FhhFjdPkRefNoNavigation { get; set; }
        public virtual ICollection<RmFormhImageDtl> RmFormhImageDtl { get; set; }
    }
}
