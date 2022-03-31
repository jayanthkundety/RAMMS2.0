using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormV3Hdr
    {
        public RmFormV3Hdr()
        {
            RmFormV3Dtl = new HashSet<RmFormV3Dtl>();
        }

        public int Fv3hPkRefNo { get; set; }
        public int? Fv3hContNo { get; set; }
        public string Fv3hRmu { get; set; }
        public int? Fv3hCrew { get; set; }
        public string Fv3hCrewname { get; set; }
        public string Fv3hSecCode { get; set; }
        public string Fv3hRefId { get; set; }
        public int? Fv3hActCode { get; set; }
        public DateTime? Fv3hDt { get; set; }
        public bool? Fv3hSignFac { get; set; }
        public string Fv3hUseridFac { get; set; }
        public string Fv3hUsernameFac { get; set; }
        public string Fv3hDesignationFac { get; set; }
        public DateTime? Fv3hDtSch { get; set; }
        public bool? Fv3hSignAgr { get; set; }
        public string Fv3hUseridAgr { get; set; }
        public string Fv3hUsernameAgr { get; set; }
        public string Fv3hDesignationAgr { get; set; }
        public DateTime? Fv3hDtAgr { get; set; }
        public bool? Fv3hSignRec { get; set; }
        public string Fv3hUseridRec { get; set; }
        public string Fv3hUsernameRec { get; set; }
        public string Fv3hDesignationRec { get; set; }
        public DateTime? Fv3hDtRec { get; set; }
        public string Fv3hServiceProvider { get; set; }
        public string Fv3hVerifier { get; set; }
        public string Fv3hFacilitator { get; set; }
        public string Fv3hRemarks { get; set; }
        public string Fv3hModBy { get; set; }
        public DateTime? Fv3hModDt { get; set; }
        public string Fv3hCrBy { get; set; }
        public DateTime? Fv3hCrDt { get; set; }
        public bool? Fv3hSubmitSts { get; set; }
        public bool? Fv3hActiveYn { get; set; }
        public string Fv3hStatus { get; set; }
        public string Fv3hAuditLog { get; set; }

        public virtual ICollection<RmFormV3Dtl> RmFormV3Dtl { get; set; }
    }
}
