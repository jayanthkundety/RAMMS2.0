﻿using Microsoft.AspNetCore.Hosting;
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
        private readonly IRoadMasterService _roadMasterService;
        private readonly IDDLookupBO _dDLookupBO;

        FormQa1Model _formQa1Model = new FormQa1Model();
        public FormQA1Controller(IHostingEnvironment _environment,
            IDDLookupBO _ddLookupBO,
            IDDLookUpService ddLookupService,
            IUserService userService,
            IWebHostEnvironment webhostenvironment,
            IFormN1Service formN1Service,
            IFormJServices formJServices,
            ISecurity security,
            IRoadMasterService roadMasterService,
            ILogger logger,
            IFormQa1Service formQa1Service)
        {
            _userService = userService;
            _dDLookupBO = _ddLookupBO;
            Environment = _environment;
            _webHostEnvironment = webhostenvironment;
            _ddLookupService = ddLookupService;
            _security = security;
            _formN1Service = formN1Service ?? throw new ArgumentNullException(nameof(formN1Service));
            _formJService = formJServices ?? throw new ArgumentNullException(nameof(formJServices));
            _formQa1Service = formQa1Service ?? throw new ArgumentNullException(nameof(formQa1Service));
            _roadMasterService = roadMasterService ?? throw new ArgumentNullException(nameof(roadMasterService));
            _logger = logger;
        }

        private List<SelectListItem> DDLYESNO()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Yes", Value = "Yes" });
            list.Add(new SelectListItem { Text = "No", Value = "No" });
            return list;
        }

        private List<SelectListItem> DDLWELLNO()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Well", Value = "Well" });
            list.Add(new SelectListItem { Text = "No", Value = "No" });
            return list;
        }


        private List<SelectListItem> DDLADENO()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Adequate", Value = "Adequate" });
            list.Add(new SelectListItem { Text = "No", Value = "No" });
            return list;
        }


        private List<SelectListItem> DDLSTHD()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Soft", Value = "Soft" });
            list.Add(new SelectListItem { Text = "Hard", Value = "Hard" });
            return list;
        }



        private async Task LoadDropDown()
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();

            ViewData["RD_Code"] = await _formN1Service.GetRoadCodesByRMU("");
            //ViewData["RMU"] = await _formN1Service.GetRMU();

            ddLookup.Type = "ACT-CWF";
            ViewData["Activity"] = await _ddLookupService.GetLookUpCodeTextConcat(ddLookup);

            LoadLookupService("User", "RMU");

            ddLookup.Type = "Week No";
            ViewData["WeekNo"] = await _ddLookupService.GetDdDescValue(ddLookup);

            FormASearchDropdown ddl = _formJService.GetDropdown(new RequestDropdownFormA { });

            ViewData["SectionCode"] = ddl.Section.Select(s => new SelectListItem { Text = s.Text, Value = s.Value }).ToArray();

            ddLookup.Type = "Year";
            var year = await _ddLookupService.GetDdLookup(ddLookup);
            ViewData["Year"] = year.Select(l => new SelectListItem { Selected = (l.Value == DateTime.Today.Year.ToString()), Text = l.Text, Value = l.Value });

            ddLookup.Type = "Day";
            var day = await _ddLookupService.GetDdLookup(ddLookup);
            ViewData["Day"] = day.Select(l => new SelectListItem { Selected = (l.Value == DateTime.Now.DayOfWeek.ToString()), Text = l.Text, Value = l.Value });

            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            var weekno = cal.GetWeekOfYear(DateTime.Today, dfi.CalendarWeekRule,
                                         dfi.FirstDayOfWeek);

            var weekNoLst = _dDLookupBO.GetWeekNo();
            ViewData["WeekNo"] = weekNoLst.Select(l => new SelectListItem { Selected = (l.Value == weekno.ToString()), Text = l.Text, Value = l.Value });

            ViewData["YESNO"] = DDLYESNO();

            ViewData["WELLNO"] = DDLWELLNO();

            ViewData["ADENO"] = DDLADENO();

            ViewData["STHD"] = DDLSTHD();
        }

        public async Task<IActionResult> QA1()
        {
            await LoadDropDown();
            return View("~/Views/MAM/FormQa1/FormQa1.cshtml");
        }

        [HttpPost]
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

        public async Task<IActionResult> EditFormQa1()
        {
            await LoadDropDown();
            _formQa1Model.SaveFormQa1Model = new FormQa1HeaderDTO();
            _formQa1Model.SaveFormQa1Model.Labour = new List<FormQa1LabDTO>();
            _formQa1Model.SaveFormQa1Model.Labour.Add(new FormQa1LabDTO { Labour = "Crew Supervisor" });
            _formQa1Model.SaveFormQa1Model.Labour.Add(new FormQa1LabDTO { Labour = "Operators" });
            _formQa1Model.SaveFormQa1Model.Labour.Add(new FormQa1LabDTO { Labour = "Drivers" });
            _formQa1Model.SaveFormQa1Model.Labour.Add(new FormQa1LabDTO { Labour = "Workmates" });
            _formQa1Model.SaveFormQa1Model.Labour.Add(new FormQa1LabDTO { Labour = "Others" });

            _formQa1Model.SaveFormQa1Model.WorkCompletionQuality = new FormQa1WcqDTO();
            _formQa1Model.SaveFormQa1Model.WorkExecution = new FormQa1WeDTO();
            _formQa1Model.SaveFormQa1Model.Equipment_Vehicle = new FormQa1EqVhDTO();
            _formQa1Model.SaveFormQa1Model.Material = new FormQa1MatDTO();
            _formQa1Model.SaveFormQa1Model.SpecificSiteCondition = new FormQa1SscDTO();
            _formQa1Model.SaveFormQa1Model.GeneralComments = new FormQa1GCDTO();
            _formQa1Model.SaveFormQa1Model.GeneralQA1 = new FormQa1GenDTO();


            return View("~/Views/MAM/FormQa1/_AddFormQA1.cshtml", _formQa1Model);
        }


        [HttpPost]
        public IActionResult GetRoadCodeBySection(int secCode)
        {
            var obj = _roadMasterService.GetRoadCodeBySectionCode(secCode).Result;
            return Json(obj);
        }

        [HttpPost]
        public async Task<JsonResult> FindDetails(FormQa1Model header, bool create = false)
        {
            FormQa1HeaderDTO formQa1 = new FormQa1HeaderDTO();
            FormQa1HeaderDTO formQa1Res = new FormQa1HeaderDTO();

            formQa1 = header.SaveFormQa1Model;
            formQa1.Dt = header.FormQa1Date;

            formQa1Res = await _formQa1Service.FindQa1Details(formQa1);
            //Qa1 Not Exisit
            if (formQa1Res == null)
            {
                formQa1.Dt = header.FormQa1Date;
                formQa1.UseridAssgn = _security.UserID;
                formQa1.UsernameAssgn = _security.UserName;
                formQa1.DtAssgn = DateTime.Today;
                formQa1.CrBy = _security.UserID;
                formQa1.ModBy = _security.UserID;
                formQa1.ModDt = DateTime.Now;
                formQa1.CrDt = DateTime.Now;
                formQa1Res = await _formQa1Service.FindAndSaveFormQA1Hdr(formQa1, false);

                if (formQa1Res.PkRefNo > 0)
                {
                    await _formQa1Service.InsertLabourDetails(formQa1Res.PkRefNo);
                }
            }
            return Json(formQa1Res, JsonOption());
        }

        [HttpPost]
        public async Task<IActionResult> LoadFormQa1EquipmentList(DataTableAjaxPostModel<FormQa1SearchGridDTO> formQa1Filter, int id)
        {

            FilteredPagingDefinition<FormQa1SearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormQa1SearchGridDTO>();
            filteredPagingDefinition.RecordsPerPage = formQa1Filter.length;
            filteredPagingDefinition.StartPageNo = formQa1Filter.start;

            var result = await _formQa1Service.GetEquipmentFormQa1Grid(filteredPagingDefinition, id).ConfigureAwait(false);

            return Json(new { draw = formQa1Filter.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }

        [HttpPost]
        public async Task<IActionResult> LoadFormQa1GenList(DataTableAjaxPostModel<FormQa1SearchGridDTO> formQa1Filter, int id)
        {

            FilteredPagingDefinition<FormQa1SearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormQa1SearchGridDTO>();
            filteredPagingDefinition.RecordsPerPage = formQa1Filter.length;
            filteredPagingDefinition.StartPageNo = formQa1Filter.start;

            var result = await _formQa1Service.GetGeneralFormQa1Grid(filteredPagingDefinition, id).ConfigureAwait(false);

            return Json(new { draw = formQa1Filter.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }



        [HttpPost]
        public async Task<IActionResult> LoadFormQa1MaterialList(DataTableAjaxPostModel<FormQa1SearchGridDTO> formQa1Filter, int id)
        {

            FilteredPagingDefinition<FormQa1SearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormQa1SearchGridDTO>();
            filteredPagingDefinition.RecordsPerPage = formQa1Filter.length;
            filteredPagingDefinition.StartPageNo = formQa1Filter.start;

            var result = await _formQa1Service.GetMaterialFormQa1Grid(filteredPagingDefinition, id).ConfigureAwait(false);

            return Json(new { draw = formQa1Filter.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }

    }
}
