using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.Web.UI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO;
using RAMMS.DTO.Wrappers;
using System.Collections.Generic;
using RAMMS.Business.ServiceProvider;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using System.IO;
namespace RAMMS.Web.UI.Controllers
{
    public class FormG1G2Controller : Models.BaseController
    {

        private IHostingEnvironment Environment;
        private readonly ILogger _logger;
        private readonly IDDLookUpService _ddLookupService;
        private readonly ISecurity _security;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFormN1Service _formN1Service;
        private readonly IFormJServices _formJService;
        private readonly IRoadMasterService _roadMasterService;
        private readonly IDDLookupBO _dDLookupBO;
        private readonly IFormG1G2Service _formG1G2Service;

        public FormG1G2Controller(IHostingEnvironment _environment,
           IDDLookupBO _ddLookupBO,
           IDDLookUpService ddLookupService,
           IUserService userService,
           IWebHostEnvironment webhostenvironment,
           IFormN1Service formN1Service,
           IFormJServices formJServices,
           ISecurity security,
           IRoadMasterService roadMasterService,
           ILogger logger,
           IFormG1G2Service formG1G2Service
           )
        {
            _userService = userService;
            _dDLookupBO = _ddLookupBO;
            Environment = _environment;
            _webHostEnvironment = webhostenvironment;
            _ddLookupService = ddLookupService;
            _security = security;
            _formN1Service = formN1Service ?? throw new ArgumentNullException(nameof(formN1Service));
            _formJService = formJServices ?? throw new ArgumentNullException(nameof(formJServices));
            _roadMasterService = roadMasterService ?? throw new ArgumentNullException(nameof(roadMasterService));
            _logger = logger;
            _formG1G2Service = formG1G2Service ?? throw new ArgumentNullException(nameof(formG1G2Service));
        }

        private async Task LoadDropDown()
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();

            ViewData["RD_Code"] = await _formN1Service.GetRoadCodesByRMU("");
            //ViewData["RMU"] = await _formN1Service.GetRMU();

            ddLookup.Type = "ACT-QA1";
            ViewData["Activity"] = await _ddLookupService.GetLookUpCodeTextConcat(ddLookup);

            LoadLookupService("User", "RMU", "Year");

            ddLookup.Type = "Week No";
            ViewData["WeekNo"] = await _ddLookupService.GetDdDescValue(ddLookup);

            FormASearchDropdown ddl = _formJService.GetDropdown(new RequestDropdownFormA { });

            ViewData["SectionCode"] = ddl.Section.Select(s => new SelectListItem { Text = s.Text, Value = s.Value }).ToArray();

           

        }
        public async Task<IActionResult> Index()
        {
            await LoadDropDown();
            return View();
        }
    }
}
