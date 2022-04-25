using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormV4Hdr
    {
        public int Fv4hPkRefNo { get; set; }
        public int? Fv4hFv3PkRefNo { get; set; }
        public string Fv4hFv3PkRefId { get; set; }
        public int? Fv4hContNo { get; set; }
        public string Fv4hRmu { get; set; }
        public string Fv4hRmuName { get; set; }
        public string Fv4hDivision { get; set; }
        public int? Fv4hCrew { get; set; }
        public string Fv4hCrewname { get; set; }
        public string Fv4hSecCode { get; set; }
        public string Fv4hSecName { get; set; }
        public string Fv4hRefId { get; set; }
        public string Fv4hActCode { get; set; }
        public string Fv4hActName { get; set; }
        public DateTime? Fv4hDt { get; set; }
        public string Fv4hStartTime { get; set; }
        public string Fv4hEndTime { get; set; }
        public decimal? Fv4hTotalProduction { get; set; }
        public string Fv4hRemarks { get; set; }
        public bool? Fv4hSignFac { get; set; }
        public int? Fv4hUseridFac { get; set; }
        public string Fv4hUsernameFac { get; set; }
        public string Fv4hDesignationFac { get; set; }
        public DateTime? Fv4hDtFac { get; set; }
        public bool? Fv4hSignAgr { get; set; }
        public string Fv4hUseridAgr { get; set; }
        public string Fv4hUsernameAgr { get; set; }
        public string Fv4hDesignationAgr { get; set; }
        public DateTime? Fv4hDtAgr { get; set; }
        public bool? Fv4hSignVet { get; set; }
        public int? Fv4hUseridVet { get; set; }
        public string Fv4hUsernameVet { get; set; }
        public string Fv4hDesignationVet { get; set; }
        public DateTime? Fv4hDtVet { get; set; }
        public string Fv4hServiceProvider { get; set; }
        public string Fv4hVerifier { get; set; }
        public string Fv4hFacilitator { get; set; }
        public int? Fv4hModBy { get; set; }
        public DateTime? Fv4hModDt { get; set; }
        public int? Fv4hCrBy { get; set; }
        public DateTime? Fv4hCrDt { get; set; }
        public bool Fv4hSubmitSts { get; set; }
        public bool Fv4hActiveYn { get; set; }
        public string Fv4hStatus { get; set; }
        public string Fv4hAuditLog { get; set; }
    }
}
