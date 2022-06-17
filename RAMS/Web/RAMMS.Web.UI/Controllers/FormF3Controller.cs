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
    public class FormF3Controller : BaseController
    {

        private IFormF3Service _formF3Service;
        private readonly IFormJServices _formJService;
        private ISecurity _security;
        private IWebHostEnvironment _environment;
        private IUserService _userService;
        private IRoadMasterService _roadMasterService;
        public FormF3Controller(
            IFormF3Service service,
            ISecurity security,
            IUserService userService,
            IWebHostEnvironment webhostenvironment,
            IRoadMasterService roadMasterService,
             IFormJServices formJServices)
        {
            _userService = userService;
            _formF3Service = service;
            _security = security;
            _environment = webhostenvironment;
            _roadMasterService = roadMasterService;
            _formJService = formJServices ?? throw new ArgumentNullException(nameof(formJServices));
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
            var result = await _formF3Service.GetHeaderList(filteredPagingDefinition);
            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });

            return null;
        }

        public async Task<IActionResult> GetDetailList(DataTableAjaxPostModel<FormF3DtlResponseDTO> searchData)
        {
            FilteredPagingDefinition<FormF3DtlResponseDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormF3DtlResponseDTO>();

            filteredPagingDefinition.Filters = searchData.filterData;
            if (searchData.order != null)
            {
                filteredPagingDefinition.ColumnIndex = searchData.order[0].column;
                filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }
            filteredPagingDefinition.RecordsPerPage = searchData.length; //Convert.ToInt32(Request.Form["length"]);
            filteredPagingDefinition.StartPageNo = searchData.start; //Convert.ToInt32(Request.Form["start"]); //TODO
            var result = await _formF3Service.GetDetailList(filteredPagingDefinition);
            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });

        }


        public async Task<IActionResult> Add(int id, int view)
        {
            LoadLookupService("Supervisor", "User");

            FormASearchDropdown ddl = _formJService.GetDropdown(new RequestDropdownFormA { });
            ViewData["SectionCode"] = ddl.Section.Select(s => new SelectListItem { Text = s.Text, Value = s.Value }).ToArray();

            FormF3Model _model = new FormF3Model();
            if (id > 0)
            {
                _model.FormF3 = await _formF3Service.GetHeaderById(id);

                if (_model.FormF3.Source == "New")
                    ViewData["Asset"] = _formF3Service.GetAssetDetails("New");
                else
                    ViewData["Asset"] = _formF3Service.GetAssetDetails("G1G2");
            }
            else
            {
                ViewData["Asset"] = _formF3Service.GetAssetDetails("New");
            }


            _model.FormF3 = _model.FormF3 ?? new FormF3ResponseDTO();
            _model.view = view;

            if ((_model.FormF3.InspectedBy == null || _model.FormF3.InspectedBy == 0) && _model.FormF3.Status == RAMMS.Common.StatusList.Submitted)
            {
                _model.FormF3.InspectedBy = _security.UserID;
                _model.FormF3.InspectedDate = DateTime.Today;
                _model.FormF3.InspectedBySign = true;
            }

            return PartialView("~/Views/FormF3/_AddFormF3.cshtml", _model);
        }



        public async Task<IActionResult> SaveFormF3(FormF3Model frm)
        {
            int refNo = 0;
            frm.FormF3.ActiveYn = true;
            if (frm.FormF3.PkRefNo == 0)
            {
                frm.FormF3 = await _formF3Service.SaveFormF3(frm.FormF3);
                //frm.RefNoDS = _formF3Service.FindRefNoFromG1(frm.FormF3);


                return Json(new { FormExist = frm.FormF3.FormExist, RefId = frm.FormF3.PkRefId, PkRefNo = frm.FormF3.PkRefNo, Status = frm.FormF3.Status, Source = frm.FormF3.Source });
            }
            else
            {
                if (frm.FormF3.Status == "Initialize")
                    frm.FormF3.Status = "Saved";
                refNo = await _formF3Service.Update(frm.FormF3);
            }
            return Json(refNo);


        }


        public async Task<IActionResult> SaveFormF3Dtl(FormF3Model frm)
        {
            int? refNo = 0;

            frm.FormF3Dtl.Ff3hPkRefNo = frm.FormF3.PkRefNo;
            if (frm.FormF3Dtl.PkRefNo == 0)
            {
                refNo = _formF3Service.SaveFormF3Dtl(frm.FormF3Dtl);

            }
            else
            {
                _formF3Service.UpdateFormF3Dtl(frm.FormF3Dtl);
            }
            return Json(refNo);


        }

        public async Task<IActionResult> DeleteFormF3(int id)
        {
            int? rowsAffected = 0;
            rowsAffected = _formF3Service.DeleteFormF3(id);
            return Json(rowsAffected);
        }

        public async Task<IActionResult> DeleteFormF3Dtl(int id)
        {
            int? rowsAffected = 0;
            rowsAffected = _formF3Service.DeleteFormF3Dtl(id);
            return Json(rowsAffected);
        }


    }
}
