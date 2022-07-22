using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormTHdr
    {
        public RmFormTHdr()
        {
            RmFormTDailyInspection = new HashSet<RmFormTDailyInspection>();
        }

        public int FmtPkRefNo { get; set; }
        public string FmtPkRefId { get; set; }
        public string FmtReferenceNo { get; set; }
        public string FmtRmuCode { get; set; }
        public string FmtRdCode { get; set; }
        public string FmtRdName { get; set; }
        public string FmtDivCode { get; set; }
        public string FmtDivName { get; set; }
        public string FmtSecCode { get; set; }
        public string FmtSecName { get; set; }
        public string FmtDirectionFrm { get; set; }
        public string FmtDirectionTo { get; set; }
        public DateTime? FmtInspectionDate { get; set; }
        public DateTime? FmtAuditTimeFrm { get; set; }
        public DateTime? FmtAuditTimeTo { get; set; }
        public int? FmtPcsTotal { get; set; }
        public int? FmtHvTotal { get; set; }
        public int? FmtMcTotal { get; set; }
        public bool? FmtSignRcd { get; set; }
        public int? FmtUseridRcd { get; set; }
        public string FmtUsernameRcd { get; set; }
        public string FmtDesignationRcd { get; set; }
        public DateTime? FmtDateRcd { get; set; }
        public bool? FmtSignHdd { get; set; }
        public int? FmtUseridHdd { get; set; }
        public string FmtUsernameHdd { get; set; }
        public string FmtDesignationHdd { get; set; }
        public DateTime? FmtDateHdd { get; set; }
        public int? FmtModBy { get; set; }
        public DateTime? FmtModDt { get; set; }
        public int? FmtCrBy { get; set; }
        public DateTime? FmtCrDt { get; set; }
        public bool FmtSubmitSts { get; set; }
        public bool FmtActiveYn { get; set; }
        public string FmtStatus { get; set; }
        public string FmtAuditLog { get; set; }

        public virtual ICollection<RmFormTDailyInspection> RmFormTDailyInspection { get; set; }
    }
}
