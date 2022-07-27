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
    public class FormMController : Models.BaseController
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
        private readonly IFormMService _formMService;
        private readonly IAssetsService _AssetService;

        public FormMController(IHostingEnvironment _environment,
           IDDLookupBO _ddLookupBO,
           IDDLookUpService ddLookupService,
           IUserService userService,
           IWebHostEnvironment webhostenvironment,
           IFormN1Service formN1Service,
           IFormJServices formJServices,
           ISecurity security,
           IRoadMasterService roadMasterService,
           ILogger logger,
           IFormMService formMService,
           IAssetsService assestService)
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
            _formMService = formMService ?? throw new ArgumentNullException(nameof(formMService));
            _AssetService = assestService;
        }

        private async Task LoadDropDown()
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();

            ViewData["RD_Code"] = await _formN1Service.GetRoadCodesByRMU("");
            //ViewData["RMU"] = await _formN1Service.GetRMU();

            ddLookup.Type = "ACT-QA1";
            ViewData["Activity"] = await _ddLookupService.GetLookUpCodeTextConcat(ddLookup);
            //_ddLookupService.GetDdDescValue

            LoadLookupService("User", "RMU", "Year", "Section Code");

            ddLookup.Type = "Week No";
            ViewData["WeekNo"] = await _ddLookupService.GetDdDescValue(ddLookup);

            //FormASearchDropdown ddl = _formJService.GetDropdown(new RequestDropdownFormA { });
        }

        public async Task<IActionResult> Index()
        {
            await LoadDropDown();
            var grid = new Models.CDataTable() { Name = "tblFMHGrid", APIURL = "/FormM/HeaderList", LeftFixedColumn = 1 };
            grid.IsModify = _security.IsPCModify(ModuleNameList.Condition_Inspection);
            grid.IsDelete = _security.IsPCDelete(ModuleNameList.Condition_Inspection);
            grid.IsView = _security.IsPCView(ModuleNameList.Condition_Inspection);
            grid.Columns.Add(new CDataColumns() { data = null, title = "Action", IsFreeze = true, sortable = false, render = "frmM.HeaderGrid.ActionRender" });
            grid.Columns.Add(new CDataColumns() { data = "RefID", title = "Reference No" });
            grid.Columns.Add(new CDataColumns() { data = "AuditDate", title = "Audit Date", render = "frmM.HeaderGrid.AuditDate" });
            grid.Columns.Add(new CDataColumns() { data = "RMUCode", title = "RMU" });
            grid.Columns.Add(new CDataColumns() { data = "RMUDesc", title = "RMU Name" });
            grid.Columns.Add(new CDataColumns() { data = "RoadCode", title = "Road Code" });
            grid.Columns.Add(new CDataColumns() { data = "RoadName", title = "Road Name" });
            grid.Columns.Add(new CDataColumns() { data = "ActivityCode", title = "Activity Code" });
            grid.Columns.Add(new CDataColumns() { data = "ClosureType", title = "Type of Closure" });
            grid.Columns.Add(new CDataColumns() { data = "Method", title = "Method" });
            grid.Columns.Add(new CDataColumns() { data = "ProcessStatus", title = "Status" });
            grid.Columns.Add(new CDataColumns() { data = "AuditedBy", title = "Audited By" });
            grid.Columns.Add(new CDataColumns() { data = "WitnessedBy", title = "Witnessed(1) By" });
            return View(grid);
        }

        public async Task<IActionResult> FindDetails(DTO.ResponseBO.FormMDTO frmR1)
        {
            return Json(await _formMService.FindDetails(frmR1, _security.UserID), JsonOption());
        }

        public async Task<JsonResult> HeaderList(DataTableAjaxPostModel searchData)
        {
            if (searchData.order != null && searchData.order.Count > 0)
            {
                searchData.order = searchData.order.Select(x => { if (x.column == 4 || x.column == 1 || x.column == 9) { x.column = 16; } return x; }).ToList();
            }
            return Json(await _formMService.GetHeaderGrid(searchData), JsonOption());
        }

        public IActionResult Add()
        {
            ViewBag.IsAdd = true;
            ViewBag.IsEdit = true;
            return ViewRequest(0);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.IsEdit = true;
            return id > 0 ? ViewRequest(id) : RedirectToAction("404", "Error");
        }
        public IActionResult View(int id)
        {
            ViewBag.IsEdit = false;
            return id > 0 ? ViewRequest(id) : RedirectToAction("404", "Error");
        }
        private IActionResult ViewRequest(int id)
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            LoadLookupService("Year", "User", "Photo Type~RWG");
            ViewData["DistressObserved1"] = _ddLookupService.GetDdDistressDetails().Result;
            ddLookup.Type = "ACT-QA1";
            ViewData["ActivityCode"] = _ddLookupService.GetLookUpCodeTextConcat(ddLookup).Result;
            ViewBag.Dis_Severity = LookupService.GetDdlLookupByCode("Form C1C2");
            FormMDTO frmR1R2 = new FormMDTO();
            if (id > 0)
            {
                ViewBag.IsAdd = false;
                frmR1R2 = _formMService.FindByHeaderID(id).Result;

                if (frmR1R2.SubmitSts && frmR1R2.status == Common.StatusList.FormMSubmitted)
                {
                    frmR1R2.UseridAudit = _security.UserID;
                    frmR1R2.DateAudit = DateTime.Today;
                }
                //else if (frmR1R2.SubmitSts && frmR1R2.status == Common.StatusList.FormMVerified)
                //{
                //    frmR1R2.UseridWit = _security.UserID;
                //    frmR1R2.DateWit = DateTime.Today;
                //}
                //else if (frmR1R2.SubmitSts && frmR1R2.status == Common.StatusList.FormMApproved)
                //{
                //    frmR1R2.UseridWitone = _security.UserID;
                //    frmR1R2.DateWitone = DateTime.Today;
                //}
            }
            else
            {
                LoadLookupService("RMU", "Section Code", "Division", "RD_Code");
                ViewData["AssetIds"] = _AssetService.ListOfReatiningWallAssestIds().Result;
            }
            LoadLookupService("Road_Master", "Act-FormM", "User", GroupNameList.Supervisor, "FS1-StatusLegend");
            return View("~/Views/FormM/_AddFormM.cshtml", frmR1R2);
        }

        [HttpPost]
        public async Task<JsonResult> Save(DTO.ResponseBO.FormMDTO frmR1R2)
        {
            return await SaveAll(frmR1R2, false);
        }
        [HttpPost]
        public async Task<JsonResult> Submit(DTO.ResponseBO.FormMDTO frmR1R2)
        {
            frmR1R2.SubmitSts = true;
            frmR1R2.UseridAudit = _security.UserID;
            frmR1R2.DateAudit = DateTime.Today;
            return await SaveAll(frmR1R2, true);
        }
        private async Task<JsonResult> SaveAll(DTO.ResponseBO.FormMDTO frmR1R2, bool updateSubmit)
        {
            frmR1R2.CrBy = _security.UserID;
            frmR1R2.ModBy = _security.UserID;
            frmR1R2.ModDt = DateTime.UtcNow;
            frmR1R2.CrDt = DateTime.UtcNow;
            frmR1R2.FormMAD.FmhPkRefNo = frmR1R2.PkRefNo;
            frmR1R2.SrProvider = "Endaya Construction";
            frmR1R2.FormMAD.Total = ((frmR1R2.FormMAD.A1total == null ? 0 : frmR1R2.FormMAD.A1total) + (frmR1R2.FormMAD.A2total == null ? 0 : frmR1R2.FormMAD.A2total) + (frmR1R2.FormMAD.A3total == null ? 0 : frmR1R2.FormMAD.A3total) + (frmR1R2.FormMAD.A4total == null ? 0 : frmR1R2.FormMAD.A4total) + (frmR1R2.FormMAD.A5total == null ? 0 : frmR1R2.FormMAD.A5total) + (frmR1R2.FormMAD.A6total == null ? 0 : frmR1R2.FormMAD.A6total) + (frmR1R2.FormMAD.A7total == null ? 0 : frmR1R2.FormMAD.A7total) + (frmR1R2.FormMAD.A8total == null ? 0 : frmR1R2.FormMAD.A8total) + (frmR1R2.FormMAD.B1total == null ? 0 : frmR1R2.FormMAD.B1total) + (frmR1R2.FormMAD.B2total == null ? 0 : frmR1R2.FormMAD.B2total) + (frmR1R2.FormMAD.B3total == null ? 0 : frmR1R2.FormMAD.B3total) + (frmR1R2.FormMAD.B4total == null ? 0 : frmR1R2.FormMAD.B4total) + (frmR1R2.FormMAD.B5total == null ? 0 : frmR1R2.FormMAD.B5total) + (frmR1R2.FormMAD.B6total == null ? 0 : frmR1R2.FormMAD.B6total) + (frmR1R2.FormMAD.B7total == null ? 0 : frmR1R2.FormMAD.B7total) + (frmR1R2.FormMAD.B8total == null ? 0 : frmR1R2.FormMAD.B8total) + (frmR1R2.FormMAD.B9total == null ? 0 : frmR1R2.FormMAD.B9total) + (frmR1R2.FormMAD.C1total == null ? 0 : frmR1R2.FormMAD.C1total) + (frmR1R2.FormMAD.C2total == null ? 0 : frmR1R2.FormMAD.C2total) + (frmR1R2.FormMAD.D1total == null ? 0 : frmR1R2.FormMAD.D1total) + (frmR1R2.FormMAD.D2total == null ? 0 : frmR1R2.FormMAD.D2total) + (frmR1R2.FormMAD.D3total == null ? 0 : frmR1R2.FormMAD.D3total) + (frmR1R2.FormMAD.D4total == null ? 0 : frmR1R2.FormMAD.D4total) + (frmR1R2.FormMAD.D5total == null ? 0 : frmR1R2.FormMAD.D5total) + (frmR1R2.FormMAD.D6total == null ? 0 : frmR1R2.FormMAD.D6total) + (frmR1R2.FormMAD.D7total == null ? 0 : frmR1R2.FormMAD.D7total) + (frmR1R2.FormMAD.D8total == null ? 0 : frmR1R2.FormMAD.D8total) + (frmR1R2.FormMAD.E1total == null ? 0 : frmR1R2.FormMAD.E1total) + (frmR1R2.FormMAD.E2total == null ? 0 : frmR1R2.FormMAD.E2total) + (frmR1R2.FormMAD.F1total == null ? 0 : frmR1R2.FormMAD.F1total) + (frmR1R2.FormMAD.F2total == null ? 0 : frmR1R2.FormMAD.F2total) + (frmR1R2.FormMAD.F3total == null ? 0 : frmR1R2.FormMAD.F3total) + (frmR1R2.FormMAD.F4total == null ? 0 : frmR1R2.FormMAD.F4total) + (frmR1R2.FormMAD.F5total == null ? 0 : frmR1R2.FormMAD.F5total) + (frmR1R2.FormMAD.F6total == null ? 0 : frmR1R2.FormMAD.F6total) + (frmR1R2.FormMAD.F7total == null ? 0 : frmR1R2.FormMAD.F7total) + (frmR1R2.FormMAD.G1total == null ? 0 : frmR1R2.FormMAD.G1total) + (frmR1R2.FormMAD.G2total == null ? 0 : frmR1R2.FormMAD.G2total) + (frmR1R2.FormMAD.G3total == null ? 0 : frmR1R2.FormMAD.G3total) + (frmR1R2.FormMAD.G4total == null ? 0 : frmR1R2.FormMAD.G4total) + (frmR1R2.FormMAD.G5total == null ? 0 : frmR1R2.FormMAD.G5total) + (frmR1R2.FormMAD.G6total == null ? 0 : frmR1R2.FormMAD.G6total) + (frmR1R2.FormMAD.G7total == null ? 0 : frmR1R2.FormMAD.G7total) + (frmR1R2.FormMAD.G8total == null ? 0 : frmR1R2.FormMAD.G8total) + (frmR1R2.FormMAD.G9total == null ? 0 : frmR1R2.FormMAD.G9total) + (frmR1R2.FormMAD.G10total == null ? 0 : frmR1R2.FormMAD.G10total));
            var result = await _formMService.Save(frmR1R2, updateSubmit);
            return Json(new { Id = result.PkRefNo }, JsonOption());
        }
        [HttpPost] //Tab
        public IActionResult Delete(int id)
        {
            if (id > 0) { return Ok(new { id = _formMService.Delete(id) }); }
            else { return BadRequest("Invalid Request!"); }

        }
        public IActionResult Download(int id)
        {
            var content1 = _formMService.FormDownload("FormM", id, _webHostEnvironment.WebRootPath, _webHostEnvironment.WebRootPath + "/Templates/FormM.xlsx");
            string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(content1, contentType1, "FormM" + ".xlsx");
        }
    }
}
