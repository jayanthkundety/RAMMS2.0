using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.RequestBO;
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
    public class FormF1Controller : BaseController
    {

        private IFormF1Service _formF1Service;
        private readonly IFormJServices _formJService;
        private ISecurity _security;
        private IWebHostEnvironment _environment;
        private IUserService _userService;
        private IRoadMasterService _roadMasterService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FormF1Controller(
            IFormF1Service service,
            ISecurity security,
            IUserService userService,
            IWebHostEnvironment webhostenvironment,
            IRoadMasterService roadMasterService,
             IFormJServices formJServices)
        {
            _userService = userService;
            _formF1Service = service;
            _security = security;
            _roadMasterService = roadMasterService;
            _formJService = formJServices ?? throw new ArgumentNullException(nameof(formJServices));
            _webHostEnvironment = webhostenvironment;
        }
        public IActionResult Index()
        {
            LoadLookupService("RMU", "Section Code", "Division", "RD_Code", "Year");
            return View();
        }

        public async Task<IActionResult> GetHeaderList(DataTableAjaxPostModel<FormF1SearchGridDTO> searchData)
        {
            int _id = 0;
            DateTime dt;
            FilteredPagingDefinition<FormF1SearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormF1SearchGridDTO>();
            searchData.filterData = searchData.filterData ?? new FormF1SearchGridDTO();
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

          


            filteredPagingDefinition.Filters = searchData.filterData;
            if (searchData.order != null)
            {
                filteredPagingDefinition.ColumnIndex = searchData.order[0].column;
                filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }
            filteredPagingDefinition.RecordsPerPage = searchData.length; //Convert.ToInt32(Request.Form["length"]);
            filteredPagingDefinition.StartPageNo = searchData.start; //Convert.ToInt32(Request.Form["start"]); //TODO
            var result = await _formF1Service.GetHeaderList(filteredPagingDefinition);
            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });

            return null;
        }

        public async Task<IActionResult> GetDetailList(DataTableAjaxPostModel<FormF1DtlResponseDTO> searchData)
        {
            FilteredPagingDefinition<FormF1DtlResponseDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormF1DtlResponseDTO>();

            filteredPagingDefinition.Filters = searchData.filterData;
            if (searchData.order != null)
            {
                filteredPagingDefinition.ColumnIndex = searchData.order[0].column;
                filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }
            filteredPagingDefinition.RecordsPerPage = searchData.length; //Convert.ToInt32(Request.Form["length"]);
            filteredPagingDefinition.StartPageNo = searchData.start; //Convert.ToInt32(Request.Form["start"]); //TODO
            var result = await _formF1Service.GetDetailList(filteredPagingDefinition);
            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });

        }


        public async Task<IActionResult> Add(int id, int view)
        {
            LoadLookupService("Supervisor", "User");

            FormASearchDropdown ddl = _formJService.GetDropdown(new RequestDropdownFormA { });
            ViewData["SectionCode"] = ddl.Section.Select(s => new SelectListItem { Text = s.Text, Value = s.Value }).ToArray();

            FormF1Model _model = new FormF1Model();
            if (id > 0)
            {
                _model.FormF1 = await _formF1Service.GetHeaderById(id);
            }
            else
            {
                _model.FormF1 = new FormF1ResponseDTO();
            }
            ViewData["Asset"] = _formF1Service.GetAssetDetails(_model.FormF1);


            _model.FormF1 = _model.FormF1 ?? new FormF1ResponseDTO();
            _model.view = view;

            if ((_model.FormF1.InspectedBy == null || _model.FormF1.InspectedBy == 0) && _model.FormF1.Status == RAMMS.Common.StatusList.Submitted)
            {
                _model.FormF1.InspectedBy = _security.UserID;
                _model.FormF1.InspectedDate = DateTime.Today;
                _model.FormF1.InspectedBySign = true;
            }

            return PartialView("~/Views/FormF1/_AddFormF1.cshtml", _model);
        }



        public async Task<IActionResult> SaveFormF1(FormF1Model frm)
        {
            int refNo = 0;
            frm.FormF1.ActiveYn = true;
            if (frm.FormF1.PkRefNo == 0)
            {
                frm.FormF1 = await _formF1Service.SaveFormF1(frm.FormF1);
               

                return Json(new { FormExist = frm.FormF1.FormExist, RefId = frm.FormF1.PkRefId, PkRefNo = frm.FormF1.PkRefNo, Status = frm.FormF1.Status });
            }
            else
            {
                if (frm.FormF1.Status == "Initialize")
                    frm.FormF1.Status = "Saved";
                refNo = await _formF1Service.Update(frm.FormF1);
            }
            return Json(refNo);


        }


        public async Task<IActionResult> SaveFormF1Dtl(FormF1Model frm)
        {
            int? refNo = 0;

            frm.FormF1Dtl.Ff1hPkRefNo = frm.FormF1.PkRefNo;
            if (frm.FormF1Dtl.PkRefNo == 0)
            {
                refNo = _formF1Service.SaveFormF1Dtl(frm.FormF1Dtl);

            }
            else
            {
                _formF1Service.UpdateFormF1Dtl(frm.FormF1Dtl);
            }
            return Json(refNo);


        }

        public async Task<IActionResult> DeleteFormF1(int id)
        {
            int? rowsAffected = 0;
            rowsAffected = _formF1Service.DeleteFormF1(id);
            return Json(rowsAffected);
        }

        public async Task<IActionResult> DeleteFormF1Dtl(int id)
        {
            int? rowsAffected = 0;
            rowsAffected = _formF1Service.DeleteFormF1Dtl(id);
            return Json(rowsAffected);
        }


        public async Task<IActionResult> FormF1Download(int id, [FromServices] IWebHostEnvironment _environment)
        {
            var content1 = await _formF1Service.FormDownload("FORMF1", id, _environment.WebRootPath + "/Templates/FORMF1.xlsx");
            string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(content1, contentType1, "FORMF1" + ".xlsx");
        }

    }
}
