using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormV2Hdr
    {
        public RmFormV2Hdr()
        {
            RmFormV2Eqp = new HashSet<RmFormV2Eqp>();
            RmFormV2Lab = new HashSet<RmFormV2Lab>();
        }

        public int Fv2hPkRefNo { get; set; }
        public int? Fv2hContNo { get; set; }
        public string Fv2hRmu { get; set; }
        public string Fv2hSecCode { get; set; }
        public string Fv2hSecName { get; set; }
        public string Fv2hDivCode { get; set; }
        public string Fv2hDivName { get; set; }
        public string Fv2hRefId { get; set; }
        public int? Fv2hCrew { get; set; }
        public string Fv2hCrewname { get; set; }
        public string Fv2hActCode { get; set; }
        public string Fv2hActName { get; set; }
        public DateTime? Fv2hDt { get; set; }
        public bool? Fv2hSignSch { get; set; }
        public int? Fv2hUseridSch { get; set; }
        public string Fv2hUsernameSch { get; set; }
        public string Fv2hDesignationSch { get; set; }
        public DateTime? Fv2hDtSch { get; set; }
        public bool? Fv2hSignAgr { get; set; }
        public int? Fv2hUseridAgr { get; set; }
        public string Fv2hUsernameAgr { get; set; }
        public string Fv2hDesignationAgr { get; set; }
        public DateTime? Fv2hDtAgr { get; set; }
        public bool? Fv2hSignAck { get; set; }
        public int? Fv2hUseridAck { get; set; }
        public string Fv2hUsernameAck { get; set; }
        public string Fv2hDesignationAck { get; set; }
        public DateTime? Fv2hDtAck { get; set; }
        public string Fv2hServiceProvider { get; set; }
        public string Fv2hVerifier { get; set; }
        public string Fv2hFacilitator { get; set; }
        public string Fv2hRemarks { get; set; }
        public int? Fv2hModBy { get; set; }
        public DateTime? Fv2hModDt { get; set; }
        public int? Fv2hCrBy { get; set; }
        public DateTime? Fv2hCrDt { get; set; }
        public bool Fv2hSubmitSts { get; set; }
        public bool Fv2hActiveYn { get; set; }
        public string Fv2hStatus { get; set; }
        public string Fv2hAuditLog { get; set; }

        public virtual ICollection<RmFormV2Eqp> RmFormV2Eqp { get; set; }
        public virtual ICollection<RmFormV2Lab> RmFormV2Lab { get; set; }
    }
}
