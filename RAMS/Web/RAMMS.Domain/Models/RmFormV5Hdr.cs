using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormV5Hdr
    {
        public int Fv5hPkRefNo { get; set; }
        public int? Fv5hFv4PkRefNo { get; set; }
        public int? Fv5hContNo { get; set; }
        public string Fv5hRmu { get; set; }
        public string Fv5hRmuName { get; set; }
        public int? Fv5hCrew { get; set; }
        public string Fv5hCrewname { get; set; }
        public string Fv5hSecCode { get; set; }
        public string Fv5hSecName { get; set; }
        public string Fv4hDivision { get; set; }
        public string Fv5hRefId { get; set; }
        public string Fv5hActCode { get; set; }
        public string Fv5hActName { get; set; }
        public DateTime? Fv5hDt { get; set; }
        public int? Fv5hYear { get; set; }
        public bool? Fv5hSignRec { get; set; }
        public int? Fv5hUseridRec { get; set; }
        public string Fv5hUsernameRec { get; set; }
        public string Fv5hDesignationRec { get; set; }
        public DateTime? Fv5hDtRec { get; set; }
        public string Fv5hVerifier { get; set; }
        public string Fv5hRemarks { get; set; }
        public int? Fv5hModBy { get; set; }
        public DateTime? Fv5hModDt { get; set; }
        public int? Fv5hCrBy { get; set; }
        public DateTime? Fv5hCrDt { get; set; }
        public bool Fv5hSubmitSts { get; set; }
        public bool Fv5hActiveYn { get; set; }
        public string Fv5hStatus { get; set; }
        public string Fv5hAuditLog { get; set; }
    }
}
