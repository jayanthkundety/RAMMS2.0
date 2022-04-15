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

namespace RAMMS.Web.UI.Controllers
{
    public class FormQA1Controller : Models.BaseController
    {
        private IHostingEnvironment Environment;
        private readonly ILogger _logger;
        private readonly IDDLookUpService _ddLookupService;
        private readonly ISecurity _security;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFormN1Service _formN1Service;
        private readonly IFormJServices _formJService;
        private readonly IFormQa1Service _formQa1Service;

        FormQa1Model _formQa1Model = new FormQa1Model();
        public FormQA1Controller(IHostingEnvironment _environment,
            IDDLookUpService ddLookupService,
            IUserService userService,
            IWebHostEnvironment webhostenvironment,
            IFormN1Service formN1Service,
            IFormJServices formJServices,
            ISecurity security,
            ILogger logger,
            IFormQa1Service formQa1Service)
        {
            _userService = userService;
            Environment = _environment;
            _webHostEnvironment = webhostenvironment;
            _ddLookupService = ddLookupService;
            _security = security;
            _formN1Service = formN1Service ?? throw new ArgumentNullException(nameof(formN1Service));
            _formJService = formJServices ?? throw new ArgumentNullException(nameof(formJServices));
            _formQa1Service = formQa1Service ?? throw new ArgumentNullException(nameof(formQa1Service));
            _logger = logger;
        }
        public  async Task<IActionResult> QA1()
        {

            DDLookUpDTO ddLookup = new DDLookUpDTO();
            ddLookup.Type = "Other Follow Up Action";
            ViewData["Other Follow Up Action"] = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "RMU";
            ViewData["RMU"] = await _formN1Service.GetRMU();

            ddLookup.Type = "Act-FormD";
            ViewData["Activity"] = await _ddLookupService.GetLookUpCodeTextConcat(ddLookup);
            ViewData["RD_Code"] = await _formN1Service.GetRoadCodesByRMU("");
            LoadLookupService("User");

            FormASearchDropdown ddl = _formJService.GetDropdown(new RequestDropdownFormA { });

            ViewData["SectionCode"] = ddl.Section.Select(s => new SelectListItem { Text = s.Text, Value = s.Value }).ToArray();
            return View("~/Views/MAM/FormQa1/FormQa1.cshtml");
        }

        public async Task<IActionResult> LoadFormQa1List(DataTableAjaxPostModel<FormQa1SearchGridDTO> formQa1Filter)
        {

            if (Request.Form.ContainsKey("columns[0][search][value]"))
            {
                formQa1Filter.filterData.SmartInputValue = Request.Form["columns[0][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[1][search][value]"))
            {
                formQa1Filter.filterData.RMU = Request.Form["columns[1][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[2][search][value]"))
            {
                formQa1Filter.filterData.Section = Request.Form["columns[2][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[3][search][value]"))
            {
                formQa1Filter.filterData.Crew = Request.Form["columns[3][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[4][search][value]"))
            {
                formQa1Filter.filterData.ActivityCode = Request.Form["columns[4][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[5][search][value]"))
            {
                formQa1Filter.filterData.ByFromdate = Request.Form["columns[5][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[6][search][value]"))
            {
                formQa1Filter.filterData.ByTodate = Request.Form["columns[6][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[7][search][value]"))
            {
                formQa1Filter.filterData.RoadCode = Request.Form["columns[7][search][value]"].ToString();
            }

            FilteredPagingDefinition<FormQa1SearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormQa1SearchGridDTO>();
            filteredPagingDefinition.Filters = formQa1Filter.filterData;
            filteredPagingDefinition.RecordsPerPage = formQa1Filter.length;
            filteredPagingDefinition.StartPageNo = formQa1Filter.start;

            if (formQa1Filter.order != null)
            {
                filteredPagingDefinition.ColumnIndex = formQa1Filter.order[0].column;
                filteredPagingDefinition.sortOrder = formQa1Filter.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }

            var result = await _formQa1Service.GetFilteredFormQa1Grid(filteredPagingDefinition).ConfigureAwait(false);

            return Json(new { draw = formQa1Filter.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }


        public IActionResult EditFormQa1()
        {
            return View("~/Views/MAM/FormQa1/_AddFormQA1.cshtml");
        }
    }
}
