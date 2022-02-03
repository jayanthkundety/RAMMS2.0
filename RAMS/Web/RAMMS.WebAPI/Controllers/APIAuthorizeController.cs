using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Repository.Interfaces;

namespace RAMMS.WebAPI.Controllers
{
    
    [ApiController]
    public class APIAuthorizeController : ControllerBase
    {
        private readonly ISecurity security;
        private readonly IModuleRepository moduleRights;
        public APIAuthorizeController(ISecurity iSecurity, IModuleRepository ModuleRightsRepo)
        {
            security = iSecurity;
            moduleRights = ModuleRightsRepo;
        }

        [Authorize]
        [HttpGet]
        [Route("api/GetUserDetails")]
        public IActionResult GetUserDetails()
        {
            return Ok(new
            {
                Username = security.UserName,
                Email = security.Email,
                Group = security.Group,
                Module = security.Module,
                AllFieldRights = security.FieldRights(),
                //AllGroups = security.AllGroups(),
                ModuleRights = security.ModuleRights()
            });


        }
        
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetMasterData()
        {
            return Ok(new
            {
                FieldRights = security.FieldRights(),
                Groups = security.AllGroups(),
                ModuleRights = security.ModuleRights()
            });
        }
    }
}
