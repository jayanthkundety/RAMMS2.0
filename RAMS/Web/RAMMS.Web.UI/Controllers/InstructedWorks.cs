
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RAMMS.Business.ServiceProvider;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.RequestBO;
using RAMMS.Web.UI.Models;
using System.IO;
using Microsoft.Extensions.Configuration;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using AutoMapper;

namespace RAMMS.Web.UI.Controllers
{

    public class InstructedWorks : Models.BaseController
    {
        private readonly IFormW1Service _formW1Service;
        private readonly IFormW2Service _formW2Service;
        private readonly IDDLookUpService _ddLookupService;
        private IHostingEnvironment Environment;
        //  private readonly ILogger _logger;
        private readonly ISecurity _security;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserService _userService;
        FormW2Model _formW2Model = new FormW2Model();
        private readonly IConfiguration _configuration;
        private readonly IBridgeBO _bridgeBO;
        private readonly IMapper _mapper;

        public InstructedWorks(IWebHostEnvironment webhostenvironment, ISecurity security, IUserService userService, IDDLookUpService ddLookupService, IHostingEnvironment _environment, IFormW1Service formW1Service, IFormW2Service formW2Service, IConfiguration configuration, IBridgeBO bridgeBO, IMapper mapper)
        {


            _ddLookupService = ddLookupService;
            Environment = _environment;
            _webHostEnvironment = webhostenvironment;

            _userService = userService;
            _formW1Service = formW1Service ?? throw new ArgumentNullException(nameof(formW1Service));
            _formW2Service = formW2Service ?? throw new ArgumentNullException(nameof(formW2Service));
            _configuration = configuration;
            _bridgeBO = bridgeBO;
            _security = security;
            _mapper = mapper;
            //  _logger = logger;

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddFormW1()
        {
            FormW1Model model = new FormW1Model();
            model.FormW1.RecomondedInstrctedWork = "none";

            DDLookUpDTO ddLookup = new DDLookUpDTO();
            ddLookup.Type = "Month";
            ViewData["Months"] = await _ddLookupService.GetDdDescValue(ddLookup);
            LoadLookupService("RMU", "Division", "RD_Code", "User");

            GetRMUWithDivision("RMU_Division");

            return View("~/Views/InstructedWorks/AddFormW1.cshtml", model);
        }

        public async Task<IActionResult> EditFormW1(int id)
        {
            var _formW1Model = new FormW1ResponseDTO();


            if (id > 0)
            {
                DDLookUpDTO ddLookup = new DDLookUpDTO();
                ddLookup.Type = "Month";
                ViewData["Months"] = await _ddLookupService.GetDdDescValue(ddLookup);
                LoadLookupService("RMU", "Division", "RD_Code", "User");
            
               _formW1Model = await _formW1Service.FindFormW1ByID(id);
            }
            FormW1Model model = new FormW1Model();
            model.FormW1 = _formW1Model;

            return PartialView("~/Views/InstructedWorks/AddFormW1.cshtml", model);
        }



        [HttpPost]
        public async Task<IActionResult> SaveFormW1(FormW1Model frm)
        {
            int refNo = 0;
            frm.FormW1.ActiveYn = true;
            if (frm.FormW1.PkRefNo == 0)
            {
                refNo = await _formW1Service.SaveFormW1(frm.FormW1);
            }
            else
            {
                refNo = await _formW1Service.Update(frm.FormW1);
            }
            return Json(refNo);


        }


        private async Task LoadN2DropDown()
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            ddLookup.Type = "Region";
            ViewData["Region"] = await _ddLookupService.GetDdLookup(ddLookup);

            ddLookup.Type = "Service Provider";
            ddLookup.TypeCode = "SP";
            ViewData["Service Provider"] = await _ddLookupService.GetDdLookup(ddLookup);

            LoadLookupService("RMU", "Division", "RD_Code");

            ddLookup.Type = "Month";
            ddLookup.TypeCode = "";
            ViewData["Months"] = await _ddLookupService.GetDdDescValue(ddLookup);

            ViewData["Users"] = _userService.GetUserSelectList(null);

            ViewData["FormW1s"] = await _formW2Service.GetFormW1DDL();

        }
        public async Task<IActionResult> AddFormW2(int id = 0)
        {
            _formW2Model = new FormW2Model();
            _formW2Model.SaveFormW2Model = new DTO.ResponseBO.FormW2ResponseDTO();
            if (id != 0)
            {
                var _formW1Model = await _formW2Service.GetFormW1ById(id);
                _formW2Model.SaveFormW2Model.SupInstNo = _formW1Model.ReferenceNo;
                _formW2Model.SaveFormW2Model.Fw1RefNo = _formW1Model.PkRefNo;
                _formW2Model.SaveFormW2Model.TitleOfInstructWork = _formW1Model.ProjectTitle;
                _formW2Model.SaveFormW2Model.CeilingEstCost = _formW1Model.EstimTotalCost;
                _formW2Model.FormW1 = _formW1Model;
            }
            await LoadN2DropDown();
            return View("~/Views/InstructedWorks/AddFormW2.cshtml", _formW2Model);
        }

        public IActionResult IWIndex()
        {
            return View();
        }

        public async Task<IActionResult> EditFormW2(int id)
        {
            _formW2Model = new FormW2Model();

            if (id > 0)
            {
                await LoadN2DropDown();
                var result = await _formW2Service.FindW2ByID(id);
                var res = (List<CSelectListItem>)ViewData["RD_Code"];
                res.Find(c => c.Value == result.RoadCode).Selected = true;
                
                _formW2Model.SaveFormW2Model = result;
                _formW2Model.FormW1 = _formW2Model.SaveFormW2Model.Fw1RefNoNavigation;
            }

            return View("~/Views/InstructedWorks/AddFormW2.cshtml", _formW2Model);
        }

        [HttpPost]
        public async Task<JsonResult> SaveFormW2(Models.FormW2Model formW2)
        {
            int refNo = 0;
            FormW2ResponseDTO saveRequestObj = new FormW2ResponseDTO();
            saveRequestObj = formW2.SaveFormW2Model;
            if (saveRequestObj.PkRefNo == 0)
                refNo = await _formW2Service.Save(formW2.SaveFormW2Model);
            else
                refNo = await _formW2Service.Update(formW2.SaveFormW2Model);

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
                var imageDetails = new List<FormW2ImageResponseDTO>();
                #endregion Get File and renaming

                //Copying File
                System.IO.File.Copy(Oldfilename, cachefile, true);
                using (var xlWorkbook = new XLWorkbook(cachefile))
                {
                    IXLWorksheet ixlWorksheet;
                    xlWorkbook.Worksheets.TryGetWorksheet("Sheet1", out ixlWorksheet);
                    if (form == "FormW1")
                    {
                        imageDetails = await _formW2Service.GetImageList(id);
                    }
                    else if (form == "FormW2")
                    {
                        imageDetails = await _formW2Service.GetImageList(id);
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
                                var imageType = simageDetails[x].ImageUserFilePath.ToString().ToLower().Split('.');
                                var imagePath = "";
                                if (imageType[1] == "jpg" || imageType[1] == "png" || imageType[1] == "jpeg")
                                {
                                    if (form == "FormW2")
                                    {
                                        imagePath = _webHostEnvironment.WebRootPath + "\\"/* + "Uploads" + "\\" + "FormD" + "\\"*/ + simageDetails[x].ImageFilenameUpload;
                                    }
                                    else if (form == "FormW1")
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
                    if (form == "FormW2")
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
        public IActionResult Delete(int id)
        {
            if (id > 0) { return Ok(new { id = _formW2Service.Delete(id) }); }
            else { return BadRequest("Invalid Request!"); }

        }

        [HttpPost]
        public async Task<IActionResult> GetW2ImageList(int formW2Id, string assetgroup)
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            FormW2Model assetsModel = new FormW2Model();
            assetsModel.ImageList = new List<FormW2ImageResponseDTO>();
            assetsModel.ImageTypeList = new List<string>();
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
            ViewBag.PhotoTypeList = await _ddLookupService.GetDdLookup(ddLookup);
            assetsModel.ImageList = await _formW2Service.GetImageList(formW2Id);
            assetsModel.ImageTypeList = assetsModel.ImageList.Select(c => c.ImageTypeCode).Distinct().ToList();
            return PartialView("~/Views/InstructedWorks/_PhotoSectionPage.cshtml", assetsModel);
        }

        [HttpPost]
        public async Task<IActionResult> ImageUploadedFormW2(IList<IFormFile> formFile, string id, List<string> photoType)
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
                    List<FormW2ImageResponseDTO> uploadedFiles = new List<FormW2ImageResponseDTO>();
                    FormW2ImageResponseDTO _rmAssetImageDtl = new FormW2ImageResponseDTO();
                    string photo_Type = Regex.Replace(photoType[j], @"[^a-zA-Z]", "");
                    string subPath = Path.Combine(@"Uploads/FormW2/", _id, photo_Type);
                    string path = Path.Combine(wwwPath, Path.Combine(@"Uploads\FormW2\", _id, photo_Type));
                    int i = await _formW2Service.LastInsertedIMAGENO(int.Parse(id), photo_Type);
                    i++;
                    string fileName = Path.GetFileName(postedFile.FileName);
                    string fileRename = i + "_" + photo_Type + "_" + fileName;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream stream = new FileStream(Path.Combine(path, fileRename), FileMode.Create))
                    {
                        _rmAssetImageDtl.Fw2RefNo = int.Parse(id);
                        _rmAssetImageDtl.ImageTypeCode = photoType[j];
                        _rmAssetImageDtl.ImageUserFilePath = postedFile.FileName;
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
                        await _formW2Service.SaveImage(uploadedFiles);
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

    }
}
