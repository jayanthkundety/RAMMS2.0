using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Common;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Web.UI.Models;

namespace RAMMS.Web.UI.Controllers
{
    public class AdministrationController : BaseController
    {
        private readonly IAdministrationService _service;
        private readonly ISecurity _security;
        private readonly IDivisionService divisionService;
        private readonly IRMUService rmuService;
        public AdministrationController(IAdministrationService service, ISecurity security,
            IRMUService rmuService,
            IDivisionService divisionService)
        {
            _service = service;
            _security = security;
            this.divisionService = divisionService;
            this.rmuService = rmuService;
        }
        private Dictionary<string, string> GetPages()
        {
            Dictionary<string, string> lstPages = new Dictionary<string, string>();
            lstPages.Add("division", "Division");
            lstPages.Add("rmu", "RMU");
            lstPages.Add("section", "Section");
            lstPages.Add("road", "List of Road");
            lstPages.Add("roadfeatureid", "Road Feature ID");
            lstPages.Add("assettype", "Asset Type");
            lstPages.Add("defect", "Defect");
            return lstPages;
        }
        public IActionResult List(string id)
        {
            id = string.IsNullOrEmpty(id) ? "" : id.ToLower();
            Dictionary<string, string> lstPages = GetPages();
            if (!string.IsNullOrEmpty(id) && lstPages.ContainsKey(id))
            {
                ViewBag.PageID = id;
                ViewBag.PageName = lstPages[id];
                switch (id)
                {
                    case "section":
                        LoadLookupService("RMU", "Division");
                        break;
                    case "rmu":
                        return View("~/Views/Administration/RMU/RMU.cshtml");
                    case "assettype":
                        ViewData["AssestGroup"] = _service.AssetGroupList().Result;
                        break;
                    case "defect":
                        ViewData["AssestGroup"] = _service.DefectAssetGroupList().Result;
                        break;
                    case "division":
                        return View("~/Views/Administration/Division/Division.cshtml");
                }
                return View(id, GridTable(id));
            }
            else
            {
                return BadRequest("Invalid Request");
            }
        }

        public async Task<IActionResult> GetDivisionById(int id)
        {
            DivisionRequestDTO obj = new DivisionRequestDTO(); if (id > 0)
            {
                obj = await divisionService.GetById(id); obj = obj ?? new DivisionRequestDTO();
            }
            return Json(obj);
        }

        public async Task<IActionResult> GetSectionById(int id)
        {
            DivRmuSectionRequestDTO obj = new DivRmuSectionRequestDTO(); if (id > 0)
            {
                obj = await divisionService.GetDivRmuSectionById(id); obj = obj ?? new DivRmuSectionRequestDTO();
            }
            return Json(obj);
        }
        public async Task<bool> RemoveDivision(int id) => await divisionService.Remove(id);
        public async Task<int> SaveDivision(DivisionRequestDTO model) => await divisionService.Save(model);

        public async Task<int> SaveSection(DivRmuSectionRequestDTO model) => await divisionService.Save(model);
        public async Task<IActionResult> GetDivisionList(DataTableAjaxPostModel<DivisionRequestDTO> searchData)
        {
            FilteredPagingDefinition<DivisionRequestDTO> filteredPagingDefinition = new FilteredPagingDefinition<DivisionRequestDTO>();

            if (Request.Form.ContainsKey("columns[0][search][value]"))
            {
                searchData.filterData.Code = Request.Form["columns[0][search][value]"].ToString();
            }
            filteredPagingDefinition.Filters = searchData.filterData;
            if (searchData.order != null)
            {
                filteredPagingDefinition.ColumnIndex = searchData.order[0].column;
                filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }
            filteredPagingDefinition.RecordsPerPage = searchData.length; //Convert.ToInt32(Request.Form["length"]);
            filteredPagingDefinition.StartPageNo = searchData.start; //Convert.ToInt32(Request.Form["start"]); //TODO
            var result = await divisionService.GetList(filteredPagingDefinition);
            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }
        public async Task<IActionResult> GetRMUById(int id)
        {
            RMURequestDTO obj = new RMURequestDTO(); if (id > 0) { obj = await rmuService.GetById(id); obj = obj ?? new RMURequestDTO(); }
            return Json(obj);
        }
        public async Task<bool> RemoveRMU(int id) => await rmuService.Remove(id);
        public async Task<int> SaveRMU(RMURequestDTO model) => await rmuService.Save(model);
        public IActionResult GetRMUListByDiv(string code) => Json(rmuService.GetList(code));
        public async Task<IActionResult> GetRMUList(DataTableAjaxPostModel<RMURequestDTO> searchData)
        {
            FilteredPagingDefinition<RMURequestDTO> filteredPagingDefinition = new FilteredPagingDefinition<RMURequestDTO>();

            if (Request.Form.ContainsKey("columns[0][search][value]"))
            {
                searchData.filterData.Code = Request.Form["columns[0][search][value]"].ToString();
            }
            filteredPagingDefinition.Filters = searchData.filterData;
            if (searchData.order != null) { filteredPagingDefinition.ColumnIndex = searchData.order[0].column; filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending; }
            filteredPagingDefinition.RecordsPerPage = searchData.length; //Convert.ToInt32(Request.Form["length"]);
            filteredPagingDefinition.StartPageNo = searchData.start; //Convert.ToInt32(Request.Form["start"]); //TODO
            var result = await rmuService.GetList(filteredPagingDefinition);
            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }
        public CDataTable GridTable(string pageid)
        {
            var grid = new CDataTable() { Name = "tblGrid", APIURL = "/Administration/GridList/?pid=" + pageid, LeftFixedColumn = 1 };
            grid.Columns.Add(new CDataColumns() { data = null, title = "Action", IsFreeze = true, sortable = false, render = "jsAdmin.HeaderGrid.ActionRender" });
            if (pageid == "division" || pageid == "rmu")
            {
                grid.Columns.Add(new CDataColumns() { data = "Code", title = "Code" });
                grid.Columns.Add(new CDataColumns() { data = "Desc", title = "Description" });
            }
            else if (pageid == "section")
            {
                grid.Columns.Add(new CDataColumns() { data = "Code", title = "Code" });
                grid.Columns.Add(new CDataColumns() { data = "Desc", title = "Description" });
                grid.Columns.Add(new CDataColumns() { data = "Div", title = "Division" });
                grid.Columns.Add(new CDataColumns() { data = "RMUCode", title = "RMU Code" });
                grid.Columns.Add(new CDataColumns() { data = "RMU", title = "RMU Description" });
            }
            else if (pageid == "road")
            {
                grid.Columns.Add(new CDataColumns() { data = "Code", title = "Code" });
                grid.Columns.Add(new CDataColumns() { data = "Desc", title = "Description" });
                grid.Columns.Add(new CDataColumns() { data = "Div", title = "Division" });
                grid.Columns.Add(new CDataColumns() { data = "RMUCode", title = "RMU Code" });
                grid.Columns.Add(new CDataColumns() { data = "RMU", title = "RMU Description" });
                grid.Columns.Add(new CDataColumns() { data = "SectionCode", title = "Section Code" });
                grid.Columns.Add(new CDataColumns() { data = "Section", title = "Section" });
                grid.Columns.Add(new CDataColumns() { data = "RoadCategoryCode", title = "Road Category Code" });
                grid.Columns.Add(new CDataColumns() { data = "RoadCategory", title = "Road Category" });
                grid.Columns.Add(new CDataColumns() { data = "LocationFrom", title = "Location From" });
                grid.Columns.Add(new CDataColumns() { data = "LocationTo", title = "Location To" });
                grid.Columns.Add(new CDataColumns() { data = "ChFrom", title = "Ch From" });
                grid.Columns.Add(new CDataColumns() { data = "ChFromDeci", title = "Ch From Deci" });
                grid.Columns.Add(new CDataColumns() { data = "ChTo", title = "Ch To" });
                grid.Columns.Add(new CDataColumns() { data = "ChToDeci", title = "Ch To Deci" });
                grid.Columns.Add(new CDataColumns() { data = "PavedLength", title = "Paved Length" });
                grid.Columns.Add(new CDataColumns() { data = "UnpavedLength", title = "UnPaved Length" });
                grid.Columns.Add(new CDataColumns() { data = "Owner", title = "Owner" });

            }
            else if (pageid == "assettype" || pageid == "defect")
            {
                grid.Columns.Add(new CDataColumns() { data = "GrpCode", title = "Group Code" });
                grid.Columns.Add(new CDataColumns() { data = "GrpName", title = "Group Name" });
                grid.Columns.Add(new CDataColumns() { data = "Code", title = "Code" });
                grid.Columns.Add(new CDataColumns() { data = "Desc", title = "Description" });
                grid.Columns.Add(new CDataColumns() { data = "ContractCode", title = "Contract Code" });
            }
            grid.Columns.Add(new CDataColumns() { data = "CreatedBy", title = "Created By" });
            grid.Columns.Add(new CDataColumns() { data = "CreatedOn", title = "Created On", render = "jsAdmin.HeaderGrid.DateOfEntry" });
            grid.Columns.Add(new CDataColumns() { data = "ModifiedBy", title = "Modified By" });
            grid.Columns.Add(new CDataColumns() { data = "ModifiedOn", title = "Modified On", render = "jsAdmin.HeaderGrid.DateOfEntry" });
            return grid;
        }
        public async Task<JsonResult> GridList(DataTableAjaxPostModel searchData, [FromQuery(Name = "pid")] string type)
        {
            return Json(await _service.GridList(searchData, type), JsonOption());

        }
        public async Task<JsonResult> Update(AdministratorDTO form)
        {
            Result<string> objResult = new Result<string>();
            try
            {
                _service.Save(form, _security.UserID.ToString());
                objResult.Message = "Sucessfully updated";
                objResult.IsSuccess = true;
            }
            catch (Exception ex)
            {
                objResult.IsSuccess = false;
                objResult.Message = ex.Message;
            }
            return Json(objResult, JsonOption());
        }
        public async Task<JsonResult> Delete(AdministratorDTO form)
        {
            Result<string> objResult = new Result<string>();
            try
            {
                _service.Delete(form, _security.UserID.ToString());
                objResult.Message = "Sucessfully deleted";
                objResult.IsSuccess = true;
            }
            catch (Exception ex)
            {
                objResult.IsSuccess = false;
                objResult.Message = ex.Message;
            }
            return Json(objResult, JsonOption());
        }
        //public IActionResult Division()
        //{
        //    return View();
        //}

        //public IActionResult RMU()
        //{
        //    return View();
        //}

        //public IActionResult Section()
        //{
        //    return View();
        //}

        //public IActionResult ListofRoad()
        //{
        //    return View();
        //}

        //public IActionResult RoadFeatureID()
        //{
        //    return View();
        //}

        //public IActionResult AssetType()
        //{
        //    return View();
        //}

        //public IActionResult Defect()
        //{
        //    return View();
        //}


        //New Change

        public async Task<IActionResult> GetSectionList(DataTableAjaxPostModel<DivRmuSectionRequestDTO> searchData)
        {
            FilteredPagingDefinition<DivRmuSectionRequestDTO> filteredPagingDefinition = new FilteredPagingDefinition<DivRmuSectionRequestDTO>();

            if (Request.Form.ContainsKey("columns[0][search][value]"))
            {
                searchData.filterData.DivCode  = Request.Form["columns[0][search][value]"].ToString();
            }
            filteredPagingDefinition.Filters = searchData.filterData;
            if (searchData.order != null)
            {
                filteredPagingDefinition.ColumnIndex = searchData.order[0].column;
                filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }
            filteredPagingDefinition.RecordsPerPage = searchData.length; //Convert.ToInt32(Request.Form["length"]);
            filteredPagingDefinition.StartPageNo = searchData.start; //Convert.ToInt32(Request.Form["start"]); //TODO
            var result = await divisionService.GetList(filteredPagingDefinition);
            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }
    }
}