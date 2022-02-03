using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RAMMS.Repository.Interfaces;
namespace RAMMS.Repository
{
    public class UserContext : IUserContext
    {
        private readonly HttpContext context = null;
        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            context = httpContextAccessor.HttpContext;
            BindContext();
        }
        public int UserID { get; set; }
        public string UserName { get; private set; }
        private void BindContext()
        {
            var currentUser = context.User;
            if (currentUser != null && currentUser.Identity != null && currentUser.Identity.IsAuthenticated)
            {
                
                this.UserName = currentUser.Identity.Name;
                var claims = currentUser.Claims;                
                this.UserID = Convert.ToInt32(claims.Where(x => x.Type == "UserID").Select(x => x.Value).FirstOrDefault());                
            }
            else
            {
                this.UserName = "Anonymous";
            }
        }
        public string IPAddress
        {
            get
            {
                return context?.Connection?.RemoteIpAddress != null ? context.Connection?.RemoteIpAddress.ToString() : "";                
            }
        }
        public string ActionMessage { get; set; }
    }
}
