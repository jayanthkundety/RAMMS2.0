using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class UserGroupUser
    {
        public int UserGroupPk { get; set; }
        public int UserPk { get; set; }
    }
}
