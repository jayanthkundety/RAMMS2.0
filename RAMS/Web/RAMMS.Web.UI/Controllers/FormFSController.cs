using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Web.UI.Controllers
{
    public class FormFSController : Models.BaseController
    {
        private readonly IFormFSService formfsService;

        private IWebHostEnvironment Environment;
        public FormFSController(IFormFSService _formfsService,
        IWebHostEnvironment _Environment)
        {
            this.formfsService = _formfsService;
            this.Environment = _Environment;

        }

        public IActionResult Index()
        {
            LoadLookupService("RMU", "Section Code", "Division", "RD_Code", "Year");
            return View();
        }
        public async Task<IActionResult> Add(int id, bool isview)
        {
            LoadLookupService("RMU", "Section Code", "Division", "RD_Code", "Year", "Supervisor", "User");
            RAMMS.DTO.RequestBO.FormFSHeaderRequestDTO model = null;
            if (id > 0)
            {
                model = await formfsService.GetHeaderById(id);
            }
            model = model ?? new FormFSHeaderRequestDTO();
            model.IsView = model.SubmitSts ? true : isview;
            return View("~/Views/FormFS/_AddFormFS.cshtml", model);
        }

        public async Task<IActionResult> GetHeaderById(int id)
        {
            FormFSHeaderRequestDTO obj = new FormFSHeaderRequestDTO();
            if (id > 0)
            {
                obj = await formfsService.GetHeaderById(id);
                obj = obj ?? new FormFSHeaderRequestDTO();
            }
            return Json(obj);
        }

        public IActionResult Download(int id)
        {
            var content1 = formfsService.FormDownload("FormFS", id, Environment.WebRootPath, Environment.WebRootPath + "/Templates/FormFS.xlsx");
            string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(content1, contentType1, "FormFS" + ".xlsx");
        }

        public async Task<bool> RemoveHeader(int id) => await formfsService.RemoveHeader(id);
        public async Task<int> SaveHeader(FormFSHeaderRequestDTO model) => await formfsService.SaveHeader(model);
        public async Task<int> FindDetail(FormFSHeaderRequestDTO model) => await formfsService.FindDetail(model);
        public long LastInsertedHeaderNo() => formfsService.LastHeaderInsertedNo();
        public async Task<IActionResult> GetHeaderList(DataTableAjaxPostModel<FormFSHeaderRequestDTO> searchData)
        {
            FilteredPagingDefinition<FormFSHeaderRequestDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormFSHeaderRequestDTO>();
            searchData.filterData = searchData.filterData ?? new FormFSHeaderRequestDTO();
            int _id;
            if (Request.Form.ContainsKey("columns[0][search][value]"))
            {
                searchData.filterData.SmartSearch = Request.Form["columns[0][search][value]"].ToString();
            }

            if (Request.Form.ContainsKey("columns[1][search][value]"))
            {
                searchData.filterData.RmuCode = Request.Form["columns[1][search][value]"].ToString();
            }

            if (Request.Form.ContainsKey("columns[2][search][value]"))
            {
                if (int.TryParse(Request.Form["columns[2][search][value]"].ToString(), out _id))
                {
                    searchData.filterData.SecCode = _id;
                }
            }

            if (Request.Form.ContainsKey("columns[3][search][value]"))
            {
                searchData.filterData.RoadCode = Request.Form["columns[3][search][value]"].ToString();
            }

            if (Request.Form.ContainsKey("columns[4][search][value]"))
            {
                if (int.TryParse(Request.Form["columns[4][search][value]"].ToString(), out _id))
                {
                    searchData.filterData.FromYear = _id;
                }
            }

            if (Request.Form.ContainsKey("columns[5][search][value]"))
            {
                if (int.TryParse(Request.Form["columns[5][search][value]"].ToString(), out _id))
                {
                    searchData.filterData.ToYear = _id;
                }
            }
            if (Request.Form.ContainsKey("columns[6][search][value]"))
            {
                searchData.filterData.FormType = Request.Form["columns[6][search][value]"].ToString();
            }
            if (!string.IsNullOrEmpty(Request.Form["columns[7][search][value]"].ToString()))
            {
                searchData.filterData.locchFromKm = Convert.ToInt32(Request.Form["columns[7][search][value]"]);
            }
            if (!string.IsNullOrEmpty(Request.Form["columns[8][search][value]"].ToString()))
            {
                searchData.filterData.locchFromM = Request.Form["columns[8][search][value]"].ToString();
            }
            if (!string.IsNullOrEmpty(Request.Form["columns[9][search][value]"].ToString()))
            {
                searchData.filterData.locchToKm = Convert.ToInt32(Request.Form["columns[9][search][value]"]);
            }
            if (!string.IsNullOrEmpty(Request.Form["columns[10][search][value]"].ToString()))
            {
                searchData.filterData.locchToM = Request.Form["columns[10][search][value]"].ToString();
            }
            filteredPagingDefinition.Filters = searchData.filterData;
            if (searchData.order != null)
            {
                filteredPagingDefinition.ColumnIndex = searchData.order[0].column;
                filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }
            filteredPagingDefinition.RecordsPerPage = searchData.length; //Convert.ToInt32(Request.Form["length"]);
            filteredPagingDefinition.StartPageNo = searchData.start; //Convert.ToInt32(Request.Form["start"]); //TODO
            var result = await formfsService.GetHeaderList(filteredPagingDefinition);
            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }

        public async Task<IActionResult> GetDetailById(int id)
        {
            FormFSDetailRequestDTO obj = new FormFSDetailRequestDTO();
            if (id > 0)
            {
                obj = await formfsService.GetDetailById(id);
                obj = obj ?? new FormFSDetailRequestDTO();
            }
            return Json(obj);
        }
        public async Task<bool> RemoveDetail(int id) => await formfsService.RemoveDetail(id);
        public async Task<int> SaveDetail(FormFSDetailRequestDTO model) => await formfsService.SaveDetail(model);
        public async Task<IActionResult> GetDetailList(DataTableAjaxPostModel<FormFSDetailRequestDTO> searchData)
        {
            FilteredPagingDefinition<FormFSDetailRequestDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormFSDetailRequestDTO>();

            filteredPagingDefinition.Filters = searchData.filterData;
            if (searchData.order != null)
            {
                filteredPagingDefinition.ColumnIndex = searchData.order[0].column;
                filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }
            filteredPagingDefinition.RecordsPerPage = searchData.length; //Convert.ToInt32(Request.Form["length"]);
            filteredPagingDefinition.StartPageNo = searchData.start; //Convert.ToInt32(Request.Form["start"]); //TODO
            var result = await formfsService.GetDetailList(filteredPagingDefinition);
            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }

        public async Task<IActionResult> GetRecordList(int headerid)
        {
            return Json(await formfsService.GetRecordList(headerid));
        }
    }
}
