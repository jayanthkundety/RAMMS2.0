using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmGroupUser
    {
        public int? RmUsersUsrPkId { get; set; }
        public int? RmGroupsUgPkId { get; set; }
        public int UsrGpkid { get; set; }

        public virtual RmGroup RmGroupsUgPk { get; set; }
        public virtual RmUsers RmUsersUsrPk { get; set; }
    }
}
