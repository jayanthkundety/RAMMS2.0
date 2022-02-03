using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.RequestBO
{
    public class UserRequestDTO
    {
        
        public int UserId { get; set; }

        
        public int? ContrPkId { get; set; }

        
        public string UserName { get; set; }

        
        public string Password { get; set; }

        
        public string Position { get; set; }

        
        public string Department { get; set; }

        
        public string CompanyName { get; set; }

        
        public string Email { get; set; }

        
        public string ContactNo { get; set; }

        
        public int? ReportingUsrPkId { get; set; }

        
        public string ModBy { get; set; }

        
        public DateTime ModDt { get; set; }

        
        public string CreatedBy { get; set; }

        
        public DateTime CreatedDt { get; set; }

        
        public bool SubmitSts { get; set; }

        
        public bool? ActiveYn { get; set; }

        
        public DateTime LoginDate { get; set; }

        
        public bool IsDisabled { get; set; }

        
        public short RetryCount { get; set; }

        
        public DateTime? LockedUntil { get; set; }

        
        public DateTime? PasswordExpiry { get; set; }

        
        public int? DfltUserRole { get; set; }

        
        public int? UgDfltYn { get; set; }
    }
}
