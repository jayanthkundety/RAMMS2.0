using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Web.UI.Models;

namespace RAMMS.Web.UI.Controllers
{
    public class FormF2Controller : BaseController
    {
        private IFormF2Service formF2Service;
        private ISecurity _security;
        private IWebHostEnvironment _environment;
        private IUserService _userService;
        private IRoadMasterService _roadMasterService;
        public FormF2Controller(
            IFormF2Service service,
            ISecurity security,
            IUserService userService,
            IWebHostEnvironment webhostenvironment,
            IRoadMasterService roadMasterService)
        {
            _userService = userService;
            formF2Service = service;
            _security = security;
            _environment = webhostenvironment;
            _roadMasterService = roadMasterService;
        }
        public IActionResult Index()
        {
            LoadLookupService("RMU", "Section Code", "Division", "RD_Code", "Year");
            return View();
        }

        public async Task<IActionResult> GetHeaderList(DataTableAjaxPostModel<FormF2SearchGridDTO> searchData)
        {
            int _id = 0;
            DateTime dt;
            FilteredPagingDefinition<FormF2SearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormF2SearchGridDTO>();
            searchData.filterData = searchData.filterData ?? new FormF2SearchGridDTO();
            if (Request.Form.ContainsKey("columns[0][search][value]"))
            {
                searchData.filterData.SmartSearch = Request.Form["columns[0][search][value]"].ToString();
            }

            if (Request.Form.ContainsKey("columns[1][search][value]"))
            {
                searchData.filterData.RmuCode = Request.Form["columns[1][search][value]"].ToString() == "null" ? "" : Request.Form["columns[1][search][value]"].ToString();
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
                searchData.filterData.AssertType = Request.Form["columns[6][search][value]"].ToString();
            }

            if (Request.Form.ContainsKey("columns[7][search][value]"))
            {
                if (int.TryParse(Request.Form["columns[7][search][value]"].ToString(), out _id))
                {
                    searchData.filterData.FromChKM = _id;
                }
            }
            if (Request.Form.ContainsKey("columns[8][search][value]"))
            {
                searchData.filterData.FromChM = Request.Form["columns[8][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[9][search][value]"))
            {
                if (int.TryParse(Request.Form["columns[9][search][value]"].ToString(), out _id))
                {
                    searchData.filterData.ToChKM = _id;
                }
            }
            if (Request.Form.ContainsKey("columns[10][search][value]"))
            {
                searchData.filterData.ToChM = Request.Form["columns[10][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[11][search][value]"))
            {
                searchData.filterData.Bound = Request.Form["columns[11][search][value]"].ToString();
            }



            filteredPagingDefinition.Filters = searchData.filterData;
            if (searchData.order != null)
            {
                filteredPagingDefinition.ColumnIndex = searchData.order[0].column;
                filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }
            filteredPagingDefinition.RecordsPerPage = searchData.length; //Convert.ToInt32(Request.Form["length"]);
            filteredPagingDefinition.StartPageNo = searchData.start; //Convert.ToInt32(Request.Form["start"]); //TODO
            var result = await formF2Service.GetHeaderList(filteredPagingDefinition);
            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });

        }
        /// <summary>
        /// To Get road Master list
        /// </summary>
        /// <param name="rmu"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetRoadListByDivisionCode(string code)
        {
            return Json(await _roadMasterService.GetRMUBasedData(new RoadMasterRequestDTO
            {
                DivisionCode = code
            }));
        }

        /// <summary>
        /// To Get road detail
        /// </summary>
        /// <param name="rmu"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetRoadDetailByCode(string code)
        {
            var result = (await _roadMasterService.GetRMUBasedData(new RoadMasterRequestDTO
            {
                RoadCode = code
            })).FirstOrDefault();
            return Json(result);
        }
        /// <summary>
        /// To Add Form F2
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isview"></param>
        /// <returns></returns>
        public async Task<IActionResult> Add(int id, bool isview)
        {
            LoadLookupService("Supervisor","User");
            FormF2HeaderRequestDTO _model = new FormF2HeaderRequestDTO();
            if (id > 0)
            {
                _model = await formF2Service.GetHeaderById(id);
                _model = _model ?? new FormF2HeaderRequestDTO();
            }
            _model.IsViewMode = _model.SubmitSts ? true : isview;
            return PartialView("~/Views/FormF2/_AddFormF2.cshtml", _model);
        }
        [HttpPost]
        public async Task<IActionResult> RMUSecRoad(AssetDDLRequestDTO request)
        {
            AssetDDLResponseDTO assetDDLResponseDTO = new AssetDDLResponseDTO();
            if (string.IsNullOrEmpty(request.RdCode) &&
                string.IsNullOrEmpty(request.RMU) &&
                 (request.SectionCode == 0))
            {
                FormASearchDropdown ddl = new FormASearchDropdown();
                DDLookUpDTO ddlookup = new DDLookUpDTO();
                RoadMasterRequestDTO roadMasterReq = new RoadMasterRequestDTO();

            }
            assetDDLResponseDTO = await formF2Service.GetAssetDDL(request);
            return Json(assetDDLResponseDTO);
            // return Json(_formJService.GetDropdown(request));

        }
        /// <summary>
        /// To Save Header
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> SaveHeader(FormF2HeaderRequestDTO model)
        {
            int response;
            if (model.SubmitSts)
            {
                //var list = await formF2Service.GetF2DetailList(model.PkRefNo);
                //if (list.All(x => x.GrCondition1 != null || x.GrCondition2 != null || x.GrCondition3 != null))
                //{
                response = await formF2Service.SaveHeader(model);
                return response;
                //}
                //else
                //{
                //    return 0;
                //}
            }
            else
            {
                return await formF2Service.SaveHeader(model);
            }
        }
        /// <summary>
        /// To Remove Header
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveHeader(int id) => await formF2Service.RemoveHeader(id);
        public async Task<decimal?> GetRoadLength(string roadcode) => await formF2Service.TotalLength(roadcode);
        /// <summary>
        /// To Get detail by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetDetailById(int id)
        {
            FormF2DetailRequestDTO obj = new FormF2DetailRequestDTO();
            if (id > 0)
            {
                obj = await formF2Service.GetDetailById(id);
                obj = obj ?? new FormF2DetailRequestDTO();
            }
            return Json(obj);
        }
        /// <summary>
        /// To Remove Detail by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveDetail(int id) => await formF2Service.RemoveDetail(id);
        /// <summary>
        /// To Save Detail.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> SaveDetail(FormF2DetailRequestDTO model) => await formF2Service.SaveDetail(model);
        /// <summary>
        /// TO Get Last Inserted Detail No.
        /// </summary>
        /// <param name="headerId"></param>
        /// <returns></returns>
        public int LastInsertedDetailNo(int headerId) => formF2Service.LastInsertedDetailNo(headerId);
        /// <summary>
        /// TO Get Detail List
        /// </summary>
        /// <param name="searchData"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetDetailList(DataTableAjaxPostModel<FormF2DetailRequestDTO> searchData)
        {
            FilteredPagingDefinition<FormF2DetailRequestDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormF2DetailRequestDTO>();

            filteredPagingDefinition.Filters = searchData.filterData;
            if (searchData.order != null)
            {
                filteredPagingDefinition.ColumnIndex = searchData.order[0].column;
                filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }
            filteredPagingDefinition.RecordsPerPage = searchData.length; //Convert.ToInt32(Request.Form["length"]);
            filteredPagingDefinition.StartPageNo = searchData.start; //Convert.ToInt32(Request.Form["start"]); //TODO
            var result = await formF2Service.GetDetailList(filteredPagingDefinition);
            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });

        }

        public async Task<IActionResult> GetLocationCh(string roadcode)
        {
            return Json(await formF2Service.GetLocationCh(roadcode));
        }

        public async Task<IActionResult> GetStructureCode(string roadcode, string locationch)
        {
            return Json(await formF2Service.GetStructureCode(roadcode, locationch));
        }

        public async Task<IActionResult> GetAIBound(string roadcode, string locationch, string structurecode)
        {
            return Json(await formF2Service.GetAIBound(roadcode, locationch, structurecode));
        }

        public async Task<IActionResult> GetPostSpacing(string roadcode, string locationch, string structurecode, string bound)
        {
            return Json(await formF2Service.GetPostSpacing(roadcode, locationch, structurecode, bound));
        }

        public IActionResult FormF2Download(int id)
        {
            var content1 = formF2Service.FormDownload("FORMF2", id, _environment.WebRootPath + "/Templates/FORMF2.xlsx");
            string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(content1, contentType1, "FORMF2" + ".xlsx");
        }

    }
}