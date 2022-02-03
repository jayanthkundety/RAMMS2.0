using System;
using System.Collections.Generic;

namespace RAMMS.Domain
{
    public partial class RmModule
    {
        public RmModule()
        {            
        }

        public int ModPkId { get; set; }
        public string ModName { get; set; }
        public string ModDescription { get; set; }
        public string ModCreatedBy { get; set; }
        public string ModModifiedBy { get; set; }
        public DateTime ModCreatedOn { get; set; }
        public DateTime ModModifiedOn { get; set; }

        public virtual ICollection<RmUvModuleGroupFieldRights> RmModuleGroupFieldRights { get; set; }
        public virtual ICollection<RmUvModuleGroupRights> RmModuleGroupRights { get; set; }
    }
}
