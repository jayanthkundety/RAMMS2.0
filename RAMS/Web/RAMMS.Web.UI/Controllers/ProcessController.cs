using Microsoft.AspNetCore.Mvc;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Web.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAMMS.Web.UI.Controllers
{
    [CAuthorize]
    public class ProcessController : Models.BaseController
    {
        private readonly IProcessService _processService;
        private readonly IUserService userService;
        public ProcessController(IProcessService processService, IUserService userSer)
        {
            _processService = processService;
            userService = userSer;
        }
        public IActionResult ViewApprove(string group)
        {
            ViewBag.GroupName = group;
            base.LoadLookupService(group);
            return PartialView("~/Views/Process/ViewApprove.cshtml");
        }
        public async Task<JsonResult> Save(DTO.RequestBO.ProcessDTO process)
        {
            Common.Result<int> result = new Common.Result<int>();
            try
            {
                await _processService.Save(process);
                result.Message = "Sucessfully Saved";
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.IsSuccess = false;
            }
            return Json(result, base.JsonOption());
        }
        public async Task<JsonResult> GetNotification(int Count, DateTime? from)
        {
            return Json(await userService.GetUserNotification(Count, from), JsonOption());
        }
    }
}
