using System;
using System.Collections.Generic;

namespace RAMMS.Domain
{
    public partial class RmGroup
    {
        public RmGroup()
        {
            
            
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
        
        public virtual ICollection<RmUvModuleGroupFieldRights> RmModuleGroupFieldRights { get; set; }
        public virtual ICollection<RmUvModuleGroupRights> RmModuleGroupRights { get; set; }
    }
}
