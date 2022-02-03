using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.JQueryModel;
using RAMMS.Web.UI.Models;
using RAMMS.Repository.Audit;

namespace RAMMS.Web.UI.Controllers
{
    
    public class FormFCController : BaseController
    {
        private readonly IFormFCService _fcService;
        private readonly ISecurity _security;
        private readonly IWebHostEnvironment _environment;        
        public FormFCController(IFormFCService fCService, ISecurity security, IWebHostEnvironment environment)
        {
            _fcService = fCService;
            _security = security;
            _environment = environment;            
        }
        public IActionResult Index()
        {
            LoadLookupService("RMU", "Section Code", "Division", "RD_Code", "Year", "User", "Supervisor");
            var grid = new Models.CDataTable() { Name = "tblFCHGrid", APIURL = "/FormFC/HeaderList", LeftFixedColumn = 1 };
            grid.IsModify = _security.IsPCModify(ModuleNameList.Condition_Inspection);
            grid.IsDelete = _security.IsPCDelete(ModuleNameList.Condition_Inspection);
            grid.IsView = _security.IsPCView(ModuleNameList.Condition_Inspection);
            grid.Columns.Add(new CDataColumns() { data = null, title = "Action", IsFreeze = true, sortable = false, render = "formFC.HeaderGrid.ActionRender" });
            grid.Columns.Add(new CDataColumns() { data = "RefID", title = "Reference No" });
            grid.Columns.Add(new CDataColumns() { data = "Year", title = "Year of Inspection" });
            grid.Columns.Add(new CDataColumns() { data = "InsDate", title = "Date of Inspection", render = "formFC.HeaderGrid.DateOfEntry" });
            grid.Columns.Add(new CDataColumns() { data = "InspectedBy", title = "Inspected By" });
            grid.Columns.Add(new CDataColumns() { data = "RMUCode", title = "RMU" });
            grid.Columns.Add(new CDataColumns() { data = "RMUDesc", title = "RMU Name" });
            grid.Columns.Add(new CDataColumns() { data = "SecCode", title = "Section Code" });
            grid.Columns.Add(new CDataColumns() { data = "SecName", title = "Section Name" });
            grid.Columns.Add(new CDataColumns() { data = "RoadCode", title = "Road Code" });
            grid.Columns.Add(new CDataColumns() { data = "RoadName", title = "Road Name" });
            grid.Columns.Add(new CDataColumns() { data = "CrewLeader", title = "Crew Leader" });
            grid.Columns.Add(new CDataColumns() { data = "RoadId", title = "Road ID", visible = false });            
            grid.columnDefs = new List<CDataColumnDefs>();
            grid.columnDefs.Add(new CDataColumnDefs("1", "12,2,3"));
            grid.columnDefs.Add(new CDataColumnDefs("2", "2,3"));
            grid.columnDefs.Add(new CDataColumnDefs("9", "12"));
            return View(grid);
        }
        public async Task<JsonResult> HeaderList(DataTableAjaxPostModel searchData)
        {            
            return Json(await _fcService.GetHeaderGrid(searchData), JsonOption());
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
            DTO.ResponseBO.FormFCDTO frmFC = null;
            if (id > 0)
            {
                ViewBag.IsAdd = false;

                frmFC = _fcService.FindByHeaderID(id).Result;
            }
            LoadLookupService("RMU", "Section Code", "Division", "RD_Code", "Year", "User", "Supervisor");
            return View("~/Views/FormFC/_AddFormFC.cshtml", frmFC);
        }
        public async Task<IActionResult> FindDetails(DTO.ResponseBO.FormFCDTO frmFC)
        {
            try
            {
                return Json(await _fcService.FindDetails(frmFC, _security.UserID), JsonOption());
            }
            catch (Exception ex)
            {
                return Json(new { _error = ex.Message }, JsonOption());
            }
        }
        [HttpPost]
        [RequestFormLimits(ValueCountLimit = int.MaxValue)]
        [Audit(ActionMessage = "Save Form FC")]
        public async Task<JsonResult> Save(DTO.ResponseBO.FormFCDTO frmFC)
        {
            return await SaveAll(frmFC, false);
        }
        [HttpPost]
        [RequestFormLimits(ValueCountLimit = int.MaxValue)]        
        public async Task<JsonResult> Submit(DTO.ResponseBO.FormFCDTO frmFC)
        {
            frmFC.SubmitSts = true;
            return await SaveAll(frmFC, true);
        }
        private async Task<JsonResult> SaveAll(DTO.ResponseBO.FormFCDTO frmFC, bool updateSubmit)
        {
            frmFC.CrBy = _security.UserID;
            frmFC.ModBy = _security.UserID;
            frmFC.ModDt = DateTime.UtcNow;
            frmFC.CrDt = DateTime.UtcNow;
            var result = await _fcService.Save(frmFC, updateSubmit);
            return Json(new { RefNo = result.FormRefId, Id = result.PkRefNo }, JsonOption());
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id > 0) { return Ok(new { id = _fcService.Delete(id) }); }
            else { return BadRequest("Invalid Request!"); }

        }

        [HttpGet]
        public IActionResult Download(int id)
        {
            var content1 = _fcService.FormDownload("FORMFC", id, _environment.WebRootPath + "/Templates/FormFC.xlsx");
            string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(content1, contentType1, "FORMFC" + ".xlsx");
        }
    }
}