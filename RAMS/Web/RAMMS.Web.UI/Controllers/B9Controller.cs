using Microsoft.AspNetCore.Mvc;
using RAMMS.Web.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAMMS.Web.UI.Controllers
{
    public class B9Controller : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
