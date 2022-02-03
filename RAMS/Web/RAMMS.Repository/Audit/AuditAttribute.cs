using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.Repository.Audit
{
    [AttributeUsage(AttributeTargets.All)]
    public class AuditAttribute : ActionFilterAttribute
    {
        private IUserContext _context;
        private string actionMessage = "";
        public AuditAttribute()
        {

        }        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {            
            _context = filterContext.HttpContext.RequestServices.GetService(typeof(IUserContext)) as IUserContext;
            if (string.IsNullOrEmpty(ActionMessage)) {                
                var descriptor = ((ControllerBase)filterContext.Controller).ControllerContext.ActionDescriptor;
                var actionName = descriptor.ActionName;
                var controllerName = descriptor.ControllerName;
                _context.ActionMessage = controllerName + " " + actionName;
            }
            else
            {
                _context.ActionMessage = ActionMessage;
            }
        }       
        public virtual string ActionMessage
        {
            get { return actionMessage; }
            set { actionMessage = value; }
        }
    }
}
