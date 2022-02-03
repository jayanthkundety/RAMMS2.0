using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Business.ServiceProvider.Services;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RAMMS.Web.UI.Models
{
    public class CAuthorize : AuthorizeAttribute, IAuthorizationFilter
    {
        public string GroupName = string.Empty;
        public string ModuleName = string.Empty;
        public bool IsView = false;
        public bool IsDelete = false;
        public bool IsModify = false;
        public bool IsWebDevice = false;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            ISecurity security = context.HttpContext.RequestServices.GetService(typeof(ISecurity)) as ISecurity;

            if (!security.IsLogin)
            {
                context.Result = new UnauthorizedResult();
            }
            else if (!string.IsNullOrEmpty(GroupName) && !security.HasAnyGroup(GroupName.Split(',')))
            {
                context.Result = new ForbidResult();
            }
            else if (!string.IsNullOrEmpty(ModuleName) && !security.HasAnyModule(ModuleName.Split(',')))
            {
                context.Result = new ForbidResult();
            }
            else if (IsView)
            {
                bool hasView = false;
                foreach (string name in ModuleName.Split(','))
                {
                    if (security.IsPCView(name)) { hasView = true; break; }
                }
                if (!hasView) { context.Result = new ForbidResult(); }
            }
            else if (IsDelete)
            {
                bool hasDelete = false;
                foreach (string name in ModuleName.Split(','))
                {
                    if (security.IsPCDelete(name)) { hasDelete = true; break; }
                }
                if (!hasDelete) { context.Result = new ForbidResult(); }
            }
            else if (IsModify)
            {
                bool hasModify = false;
                foreach (string name in ModuleName.Split(','))
                {
                    if (security.IsPCModify(name)) { hasModify = true; break; }
                }
                if (!hasModify) { context.Result = new ForbidResult(); }
            }
        }
    }    
    public class GroupNameList : Common.GroupNames
    { }
    public class ModuleNameList : Common.ModuleNames
    { }
}
