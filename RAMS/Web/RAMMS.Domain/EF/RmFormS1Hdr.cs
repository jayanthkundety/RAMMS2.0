using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormS1Hdr
    {
        public RmFormS1Hdr()
        {
            RmFormS1Dtl = new HashSet<RmFormS1Dtl>();
        }

        public int FsihPkRefNo { get; set; }
        public string FsihRefId { get; set; }
        public string FsihRmu { get; set; }
        public DateTime? FsihDt { get; set; }
        public int? FsihWeekNo { get; set; }
        public DateTime? FsihFromDt { get; set; }
        public DateTime? FsihToDt { get; set; }
        public int? FsiihUseridPlan { get; set; }
        public DateTime? FsiihDtPlan { get; set; }
        public int? FsiihUseridVet { get; set; }
        public DateTime? FsiihDtVet { get; set; }
        public int? FsiihUseridAgrd { get; set; }
        public DateTime? FsiihDtAgrd { get; set; }
        public int? FsihModBy { get; set; }
        public DateTime? FsihModDt { get; set; }
        public int? FsihCrBy { get; set; }
        public DateTime? FsihCrDt { get; set; }
        public bool? FsihSubmitSts { get; set; }
        public bool? FsihActiveYn { get; set; }
        public string FsihRemarks { get; set; }
        public string FsiihUserNamePlan { get; set; }
        public string FsiihUserDesignationPlan { get; set; }
        public string FsiihUserNameVet { get; set; }
        public string FsiihUserDesignationVet { get; set; }
        public string FsiihUserNameAgrd { get; set; }
        public string FsiihUserDesignationAgrd { get; set; }
        public string FsihStatus { get; set; }
        public string FsihAuditLog { get; set; }

        public virtual ICollection<RmFormS1Dtl> RmFormS1Dtl { get; set; }
    }
}
