using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormMAuditDetails
    {
        public int FmadPkRefNo { get; set; }
        public int? FmadFmhPkRefNo { get; set; }
        public int? FmadCategory { get; set; }
        public string FmadActivityCode { get; set; }
        public string FmadActivityName { get; set; }
        public string FmadActivityFor { get; set; }
        public bool? FmadIsEditable { get; set; }
        public int? FmadTallyBox { get; set; }
        public int? FmadWeightage { get; set; }
        public int? FmadTotal { get; set; }
        public int? FmadModBy { get; set; }
        public DateTime? FmadModDt { get; set; }
        public int? FmadCrBy { get; set; }
        public DateTime? FmadCrDt { get; set; }

        public virtual RmFormMHdr FmadFmhPkRefNoNavigation { get; set; }
    }
}
