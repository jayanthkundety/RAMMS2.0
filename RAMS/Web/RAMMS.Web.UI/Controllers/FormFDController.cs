using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.Web.UI.Models;

namespace RAMMS.Web.UI.Controllers
{
    [CAuthorize(ModuleName = ModuleNameList.Condition_Inspection)]
    public class FormFDController : Models.BaseController
    {
        private IFormFDService _formFDService;
        private ISecurity _security;
        private IDDLookUpService _dDLookUpService;
        private readonly IWebHostEnvironment _environment;
        public FormFDController(IWebHostEnvironment environment, IFormFDService formFDService, ISecurity security, IDDLookUpService dDLookUpService)
        {
            _formFDService = formFDService;
            _environment = environment;
            _security = security;
            _dDLookUpService = dDLookUpService;
        }
        public IActionResult Index()
        {
            LoadLookupService("RMU", "RD_Code", "Section Code", "Bound", "Asset Type", "Year", "User", "Supervisor", "FormFD_Assets", "Supervisor");


            LoadLookupService("RMU", "Section Code", "Division", "RD_Code", "Year", "User");
            var grid = new Models.CDataTable() { Name = "tblFDHeaderGrid", APIURL = "/FormFD/HeaderList", LeftFixedColumn = 1 };
            grid.IsModify = _security.IsPCModify(ModuleNameList.Condition_Inspection);
            grid.IsDelete = _security.IsPCDelete(ModuleNameList.Condition_Inspection);
            grid.IsView = _security.IsPCView(ModuleNameList.Condition_Inspection);
            grid.Columns.Add(new CDataColumns() { data = null, title = "Action", IsFreeze = true, sortable = false, render = "formFD.HeaderGrid.ActionRender" });
            grid.Columns.Add(new CDataColumns() { data = "RefID", title = "Reference No" });
            grid.Columns.Add(new CDataColumns() { data = "Year", title = "Year of Inspection" });
            grid.Columns.Add(new CDataColumns() { data = "InsDate", title = "Date of Inspection", render = "formFD.HeaderGrid.DateOfIns" });
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
            grid.columnDefs.Add(new CDataColumnDefs("1", "12,2"));
            grid.columnDefs.Add(new CDataColumnDefs("2", "2,3"));
            grid.columnDefs.Add(new CDataColumnDefs("8", "12"));

            //var grid = new Models.CDataTable() { Name = "", APIURL = "/FormFD/HeaderList", LeftFixedColumn = 1 };
            //grid.IsModify = _security.IsPCModify(ModuleNameList.Condition_Inspection);
            //grid.IsDelete = _security.IsPCDelete(ModuleNameList.Condition_Inspection);
            //grid.IsView = _security.IsPCView(ModuleNameList.Condition_Inspection);
            //grid.Columns.Add(new CDataColumns() { data = null, title = "Action", IsFreeze = true, sortable = false, render = "formFD.HeaderGrid.ActionRender" });
            //grid.Columns.Add(new CDataColumns() { data = "RefId", title = "Reference No" });
            //grid.Columns.Add(new CDataColumns() { data = "InspectedDt", title = "Date of Inspection" });
            //grid.Columns.Add(new CDataColumns() { data = "InspectedBy", title = "Inspected By" });
            //grid.Columns.Add(new CDataColumns() { data = "DivCode", title = "Division" });
            //grid.Columns.Add(new CDataColumns() { data = "District", title = "District" });
            //grid.Columns.Add(new CDataColumns() { data = "RMU", title = "RMU" });
            //grid.Columns.Add(new CDataColumns() { data = "RmuName", title = "RMU Name" });
            //grid.Columns.Add(new CDataColumns() { data = "Section", title = "Section Code" });
            //grid.Columns.Add(new CDataColumns() { data = "SectionName", title = "Section Name" });
            //grid.Columns.Add(new CDataColumns() { data = "RoadCode", title = "Road Code" });
            //grid.Columns.Add(new CDataColumns() { data = "CrewLeader", title = "Crew Leader" });
            return View("~/Views/FormFD/Index.cshtml", grid);
        }
        public async Task<JsonResult> HeaderList(DataTableAjaxPostModel searchData)
        {
            return Json(await _formFDService.GetFormFDHeaderGrid(searchData), JsonOption());
        }

        public async Task<IActionResult> GetType_BoundByGroup(string assetGroup, [FromServices] IAssetsService assetsService)
        {
            DDLookUpDTO ddlookup = new DDLookUpDTO();
            FormFDModel typeBoundList = new FormFDModel();

            ddlookup.TypeCode = assetsService.GetAssetCodeByName(assetGroup);
            ddlookup.Type = "Asset Type";
            typeBoundList.AssetType = await _dDLookUpService.GetLookUpTextDescConcat(ddlookup);

            ddlookup.Type = "Bound";
            typeBoundList.Bound = await _dDLookUpService.GetLookUpTextDescConcat(ddlookup);
            return Json(typeBoundList, JsonOption());
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
            DTO.ResponseBO.FormFDDTO frmFD = null;
            if (id > 0)
            {
                ViewBag.IsAdd = false;
                frmFD = _formFDService.FindByHeaderID(id).Result;
            }
            LoadLookupService("RMU", "Section Code", "Division", "RD_Code", "Year", "User", "Supervisor");
            return View("~/Views/FormFD/_AddFormFD.cshtml", frmFD);
        }
        public async Task<IActionResult> FindDetails(DTO.ResponseBO.FormFDDTO frmFD)
        {
            try
            {
                return Json(await _formFDService.FindDetails(frmFD, _security.UserID), JsonOption());
            }
            catch (Exception ex)
            {
                return Json(new { _error = ex.Message }, JsonOption());
            }
        }
        [HttpPost]
        [RequestFormLimits(ValueCountLimit = int.MaxValue)]
        public async Task<JsonResult> Save(DTO.ResponseBO.FormFDDTO frmFD)
        {
            return await SaveAll(frmFD, false);
        }
        [HttpPost]
        [RequestFormLimits(ValueCountLimit = int.MaxValue)]
        public async Task<JsonResult> Submit(DTO.ResponseBO.FormFDDTO frmFD)
        {
            frmFD.SubmitSts = true;
            return await SaveAll(frmFD, true);
        }
        private async Task<JsonResult> SaveAll(DTO.ResponseBO.FormFDDTO frmFD, bool updateSubmit)
        {
            frmFD.CrBy = _security.UserID;
            frmFD.ModBy = _security.UserID;
            frmFD.ModDt = DateTime.UtcNow;
            frmFD.CrDt = DateTime.UtcNow;
            var result = await _formFDService.Save(frmFD, updateSubmit);
            return Json(new { RefNo = result.FormRefId, Id = result.PkRefNo }, JsonOption());
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id > 0) { return Ok(new { id = _formFDService.Delete(id) }); }
            else { return BadRequest("Invalid Request!"); }

        }

        [HttpGet]
        public IActionResult Download(int id)
        {
            var content1 = _formFDService.FormDownload("FORMFD", id, _environment.WebRootPath + "/Templates/FormFD.xlsx");
            string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(content1, contentType1, "FORMFD" + ".xlsx");
        }
    }
}