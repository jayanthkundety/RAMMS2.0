using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace RAMMS.Web.UI.Controllers
{
    [Route("error")]
    public class AppErrorController : Controller
    {
        [Route("500")]
        public IActionResult InternalServer()
        {
            //var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            //if (exceptionFeature != null)
            //{
            //    ViewBag.ErrorMessage = exceptionFeature.Error.Message;
            //    ViewBag.RouteOfException = exceptionFeature.Path;
            //}

            return View("InternalServer");
        }

        [Route("404")]
        public IActionResult PageNotFound()
        {
            string originalPath = "unknown";
            if (HttpContext.Items.ContainsKey("originalPath"))
            {
                originalPath = HttpContext.Items["originalPath"] as string;
            }
            return View("PageNotFound");
        }

        [Route("401")]
        public IActionResult Unauthorized()
        {
            string originalPath = "unknown";
            if (HttpContext.Items.ContainsKey("originalPath"))
            {
                originalPath = HttpContext.Items["originalPath"] as string;
            }
            return View("Unauthorized");
        }

        [Route("403")]
        public IActionResult Forbidden()
        {
            string originalPath = "unknown";
            if (HttpContext.Items.ContainsKey("originalPath"))
            {
                originalPath = HttpContext.Items["originalPath"] as string;
            }
            return View("Forbidden");
        }
    }
}