using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.ResponseBO;
using RAMMS.Web.UI.Models;

namespace RAMMS.Web.UI.Controllers
{
    public class FormC1C2Controller : Models.BaseController
    {
        private readonly IAssetsService _AssetService;
        private readonly IFormC1C2Service _C1C2Service;
        private readonly ISecurity _security;
        private readonly IWebHostEnvironment _webhostenvironment;
        public FormC1C2Controller(IAssetsService assestService, IFormC1C2Service C1C2Service, ISecurity security, IWebHostEnvironment webhostenvironment)
        {
            _AssetService = assestService;
            _C1C2Service = C1C2Service;
            _security = security;
            _webhostenvironment = webhostenvironment;
        }
        public IActionResult Index()
        {
            LoadLookupService("Asset Type~CV^Value", "Year", "RMU", "Section Code", "RD_Code", "Bound~CV");
            var grid = new Models.CDataTable() { Name = "tblFC1C2HGrid", APIURL = "/FormC1C2/HeaderList", LeftFixedColumn = 1 };
            grid.IsModify = _security.IsPCModify(ModuleNameList.Condition_Inspection);
            grid.IsDelete = _security.IsPCDelete(ModuleNameList.Condition_Inspection);
            grid.IsView = _security.IsPCView(ModuleNameList.Condition_Inspection);
            grid.Columns.Add(new CDataColumns() { data = null, title = "Action", IsFreeze = true, sortable = false, render = "frmC1C2.HeaderGrid.ActionRender" });
            grid.Columns.Add(new CDataColumns() { data = "RefID", title = "Reference No" });
            grid.Columns.Add(new CDataColumns() { data = "Year", title = "Year of Inspection" });
            grid.Columns.Add(new CDataColumns() { data = "InsDate", title = "Date of Inspection", render = "frmC1C2.HeaderGrid.DateOfIns" });
            grid.Columns.Add(new CDataColumns() { data = "AssetRefId", title = "Asset ID" });
            grid.Columns.Add(new CDataColumns() { data = "RMUCode", title = "RMU" });
            grid.Columns.Add(new CDataColumns() { data = "RMUDesc", title = "RMU Name" });
            grid.Columns.Add(new CDataColumns() { data = "SecCode", title = "Section Code" });
            grid.Columns.Add(new CDataColumns() { data = "SecName", title = "Section Name" });
            grid.Columns.Add(new CDataColumns() { data = "RoadCode", title = "Road Code" });
            grid.Columns.Add(new CDataColumns() { data = "RoadName", title = "Road Name" });
            grid.Columns.Add(new CDataColumns() { data = "LocationCH", title = "Location CH" });
            grid.Columns.Add(new CDataColumns() { data = "CDia", title = "Culvert Diameter" });
            grid.Columns.Add(new CDataColumns() { data = "CULWidth", title = "Culvert Width" });
            grid.Columns.Add(new CDataColumns() { data = "InspectedBy", title = "Inspected By" });
            grid.Columns.Add(new CDataColumns() { data = "AuditedBy", title = "Audited By" });
            grid.Columns.Add(new CDataColumns() { data = "RoadId", title = "Road Id", visible = false });
            return View(grid);
        }
        public async Task<JsonResult> HeaderList(DataTableAjaxPostModel searchData)
        {
            if (searchData.order != null && searchData.order.Count > 0)
            {
                searchData.order = searchData.order.Select(x => { if (x.column == 4 || x.column == 1 || x.column == 9) { x.column = 16; } return x; }).ToList();
            }
            return Json(await _C1C2Service.GetHeaderGrid(searchData), JsonOption());
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
            LoadLookupService("Year", "User", "Photo Type~CV");
            ViewBag.Dis_Severity = LookupService.GetDdlLookupByCode("Form C1C2");
            FormC1C2DTO frmC1C2 = null;
            if (id > 0)
            {
                ViewBag.IsAdd = false;

                frmC1C2 = _C1C2Service.FindByHeaderID(id).Result;
            }
            else
            {
                LoadLookupService("RMU", "Section Code", "Division", "RD_Code");
                ViewData["AssetIds"] = _AssetService.ListOfCulvertAssestIds().Result;
            }

            return View("~/Views/FormC1C2/_AddFormC1C2.cshtml", frmC1C2);
        }
        public async Task<IActionResult> FindDetails(DTO.ResponseBO.FormC1C2DTO frmC1C2)
        {
            return Json(await _C1C2Service.FindDetails(frmC1C2, _security.UserID), JsonOption());
        }
        [HttpPost]
        public async Task<JsonResult> Save(DTO.ResponseBO.FormC1C2DTO frmC1C2)
        {
            return await SaveAll(frmC1C2, false);
        }
        [HttpPost]
        public async Task<JsonResult> Submit(DTO.ResponseBO.FormC1C2DTO frmC1C2)
        {
            frmC1C2.SubmitSts = true;
            return await SaveAll(frmC1C2, true);
        }
        private async Task<JsonResult> SaveAll(DTO.ResponseBO.FormC1C2DTO frmC1C2, bool updateSubmit)
        {
            frmC1C2.CrBy = _security.UserID;
            frmC1C2.ModBy = _security.UserID;
            frmC1C2.ModDt = DateTime.UtcNow;
            frmC1C2.CrDt = DateTime.UtcNow;
            var result = await _C1C2Service.Save(frmC1C2, updateSubmit);
            return Json(new { RefNo = result.CInspRefNo, Id = result.PkRefNo }, JsonOption());
        }
        [HttpPost]
        //[DisableRequestSizeLimit]
        public async Task<IActionResult> ImageUploaded(IList<IFormFile> FormFile, int headerId, string InspRefNum, List<string> PhotoType)
        {
            if (FormFile != null && FormFile.Count > 0)
            {
                List<FormC1C2ImageDTO> lstImages = new List<FormC1C2ImageDTO>();
                var objExistsPhotoType = _C1C2Service.GetExitingPhotoType(headerId).Result;
                if (objExistsPhotoType == null) { objExistsPhotoType = new List<FormC1C2PhotoTypeDTO>(); }
                InspRefNum = Regex.Replace(InspRefNum, @"[^0-9a-zA-Z]+", "");
                string wwwPath = this._webhostenvironment.WebRootPath;
                for (int j = 0; j < FormFile.Count; j++)
                {
                    var objSNo = objExistsPhotoType.Where(x => x.Type == PhotoType[j]).FirstOrDefault();
                    if (objSNo == null) { objSNo = new FormC1C2PhotoTypeDTO() { SNO = 1, Type = PhotoType[j] }; objExistsPhotoType.Add(objSNo); }
                    else { objSNo.SNO = objSNo.SNO + 1; }
                    IFormFile postedFile = FormFile[j];
                    string photoType = Regex.Replace(PhotoType[j], @"[^a-zA-Z]", "");
                    string strFileUploadDir = Path.Combine("Form C1C2", InspRefNum, photoType);
                    string strSaveDir = Path.Combine(wwwPath, "Uploads", strFileUploadDir);
                    string strSysFileName = InspRefNum + "_" + photoType + "_" + objSNo.SNO.ToString("000");
                    string strUploadFileName = objSNo.SNO.ToString() + "_" + photoType + "_" + postedFile.FileName;
                    if (!Directory.Exists(strSaveDir)) { Directory.CreateDirectory(strSaveDir); }
                    using (FileStream stream = new FileStream(Path.Combine(strSaveDir, strUploadFileName), FileMode.Create))
                    {
                        await postedFile.CopyToAsync(stream);
                    }
                    lstImages.Add(new FormC1C2ImageDTO()
                    {
                        ActiveYn = true,
                        CrBy = _security.UserID,
                        ModBy = _security.UserID,
                        CrDt = DateTime.UtcNow,
                        ModDt = DateTime.UtcNow,
                        hPkRefNo = headerId,
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
                    var a = await _C1C2Service.AddMultiImage(lstImages);
                    return Json(a.Item2);
                }
            }
            return Json(new { Message = "Sucess" });
        }

        public IActionResult ImageList(int headerId)
        {
            return Json(_C1C2Service.ImageList(headerId), JsonOption());
        }
        public async Task<IActionResult> DeleteImage(int headerId, int imgId)
        {
            await _C1C2Service.DeleteImage(headerId, imgId);
            return ImageList(headerId);
            //return Json(new { Message = "Sucess" }, JsonOption());
        }
        [HttpPost] //Tab
        public IActionResult Delete(int id)
        {
            if (id > 0) { return Ok(new { id = _C1C2Service.Delete(id) }); }
            else { return BadRequest("Invalid Request!"); }

        }

        public IActionResult Download(int id)
        {
            var content1 = _C1C2Service.FormDownload("FormC1C2", id, _webhostenvironment.WebRootPath, _webhostenvironment.WebRootPath + "/Templates/FormC1C2.xlsx");
            string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(content1, contentType1, "FormC1C2" + ".xlsx");
        }


        [HttpPost] ///Tab
        public async Task<int> ImageC1C2Tab(IFormCollection FormFile, int headerId, string InspRefNum, string PhotoType)
        {
            IFormCollection files = Request.ReadFormAsync().Result;
            if (files != null && files.Count > 0)
            {
                List<FormC1C2ImageDTO> lstImages = new List<FormC1C2ImageDTO>();
                var objExistsPhotoType = _C1C2Service.GetExitingPhotoType(headerId).Result;
                if (objExistsPhotoType == null) { objExistsPhotoType = new List<FormC1C2PhotoTypeDTO>(); }
                InspRefNum = Regex.Replace(InspRefNum, @"[^0-9a-zA-Z]+", "");
                string wwwPath = this._webhostenvironment.WebRootPath;

                foreach (var file in files.Files)
                {
                    var objSNo = objExistsPhotoType.Where(x => x.Type == PhotoType).FirstOrDefault();
                    if (objSNo == null) { objSNo = new FormC1C2PhotoTypeDTO() { SNO = 1, Type = PhotoType }; objExistsPhotoType.Add(objSNo); }
                    else { objSNo.SNO = objSNo.SNO + 1; }
                    IFormFile postedFile = file;
                    string photoType = Regex.Replace(PhotoType, @"[^a-zA-Z]", "");
                    string strFileUploadDir = Path.Combine("Form C1C2", InspRefNum, photoType);
                    string strSaveDir = Path.Combine(wwwPath, "Uploads", strFileUploadDir);
                    string strSysFileName = InspRefNum + "_" + photoType + "_" + objSNo.SNO.ToString("000");
                    string strUploadFileName = objSNo.SNO.ToString() + "_" + photoType + "_" + postedFile.FileName;
                    if (!Directory.Exists(strSaveDir)) { Directory.CreateDirectory(strSaveDir); }
                    using (FileStream stream = new FileStream(Path.Combine(strSaveDir, strUploadFileName), FileMode.Create))
                    {
                        await postedFile.CopyToAsync(stream);
                    }
                    lstImages.Add(new FormC1C2ImageDTO()
                    {
                        ActiveYn = true,
                        CrBy = _security.UserID,
                        ModBy = _security.UserID,
                        CrDt = DateTime.UtcNow,
                        ModDt = DateTime.UtcNow,
                        hPkRefNo = headerId,
                        ImageFilenameSys = strSysFileName,
                        ImageFilenameUpload = strUploadFileName,
                        ImageSrno = objSNo.SNO,
                        ImageTypeCode = PhotoType,
                        ImageUserFilePath = strFileUploadDir,
                        SubmitSts = true
                    });

                }
                if (lstImages.Count > 0)
                {
                    await _C1C2Service.AddMultiImage(lstImages);
                }
            }
            else
            {
                return -1;
            }
            return 1;
        }
    }
}