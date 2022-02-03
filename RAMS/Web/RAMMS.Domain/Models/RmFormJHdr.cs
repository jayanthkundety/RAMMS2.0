using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormJHdr
    {
        public RmFormJHdr()
        {
            RmFormJDtl = new HashSet<RmFormJDtl>();
        }

        public int FjhPkRefNo { get; set; }
        public string FjhRoadCode { get; set; }
        public string FjhRmu { get; set; }
        public string FjhRoadName { get; set; }
        public string FjhContNo { get; set; }
        public string FjhAssetGroupCode { get; set; }
        public int? FjhMonth { get; set; }
        public string FjhRemarks { get; set; }
        public string FjhSignPrp { get; set; }
        public int? FjhUseridPrp { get; set; }
        public string FjhUsernamePrp { get; set; }
        public string FjhDesignationPrp { get; set; }
        public DateTime? FjhDtPrp { get; set; }
        public string FjhSignVer { get; set; }
        public int? FjhUseridVer { get; set; }
        public string FjhUsernameVer { get; set; }
        public string FjhDesignationVer { get; set; }
        public DateTime? FjhDtVer { get; set; }
        public string FjhSignVet { get; set; }
        public int? FjhUseridVet { get; set; }
        public string FjhUsernameVet { get; set; }
        public string FjhDesignationVet { get; set; }
        public DateTime? FjhDtVet { get; set; }
        public string FjhModBy { get; set; }
        public DateTime? FjhModDt { get; set; }
        public string FjhCrBy { get; set; }
        public DateTime? FjhCrDt { get; set; }
        public bool FjhSubmitSts { get; set; }
        public bool? FjhActiveYn { get; set; }
        public string FjhRefId { get; set; }
        public int? FjhYear { get; set; }
        public string FjhSection { get; set; }
        public string FjhStatus { get; set; }
        public string FjhAuditLog { get; set; }

        public virtual ICollection<RmFormJDtl> RmFormJDtl { get; set; }
    }
}
