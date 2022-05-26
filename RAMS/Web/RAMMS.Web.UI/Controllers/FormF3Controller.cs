using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Web.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
 
namespace RAMMS.Web.UI.Controllers
{
    public class FormF3Controller : BaseController
    {

        private IFormF3Service formF3Service;
        private ISecurity _security;
        private IWebHostEnvironment _environment;
        private IUserService _userService;
        private IRoadMasterService _roadMasterService;
        public FormF3Controller(
            IFormF3Service service,
            ISecurity security,
            IUserService userService,
            IWebHostEnvironment webhostenvironment,
            IRoadMasterService roadMasterService)
        {
            _userService = userService;
            formF3Service = service;
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
                                                                     //  var result = await formF3Service.GetHeaderList(filteredPagingDefinition);
                                                                     // return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });

            return null;
        }

        public async Task<IActionResult> Add(int id, bool isview)
        {
            LoadLookupService("Supervisor", "User");
            FormF3ResponseDTO _model = new FormF3ResponseDTO();
            if (id > 0)
            {
                _model = await formF3Service.GetHeaderById(id);
                _model = _model ?? new FormF3ResponseDTO();
            }
            _model.IsViewMode = _model.SubmitSts ? true : isview;
            return PartialView("~/Views/FormF3/_AddFormF3.cshtml", _model);
        }

    }
}
