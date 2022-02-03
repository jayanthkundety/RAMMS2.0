using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAMMS.Web.UI.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Map/Index.cshtml");
        }
        public IActionResult RoadMap(string roadCode,string section)
        {
            ViewBag.roadCode = roadCode;
            ViewBag.section = section;
            return View("~/Views/Map/Index.cshtml");
        }
    }
}
