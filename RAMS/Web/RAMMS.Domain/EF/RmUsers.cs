using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmUsers
    {
        public RmUsers()
        {
            RmGroupUser = new HashSet<RmGroupUser>();
            RmModuleGroupRights = new HashSet<RmModuleGroupRights>();
            RmUserGroup = new HashSet<RmUserGroup>();
        }

        public int UsrPkId { get; set; }
        public int? UsrContrPkId { get; set; }
        public string UsrUserName { get; set; }
        public string UsrPassword { get; set; }
        public string UsrPosition { get; set; }
        public string UsrDepartment { get; set; }
        public string UsrCompanyName { get; set; }
        public string UsrEmail { get; set; }
        public string UsrContactNo { get; set; }
        public int? UsrReportingUsrPkId { get; set; }
        public string UsrModBy { get; set; }
        public DateTime UsrModDt { get; set; }
        public string UsrCrBy { get; set; }
        public DateTime UsrCrDt { get; set; }
        public bool UsrSubmitSts { get; set; }
        public bool? UsrActiveYn { get; set; }
        public DateTime UsrLoginDate { get; set; }
        public bool UsrIsDisabled { get; set; }
        public short UsrRetryCount { get; set; }
        public DateTime? UsrLockedUntil { get; set; }
        public DateTime? UsrPasswordExpiry { get; set; }
        public int? UsrDfltUserRole { get; set; }
        public int? UsrUgDfltYn { get; set; }
        public string UsrSign { get; set; }
        public string UsrUserid { get; set; }
        public bool UsrForceRstPwd { get; set; }

        public virtual ICollection<RmGroupUser> RmGroupUser { get; set; }
        public virtual ICollection<RmModuleGroupRights> RmModuleGroupRights { get; set; }
        public virtual ICollection<RmUserGroup> RmUserGroup { get; set; }
    }
}
