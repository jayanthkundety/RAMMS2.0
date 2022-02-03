using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmDepartment
    {
        public RmDepartment()
        {
            RmGroup = new HashSet<RmGroup>();
        }

        public int DeptPkId { get; set; }
        public string DeptName { get; set; }
        public string DeptDescription { get; set; }
        public string DeptCreatedBy { get; set; }
        public string DeptModifiedBy { get; set; }
        public DateTime? DeptCreatedOn { get; set; }
        public DateTime? DeptMofiedOn { get; set; }

        public virtual ICollection<RmGroup> RmGroup { get; set; }
    }
}
