using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class UserStg
    {
        public int Pk { get; set; }
        public int? ContractorPk { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool IsDisabled { get; set; }
        public short RetryCount { get; set; }
        public DateTime? LockedUntil { get; set; }
        public DateTime PasswordExpiry { get; set; }
        public int? UserRole { get; set; }
        public int? UserGroupPk { get; set; }
        public int? SectionGroupPk { get; set; }
    }
}
