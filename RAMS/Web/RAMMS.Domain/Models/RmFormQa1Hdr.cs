using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormQa1Hdr
    {
        public RmFormQa1Hdr()
        {
            RmFormQa1EqVh = new HashSet<RmFormQa1EqVh>();
            RmFormQa1Gc = new HashSet<RmFormQa1Gc>();
            RmFormQa1Gen = new HashSet<RmFormQa1Gen>();
            RmFormQa1Lab = new HashSet<RmFormQa1Lab>();
            RmFormQa1Mat = new HashSet<RmFormQa1Mat>();
            RmFormQa1Ssc = new HashSet<RmFormQa1Ssc>();
            RmFormQa1Tes = new HashSet<RmFormQa1Tes>();
            RmFormQa1Wcq = new HashSet<RmFormQa1Wcq>();
            RmFormQa1We = new HashSet<RmFormQa1We>();
        }

        public int Fqa1hPkRefNo { get; set; }
        public int? Fqa1hContNo { get; set; }
        public string Fqa1hRmu { get; set; }
        public string Fqa1hRmuName { get; set; }
        public string Fqa1hSecCode { get; set; }
        public string Fqa1hSecName { get; set; }
        public string Fqa1hRoadCode { get; set; }
        public string Fqa1hRoadName { get; set; }
        public int? Fqa1hWeekNo { get; set; }
        public string Fqa1hDay { get; set; }
        public int? Fqa1hYear { get; set; }
        public string Fqa1hRefId { get; set; }
        public int? Fqa1hCrew { get; set; }
        public string Fqa1hCrewname { get; set; }
        public string Fqa1hActCode { get; set; }
        public string Fqa1hActName { get; set; }
        public DateTime? Fqa1hDt { get; set; }
        public int? Fqa1hUseridAssgn { get; set; }
        public string Fqa1hInitialAssgn { get; set; }
        public string Fqa1hUsernameAssgn { get; set; }
        public DateTime? Fqa1hDtAssgn { get; set; }
        public int? Fqa1hUseridExec { get; set; }
        public string Fqa1hInitialExec { get; set; }
        public string Fqa1hUsernameExec { get; set; }
        public DateTime? Fqa1hDtExec { get; set; }
        public int? Fqa1hUseridChked { get; set; }
        public string Fqa1hInitialChked { get; set; }
        public string Fqa1hUsernameChked { get; set; }
        public DateTime? Fqa1hDtChked { get; set; }
        public int? Fqa1hModBy { get; set; }
        public DateTime? Fqa1hModDt { get; set; }
        public int? Fqa1hCrBy { get; set; }
        public DateTime? Fqa1hCrDt { get; set; }
        public bool Fqa1hSubmitSts { get; set; }
        public bool? Fqa1hSignAudit { get; set; }
        public int? Fqa1hUseridAudit { get; set; }
        public string Fqa1hUsernameAudit { get; set; }
        public string Fqa1hDesignationAudit { get; set; }
        public DateTime? Fqa1hDtAudit { get; set; }
        public string Fqa1hOfficeAudit { get; set; }
        public bool? Fqa1hSignWit { get; set; }
        public int? Fqa1hUseridWit { get; set; }
        public string Fqa1hUsernameWit { get; set; }
        public string Fqa1hDesignationWit { get; set; }
        public DateTime? Fqa1hDtWit { get; set; }
        public string Fqa1hOfficeWit { get; set; }
        public string Fqa1hRemarks { get; set; }
        public bool Fqa1hActiveYn { get; set; }
        public string Fqa1hStatus { get; set; }
        public string Fqa1hAuditLog { get; set; }

        public virtual ICollection<RmFormQa1EqVh> RmFormQa1EqVh { get; set; }
        public virtual ICollection<RmFormQa1Gc> RmFormQa1Gc { get; set; }
        public virtual ICollection<RmFormQa1Gen> RmFormQa1Gen { get; set; }
        public virtual ICollection<RmFormQa1Lab> RmFormQa1Lab { get; set; }
        public virtual ICollection<RmFormQa1Mat> RmFormQa1Mat { get; set; }
        public virtual ICollection<RmFormQa1Ssc> RmFormQa1Ssc { get; set; }
        public virtual ICollection<RmFormQa1Tes> RmFormQa1Tes { get; set; }
        public virtual ICollection<RmFormQa1Wcq> RmFormQa1Wcq { get; set; }
        public virtual ICollection<RmFormQa1We> RmFormQa1We { get; set; }
    }
}
