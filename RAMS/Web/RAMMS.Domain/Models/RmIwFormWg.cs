using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmIwFormWg
    {
        public int FwgPkRefNo { get; set; }
        public int? FwgFw1PkRefNo { get; set; }
        public int? FwgIwWrksDeptId { get; set; }
        public string FwgRmuCode { get; set; }
        public string FwgSecCode { get; set; }
        public string FwgRoadCode { get; set; }
        public string FwgRoadName { get; set; }
        public int? FwgCh { get; set; }
        public int? FwgChDeci { get; set; }
        public string FwgIwRefNo { get; set; }
        public string FwgIwProjectTitle { get; set; }
        public string FwgOurRefNo { get; set; }
        public string FwgServRefNo { get; set; }
        public DateTime? FwgDtWg { get; set; }
        public DateTime? FwgDtDefectCompl { get; set; }
        public bool? FwgSignIssu { get; set; }
        public int? FwgUseridIssu { get; set; }
        public string FwgUsernameIssu { get; set; }
        public string FwgDesignationIssu { get; set; }
        public DateTime? FwgDtIssu { get; set; }
        public string FwgOfficeIssu { get; set; }
        public int? FwgModBy { get; set; }
        public DateTime? FwgModDt { get; set; }
        public int? FwgCrBy { get; set; }
        public DateTime? FwgCrDt { get; set; }
        public bool FwgSubmitSts { get; set; }
        public bool FwgActiveYn { get; set; }
        public string FwgStatus { get; set; }
        public string FwgAuditLog { get; set; }

        public virtual RmIwFormW1 FwgFw1PkRefNoNavigation { get; set; }
        public virtual RmIwWorksDeptMaster FwgIwWrksDept { get; set; }
    }
}
