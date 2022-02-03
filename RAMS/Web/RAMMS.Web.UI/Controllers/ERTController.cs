using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RAMMS.Business.ServiceProvider;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.SearchBO;
using ClosedXML.Excel;
using Microsoft.Extensions.Configuration;
using System.Drawing;
using RAMMS.DTO.Wrappers;
using RAMMS.Web.UI.Models;
using Serilog;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Net.Http;
using System.Collections;

namespace RAMMS.Web.UI.Controllers
{
    public class ERTController : Models.BaseController
    {
        private readonly IFormXService _formXService;
        private readonly IFormJServices _formJService;
        private readonly IFormDService _formDService;
        private readonly IBridgeBO _bridgeBO;
        private readonly IDDLookupBO _dDLookupBO;
        private readonly IFormABO _formABO;
        private IHostingEnvironment Environment;
        private readonly ILogger _logger;
        private readonly IDDLookUpService _ddLookupService;

        FormXModel _formXModel = new FormXModel();
        FormDModel _formDModel = new FormDModel();
        FormDLabourDtlModel _formDLabourModel = new FormDLabourDtlModel();
        FormDMaterialDetailsModel _formDMaterialModel = new FormDMaterialDetailsModel();
        FormDEquipDetailsModel _formDEquipmentModel = new FormDEquipDetailsModel();
        FormDDetailsDtlModel _formDDetailsModel = new FormDDetailsDtlModel();

        private readonly IRoadMasterService _roadMasterService;

        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly ISecurity _security;

        public ERTController(IDDLookupBO _ddLookupBO,
             IFormABO formABO,
           IHostingEnvironment _environment,
           IFormXService formXService,
           IFormDService formDService,
            IFormJServices formJServices,
           IDDLookUpService ddLookupService,
           ILogger logger, IRoadMasterService roadMaster, IUserService userService, IWebHostEnvironment webhostenvironment, IConfiguration configuration, IBridgeBO bridgeBO, ISecurity security)
        {
            _dDLookupBO = _ddLookupBO;
            Environment = _environment;
            _ddLookupService = ddLookupService;
            _roadMasterService = roadMaster;
            _userService = userService;
            _formXService = formXService ?? throw new ArgumentNullException(nameof(formXService));
            _formDService = formDService ?? throw new ArgumentNullException(nameof(formDService));
            _formJService = formJServices ?? throw new ArgumentNullException(nameof(formJServices));
            _webHostEnvironment = webhostenvironment;
            _configuration = configuration;
            _bridgeBO = bridgeBO;
            _security = security;
        }

        public async Task LoadDropDowns()
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            RoadMasterRequestDTO roadMasterReq = new RoadMasterRequestDTO();
            ddLookup.Type = "ComMode";
            ViewData["communicationmode"] = await _ddLookupService.GetDdLookup(ddLookup);

            ddLookup.Type = "Reportedby";
            ViewData["Reportedby"] = await _ddLookupService.GetDdLookup(ddLookup);

            ddLookup.Type = "RMU";
            ViewData["RMU"] = await _formDService.GetRMU();

            ddLookup.Type = "RMU";
            ViewBag.RMUList = await _ddLookupService.GetLookUpTextConcat(ddLookup);

            ViewData["USERVER"] = _userService.GetUserSelectList(null);

            ddLookup.Type = "RD_Name";
            ViewData["RD_Name"] = await _ddLookupService.GetDdLookup(ddLookup);

            ddLookup.Type = "ACT-Main_Task";
            ViewData["ACTMainTask"] = await _ddLookupService.GetLookUpCodeText(ddLookup);

            ddLookup.Type = "ACT-Sub_Task";
            ViewData["ACTSubTask"] = await _ddLookupService.GetLookUpCodeTextConcat(ddLookup);

            ddLookup.Type = "ACT-Main_Task";
            ViewData["ACTMainTaskList"] = await _ddLookupService.GetLookUpCodeTextConcat(ddLookup);

            ddLookup.Type = "ACT-Sub_Task";
            ViewData["ACTSubTaskList"] = await _ddLookupService.GetDdLookup(ddLookup);

            FormASearchDropdown ddl = _formJService.GetDropdown(new RequestDropdownFormA { });

            ViewData["SectionCode"] = ddl.Section.Select(s => new SelectListItem { Text = s.Text, Value = s.Value }).ToArray();

            ddLookup.Type = "Month";
            ViewData["Months"] = await _ddLookupService.GetDdLookup(ddLookup);

            ddLookup.Type = "Year";
            ViewData["Year"] = await _ddLookupService.GetDdLookup(ddLookup);

            ddLookup.Type = "Day";
            ViewData["Day"] = await _ddLookupService.GetLookUpValueDesc(ddLookup);

            ddLookup.Type = "Week No";
            ViewData["WeekNo"] = await _ddLookupService.GetDdDescValue(ddLookup);

            ddLookup.Type = "Status";
            ViewData["Status"] = await _ddLookupService.GetDdDescValue(ddLookup);

        }

        public async Task<IActionResult> GetUserByCode(int userCode)
        {
            UserRequestDTO userRequest = new UserRequestDTO();
            userRequest.UserId = userCode;
            var _RMAllData = await _userService.GetUserNameByCode(userRequest);
            return Json(_RMAllData);
        }

        public async Task<IActionResult> Index()
        {
            await LoadDropDowns();

            FormXModel formxModel = new FormXModel();
            FormXSearchGridDTO filterData = new FormXSearchGridDTO();

            return View("~/Views/ERT/FormX/FormX.cshtml", _formXModel);
        }

        [HttpPost]
        public IActionResult ChooseFile(IList<IFormFile> formFile, string refNO)
        {
            string wwwPath = this._webHostEnvironment.WebRootPath;
            int i = 0;
            string path = Path.Combine(wwwPath, Path.Combine(@"Uploads\FormX\", refNO));
            string filePath = "";
            string fileName = "";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (IFormFile postedFile in formFile)
            {
                filePath = @"Uploads\FormX\" + refNO;
                AccUccImageDtlRequestDTO _rmAssetImageDtl = new AccUccImageDtlRequestDTO();
                fileName = Path.GetFileName(postedFile.FileName);
                string fileRename = i + "_" + refNO + "_" + fileName;
                filePath = filePath + @"\" + fileRename;
                using (FileStream stream = new FileStream(Path.Combine(path, fileRename), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                i = i + 1;
            }
            var obj = new { filePath = filePath, fileName = fileName };
            return Json(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Save(FormXHeaderRequestDTO saveObj)
        {
            int rowsAffected = 0;
            int refNo = 0;
            FormXHeaderRequestDTO saveRequestObj = new FormXHeaderRequestDTO();
            saveRequestObj = saveObj;
            if (saveObj.No == 0 || saveObj.No == null)
            {
                var detail = await _formXService.SaveHeaderwithResponse(saveRequestObj);
                if (detail != null)
                {
                    refNo = detail.No.GetValueOrDefault();
                }
            }
            else
            {
                rowsAffected = await _formXService.UpdateFormXAsync(saveRequestObj);
                refNo = saveObj.No.Value;
            }
            return Json(refNo);
        }

        [CAuthorize(ModuleName = ModuleNameList.Emergency_Response_Team)]
        public async Task<IActionResult> FormX([FromQuery(Name = "vid")] string viewId)
        {
            ViewBag.ViewId = viewId;
            await LoadDropDowns();
            return View("~/Views/ERT/FormX/FormX.cshtml");
        }

        public async Task<IActionResult> AddFormX(string assetName)
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            base.LoadLookupService(GroupNameList.Supervisor, GroupNameList.OpeHeadMaintenance, GroupNameList.JKRSSuperiorOfficerSO);
            ddLookup.Type = "ComMode";
            ViewData["communicationmode"] = await _ddLookupService.GetDdLookup(ddLookup);
            return PartialView("~/Views/ERT/FormX/_AddFormX.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> LoadFormXList(DataTableAjaxPostModel<FormXSearchDTO> formXFilter)
        {
            if (Request.Form.ContainsKey("columns[0][search][value]"))
            {
                formXFilter.filterData.SmartInputValue = Request.Form["columns[0][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[1][search][value]"))
            {
                formXFilter.filterData.Rmu = Request.Form["columns[1][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[2][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[2][search][value]"].ToString()))
                {
                    formXFilter.filterData.ActMainCode = Convert.ToInt32(Request.Form["columns[2][search][value]"]);
                }

            }
            if (Request.Form.ContainsKey("columns[3][search][value]"))
            {

                formXFilter.filterData.ActSubCode = Request.Form["columns[3][search][value]"].ToString();

            }
            if (!string.IsNullOrEmpty(Request.Form["columns[4][search][value]"].ToString()))
            {
                if (Request.Form.ContainsKey("columns[4][search][value]"))
                {

                    formXFilter.filterData.WorkScheduleDt = Convert.ToDateTime(Request.Form["columns[4][search][value]"].ToString());
                }
            }
            else
            {
                formXFilter.filterData.WorkScheduleDt = string.IsNullOrEmpty(Request.Form["columns[4][search][value]"].ToString()) ? (DateTime?)null : Convert.ToDateTime(Request.Form["columns[4][search][value]"].ToString());
            }
            if (!string.IsNullOrEmpty(Request.Form["columns[5][search][value]"].ToString()))
            {
                if (Request.Form.ContainsKey("columns[5][search][value]"))
                {
                    formXFilter.filterData.WorkCompltDt = (Convert.ToDateTime(Request.Form["columns[5][search][value]"].ToString())).Date;
                }
            }
            else
            {
                formXFilter.filterData.WorkCompltDt = string.IsNullOrEmpty(Request.Form["columns[5][search][value]"].ToString()) ? (DateTime?)null : Convert.ToDateTime(Request.Form["columns[5][search][value]"].ToString());
            }
            if (!string.IsNullOrEmpty(Request.Form["columns[6][search][value]"].ToString()))
            {
                if (Request.Form.ContainsKey("columns[6][search][value]"))
                {
                    formXFilter.filterData.CaseClosedDt = Convert.ToDateTime(Request.Form["columns[6][search][value]"].ToString());
                }
            }
            else
            {
                formXFilter.filterData.CaseClosedDt = string.IsNullOrEmpty(Request.Form["columns[6][search][value]"].ToString()) ? (DateTime?)null : Convert.ToDateTime(Request.Form["columns[6][search][value]"].ToString());
            }
            if (Request.Form.ContainsKey("columns[7][search][value]"))
            {
                formXFilter.filterData.RoadCode = Request.Form["columns[7][search][value]"].ToString();
            }

            FilteredPagingDefinition<FormXSearchDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormXSearchDTO>();

            filteredPagingDefinition.Filters = formXFilter.filterData;
            filteredPagingDefinition.RecordsPerPage = formXFilter.length;
            filteredPagingDefinition.StartPageNo = formXFilter.start;

            if (formXFilter.order != null)
            {
                filteredPagingDefinition.ColumnIndex = formXFilter.order[0].column;
                filteredPagingDefinition.sortOrder = formXFilter.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }

            var result = await _formXService.GetFilteredFormXGrid(filteredPagingDefinition).ConfigureAwait(false);

            return Json(new { draw = formXFilter.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }

        public async Task<IActionResult> EditFormX(int headerId)
        {
            await LoadDropDowns();
            _formXModel.SaveFormXModel = new FormXHeaderRequestDTO();

            if (headerId > 0)
            {

                var result = await _formXService.GetFormXWithDetailsByNoAsync(headerId);
                _formXModel.SaveFormXModel = result;
            }

            return PartialView("~/Views/ERT/FormX/_AddFormX.cshtml", _formXModel);
        }

        [HttpPost]
        public string GetIdByRoadCode(string rdCode, string rpDate)
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

            if (rdCode != null && month != 0 && year != 0)
            {
                id = "ERT/FormX/" + rdCode + "/" + month + "/" + year;
            }
            return id;
        }

        [HttpPost]
        public IActionResult GetReferenceNo(string rdCode, string rpDate)
        {
            string id = "";
            int month = 0;
            int year = 0;
            int day = 0;
            DateTime dt;
            dt = Convert.ToDateTime(rpDate);
            if (rpDate != null)
            {
                day = string.IsNullOrEmpty(dt.ToString()) ? 0 : dt.Day;
                month = string.IsNullOrEmpty(dt.ToString()) ? 0 : dt.Month;
                year = string.IsNullOrEmpty(dt.ToString()) ? 0 : dt.Year;
            }

            (int _id, bool isexists) = this._formXService.CheckExistence(rdCode, month, year, dt);
            if (!isexists)
            {
                if (rdCode != null && month != 0 && year != 0)
                {
                    dt = Convert.ToDateTime(rpDate);
                    id = $"ERT/FormX/{rdCode}/{dt.ToShortDateString()}/{_id}-{year}";
                }
            }
            return Json(new { Reference = id, IsExists = isexists });
        }

        [HttpPost]
        public async Task<IActionResult> GetSectionBYRMUAndRoadCode(string rdCode, string rmu)
        {
            return Json(await _formXService.GetSectionByRoadCodeAndRMU(rdCode, rmu));
        }

        [HttpPost]
        public async Task<IActionResult> GetAllRoadCodeDataBySectionCode(string secCode)
        {
            RoadMasterRequestDTO _Rmroad = new RoadMasterRequestDTO();
            var id = "";
            _Rmroad.SecCode = Convert.ToInt32(secCode);
            var _RMAllData = await _roadMasterService.GetAllRoadCodeDataBySectionCode(_Rmroad);
            var obj = new
            {
                _RMAllData = _RMAllData,
                id = id
            };
            return Json(obj);
        }

        [HttpPost]
        public async Task<IActionResult> HeaderListDelete(int headerId)
        {
            int rowsAffected = 0;
            rowsAffected = await _formXService.DeActivateFormXAsync(headerId);
            return Json(rowsAffected);
        }

        [HttpPost]
        public async Task<IActionResult> GetImageListFormD(int id)
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            FormDModel formModel = new FormDModel();
            formModel.WarImageimageList = new List<WarImageDtlResponseDTO>();
            formModel.ImageTypeList = new List<string>();
            ddLookup.Type = "Photo Type";
            ddLookup.TypeCode = "FormX_War";
            ViewBag.PhotoTypeList = await _ddLookupService.GetDdLookup(ddLookup);
            formModel.WarImageimageList = await _formDService.GetWarImageList(id);
            formModel.SaveFormDModel = new FormDHeaderRequestDTO();

            formModel.SaveFormDModel.No = id;
            formModel.ImageTypeList = formModel.WarImageimageList.Select(c => c.ImageTypeCode).Distinct().ToList();
            return PartialView("~/Views/ERT/FormD/_PhotoSectionPage.cshtml", formModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetImageList(int id, string location)
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            FormXModel formModel = new FormXModel();
            formModel.WarImageimageList = new List<WarImageDtlResponseDTO>();
            formModel.ImageTypeList = new List<string>();
            ddLookup.Type = "Photo Type";
            ddLookup.TypeCode = "FormX_War";
            ViewBag.PhotoTypeList = await _ddLookupService.GetDdLookup(ddLookup);
            formModel.WarImageimageList = await _formXService.GetWarImageList(id);
            formModel.SaveFormXModel = new FormXHeaderRequestDTO();

            formModel.SaveFormXModel.No = id;
            formModel.ImageTypeList = formModel.WarImageimageList.Select(c => c.ImageTypeCode).Distinct().ToList();
            List<WarImageDtlResponseDTO> TempList = new List<WarImageDtlResponseDTO>();
            TempList = formModel.WarImageimageList.ToList();
            if (TempList.Count != 0)
            {
                for (int i = 0; i < TempList.Count; i++)
                {
                    TempList[i].FileFullPath = location + "\\" + TempList[i].ImageFilenameUpload;
                }
            }
            return PartialView("~/Views/ERT/FormX/_PhotoSectionPage.cshtml", formModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetUSeeUPage(int id)
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            FormXModel formModel = new FormXModel();
            formModel.AccUccImageList = new List<AccUccImageDtlResponseDTO>();
            formModel.ImageTypeList = new List<string>();
            formModel.SaveFormXModel = new FormXHeaderRequestDTO();

            ddLookup.Type = "Photo Type";
            ddLookup.TypeCode = "FormX_useeu";
            ViewBag.PhotoTypeList = await _ddLookupService.GetDdLookup(ddLookup);

            formModel.SaveFormXModel.No = id;
            formModel.AccUccImageList = await _formXService.GetAccUccImageList(id);
            formModel.ImageTypeList = formModel.AccUccImageList.Select(c => c.AccUcu).Distinct().ToList();

            return PartialView("~/Views/ERT/FormX/_USeeUPloadPage.cshtml", formModel);
        }

        [HttpPost]
        public async Task<IActionResult> ImageUploadedWar(IList<IFormFile> FormFile, string id, List<string> photoType)
        {
            try
            {
                string wwwPath = this._webHostEnvironment.WebRootPath;
                string contentPath = this._webHostEnvironment.ContentRootPath;
                string _id = Regex.Replace(id, @"[^0-9a-zA-Z]+", "");



                int j = 0;
                foreach (IFormFile postedFile in FormFile)
                {
                    List<WarImageDtlRequestDTO> uploadedFiles = new List<WarImageDtlRequestDTO>();
                    int i = await _formXService.LastInsertedWARSRNO(int.Parse(id), photoType[j]);
                    i++;
                    string photo_Type = Regex.Replace(photoType[j], @"[^a-zA-Z]", "");
                    string filepath = Path.Combine(@"Uploads\\FormX\\", _id, photo_Type);
                    string path = Path.Combine(wwwPath, filepath);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    WarImageDtlRequestDTO _rmAssetImageDtl = new WarImageDtlRequestDTO();
                    string fileName = Path.GetFileName(postedFile.FileName);
                    string fileRename = i + "_" + photoType[j] + "_" + fileName;
                    string storedpath = Path.Combine(path, fileRename);
                    using (FileStream stream = new FileStream(storedpath, FileMode.Create))
                    {
                        _rmAssetImageDtl.FxhPkRefNo = int.Parse(id);
                        _rmAssetImageDtl.ImageTypeCode = photo_Type;
                        _rmAssetImageDtl.ImageUserFilename = postedFile.FileName;
                        _rmAssetImageDtl.ImageSrno = i;
                        _rmAssetImageDtl.ImageFilenameSys = postedFile.FileName;
                        _rmAssetImageDtl.ActiveYn = true;
                        if (i < 10)
                        {
                            _rmAssetImageDtl.ImageFilenameSys = _id + "_" + photo_Type + "_" + "00" + i;
                        }
                        else if (i >= 10 && i < 100)
                        {
                            _rmAssetImageDtl.ImageFilenameSys = _id + "_" + photo_Type + "_" + "0" + i;
                        }
                        else
                        {
                            _rmAssetImageDtl.ImageFilenameSys = _id + "_" + photo_Type + "_" + i;
                        }
                        _rmAssetImageDtl.ImageFilenameUpload = $"{filepath}\\{fileRename}";


                        postedFile.CopyTo(stream);


                    }
                    uploadedFiles.Add(_rmAssetImageDtl);
                    if (uploadedFiles.Count() > 0)
                    {
                        await _formXService.SaveFormXWarImage(uploadedFiles);

                    }
                    j++;
                }

                return Json("");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> ImageUploadedUCU(IList<IFormFile> formFile, string id, List<string> photoType)
        {
            try
            {
                string wwwPath = this._webHostEnvironment.WebRootPath;
                string contentPath = this._webHostEnvironment.ContentRootPath;
                string _id = id;

                int j = 0;
                foreach (IFormFile postedFile in formFile)
                {
                    List<AccUccImageDtlRequestDTO> uploadedFiles = new List<AccUccImageDtlRequestDTO>();
                    int i = await _formXService.LastInsertedUCUSRNO(int.Parse(id), photoType[j]);
                    i++;
                    string photo_Type = Regex.Replace(photoType[j], @"[^a-zA-Z]", "");
                    string subPath = Path.Combine(@"Uploads/FormX/", _id, photo_Type);
                    string path = Path.Combine(wwwPath, subPath);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    AccUccImageDtlRequestDTO _rmAssetImageDtl = new AccUccImageDtlRequestDTO();
                    string fileName = Path.GetFileName(postedFile.FileName);
                    string fileRename = i + "_" + photoType + "_" + fileName;
                    using (FileStream stream = new FileStream(Path.Combine(path, fileRename), FileMode.Create))
                    {
                        _rmAssetImageDtl.FxhPkRefNo = int.Parse(id);
                        _rmAssetImageDtl.AccUcu = photoType[j];
                        _rmAssetImageDtl.ImageUserFilename = postedFile.FileName;
                        _rmAssetImageDtl.ImageSrno = i;
                        _rmAssetImageDtl.ImageFilenameSys = postedFile.FileName;
                        _rmAssetImageDtl.ActiveYn = true;
                        if (i < 10)
                        {
                            _rmAssetImageDtl.ImageFilenameSys = _id + "_" + photo_Type + "_" + "00" + i;
                        }
                        else if (i >= 10 && i < 100)
                        {
                            _rmAssetImageDtl.ImageFilenameSys = _id + "_" + photo_Type + "_" + "0" + i;
                        }
                        else
                        {
                            _rmAssetImageDtl.ImageFilenameSys = _id + "_" + photo_Type + "_" + i;
                        }
                        _rmAssetImageDtl.ImageFilenameUpload = $"{subPath}/{fileRename}";


                        postedFile.CopyTo(stream);


                    }
                    uploadedFiles.Add(_rmAssetImageDtl);
                    if (uploadedFiles.Count() > 0)
                    {
                        await _formXService.SaveFormXAccUccDtl(uploadedFiles);

                    }
                    j = j + 1;
                }
                return Json("");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id, string type)
        {
            int rowsAffected = 0;
            if (type == "War") rowsAffected = await _formXService.DeActivateWarImage(id);
            if (type == "UCU") rowsAffected = await _formXService.DeActivateAccUCc(id);
            return Json(rowsAffected);
        }

        public IActionResult DowloadPdf(string id)
        {

            var webRoot = _webHostEnvironment.WebRootPath;
            String fileName = "";
            string path = webRoot + @"\Uploads\FormX\USeeU_Accident_PDF";
            HttpContext.Response.ContentType = "application/pdf";
            if (id == "UCU")
            {
                path = path + @"\Unsafe Act & Condition form.pdf";
                fileName = "Unsafe Act & Condition form";
            }
            else if (id == "Accid")
            {
                path = path + @"\Accident Investigation Form.pdf";
                fileName = "Accident Investigation Form";
            }
            try
            {
                var result = new FileContentResult(System.IO.File.ReadAllBytes(path), HttpContext.Response.ContentType)
                {
                    FileDownloadName = $"{fileName}.pdf"
                };

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<IActionResult> OpenPdf(int id)
        {
            FormXModel formModel = new FormXModel();
            formModel.AccUccImage = new AccUccImageDtlResponseDTO();
            formModel.AccUccImage = await _formXService.GetAccUccImage(id);
            string wwwPath = this._webHostEnvironment.WebRootPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(wwwPath + @"\" + formModel.AccUccImage.ImageFilenameUpload);
            return File(FileBytes, "application/pdf");
        }

        public async Task<IActionResult> OpenDocWar(int id)
        {
            FormXModel formModel = new FormXModel();
            formModel.WarDocData = new WarImageDtlResponseDTO();
            formModel.WarDocData = await _formXService.GetWarDocById(id);
            string wwwPath = this._webHostEnvironment.WebRootPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(wwwPath + @"\" + formModel.WarDocData.ImageFilenameUpload);
            return File(FileBytes, "application/pdf");
        }

        //Form D
        public async Task<IActionResult> OpenPdfFormD(int id, string headerId)
        {
            FormDModel formModel = new FormDModel();
            formModel.AccUccImage = new AccUccImageDtlResponseDTO();
            formModel.AccUccImage = await _formDService.GetAccUccImage(id);
            string wwwPath = this._webHostEnvironment.WebRootPath;
            string photoType = Regex.Replace(formModel.AccUccImage.AccUcu, @"[^a-zA-Z]", "");
            byte[] FileBytes = System.IO.File.ReadAllBytes(wwwPath + @"\" + formModel.AccUccImage.ImageFilenameUpload);
            return File(FileBytes, "application/pdf");
        }

        public IActionResult DownloadPdfFormD(string id)
        {

            var webRoot = _webHostEnvironment.WebRootPath;
            String fileName = "";
            string path = webRoot + @"\Uploads\FormD\USeeU_Accident_PDF";
            HttpContext.Response.ContentType = "application/pdf";
            if (id == "UCU")
            {
                path = path + @"\Unsafe Act & Condition form.pdf";
                fileName = "Unsafe Act & Condition form";
            }
            else if (id == "Accid")
            {
                path = path + @"\Accident Investigation Form.pdf";
                fileName = "Accident Investigation Form";
            }
            try
            {
                var result = new FileContentResult(System.IO.File.ReadAllBytes(path), HttpContext.Response.ContentType)
                {
                    FileDownloadName = $"{fileName}.pdf"
                };

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> HeaderListFormDDelete(int headerId)
        {
            int rowsAffected = 0;
            rowsAffected = await _formDService.DeActivateFormDAsync(headerId);
            return Json(rowsAffected);

        }

        [HttpPost]
        public async Task<IActionResult> FormDLabourDelete(int headerId)
        {
            int rowsAffected = 0;
            rowsAffected = await _formDService.DeActivateFormDLabourAsync(headerId);
            return Json(rowsAffected);

        }

        [HttpPost]
        public async Task<IActionResult> FormDMaterialDelete(int headerId)
        {
            int rowsAffected = 0;
            rowsAffected = await _formDService.DeActivateFormMaterialDAsync(headerId);
            return Json(rowsAffected);

        }

        [HttpPost]
        public async Task<IActionResult> FormDEquipmentDelete(int headerId)
        {
            int rowsAffected = 0;
            rowsAffected = await _formDService.DeActivateFormDEquipmentAsync(headerId);
            return Json(rowsAffected);

        }

        [HttpPost]
        public async Task<IActionResult> FormDDetailsDelete(int headerId)
        {
            int rowsAffected = 0;
            rowsAffected = await _formDService.DeActivateFormDDetailAsync(headerId);
            return Json(rowsAffected);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteImageFormD(int id, string type)
        {
            int rowsAffected = 0;
            if (type == "War") rowsAffected = await _formDService.DeActivateWarImage(id);
            if (type == "UCU") rowsAffected = await _formDService.DeActivateAccUCc(id);
            return Json(rowsAffected);
        }

        [HttpPost]
        public async Task<IActionResult> GetUSeeUPageFormD(int id)
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            FormDModel formModel = new FormDModel();
            formModel.AccUccImageList = new List<AccUccImageDtlResponseDTO>();
            formModel.ImageTypeList = new List<string>();
            formModel.SaveFormDModel = new FormDHeaderRequestDTO();

            ddLookup.Type = "Photo Type";
            ddLookup.TypeCode = "FormX_useeu";
            ViewBag.PhotoTypeList = await _ddLookupService.GetDdLookup(ddLookup);

            formModel.SaveFormDModel.No = id;
            formModel.AccUccImageList = await _formDService.GetAccUccImageList(id);
            formModel.ImageTypeList = formModel.AccUccImageList.Select(c => c.AccUcu).Distinct().ToList();

            return PartialView("~/Views/ERT/FormD/_USeeUPloadPage.cshtml", formModel);
        }

        public async Task<IActionResult> ImageUploadedWarFormD(IList<IFormFile> formFile, string id, List<string> photoType)
        {
            try
            {
                bool successFullyUploaded = false;
                string wwwPath = this._webHostEnvironment.WebRootPath;
                string contentPath = this._webHostEnvironment.ContentRootPath;
                string _id = Regex.Replace(id, @"[^0-9a-zA-Z]+", "");

                int j = 0;
                foreach (IFormFile postedFile in formFile)
                {
                    List<WarImageDtlRequestDTO> uploadedFiles = new List<WarImageDtlRequestDTO>();
                    WarImageDtlRequestDTO _rmAssetImageDtl = new WarImageDtlRequestDTO();
                    string photo_Type = Regex.Replace(photoType[j], @"[^a-zA-Z]", "");
                    string subPath = Path.Combine(@"Uploads/FormD/", _id, photo_Type);
                    string path = Path.Combine(wwwPath, Path.Combine(@"Uploads\FormD\", _id, photo_Type));
                    int i = await _formDService.LastInsertedWARSRNO(int.Parse(id), photo_Type);
                    i++;
                    string fileName = Path.GetFileName(postedFile.FileName);
                    string fileRename = i + "_" + photo_Type + "_" + fileName;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream stream = new FileStream(Path.Combine(path, fileRename), FileMode.Create))
                    {
                        _rmAssetImageDtl.HeaderId = int.Parse(id);
                        _rmAssetImageDtl.ImageTypeCode = photoType[j];
                        _rmAssetImageDtl.ImageUserFilename = postedFile.FileName;
                        _rmAssetImageDtl.ImageSrno = i;

                        _rmAssetImageDtl.ActiveYn = true;
                        if (i < 10)
                        {
                            _rmAssetImageDtl.ImageFilenameSys = _id + "_" + photo_Type + "_" + "00" + i;
                        }
                        else if (i >= 10 && i < 100)
                        {
                            _rmAssetImageDtl.ImageFilenameSys = _id + "_" + photo_Type + "_" + "0" + i;
                        }
                        else
                        {
                            _rmAssetImageDtl.ImageFilenameSys = _id + "_" + photo_Type + "_" + i;
                        }
                        _rmAssetImageDtl.ImageFilenameUpload = $"{subPath}/{fileRename}";


                        postedFile.CopyTo(stream);


                    }
                    uploadedFiles.Add(_rmAssetImageDtl);
                    if (uploadedFiles.Count() > 0)
                    {
                        await _formDService.SaveFormDWarImage(uploadedFiles);
                        successFullyUploaded = true;
                    }
                    else
                    {
                        successFullyUploaded = false;
                    }

                    j = j + 1;
                }

                return Json(successFullyUploaded);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public async Task<IActionResult> ImageUploadedUCUFormD(IList<IFormFile> formFile, string id, string photoType)
        {
            try
            {
                int i = await _formDService.LastInsertedUCUSRNO(int.Parse(id), photoType);
                i++;
                bool successFullyUploaded = false;
                string wwwPath = this._webHostEnvironment.WebRootPath;
                string contentPath = this._webHostEnvironment.ContentRootPath;
                string _id = id;
                string photo_Type = Regex.Replace(photoType, @"[^a-zA-Z]", "");
                string subPath = Path.Combine(@"Uploads/FormD/", _id, photo_Type);
                string path = Path.Combine(wwwPath, Path.Combine(@"Uploads\FormD\", _id, photo_Type));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                List<AccUccImageDtlRequestDTO> uploadedFiles = new List<AccUccImageDtlRequestDTO>();
                foreach (IFormFile postedFile in formFile)
                {
                    AccUccImageDtlRequestDTO _rmAssetImageDtl = new AccUccImageDtlRequestDTO();
                    string fileName = Path.GetFileName(postedFile.FileName);
                    string fileRename = i + "_" + photoType + "_" + fileName;
                    using (FileStream stream = new FileStream(Path.Combine(path, fileRename), FileMode.Create))
                    {
                        _rmAssetImageDtl.HeaderId = int.Parse(id);
                        _rmAssetImageDtl.AccUcu = photoType;
                        _rmAssetImageDtl.ImageUserFilename = postedFile.FileName;
                        _rmAssetImageDtl.ImageSrno = i;
                        _rmAssetImageDtl.ImageFilenameSys = postedFile.FileName;
                        _rmAssetImageDtl.ActiveYn = true;
                        if (i < 10)
                        {
                            _rmAssetImageDtl.ImageFilenameSys = _id + "_" + photoType + "_" + "00" + i;
                        }
                        else if (i >= 10 && i < 100)
                        {
                            _rmAssetImageDtl.ImageFilenameSys = _id + "_" + photoType + "_" + "0" + i;
                        }
                        else
                        {
                            _rmAssetImageDtl.ImageFilenameSys = _id + "_" + photoType + "_" + i;
                        }
                        _rmAssetImageDtl.ImageFilenameUpload = $"{subPath}/{fileRename}";


                        postedFile.CopyTo(stream);


                    }
                    uploadedFiles.Add(_rmAssetImageDtl);
                    i = i + 1;
                }

                if (uploadedFiles.Count() > 0)
                {
                    await _formDService.SaveFormDAccUccDtl(uploadedFiles);
                    successFullyUploaded = true;
                }
                else
                {
                    successFullyUploaded = false;
                }
                return Json(successFullyUploaded);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [CAuthorize(ModuleName = ModuleNameList.Emergency_Response_Team)]
        public async Task<IActionResult> FormD()
        {            
            await LoadDropDowns();
            return View("~/Views/ERT/FormD/FormD.cshtml");
        }

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

        private int GetWeek(DateTime date)
        {
            var day = (int)CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date.AddDays(4 - (day == 0 ? 7 : day)), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        [HttpPost]
        public async Task<IActionResult> LoadFormDList(DataTableAjaxPostModel<FormDSearchGridDTO> formDFilter)
        {

            if (Request.Form.ContainsKey("columns[0][search][value]"))
            {
                formDFilter.filterData.SmartInputValue = Request.Form["columns[0][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[1][search][value]"))
            {
                formDFilter.filterData.RMU = Request.Form["columns[1][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[2][search][value]"))
            {
                formDFilter.filterData.Road_Code = Request.Form["columns[2][search][value]"].ToString();
            }

            if (Request.Form.ContainsKey("columns[3][search][value]"))
            {
                string searchByDate = "", years = "", day = "", month = "";
                int WeekNo = 0;
                searchByDate = Request.Form["columns[3][search][value]"].ToString(); //yyyy-mm-dd
                if (searchByDate != "" && searchByDate.IndexOf("-") >= 0)
                {
                    years = searchByDate.Split("-")[0];
                    month = searchByDate.Split("-")[1];
                    day = searchByDate.Split("-")[2];
                    DateTime dt = new DateTime(Convert.ToInt32(years), Convert.ToInt32(month), Convert.ToInt32(day));
                    WeekNo = GetWeek(dt);
                    formDFilter.filterData.WeekNo = Convert.ToString(WeekNo);
                    formDFilter.filterData.Year = dt.Year;
                    formDFilter.filterData.WeekDay = dt.ToString("dddd");
                }
            }

            FilteredPagingDefinition<FormDSearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormDSearchGridDTO>();
            filteredPagingDefinition.Filters = formDFilter.filterData;
            filteredPagingDefinition.RecordsPerPage = formDFilter.length;
            filteredPagingDefinition.StartPageNo = formDFilter.start;

            if (formDFilter.order != null)
            {
                filteredPagingDefinition.ColumnIndex = formDFilter.order[0].column;
                filteredPagingDefinition.sortOrder = formDFilter.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }

            var result = await _formDService.GetFilteredFormDGrid(filteredPagingDefinition).ConfigureAwait(false);

            return Json(new { draw = formDFilter.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }

        static DateTime FirstDateOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;

            DateTime firstMonday = jan1.AddDays(daysOffset);

            int firstWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(jan1, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

            if (firstWeek <= 1)
            {
                weekOfYear -= 1;
            }
            return firstMonday.AddDays(weekOfYear * 7);
        }

        public async Task<IActionResult> EditFormD(int id, string view)
        {
            base.LoadLookupService(GroupNameList.Supervisor, GroupNameList.OperationsExecutive, GroupNameList.OpeHeadMaintenance, GroupNameList.JKRSSuperiorOfficerSO);
            _formDModel.SaveFormDModel = new FormDHeaderRequestDTO();
            DDLookUpDTO ddLookup = new DDLookUpDTO();

            //Labour
            FormDLabourDtlModel formDLabour = new FormDLabourDtlModel();
            formDLabour.selectList = await _formDService.GetLabourCode();
            _formDModel.FormDLabour = formDLabour;
            ddLookup.Type = "UNIT";
            _formDModel.FormDLabour.UnitList = await _ddLookupService.GetDdLookup(ddLookup);

            //Material
            FormDMaterialDetailsModel formDMaterial = new FormDMaterialDetailsModel();
            formDMaterial.selectList = await _formDService.GetMaterialCode();
            _formDModel.FormDMaterial = formDMaterial;
            ddLookup.Type = "UNIT";
            _formDModel.FormDMaterial.UnitList = await _ddLookupService.GetDdLookup(ddLookup);

            //Equipment
            FormDEquipDetailsModel formDEquipDetails = new FormDEquipDetailsModel();
            formDEquipDetails.selectList = await _formDService.GetEquipmentCode();
            _formDModel.FormDEquip = formDEquipDetails;

            ViewData["RMU"] = await _formDService.GetRMU();

            FormDDetailsDtlModel formDDetailsDtl = new FormDDetailsDtlModel();
            RoadMasterRequestDTO roadMasterReq = new RoadMasterRequestDTO();

            ViewData["Division"] = await _formDService.GetDivisions();

            FormASearchDropdown ddl = _formJService.GetDropdown(new RequestDropdownFormA { });

            ViewData["SectionCodeList"] = ddl.Section.Select(s => new SelectListItem { Text = s.Text, Value = s.Value }).ToArray();

            ddLookup.Type = "Year";
            var year = await _ddLookupService.GetDdLookup(ddLookup);
            ViewData["Year"] = year.Select(l => new SelectListItem { Selected = (l.Value == DateTime.Today.Year.ToString()), Text = l.Text, Value = l.Value });

            ddLookup.Type = "Day";
            var day = await _ddLookupService.GetDdLookup(ddLookup);
            ViewData["Day"] = day.Select(l => new SelectListItem { Selected = (l.Value == DateTime.Now.DayOfWeek.ToString()), Text = l.Text, Value = l.Value });

            //ddLookup.Type = "Week No";
            var week = await _ddLookupService.GetDdDescValue(ddLookup);
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            var weekno = cal.GetWeekOfYear(DateTime.Today, dfi.CalendarWeekRule,
                                         dfi.FirstDayOfWeek);

            var weekNoLst = _dDLookupBO.GetWeekNo();
            ViewData["WeekNo"] = weekNoLst.Select(l => new SelectListItem { Selected = (l.Value == weekno.ToString()), Text = l.Text, Value = l.Value });

            //var objdt = GetDateByWeekNo_WeeDay(DateTime.Today.Year.ToString(), weekno.ToString(), DateTime.Now.DayOfWeek.ToString());
            //ViewData["weedate"] = objdt.Split("~")[0];

            //ViewData["monthno"] = objdt.Split("~")[1];

            formDDetailsDtl.RoadCodeList = await _formDService.GetRoadCodeList();

            ddLookup.Type = "Site Ref";
            formDDetailsDtl.siteRefList = await _ddLookupService.GetLookUpCodeText(ddLookup);

            ddLookup.Type = "Source Type";
            formDDetailsDtl.sourceTypefList = await _ddLookupService.GetDdLookup(ddLookup);

            ddLookup.Type = "UNIT";
            formDDetailsDtl.UnitList = await _ddLookupService.GetDdLookup(ddLookup);

            ddLookup.Type = "Act-FormD";
            formDDetailsDtl.ActCodeList = await _ddLookupService.GetLookUpCodeText(ddLookup);

            ddLookup.Type = "ERTWorkStatus";
            formDDetailsDtl.WrkStatusList = await _ddLookupService.GetDdLookup(ddLookup);

            _formDModel.MaxNo = await _formDService.GetMaxIdLength();

            _formDModel.FormDDetails = formDDetailsDtl;

            _formDModel.FormDUsers = new FormDUserDetailsModel();

            _formDModel.SaveFormDModel = new FormDHeaderRequestDTO();

            _formDModel.SaveFormDModel.FormDDetails = new List<FormDDetailsRequestDTO>();


            _formDModel.SaveFormDModel.FormDLabour = new List<FormDLabourDetailsRequestDTO>();
            _formDModel.SaveFormDModel.FormDEquip = new List<FormDEquipRequestDTO>();
            _formDModel.SaveFormDModel.FormDMaterial = new List<FormDMaterialDetailsRequestDTO>();

            if (id > 0)
            {
                var result = await _formDService.GetFormDWithDetailsByNoAsync(id);
                _formDModel.SaveFormDModel = result;
                _formDModel.viewm = result.SubmitSts == true ? "1" : view;
                ViewData["RD_Code"] = await _formDService.GetRoadCodesByRMU(result.Rmu);
            }
            else
            {
                _formDModel.viewm = view != null ? view : "0";
                ViewData["RD_Code"] = await _formDService.GetRoadCodesByRMU("");
            }
            ViewBag.view = view;
            ViewData["users"] = _userService.GetUserSelectList(null);
            ViewData["Supervisor"] = await _userService.GetSupervisor();

            return PartialView("~/Views/ERT/FormD/_AddFormD.cshtml", _formDModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetformDReferenceData(string weekNo, string monthNo, string year, string crewUnit, string recCount, string day, string rmu, string secCode)
        {
            var autoRefNo = "";
            if (weekNo != null && crewUnit != null)
            {
                try
                {
                    string existId = await _formDService.CheckAlreadyExists(Convert.ToInt32(weekNo), Convert.ToInt32(year), crewUnit, day, rmu, secCode);

                    if (string.IsNullOrEmpty(existId))
                    {
                        string maxcount = (Convert.ToInt32(recCount) + 1).ToString();

                        autoRefNo = "ERT/FORM D/" + weekNo + "-" + monthNo + "-" + year + "/" + crewUnit + "/" + maxcount.PadLeft(4, '0');
                    }
                    else
                    {
                        autoRefNo = "ERT/FORM D/" + weekNo + "-" + monthNo + "-" + year + "/" + crewUnit + "/" + existId.PadLeft(4, '0');
                    }

                    return Json(new { autoRefNo = autoRefNo, DataId = existId });
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

        [HttpPost]
        public string GetDateString(string year, string weekno, string weekday)
        {
            var obj = GetDateByWeekNo_WeeDay(year, weekno, weekday);
            return obj;
        }

        public string GetDateByWeekNo_WeeDay(string year, string weekno, string weekday)
        {
            var obj = FirstDateOfWeek(Convert.ToInt32(year), Convert.ToInt32(weekno));
            var retVal = 0;
            var retMonth = "";
            //0 - sun , 1 - mon 2 - tue,3 - wed ,4 - thu,5 - fri ,6 - sat
            switch (weekday.ToLower())
            {
                case "monday":
                    retVal = obj.AddDays(1).Day;
                    retMonth = obj.AddDays(1).Month.ToString();
                    break;
                case "tuesday":
                    retVal = obj.AddDays(2).Day;
                    retMonth = obj.AddDays(2).Month.ToString();
                    break;
                case "wednesday":
                    retVal = obj.AddDays(3).Day;
                    retMonth = obj.AddDays(3).Month.ToString();
                    break;
                case "thursday":
                    retVal = obj.AddDays(4).Day;
                    retMonth = obj.AddDays(4).Month.ToString();
                    break;
                case "friday":
                    retVal = obj.AddDays(5).Day;
                    retMonth = obj.AddDays(5).Month.ToString();
                    break;
                case "saturday":
                    retVal = obj.AddDays(6).Day;
                    retMonth = obj.AddDays(6).Month.ToString();
                    break;
                case "sunday":
                    retMonth = obj.Month.ToString();
                    retVal = obj.Day;
                    break;
                default:
                    retMonth = obj.Month.ToString();
                    retVal = obj.Day;
                    break;
            }

            return retVal.ToString() + "~" + retMonth;
        }

        [HttpPost]
        public async Task<IActionResult> GetDivisionByRoadCode(string roadCode, string rpDate)
        {
            RoadMasterRequestDTO _Rmroad = new RoadMasterRequestDTO();
            var id = "";
            _Rmroad.RoadCode = roadCode;
            var _RMAllData = await _roadMasterService.GetAllRoadCodeData(_Rmroad);
            if (roadCode != "Select Road Code" && roadCode != null)
            {
                id = GetIdByRoadCode(roadCode, DateTime.Now.ToString());
            }
            var obj = new
            {
                _RMAllData = _RMAllData,
                id = id
            };
            return Json(obj);
        }


        public async Task<IActionResult> EditFormLabour(int id)
        {
            _formDLabourModel = new FormDLabourDtlModel();

            _formDLabourModel.selectList = await _formDService.GetLabourCode();
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            ddLookup.Type = "LabourUnit";
            _formDLabourModel.UnitList = await _ddLookupService.GetDdLookup(ddLookup);

            if (id > 0)
            {
                var result = await _formDService.GetFormDLabourDetailsByNoAsync(id);
                _formDLabourModel.SaveFormDLabourModel = result;
            }

            return PartialView("~/Views/ERT/FormD/_LabourAdd.cshtml", _formDLabourModel);
        }

        public async Task<IActionResult> EditFormMaterial(int id)
        {
            _formDMaterialModel = new FormDMaterialDetailsModel();

            _formDMaterialModel.selectList = await _formDService.GetMaterialCode();
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            ddLookup.Type = "MaterialUnit";
            _formDMaterialModel.UnitList = await _ddLookupService.GetDdLookup(ddLookup);

            if (id > 0)
            {

                var result = await _formDService.GetFormDMaterialDetailsByNoAsync(id);
                _formDMaterialModel.SaveFormDMaterialModel = result;
            }

            return PartialView("~/Views/ERT/FormD/_MaterialAdd.cshtml", _formDMaterialModel);
        }

        public async Task<IActionResult> EditFormDDetails(int id, int headerid)
        {
            _formDDetailsModel = new FormDDetailsDtlModel();
            DDLookUpDTO ddLookup = new DDLookUpDTO();


            ddLookup.Type = "Site Ref";
            _formDDetailsModel.siteRefList = await _ddLookupService.GetDdLookup(ddLookup);

            ddLookup.Type = "Source Type";
            _formDDetailsModel.sourceTypefList = await _ddLookupService.GetDdDescValue(ddLookup);

            ddLookup.Type = "ProductionUnit";
            _formDDetailsModel.UnitList = await _ddLookupService.GetDdLookup(ddLookup);

            ddLookup.Type = "Act-FormD";
            _formDDetailsModel.ActCodeList = await _formDService.GetERTActivityCode();

            ddLookup.Type = "ERTWorkStatus";
            _formDDetailsModel.WrkStatusList = await _ddLookupService.GetDdLookup(ddLookup);
            var FormD = await _formDService.GetFormDWithDetailsByNoAsync(headerid);

            _formDDetailsModel.RoadCodeList = await _formDService.GetRoadCodeBySectionCode(FormD.RoadCode);

            if (id > 0)
            {
                var result = await _formDService.GetFormDDetailsByNoAsync(id);
                _formDDetailsModel.SaveFormDDetailsModel = result;

                _formDDetailsModel.SiteRef_multiSelect = result.SiteRef.Split(",").OfType<string>().ToList();
                // _formDDetailsModel.SaveFormDDetailsModel.SiteRef_multiSelect = Result.SiteRef.Split(",").OfType<string>().ToList();


                if (_formDDetailsModel.SaveFormDDetailsModel != null)
                {
                    if (_formDDetailsModel.SaveFormDDetailsModel.SourceType == "FormX")
                    {
                        _formDDetailsModel.isFromSource = true;
                        _formDDetailsModel.sourceFormList = await _formDService.GetFormXReferenceId(_formDDetailsModel.SaveFormDDetailsModel.RoadCode);
                    }
                    else if (_formDDetailsModel.SaveFormDDetailsModel.SourceType.StartsWith("New"))
                    {
                        _formDDetailsModel.isFromSource = false;
                        _formDDetailsModel.sourceFormList = await _formDService.GetFormXReferenceId("");
                    }
                }
                else
                    _formDDetailsModel.sourceFormList = await _formDService.GetFormXReferenceId("");
            }
            else
            {
                _formDDetailsModel.isFromSource = false;
                _formDDetailsModel.sourceFormList = await _formDService.GetFormXReferenceId("");
            }

            return PartialView("~/Views/ERT/FormD/_DetailsAdd.cshtml", _formDDetailsModel);
        }

        public async Task<IActionResult> EditFormDEquipmentDetails(int id)
        {
            _formDEquipmentModel = new FormDEquipDetailsModel();

            _formDEquipmentModel.selectList = await _formDService.GetEquipmentCode();
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            ddLookup.Type = "EquipmentUnit";
            _formDEquipmentModel.UnitList = await _ddLookupService.GetDdLookup(ddLookup);

            if (id > 0)
            {
                var result = await _formDService.GetFormDEquipmentDetailsByNoAsync(id);
                _formDEquipmentModel.SaveFormDEquipModel = result;
            }

            return PartialView("~/Views/ERT/FormD/_EquipAdd.cshtml", _formDEquipmentModel);
        }

        [HttpPost]
        public async Task<IActionResult> LoadFormDEquipmentList(DataTableAjaxPostModel<FormDSearchGridDTO> formDFilter, string id)
        {

            FilteredPagingDefinition<FormDSearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormDSearchGridDTO>();
            filteredPagingDefinition.RecordsPerPage = formDFilter.length;
            filteredPagingDefinition.StartPageNo = formDFilter.start;

            var result = await _formDService.GetEquipmentFormDGrid(filteredPagingDefinition, id).ConfigureAwait(false);

            return Json(new { draw = formDFilter.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }

        [HttpPost]
        public async Task<IActionResult> LoadFormDMaterialList(DataTableAjaxPostModel<FormDSearchGridDTO> formDFilter, string id)
        {

            FilteredPagingDefinition<FormDSearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormDSearchGridDTO>();
            filteredPagingDefinition.RecordsPerPage = formDFilter.length;
            filteredPagingDefinition.StartPageNo = formDFilter.start;

            var result = await _formDService.GetMaterialFormDGrid(filteredPagingDefinition, id).ConfigureAwait(false);

            return Json(new { draw = formDFilter.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }

        [HttpPost]
        public async Task<IActionResult> LoadFormDLabourList(DataTableAjaxPostModel<FormDSearchGridDTO> formDFilter, string id)
        {

            FilteredPagingDefinition<FormDSearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormDSearchGridDTO>();
            filteredPagingDefinition.RecordsPerPage = formDFilter.length;
            filteredPagingDefinition.StartPageNo = formDFilter.start;

            var result = await _formDService.GetLabourFormDGrid(filteredPagingDefinition, id).ConfigureAwait(false);

            return Json(new { draw = formDFilter.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }

        [HttpPost]
        public async Task<IActionResult> LoadFormDDetailsList(DataTableAjaxPostModel<FormDSearchGridDTO> formDFilter, string id)
        {

            FilteredPagingDefinition<FormDSearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormDSearchGridDTO>();
            filteredPagingDefinition.RecordsPerPage = formDFilter.length;
            filteredPagingDefinition.StartPageNo = formDFilter.start;

            var result = await _formDService.GetFormDDetailsGrid(filteredPagingDefinition, id).ConfigureAwait(false);

            return Json(new { draw = formDFilter.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }

        [HttpPost]
        public async Task<IActionResult> FormDCheckandSaveHdr(FormDHeaderRequestDTO saveObj)
        {
            FormDHeaderRequestDTO saveRequestObj = new FormDHeaderRequestDTO();
            saveRequestObj = saveObj;
            FormDHeaderResponseDTO formDHeaderResponseDTO = new FormDHeaderResponseDTO();

            formDHeaderResponseDTO = await _formDService.SaveHeaderwithResponse(saveRequestObj);

            return Json(formDHeaderResponseDTO);

        }

        [HttpPost]
        public async Task<JsonResult> FindDetails(FormDModel header)
        {
            FormDHeaderRequestDTO formD = new FormDHeaderRequestDTO();
            FormDHeaderRequestDTO formDRes = new FormDHeaderRequestDTO();

            formD = header.SaveFormDModel;
            formD.DivisionName = header.DivisionName;
            formD.RoadCode = header.RoadCode;
            formDRes = await _formDService.FindDetails(formD);
            if (formDRes == null || formDRes.No == 0)
            {

                formD.CreatedBy = _security.UserID.ToString();
                formD.ModifeidBy = _security.UserID.ToString();
                formD.ModifiedDate = DateTime.Now;
                formD.CreatedDate = DateTime.Now;

                formDRes = await _formDService.FindAndSaveFormDHdr(formD, false);
            }
            header.SaveFormDModel = formDRes;
            return Json(formDRes, JsonOption());
        }

        [HttpPost]
        public async Task<IActionResult> FormDSaveHdr(FormDHeaderRequestDTO saveObj)
        {
            int rowsAffected = 0;

            int refNo = 0;
            FormDHeaderRequestDTO saveRequestObj = new FormDHeaderRequestDTO();
            saveRequestObj = saveObj;
            if (saveObj.No == 0 || saveObj.No == null)
            {
                refNo = await _formDService.SaveFormDAsync(saveRequestObj);
            }
            else
            {
                rowsAffected = await _formDService.UpdateFormDAsync(saveRequestObj);
                refNo = int.Parse(saveObj.No.ToString());
            }

            return Json(refNo);
        }

        [HttpPost]
        public async Task<IActionResult> FormDSaveLabour(FormDLabourDetailsRequestDTO saveObj)
        {
            int rowsAffected = 0;
            int refNo = 0;
            FormDLabourDetailsRequestDTO saveRequestObj = new FormDLabourDetailsRequestDTO();
            saveRequestObj = saveObj;
            if (saveObj.No == 0 || saveObj.No == null)
            {
                var SrNo = await _formDService.GetLabourSrNo(saveObj.FdmdFdhPkRefNo);
                saveRequestObj.SerialNo = ((SrNo == null) ? 0 : SrNo) + 1;
                refNo = await _formDService.SaveFormDLabourAsync(saveRequestObj);
            }
            else
            {
                rowsAffected = await _formDService.UpdateFormDLabourAsync(saveRequestObj);
                refNo = int.Parse(saveObj.No.ToString());
            }

            return Json(refNo);
        }

        [HttpPost]
        public async Task<IActionResult> FormDSaveMaterial(FormDMaterialDetailsRequestDTO saveObj)
        {
            int rowsAffected = 0;
            int refNo = 0;
            FormDMaterialDetailsRequestDTO saveRequestObj = new FormDMaterialDetailsRequestDTO();
            saveRequestObj = saveObj;
            if (saveObj.No == 0 || saveObj.No == null)
            {
                var SrNo = await _formDService.GetMaterialSRNO(saveObj.FdmdFdhPkRefNo);
                saveRequestObj.SerialNo = ((SrNo == null) ? 0 : SrNo) + 1;
                refNo = await _formDService.SaveFormDMaterialAsync(saveRequestObj);
            }
            else
            {
                rowsAffected = await _formDService.UpdateFormDMaterialAsync(saveRequestObj);
                refNo = int.Parse(saveObj.No.ToString());
            }

            return Json(refNo);
        }

        [HttpPost]
        public async Task<IActionResult> FormDSaveEquipment(FormDEquipRequestDTO saveObj)
        {
            int rowsAffected = 0;
            int refNo = 0;
            FormDEquipRequestDTO saveRequestObj = new FormDEquipRequestDTO();
            saveRequestObj = saveObj;
            if (saveObj.No == 0 || saveObj.No == null)
            {
                var SrNo = await _formDService.GetEqpSRNO(saveObj.FormDEDFHeaderNo);
                saveRequestObj.SerialNo = ((SrNo == null) ? 0 : SrNo) + 1;
                refNo = await _formDService.SaveFormDEquipmentAsync(saveRequestObj);
            }
            else
            {
                rowsAffected = await _formDService.UpdateFormDEquipmentAsync(saveRequestObj);
                refNo = int.Parse(saveObj.No.ToString());
            }
            return Json(refNo);
        }

        [HttpPost]
        public async Task<IActionResult> FormDSaveDetails(FormDDetailsRequestDTO saveObj)
        {
            int rowsAffected = 0;
            bool refIdStatus = false;
            int refNo = 0;
            FormDDetailsRequestDTO saveRequestObj = new FormDDetailsRequestDTO();
            saveRequestObj = saveObj;
            if (saveObj.No == 0 || saveObj.No == null)
            {
                refIdStatus = await _formDService.CheckDetailsRefereceId(saveObj.ReferenceID);
                if (!refIdStatus)
                {
                    var SrNo = await _formDService.GetDetailSRNO(saveObj.FormDHeaderNo);
                    saveRequestObj.SrNo = ((SrNo == null) ? 0 : SrNo) + 1;
                    saveRequestObj.Unit = "m";
                    refNo = await _formDService.SaveFormDDetailAsync(saveRequestObj);
                }

                else
                {
                    refNo = -1;
                }
            }
            else
            {
                saveRequestObj.Unit = "m";
                rowsAffected = await _formDService.UpdateFormDDetailAsync(saveRequestObj);
                refNo = int.Parse(saveObj.No.ToString());
            }

            return Json(refNo);
        }

        public async Task<IActionResult> PrintWarForm(int id, string form, string filename)
        {
            try
            {
                int i = 11; //RowIndex
                int j = 2; //ColumnIndex

                MemoryStream stream = new MemoryStream();

                #region Get File and renaming
                string templatePath = _webHostEnvironment.WebRootPath + _configuration.GetValue<string>("FormTemplateLocation");
                string Oldfilename = templatePath + filename + ".xlsx";
                string _filename = filename + DateTime.Now.ToString("yyyyMMddHHmmssfffffff").ToString();
                string cachefile = templatePath + _filename + ".xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var imageDetails = new List<WarImageDtlResponseDTO>();
                #endregion Get File and renaming

                //Copying File
                System.IO.File.Copy(Oldfilename, cachefile, true);
                using (var xlWorkbook = new XLWorkbook(cachefile))
                {
                    IXLWorksheet ixlWorksheet;
                    xlWorkbook.Worksheets.TryGetWorksheet("Sheet1", out ixlWorksheet);
                    if (form == "FormD")
                    {
                        imageDetails = await _formDService.GetWarImageList(id);
                    }
                    else if (form == "FormX")
                    {
                        imageDetails = await _formXService.GetWarImageList(id);
                    }
                    int tempVar = 0;

                    if (imageDetails.Count <= 3)
                    {
                        tempVar = 11;
                        for (int insideborder = 11; insideborder <= (((imageDetails.Count / 2) + (imageDetails.Count % 2)) * 11) + (imageDetails.Count / 2) + 10; insideborder++)
                        {
                            IXLRange insiderange = ixlWorksheet.Range(ixlWorksheet.Cell(insideborder, 2), ixlWorksheet.Cell(insideborder, 2));
                            insiderange.Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                            insiderange = ixlWorksheet.Range(ixlWorksheet.Cell(insideborder, 5), ixlWorksheet.Cell(insideborder, 5));
                            insiderange.Style.Border.LeftBorder = XLBorderStyleValues.Medium;

                            if (((imageDetails.Count / 2) * 11) + 11 + (imageDetails.Count / 2) - 1 > insideborder)
                            {
                                insiderange = ixlWorksheet.Range(ixlWorksheet.Cell(insideborder, 8), ixlWorksheet.Cell(insideborder, 8));
                                insiderange.Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                            }
                        }
                    }
                    else
                    {
                        for (var y = 1; y < imageDetails.Count / 2; y++)
                        {
                            tempVar = 11;
                            for (int insideborder = 11; insideborder <= (((imageDetails.Count / 2) + (imageDetails.Count % 2)) * 11) + (imageDetails.Count / 2) + 10; insideborder++)
                            {
                                IXLRange insiderange = ixlWorksheet.Range(ixlWorksheet.Cell(insideborder, 2), ixlWorksheet.Cell(insideborder, 2));
                                insiderange.Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                                insiderange = ixlWorksheet.Range(ixlWorksheet.Cell(insideborder, 5), ixlWorksheet.Cell(insideborder, 5));
                                insiderange.Style.Border.LeftBorder = XLBorderStyleValues.Medium;

                                if (((imageDetails.Count / 2) * 11) + 11 + (imageDetails.Count / 2) - 1 > insideborder)
                                {
                                    insiderange = ixlWorksheet.Range(ixlWorksheet.Cell(insideborder, 8), ixlWorksheet.Cell(insideborder, 8));
                                    insiderange.Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                                }

                            }
                        }
                    }

                    var imgTypes = imageDetails.Select(x => x.ImageTypeCode).Distinct();
                    if (imgTypes.Count() > 0)
                    {
                        int iImgCount = -1;
                        foreach (string strType in imgTypes)
                        {
                            var simageDetails = imageDetails.Where(x => x.ImageTypeCode == strType).ToList();
                            for (var x = 0; x < simageDetails.Count; x++)
                            {
                                iImgCount++;
                                var imageType = simageDetails[x].ImageUserFilename.ToString().ToLower().Split('.');
                                var imagePath = "";
                                if (imageType[1] == "jpg" || imageType[1] == "png" || imageType[1] == "jpeg")
                                {
                                    if (form == "FormD")
                                    {
                                        imagePath = _webHostEnvironment.WebRootPath + "\\"/* + "Uploads" + "\\" + "FormD" + "\\"*/ + simageDetails[x].ImageFilenameUpload;
                                    }
                                    else if (form == "FormX")
                                    {
                                        imagePath = _webHostEnvironment.WebRootPath + "\\" /*+ "Uploads" + "\\" + "FormX" + "\\"*/ + simageDetails[x].ImageFilenameUpload;
                                    }
                                    var imagedAdded = ixlWorksheet.AddPicture(imagePath, simageDetails[x].ImageTypeCode + x);

                                    if ((iImgCount + 1) % 2 == 0)
                                    {

                                        imagedAdded.MoveTo(ixlWorksheet.Cell(i, j + 3), i, j + 3).WithSize(350, 350).Scale(0.5);
                                        ixlWorksheet.Cell(i + 11, j + 3).Value = simageDetails[x].ImageTypeCode;
                                        IXLRange insiderange = ixlWorksheet.Range(ixlWorksheet.Cell(i + 11, j), ixlWorksheet.Cell(i + 11, j + 5));
                                        insiderange.Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                                        i += 12;
                                    }
                                    else
                                    {

                                        imagedAdded.MoveTo(ixlWorksheet.Cell(i, j), i, j).WithSize(350, 350).Scale(0.5);
                                        ixlWorksheet.Cell(i + 11, j).Value = simageDetails[x].ImageTypeCode;
                                        IXLRange insiderange = ixlWorksheet.Range(ixlWorksheet.Cell(i + 11, j), ixlWorksheet.Cell(i + 11, j + 2));
                                        insiderange.Style.Border.OutsideBorder = XLBorderStyleValues.Medium;

                                    }
                                }
                            }
                        }
                    }


                    xlWorkbook.SaveAs(cachefile);

                    _bridgeBO.bridgeObj = _bridgeBO.GetBridgeGridBO();
                    var RmAllassetInventoryDTO = _bridgeBO.bridgeObj.ToList();
                    string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string filepath = cachefile;
                    Byte[] content1;
                    if (form == "FormX")
                    {
                        content1 = _bridgeBO.formdownload("war", id, filepath);
                    }
                    else
                    {
                        content1 = _bridgeBO.formdownload("warD", id, filepath);
                    }
                    return File(content1, contentType1, "war" + ".xlsx");

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetRoadCodeByRMU(string rmu)
        {
            var obj = await _formDService.GetRoadCodesByRMU(rmu);
            return Json(obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetSectionCodeByRMU(string rmu)
        {
            var obj = await _formDService.GetSectionCodesByRMU(rmu);
            return Json(obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetUserById(int id) => Json(await _userService.GetUserNameByCode(new UserRequestDTO { UserId = id }));

        [HttpPost]
        public async Task<IActionResult> GetReferenceId(string form, string roadCode)
        {
            if (form == "FormX")
            {
                var obj = await _formDService.GetFormXReferenceId(roadCode);
                return Json(obj);
            }

            return Json(new StringContent("{}"));
        }

        // FormD War Image Upload for Tab
        [HttpPost]
        public async Task<int> ImageUploadedWarFormDTab(IFormCollection filesCollection, string Id, string photoType)
        {
            try
            {
                int i = await _formDService.LastInsertedWARSRNO(int.Parse(Id), photoType);
                i++;
                string id = Regex.Replace(Id, @"[^0-9a-zA-Z]+", "");
                string photo_Type = Regex.Replace(photoType, @"[^a-zA-Z]", "");
                string wwwPath = this._webHostEnvironment.WebRootPath;
                string subPath = Path.Combine(@"Uploads/FormD/", id, photo_Type);

                string path = Path.Combine(this._webHostEnvironment.WebRootPath, @"Uploads\FormD\", id, photoType);
                IFormCollection files = Request.ReadFormAsync().Result;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                List<WarImageDtlRequestDTO> uploadedFiles = new List<WarImageDtlRequestDTO>();
                foreach (var file in files.Files)
                {
                    WarImageDtlRequestDTO _rmAssetImageDtl = new WarImageDtlRequestDTO();
                    string fileName = Path.GetFileName(file.FileName);
                    string fileRename = i + "_" + photoType + "_" + fileName;
                    using (var stream = new FileStream(Path.Combine(path, fileRename), FileMode.Create))
                    {
                        _rmAssetImageDtl.HeaderId = int.Parse(Id);
                        _rmAssetImageDtl.ImageTypeCode = photoType;
                        _rmAssetImageDtl.ImageUserFilename = file.FileName;
                        _rmAssetImageDtl.ImageSrno = i;

                        _rmAssetImageDtl.ActiveYn = true;
                        if (i < 10)
                        {
                            _rmAssetImageDtl.ImageFilenameSys = id + "_" + photoType + "_" + "00" + i;
                        }
                        else if (i >= 10 && i < 100)
                        {
                            _rmAssetImageDtl.ImageFilenameSys = id + "_" + photoType + "_" + "0" + i;
                        }
                        else
                        {
                            _rmAssetImageDtl.ImageFilenameSys = id + "_" + photoType + "_" + i;
                        }
                        _rmAssetImageDtl.ImageFilenameUpload = $"{subPath}/{fileRename}";


                        await file.CopyToAsync(stream);


                    }
                    uploadedFiles.Add(_rmAssetImageDtl);
                    i = i + 1;
                }
                int rowsAffected = 0;
                if (uploadedFiles.Count() > 0)
                {
                    rowsAffected = await _formDService.SaveFormDWarImage(uploadedFiles);
                }

                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        //FormD UCU upload Tab 
        [HttpPost]
        public async Task<int> ImageUploadedUCUFormDTab(IFormCollection fileCollection, string id, string photoType)
        {
            try
            {
                int i = await _formDService.LastInsertedUCUSRNO(int.Parse(id), photoType);
                i++;


                string contentPath = this._webHostEnvironment.ContentRootPath;
                string _id = id;
                IFormCollection files = Request.ReadFormAsync().Result;
                string photo_Type = Regex.Replace(photoType, @"[^a-zA-Z]", "");
                string wwwPath = this._webHostEnvironment.WebRootPath;
                string subPath = Path.Combine(@"Uploads/FormD/", _id, photo_Type);
                string path = Path.Combine(wwwPath, Path.Combine(@"Uploads\FormD\", _id, photoType));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                List<AccUccImageDtlRequestDTO> uploadedFiles = new List<AccUccImageDtlRequestDTO>();
                foreach (var file in files.Files)
                {
                    AccUccImageDtlRequestDTO _rmAssetImageDtl = new AccUccImageDtlRequestDTO();
                    string fileName = Path.GetFileName(file.FileName);
                    string fileRename = i + "_" + photoType + "_" + fileName;
                    using (var stream = new FileStream(Path.Combine(path, fileRename), FileMode.Create))
                    {
                        _rmAssetImageDtl.HeaderId = int.Parse(id);
                        _rmAssetImageDtl.AccUcu = photoType;
                        _rmAssetImageDtl.ImageUserFilename = file.FileName;
                        _rmAssetImageDtl.ImageSrno = i;
                        _rmAssetImageDtl.ImageFilenameSys = file.FileName;
                        _rmAssetImageDtl.ActiveYn = true;
                        if (i < 10)
                        {
                            _rmAssetImageDtl.ImageFilenameSys = _id + "_" + photoType + "_" + "00" + i;
                        }
                        else if (i >= 10 && i < 100)
                        {
                            _rmAssetImageDtl.ImageFilenameSys = _id + "_" + photoType + "_" + "0" + i;
                        }
                        else
                        {
                            _rmAssetImageDtl.ImageFilenameSys = _id + "_" + photoType + "_" + i;
                        }
                        _rmAssetImageDtl.ImageFilenameUpload = $"{subPath}/{fileRename}";


                        await file.CopyToAsync(stream);
                    }
                    uploadedFiles.Add(_rmAssetImageDtl);
                    i = i + 1;
                }
                int rowsAffected = 0;
                if (uploadedFiles.Count() > 0)
                {
                    rowsAffected = await _formDService.SaveFormDAccUccDtl(uploadedFiles);
                }

                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}