﻿using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmIwFormWd
    {
        public RmIwFormWd()
        {
            RmIwFormWdDtl = new HashSet<RmIwFormWdDtl>();
        }

        public int FwdPkRefNo { get; set; }
        public string FwdRmuCode { get; set; }
        public string FwdSecCode { get; set; }
        public string FwdRoadCode { get; set; }
        public string FwdRoadName { get; set; }
        public int? FwdCh { get; set; }
        public int? FwdChDeci { get; set; }
        public string FwdIwRefNo { get; set; }
        public string FwdIwProjectTitle { get; set; }
        public string FwdOurRefNo { get; set; }
        public string FwdServRefNo { get; set; }
        public DateTime? FwdDtWd { get; set; }
        public DateTime? FwdDtPervCompl { get; set; }
        public DateTime? FwdDtExtn { get; set; }
        public bool? FwdSignIssu { get; set; }
        public int? FwdUseridIssu { get; set; }
        public string FwdUsernameIssu { get; set; }
        public string FwdDesignationIssu { get; set; }
        public DateTime? FwdDtIssu { get; set; }
        public string FwdOfficeIssu { get; set; }
        public int? FwdModBy { get; set; }
        public DateTime? FwdModDt { get; set; }
        public int? FwdCrBy { get; set; }
        public DateTime? FwdCrDt { get; set; }
        public bool FwdSubmitSts { get; set; }
        public bool FwdActiveYn { get; set; }
        public string FwdStatus { get; set; }
        public string FwdAuditLog { get; set; }

        public virtual ICollection<RmIwFormWdDtl> RmIwFormWdDtl { get; set; }
    }
}
