using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RAMMS.Business.ServiceProvider;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Web.UI.Models;
using Serilog;
using System.Net.Http;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO.ResponseBO;
using RAMMS.Repository.Interfaces;

namespace RAMMS.Web.UI.Controllers
{
    public class MAMController : Models.BaseController
    {
        private readonly IFormN1Service _formN1Service;
        private readonly IFormN2Service _formN2Service;
        private readonly IFormJServices _formJService;
        private readonly IFormQa2Service _formQa2Service;
        private readonly IFormQa2Repository _mAMQA2Repository;

        private readonly IBridgeBO _bridgeBO;
        private readonly IDDLookupBO _dDLookupBO;
        private readonly IFormABO _formABO;
        private IHostingEnvironment Environment;
        private readonly ILogger _logger;
        private readonly IDDLookUpService _ddLookupService;
        private readonly ISecurity _security;

        FormN1Model _formN1Model = new FormN1Model();
        FormN2Model _formN2Model = new FormN2Model();
        FormQa2Model _formQa2Model = new FormQa2Model();

        private readonly IRoadMasterService _roadMasterService;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFormS1Service _formS1Service;
      

        public MAMController(IDDLookupBO _ddLookupBO,
        IFormABO _FormABO,
        IHostingEnvironment _environment,
        IFormN1Service formN1Service,
        IFormN2Service formN2Service,
        IFormJServices formJServices,
        IDDLookUpService ddLookupService,
        IFormQa2Service formQa2Service,
        IFormS2Service formS2Service,
        IFormS1Service formS1Service,
        ISecurity security,
        ILogger logger, IRoadMasterService roadMaster, IUserService userService, IWebHostEnvironment webhostenvironment,
        IBridgeBO bridgeBO, IFormQa2Repository mAMQA2Repository)
        {
            _dDLookupBO = _ddLookupBO;
            Environment = _environment;
            _ddLookupService = ddLookupService;
            _roadMasterService = roadMaster;
            _userService = userService;
            _formN1Service = formN1Service ?? throw new ArgumentNullException(nameof(formN1Service));
            _formN2Service = formN2Service ?? throw new ArgumentNullException(nameof(formN2Service));
            _formJService = formJServices ?? throw new ArgumentNullException(nameof(formJServices));
            _formQa2Service = formQa2Service ?? throw new ArgumentNullException(nameof(formQa2Service));
            _formS1Service = formS1Service ?? throw new ArgumentNullException(nameof(formS1Service));
            _webHostEnvironment = webhostenvironment;
            _bridgeBO = bridgeBO;
            _mAMQA2Repository = mAMQA2Repository;
            _security = security;
        }

        #region "Dropdowns"

        public async Task LoadDropDowns(string from, string id)
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            ddLookup.Type = "Other Follow Up Action";
            ViewData["Other Follow Up Action"] = await _ddLookupService.GetDdLookup(ddLookup);
            if (from == "N1")
            {
                ddLookup.Type = "SourceTypeN1";
                ViewData["sourceType"] = await _ddLookupService.GetDdLookup(ddLookup);

                System.Collections.Generic.List<SelectListItem> lsAction = new System.Collections.Generic.List<SelectListItem>();
                lsAction.Add(new SelectListItem() { Text = "Yes", Value = "true" });
                lsAction.Add(new SelectListItem() { Text = "No", Value = "false" });

                ViewData["IsCorrectionTaken"] = lsAction;
                ViewData["NCRIssued"] = lsAction;

                ViewData["FormQARefNos"] = await _formN1Service.GetFormQA2ReferenceId(id);
                ViewData["FormS1RefNos"] = await _formN1Service.GetFormS1ReferenceId("");

                ddLookup.Type = "RMU";
                ViewData["RMU"] = await _formN1Service.GetRMU();

                ViewData["RD_Code"] = await _formN1Service.GetRoadCodesByRMU("");

                ddLookup.TypeCode = "FORM N1";
                ddLookup.Type = "Other Follow Up Action";
                ViewData["Other Follow Up Action"] = await _ddLookupService.GetDdLookup(ddLookup);
                ddLookup.TypeCode = "";
            }
            else if (from == "N2")
            {
                ddLookup.Type = "SourceTypeN2";
                ViewData["sourceType"] = await _ddLookupService.GetDdLookup(ddLookup);

                ddLookup.Type = "Region";
                ViewData["Region"] = await _ddLookupService.GetDdLookup(ddLookup);

                ViewData["FormN1RefNos"] = await _formN2Service.GetFormN1ReferenceId(id);

                ddLookup.Type = "RMU";
                ViewData["RMU"] = await _formN2Service.GetRMU();

                ViewData["RD_Code"] = await _formN2Service.GetRoadCodesByRMU("");

                ddLookup.Type = "Other Follow Up Action";
                ddLookup.TypeCode = "FORM N2";
                ViewData["Other Follow Up Action"] = await _ddLookupService.GetDdLookup(ddLookup);
                ddLookup.TypeCode = "";

            }

            else if (from == "QA2")
            {

                ViewData["FormN1RefNos"] = await _formN2Service.GetFormN1ReferenceId(id);

                ddLookup.Type = "RMU";
                ViewData["RMU"] = await _formQa2Service.GetRMU();

                ViewData["RD_Code"] = await _formQa2Service.GetRoadCodesByRMU("");

                ddLookup.Type = "Act-FormD";
                ViewData["Activity"] = await _ddLookupService.GetLookUpCodeTextConcat(ddLookup);

                ddLookup.Type = "Week No";
                ViewData["WeekNo"] = await _ddLookupService.GetDdDescValue(ddLookup);

                ddLookup.Type = "QA2-WWS/13A Fol";
                ViewData["WWS"] = await _ddLookupService.GetDdDescValue(ddLookup);

                ddLookup.Type = "QA2-CycleType";
                ViewData["CycleType"] = await _ddLookupService.GetDdDescValue(ddLookup);

                ddLookup.Type = "QA2-Ratings";
                ViewData["Rating"] = await _ddLookupService.GetLookUpTextDescConcat(ddLookup);
                ddLookup.Type = "Site Ref";
                ViewData["lookupSiteReg"] = await _ddLookupService.GetLookUpValueDesc(ddLookup);

                ViewData["USERVER"] = _userService.GetUserSelectList(null);

                ViewData["Supervisor"] = await _userService.GetSupervisor();

                ddLookup.Type = "QA2_Source Type";
                ViewData["SourceType"] = await _ddLookupService.GetDdLookup(ddLookup);
                ViewData["DefectCode"] = await _ddLookupService.GetAllDefectCode();
                ddLookup.Type = "Month";
                ViewData["Month"] = await _ddLookupService.GetDdDescValue(ddLookup);

            }
            ddLookup.Type = "Month";
            ViewData["Months"] = await _ddLookupService.GetDdLookup(ddLookup);

            ddLookup.Type = "Year";
            ViewData["Year"] = await _ddLookupService.GetDdLookup(ddLookup);

            ddLookup.Type = "Service Provider";
            ddLookup.TypeCode = "SP";
            ViewData["Service Provider"] = await _ddLookupService.GetDdLookup(ddLookup);

           
            ViewData["Users"] = _userService.GetUserSelectList(null);


        }
        #endregion

        #region "Form N1"

        [HttpPost]
        public async Task<IActionResult> RMUSecRoad(RequestDropdownFormA request)
        {
            if (string.IsNullOrEmpty(request.RoadCode) &&
                string.IsNullOrEmpty(request.RMU) &&
                string.IsNullOrEmpty(request.Section))
            {
                FormASearchDropdown ddl = new FormASearchDropdown();
                DDLookUpDTO ddLookup = new DDLookUpDTO();
                RoadMasterRequestDTO roadMasterReq = new RoadMasterRequestDTO();
            }
            return Json(_formJService.GetDropdown(request));
        }

        public async Task<IActionResult> FormN1()
        {
            await LoadDropDowns("N1", "");
            return View("~/Views/MAM/FormN1/FormN1.cshtml", _formN1Model);
        }

        public IActionResult N1Download(int id)
        {
            var content1 = _formN1Service.FormDownload("FORMN1", id, Environment.WebRootPath + "/Templates/FORMN1.xlsx");
            string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(content1, contentType1, "FORMN1" + ".xlsx");
        }

        [HttpPost]
        public async Task<IActionResult> LoadFormN1List(DataTableAjaxPostModel<FormN1SearchGridDTO> formNFilter) 
        {
            string searchByDate = "", years = "", day = "", month = "";
            if (Request.Form.ContainsKey("columns[0][search][value]"))
            {
                formNFilter.filterData.SmartInputValue = Request.Form["columns[0][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[1][search][value]"))
            {
                formNFilter.filterData.RMU = Request.Form["columns[1][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[2][search][value]"))
            {

                formNFilter.filterData.Road_Code = Request.Form["columns[2][search][value]"].ToString();

            }
            if (Request.Form.ContainsKey("columns[3][search][value]"))
            {
                var tmp = Request.Form["columns[3][search][value]"].ToString();
                formNFilter.filterData.IssueMonth = tmp != "" ? Convert.ToInt32(tmp) : (int?)null;
            }

            if (Request.Form.ContainsKey("columns[4][search][value]"))
            {
                searchByDate = Request.Form["columns[4][search][value]"].ToString(); //yyyy-mm-dd
                if (searchByDate != "" && searchByDate.IndexOf("-") >= 0)
                {
                    years = searchByDate.Split("-")[0];
                    month = searchByDate.Split("-")[1];
                    day = searchByDate.Split("-")[2];
                    DateTime dt = new DateTime(Convert.ToInt32(years), Convert.ToInt32(month), Convert.ToInt32(day));
                    formNFilter.filterData.IssueFrom = dt;
                }

            }
            if (Request.Form.ContainsKey("columns[5][search][value]"))
            {
                searchByDate = Request.Form["columns[5][search][value]"].ToString(); //yyyy-mm-dd
                if (searchByDate != "" && searchByDate.IndexOf("-") >= 0)
                {
                    years = searchByDate.Split("-")[0];
                    month = searchByDate.Split("-")[1];
                    day = searchByDate.Split("-")[2];
                    DateTime dt = new DateTime(Convert.ToInt32(years), Convert.ToInt32(month), Convert.ToInt32(day));
                    formNFilter.filterData.IssueTo = dt;
                }
            }
            FilteredPagingDefinition<FormN1SearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormN1SearchGridDTO>();
            filteredPagingDefinition.Filters = formNFilter.filterData;
            filteredPagingDefinition.RecordsPerPage = formNFilter.length;
            filteredPagingDefinition.StartPageNo = formNFilter.start; 

            if (formNFilter.order != null)
            {
                filteredPagingDefinition.ColumnIndex = formNFilter.order[0].column;
                filteredPagingDefinition.sortOrder = formNFilter.order[0].SortOrder == SortDirection.Asc ? DTO.Wrappers.SortOrder.Ascending : DTO.Wrappers.SortOrder.Descending;
            }


            var result = await _formN1Service.GetFilteredFormN1Grid(filteredPagingDefinition).ConfigureAwait(false);

            return Json(new { draw = formNFilter.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });


        }

        [HttpPost]
        public async Task<IActionResult> GetDivisionByRoadCode(string roadCode, string from, string issueDate, int refId)
        {
            RoadMasterRequestDTO _rmRoad = new RoadMasterRequestDTO();
            var id = "";
            int month = 0;
            int year = 0;
            string runningNo = "";
            _rmRoad.RoadCode = roadCode;

            int _id = 0;
            bool isExist = false;
            var _RMAllData = await _roadMasterService.GetAllRoadCodeData(_rmRoad);
            DateTime dt = Convert.ToDateTime(issueDate);
            month = string.IsNullOrEmpty(dt.ToString()) ? 0 : dt.Month;
            year = string.IsNullOrEmpty(dt.ToString()) ? 0 : dt.Year;
          
            (_id, isExist) = this._formN1Service.CheckExistence(roadCode, month, year);
            if (refId == 0)
            {

                if (roadCode != "Select Road Code" && roadCode != null && from != null)
                {
                    if (_id > 1 && isExist == false)
                    {
                        id = await GetIdByRoadCode(roadCode, issueDate, from);
                    }
                }
                var obj = new
                {
                    _RMAllData = _RMAllData,
                    id = id,
                    IsExists = isExist
                };
                return Json(obj);
            }
            else
            {
                var obj = new
                {
                    _RMAllData = _RMAllData,
                    id = _id,
                    IsExists = isExist
                };
                return Json(obj);
            }

        }

        [HttpPost]
        public async Task<string> GetIdByRoadCode(string rdCode, string rpDate, string from)
        {
            string id = "";
            string runningNo = "";
            int month = 0;
            int year = 0;
            int maxNo = 0;

            if (from == "N1")
                maxNo = await _formN1Service.GetMaxCount();
            else
                maxNo = await _formN2Service.GetMaxCount();
            maxNo = maxNo + 1;

            runningNo = (maxNo) < 10 ? "000" + maxNo.ToString() : (maxNo < 100) ? "00" + maxNo.ToString() : (maxNo < 1000) ? "0" + maxNo.ToString() : maxNo.ToString();

            if (rpDate != null)
            {
                DateTime dt = Convert.ToDateTime(rpDate);
                month = string.IsNullOrEmpty(dt.ToString()) ? 0 : dt.Month;
                year = string.IsNullOrEmpty(dt.ToString()) ? 0 : dt.Year;
            }
            //MM/N1/RoadCode/Month/AssetGroup/0001 (RunningNo)-Year
            if (rdCode != null && month != 0 && year != 0)
            {
                id = "MM/" + from + "/" + rdCode + "/" + month + "/" + runningNo + "-" + year;
            }
            return id;
        }

        public async Task<IActionResult> EditFormN1(int headerId, string view)
        {
            base.LoadLookupService(Common.GroupNames.JKRSSuperiorOfficerSO);
            await LoadDropDowns("N1", headerId == 0 ? "" : headerId.ToString());
            _formN1Model.SaveFormN1Model = new FormN1HeaderRequestDTO();

            if (headerId > 0)
            {
                var result = await _formN1Service.GetFormN1WithDetailsByNoAsync(headerId);
                _formN1Model.viewm = result.SubmitStatus == true ? "1" : view;
                _formN1Model.SaveFormN1Model = result;
            }
            else
            {
                _formN1Model.viewm = view != null ? view : "0";
            }

            return View("~/Views/MAM/FormN1/_AddFormN1.cshtml", _formN1Model);
        }

        [HttpPost]
        public async Task<IActionResult> HeaderListFormN1Delete(int headerId)
        {
            int RowsAffected = 0;
            RowsAffected = await _formN1Service.DeActivateFormN1Async(headerId);
            return Json(RowsAffected);

        }

        [HttpPost]
        public IActionResult GetReferenceNo(string rdCode, string rpDate)
        {
            string id = "";
            int month = 0;
            int year = 0;
            if (rpDate != null)
            {
                DateTime dt = Convert.ToDateTime(rpDate);
                month = string.IsNullOrEmpty(dt.ToString()) ? 0 : dt.Month;
                year = string.IsNullOrEmpty(dt.ToString()) ? 0 : dt.Year;
            }

            (int _id, bool isExist) = this._formN1Service.CheckExistence(rdCode, month, year);

            if (rdCode != null && month != 0 && year != 0)
            {
                id = $"MAM/FormN1/{rdCode}/{month}/{_id}-{year}";
            }
            return Json(new { Reference = id, IsExists = isExist });
        }

        [HttpPost]
        public async Task<IActionResult> SaveN1Hdr(FormN1HeaderRequestDTO saveObj)
        {
            int rowsAffected = 0;
            bool refIdStatus = false;
            bool isRefNoExists = false;
            string errorMessage = "";
            int refNo = 0;
            FormN1HeaderRequestDTO saveRequestObj = new FormN1HeaderRequestDTO();
            saveRequestObj = saveObj;
            if (saveObj.No == 0)
            {
                refIdStatus = await _formN1Service.CheckHdrRefereceId(saveObj.NCNo);
                if (!refIdStatus)
                {
                    if (saveObj.SourceType == "New")
                    {
                        refNo = await _formN1Service.SaveFormN1Async(saveRequestObj);
                    }
                    else
                    {
                        isRefNoExists = await _formN1Service.CheckHdrRefereceNo(saveObj.ReferenceID);
                        if (!isRefNoExists)
                            refNo = await _formN1Service.SaveFormN1Async(saveRequestObj);
                        else
                        {

                            refNo = -1;
                            errorMessage = "Record already exist for this Ref No. " + saveObj.ReferenceID;
                        }
                    }
                }
                else
                {
                    refNo = -1;
                    errorMessage = "Form N1 No. already exist.";
                }
            }
            else
            {
                rowsAffected = await _formN1Service.UpdateFormN1Async(saveRequestObj);
                refNo = int.Parse(saveObj.No.ToString());
            }
            var result = new { refNo, errorMessage };
           
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetRoadCodeByRMU(string rmu)
        {
            var obj = await _formN1Service.GetRoadCodesByRMU(rmu);
            return Json(obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetSectionCodeByRMU(string rmu)
        {
            var obj = await _formN1Service.GetSectionCodesByRMU(rmu);
            return Json(obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetUsers(string rmu)
        {
            
            var obj = _userService.GetUserSelectList(null);
            return Json(obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetUserById(int id) => Json(await _userService.GetUserNameByCode(new UserRequestDTO { UserId = id }));

        [HttpPost]
        public async Task<IActionResult> GetReferenceId(string form, string roadCode)
        {
            if (form == "Form S1")
            {
                var obj = await _formN1Service.GetFormS1ReferenceId(roadCode);
                return Json(obj);
            }
            else if (form == "Form Qa2")
            {
                var obj = await _formN1Service.GetFormQA2ReferenceId(roadCode);
                return Json(obj);
            }

            return Json(new StringContent("{}"));
        }

        [HttpPost]
        public async Task<IActionResult> GetFormData(int id, string form)
        {
            if (form == "Form S1")
            {
                var obj = await _formN1Service.GetFormS1Data(id);
                return Json(obj);
            }
            else if (form == "Form Qa2")
            {
                var obj = await _formN1Service.GetFormQa2Data(id);
                return Json(obj);
            }
            else if (form == "Form N2")
            {
                var obj = await _formN1Service.GetFormN1WithDetailsByNoAsync(id);
                return Json(obj);
            }

            return Json(new StringContent("{}"));
        }
        #endregion

        #region "Form N2"
        public async Task<IActionResult> FormN2()
        {
            await LoadDropDowns("N2", "");
            return View("~/Views/MAM/FormN2/FormN2.cshtml");
        }

        public IActionResult N2Download(int id)
        {
            var content1 = _formN2Service.FormDownload("FORMN2", id, Environment.WebRootPath + "/Templates/FORMN2.xlsx");
            string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(content1, contentType1, "FORMN2" + ".xlsx");
        }

        [HttpPost]
        public async Task<IActionResult> LoadFormN2List(DataTableAjaxPostModel<FormN2SearchGridDTO> formNFilter) 
        {
            string searchByDate = "", years = "", day = "", month = "";

            if (Request.Form.ContainsKey("columns[0][search][value]"))
            {
                formNFilter.filterData.SmartInputValue = Request.Form["columns[0][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[1][search][value]"))
            {
                formNFilter.filterData.RMU = Request.Form["columns[1][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[2][search][value]"))
            {

                formNFilter.filterData.Road_Code = Request.Form["columns[2][search][value]"].ToString();

            }
            if (Request.Form.ContainsKey("columns[3][search][value]"))
            {
                var tmp = Request.Form["columns[3][search][value]"].ToString();
                formNFilter.filterData.IssueMonth = tmp != "" ? Convert.ToInt32(tmp) : (int?)null;
            }

            if (Request.Form.ContainsKey("columns[4][search][value]"))
            {
                searchByDate = Request.Form["columns[4][search][value]"].ToString(); //yyyy-mm-dd
                if (searchByDate != "" && searchByDate.IndexOf("-") >= 0)
                {
                    years = searchByDate.Split("-")[0];
                    month = searchByDate.Split("-")[1];
                    day = searchByDate.Split("-")[2];
                    DateTime dt = new DateTime(Convert.ToInt32(years), Convert.ToInt32(month), Convert.ToInt32(day));
                    formNFilter.filterData.IssueFrom = dt;
                }

            }
            if (Request.Form.ContainsKey("columns[5][search][value]"))
            {
                searchByDate = Request.Form["columns[5][search][value]"].ToString(); //yyyy-mm-dd
                if (searchByDate != "" && searchByDate.IndexOf("-") >= 0)
                {
                    years = searchByDate.Split("-")[0];
                    month = searchByDate.Split("-")[1];
                    day = searchByDate.Split("-")[2];
                    DateTime dt = new DateTime(Convert.ToInt32(years), Convert.ToInt32(month), Convert.ToInt32(day));
                    formNFilter.filterData.IssueTo = dt;
                }
            }

            FilteredPagingDefinition<FormN2SearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormN2SearchGridDTO>();
            filteredPagingDefinition.Filters = formNFilter.filterData;
            filteredPagingDefinition.RecordsPerPage = formNFilter.length;
            filteredPagingDefinition.StartPageNo = formNFilter.start;

            if (formNFilter.order != null)
            {
                filteredPagingDefinition.ColumnIndex = formNFilter.order[0].column;
                filteredPagingDefinition.sortOrder = formNFilter.order[0].SortOrder == SortDirection.Asc ? DTO.Wrappers.SortOrder.Ascending : DTO.Wrappers.SortOrder.Descending;
            }

            var result = await _formN2Service.GetFilteredFormN2Grid(filteredPagingDefinition).ConfigureAwait(false);

            return Json(new { draw = formNFilter.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }

        public async Task<IActionResult> EditFormN2(int headerId, string view)
        {
            await LoadDropDowns("N2", headerId == 0 ? "" : headerId.ToString());
            _formN2Model.SaveFormN2Model = new FormN2HeaderRequestDTO();

            if (headerId > 0)
            {
                var result = await _formN2Service.GetFormN2WithDetailsByNoAsync(headerId);
                _formN2Model.viewm = result.SubmitStatus == true ? "1" : view;
                _formN2Model.SaveFormN2Model = result;
            }
            else
            {
                _formN2Model.viewm = view != null ? view : "0";
            }

            return PartialView("~/Views/MAM/FormN2/_AddFormN2.cshtml", _formN2Model);
        }

        [HttpPost]
        public async Task<IActionResult> HeaderListFormN2Delete(int headerId)
        {
            int RowsAffected = 0;
            RowsAffected = await _formN2Service.DeActivateFormN2Async(headerId);
            return Json(RowsAffected);

        }

        [HttpPost]
        public IActionResult GetN2ReferenceNo(string rdCode, string rpDate)
        {
            string id = "";
            int month = 0;
            int year = 0;
            if (rpDate != null)
            {
                DateTime dt = Convert.ToDateTime(rpDate);
                month = string.IsNullOrEmpty(dt.ToString()) ? 0 : dt.Month;
                year = string.IsNullOrEmpty(dt.ToString()) ? 0 : dt.Year;
            }

            (int _id, bool isExist) = this._formN1Service.CheckExistence(rdCode, month, year);

            if (rdCode != null && month != 0 && year != 0)
            {
                id = $"MAM/FormN2/{rdCode}/{month}/{_id}-{year}";
            }
            return Json(new { Reference = id, IsExists = isExist });
        }

        [HttpPost]
        public async Task<IActionResult> SaveN2Hdr(FormN2HeaderRequestDTO saveObj)
        {
            int RowsAffected = 0;
            bool refIdStatus = false;
            int refNo = 0;
            FormN2HeaderRequestDTO saveRequestObj = new FormN2HeaderRequestDTO();
            saveRequestObj = saveObj;
            if (saveObj.No == 0 || saveObj.No == null)
            {
                refIdStatus = await _formN2Service.CheckHdrRefereceId(saveObj.NcrNo);
                if (!refIdStatus)
                    refNo = await _formN2Service.SaveFormN2Async(saveRequestObj);
                else
                    refNo = -1;
            }
            else
            {
                RowsAffected = await _formN2Service.UpdateFormN2Async(saveRequestObj);
                refNo = int.Parse(saveObj.No.ToString());
            }
            
            return Json(refNo);
        }

        [HttpPost]
        public async Task<IActionResult> GetN1ReferenceId(string form, string roadCode)
        {
            var obj = await _formN2Service.GetFormN1ReferenceId(roadCode);
            return Json(obj);
        }

        #endregion

        #region "Form QA2"

        [HttpPost]
        public async Task<IActionResult> LoadQA2List(DataTableAjaxPostModel<FormQa2SearchGridDTO> formFilter) 
        {
            if (Request.Form.ContainsKey("columns[0][search][value]"))
            {
                formFilter.filterData.SmartInputValue = Request.Form["columns[0][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[1][search][value]"))
            {
                formFilter.filterData.RMU = Request.Form["columns[1][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[2][search][value]"))
            {

                formFilter.filterData.Road_Code = Request.Form["columns[2][search][value]"].ToString();

            }
            if (Request.Form.ContainsKey("columns[3][search][value]"))
            {
                formFilter.filterData.ActivityCode = Request.Form["columns[3][search][value]"].ToString();
            }

            if (Request.Form.ContainsKey("columns[4][search][value]"))
            {
                formFilter.filterData.WWS = Request.Form["columns[4][search][value]"].ToString();
            }

            FilteredPagingDefinition<FormQa2SearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormQa2SearchGridDTO>();
            filteredPagingDefinition.Filters = formFilter.filterData;
            filteredPagingDefinition.RecordsPerPage = formFilter.length;
            filteredPagingDefinition.StartPageNo = formFilter.start;

            var result = await _formQa2Service.GetFilteredFormQa2Grid(filteredPagingDefinition).ConfigureAwait(false);

            return Json(new { draw = formFilter.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }

        public async Task<IActionResult> EditFormQa2(int headerId, string view)
        {
            await LoadDropDowns("QA2", headerId == 0 ? "" : headerId.ToString());
            _formQa2Model.SaveFormQa2Model = new FormQa2HeaderRequestDTO();

            if (headerId > 0)
            {
                var result = await _formQa2Service.GetRmFormQa2Hdr(headerId);
                if (result != null)
                    _formQa2Model.SaveFormQa2Model = result;
                _formQa2Model.viewm = view != null ? view : "0";
            }
            else
            {
                _formQa2Model.viewm = view != null ? view : "0";
            }
            return PartialView("~/Views/MAM/FormQa2/_AddFormQa2.cshtml", _formQa2Model);
        }
        #endregion

        #region Form S1

        public IActionResult FormS1()
        {
           // return View("~/Views/MAM/FormS1/OLD_FormS1.cshtml");
            return View("~/Views/MAM/FormS1/FormS1.cshtml");
        }

        #endregion

        #region "Form QA2 Grid"

        public IActionResult QA2()
        {
            LoadLookupService("RMU", "RD_Name", "RD_Code", "User");
            bool isModify = _security.IsPCModify(ModuleNameList.Routine_Maintanance_Work);
            bool isDelete = _security.IsPCDelete(ModuleNameList.Routine_Maintanance_Work);
            var grid = new Models.CDataTable() { Name = "tblHeaderGrid", APIURL = "/MAM/QA2LoadHeaderList", LeftFixedColumn = 1, IsDelete = isDelete, IsModify = isModify };
            grid.Columns.Add(new CDataColumns() { data = null, title = "Action", sortable = false, render = "mAMQA2.ActionRender", className = "headcol" });
            
            grid.Columns.Add(new CDataColumns() { data = "RefID", title = "Reference No" });
            grid.Columns.Add(new CDataColumns() { data = "RMU", title = "RMU" });
            grid.Columns.Add(new CDataColumns() { data = "RoadCode", title = "Road Code" });
            grid.Columns.Add(new CDataColumns() { data = "RoadName", title = "Road Name" });
            grid.Columns.Add(new CDataColumns() { data = "CrewSupName", title = "Crew Supervisor" });
            grid.Columns.Add(new CDataColumns() { data = "Status", title = "Status" });
            return View("~/Views/MAM/FormQa2/FormQa2.cshtml", grid);
        }

        [HttpPost]
        public async Task<IActionResult> FormQA2HeaderListDelete(int headerId)
        {
            int RowsAffected = 0;
            RowsAffected = await _formQa2Service.DeActivateFormQA2Async(headerId);
            return Json(RowsAffected);

        }
     
        [HttpPost]
        public async Task<IActionResult> QA2LoadHeaderList(DataTableAjaxPostModel searchData)
        {
            return Json(await _mAMQA2Repository.GetFormQa2HeaderGrid(searchData), JsonOption());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHeader(FormQa2HeaderRequestDTO request)
        {
            var responseDto = await _formQa2Service.UpdateQa2Hdr(request);
            return Json(responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> SaveHeader(FormQa2HeaderRequestDTO request)
        {
            var responseDto = await _formQa2Service.SaveFormQa2Hdr(request);
            return Json(responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> GetQa2ReferenceNo(string roadCode, string rmu, string month, string year)
        {
            string refNo = "";

            (int id, bool isExist) lastSNo = await _formQa2Service.CheckExistence(roadCode, rmu, month, year);
           
            refNo = $"MM/FORM QA2/{rmu}/{roadCode}/{month}-{year}/{lastSNo.id + (lastSNo.isExist ? 0 : 1)}";
            return Json(new { reference = refNo, headerNo = lastSNo.id, isExists = lastSNo.isExist });
        }

        [HttpPost]
        public async Task<IActionResult> GetFilteredQa2Details(DataTableAjaxPostModel<FormQa2SearchGridDTO> searchData)
        {
            FilteredPagingDefinition<FormQa2SearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormQa2SearchGridDTO>();
            filteredPagingDefinition.Filters = searchData.filterData;
            filteredPagingDefinition.RecordsPerPage = searchData.length;
            filteredPagingDefinition.StartPageNo = searchData.start; 
            var result = await _formQa2Service.GetFilteredFormQa2DetailsGrid(filteredPagingDefinition).ConfigureAwait(false);
            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });

        }

        [HttpPost]
        public async Task<IActionResult> GetQa2Details(int headerId, int id, string hdrRefNo, string roadCode)
        {
            await LoadDropDowns("QA2", headerId == 0 ? "" : headerId.ToString());
            ViewData["FormS1RefNos"] = await _formN1Service.GetFormS1ReferenceId(roadCode);
            FormQa2DtlRequestDTO obj = new FormQa2DtlRequestDTO();
            obj.FormQA2HeaderRefNo = headerId;
            if (id != 0)
            {
                obj = await _formQa2Service.GetFormWithDetailsByNoAsync(id);
            }
            else
            {
                var snro = await _formQa2Service.DtlSrNo(headerId);
                obj.RefId = hdrRefNo + "/" + snro;
            }
            return PartialView("~/Views/MAM/FormQa2/_AddDetailFormQA2.cshtml", obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetS1RefDetails(int id)
        {
            var DefCode = "";
            var result = await _formS1Service.FindS1Details(id);
            if (result.FsidFormType == "FormA")
            {
                DefCode = await _formQa2Service.GetFormADetailByIdAsync(Convert.ToInt32(result.FsidFormTypeRefNo));

                return Json(new
                {

                    SiteRef = result.FsidFormASiteRef,
                    ActCode = result.FsidActCode,
                    ChainageFrom = result.FsidFrmChKm,
                    ChainageFromDec = result.FsidFrmChM,
                    ChainageTo = result.FsidToChKm,
                    ChainageToDec = result.FsidToChM,
                    Defect = DefCode
                });
            }
            else
            {
                return Json(new
                {

                   
                    ActCode = result.FsidActCode,
                    ChainageFrom = result.FsidFrmChKm,
                    ChainageFromDec = result.FsidFrmChM,
                    ChainageTo = result.FsidToChKm,
                    ChainageToDec = result.FsidToChM,
                   
                });
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> SaveQa2Detail(FormQa2DtlRequestDTO request)
        {
            var row = await _formQa2Service.SaveFormQa2Detail(request);
            return Json(row);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteQa2Detail(int id)
        {
            int affected = await _formQa2Service.RemoveDetail(id);
            return Json(affected);
        }

        public IActionResult Qa2Print(int id)
        {
            var content1 = _formQa2Service.FormDownload("FORMQa2", id, Environment.WebRootPath + "/Templates/Form QA2.xlsx");
            string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(content1, contentType1, "FORMQa2" + ".xlsx");
        }

        #endregion
    }
}
