using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using RAMMS.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class Security : ISecurity
    {
        #region Private Declaration
        private readonly HttpContext context = null;
        private readonly IFieldRightsRepository repFieldRights;
        private readonly IModuleRightsRepository repModuleRights;
        private readonly IGroupRepository repGroup;
        #endregion
        #region Ctor and Default Binding
        public Security(IHttpContextAccessor httpContextAccessor, IFieldRightsRepository FieldrightsRepo, IModuleRightsRepository ModuleRightsRepo, IGroupRepository GroupRepo)
        {
            repFieldRights = FieldrightsRepo;            
            repModuleRights = ModuleRightsRepo;
            repGroup = GroupRepo;
            context = httpContextAccessor.HttpContext;
            BindContext();            
        }

        /// <summary>
        /// Bind login details to object
        /// </summary>
        private void BindContext()
        {
            var currentUser = context.User;
            if (currentUser != null && currentUser.Identity != null && currentUser.Identity.IsAuthenticated)
            {
                this.IsLogin = true;
                this.UserName = currentUser.Identity.Name;
                var claims = currentUser.Claims;
                this.Email = claims.Where(x => x.Type == ClaimTypes.Email).Select(x => x.Value).FirstOrDefault();
                this.Group = claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).FirstOrDefault();
                this.Module = claims.Where(x => x.Type == "Modules").Select(x => x.Value).FirstOrDefault();
                this.UserID = Convert.ToInt32(claims.Where(x => x.Type == "UserID").Select(x => x.Value).FirstOrDefault());
                this.Group = this.Group == null ? "" : this.Group;
                this.Module = this.Module == null ? "" : this.Module;
                this.Groups = this.Group == "" ? null : this.Group.Split(',');
                this.Modules = this.Module.Split(',');
                this.IsSupervisor = this.HasAnyGroup(Common.GroupNames.Admin, Common.GroupNames.Supervisor);
                this.IsExecutive = this.HasAnyGroup(Common.GroupNames.Admin, Common.GroupNames.OperationsExecutive);
                this.IsHeadMaintenance = this.HasAnyGroup(Common.GroupNames.Admin, Common.GroupNames.OpeHeadMaintenance);
                this.IsJKRSSuperiorOfficer = this.HasAnyGroup(Common.GroupNames.Admin, Common.GroupNames.JKRSSuperiorOfficerSO);
                this.IsRegionManager = this.HasAnyGroup(Common.GroupNames.Admin, Common.GroupNames.OperRegionManager);
            }
            else
            {
                this.UserName = "Anonymous";
            }
        }
        #endregion
        #region Public Property
        public bool IsLogin { get; private set; }
        public int UserID { get; set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string Group { get; private set; }
        public string Module { get; private set; }
        public IList<string> Groups { get; private set; }
        public IList<string> Modules { get; private set; }
        #endregion
        #region Web Authentication
        /// <summary>
        /// Cookie Authentication for web
        /// </summary>
        /// <param name="user"></param>
        public void WebSignInRegister(RAMMS.Domain.Models.RmUsers user)
        {
            IList<RmUvModuleGroupRights> modRights = ModuleRights();
            IList<RmGroup> allGroups = AllGroups();
            IEnumerable<int> gIds = user.RmGroupUser.Where(x => x.RmGroupsUgPkId.HasValue).Select(x => x.RmGroupsUgPkId.Value);
            string strRoles = string.Join(',', allGroups.Where(x => gIds.Contains(x.UgPkId)).Select(x => x.UgGroupCode));
            string strModules = string.Join(',', modRights.Where(x => (x.UgPkId.HasValue && gIds.Contains(x.UgPkId.Value)) || (x.UsrPkId.HasValue && x.UsrPkId == user.UsrPkId)).Select(x => x.ModuleName).Distinct());
            var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, user.UsrUserName) ,
                 new Claim("UserID", user.UsrPkId.ToString()),
                new Claim(ClaimTypes.Email,string.IsNullOrEmpty(user.UsrEmail) ? string.Empty : user.UsrEmail),
                new Claim(ClaimTypes.Role, strRoles),
                new Claim("Modules",strModules)
            }, CookieAuthenticationDefaults.AuthenticationScheme); ;
            var principal = new ClaimsPrincipal(identity);
            context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
        public string DeviceSignInRegister(IConfiguration config, RAMMS.Domain.Models.RmUsers user)
        {
            var _secret = config.GetSection("JwtConfig").GetSection("secret").Value;
            var _expDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
            IList<RmUvModuleGroupRights> modRights = ModuleRights();
            IList<RmGroup> allGroups = AllGroups();
            IEnumerable<int> gIds = user.RmGroupUser.Where(x => x.RmGroupsUgPkId.HasValue).Select(x => x.RmGroupsUgPkId.Value);
            string strRoles = string.Join(',', allGroups.Where(x => gIds.Contains(x.UgPkId)).Select(x => x.UgGroupCode));
            string strModules = string.Join(',', modRights.Where(x => (x.UgPkId.HasValue && gIds.Contains(x.UgPkId.Value)) || (x.UsrPkId.HasValue && x.UsrPkId == user.UsrPkId)).Select(x => x.ModuleName));
            var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, user.UsrUserName),
                new Claim("UserID", user.UsrPkId.ToString()),
                new Claim(ClaimTypes.Email, user.UsrEmail ?? ""),
                new Claim(ClaimTypes.Role, strRoles),
                new Claim("Modules",strModules)
            });
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        /// <summary>
        /// Sign Out - Cookie Authentication
        /// </summary>
        public void WebSignOut()
        {
            context.SignOutAsync();
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Get all Field Rights with respective of Group and Module
        /// </summary>
        /// <returns>Collection of Field Rights</returns>
        public IList<RmUvModuleGroupFieldRights> FieldRights()
        {
            return MemoryCache.Instance.FieldRights ??= repFieldRights.GetAllFieldRights();
        }
        /// <summary>
        /// Get all Rights with respective of Group and Module
        /// </summary>
        /// <returns></returns>
        public IList<RmUvModuleGroupRights> ModuleRights()
        {
            return MemoryCache.Instance.ModuleRights ??= repModuleRights.GetAllModuleRights();
        }
        /// <summary>
        /// Gett all the groups
        /// </summary>
        /// <returns></returns>
        public IList<RmGroup> AllGroups()
        {
            return MemoryCache.Instance.Groups ??= repGroup.GetAllGroups();
        }

        /// <summary>
        /// Contains Group with respective of gorup name
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public bool HasGroup(string groupName)
        {
            return ("," + this.Group + ",").Contains("," + groupName + ",");
        }
        public bool HasAnyGroup(params string[] groupNames)
        {
            return HasAnyGroup(groupNames.ToList());
        }
        /// <summary>
        /// Contains group with respective of list of group name
        /// </summary>
        /// <param name="groupNames">List of group name</param>
        /// <returns></returns>
        public bool HasAnyGroup(ICollection<string> groupNames)
        {
            foreach (var group in groupNames)
            {
                if (HasGroup(group))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Contains Module with respective of Module Name
        /// </summary>
        /// <param name="modName">Module Name</param>
        /// <returns></returns>
        public bool HasModule(string modName)
        {
            return ("," + this.Module + ",").Contains("," + modName + ",");
        }
        public bool HasAnyModule(params string[] modNames)
        {
            return HasAnyModule(modNames.ToList());
        }
        /// <summary>
        /// Contains Module with respective of list of module name
        /// </summary>
        /// <param name="modNames">Collection of Module name</param>
        /// <returns></returns>
        public bool HasAnyModule(ICollection<string> modNames)
        {
            foreach (var modName in modNames)
            {
                if (HasModule(modName))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Get Field Rights with respective of field name, Group and Module
        /// </summary>
        /// <param name="fieldName">Field Name</param>
        /// <returns>First In First Out / Default</returns>
        public RmUvModuleGroupFieldRights FieldRights(string fieldName)
        {
            IList<RmUvModuleGroupFieldRights> fields = FieldRights();
            RmUvModuleGroupFieldRights result = fields.Where(x => x.MgfrFieldName == fieldName && this.Groups.Contains(x.GroupCode) && this.Modules.Contains(x.ModuleName)).FirstOrDefault();
            return result ??= new RmUvModuleGroupFieldRights() { MgfrIsDisabled = false, MgfrIsHide = false };
        }
        /// <summary>
        /// Is field disabled with respective of Group and Module
        /// </summary>
        /// <param name="fieldName">Field Name</param>
        /// <returns>True / False. Default is false, if there is no records in configura table</returns>
        public bool IsFieldDisabled(string moduleName, string fieldName)
        {
            IList<RmUvModuleGroupFieldRights> fields = FieldRights();
            if (this.Groups != null && this.Modules != null)
            {
                return fields.Where(x => x.MgfrFieldName == fieldName && x.MgfrIsDisabled.HasValue && x.MgfrIsDisabled.Value && this.Groups.Contains(x.GroupCode) && x.ModuleName == moduleName && this.Modules.Contains(x.ModuleName)).Count() > 0 ? true : false;
            }
            return false;
        }
        /// <summary>
        /// Is field  hidden with respective of Group and Module
        /// </summary>
        /// <param name="fieldName">Field Name</param>
        /// <returns>True / False. Default is false, if there is no records in configura table</returns>
        public bool IsFieldHide(string moduleName, string fieldName)
        {
            IList<RmUvModuleGroupFieldRights> fields = FieldRights();
            if (this.Groups != null && this.Modules != null)
            {
                return fields.Where(x => x.MgfrFieldName == fieldName && x.MgfrIsHide.HasValue && x.MgfrIsHide.Value && this.Groups.Contains(x.GroupCode) && x.ModuleName == moduleName && this.Modules.Contains(x.ModuleName)).Count() > 0 ? true : false;
            }
            return false;
        }
        public bool IsPCView(string moduleName)
        {
            return PCOperationRights(moduleName, null, true, false, false, false).Item1;
        }
        public bool IsPCModify(string moduleName)
        {
            return PCOperationRights(moduleName, null, false, true, false, false).Item2;
        }
        public bool IsPCDelete(string moduleName)
        {
            return PCOperationRights(moduleName, null, false, false, true, false).Item3;
        }
        public bool IsPCAdd(string moduleName)
        {
            return PCOperationRights(moduleName, null, false, false, false, true).Item4;
        }
        public bool IsPCView(string moduleName, params string[] groupName)
        {
            return PCOperationRights(moduleName, groupName, true, false, false, false).Item1;
        }
        public bool IsPCModify(string moduleName, params string[] groupName)
        {
            return PCOperationRights(moduleName, groupName, false, true, false, false).Item2;
        }
        public bool IsPCDelete(string moduleName, params string[] groupName)
        {
            return PCOperationRights(moduleName, groupName, false, false, true, false).Item3;
        }
        public bool IsPCAdd(string moduleName, params string[] groupName)
        {
            return PCOperationRights(moduleName, groupName, false, false, false, true).Item4;
        }
        public Tuple<bool, bool, bool, bool> PCOperationRights(string moduleName, string[] groupName, bool reqView, bool reqModify, bool reqDelete, bool reqAdd = false)
        {
            bool blnView = false, blnDelete = false, blnModify = false, blnAdd = false;
            IList<RmUvModuleGroupRights> mgRights = ModuleRights();
            if (this.Modules != null)
            {
                if (this.HasGroup("admin"))
                {
                    if (reqView)
                        blnView = true;
                    if (reqModify)
                        blnModify = true;
                    if (reqDelete)
                        blnDelete = true;
                    if (reqAdd)
                        blnAdd = true;
                }
                else
                {
                    var rsltFilter = mgRights.Where(x => (this.Groups != null && this.Groups.Contains(x.GroupCode) && this.Modules.Contains(x.ModuleName)) || (x.UsrPkId == this.UserID && this.Modules.Contains(x.ModuleName))).ToList();
                    if (reqView)
                        blnView = rsltFilter.Where(x => (groupName == null || groupName.Contains(x.GroupCode)) && x.ModuleName == moduleName && x.PcIsView.HasValue && x.PcIsView.Value).Count() > 0 ? true : false;
                    if (reqModify)
                        blnModify = rsltFilter.Where(x => (groupName == null || groupName.Contains(x.GroupCode)) && x.ModuleName == moduleName && x.PcIsModify.HasValue && x.PcIsModify.Value).Count() > 0 ? true : false;
                    if (reqDelete)
                        blnDelete = rsltFilter.Where(x => (groupName == null || groupName.Contains(x.GroupCode)) && x.ModuleName == moduleName && x.PcIsDelete.HasValue && x.PcIsDelete.Value).Count() > 0 ? true : false;
                    if (reqAdd)
                        blnAdd = rsltFilter.Where(x => (groupName == null || groupName.Contains(x.GroupCode)) && x.ModuleName == moduleName && x.PcIsAdd.HasValue && x.PcIsAdd.Value).Count() > 0 ? true : false;
                }
            }
            return new Tuple<bool, bool, bool, bool>(blnView, blnModify, blnDelete, blnAdd);
        }
        //public bool IsPCView(string groupName, string moduleName)
        //{
        //    IList<RmModuleGroupRights> mgRights = ModuleRights();
        //    return mgRights.Where(x => x.UgPk.UgGroupCode == groupName && x.ModPk.ModName == moduleName && x.PcIsView.HasValue && x.PcIsView.Value).Count() > 0 ? true : false;
        //}
        public string IPAddress
        {
            get
            {
                return context?.Connection?.RemoteIpAddress != null ? context.Connection?.RemoteIpAddress.ToString() : "";
            }
        }
        public bool IsSupervisor { get; private set; }
        public bool IsExecutive { get; private set; }
        public bool IsHeadMaintenance { get; private set; }
        public bool IsJKRSSuperiorOfficer { get; private set; }
        public bool IsRegionManager { get; private set; }
        #endregion
    }
}
