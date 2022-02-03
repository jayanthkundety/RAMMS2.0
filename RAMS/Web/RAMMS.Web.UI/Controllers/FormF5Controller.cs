using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.JQueryModel;
using RAMMS.Web.UI.Models;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using Microsoft.AspNetCore.Hosting;

namespace RAMMS.Web.UI.Controllers
{
    [CAuthorize(ModuleName = ModuleNameList.Condition_Inspection)]
    public class FormF5Controller : Models.BaseController
    {
        private readonly IFormF5Service _formF5Service;
        private readonly IDDLookUpService _dDLookUpService;
        private readonly ISecurity _security;
        public FormF5Controller(IFormF5Service formF5Service, IDDLookUpService dDLookUpService, ISecurity security)
        {
            _formF5Service = formF5Service;
            _dDLookUpService = dDLookUpService;
            _security = security;
        }

        #region HeaderGrid
        public async Task<IActionResult> Index()
        {
            LoadLookupService("RMU", "RD_Code", "Section Code", "Bound~BR", "Year", "Asset Type~BR^Value");
            var grid = new Models.CDataTable() { Name = "tblF5HeaderGrid", APIURL = "FormF5/HeaderList", LeftFixedColumn = 1 };
            grid.IsModify = _security.IsPCModify(ModuleNameList.Condition_Inspection);
            grid.IsDelete = _security.IsPCDelete(ModuleNameList.Condition_Inspection);
            grid.IsView = _security.IsPCView(ModuleNameList.Condition_Inspection);
            grid.Columns.Add(new CDataColumns() { data = null, title = "Action", IsFreeze = true, sortable = false, render = "formF5.HeaderGrid.ActionRender" });
            grid.Columns.Add(new CDataColumns() { data = "RefID", title = "Reference No" });
            grid.Columns.Add(new CDataColumns() { data = "Year", title = "Year of Inspection" });
            grid.Columns.Add(new CDataColumns() { data = "InspectionDt", title = "Date of Inspection", render = "formF5.HeaderGrid.DateOfIns" });
            grid.Columns.Add(new CDataColumns() { data = "InspectedBy", title = "Inspected By" });
            grid.Columns.Add(new CDataColumns() { data = "Division", title = "Division" });
            grid.Columns.Add(new CDataColumns() { data = "District", title = "District" });
            grid.Columns.Add(new CDataColumns() { data = "RMU", title = "RMU Abbreviation" });
            grid.Columns.Add(new CDataColumns() { data = "RMUName", title = "RMU Name" });
            grid.Columns.Add(new CDataColumns() { data = "SecCode", title = "Section Code" });
            grid.Columns.Add(new CDataColumns() { data = "SecName", title = "Section Name" });
            grid.Columns.Add(new CDataColumns() { data = "RdCode", title = "Road Code" });
            grid.Columns.Add(new CDataColumns() { data = "RdId", title = "Road Id", visible = false });
            grid.Columns.Add(new CDataColumns() { data = "CrewLeaderName", title = "Crew Leader" });
            return View("~/Views/FormF5/Index.cshtml", grid);
        }
        public async Task<JsonResult> HeaderList(DataTableAjaxPostModel searchData)
        {
            if (searchData.order != null && searchData.order.Count > 0)
            {
                searchData.order = searchData.order.Select(x => { if (x.column == 11 || x.column == 1) { x.column = 12; } return x; }).ToList();
            }
            return Json(await _formF5Service.GetFormF5HeaderGrid(searchData), JsonOption());
        }

        #endregion

        #region Add Page & Detail Grid

        public async Task<IActionResult> Add()
        {
            ViewBag.IsEdit = true;
            return await ViewRequest(0);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.IsEdit = true;
            return await ViewRequest(id);
        }
        public async Task<IActionResult> View(int id)
        {
            ViewBag.IsEdit = false;
            return await ViewRequest(id);
        }
        public async Task<IActionResult> ViewRequest(int id)
        {
            FormF5HeaderRequestDTO formF5HeaderRequestDTO = null;
            LoadLookupService("RMU^Name", "RD_Code", "Section Code", "Division", "Year", "Supervisor", "User");
            if (id > 0)
            {
                formF5HeaderRequestDTO = await _formF5Service.FindHeaderById(id);
            }
            var grid = new Models.CDataTable() { Name = "tblF5DtlGrid", APIURL = "/FormF5/DetailList/" + id.ToString() };
            grid.Columns.Add(new CDataColumns() { data = "CenterLineChainage", title = "Starting Chainage", render = "jsMaster.LocationCh" });
            grid.Columns.Add(new CDataColumns() { data = "StructureCode", title = "Code" });
            grid.Columns.Add(new CDataColumns() { data = "Length", title = "Length" });
            grid.Columns.Add(new CDataColumns() { data = "Width", title = "Width" });
            grid.Columns.Add(new CDataColumns() { data = "Span", title = "Span" });
            grid.Columns.Add(new CDataColumns() { data = "River", title = "River Name" });
            grid.Columns.Add(new CDataColumns() { data = "Remarks", title = "Remarks" });
            grid.Columns.Add(new CDataColumns() { data = "OverAllCondition", title = "Over All Condition" });
            return View("~/Views/FormF5/_AddFormF5.cshtml", new Tuple<FormF5HeaderRequestDTO, CDataTable>(formF5HeaderRequestDTO, grid));
        }
        public async Task<JsonResult> DetailList(int id, DataTableAjaxPostModel searchData)
        {
            return Json(await _formF5Service.GetFormF5DetailGrid(id, searchData), JsonOption());
        }

        #endregion

        #region Header & Detail Save, Submit, Delete
        public async Task<JsonResult> FindDetails(FormF5HeaderRequestDTO headerDTO)
        {
            FormF5HeaderRequestDTO result = await _formF5Service.FindDetails(headerDTO);
            if (result == null || result.PkRefNo == 0)
            {
                headerDTO.CrBy = _security.UserID;
                headerDTO.ModBy = _security.UserID;
                headerDTO.ModDt = DateTime.UtcNow;
                headerDTO.CrDt = DateTime.UtcNow;
                result = await _formF5Service.SaveHeader(headerDTO, false);
                if (result.PkRefNo > 0)
                {
                    var rowsAffected = await _formF5Service.SaveDetail(result);
                }
            }
            return Json(result, JsonOption());
        }

        public async Task<JsonResult> Save(FormF5HeaderRequestDTO header)
        {
            header.SubmitSts = false;
            return Json(await SaveAll(header, header.SubmitSts));
        }
        public async Task<JsonResult> Submit(FormF5HeaderRequestDTO header)
        {
            header.SubmitSts = true;
            return Json(await SaveAll(header, header.SubmitSts));
        }
        public async Task<JsonResult> SaveAll(FormF5HeaderRequestDTO header, bool submitStatus)
        {
            header.ModBy = _security.UserID;
            header.ModDt = DateTime.UtcNow;
            var result = await _formF5Service.SaveHeader(header, submitStatus);
            return Json(result, JsonOption());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id > 0) { return Ok(new { id = await _formF5Service.DeleteFormF5Hdr(id) }); }
            else { return BadRequest("Invalid Request!"); }

        }
        #endregion

        #region Print
        public async Task<IActionResult> FormF5Download(int id, [FromServices] IWebHostEnvironment _environment)
        {
            var content1 = await _formF5Service.FormDowload("FORMF5", id, _environment.WebRootPath + "/Templates/FORMF5.xlsx");
            string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(content1, contentType1, "FORMF5" + ".xlsx");
        }
        #endregion
    }
}