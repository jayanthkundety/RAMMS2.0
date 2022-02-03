using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormS2Hdr
    {
        public RmFormS2Hdr()
        {
            RmFormS2Dtl = new HashSet<RmFormS2Dtl>();
        }

        public int FsiihPkRefNo { get; set; }
        public string FsiihRefId { get; set; }
        public int? FsiihYear { get; set; }
        public int? FsiihQuaterId { get; set; }
        public string FsiihContNo { get; set; }
        public string FsiihRmu { get; set; }
        public int? FsiihActId { get; set; }
        public int? FsiihUseridPrioritised { get; set; }
        public DateTime? FsiihDtPrioritised { get; set; }
        public int? FsiihUseridSchld { get; set; }
        public DateTime? FsiihDtSchld { get; set; }
        public int? FsiihUseridSub { get; set; }
        public DateTime? FsiihDtSub { get; set; }
        public int? FsiihUseridVet { get; set; }
        public DateTime? FsiihDtVet { get; set; }
        public int? FsiihUseridAgrd { get; set; }
        public DateTime? FsiihDtAgrd { get; set; }
        public int? FsiihModBy { get; set; }
        public DateTime? FsiihModDt { get; set; }
        public int? FsiihCrBy { get; set; }
        public DateTime? FsiihCrDt { get; set; }
        public bool FsiihSubmitSts { get; set; }
        public bool? FsiihActiveYn { get; set; }
        public string FsiihActCode { get; set; }
        public string FsiihActName { get; set; }
        public string FsiihUserNamePrioritised { get; set; }
        public string FsiihUserDesignationPrioritised { get; set; }
        public string FsiihUserNameSchId { get; set; }
        public string FsiihUserDesignationSchId { get; set; }
        public string FsiihUserNameSub { get; set; }
        public string FsiihUserDesignationSub { get; set; }
        public string FsiihUserNameVet { get; set; }
        public string FsiihUserDesignationVet { get; set; }
        public string FsiihUserNameAgrd { get; set; }
        public string FsiihUserDesignationAgrd { get; set; }
        public string FsiihStatus { get; set; }
        public string FsiihAuditLog { get; set; }

        public virtual ICollection<RmFormS2Dtl> RmFormS2Dtl { get; set; }
    }
}
