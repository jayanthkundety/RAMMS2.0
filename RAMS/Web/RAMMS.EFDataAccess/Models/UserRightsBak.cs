using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class UserRightsBak
    {
        public int UserPk { get; set; }
        public int AccessRight { get; set; }

        public virtual UserBak2 UserPkNavigation { get; set; }
    }
}
