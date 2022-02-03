using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmModule
    {
        public RmModule()
        {
            RmModuleGroupFieldRights = new HashSet<RmModuleGroupFieldRights>();
            RmModuleGroupRights = new HashSet<RmModuleGroupRights>();
        }

        public int ModPkId { get; set; }
        public string ModName { get; set; }
        public string ModDescription { get; set; }
        public string ModCreatedBy { get; set; }
        public string ModModifiedBy { get; set; }
        public DateTime ModCreatedOn { get; set; }
        public DateTime ModModifiedOn { get; set; }

        public virtual ICollection<RmModuleGroupFieldRights> RmModuleGroupFieldRights { get; set; }
        public virtual ICollection<RmModuleGroupRights> RmModuleGroupRights { get; set; }
    }
}
