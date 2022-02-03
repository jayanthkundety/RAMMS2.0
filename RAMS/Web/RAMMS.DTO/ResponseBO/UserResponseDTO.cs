using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class UserResponseDTO
    {
        [MapTo("UsrPkId")]
        public int UserId { get; set; }

        [MapTo("UsrContrPkId")]
        public int? ContrPkId { get; set; }

        [MapTo("UsrUserName")]
        public string UserName { get; set; }

        [MapTo("UsrPassword")]
        public string Password { get; set; }

        [MapTo("UsrPosition")]
        public string Position { get; set; }

        [MapTo("UsrDepartment")]
        public string Department { get; set; }

        [MapTo("UsrCompanyName")]
        public string CompanyName { get; set; }

        [MapTo("UsrEmail")]
        public string Email { get; set; }

        [MapTo("UsrContactNo")]
        public string ContactNo { get; set; }

        [MapTo("UsrReportingUsrPkId")]
        public int? ReportingUsrPkId { get; set; }

        [MapTo("UsrModBy")]
        public string ModBy { get; set; }

        [MapTo("UsrModDt")]
        public DateTime ModDt { get; set; }

        [MapTo("UsrCrBy")]
        public string CreatedBy { get; set; }

        [MapTo("UsrCrDt")]
        public DateTime CreatedDt { get; set; }

        [MapTo("UsrSubmitSts")]
        public bool SubmitSts { get; set; }

        [MapTo("UsrActiveYn")]
        public bool? ActiveYn { get; set; }

        [MapTo("UsrLoginDate")]
        public DateTime LoginDate { get; set; }

        [MapTo("UsrIsDisabled")]
        public bool IsDisabled { get; set; }

        [MapTo("UsrRetryCount")]
        public short RetryCount { get; set; }

        [MapTo("UsrLockedUntil")]
        public DateTime? LockedUntil { get; set; }

        [MapTo("UsrPasswordExpiry")]
        public DateTime? PasswordExpiry { get; set; }

        [MapTo("UsrDfltUserRole")]
        public int? DfltUserRole { get; set; }

        [MapTo("UsrUgDfltYn")]
        public int? UgDfltYn { get; set; }

        [MapTo("UsrSign")]
        public string SignIn { get; set; }

        [MapTo("UsrForceRstPwd")]
        public bool ForceResetPwd { get; set; }
    }
}
