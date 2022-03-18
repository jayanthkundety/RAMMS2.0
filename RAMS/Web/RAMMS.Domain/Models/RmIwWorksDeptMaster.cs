using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmIwWorksDeptMaster
    {
        public RmIwWorksDeptMaster()
        {
            RmIwFormWc = new HashSet<RmIwFormWc>();
            RmIwFormWg = new HashSet<RmIwFormWg>();
            RmIwFormWn = new HashSet<RmIwFormWn>();
        }

        public int FiwWrksDeptId { get; set; }
        public string FiwWrksDeptCode { get; set; }
        public string FiwWrksDeptName { get; set; }
        public string FiwWrksDeptAddress1 { get; set; }
        public string FiwWrksDeptAddress2 { get; set; }
        public string FiwWrksDeptAddress3 { get; set; }
        public decimal? FiwWrksDeptPhoneNo { get; set; }
        public decimal? FiwWrksDeptFaxNo { get; set; }
        public decimal? FiwWrksDeptZipcode { get; set; }
        public int? FiwWrksDeptModBy { get; set; }
        public DateTime? FiwWrksDeptModDt { get; set; }
        public int? FiwWrksDeptCrBy { get; set; }
        public DateTime? FiwWrksDeptCrDt { get; set; }
        public bool? FiwWrksDeptActiveYn { get; set; }

        public virtual RmIwFormWd RmIwFormWd { get; set; }
        public virtual ICollection<RmIwFormWc> RmIwFormWc { get; set; }
        public virtual ICollection<RmIwFormWg> RmIwFormWg { get; set; }
        public virtual ICollection<RmIwFormWn> RmIwFormWn { get; set; }
    }
}
