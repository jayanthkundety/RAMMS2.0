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
    public class FormTController : BaseController
    {

        private IFormTService _formTService;
        private readonly IFormJServices _formJService;
        private ISecurity _security;
        private IWebHostEnvironment _environment;
        private IUserService _userService;
        private IRoadMasterService _roadMasterService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FormTController(
            IFormTService service,
            ISecurity security,
            IUserService userService,
            IWebHostEnvironment webhostenvironment,
            IRoadMasterService roadMasterService,
             IFormJServices formJServices)
        {
            _userService = userService;
            _formTService = service;
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

        public async Task<IActionResult> GetHeaderList(DataTableAjaxPostModel<FormTSearchGridDTO> searchData)
        {
            int _id = 0;
            DateTime dt;
            FilteredPagingDefinition<FormTSearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormTSearchGridDTO>();
            searchData.filterData = searchData.filterData ?? new FormTSearchGridDTO();
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
                searchData.filterData.RoadCode = Request.Form["columns[2][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[3][search][value]"))
            {
                searchData.filterData.FromInspectionDate = Request.Form["columns[3][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[4][search][value]"))
            {
                searchData.filterData.ToInspectionDate = Request.Form["columns[4][search][value]"].ToString();
            }


            filteredPagingDefinition.Filters = searchData.filterData;
            if (searchData.order != null)
            {
                filteredPagingDefinition.ColumnIndex = searchData.order[0].column;
                filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }
            filteredPagingDefinition.RecordsPerPage = searchData.length; //Convert.ToInt32(Request.Form["length"]);
            filteredPagingDefinition.StartPageNo = searchData.start; //Convert.ToInt32(Request.Form["start"]); //TODO
            var result = await _formTService.GetHeaderList(filteredPagingDefinition);
            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
 
        }

        public async Task<IActionResult> GetDetailList(DataTableAjaxPostModel<FormTDtlResponseDTO> searchData)
        {
            FilteredPagingDefinition<FormTDtlResponseDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormTDtlResponseDTO>();

            filteredPagingDefinition.Filters = searchData.filterData;
            if (searchData.order != null)
            {
                filteredPagingDefinition.ColumnIndex = searchData.order[0].column;
                filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }
            filteredPagingDefinition.RecordsPerPage = searchData.length; //Convert.ToInt32(Request.Form["length"]);
            filteredPagingDefinition.StartPageNo = searchData.start; //Convert.ToInt32(Request.Form["start"]); //TODO
            var result = await _formTService.GetDetailList(filteredPagingDefinition);
            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });

        }


        public async Task<IActionResult> Add(int id, int view)
        {
            LoadLookupService("Supervisor", "User");

            FormASearchDropdown ddl = _formJService.GetDropdown(new RequestDropdownFormA { });
            ViewData["SectionCode"] = ddl.Section.Select(s => new SelectListItem { Text = s.Text, Value = s.Value }).ToArray();

            FormTModel _model = new FormTModel();
            if (id > 0)
            {
                _model.FormT = await _formTService.GetHeaderById(id);
            }
            else
            {
                _model.FormT = new FormTResponseDTO();
            }
            ViewData["Asset"] = _formTService.GetAssetDetails(_model.FormT);


            _model.FormT = _model.FormT ?? new FormTResponseDTO();
            _model.view = view;

            //if ((_model.FormT.InspectedBy == null || _model.FormT.InspectedBy == 0) && _model.FormT.Status == RAMMS.Common.StatusList.Submitted)
            //{
            //    _model.FormT.InspectedBy = _security.UserID;
            //    _model.FormT.InspectedDate = DateTime.Today;
            //    _model.FormT.InspectedBySign = true;
            //}

            return PartialView("~/Views/FormT/_AddFormT.cshtml", _model);
        }



        public async Task<IActionResult> SaveFormT(FormTModel frm)
        {
            int refNo = 0;
            frm.FormT.ActiveYn = true;
            if (frm.FormT.PkRefNo == 0)
            {
                frm.FormT = await _formTService.SaveFormT(frm.FormT);
                return Json(new
                {
                    FormExist = frm.FormT.FormExist
                });

                // return Json(new { FormExist = frm.FormT.FormExist, RefId = frm.FormT.PkRefId, PkRefNo = frm.FormT.PkRefNo, Status = frm.FormT.Status });
            }
            else
            {
                if (frm.FormT.Status == "Initialize")
                    frm.FormT.Status = "Saved";
                refNo = await _formTService.Update(frm.FormT);
            }
            return Json(refNo);


        }


        public async Task<IActionResult> SaveFormTDtl(FormTModel frm)
        {
            int? refNo = 0;

            frm.FormTDtl.FmtPkRefNo = frm.FormT.PkRefNo;
            if (frm.FormTDtl.PkRefNo == 0)
            {
                refNo = _formTService.SaveFormTDtl(frm.FormTDtl);

            }
            else
            {
                _formTService.UpdateFormTDtl(frm.FormTDtl);
            }
            return Json(refNo);


        }

        public async Task<IActionResult> DeleteFormT(int id)
        {
            int? rowsAffected = 0;
            rowsAffected = _formTService.DeleteFormT(id);
            return Json(rowsAffected);
        }

        public async Task<IActionResult> DeleteFormTDtl(int id)
        {
            int? rowsAffected = 0;
            rowsAffected = _formTService.DeleteFormTDtl(id);
            return Json(rowsAffected);
        }


        //public async Task<IActionResult> FormTDownload(int id, [FromServices] IWebHostEnvironment _environment)
        //{
        //    var content1 = await _formTService.FormDownload("FORMT", id, _environment.WebRootPath + "/Templates/FORMT.xlsx");
        //    string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    return File(content1, contentType1, "FORMT" + ".xlsx");
        //}

    }
}
