using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Business.ServiceProvider;
using RAMMS.Domain.Models;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Dynamic;
using X.PagedList.Mvc.Core;
using X.PagedList;
using RAMMS.DTO.RequestBO;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Common;
using Serilog;
using RAMMS.Web.UI.Filters;
using RAMMS.Web.UI.Helpers;
using RAMMS.Web.UI.Models;
using RAMMS.DTO.Wrappers;
using RAMMS.DTO.ResponseBO;
using Newtonsoft.Json;
using RAMMS.DTO;
using RAMMS.DTO.JQueryModel;
using System.Text.RegularExpressions;
using RAMMS.Business.ServiceProvider.Services;
using RAMMS.DTO.SearchBO;
using System.Linq;

namespace RAMMS.Web.UI.Controllers
{
    public class NODController : Controller
    {
        private readonly IFormAService _formAService;
        private readonly IFormJServices _formJService;
        private readonly IFormHService _formHService;
        private readonly IDDLookupBO _dDLookupBO;
        private readonly IFormABO _formABO;
        private IWebHostEnvironment Environment;
        private readonly IDDLookUpService _ddLookupService;
        FormAModel _formAModel = new FormAModel();
        private readonly IRoadMasterService _roadMasterService;
        private readonly IAssetsService _assetsService;
        private readonly IUserService _userService;
        private readonly IFormAImageService _formaImgService;
        private readonly IFormJImageService _formjImgService;
        private readonly IFormHImageService _formhImgService;
        private readonly ISecurity _security;

        public NODController(IDDLookupBO _ddLookupBO,
            IUserService userService,
            IFormABO _formAbo,
            IWebHostEnvironment _environment,
            IFormAImageService formaImgService,
            IFormAService formAService,
            IFormJServices formJServices,
            IFormHService formHService,
            IDDLookUpService ddLookupService,
            IFormJImageService _formjImgService,
            IFormHImageService _formhImgService,
            ISecurity security,
        ILogger logger, IRoadMasterService roadMaster, IAssetsService assetsService)
        {
            this._security = security;
            _formHService = formHService;
            this._formjImgService = _formjImgService;
            _formaImgService = formaImgService;
            _dDLookupBO = _ddLookupBO;
            _formABO = _formAbo;
            Environment = _environment;
            _ddLookupService = ddLookupService;
            this._formJService = formJServices;
            _roadMasterService = roadMaster;
            _assetsService = assetsService;
            _userService = userService;
            this._formhImgService = _formhImgService;
            _formAService = formAService ?? throw new ArgumentNullException(nameof(formAService));
        }

        //[ValidateAntiForgeryToken]
        [CAuthorize(ModuleName = ModuleNameList.NOD)]
        public async Task<IActionResult> Index([FromQuery(Name = "vid")] string viewId)
        {
            ViewBag.ViewId = viewId;
            await LoadDropDowns();
            FormAModel formAModel = new FormAModel();
            FormASearchGridDTO filterData = new FormASearchGridDTO();
            formAModel.SearchObj = filterData;

            return View("~/Views/NOD/FormA/landingpage.cshtml", _formAModel);
        }

        public IActionResult FormADownload(int id)
        {
            var content1 = _formAService.FormDownload("FORMA", id, Environment.WebRootPath + "/Templates/FORMA.xlsx");
            string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(content1, contentType1, "FORMA" + ".xlsx");
        }

        public IActionResult FormJDownload(int id)
        {
            var content1 = _formJService.FormDownload("FORMJ", id, Environment.WebRootPath + "/Templates/FORMJ.xlsx");
            string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(content1, contentType1, "FORMJ" + ".xlsx");
        }

        public IActionResult HFormJDownload(int id)
        {
            var content1 = _formHService.FormDownload("FORMH", id, Environment.WebRootPath + "/Templates/FORMH.xlsx");
            string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(content1, contentType1, "FORMH" + ".xlsx");
        }

        public async Task LoadDropDowns()
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            RoadMasterRequestDTO roadMasterReq = new RoadMasterRequestDTO();
            ddLookup.Type = "FormA_Assets";
            ViewData["AssetListing"] = await _ddLookupService.GetDdLookup(ddLookup);

            var ddl = _formAService.GetDropdown(new RequestDropdownFormA { });

            ViewData["RD_Code"] = ddl.RoadCode.Select(s => new SelectListItem { Text = s.Text, Value = s.Value }).ToArray();
            ddLookup.Type = "RD_Name";
            ViewData["RD_Name"] = await _ddLookupService.GetDdDescValue(ddLookup);
            ViewData["Section_Code"] = ddl.Section.Select(s => new SelectListItem { Text = s.Text, Value = s.Value }).ToArray();

            ddLookup.Type = "RMU";
            ViewData["RMU"] = ddl.RMU.Select(s => new SelectListItem { Text = s.Text, Value = s.Value }).ToArray();

            ddLookup.Type = "Month";
            ViewData["Months"] = await _ddLookupService.GetDdDescValue(ddLookup);

            ddLookup.Type = "Year";
            ViewData["Year"] = await _ddLookupService.GetDdLookup(ddLookup);
        }

        public async Task JLoadDropDowns()
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            RoadMasterRequestDTO roadMasterReq = new RoadMasterRequestDTO();
            ddLookup.Type = "FormJ_Assets";
            ViewData["AssetListing"] = await _ddLookupService.GetDdLookup(ddLookup);

            FormASearchDropdown ddl = _formJService.GetDropdown(new RequestDropdownFormA { });

            ViewData["RD_Code"] = ddl.RoadCode.Select(s => new SelectListItem { Text = s.Text, Value = s.Value }).ToArray();
            ddLookup.Type = "RD_Name";
            ViewData["RD_Name"] = await _ddLookupService.GetDdDescValue(ddLookup);
            ViewData["Section_Code"] = ddl.Section.Select(s => new SelectListItem { Text = s.Text, Value = s.Value }).ToArray();

            ddLookup.Type = "RMU";
            ViewData["RMU"] = ddl.RMU.Select(s => new SelectListItem { Text = s.Text, Value = s.Value }).ToArray();

            ddLookup.Type = "Month";
            ViewData["Months"] = await _ddLookupService.GetDdDescValue(ddLookup);

            ddLookup.Type = "Year";
            ViewData["Year"] = await _ddLookupService.GetDdLookup(ddLookup);
        }

        public async Task HLoadDropDowns()
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            RoadMasterRequestDTO roadMasterReq = new RoadMasterRequestDTO();
            ddLookup.Type = "FormJ_Assets";
            var formJAssets = await _ddLookupService.GetDdLookup(ddLookup);

            ddLookup.Type = "FormA_Assets";
            var formAassests = await _ddLookupService.GetDdLookup(ddLookup);
            ViewData["AssetListing"] = formJAssets.Concat(formAassests);
            FormASearchDropdown ddl = _formJService.GetDropdown(new RequestDropdownFormA { });

            ViewData["RD_Code"] = ddl.RoadCode.Select(s => new SelectListItem { Text = s.Text, Value = s.Value }).ToArray();
            ddLookup.Type = "RD_Name";
            ViewData["RD_Name"] = await _ddLookupService.GetDdDescValue(ddLookup);
            ViewData["Section_Code"] = ddl.Section.Select(s => new SelectListItem { Text = s.Text, Value = s.Value }).ToArray();

            ddLookup.Type = "RMU";
            ViewData["RMU"] = ddl.RMU.Select(s => new SelectListItem { Text = s.Text, Value = s.Value }).ToArray();

            ddLookup.Type = "Month";
            ViewData["Months"] = await _ddLookupService.GetDdDescValue(ddLookup);

            ddLookup.Type = "Year";
            ViewData["Year"] = await _ddLookupService.GetDdLookup(ddLookup);
        }

        /// <summary>
        /// TO get road code drop down list
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LoadHeaderGridList(FormAModel formAModel)
        {
            FormASearchGridDTO searchObj = new FormASearchGridDTO();
            searchObj = formAModel.SearchObj;

            ViewBag.CurrentSort = formAModel.SearchObj.sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(formAModel.SearchObj.sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = formAModel.SearchObj.sortOrder == "Date" ? "date_desc" : "Date";
            if (formAModel.SearchObj.searchString != null)
            {
                formAModel.SearchObj.Page_No = 1;
            }
            else
            {
                formAModel.SearchObj.searchString = formAModel.SearchObj.currentFilter;
            }

            ViewBag.CurrentFilter = formAModel.SearchObj.searchString;
            int Size_Of_Page = (formAModel.SearchObj.pageSize ?? 1000);
            int No_Of_Page = (formAModel.SearchObj.Page_No ?? 1);
            ViewBag.psize = Size_Of_Page;
            ViewBag.PageSize = new List<SelectListItem>()
            {
             new SelectListItem() { Value="5", Text= "5" },
             new SelectListItem() { Value="10", Text= "10" },
             new SelectListItem() { Value="15", Text= "15" },
             new SelectListItem() { Value="25", Text= "25" },
             new SelectListItem() { Value="50", Text= "50" }
            };


            FilteredPagingDefinition<FormASearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormASearchGridDTO>();
            filteredPagingDefinition.Filters = searchObj;
            filteredPagingDefinition.RecordsPerPage = 5;
            filteredPagingDefinition.StartPageNo = 1; //TODO

            var result = await _formAService.GetFilteredFormAGrid(filteredPagingDefinition);
            var obj = result.PageResult;
            IPagedList<FormAHeaderResponseDTO> headerList = obj.ToPagedList(No_Of_Page, Size_Of_Page);

            _formAModel.FormAHeaderList = headerList;
            ViewBag.TotalNoRecords = headerList.TotalItemCount.ToString();
            int iPreDisplay = ((No_Of_Page) * Size_Of_Page);
            ViewBag.DisplayRecords = iPreDisplay;

            ViewBag.TotalPage = headerList.PageCount;
            var CurrentPage = (headerList.PageCount < headerList.PageNumber ? 0 : headerList.PageNumber);
            ViewBag.CurrentPage = CurrentPage;

            //Added for Temporary Count showing
            ViewBag.DisplayRecords = result.FilteredRecords;
            ViewBag.TotalNoRecords = result.TotalRecords;

            return View(_formAModel);
        }

        [HttpPost]
        public async Task<IActionResult> SearchHeaderList(FormASearchGridDTO filterData)
        {
            FormAModel formAObj = new FormAModel();
            formAObj.SearchObj = filterData;
            await LoadHeaderGridList(formAObj);
            return PartialView("~/Views/NOD/FormA/_HeaderListGrid.cshtml", _formAModel);
        }

        [HttpPost]
        public async Task<IActionResult> HeaderListDelete(int headerId)
        {
            int rowsAffected = 0;

            rowsAffected = await _formAService.DeActivateFormAAsync(headerId);
            return Json(rowsAffected);
        }

        [HttpPost]
        public async Task<IActionResult> FormHHeaderListDelete(int headerId)
        {
            int rowsAffected = 0;
            rowsAffected = await _formHService.DeleteFormH(headerId);
            return Json(rowsAffected);
        }

        [HttpPost]
        public async Task<IActionResult> DetailListDelete(int detailId)
        {
            int rowsAffected = 0;
            rowsAffected = await _formAService.DeActivateDetail(detailId);
            return Json(rowsAffected);
        }

        /// <summary>
        /// To get user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetUserById(int id) => Json(await _userService.GetUserNameByCode(new UserRequestDTO { UserId = id }));

        [HttpPost]
        public async Task<IActionResult> HeaderListEdit(DataTableAjaxPostModel<FormADetailsRequestDTO> searchData)
        {
            await LoadDropDowns();
            int headerId = searchData.filterData.HeaderNo;
            _formAModel.SaveFormAModel = new FormAHeaderRequestDTO();
            if (headerId > 0)
            {


                var Result = await _formAService.GetFormAWithDetailsByNoAsync(headerId);
                _formAModel.SaveFormAModel = Result;
                _formAModel.SaveFormAModel.Id = headerId.ToString();
            }

            searchData.length = 10;
            searchData.start = 0;

            return PartialView("~/Views/NOD/FormA/_AddFormAView.cshtml", _formAModel);
        }

        [HttpPost]
        public async Task<IActionResult> DetailedListEdit(DataTableAjaxPostModel<FormADetailsRequestDTO> searchData)
        {
            FilteredPagingDefinition<FormADetailsRequestDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormADetailsRequestDTO>();


            filteredPagingDefinition.Filters = searchData.filterData;

            filteredPagingDefinition.RecordsPerPage = searchData.length;
            filteredPagingDefinition.StartPageNo = searchData.start;  //TODO

            var result = await _formAService.GetFormADetailGrid(filteredPagingDefinition).ConfigureAwait(false);

            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }

        //Details page save and submit 
        [HttpPost]
        public async Task<IActionResult> Save(FormAHeaderRequestDTO saveObj)
        {
            int rowsAffected = 0;
            FormAHeaderRequestDTO saveRequestObj = new FormAHeaderRequestDTO();
            saveRequestObj = saveObj;
            saveRequestObj.CreatedBy = _security.UserID.ToString();
            saveRequestObj.ModBy = _security.UserID.ToString();
            saveRequestObj.ModDt = DateTime.UtcNow;
            saveRequestObj.CreatedDt = DateTime.UtcNow;
            rowsAffected = await _formAService.SaveFormAAsync(saveRequestObj);
            return Json(rowsAffected);
        }

        [HttpPost]
        public async Task<IActionResult> HeaderSave(FormAHeaderRequestDTO saveObj)
        {
            try
            {
                DataTableAjaxPostModel<FormASearchGridDTO> searchData = new DataTableAjaxPostModel<FormASearchGridDTO>();
                FormAHeaderResponseDTO formAHeaderResponseDTO = new FormAHeaderResponseDTO();

                if (saveObj.No == 0)
                {
                    saveObj.CreatedBy = _security.UserID.ToString();
                    saveObj.ModBy = _security.UserID.ToString();
                    saveObj.ModDt = DateTime.UtcNow;
                    saveObj.CreatedDt = DateTime.UtcNow;
                    formAHeaderResponseDTO = await _formAService.SaveHeaderwithResponse(saveObj);
                }

                return Json(formAHeaderResponseDTO);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        /// <summary>
        /// to get latest serial number.
        /// </summary>
        /// <param name="headerno"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetLatestSRNO(int headerno) => Json(await _formAService.LastInsertedFormDetailSRNO(headerno) + 1);

        [HttpPost]
        public async Task<IActionResult> DetailsSaveV1(FormADetailsRequestDTO saveFormADetList)
        {
            if (saveFormADetList.No == 0)
            {
                saveFormADetList.CreatedBy = _security.UserID.ToString();
            }
            saveFormADetList.ModBy = _security.UserID.ToString();
            int? detailSave = await _formAService.SaveDetailforHeaderV1(saveFormADetList);

            return Json(detailSave.GetValueOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> LoadGridList(int detailSave)
        {
            var Result = await _formAService.GetFormAWithDetailsByNoAsync(detailSave);
            _formAModel.SaveFormAModel = Result;
            return PartialView("~/Views/NOD/FormA/_DetailGridList.cshtml", _formAModel);
        }

        #region Image Section
        
        [HttpPost]
        //[DisableRequestSizeLimit]
        public async Task<IActionResult> ImageUploaded(IList<IFormFile> formFile, int assetId, List<string> photoType)
        {
            try
            {
                string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;

                int j = 0;
                if (formFile.Count > 0)
                {
                    foreach (IFormFile postedFile in formFile)
                    {
                        List<FormAImageListRequestDTO> uploadedFiles = new List<FormAImageListRequestDTO>();
                        string photo_Type = Regex.Replace(photoType[j], @"[^a-zA-Z]", "");
                        string path = Path.Combine(wwwPath, Path.Combine("Uploads", "FormADetail", assetId.ToString(), photo_Type));
                        int i = await _formaImgService.LastInsertedSRNO(assetId, photoType[j]);
                        i += 1;
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        FormAImageListRequestDTO _rmAssetImageDtl = new FormAImageListRequestDTO();
                        string fileName = Path.GetFileName(postedFile.FileName);
                        string filerename = i + "_" + photoType + "_" + fileName;

                        using (FileStream stream = new FileStream(Path.Combine(path, filerename), FileMode.Create))
                        {
                            _rmAssetImageDtl.AssetId = assetId;
                            _rmAssetImageDtl.ImageTypeCode = photoType[j];
                            _rmAssetImageDtl.SNO = i;
                            _rmAssetImageDtl.FileName = postedFile.FileName;
                            _rmAssetImageDtl.ActiveYn = true;
                            if (i < 10)
                            {
                                _rmAssetImageDtl.ImageFilenameSys = assetId.ToString() + "_" + photoType + "_" + "00" + i;
                            }
                            else if (i >= 10 && i < 100)
                            {
                                _rmAssetImageDtl.ImageFilenameSys = assetId.ToString() + "_" + photoType + "_" + "0" + i;
                            }
                            else
                            {
                                _rmAssetImageDtl.ImageFilenameSys = assetId.ToString() + "_" + photoType + "_" + i;
                            }
                            _rmAssetImageDtl.ImageFilename = $"/Uploads/FormADetail/{assetId}/{photoType}/{filerename}";

                            //AssetID_Section(Water)_incrementalNumber -- i + "_" + _rmAssetImageDtl.assetId +_rmAssetImageDtl.ImageTypeCode + "_" + fileName;
                            //_rmAssetImageDtl.ImageFilename = _httpContext.HttpContext.Request.Host.Value + "\\" + "wwwroot" + "\\" + "Uploads" + "\\" + assetGrpCode[0] + "\\" + id + "\\" + photoType +"\\" + filerename;
                            postedFile.CopyTo(stream);

                            //ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                        }
                        _rmAssetImageDtl.ModifyBy = _security.UserID.ToString();
                        _rmAssetImageDtl.CreatedBy = _security.UserID.ToString();
                        _rmAssetImageDtl.ModifyDate = DateTime.UtcNow;
                        _rmAssetImageDtl.CreatedDate = DateTime.UtcNow;
                        uploadedFiles.Add(_rmAssetImageDtl);
                        await _formaImgService.SaveImageDtl(uploadedFiles);
                        j = j + 1;
                    }
                }
                else
                {
                    return BadRequest("No file found to upload");
                }
                return Json($"Successfully Uploaded {formFile.Count} file(s");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> GetImageList(int assetPk, string assetgroup)
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            FormAModel assetsModel = new FormAModel();
            assetsModel.AssetimageList = new List<FormAImageListRequestDTO>();
            assetsModel.ImageTypeList = new List<string>();
            ddLookup.Type = "Photo Type";
            assetgroup = _formAService.GetAssetCodeByName(assetgroup);
            ddLookup.TypeCode = assetgroup;
            assetsModel.PhotoType = await _ddLookupService.GetDdLookup(ddLookup);
            assetsModel.AssetimageList = await _formaImgService.GetAllImageByAssetPK(assetPk);
            assetsModel.ImageTypeList = assetsModel.AssetimageList.Select(c => c.ImageTypeCode).Distinct().ToList();
            return PartialView("~/Views/NOD/FormA/_PhotoSectionPage.cshtml", assetsModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssetDeleteImage(int assetPkId)
        {
            int rowsAffected = 0;
            rowsAffected = await _formaImgService.DectivateAssetImage(assetPkId);
            return Json(rowsAffected);
        }

        #endregion

        //Rendering the popup of details page create new
        [HttpPost]
        public async Task<IActionResult> GetAssetDataByRoadCode(string roadCode)
        {
            RoadMasterRequestDTO _rmRoad = new RoadMasterRequestDTO();
            _rmRoad.RoadCode = roadCode;
            var _RMAllData = await _roadMasterService.GetAllRoadCodeData(_rmRoad);
            return Json(_RMAllData);
        }

        public async Task<IActionResult> AddNOD(string assetName, int hdrId, string hdrrefno, int detId)
        {
            try
            {
                DDLookUpDTO ddLookup = new DDLookUpDTO();
                _formAModel.SaveFormADetails = new FormADetailsRequestDTO();
                _formAModel.SaveFormADetails.HeaderNo = hdrId;

                var defectCode = _formAService.GetAssetCodeByName(assetName);
                ViewData["DistressCode"] = await _formAService.GetDefectCodeService(defectCode);

                ddLookup.Type = "Priority";
                ViewData["lookupPriority"] = await _ddLookupService.GetDdLookupValue(ddLookup);

                ddLookup.Type = "ACT-" + defectCode;
                ViewData["lookupActivityCode"] = await _ddLookupService.GetLookUpCodeTextConcat(ddLookup);

                ddLookup.Type = "Unit";
                ViewData["lookupUnit"] = await _ddLookupService.GetDdLookup(ddLookup);

                ddLookup.Type = "Site Ref";
                ViewData["lookupSiteReg"] = await _ddLookupService.GetLookUpValueDesc(ddLookup);

                // this will work only for add functionality
                if (detId == 0)
                {
                    var result = await _formAService.LastInsertedFormDetailSRNO(hdrId);

                    _formAModel.SaveFormADetails.Srno = (result + 1);
                    _formAModel.SaveFormADetails.FadRefNO = hdrrefno + "/" + (result + 1);
                }
                else
                {
                    List<FormADetailsRequestDTO> saveFormADetList = new List<FormADetailsRequestDTO>();
                    var Result = await _formAService.GetDetailById(detId);
                    _formAModel.SaveFormADetails = Result;
                }

                var _header = await _formAService.GetHeaderById(hdrId);
                _formAModel.SaveFormAModel = new FormAHeaderRequestDTO
                {
                    AssetGroupCode = _header.AssetGroupCode
                };
                if (_header != null)
                {
                    if (_header.Month.GetValueOrDefault() > 0 && _header.Year.GetValueOrDefault() > 1900)
                    {

                        string minstring = $"{(_header.Month.GetValueOrDefault() < 10 ? "0" : "")}{_header.Month.GetValueOrDefault()}/01/{_header.Year}";
                        DateTime mindate;
                        if (DateTime.TryParseExact(minstring, "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.CurrentInfo, System.Globalization.DateTimeStyles.None, out mindate))
                        {
                            _formAModel.SaveFormADetails.MinDate = mindate;
                        }
                        else
                        {
                            _formAModel.SaveFormADetails.MinDate = DateTime.Now;
                        }

                        int maxDays = DateTime.DaysInMonth(_header.Year.Value, _header.Month.Value);
                        string maxString = $"{(_header.Month.GetValueOrDefault() < 10 ? "0" : "")}{_header.Month.GetValueOrDefault()}/{maxDays}/{_header.Year.Value}";
                        DateTime maxDate;
                        if (DateTime.TryParseExact(maxString, "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.CurrentInfo, System.Globalization.DateTimeStyles.None, out maxDate))
                        {
                            _formAModel.SaveFormADetails.MaxDate = maxDate;
                        }
                        else
                        {
                            _formAModel.SaveFormADetails.MaxDate = DateTime.Now;
                        }
                    }
                }
                ViewBag.WI = _dDLookupBO.GetWeekNo();
                ViewBag.WS = _dDLookupBO.GetWeekNo();
                ViewBag.WTC = _dDLookupBO.GetWeekNo();
                ViewBag.WC = _dDLookupBO.GetWeekNo();
                ViewBag.ShiftPS = _dDLookupBO.GetWeekNo();
                ViewBag.ShiftWIS = _dDLookupBO.GetWeekNo();
                ViewBag.RT = _dDLookupBO.GetWeekNo();

                return PartialView("~/Views/NOD/FormA/Add.cshtml", _formAModel);

            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
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
                DDLookUpDTO ddLookup = new DDLookUpDTO();
                RoadMasterRequestDTO roadMasterReq = new RoadMasterRequestDTO();
            }
            assetDDLResponseDTO = await _roadMasterService.GetAssetDDL(request);
            return Json(assetDDLResponseDTO);

        }

        [HttpPost]
        public async Task<IActionResult> GetReferenceNOData(string roadCode, string month, string year, string assetGroup)
        {
            if (roadCode != null && assetGroup != null)
            {
                try
                {
                    string isexist = _formAService.CheckAlreadyExists(roadCode, int.Parse(month), int.Parse(year), assetGroup);
                    assetGroup = _formAService.GetAssetCodeByName(assetGroup);
                    if (string.IsNullOrEmpty(isexist))
                    {
                        isexist = (await _formAService.GetLastInsertedHeader()).ToString();
                    }
                    if (!string.IsNullOrEmpty(assetGroup))
                    {
                        var AutoRefNo = $"NOD/Form A/{roadCode}/{month}/{assetGroup}/{String.Format("{0:0000}", isexist)}-" + year;
                        return Json(AutoRefNo);
                    }
                    else
                    {
                        return Json("");
                    }
                }
                catch
                {
                    return Json("");
                }
            }
            else
            {
                return Json("");
            }


        }

        public IActionResult HGetReferenceNOData(FormType formType, string roadCode, DateTime inspectiondate, string assetGroup, int locationfrom, int locationto, int sourcerefno, string sourcerefnoText)
        {

            if (roadCode != null && assetGroup != null)
            {
                try
                {
                    (int id, bool alreadyexists) isExist = _formHService.CheckAlreadyExists(formType, roadCode, inspectiondate, assetGroup, locationfrom, locationto, sourcerefno);
                    var _asset = assetGroup;
                    assetGroup = _formJService.GetAssetCodeByName(assetGroup);
                    if (string.IsNullOrEmpty(assetGroup))
                    {
                        assetGroup = _formAService.GetAssetCodeByName(_asset);
                    }
                    var f = formType == FormType.FormA ? "A" : formType == FormType.FormJ ? "J" : "N";
                    if (!string.IsNullOrEmpty(assetGroup) && sourcerefno == 0 && sourcerefnoText == "Select Reference")
                    {
                        var AutoRefNo = $"NOD/Form H/{roadCode}/{assetGroup}/{f}/{inspectiondate.ToString("yyyyMMdd")}/{locationfrom}/{locationto}/{isExist.id}";
                        return Json(new
                        {
                            isAlreadyexists = isExist.alreadyexists,
                            id = isExist.id,
                            RefNo = AutoRefNo
                        });
                    }
                    else if (sourcerefno != 0 && sourcerefnoText != "Select Reference")
                    {
                        if (f != "N")
                        {
                            var refno = sourcerefnoText.Split('/');
                            var AutoRefNo = $"NOD/Form H/{roadCode}/{assetGroup}/{f}/{inspectiondate.ToString("yyyyMMdd")}/{locationfrom}/{locationto}/{isExist.id}/{refno[5]}/{refno[6]}";
                            return Json(new
                            {
                                isAlreadyexists = isExist.alreadyexists,
                                id = isExist.id,
                                RefNo = AutoRefNo
                            });
                        }
                        else
                        {
                            var autoRefNo = $"NOD/Form H/{roadCode}/{assetGroup}/{f}/{inspectiondate.ToString("yyyyMMdd")}/{locationfrom}/{locationto}/{isExist.id}";
                            return Json(new
                            {
                                isAlreadyexists = isExist.alreadyexists,
                                id = isExist.id,
                                RefNo = autoRefNo
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json("");
                }
            }
            else
            {
                return Json("");
            }
            return Json("");

        }

        [HttpPost]
        public ActionResult GridView(RmFormAHdr rmFormAHdr)
        {

            var sData = Newtonsoft.Json.JsonConvert.SerializeObject(rmFormAHdr);

            TempData["TDrmFormAHdr"] = sData;
            return RedirectToAction("Add");
        }

        [HttpPost]
        public async Task<IActionResult> DetailEditOrUpdate(int headerId)
        {

            if (headerId > 0)
            {
                List<FormADetailsRequestDTO> saveFormADetList = new List<FormADetailsRequestDTO>();
                var Result = await _formAService.GetDetailById(headerId);
                _formAModel.SaveFormADetails = Result;
            }
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            string[] assetName = _formAModel.SaveFormADetails.AssetId.Split("/");

            var defectCode = assetName[0];

            ViewData["DistressCode"] = await _formAService.GetDefectCodeService(defectCode);

            ddLookup.Type = "Priority";
            ViewData["lookupPriority"] = await _ddLookupService.GetDdLookup(ddLookup);

            ddLookup.Type = "ACT-" + defectCode;
            ViewData["lookupActivityCode"] = await _ddLookupService.GetLookUpCodeDesc(ddLookup);

            ddLookup.Type = "Unit";
            ViewData["lookupUnit"] = await _ddLookupService.GetDdLookup(ddLookup);

            ddLookup.Type = "Site Ref";
            ViewData["lookupSiteReg"] = await _ddLookupService.GetDdLookup(ddLookup);

            ViewBag.WI = _dDLookupBO.GetWeekNo();
            ViewBag.WS = _dDLookupBO.GetWeekNo();// FetchWeeks(DateTime.Now.Year);
            ViewBag.WTC = _dDLookupBO.GetWeekNo();
            ViewBag.WC = _dDLookupBO.GetWeekNo();
            ViewBag.ShiftPS = _dDLookupBO.GetWeekNo();
            ViewBag.ShiftWIS = _dDLookupBO.GetWeekNo();
            ViewBag.RT = _dDLookupBO.GetWeekNo();

            return PartialView("~/Views/NOD/FormA/Add.cshtml", _formAModel);

        }

        #region FORM J
        
        FormJModel _formJModel = new FormJModel();

        [HttpPost]
        public async Task<IActionResult> JGetImageList(int assetPk, string assetGroup)
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            FormJModel assetsModel = new FormJModel();
            assetsModel.AssetimageList = new List<FormJImageListRequestDTO>();
            assetsModel.ImageTypeList = new List<string>();
            assetGroup = _formJService.GetAssetCodeByName(assetGroup);
            ddLookup.TypeCode = assetGroup;
            ddLookup.Type = "Photo Type";
            assetsModel.PhotoType = await _ddLookupService.GetDdLookup(ddLookup);
            if (assetsModel.PhotoType.Count() == 0)
            {
                assetsModel.PhotoType = new[]{ new SelectListItem
                {
                    Text = "Others",
                    Value = "Others"
                }};
            }
            assetsModel.AssetimageList = await _formjImgService.GetAllImageByAssetPK(assetPk);
            assetsModel.ImageTypeList = assetsModel.AssetimageList.Select(c => c.ImageTypeCode).Distinct().ToList();
            return PartialView("~/Views/NOD/FormJ/_PhotoSectionPage.cshtml", assetsModel);
        }

        /// <summary>
        /// to get latest serial number.
        /// </summary>
        /// <param name="headerno"></param>
        /// <returns></returns>
        public async Task<IActionResult> JGetLatestSRNO(int headerNo) => Json(await _formJService.LastInsertedFormDetailSRNO(headerNo) + 1);

        [HttpPost]
        public async Task<IActionResult> JDetailsSaveV1(FormJDetailsRequestDTO saveFormADetList)
        {
            // List<FormADetailsRequestDTO> detList = new List<FormADetailsRequestDTO>();
            if (saveFormADetList.No == 0)
            {
                saveFormADetList.CreatedBy = _security.UserID.ToString();
            }
            saveFormADetList.ModBy = _security.UserID.ToString();
            int? detailSave = await _formJService.SaveDetailforHeaderV1(saveFormADetList);
            return Json(detailSave);
        }

        [HttpPost]
        public async Task<IActionResult> JLoadGridList(int detailSave)
        {

            var Result = await _formJService.GetFormAWithDetailsByNoAsync(detailSave);
            _formJModel.SaveFormAModel = Result;

            return PartialView("~/Views/NOD/FormJ/_DetailGridList.cshtml", _formJModel);
        }

        [HttpPost]
        public async Task<IActionResult> JImageUploaded(IList<IFormFile> formFile, int assetId, string photoTypes)
        {
            try
            {
                int i = await _formjImgService.LastInsertedSRNO(assetId, photoTypes);
                i++;
                string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;
                string photoType = Regex.Replace(photoTypes, @"[^a-zA-Z]", "");
                string path = Path.Combine(wwwPath, Path.Combine("Uploads", "FormJDetail", assetId.ToString(), photoType));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                List<FormJImageListRequestDTO> uploadedFiles = new List<FormJImageListRequestDTO>();
                if (formFile.Count > 0)
                {
                    foreach (IFormFile postedFile in formFile)
                    {
                        FormJImageListRequestDTO _rmAssetImageDtl = new FormJImageListRequestDTO();
                        string fileName = Path.GetFileName(postedFile.FileName);
                        string filerename = i + "_" + photoTypes + "_" + fileName;
                        using (FileStream stream = new FileStream(Path.Combine(path, filerename), FileMode.Create))
                        {
                            _rmAssetImageDtl.AssetId = assetId;
                            _rmAssetImageDtl.ImageTypeCode = photoTypes;
                            _rmAssetImageDtl.SNO = i;
                            _rmAssetImageDtl.FileName = postedFile.FileName;
                            _rmAssetImageDtl.ActiveYn = true;
                            if (i < 10)
                            {
                                _rmAssetImageDtl.ImageFilenameSys = assetId.ToString() + "_" + photoTypes + "_" + "00" + i;
                            }
                            else if (i >= 10 && i < 100)
                            {
                                _rmAssetImageDtl.ImageFilenameSys = assetId.ToString() + "_" + photoTypes + "_" + "0" + i;
                            }
                            else
                            {
                                _rmAssetImageDtl.ImageFilenameSys = assetId.ToString() + "_" + photoTypes + "_" + i;
                            }
                            _rmAssetImageDtl.ImageFilename = $"/Uploads/FormJDetail/{assetId}/{photoType}/{filerename}";

                            postedFile.CopyTo(stream);

                        }
                        _rmAssetImageDtl.CreatedDate = DateTime.UtcNow;
                        _rmAssetImageDtl.ModifyDate = DateTime.UtcNow;
                        _rmAssetImageDtl.CreatedBy = _security.UserID.ToString();
                        _rmAssetImageDtl.ModifyBy = _security.UserID.ToString();
                        uploadedFiles.Add(_rmAssetImageDtl);
                        i = i + 1;
                    }

                    await _formjImgService.SaveImageDtl(uploadedFiles);
                }
                else
                {
                    return BadRequest("No file found to upload");
                }
                return Json($"Successfully Uploaded {formFile.Count} file(s");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> JAddNOD(string assetName, int hdrId, string hdrrefno, int detId)
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            _formJModel.SaveFormADetails = new FormJDetailsRequestDTO();
            _formJModel.SaveFormADetails.HeaderNo = hdrId;


            var defectCode = _formJService.GetAssetCodeByName(assetName);

            //Dropdown values is fetched with asset group null
            if (defectCode == "CWR" || defectCode == "CWU" || defectCode == "CWF")
            {
                defectCode = null;
            }

            ViewData["DistressCode"] = await _formJService.GetDefectCodeServiceConCat(defectCode);

            ddLookup.Type = "Priority";
            ViewData["lookupPriority"] = await _ddLookupService.GetDdLookupValue(ddLookup);

            ddLookup.Type = "ACT-" + defectCode;
            ViewData["lookupActivityCode"] = await _ddLookupService.GetLookUpCodeDesc(ddLookup);

            ddLookup.Type = "Unit";
            ViewData["lookupUnit"] = await _ddLookupService.GetDdLookup(ddLookup);

            ddLookup.Type = "Site Ref";

            ViewData["lookupSiteReg"] = await _ddLookupService.GetDdLookup(ddLookup);
            // this will work only for add functionality
            if (detId == 0)
            {
                var result = await _formJService.LastInsertedFormDetailSRNO(hdrId);

                _formJModel.SaveFormADetails.Srno = (result + 1);
                _formJModel.SaveFormADetails.FadRefNO = hdrrefno + "/" + (result + 1);
            }
            else
            {
                List<FormADetailsRequestDTO> saveFormADetList = new List<FormADetailsRequestDTO>();
                var Result = await _formJService.GetDetailById(detId);
                _formJModel.SaveFormADetails = Result;
            }

            var _header = await _formJService.GetHeaderById(hdrId);
            _formJModel.SaveFormAModel = new FormJHeaderRequestDTO
            {
                AssetGroupCode = _header.AssetGroupCode
            };
            if (_header != null)
            {
                if (_header.Month.GetValueOrDefault() > 0 && _header.Year.GetValueOrDefault() > 1900)
                {

                    string minstring = $"{(_header.Month.GetValueOrDefault() < 10 ? "0" : "")}{_header.Month.GetValueOrDefault()}/01/{_header.Year}";
                    DateTime mindate;
                    if (DateTime.TryParseExact(minstring, "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.CurrentInfo, System.Globalization.DateTimeStyles.None, out mindate))
                    {
                        _formJModel.SaveFormADetails.MinDate = mindate;
                    }
                    else
                    {
                        _formJModel.SaveFormADetails.MinDate = DateTime.Now;
                    }


                    int maxDays = DateTime.DaysInMonth(_header.Year.Value, _header.Month.Value);
                    string maxString = $"{(_header.Month.GetValueOrDefault() < 10 ? "0" : "")}{_header.Month.GetValueOrDefault()}/{maxDays}/{_header.Year.Value}";
                    DateTime maxDate;
                    if (DateTime.TryParseExact(maxString, "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.CurrentInfo, System.Globalization.DateTimeStyles.None, out maxDate))
                    {
                        _formJModel.SaveFormADetails.MaxDate = maxDate;
                    }
                    else
                    {
                        _formJModel.SaveFormADetails.MaxDate = DateTime.Now;
                    }
                }
            }
            ViewBag.WI = _dDLookupBO.GetWeekNo();
            ViewBag.WS = _dDLookupBO.GetWeekNo();
            ViewBag.WTC = _dDLookupBO.GetWeekNo();
            ViewBag.WC = _dDLookupBO.GetWeekNo();
            ViewBag.ShiftPS = _dDLookupBO.GetWeekNo();
            ViewBag.ShiftWIS = _dDLookupBO.GetWeekNo();
            ViewBag.RT = _dDLookupBO.GetWeekNo();
            return PartialView("~/Views/NOD/FormJ/_FormJDetailView.cshtml", _formJModel);
        }

        [HttpPost]
        public async Task<IActionResult> JSave(FormJHeaderRequestDTO saveObj)
        {
            int rowsAffected = 0;
            FormJHeaderRequestDTO saveRequestObj = new FormJHeaderRequestDTO();

            //  saveObj.SaveFormADetails.SiteRef = (saveObj.FormADetails.SiteRef_multiSelect != null) ? String.Join(",", saveObj.SaveFormADetails.SiteRef_multiSelect) : null;
            saveRequestObj = saveObj;
            if (saveRequestObj.No == 0)
            {
                saveRequestObj.CreatedBy = _security.UserID.ToString();
            }
            saveRequestObj.ModBy = _security.UserID.ToString();
            saveRequestObj.ModDt = DateTime.UtcNow;
            rowsAffected = await _formJService.SaveFormJAsync(saveRequestObj);
            //await Index();
            return Json(rowsAffected);
        }

        [HttpPost]
        public async Task<IActionResult> JGetReferenceNOData(string roadCode, string month, string year, string assetGroup)
        {

            if (roadCode != null && assetGroup != null)
            {
                try
                {
                    string isExist = _formJService.CheckAlreadyExists(roadCode, int.Parse(month), int.Parse(year), assetGroup);
                    assetGroup = _formJService.GetAssetCodeByName(assetGroup);
                    if (string.IsNullOrEmpty(isExist))
                    {
                        isExist = (await _formJService.GetLastInsertedHeader()).ToString();
                    }
                    if (!string.IsNullOrEmpty(assetGroup))
                    {
                        var AutoRefNo = $"NOD/Form J/{roadCode}/{month}/{assetGroup}/{String.Format("{0:0000}", isExist)}-" + year;
                        return Json(AutoRefNo);
                    }
                    else
                    {
                        return Json("");
                    }
                }
                catch
                {
                    return Json("");
                }
            }
            else
            {
                return Json("");
            }


        }

        [CAuthorize(ModuleName = ModuleNameList.NOD)]
        public async Task<IActionResult> FormJ([FromQuery(Name = "vid")] string viewId)
        {
            ViewBag.ViewId = viewId;
            await JLoadDropDowns();

            FormJModel formJModel = new FormJModel();
            FormJSearchGridDTO filterData = new FormJSearchGridDTO();
            formJModel.SearchObj = filterData;
            return View("~/Views/NOD/FormJ/FormJ.cshtml", formJModel);
        }

        [HttpPost]
        public async Task<IActionResult> JHeaderSave(FormJHeaderRequestDTO saveObj)
        {
            DataTableAjaxPostModel<FormJSearchGridDTO> searchData = new DataTableAjaxPostModel<FormJSearchGridDTO>();
            FormJHeaderResponseDTO formAHeaderResponseDTO = new FormJHeaderResponseDTO();

            if (saveObj.No == 0)
            {
                saveObj.CreatedBy = _security.UserID.ToString();
                saveObj.ModBy = _security.UserID.ToString();
                formAHeaderResponseDTO = await _formJService.SaveHeaderWithResponse(saveObj);
            }
            else
            {
                // RowsAffected = await _formAService.UpdateFormAAsync(saveObj);
            }

            return Json(formAHeaderResponseDTO);
        }

        [HttpPost]
        public async Task<IActionResult> JHeaderListEdit(DataTableAjaxPostModel<FormJDetailsRequestDTO> searchData)
        {
            await JLoadDropDowns();
            int headerId = searchData.filterData.HeaderNo;
            _formJModel.SaveFormAModel = new FormJHeaderRequestDTO();
            if (headerId > 0)
            {
                FormJHeaderRequestDTO Result = await _formJService.GetFormAWithDetailsByNoAsync(headerId);
                _formJModel.SaveFormAModel = Result;
                _formJModel.SaveFormAModel.Id = headerId.ToString();
            }

            searchData.length = 10;
            searchData.start = 0;

            return PartialView("~/Views/NOD/FormJ/_AddFormJView.cshtml", _formJModel);
        }

        [HttpPost]
        public async Task<IActionResult> JDetailedListEdit(DataTableAjaxPostModel<FormJDetailsRequestDTO> searchData)
        {

            FilteredPagingDefinition<FormJDetailsRequestDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormJDetailsRequestDTO>();


            filteredPagingDefinition.Filters = searchData.filterData;

            filteredPagingDefinition.RecordsPerPage = searchData.length;
            filteredPagingDefinition.StartPageNo = searchData.start;

            var result = await _formJService.GetFormADetailGrid(filteredPagingDefinition).ConfigureAwait(false);

            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }

        [HttpPost]
        public async Task<IActionResult> JLoadHeaderList(DataTableAjaxPostModel<FormJSearchGridDTO> searchData) //jQueryDataTableParamModel param
        {
            if (Request.Form.ContainsKey("columns[0][search][value]"))
            {
                searchData.filterData.SmartInputValue = Request.Form["columns[0][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[1][search][value]"))
            {
                searchData.filterData.RMU = Request.Form["columns[1][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[2][search][value]"))
            {
                searchData.filterData.Section = Request.Form["columns[2][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[3][search][value]"))
            {
                searchData.filterData.Road_Code = Request.Form["columns[3][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[4][search][value]"))
            {
                searchData.filterData.Asset_GroupCode = Request.Form["columns[4][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[5][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[5][search][value]"].ToString()))
                {
                    searchData.filterData.Month = Convert.ToInt32(Request.Form["columns[5][search][value]"].ToString());
                }
            }
            if (Request.Form.ContainsKey("columns[6][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[6][search][value]"].ToString()))
                {
                    searchData.filterData.Year = Convert.ToInt32(Request.Form["columns[6][search][value]"].ToString());
                }
            }
            if (Request.Form.ContainsKey("columns[7][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[7][search][value]"].ToString()))
                {
                    searchData.filterData.ChinageFromKm = Convert.ToInt32(Request.Form["columns[7][search][value]"].ToString());
                }
            }
            if (Request.Form.ContainsKey("columns[8][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[8][search][value]"].ToString()))
                {
                    searchData.filterData.ChinageFromM = Request.Form["columns[8][search][value]"].ToString();
                }
            }
            if (Request.Form.ContainsKey("columns[9][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[9][search][value]"].ToString()))
                {
                    searchData.filterData.ChinageToKm = Convert.ToInt32(Request.Form["columns[9][search][value]"].ToString());
                }
            }
            if (Request.Form.ContainsKey("columns[10][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[10][search][value]"].ToString()))
                {
                    searchData.filterData.ChinageToM = Request.Form["columns[10][search][value]"].ToString();
                }
            }

            FilteredPagingDefinition<FormJSearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormJSearchGridDTO>();

            filteredPagingDefinition.Filters = searchData.filterData;
            filteredPagingDefinition.RecordsPerPage = searchData.length;
            filteredPagingDefinition.StartPageNo = searchData.start;
            if (searchData.order != null)
            {
                filteredPagingDefinition.ColumnIndex = searchData.order[0].column;
                filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }
            var result = await _formJService.GetFilteredFormJGrid(filteredPagingDefinition).ConfigureAwait(false);

            if (result.PageResult.Count > 0)
            {
                for (int i = 0; i < result.PageResult.Count; i++)
                {
                    result.PageResult[i].MonthYear = ((result.PageResult[i].Month ?? 0) < 10 ? "0" : "") + (result.PageResult[i].Month ?? 0) + "/" + (result.PageResult[i].Year.HasValue ? result.PageResult[i].Year.Value.ToString() : "2020");//TODO - hardcoded for demo - by John -  To be reworked
                    result.PageResult[i].Status = result.PageResult[i].SubmitSts ? "Submitted" : "Saved";
                }
            }

            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }

        [HttpPost]
        public async Task<IActionResult> JLoadHeaderGridList(FormJModel formAModel)
        {
            FormJSearchGridDTO searchObj = new FormJSearchGridDTO();
            searchObj = formAModel.SearchObj;

            ViewBag.CurrentSort = formAModel.SearchObj.sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(formAModel.SearchObj.sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = formAModel.SearchObj.sortOrder == "Date" ? "date_desc" : "Date";
            if (formAModel.SearchObj.searchString != null)
            {
                formAModel.SearchObj.Page_No = 1;
            }
            else
            {
                formAModel.SearchObj.searchString = formAModel.SearchObj.currentFilter;
            }

            ViewBag.CurrentFilter = formAModel.SearchObj.searchString;
            int Size_Of_Page = (formAModel.SearchObj.pageSize ?? 1000);
            int No_Of_Page = (formAModel.SearchObj.Page_No ?? 1);
            ViewBag.psize = Size_Of_Page;
            ViewBag.PageSize = new List<SelectListItem>()
            {
             new SelectListItem() { Value="5", Text= "5" },
             new SelectListItem() { Value="10", Text= "10" },
             new SelectListItem() { Value="15", Text= "15" },
             new SelectListItem() { Value="25", Text= "25" },
             new SelectListItem() { Value="50", Text= "50" }
            };


            FilteredPagingDefinition<FormJSearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormJSearchGridDTO>();
            filteredPagingDefinition.Filters = searchObj;
            filteredPagingDefinition.RecordsPerPage = 5;
            filteredPagingDefinition.StartPageNo = 1; //TODO

            var result = await _formJService.GetFilteredFormJGrid(filteredPagingDefinition);
            var obj = result.PageResult;
            IPagedList<FormJHeaderResponseDTO> headerList = obj.ToPagedList(No_Of_Page, Size_Of_Page);

            _formJModel.FormAHeaderList = headerList;
            ViewBag.TotalNoRecords = headerList.TotalItemCount.ToString();
            int iPreDisplay = ((No_Of_Page) * Size_Of_Page);
            ViewBag.DisplayRecords = iPreDisplay;

            ViewBag.TotalPage = headerList.PageCount;
            var CurrentPage = (headerList.PageCount < headerList.PageNumber ? 0 : headerList.PageNumber);
            ViewBag.CurrentPage = CurrentPage;

            //Added for Temporary Count showing
            ViewBag.DisplayRecords = result.FilteredRecords;
            ViewBag.TotalNoRecords = result.TotalRecords;

            return View(_formAModel);
        }

        [HttpPost]
        public async Task<IActionResult> JSearchHeaderList(FormJSearchGridDTO filterData)
        {
            FormJModel formAObj = new FormJModel();
            formAObj.SearchObj = filterData;
            await JLoadHeaderGridList(formAObj);
            return PartialView("~/Views/NOD/FormJ/_HeaderListGrid.cshtml", _formJModel);
        }

        [HttpPost]
        public async Task<IActionResult> JHeaderListDelete(int headerId)
        {
            int rowsAffected = 0;
            rowsAffected = await _formJService.DeActivateFormAAsync(headerId);
            return Json(rowsAffected);
        }

        [HttpPost]
        public async Task<IActionResult> JDetailListDelete(int detailId)
        {
            int rowsAffected = 0;
            rowsAffected = await _formJService.DeActivateDetail(detailId);
            return Json(rowsAffected);
        }

        [HttpPost]
        public async Task<IActionResult> JDeleteImage(int assetPkId)
        {
            int rowsAffected = 0;
            rowsAffected = await _formjImgService.DectivateAssetImage(assetPkId);
            return Json(rowsAffected);
        }

        #endregion

        #region FORM H
       
        FormHModel _formHmodel = new FormHModel();

        [HttpPost]
        public async Task<IActionResult> HImageUploaded(IList<IFormFile> FormFile, int AssetId, string PhotoType)
        {
            try
            {
                int i = await _formhImgService.LastInsertedSRNO(AssetId, PhotoType);
                i++;
                string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;
                string photoType = Regex.Replace(PhotoType, @"[^a-zA-Z]", "");
                string path = Path.Combine(wwwPath, Path.Combine("Uploads", "FormHDetail", AssetId.ToString(), photoType));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                List<FormHImageListRequestDTO> uploadedFiles = new List<FormHImageListRequestDTO>();
                foreach (IFormFile postedFile in FormFile)
                {
                    FormHImageListRequestDTO _rmAssetImageDtl = new FormHImageListRequestDTO();
                    string fileName = Path.GetFileName(postedFile.FileName);
                    string filerename = i + "_" + PhotoType + "_" + fileName;
                    using (FileStream stream = new FileStream(Path.Combine(path, filerename), FileMode.Create))
                    {
                        _rmAssetImageDtl.AssetId = AssetId;
                        _rmAssetImageDtl.ImageTypeCode = PhotoType;
                        _rmAssetImageDtl.SNO = i;
                        _rmAssetImageDtl.FileName = postedFile.FileName;
                        _rmAssetImageDtl.ActiveYn = true;
                        if (i < 10)
                        {
                            _rmAssetImageDtl.ImageFilenameSys = AssetId.ToString() + "_" + PhotoType + "_" + "00" + i;
                        }
                        else if (i >= 10 && i < 100)
                        {
                            _rmAssetImageDtl.ImageFilenameSys = AssetId.ToString() + "_" + PhotoType + "_" + "0" + i;
                        }
                        else
                        {
                            _rmAssetImageDtl.ImageFilenameSys = AssetId.ToString() + "_" + PhotoType + "_" + i;
                        }
                        _rmAssetImageDtl.ImageFilename = $"/Uploads/FormHDetail/{AssetId}/{photoType}/{filerename}";

                        //AssetID_Section(Water)_incrementalNumber -- i + "_" + _rmAssetImageDtl.AssetId +_rmAssetImageDtl.ImageTypeCode + "_" + fileName;
                        //_rmAssetImageDtl.ImageFilename = _httpContext.HttpContext.Request.Host.Value + "\\" + "wwwroot" + "\\" + "Uploads" + "\\" + assetGrpCode[0] + "\\" + id + "\\" + photoType +"\\" + filerename;
                        postedFile.CopyTo(stream);

                        //ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                    }
                    _rmAssetImageDtl.CreatedBy = _security.UserID.ToString();
                    _rmAssetImageDtl.ModifyDate = DateTime.UtcNow;
                    _rmAssetImageDtl.ModifyBy = _security.UserID.ToString();
                    _rmAssetImageDtl.CreatedDate = DateTime.UtcNow;
                    uploadedFiles.Add(_rmAssetImageDtl);
                    i = i + 1;
                }
                //_bridgeBO.SaveAssetImageDtlBO(uploadedFiles);
                await _formhImgService.SaveImageDtl(uploadedFiles);
                return Json("");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> HDeleteImage(int assetPkId)
        {
            int rowsAffected = 0;
            rowsAffected = await _formhImgService.DectivateAssetImage(assetPkId);
            return Json(rowsAffected);
        }

        [HttpPost]
        public async Task<IActionResult> HGetImageList(int assetPk, string assetGroup)
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            FormHModel assetsModel = new FormHModel();
            assetsModel.AssetimageList = new List<FormHImageListRequestDTO>();
            assetsModel.ImageTypeList = new List<string>();
            var _asset = assetGroup;
            assetGroup = _formJService.GetAssetCodeByName(assetGroup);
            if (string.IsNullOrEmpty(assetGroup))
            {
                assetGroup = _formAService.GetAssetCodeByName(_asset);
            }
            ddLookup.TypeCode = assetGroup;
            ddLookup.Type = "Photo Type";
            assetsModel.PhotoType = await _ddLookupService.GetDdLookup(ddLookup);
            if (assetsModel.PhotoType.Count() == 0)
            {
                assetsModel.PhotoType = new[]{ new SelectListItem
                {
                    Text = "Others",
                    Value = "Others"
                }};
            }
            assetsModel.AssetimageList = await _formhImgService.GetAllImageByAssetPK(assetPk);
            assetsModel.ImageTypeList = assetsModel.AssetimageList.Select(c => c.ImageTypeCode).Distinct().ToList();
            return PartialView("~/Views/NOD/FormH/_PhotoSectionPage.cshtml", assetsModel);
        }

        [HttpPost]
        public async Task<IActionResult> HLoadHeaderList(DataTableAjaxPostModel<FormHSearchDTO> searchData) //jQueryDataTableParamModel param
        {
            if (Request.Form.ContainsKey("columns[0][search][value]"))
            {
                searchData.filterData.SmartInputValue = Request.Form["columns[0][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[1][search][value]"))
            {
                searchData.filterData.RMU = Request.Form["columns[1][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[2][search][value]"))
            {
                searchData.filterData.Section = Request.Form["columns[2][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[3][search][value]"))
            {
                searchData.filterData.RoadCode = Request.Form["columns[3][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[4][search][value]"))
            {
                searchData.filterData.AssetGroupCode = Request.Form["columns[4][search][value]"].ToString();
            }

            if (Request.Form.ContainsKey("columns[7][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[7][search][value]"].ToString()))
                {
                    searchData.filterData.FromChKM = Convert.ToInt32(Request.Form["columns[7][search][value]"].ToString());
                }
            }
            if (Request.Form.ContainsKey("columns[8][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[8][search][value]"].ToString()))
                {
                    searchData.filterData.FromChM = Convert.ToInt32(Request.Form["columns[8][search][value]"].ToString());
                }
            }
            if (Request.Form.ContainsKey("columns[9][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[9][search][value]"].ToString()))
                {
                    searchData.filterData.ToChKM = Convert.ToInt32(Request.Form["columns[9][search][value]"].ToString());
                }
            }
            if (Request.Form.ContainsKey("columns[8][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[10][search][value]"].ToString()))
                {
                    searchData.filterData.ToChM = Convert.ToInt32(Request.Form["columns[10][search][value]"].ToString());
                }
            }

            if (Request.Form.ContainsKey("columns[10][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[10][search][value]"].ToString()))
                {
                    searchData.filterData.ToChM = Convert.ToInt32(Request.Form["columns[10][search][value]"].ToString());
                }
            }

            if (Request.Form.ContainsKey("columns[5][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[5][search][value]"].ToString()))
                {
                    searchData.filterData.InspectionDate = Request.Form["columns[5][search][value]"].ToString();
                }
            }


            FilteredPagingDefinition<FormHSearchDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormHSearchDTO>();

            filteredPagingDefinition.Filters = searchData.filterData;
            filteredPagingDefinition.RecordsPerPage = searchData.length;
            filteredPagingDefinition.StartPageNo = searchData.start;
            if (searchData.order != null)
            {
                filteredPagingDefinition.ColumnIndex = searchData.order[0].column;
                filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }
            var result = await _formHService.GetFilteredFormHGrid(filteredPagingDefinition).ConfigureAwait(false);

            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }
        public async Task<IActionResult> FormH([FromQuery(Name = "vid")] string viewId)
        {
            ViewBag.ViewId = viewId;
            await HLoadDropDowns();
            FormHModel formHmodel = new FormHModel();
            formHmodel.SearchFormH = new FormHSearchDTO();
            formHmodel.FormHDetail = new FormHRequestDTO();
            return View("~/Views/NOD/FormH/FormH.cshtml", formHmodel);
        }

        [HttpPost]
        public async Task<IActionResult> HEdit(int id)
        {
            await HLoadDropDowns();
            if (id > 0)
            {
                var Result = await _formHService.GetByID(id);
                _formHmodel.FormHDetail = Result;
                _formHmodel.FormHDetail.No = id;
            }
            else
            {
                _formHmodel.FormHDetail = new FormHRequestDTO();
            }
            return PartialView("~/Views/NOD/FormH/_AddFormHView.cshtml", _formHmodel);
        }

        [HttpPost]
        public IActionResult HGetReferenceList(RequestFormReference type) => Json(_formHService.GetReferenceNoByFormType(type));

        [HttpPost]
        public async Task<IActionResult> HSave(FormHRequestDTO formHRequest)
        {
            FormHRequestDTO RequestDto = new FormHRequestDTO();
            if (formHRequest.No == 0)
            {
                formHRequest.CreatedBy = _security.UserID.ToString();

            }
            formHRequest.ModBy = _security.UserID.ToString();
            RequestDto = await _formHService.SaveFormH(formHRequest);

            return Ok(RequestDto);
        }
       
        #endregion

        // FormA Image Upload from TAB
        [HttpPost] 
        public async Task<int> UploadImage(IFormCollection files1, string AssetId, string photoType)
        {
            try
            {
                int assetId = Convert.ToInt32(AssetId);

                int i = await _formaImgService.LastInsertedSRNO(assetId, photoType);
                i += 1;
                IFormCollection files = Request.ReadFormAsync().Result;
                string photoTypeforUrl = Regex.Replace(photoType, @"[^a-zA-Z]", "");
                var uploads = Path.Combine(this.Environment.WebRootPath, "Uploads", "FormADetail", AssetId.ToString(), photoTypeforUrl);
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }
                List<FormAImageListRequestDTO> uploadedFiles = new List<FormAImageListRequestDTO>();

                foreach (var file in files.Files)
                {
                    if (file != null)
                    {
                        FormAImageListRequestDTO _rmAssetImageDtl = new FormAImageListRequestDTO();
                        string fileName = Path.GetFileName(file.FileName);
                        string filerename = i + "_" + photoType + "_" + fileName;
                        using (var fileStream = new FileStream(Path.Combine(uploads, filerename), FileMode.Create))
                        {
                            _rmAssetImageDtl.AssetId = assetId;
                            _rmAssetImageDtl.ImageTypeCode = photoType;
                            _rmAssetImageDtl.SNO = i;
                            _rmAssetImageDtl.FileName = file.FileName;
                            _rmAssetImageDtl.ActiveYn = true;
                            if (i < 10)
                            {
                                _rmAssetImageDtl.ImageFilenameSys = AssetId.ToString() + "_" + photoType + "_" + "00" + i;
                            }
                            else if (i >= 10 && i < 100)
                            {
                                _rmAssetImageDtl.ImageFilenameSys = AssetId.ToString() + "_" + photoType + "_" + "0" + i;
                            }
                            else
                            {
                                _rmAssetImageDtl.ImageFilenameSys = AssetId.ToString() + "_" + photoType + "_" + i;
                            }
                            _rmAssetImageDtl.ImageFilename = $"FormADetail/{AssetId}/{photoTypeforUrl}/{filerename}";

                            await file.CopyToAsync(fileStream);
                        }
                        _rmAssetImageDtl.CreatedDate = DateTime.UtcNow;
                        _rmAssetImageDtl.ModifyBy = _security.UserID.ToString();
                        _rmAssetImageDtl.CreatedBy = _security.UserID.ToString();
                        _rmAssetImageDtl.ModifyDate = DateTime.UtcNow;
                        uploadedFiles.Add(_rmAssetImageDtl);
                        i = i + 1;
                    }
                }
                await _formaImgService.SaveImageDtl(uploadedFiles);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // FormA Image Retrive from TAB
        [HttpPost]
        public async Task<List<FormAImageListRequestDTO>> GetImages(string assetId)
        {
            try
            {
                int asset_Id = Convert.ToInt32(assetId);
                string wwwPath = this.Environment.WebRootPath;
                List<FormAImageListRequestDTO> response = new List<FormAImageListRequestDTO>();
                List<FormAImageListRequestDTO> imagelist = new List<FormAImageListRequestDTO>();
                response = await _formaImgService.GetAllImageByAssetPK(asset_Id);

                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Form J Image Upload For Tab  
        [HttpPost]
        public async Task<int> FormJImageUploadedTab(IFormCollection formFile, int assetId, string photoType)
        {
            try
            {
                int i = await _formjImgService.LastInsertedSRNO(assetId, photoType);
                i++;
                IFormCollection files = Request.ReadFormAsync().Result;
                string photoTypes = Regex.Replace(photoType, @"[^a-zA-Z]", "");
                string path = Path.Combine(this.Environment.WebRootPath, "Uploads", "FormJDetail", assetId.ToString(), photoTypes);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                List<FormJImageListRequestDTO> uploadedFiles = new List<FormJImageListRequestDTO>();
                foreach (var file in files.Files)
                {
                    if (file != null)
                    {
                        FormJImageListRequestDTO _rmAssetImageDtl = new FormJImageListRequestDTO();
                        string fileName = Path.GetFileName(file.FileName);
                        string fileRename = i + "_" + photoType + "_" + fileName;

                        //Commented By John
                        //using (var stream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                        using (var stream = new FileStream(Path.Combine(path, fileRename), FileMode.Create))
                        {
                            _rmAssetImageDtl.AssetId = assetId;
                            _rmAssetImageDtl.ImageTypeCode = photoType;
                            _rmAssetImageDtl.SNO = i;
                            _rmAssetImageDtl.FileName = file.FileName;
                            _rmAssetImageDtl.ActiveYn = true;
                            if (i < 10)
                            {
                                _rmAssetImageDtl.ImageFilenameSys = assetId.ToString() + "_" + photoType + "_" + "00" + i;
                            }
                            else if (i >= 10 && i < 100)
                            {
                                _rmAssetImageDtl.ImageFilenameSys = assetId.ToString() + "_" + photoType + "_" + "0" + i;
                            }
                            else
                            {
                                _rmAssetImageDtl.ImageFilenameSys = assetId.ToString() + "_" + photoType + "_" + i;
                            }
                            _rmAssetImageDtl.ImageFilename = $"/Uploads/FormJDetail/{assetId}/{photoType}/{fileRename}";

                            await file.CopyToAsync(stream);
                        }
                        _rmAssetImageDtl.CreatedBy = _security.UserID.ToString();
                        _rmAssetImageDtl.ModifyBy = _security.UserID.ToString();
                        _rmAssetImageDtl.CreatedDate = DateTime.UtcNow;
                        _rmAssetImageDtl.ModifyDate = DateTime.UtcNow;
                        uploadedFiles.Add(_rmAssetImageDtl);
                        i = i + 1;
                    }
                }

                int rowsAffetcted = await _formjImgService.SaveImageDtl(uploadedFiles);
                return rowsAffetcted;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<IActionResult> GetActiveRefIDs(string activityCode, string roadCode, int fromCHKM, string fromCHM, int toCHKM, string toCHM)
        {
            return Json(await _formAService.GetActiveRefIDs(activityCode, roadCode, fromCHKM, fromCHM, toCHKM, toCHM), Common.Utility.JsonOption);
        }

        [HttpPost]
        public async Task<IActionResult> LoadHeaderList(DataTableAjaxPostModel<FormASearchGridDTO> searchData)
        {
            if (Request.Form.ContainsKey("columns[0][search][value]"))
            {
                searchData.filterData.SmartInputValue = Request.Form["columns[0][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[1][search][value]"))
            {
                searchData.filterData.RMU = Request.Form["columns[1][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[2][search][value]"))
            {
                searchData.filterData.Section = Request.Form["columns[2][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[3][search][value]"))
            {
                searchData.filterData.Road_Code = Request.Form["columns[3][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[4][search][value]"))
            {
                searchData.filterData.Asset_GroupCode = Request.Form["columns[4][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[5][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[5][search][value]"].ToString()))
                {
                    searchData.filterData.Month = Convert.ToInt32(Request.Form["columns[5][search][value]"].ToString());
                }
            }
            if (Request.Form.ContainsKey("columns[6][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[6][search][value]"].ToString()))
                {
                    searchData.filterData.Year = Convert.ToInt32(Request.Form["columns[6][search][value]"].ToString());
                }
            }
            if (Request.Form.ContainsKey("columns[7][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[7][search][value]"].ToString()))
                {
                    searchData.filterData.ChinageFromKm = Convert.ToInt32(Request.Form["columns[7][search][value]"].ToString());
                }
            }
            if (Request.Form.ContainsKey("columns[8][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[8][search][value]"].ToString()))
                {
                    searchData.filterData.ChinageFromM = Request.Form["columns[8][search][value]"].ToString();
                }
            }
            if (Request.Form.ContainsKey("columns[9][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[9][search][value]"].ToString()))
                {
                    searchData.filterData.ChinageToKm = Convert.ToInt32(Request.Form["columns[9][search][value]"].ToString());
                }
            }
            if (Request.Form.ContainsKey("columns[10][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[10][search][value]"].ToString()))
                {
                    searchData.filterData.ChinageToM = Request.Form["columns[10][search][value]"].ToString();
                }
            }

            FilteredPagingDefinition<FormASearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormASearchGridDTO>();

            filteredPagingDefinition.Filters = searchData.filterData;
            filteredPagingDefinition.RecordsPerPage = searchData.length; //Convert.ToInt32(Request.Form["length"]);
            filteredPagingDefinition.StartPageNo = searchData.start; //Convert.ToInt32(Request.Form["start"]); //TODO
            if (searchData.order != null)
            {
                filteredPagingDefinition.ColumnIndex = searchData.order[0].column;
                filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }
            var result = await _formAService.GetFilteredFormAGrid(filteredPagingDefinition).ConfigureAwait(false);

            if (result.PageResult.Count > 0)
            {
                for (int i = 0; i < result.PageResult.Count; i++)
                {
                    result.PageResult[i].MonthYear = ((result.PageResult[i].Month ?? 0) < 10 ? "0" : "") + (result.PageResult[i].Month ?? 0) + "/" + (result.PageResult[i].Year.HasValue ? result.PageResult[i].Year.Value.ToString() : "2020");//TODO - hardcoded for demo - by John -  To be reworked
                    result.PageResult[i].Status = result.PageResult[i].SubmitSts ? "Submitted" : "Saved";
                }
            }

            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }
    }
}

