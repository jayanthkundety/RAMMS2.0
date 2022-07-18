using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.Web.UI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO;
using RAMMS.DTO.Wrappers;
using System.Collections.Generic;
using RAMMS.Business.ServiceProvider;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using System.IO;

namespace RAMMS.Web.UI.Controllers
{
    public class FormR1R2Controller : Models.BaseController
    {
        private IHostingEnvironment Environment;
        private readonly ILogger _logger;
        private readonly IDDLookUpService _ddLookupService;
        private readonly ISecurity _security;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFormN1Service _formN1Service;
        private readonly IFormJServices _formJService;
        private readonly IRoadMasterService _roadMasterService;
        private readonly IDDLookupBO _dDLookupBO;
        private readonly IFormR1R2Service _formR1R2Service;
        private readonly IAssetsService _AssetService;

        public FormR1R2Controller(IHostingEnvironment _environment,
           IDDLookupBO _ddLookupBO,
           IDDLookUpService ddLookupService,
           IUserService userService,
           IWebHostEnvironment webhostenvironment,
           IFormN1Service formN1Service,
           IFormJServices formJServices,
           ISecurity security,
           IRoadMasterService roadMasterService,
           ILogger logger,
           IFormR1R2Service formR1R2Service,
           IAssetsService assestService)
        {
            _userService = userService;
            _dDLookupBO = _ddLookupBO;
            Environment = _environment;
            _webHostEnvironment = webhostenvironment;
            _ddLookupService = ddLookupService;
            _security = security;
            _formN1Service = formN1Service ?? throw new ArgumentNullException(nameof(formN1Service));
            _formJService = formJServices ?? throw new ArgumentNullException(nameof(formJServices));
            _roadMasterService = roadMasterService ?? throw new ArgumentNullException(nameof(roadMasterService));
            _logger = logger;
            _formR1R2Service = formR1R2Service ?? throw new ArgumentNullException(nameof(formR1R2Service));
            _AssetService = assestService;
        }

        private async Task LoadDropDown()
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();

            ViewData["RD_Code"] = await _formN1Service.GetRoadCodesByRMU("");
            //ViewData["RMU"] = await _formN1Service.GetRMU();

            ddLookup.Type = "ACT-QA1";
            ViewData["Activity"] = await _ddLookupService.GetLookUpCodeTextConcat(ddLookup);

            LoadLookupService("User", "RMU", "Year", "Section Code");

            ddLookup.Type = "Week No";
            ViewData["WeekNo"] = await _ddLookupService.GetDdDescValue(ddLookup);

            //FormASearchDropdown ddl = _formJService.GetDropdown(new RequestDropdownFormA { });
        }
        public async Task<IActionResult> Index()
        {
            await LoadDropDown();
            var grid = new Models.CDataTable() { Name = "tblFR1R2HGrid", APIURL = "/FormR1R2/HeaderList", LeftFixedColumn = 1 };
            grid.IsModify = _security.IsPCModify(ModuleNameList.Condition_Inspection);
            grid.IsDelete = _security.IsPCDelete(ModuleNameList.Condition_Inspection);
            grid.IsView = _security.IsPCView(ModuleNameList.Condition_Inspection);
            grid.Columns.Add(new CDataColumns() { data = null, title = "Action", IsFreeze = true, sortable = false, render = "frmR1R2.HeaderGrid.ActionRender" });
            grid.Columns.Add(new CDataColumns() { data = "AssetRefId", title = "Asset ID" });
            grid.Columns.Add(new CDataColumns() { data = "RefID", title = "Reference No" });
            grid.Columns.Add(new CDataColumns() { data = "Year", title = "Year of Inspection" });
            grid.Columns.Add(new CDataColumns() { data = "InsDate", title = "Date of Inspection", render = "frmR1R2.HeaderGrid.DateOfIns" });            
            grid.Columns.Add(new CDataColumns() { data = "RMUCode", title = "RMU" });
            grid.Columns.Add(new CDataColumns() { data = "RMUDesc", title = "RMU Name" });
            grid.Columns.Add(new CDataColumns() { data = "SecCode", title = "Section Code" });
            grid.Columns.Add(new CDataColumns() { data = "SecName", title = "Section Name" });
            grid.Columns.Add(new CDataColumns() { data = "RoadCode", title = "Road Code" });
            grid.Columns.Add(new CDataColumns() { data = "RoadName", title = "Road Name" });
            grid.Columns.Add(new CDataColumns() { data = "LocationCH", title = "Location CH" });
            grid.Columns.Add(new CDataColumns() { data = "InspectedBy", title = "Inspected By" });
            grid.Columns.Add(new CDataColumns() { data = "AuditedBy", title = "Audited By" });
            grid.Columns.Add(new CDataColumns() { data = "RoadId", title = "Road Id", visible = false });
            grid.Columns.Add(new CDataColumns() { data = "ProcessStatus", title = "Status" });
            return View(grid);
        }

        public async Task<IActionResult> FindDetails(DTO.ResponseBO.FormR1DTO frmR1)
        {
            return Json(await _formR1R2Service.FindDetails(frmR1, _security.UserID), JsonOption());
        }

        public async Task<JsonResult> HeaderList(DataTableAjaxPostModel searchData)
        {
            if (searchData.order != null && searchData.order.Count > 0)
            {
                searchData.order = searchData.order.Select(x => { if (x.column == 4 || x.column == 1 || x.column == 9) { x.column = 16; } return x; }).ToList();
            }
            return Json(await _formR1R2Service.GetHeaderGrid(searchData), JsonOption());
        }

        public IActionResult Add()
        {
            ViewBag.IsAdd = true;
            ViewBag.IsEdit = true;
            return ViewRequest(0);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.IsEdit = true;
            return id > 0 ? ViewRequest(id) : RedirectToAction("404", "Error");
        }
        public IActionResult View(int id)
        {
            ViewBag.IsEdit = false;
            return id > 0 ? ViewRequest(id) : RedirectToAction("404", "Error");
        }
        private IActionResult ViewRequest(int id)
        {
            LoadLookupService("Year", "User", "Photo Type~RWG");
            ViewData["DistressObserved1"] = _ddLookupService.GetDdDistressDetails().Result;
            ViewBag.Dis_Severity = LookupService.GetDdlLookupByCode("Form C1C2");
            FormR1DTO frmR1R2 = new FormR1DTO();
            if (id > 0)
            {
                ViewBag.IsAdd = false;
                frmR1R2 = _formR1R2Service.FindByHeaderID(id).Result;

                if (frmR1R2.SubmitSts && frmR1R2.Status == Common.StatusList.FormR1R2Verified)
                {
                    frmR1R2.AuditedBy = _security.UserID;
                    frmR1R2.AuditedDt = DateTime.Today;
                }
            }
            else
            {
                LoadLookupService("RMU", "Section Code", "Division", "RD_Code");
                ViewData["AssetIds"] = _AssetService.ListOfReatiningWallAssestIds().Result;
            }

            return View("~/Views/FormR1R2/_AddFormR1R2.cshtml", frmR1R2);
        }

        [HttpPost]
        public async Task<JsonResult> Save(DTO.ResponseBO.FormR1DTO frmR1R2)
        {
            return await SaveAll(frmR1R2, false);
        }
        [HttpPost]
        public async Task<JsonResult> Submit(DTO.ResponseBO.FormR1DTO frmR1R2)
        {
            frmR1R2.SubmitSts = true;
            return await SaveAll(frmR1R2, true);
        }
        private async Task<JsonResult> SaveAll(DTO.ResponseBO.FormR1DTO frmR1R2, bool updateSubmit)
        {
            frmR1R2.CrBy = _security.UserID;
            frmR1R2.ModBy = _security.UserID;
            frmR1R2.ModDt = DateTime.UtcNow;
            frmR1R2.CrDt = DateTime.UtcNow;
            var result = await _formR1R2Service.Save(frmR1R2, updateSubmit);
            return Json(new { Id = result.PkRefNo }, JsonOption());
        }
        [HttpPost]
        //[DisableRequestSizeLimit]
        public async Task<IActionResult> ImageUploaded(IList<IFormFile> FormFile, int headerId, string InspRefNum, List<string> PhotoType)
        {
            if (FormFile != null && FormFile.Count > 0)
            {
                List<FormRImagesDTO> lstImages = new List<FormRImagesDTO>();
                var objExistsPhotoType = _formR1R2Service.GetExitingPhotoType(headerId).Result;
                if (objExistsPhotoType == null) { objExistsPhotoType = new List<FormR1R2PhotoTypeDTO>(); }
                InspRefNum = Regex.Replace(InspRefNum, @"[^0-9a-zA-Z]+", "");
                string wwwPath = this._webHostEnvironment.WebRootPath;
                for (int j = 0; j < FormFile.Count; j++)
                {
                    var objSNo = objExistsPhotoType.Where(x => x.Type == PhotoType[j]).FirstOrDefault();
                    if (objSNo == null) { objSNo = new FormR1R2PhotoTypeDTO() { SNO = 1, Type = PhotoType[j] }; objExistsPhotoType.Add(objSNo); }
                    else { objSNo.SNO = objSNo.SNO + 1; }
                    IFormFile postedFile = FormFile[j];
                    string photoType = Regex.Replace(PhotoType[j], @"[^a-zA-Z]", "");
                    string strFileUploadDir = Path.Combine("Form R1R2", InspRefNum, photoType);
                    string strSaveDir = Path.Combine(wwwPath, "Uploads", strFileUploadDir);
                    string strSysFileName = InspRefNum + "_" + photoType + "_" + objSNo.SNO.ToString("000");
                    string strUploadFileName = objSNo.SNO.ToString() + "_" + photoType + "_" + postedFile.FileName;
                    if (!Directory.Exists(strSaveDir)) { Directory.CreateDirectory(strSaveDir); }
                    using (FileStream stream = new FileStream(Path.Combine(strSaveDir, strUploadFileName), FileMode.Create))
                    {
                        await postedFile.CopyToAsync(stream);
                    }
                    lstImages.Add(new FormRImagesDTO()
                    {
                        ActiveYn = true,
                        CrBy = _security.UserID,
                        ModBy = _security.UserID,
                        CrDt = DateTime.UtcNow,
                        ModDt = DateTime.UtcNow,
                        FR1hPkRefNo = headerId,
                        ImageFilenameSys = strSysFileName,
                        ImageFilenameUpload = strUploadFileName,
                        ImageSrno = objSNo.SNO,
                        ImageTypeCode = PhotoType[j],
                        ImageUserFilePath = strFileUploadDir,
                        SubmitSts = true
                    });

                }
                if (lstImages.Count > 0)
                {
                    var a = await _formR1R2Service.AddMultiImage(lstImages);
                    return Json(a.Item2);
                }
            }
            return Json(new { Message = "Sucess" });
        }

        public IActionResult ImageList(int headerId)
        {
            return Json(_formR1R2Service.ImageList(headerId), JsonOption());
        }
        public async Task<IActionResult> DeleteImage(int headerId, int imgId)
        {
            await _formR1R2Service.DeleteImage(headerId, imgId);
            return ImageList(headerId);
            //return Json(new { Message = "Sucess" }, JsonOption());
        }
        [HttpPost] //Tab
        public IActionResult Delete(int id)
        {
            if (id > 0) { return Ok(new { id = _formR1R2Service.Delete(id) }); }
            else { return BadRequest("Invalid Request!"); }

        }

        public IActionResult Download(int id)
        {
            var content1 = _formR1R2Service.FormDownload("FormR1R2", id, _webHostEnvironment.WebRootPath, _webHostEnvironment.WebRootPath + "/Templates/FormR1R2.xlsx");
            string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(content1, contentType1, "FormR1R2" + ".xlsx");
        }
    }
}
