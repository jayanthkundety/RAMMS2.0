using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RAMMS.Business.ServiceProvider;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.RequestBO;
using RAMMS.Web.UI.Models;

namespace RAMMS.Web.UI.Controllers
{
    public class InstructedWorks : BaseController
    {

        private readonly IDDLookUpService _ddLookupService;

        private IHostingEnvironment Environment;
        // private readonly ILogger _logger;
        private readonly ISecurity _security;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserService _userService;
        private readonly IFormW2Service _formW2Service;

        FormW2Model _formW2Model = new FormW2Model();

        public InstructedWorks(IWebHostEnvironment webhostenvironment, ISecurity security, IUserService userService, IDDLookUpService ddLookupService, IFormW2Service formW2Service,
        IHostingEnvironment _environment)//, ILogger logger,)
        {
            _ddLookupService = ddLookupService;
            Environment = _environment;
            _webHostEnvironment = webhostenvironment;
            // _logger = logger;
            _userService = userService;
            _formW2Service = formW2Service ?? throw new ArgumentNullException(nameof(formW2Service));
            _security = security;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddFormW1()
        {
            return View("~/Views/InstructedWorks/AddFormW1.cshtml");
        }


        private async Task LoadN2DropDown()
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            ddLookup.Type = "Region";
            ViewData["Region"] = await _ddLookupService.GetDdLookup(ddLookup);

            ddLookup.Type = "Service Provider";
            ddLookup.TypeCode = "SP";
            ViewData["Service Provider"] = await _ddLookupService.GetDdLookup(ddLookup);

            LoadLookupService("RMU", "Division", "RD_Code");

            ddLookup.Type = "Month";
            ViewData["Months"] = await _ddLookupService.GetDdDescValue(ddLookup);

            ViewData["Users"] = _userService.GetUserSelectList(null);

        }
        public async Task<IActionResult> AddFormW2()
        {
            _formW2Model = new FormW2Model();
            _formW2Model.SaveFormW2Model = new DTO.ResponseBO.FormW2ResponseDTO();
            await LoadN2DropDown();
            return View("~/Views/InstructedWorks/AddFormW2.cshtml", _formW2Model);
        }

        public async Task<IActionResult> EditFormW2(int id)
        {
            _formW2Model = new FormW2Model();

            if (id > 0)
            {
                await LoadN2DropDown();
                var result = await _formW2Service.FindW2ByID(id);
                _formW2Model.SaveFormW2Model = result;
            }

            return PartialView("~/Views/InstructedWorks/AddFormW2.cshtml", _formW2Model);
        }

        [HttpPost]
        public async Task<JsonResult> SaveFormW2(Models.FormW2Model formW2)
        {
            int refNo = 0;
            FormW2ResponseDTO saveRequestObj = new FormW2ResponseDTO();
            saveRequestObj = formW2.SaveFormW2Model;
            if (saveRequestObj.PkRefNo == 0)
            refNo = await _formW2Service.Save(formW2.SaveFormW2Model);
            else
                refNo = await _formW2Service.Update(formW2.SaveFormW2Model);

            return Json(refNo);


        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id > 0) { return Ok(new { id = _formW2Service.Delete(id) }); }
            else { return BadRequest("Invalid Request!"); }

        }

    }
}
