using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAMMS.Web.UI.Controllers
{
    public class IWController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
