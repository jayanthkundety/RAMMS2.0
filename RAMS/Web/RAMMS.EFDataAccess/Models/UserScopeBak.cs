using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class UserScopeBak
    {
        public int UserPk { get; set; }
        public int SectionPk { get; set; }

        public virtual Section SectionPkNavigation { get; set; }
        public virtual UserBak2 UserPkNavigation { get; set; }
    }
}
