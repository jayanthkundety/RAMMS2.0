using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class UserGroupRights
    {
        public int UserGroupPk { get; set; }
        public int AccessRight { get; set; }
    }
}
