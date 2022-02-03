using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormDHdr
    {
        public RmFormDHdr()
        {
            RmFormDDtl = new HashSet<RmFormDDtl>();
            RmFormDEquipDtl = new HashSet<RmFormDEquipDtl>();
            RmFormDLabourDtl = new HashSet<RmFormDLabourDtl>();
            RmFormDMaterialDtl = new HashSet<RmFormDMaterialDtl>();
        }

        public int FdhPkRefNo { get; set; }
        public int? FdhContNo { get; set; }
        public string FdhRmu { get; set; }
        public string FdhRoadCode { get; set; }
        public string FdhDivName { get; set; }
        public int? FdhWeekNo { get; set; }
        public int? FdhMonth { get; set; }
        public string FdhDay { get; set; }
        public int? FdhYear { get; set; }
        public string FdhSn { get; set; }
        public string FdhSignPrp { get; set; }
        public string FdhUseridPrp { get; set; }
        public string FdhUsernamePrp { get; set; }
        public string FdhDesignationPrp { get; set; }
        public DateTime? FdhDtPrp { get; set; }
        public string FdhSignVer { get; set; }
        public string FdhUseridVer { get; set; }
        public string FdhUsernameVer { get; set; }
        public string FdhDesignationVer { get; set; }
        public DateTime? FdhDtVer { get; set; }
        public string FdhSignVet { get; set; }
        public string FdhUseridVet { get; set; }
        public string FdhUsernameVet { get; set; }
        public string FdhDesignationVet { get; set; }
        public DateTime? FdhDtVet { get; set; }
        public string FdhSubAuthSts { get; set; }
        public DateTime? FdhDtSubAuth { get; set; }
        public string FdhRcvdAuthSts { get; set; }
        public DateTime? FdhDtRcvdAuth { get; set; }
        public string FdhVetAuthSts { get; set; }
        public DateTime? FdhDtVetAuth { get; set; }
        public string FdhModBy { get; set; }
        public DateTime? FdhModDt { get; set; }
        public string FdhCrBy { get; set; }
        public DateTime? FdhCrDt { get; set; }
        public bool FdhSubmitSts { get; set; }
        public bool? FdhActiveYn { get; set; }
        public string FdhRefId { get; set; }
        public string FdhCrewUnit { get; set; }
        public string FdhSignVerSo { get; set; }
        public string FdhUseridVerSo { get; set; }
        public string FdhUsernameVerSo { get; set; }
        public string FdhDesignationVerSo { get; set; }
        public DateTime? FdhDtVerSo { get; set; }
        public string FdhSignPrcdSo { get; set; }
        public string FdhUseridPrcdSo { get; set; }
        public string FdhUsernamePrcdSo { get; set; }
        public string FdhDesignationPrcdSo { get; set; }
        public DateTime? FdhDtPrcdSo { get; set; }
        public string FdhSignAgrdSo { get; set; }
        public string FdhUseridAgrdSo { get; set; }
        public string FdhUsernameAgrdSo { get; set; }
        public string FdhDesignationAgrdSo { get; set; }
        public DateTime? FdhDtAgrdSo { get; set; }
        public string FdhCrewSupName { get; set; }
        public string FdhStatus { get; set; }
        public string FdhAuditLog { get; set; }
        public DateTime? FdhDate { get; set; }

        public virtual ICollection<RmFormDDtl> RmFormDDtl { get; set; }
        public virtual ICollection<RmFormDEquipDtl> RmFormDEquipDtl { get; set; }
        public virtual ICollection<RmFormDLabourDtl> RmFormDLabourDtl { get; set; }
        public virtual ICollection<RmFormDMaterialDtl> RmFormDMaterialDtl { get; set; }
    }
}
