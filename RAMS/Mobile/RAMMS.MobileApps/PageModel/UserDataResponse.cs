using System;
using RAMMS.MobileApps.Services;

namespace RAMMS.MobileApps
{
    public class UserData
    {
        public int UserId { get; set; }
        public object ContrPkId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public object ReportingUsrPkId { get; set; }
        public object ModBy { get; set; }
        public DateTime ModDt { get; set; }
        public object CreatedBy { get; set; }
        public DateTime CreatedDt { get; set; }
        public bool SubmitSts { get; set; }
        public object ActiveYn { get; set; }
        public DateTime LoginDate { get; set; }
        public bool IsDisabled { get; set; }
        public int RetryCount { get; set; }
        public object LockedUntil { get; set; }
        public object PasswordExpiry { get; set; }
        public object DfltUserRole { get; set; }
        public object UgDfltYn { get; set; }
        public object SignIn { get; set; }
    }
    
    public class UserDataResponse:ResponseBase
    {
       
        public UserData data { get; set; }
    }


}
