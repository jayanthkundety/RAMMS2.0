using System;
using System.Collections.Generic;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class UserDetailRequestDTO
    {
        [MapTo("UsrPkId")] public int PkId { get; set; }
        [MapTo("UsrContrPkId")] public int? ContrPkId { get; set; }
        [MapTo("UsrUserName")] public string Username { get; set; }
        [MapTo("UsrPassword")] public string Password { get; set; }
        [MapTo("UsrPosition")] public string Position { get; set; }
        [MapTo("UsrDepartment")] public string Department { get; set; }
        [MapTo("UsrCompanyName")] public string Companyname { get; set; }
        [MapTo("UsrEmail")] public string Email { get; set; }
        [MapTo("UsrContactno")] public string Contactno { get; set; }
        [MapTo("UsrReportingUsrPkId")] public int? ReportingPkId { get; set; }
        [MapTo("UsrModBy")] public string ModBy { get; set; }
        [MapTo("UsrModDt")] public DateTime ModDt { get; set; }
        [MapTo("UsrCrBy")] public string CrBy { get; set; }
        [MapTo("UsrCrDt")] public DateTime CrDt { get; set; }
        [MapTo("UsrSubmitSts")] public bool SubmitSts { get; set; }
        [MapTo("UsrActiveYn")] public bool? ActiveYn { get; set; }
        [MapTo("UsrLogindate")] public DateTime Logindate { get; set; }
        [MapTo("UsrIsDisabled")] public bool Isdisabled { get; set; }
        [MapTo("UsrRetryCount")] public int Retrycount { get; set; }
        [MapTo("UsrLockedUntil")] public DateTime? Lockeduntil { get; set; }
        [MapTo("UsrPasswordExpiry")] public DateTime? Passwordexpiry { get; set; }
        [MapTo("UsrDfltUserRole")] public int? DfltUserrole { get; set; }
        [MapTo("UsrUgDfltYn")] public int? UgDfltYn { get; set; }
        [MapTo("UsrSign")] public string Sign { get; set; }
        [MapTo("UsrUserid")] public string Userid { get; set; }
        [MapTo("UsrForceRstPwd")] public bool ForceRstPwd { get; set; }
        public string SmartSearch { get; set; }
        public string CurrentPassword { get; set; }
        public bool IsView { get; set; }
        public int?[] GroupUgPkId { get; set; }
        public int[] GroupId { get; set; }
        public List<UserModuleRightsDTO> ModuleRights { get; set; }
    }

    public class UserModuleRightsDTO
    {
        public int PkId { get; set; }
        public int? ModPkId { get; set; }
        public int? UsrPkId { get; set; }
        public int? GroupPkId { get; set; }        
        public bool? DIsView { get; set; }
        public bool? DIsModify { get; set; }
        public bool? DIsDelete { get; set; }
        public bool? DIsAdd { get; set; }
        public bool? PIsView { get; set; }
        public bool? PIsModify { get; set; }
        public bool? PIsDelete { get; set; }
        public bool? PIsAdd { get; set; }
    }
}
