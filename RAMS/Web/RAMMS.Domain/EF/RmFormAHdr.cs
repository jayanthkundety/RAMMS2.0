using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormAHdr
    {
        public RmFormAHdr()
        {
            RmFormADtl = new HashSet<RmFormADtl>();
        }

        public int FahPkRefNo { get; set; }
        public string FahRefId { get; set; }
        public string FahRoadCode { get; set; }
        public string FahRmu { get; set; }
        public string FahRoadName { get; set; }
        public string FahContNo { get; set; }
        public string FahAssetGroupCode { get; set; }
        public int? FahMonth { get; set; }
        public string FahRemarks { get; set; }
        public string FahSignPrp { get; set; }
        public int? FahUseridPrp { get; set; }
        public string FahUsernamePrp { get; set; }
        public string FahDesignationPrp { get; set; }
        public DateTime? FahDtPrp { get; set; }
        public string FahSignVer { get; set; }
        public int? FahUseridVer { get; set; }
        public string FahUsernameVer { get; set; }
        public string FahDesignationVer { get; set; }
        public DateTime? FahDtVer { get; set; }
        public string FahModBy { get; set; }
        public DateTime? FahModDt { get; set; }
        public string FahCrBy { get; set; }
        public DateTime? FahCrDt { get; set; }
        public bool FahSubmitSts { get; set; }
        public bool? FahActiveYn { get; set; }
        public string FahSection { get; set; }
        public int? FahYear { get; set; }
        public string FahStatus { get; set; }
        public string FahAuditLog { get; set; }

        public virtual ICollection<RmFormADtl> RmFormADtl { get; set; }
    }
}
