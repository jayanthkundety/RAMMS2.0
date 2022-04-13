using Microsoft.AspNetCore.Mvc;

namespace RAMMS.Web.UI.Controllers
{
    public class FormQA1Controller : Controller
    {
        public IActionResult QA1()
        {
            return View("~/Views/MAM/FormQa1/FormQa1.cshtml");
        }


        public IActionResult AddQA1()
        {
            return View();
        }
    }
}
