using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmIwFormWc
    {
        public int FwcPkRefNo { get; set; }
        public int? FwcFw1PkRefNo { get; set; }
        public string FwcRmuCode { get; set; }
        public string FwcSecCode { get; set; }
        public string FwcRoadCode { get; set; }
        public string FwcRoadName { get; set; }
        public int? FwcCh { get; set; }
        public int? FwcChDeci { get; set; }
        public string FwcIwRefNo { get; set; }
        public string FwcIwProjectTitle { get; set; }
        public string FwcOurRefNo { get; set; }
        public string FwcServRefNo { get; set; }
        public DateTime? FwcDtWc { get; set; }
        public DateTime? FwcDtCompl { get; set; }
        public DateTime? FwcDtDlpExtn { get; set; }
        public double? FwcDlpPeriod { get; set; }
        public bool? FwcSignIssu { get; set; }
        public int? FwcUseridIssu { get; set; }
        public string FwcUsernameIssu { get; set; }
        public string FwcDesignationIssu { get; set; }
        public DateTime? FwcDtIssu { get; set; }
        public string FwcOfficeIssu { get; set; }
        public int? FwcModBy { get; set; }
        public DateTime? FwcModDt { get; set; }
        public int? FwcCrBy { get; set; }
        public DateTime? FwcCrDt { get; set; }
        public bool FwcSubmitSts { get; set; }
        public bool FwcActiveYn { get; set; }
        public string FwcStatus { get; set; }
        public string FwcAuditLog { get; set; }

        public virtual RmIwFormW1 FwcFw1PkRefNoNavigation { get; set; }
    }
}
