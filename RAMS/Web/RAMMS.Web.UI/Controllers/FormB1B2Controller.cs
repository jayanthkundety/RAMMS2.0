using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class FormB1B2Controller : Models.BaseController
    {
        private readonly IFormB1B2Service formB1B2Service;
        private readonly ISecurity security;
        private IWebHostEnvironment Environment;
        private readonly IDDLookUpService _ddLookupService;
        public FormB1B2Controller(IFormB1B2Service _formB1B2Service,
            IWebHostEnvironment _environment,
            ISecurity security,
            IDDLookUpService _ddLookupService)
        {
            this.formB1B2Service = _formB1B2Service;
            this.security = security;
            this.Environment = _environment;
            this._ddLookupService = _ddLookupService;

        }

        public IActionResult Index()
        {
            LoadLookupService("RMU", "Section Code", "Division", "RD_Code", "Year");
            return View();
        }

        public async Task<IActionResult> Add(int id, bool isview)
        {
            LoadLookupService("RMU", "Section Code", "Division", "RD_Code", "Year");
            RAMMS.DTO.RequestBO.FormB1B2HeaderRequestDTO model = null;
            if (id > 0)
            {
                model = await formB1B2Service.GetHeaderById(id);
            }
            model = model ?? new FormB1B2HeaderRequestDTO();
            model.Detail = model.Detail ?? new DTO.RequestBO.FormB1B2DetailRequestDTO();
            model.IsView = model.SubmitSts ? true : isview;
            return View("~/Views/FormB1B2/_AddFormB1B2.cshtml", model);
        }


        public IActionResult Download(int id)
        {
            var content1 = formB1B2Service.FormDownload("FormB1B2", id, Environment.WebRootPath, Environment.WebRootPath + "/Templates/FormB1B2.xlsx");
            string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(content1, contentType1, "FormB1B2" + ".xlsx");
        }

        public async Task<IActionResult> CheckExistence(FormB1B2HeaderRequestDTO model)
        {

            var exist = await formB1B2Service.AlreadyExists(model.AiPkRefNo.Value, Convert.ToInt32(model.YearOfInsp));
            if (!exist.IsExist && model.AiPkRefNo.HasValue)
            {
                var _model = await formB1B2Service.GetBrideDetailById(model.AiPkRefNo.Value);
                _model.DtOfInsp = model.DtOfInsp;
                _model.RecordNo = model.RecordNo;
                _model.YearOfInsp = model.YearOfInsp;
                _model.DisplayAssetId = model.DisplayAssetId;
                int _id = await formB1B2Service.SaveHeader(_model);
                return Ok(new { alreadyexist = exist.IsExist, id = _id });
            }
            else
            {
                return Ok(new { alreadyexist = exist.IsExist, id = exist.PkRefNo });
            }
        }

        public async Task<IActionResult> GetHeaderList(DataTableAjaxPostModel<FormB1B2SearchGridDTO> searchData)
        {
            int _id = 0;
            DateTime dt;
            FilteredPagingDefinition<FormB1B2SearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormB1B2SearchGridDTO>();
            searchData.filterData = searchData.filterData ?? new FormB1B2SearchGridDTO();
            if (Request.Form.ContainsKey("columns[0][search][value]"))
            {
                searchData.filterData.SmartSearch = Request.Form["columns[0][search][value]"].ToString();
            }

            if (Request.Form.ContainsKey("columns[1][search][value]"))
            {
                searchData.filterData.RmuCode = Request.Form["columns[1][search][value]"].ToString();
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
                searchData.filterData.AssetType = Request.Form["columns[6][search][value]"].ToString();
            }
            if (!string.IsNullOrEmpty(Request.Form["columns[7][search][value]"].ToString()))
            {
                searchData.filterData.locchFromKm = Convert.ToInt32(Request.Form["columns[7][search][value]"]);
            }
            if (!string.IsNullOrEmpty(Request.Form["columns[8][search][value]"].ToString()))
            {
                searchData.filterData.locchFromM = Request.Form["columns[8][search][value]"];
            }
            if (!string.IsNullOrEmpty(Request.Form["columns[9][search][value]"].ToString()))
            {
                searchData.filterData.locchToKm = Convert.ToInt32(Request.Form["columns[9][search][value]"]);
            }
            if (!string.IsNullOrEmpty(Request.Form["columns[10][search][value]"].ToString()))
            {
                searchData.filterData.locchToM = Request.Form["columns[10][search][value]"].ToString();
            }
            filteredPagingDefinition.Filters = searchData.filterData;
            if (searchData.order != null)
            {
                filteredPagingDefinition.ColumnIndex = searchData.order[0].column;
                filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }
            filteredPagingDefinition.RecordsPerPage = searchData.length; //Convert.ToInt32(Request.Form["length"]);
            filteredPagingDefinition.StartPageNo = searchData.start; //Convert.ToInt32(Request.Form["start"]); //TODO
            var result = await formB1B2Service.GetHeaderList(filteredPagingDefinition);
            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });

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
            assetDDLResponseDTO = await formB1B2Service.GetAssetDDL(request);
            return Json(assetDDLResponseDTO);
            // return Json(_formJService.GetDropdown(request));

        }

        [HttpPost]
        public IActionResult GetBridgeIds(AssetDDLRequestDTO request)
        {
            return Json(formB1B2Service.GetBridgeIds(request));
        }

        public async Task<IActionResult> Save(FormB1B2HeaderRequestDTO model) => Ok(await formB1B2Service.SaveHeader(model));


        /// <summary>
        /// To Get Bridge detail by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetBrideDetailById(int id)
        {
            var result = await formB1B2Service.GetBrideDetailById(id);
            return Ok(result);
        }

        public async Task<bool> RemoveHeader(int id) => await formB1B2Service.RemoveHeader(id);

        [HttpPost]
        public async Task<IActionResult> GetImageList(int assetPk)
        {
            DDLookUpDTO ddlookup = new DDLookUpDTO();
            FormB1B2ImageModel assetsModel = new FormB1B2ImageModel();
            assetsModel.AssetimageList = new List<FormB1B2ImgRequestDTO>();
            assetsModel.ImageTypeList = new List<string>();
            ddlookup.TypeCode = "BR";
            ddlookup.Type = "Photo Type";
            assetsModel.PhotoType = await _ddLookupService.GetDdLookup(ddlookup);
            if (assetsModel.PhotoType.Count() == 0)
            {
                assetsModel.PhotoType = new[]{ new SelectListItem
                {
                    Text = "Others",
                    Value = "Others"
                }};
            }
            assetsModel.AssetimageList = await formB1B2Service.GetAllImageByAssetPK(assetPk);
            assetsModel.ImageTypeList = assetsModel.AssetimageList.Select(c => c.ImageTypeCode).Distinct().ToList();
            return PartialView("~/Views/FormB1B2/_PhotoSectionPage.cshtml", assetsModel);
        }
        [HttpPost]
        //[DisableRequestSizeLimit]
        public async Task<IActionResult> ImageUploaded(IList<IFormFile> FormFile, int AssetId, List<string> PhotoType)
        {
            try
            {
                //int i = await formB1B2Service.ImageLastInsertedSRNO(AssetId, PhotoType);
                //i++;
                string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;
                //string photoType = Regex.Replace(PhotoType, @"[^a-zA-Z]", "");
                //string path = Path.Combine(wwwPath, Path.Combine("Uploads", "FormB1B2", AssetId.ToString(), photoType));

                int j = 0;

                if (FormFile.Count > 0)
                {
                    foreach (IFormFile postedFile in FormFile)
                    {
                        List<FormB1B2ImgRequestDTO> uploadedFiles = new List<FormB1B2ImgRequestDTO>();
                        int i = await formB1B2Service.ImageLastInsertedSRNO(AssetId, PhotoType[j]);
                        i++;
                        FormB1B2ImgRequestDTO _rmAssetImageDtl = new FormB1B2ImgRequestDTO();
                        string photoType = Regex.Replace(PhotoType[j], @"[^a-zA-Z]", "");
                        string path = Path.Combine(wwwPath, Path.Combine("Uploads", "FormB1B2", AssetId.ToString(), photoType));
                        string fileName = Path.GetFileName(postedFile.FileName);
                        string filerename = i + "_" + photoType + "_" + fileName;
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        using (FileStream stream = new FileStream(Path.Combine(path, filerename), FileMode.Create))
                        {
                            _rmAssetImageDtl.FbrihPkRefNo = AssetId;
                            _rmAssetImageDtl.ImageTypeCode = PhotoType[j];
                            _rmAssetImageDtl.ImageSrno = i;
                            _rmAssetImageDtl.ImageFilenameSys = postedFile.FileName;
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
                            _rmAssetImageDtl.ImageUserFilepath = $"/Uploads/FormB1B2/{AssetId}/{photoType}/{filerename}";

                            //AssetID_Section(Water)_incrementalNumber -- i + "_" + _rmAssetImageDtl.AssetId +_rmAssetImageDtl.ImageTypeCode + "_" + fileName;
                            //_rmAssetImageDtl.ImageFilename = _httpContext.HttpContext.Request.Host.Value + "\\" + "wwwroot" + "\\" + "Uploads" + "\\" + assetGrpCode[0] + "\\" + id + "\\" + photoType +"\\" + filerename;
                            postedFile.CopyTo(stream);

                            //ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);                            
                        }
                        _rmAssetImageDtl.CrDt = DateTime.UtcNow;
                        _rmAssetImageDtl.ModDt = DateTime.UtcNow;
                        _rmAssetImageDtl.CrBy = security.UserID;
                        _rmAssetImageDtl.ModBy = security.UserID;
                        uploadedFiles.Add(_rmAssetImageDtl);
                        var result = await formB1B2Service.SaveImageDtl(uploadedFiles);
                        if (result == -1)
                        {
                            return Json(result);
                        }
                        //i = i + 1;
                        j = j + 1;
                    }
                    //_bridgeBO.SaveAssetImageDtlBO(uploadedFiles);                   
                }
                else
                {
                    return BadRequest("No file found to upload");
                }
                return Json($"Successfully Uploaded {FormFile.Count} file(s)");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public async Task<IActionResult> DeleteImage(int id)
        {
            int RowsAffected = 0;
            RowsAffected = await formB1B2Service.DectivateAssetImage(id);
            return Json(RowsAffected);
        }




        [HttpPost] //B1B2Tab
        //[DisableRequestSizeLimit]
        public async Task<int> ImageUploadedTab(IFormCollection FormFile, int AssetId, string PhotoType)
        {
            try
            {
                int rowsAffected = 0;
                string wwwPath = this.Environment.WebRootPath;
                IFormCollection files = Request.ReadFormAsync().Result;
                if (files.Count > 0)
                {
                    foreach (var file in files.Files)
                    {
                        List<FormB1B2ImgRequestDTO> uploadedFiles = new List<FormB1B2ImgRequestDTO>();
                        int i = await formB1B2Service.ImageLastInsertedSRNO(AssetId, PhotoType);
                        i++;
                        FormB1B2ImgRequestDTO _rmAssetImageDtl = new FormB1B2ImgRequestDTO();
                        string photoType = Regex.Replace(PhotoType, @"[^a-zA-Z]", "");
                        string path = Path.Combine(wwwPath, Path.Combine("Uploads", "FormB1B2", AssetId.ToString(), photoType));
                        string fileName = Path.GetFileName(file.FileName);
                        string filerename = i + "_" + photoType + "_" + fileName;
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        using (FileStream stream = new FileStream(Path.Combine(path, filerename), FileMode.Create))
                        {
                            _rmAssetImageDtl.FbrihPkRefNo = AssetId;
                            _rmAssetImageDtl.ImageTypeCode = PhotoType;
                            _rmAssetImageDtl.ImageSrno = i;
                            _rmAssetImageDtl.ImageFilenameSys = file.FileName;
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
                            _rmAssetImageDtl.ImageUserFilepath = $"/Uploads/FormB1B2/{AssetId}/{photoType}/{filerename}";

                            await file.CopyToAsync(stream);
                        }
                        _rmAssetImageDtl.CrDt = DateTime.UtcNow;
                        _rmAssetImageDtl.ModDt = DateTime.UtcNow;
                        _rmAssetImageDtl.CrBy = security.UserID;
                        _rmAssetImageDtl.ModBy = security.UserID;
                        uploadedFiles.Add(_rmAssetImageDtl);
                        rowsAffected = await formB1B2Service.SaveImageDtl(uploadedFiles);
                    }
                }
                else
                {
                    return -1;
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