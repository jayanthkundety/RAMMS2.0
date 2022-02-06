using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RAMMS.Business.ServiceProvider;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.RequestBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RAMMS.Web.UI.Models;

namespace RAMMS.Web.UI.Controllers
{
    public class InstructedWorks : Models.BaseController
    {
        private readonly IFormW1Service _formW1Service;
        private readonly IDDLookUpService _ddLookupService;
        private IHostingEnvironment Environment;
        //  private readonly ILogger _logger;
        private readonly ISecurity _security;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserService _userService;

        public InstructedWorks(IWebHostEnvironment webhostenvironment, ISecurity security, IUserService userService, IDDLookUpService ddLookupService,

       IHostingEnvironment _environment, IFormW1Service formW1Service)
        {
            _ddLookupService = ddLookupService;
            Environment = _environment;
            _webHostEnvironment = webhostenvironment;
            _userService = userService;
            _formW1Service = formW1Service ?? throw new ArgumentNullException(nameof(formW1Service));
            _security = security;
            //  _logger = logger;

        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddFormW1()
        {
            FormW1Model model = new FormW1Model();
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            ddLookup.Type = "Month";
            ViewData["Months"] = await _ddLookupService.GetDdDescValue(ddLookup);
            LoadLookupService("RMU", "Division", "RD_Code", "User");
            return View("~/Views/InstructedWorks/AddFormW1.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveFormW1(FormW1Model frm)
        {

            var result = await _formW1Service.SaveFormW1(frm);
            return Json("");
        }

        public IActionResult AddFormW2()
        {
            return View("~/Views/InstructedWorks/AddFormW2.cshtml");
        }
    }
}
