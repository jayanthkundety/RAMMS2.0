using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormMHdr
    {
        public RmFormMHdr()
        {
            RmFormMAuditDetails = new HashSet<RmFormMAuditDetails>();
        }

        public int FmhPkRefNo { get; set; }
        public string FmhRefId { get; set; }
        public string FmhDist { get; set; }
        public string FmhType { get; set; }
        public string FmhMethod { get; set; }
        public string FmhDivCode { get; set; }
        public string FmhRmuCode { get; set; }
        public string FmhRmuName { get; set; }
        public string FmhRdCode { get; set; }
        public string FmhRdName { get; set; }
        public string FmhActCode { get; set; }
        public string FmhCrewSup { get; set; }
        public string FmhDesc { get; set; }
        public DateTime? FmhAuditedDate { get; set; }
        public DateTime? FmhAuditTimeFrm { get; set; }
        public DateTime? FmhAuditTimeTo { get; set; }
        public string FmhAuditedBy { get; set; }
        public string FmhAuditType { get; set; }
        public string FmhSrProvider { get; set; }
        public int? FmhFinalResult { get; set; }
        public string FmhFinalResultValue { get; set; }
        public string FmhFindings { get; set; }
        public string FmhCorrectiveActions { get; set; }
        public bool? FmhSignAudit { get; set; }
        public int? FmhUseridAudit { get; set; }
        public string FmhUsernameAudit { get; set; }
        public string FmhDesignationAudit { get; set; }
        public DateTime? FmhDateAudit { get; set; }
        public string FmhOfAudit { get; set; }
        public bool? FmhSignWit { get; set; }
        public int? FmhUseridWit { get; set; }
        public string FmhUsernameWit { get; set; }
        public string FmhDesignationWit { get; set; }
        public DateTime? FmhDateWit { get; set; }
        public string FmhOfWit { get; set; }
        public bool? FmhSignWitone { get; set; }
        public int? FmhUseridWitone { get; set; }
        public string FmhUsernameWitone { get; set; }
        public string FmhDesignationWitone { get; set; }
        public DateTime? FmhDateWitone { get; set; }
        public string FmhOfWitone { get; set; }
        public int? FmhModBy { get; set; }
        public DateTime? FmhModDt { get; set; }
        public int? FmhCrBy { get; set; }
        public DateTime? FmhCrDt { get; set; }
        public bool FmhSubmitSts { get; set; }
        public bool FmhActiveYn { get; set; }
        public string Fmhstatus { get; set; }
        public string FmhAuditLog { get; set; }

        public virtual ICollection<RmFormMAuditDetails> RmFormMAuditDetails { get; set; }
    }
}
