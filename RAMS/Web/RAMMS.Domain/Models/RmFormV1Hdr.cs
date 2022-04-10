using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormV1Hdr
    {
        public RmFormV1Hdr()
        {
            RmFormV1Dtl = new HashSet<RmFormV1Dtl>();
            RmFormV2Hdr = new HashSet<RmFormV2Hdr>();
        }

        public int Fv1hPkRefNo { get; set; }
        public int? Fv1hContNo { get; set; }
        public string Fv1hRmu { get; set; }
        public string Fv1hSecCode { get; set; }
        public string Fv1hSecName { get; set; }
        public string Fv1hDivCode { get; set; }
        public string Fv1hDivName { get; set; }
        public string Fv1hRefId { get; set; }
        public int? Fv1hCrew { get; set; }
        public string Fv1hCrewname { get; set; }
        public string Fv1hActCode { get; set; }
        public string Fv1hActName { get; set; }
        public DateTime? Fv1hDt { get; set; }
        public bool? Fv1hSignSch { get; set; }
        public int? Fv1hUseridSch { get; set; }
        public string Fv1hUsernameSch { get; set; }
        public string Fv1hDesignationSch { get; set; }
        public DateTime? Fv1hDtSch { get; set; }
        public bool? Fv1hSignAgr { get; set; }
        public int? Fv1hUseridAgr { get; set; }
        public string Fv1hUsernameAgr { get; set; }
        public string Fv1hDesignationAgr { get; set; }
        public DateTime? Fv1hDtAgr { get; set; }
        public bool? Fv1hSignAck { get; set; }
        public int? Fv1hUseridAck { get; set; }
        public string Fv1hUsernameAck { get; set; }
        public string Fv1hDesignationAck { get; set; }
        public DateTime? Fv1hDtAck { get; set; }
        public string Fv1hServiceProvider { get; set; }
        public string Fv1hVerifier { get; set; }
        public string Fv1hFacilitator { get; set; }
        public string Fv1hRemarks { get; set; }
        public int? Fv1hModBy { get; set; }
        public DateTime? Fv1hModDt { get; set; }
        public int? Fv1hCrBy { get; set; }
        public DateTime? Fv1hCrDt { get; set; }
        public bool Fv1hSubmitSts { get; set; }
        public bool Fv1hActiveYn { get; set; }
        public string Fv1hStatus { get; set; }
        public string Fv1hAuditLog { get; set; }

        public virtual ICollection<RmFormV1Dtl> RmFormV1Dtl { get; set; }
        public virtual ICollection<RmFormV2Hdr> RmFormV2Hdr { get; set; }
    }
}
