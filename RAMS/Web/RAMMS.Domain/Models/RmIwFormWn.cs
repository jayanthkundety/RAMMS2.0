using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmIwFormWn
    {
        public int FwnPkRefNo { get; set; }
        public int? FwnFw1PkRefNo { get; set; }
        public int? FwnIwWrksDeptId { get; set; }
        public string FwnRmuCode { get; set; }
        public string FwnSecCode { get; set; }
        public string FwnRoadCode { get; set; }
        public string FwnRoadName { get; set; }
        public int? FwnCh { get; set; }
        public int? FwnChDeci { get; set; }
        public string FwnIwRefNo { get; set; }
        public string FwnIwProjectTitle { get; set; }
        public string FwnOurRefNo { get; set; }
        public string FwnServRefNo { get; set; }
        public DateTime? FwnDtWn { get; set; }
        public DateTime? FwnDtW2Initiation { get; set; }
        public DateTime? FwnDtW2Compl { get; set; }
        public double? FwnLadAmt { get; set; }
        public bool? FwnSignIssu { get; set; }
        public int? FwnUseridIssu { get; set; }
        public string FwnUsernameIssu { get; set; }
        public string FwnDesignationIssu { get; set; }
        public DateTime? FwnDtIssu { get; set; }
        public string FwnOfficeIssu { get; set; }
        public int? FwnModBy { get; set; }
        public DateTime? FwnModDt { get; set; }
        public int? FwnCrBy { get; set; }
        public DateTime? FwnCrDt { get; set; }
        public bool FwnSubmitSts { get; set; }
        public bool FwnActiveYn { get; set; }
        public string FwnStatus { get; set; }
        public string FwnAuditLog { get; set; }

        public virtual RmIwFormW1 FwnFw1PkRefNoNavigation { get; set; }
        public virtual RmIwWorksDeptMaster FwnIwWrksDept { get; set; }
    }
}
