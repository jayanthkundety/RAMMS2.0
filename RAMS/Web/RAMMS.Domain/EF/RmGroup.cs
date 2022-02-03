using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmGroup
    {
        public RmGroup()
        {
            RmGroupUser = new HashSet<RmGroupUser>();
            RmModuleGroupFieldRights = new HashSet<RmModuleGroupFieldRights>();
            RmModuleGroupRights = new HashSet<RmModuleGroupRights>();
        }

        public int UgPkId { get; set; }
        public string UgGroupName { get; set; }
        public string UgGroupCode { get; set; }
        public bool UgDfltYn { get; set; }
        public DateTime? UgEffFrmDt { get; set; }
        public DateTime? UgEffToDt { get; set; }
        public string UgRemarks { get; set; }
        public string UgModifiedBy { get; set; }
        public DateTime UgModDt { get; set; }
        public string UgCrBy { get; set; }
        public DateTime UgCrDt { get; set; }
        public int? DepartmentDeptPkId { get; set; }

        public virtual RmDepartment DepartmentDeptPk { get; set; }
        public virtual ICollection<RmGroupUser> RmGroupUser { get; set; }
        public virtual ICollection<RmModuleGroupFieldRights> RmModuleGroupFieldRights { get; set; }
        public virtual ICollection<RmModuleGroupRights> RmModuleGroupRights { get; set; }
    }
}
