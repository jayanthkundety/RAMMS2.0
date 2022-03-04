
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
using RAMMS.DTO.Wrappers;
using X.PagedList;
using RAMMS.DTO.JQueryModel;

namespace RAMMS.Web.UI.Controllers
{

    public class InstructedWorks : Models.BaseController
    {
        private readonly IFormW1Service _formW1Service;
        private readonly IFormW2Service _formW2Service;
        private readonly IFormWCService _formWCService;
        private readonly IFormWGService _formWGService;
        private readonly IDDLookUpService _ddLookupService;
        private readonly IRoadMasterService _roadMasterService;

        private readonly IDivisionService _divisionService;

        private IHostingEnvironment Environment;
        //  private readonly ILogger _logger;
        private readonly ISecurity _security;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserService _userService;
        FormW2Model _formW2Model = new FormW2Model();
        private readonly IConfiguration _configuration;
        private readonly IBridgeBO _bridgeBO;
        private readonly IMapper _mapper;
        FormIWModel _formIWModel = new FormIWModel();

        public InstructedWorks(IWebHostEnvironment webhostenvironment, ISecurity security, IUserService userService, IDDLookUpService ddLookupService,
            IRoadMasterService roadMasterService, IHostingEnvironment _environment, IFormW1Service formW1Service, IFormW2Service formW2Service,
            IDivisionService divisionService, IConfiguration configuration, IBridgeBO bridgeBO, IMapper mapper, IFormWCService formWCService,
            IFormWGService formWGService)
        {


            _ddLookupService = ddLookupService;
            _roadMasterService = roadMasterService;
            Environment = _environment;
            _webHostEnvironment = webhostenvironment;
            _divisionService = divisionService;
            _userService = userService;
            _formW1Service = formW1Service ?? throw new ArgumentNullException(nameof(formW1Service));
            _formW2Service = formW2Service ?? throw new ArgumentNullException(nameof(formW2Service));
            _formWCService = formWCService ?? throw new ArgumentNullException(nameof(formWCService));
            _formWGService = formWGService ?? throw new ArgumentNullException(nameof(formWGService));
            _configuration = configuration;
            _bridgeBO = bridgeBO;
            _security = security;
            _mapper = mapper;
            //  _logger = logger;

        }

        #region IW

        public IActionResult Index()
        {
            LoadLookupService("RMU", "RD_Code", "TECM_Status");
            var res = ((IEnumerable<CSelectListItem>)ViewData["TECM_Status"]).ToList();
            res.Insert(0, new CSelectListItem()
            {
                Value = "All",
                Text = "All"
            });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetIWImageList(string Id, string assetgroup, string form)
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            FormIWImageModel assetsModel = new FormIWImageModel();
            assetsModel.ImageList = new List<FormIWImageResponseDTO>();
            assetsModel.ImageTypeList = new List<string>();
            ddLookup.Type = "Photo Type";
            ddLookup.TypeCode = "IW";
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
            assetsModel.ImageList = await _formW1Service.GetImageList(Id);
            assetsModel.IwRefNo = Id;
            assetsModel.FormName = form;
            assetsModel.ImageTypeList = assetsModel.ImageList.Select(c => c.ImageTypeCode).Distinct().ToList();
            return PartialView("~/Views/InstructedWorks/_PhotoSectionPage.cshtml", assetsModel);
        }

        [HttpPost]
        public async Task<IActionResult> ImageUploadFormIw(IList<IFormFile> formFile, string PkRefNo, List<string> photoType, string Source = "ALL")
        {
            try
            {
                bool successFullyUploaded = false;
                string wwwPath = this._webHostEnvironment.WebRootPath;
                string contentPath = this._webHostEnvironment.ContentRootPath;
                string _id = Regex.Replace(PkRefNo, @"[^0-9a-zA-Z]+", "");

                int j = 0;
                foreach (IFormFile postedFile in formFile)
                {
                    List<FormIWImageResponseDTO> uploadedFiles = new List<FormIWImageResponseDTO>();
                    FormIWImageResponseDTO _rmAssetImageDtl = new FormIWImageResponseDTO();


                    string photo_Type = Regex.Replace(photoType[j], @"[^a-zA-Z]", "");
                    string subPath = Path.Combine(@"Uploads/FormW1/", _id, photo_Type);
                    string path = Path.Combine(wwwPath, Path.Combine(@"Uploads\FormW1\", _id, photo_Type));
                    int i = await _formW1Service.LastInsertedIMAGENO(PkRefNo, photo_Type);
                    i++;
                    string fileName = Path.GetFileName(postedFile.FileName);
                    string fileRename = i + "_" + photo_Type + "_" + fileName;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream stream = new FileStream(Path.Combine(path, fileRename), FileMode.Create))
                    {
                        _rmAssetImageDtl.Fw1IwRefNo = PkRefNo;
                        _rmAssetImageDtl.ImageTypeCode = photoType[j];
                        _rmAssetImageDtl.ImageUserFilePath = postedFile.FileName;
                        _rmAssetImageDtl.ImageSrno = i;
                        _rmAssetImageDtl.Source = Source;

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
                        await _formW1Service.SaveImage(uploadedFiles);
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


        [HttpPost]
        public async Task<IActionResult> detailSearchDdList(AssetDDLRequestDTO assetDDLRequestDTO)
        {
            AssetDDLResponseDTO assetDDLResponseDTO = new AssetDDLResponseDTO();
            assetDDLResponseDTO = await _roadMasterService.GetAssetDDL(assetDDLRequestDTO);
            return Json(assetDDLResponseDTO);
        }

        [HttpPost]
        public async Task<IActionResult> GetNameByCode(DDLookUpDTO dDLookUpDTO)
        {
            string name = "";
            if (dDLookUpDTO.Type == "Section Code")
            {
                name = await _ddLookupService.GetDDLValueforTypeAndDesc(dDLookUpDTO);
            }
            else if (dDLookUpDTO.Type == "RD_Code")
            {
                RoadMasterRequestDTO roadMasterRequestDTO = new RoadMasterRequestDTO();
                RoadMasterResponseDTO roadMasterResponseDTO = new RoadMasterResponseDTO();
                roadMasterRequestDTO.RoadCode = dDLookUpDTO.TypeCode;
                if (roadMasterRequestDTO.RoadCode != null)
                {
                    roadMasterResponseDTO = await _roadMasterService.GetAllRoadCodeData(roadMasterRequestDTO);
                    name = roadMasterResponseDTO.RoadName;
                }
                else
                {
                    name = "";
                }
            }
            return Json(name);
        }

        [HttpPost]
        public async Task<IActionResult> IwDeleteImage(int pkId)
        {
            int rowsAffected = 0;
            rowsAffected = await _formW1Service.DeActivateImage(pkId);
            return Json(rowsAffected);
        }


        #endregion

        #region FormW1

        public async Task LoadDropDownsSectionCode()
        {
            AssetDDLRequestDTO assetDDLRequestDTO = new AssetDDLRequestDTO();
            var assetDDLResponseDTO = await _roadMasterService.GetAssetDDL(assetDDLRequestDTO);
            ViewBag.SectionCodeList = from d in assetDDLResponseDTO.Section select new SelectListItem { Text = d.Text, Value = d.Value };
        }
        public async Task<IActionResult> AddFormW1()
        {
            FormW1Model model = new FormW1Model();
            model.FormW1.RecomdType = 0;
            model.View = 0;
            model.FormW1.UseridReq = _security.UserID;
            model.FormW1.TecmDt = DateTime.Today;
            model.FormW1.InitialProposedDate = DateTime.Today;
            model.FormW1.InitialProposedDate = DateTime.Today;
            model.FormW1.Dt = DateTime.Today;
            model.FormW1.DtReq = DateTime.Today;
            model.FormW1.DtVer = DateTime.Today;


            DDLookUpDTO ddLookup = new DDLookUpDTO();
            ddLookup.Type = "Month";
            ViewData["Months"] = await _ddLookupService.GetDdDescValue(ddLookup);
            LoadLookupService("RMU", "Division", "RD_Code", "User", "TECM_Status");
            await LoadDropDownsSectionCode();
            GetRMUWithDivision("RMU_Division");
            ViewData["ServiceProviderName"] = LookupService.LoadServiceProviderName().Result;

            return View("~/Views/InstructedWorks/AddFormW1.cshtml", model);
        }

        public async Task<IActionResult> EditFormW1(int id, int View)
        {
            var _formW1Model = new FormW1ResponseDTO();

            if (id > 0)
            {
                DDLookUpDTO ddLookup = new DDLookUpDTO();
                ddLookup.Type = "Month";
                ViewData["Months"] = await _ddLookupService.GetDdDescValue(ddLookup);
                LoadLookupService("RMU", "Division", "RD_Code", "User", "TECM_Status");

                _formW1Model = await _formW1Service.FindFormW1ByID(id);
            }
            FormW1Model model = new FormW1Model();
            model.FormW1 = _formW1Model;
            model.View = View;

            if (model.FormW1.Status == "Verified")
            {
                model.View = 1;
            }

            await LoadDropDownsSectionCode();
            GetRMUWithDivision("RMU_Division");
            ViewData["ServiceProviderName"] = LookupService.LoadServiceProviderName().Result;

            FormW1Model assetsModel = new FormW1Model();
            model.ImageList = await _formW1Service.GetImageList(_formW1Model.PkRefNo.ToString());
            return PartialView("~/Views/InstructedWorks/AddFormW1.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveFormW1(FormW1Model frm)
        {
            int refNo = 0;
            frm.FormW1.ActiveYn = true;
            if (frm.FormW1.PkRefNo == 0)
            {
                frm.FormW1.Status = "Saved";
                refNo = await _formW1Service.SaveFormW1(frm.FormW1);
            }
            else
            {
                refNo = await _formW1Service.Update(frm.FormW1);
            }
            return Json(refNo);


        }

        #endregion

        #region FormW2


        private async Task LoadN2DropDown()
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            ddLookup.Type = "Region";
            ViewData["Region"] = await _ddLookupService.GetDdLookup(ddLookup);

            ddLookup.Type = "Service Provider";
            ddLookup.TypeCode = "SP";
            ViewData["Service Provider"] = await _ddLookupService.GetDdLookup(ddLookup);


            ViewData["IWRefNo"] = await _formW2Service.GetFormW1DDL();


            LoadLookupService("RMU", "Division", "RD_Code", "Region", "TECM_Status", "User");

            ddLookup.Type = "Month";
            ddLookup.TypeCode = "";
            ViewData["Months"] = await _ddLookupService.GetDdDescValue(ddLookup);
        }

        public async Task<IActionResult> AddFormW2(int id)
        {
            _formW2Model = new FormW2Model();
            await LoadN2DropDown();
            _formW2Model.FormW1 = await _formW2Service.GetFormW1ById(id);
            _formW2Model.FECM = new FormFECMModel();
            _formW2Model.FECM.FECM = new FormW2FECMResponseDTO();
            _formW2Model.FECM.FormW1 = _formW2Model.FormW1;
            _formW2Model.FECM.W1Date = _formW2Model.FormW1.Dt;
            _formW2Model.FECM.FECM.Dt = DateTime.Today;

            var defaultData = new DTO.ResponseBO.FormW2ResponseDTO();
            defaultData.Fw1IwRefNo = _formW2Model.FormW1.IwRefNo;
            defaultData.Fw1PkRefNo = _formW2Model.FormW1.PkRefNo;
            defaultData.Fw1ProjectTitle = _formW2Model.FormW1.ProjectTitle;
            defaultData.DateOfInitation = DateTime.Today;
            defaultData.RmuCode = _formW2Model.FormW1.RmuCode;
            defaultData.RmuName = "";
            var ser = (List<SelectListItem>)LookupService.LoadServiceProviderName().Result.ToList();
            var serRd = ser.Find(c => c.Value == _formW2Model.FormW1.ServPropName);
            defaultData.ServProvName = serRd.Text;

            defaultData.DivCode = _formW2Model.FormW1.DivnCode;
            defaultData.DivisonName = "";

            if (_formW2Model.FormW1.RoadCode != null)
            {
                var res = (List<CSelectListItem>)ViewData["RD_Code"];
                var resRd = res.Find(c => c.Value == _formW2Model.FormW1.RoadCode);
                if (resRd != null) resRd.Selected = true;
                defaultData.RoadCode = _formW2Model.FormW1.RoadCode;
                defaultData.RoadName = _formW2Model.FormW1.RoadName;
                defaultData.Ch = _formW2Model.FormW1.Ch;
                defaultData.ChDeci = _formW2Model.FormW1.ChDeci;
            }

            defaultData.SerProvRefNo = _formW2Model.FormW1.ServPropRefNo;
            defaultData.EstCostAmt = _formW2Model.FormW1.EstimTotalCostAmt;
            _formW2Model.SaveFormW2Model = defaultData;
            //_formW2Model.FormW1 = new FormW1ResponseDTO();


            return View("~/Views/InstructedWorks/AddFormW2.cshtml", _formW2Model);
        }

        public IActionResult IWIndex()
        {
            return View();
        }

        public async Task<IActionResult> EditFormW2(int id, string view)
        {
            _formW2Model = new FormW2Model();

            if (id > 0)
            {
                await LoadN2DropDown();
                _formW2Model.FECM = new FormFECMModel();
                var resultFormW2 = await _formW2Service.FindW2ByID(id);
                var res = (List<CSelectListItem>)ViewData["RD_Code"];
                res.Find(c => c.Value == resultFormW2.RoadCode).Selected = true;
                var resultFCEM = await _formW2Service.FindFCEM2ByW2ID(id);
                if (resultFCEM == null) resultFCEM = new FormW2FECMResponseDTO();
                _formW2Model.FECM.FECM = resultFCEM;

                if (resultFCEM.SubmitSts || view == "1") _formW2Model.FECM.View = "1";

                if (resultFormW2.SubmitSts || view == "1") _formW2Model.View = "1";

                _formW2Model.SaveFormW2Model = resultFormW2;
                _formW2Model.FormW1 = _formW2Model.SaveFormW2Model.Fw1PkRefNoNavigation;
                _formW2Model.FECM.FormW1 = _formW2Model.FormW1;
                _formW2Model.FECM.FECM.Fw2PkRefNo = resultFormW2.PkRefNo;
                _formW2Model.FECM.W1Date = _formW2Model.FormW1.Dt;

            }

            return View("~/Views/InstructedWorks/AddFormW2.cshtml", _formW2Model);
        }

        [HttpPost]
        public async Task<JsonResult> SaveFormW2(FormW2ResponseDTO formW2)
        {
            int refNo = 0;
            FormW2ResponseDTO saveRequestObj = new FormW2ResponseDTO();
            saveRequestObj = formW2;
            if (saveRequestObj.PkRefNo == 0)
                refNo = await _formW2Service.Save(formW2);
            else
                refNo = await _formW2Service.Update(formW2);

            return Json(refNo);
        }

        //Image Print
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
                var imageDetailsW1 = new List<FormIWImageResponseDTO>();
                var imageDetails = new List<FormIWImageResponseDTO>();
                #endregion Get File and renaming

                //Copying File
                System.IO.File.Copy(Oldfilename, cachefile, true);
                using (var xlWorkbook = new XLWorkbook(cachefile))
                {
                    IXLWorksheet ixlWorksheet;
                    xlWorkbook.Worksheets.TryGetWorksheet("Sheet1", out ixlWorksheet);
                    if (form == "FormW1")
                    {
                        //   imageDetailsW1 = await _formW1Service.GetImageList(id);
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

        //[HttpPost]
        //public async Task<IActionResult> GetW2ImageList(int formW2Id, string assetgroup)
        //{
        //    DDLookUpDTO ddLookup = new DDLookUpDTO();
        //    FormW2Model assetsModel = new FormW2Model();
        //    assetsModel.ImageList = new List<FormIWImageResponseDTO>();
        //    assetsModel.ImageTypeList = new List<string>();
        //    ddLookup.Type = "Photo Type";
        //    ddLookup.TypeCode = "IW";
        //    assetsModel.PhotoType = await _ddLookupService.GetDdLookup(ddLookup);
        //    if (assetsModel.PhotoType.Count() == 0)
        //    {
        //        assetsModel.PhotoType = new[]{ new SelectListItem
        //        {
        //            Text = "Others",
        //            Value = "Others"
        //        }};
        //    }
        //    ViewBag.PhotoTypeList = await _ddLookupService.GetDdLookup(ddLookup);
        //    assetsModel.ImageList = await _formW2Service.GetImageList(formW2Id);
        //    assetsModel.ImageTypeList = assetsModel.ImageList.Select(c => c.ImageTypeCode).Distinct().ToList();
        //    return PartialView("~/Views/InstructedWorks/_PhotoSectionPage.cshtml", assetsModel);
        //}


        public async Task<IActionResult> GetW1DetailsByRoadCode(string roadCode)
        {
            FormW1ResponseDTO formW1 = await _formW2Service.GetFormW1ByRoadCode(roadCode);
            return Json(formW1);
        }

        public async Task<IActionResult> GetW1Details(int w1PkRefNo)
        {
            FormW1ResponseDTO formW1 = await _formW2Service.GetFormW1ById(w1PkRefNo);
            return Json(formW1);
        }

        public async Task<IActionResult> GetRoadCodeByRMU(string rmu)
        {
            var obj = await _formW2Service.GetRoadCodesByRMU(rmu);
            return Json(obj);
        }

        public async Task<IActionResult> LoadFormIWList(DataTableAjaxPostModel<FormIWSearchGridDTO> searchData)
        {
            if (Request.Form.ContainsKey("columns[0][search][value]"))
            {
                searchData.filterData.SmartInputValue = Request.Form["columns[0][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[1][search][value]"))
            {
                searchData.filterData.IWRefNo = Request.Form["columns[1][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[2][search][value]"))
            {
                searchData.filterData.CommencementFrom = Request.Form["columns[2][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[3][search][value]"))
            {
                searchData.filterData.CommencementTo = Request.Form["columns[3][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[4][search][value]"))
            {
                searchData.filterData.PrjTitle = Request.Form["columns[4][search][value]"].ToString();
            }

            if (Request.Form.ContainsKey("columns[5][search][value]"))
            {
                searchData.filterData.Status = Request.Form["columns[5][search][value]"].ToString();
            }

            if (Request.Form.ContainsKey("columns[6][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[6][search][value]"].ToString()))
                {
                    searchData.filterData.PercentageFrom = Convert.ToInt32(Request.Form["columns[6][search][value]"].ToString());
                }
            }

            if (Request.Form.ContainsKey("columns[7][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[7][search][value]"].ToString()))
                {
                    searchData.filterData.PercentageTo = Convert.ToInt32(Request.Form["columns[7][search][value]"].ToString());
                }
            }


            if (Request.Form.ContainsKey("columns[8][search][value]"))
            {
                if (!string.IsNullOrEmpty(Request.Form["columns[8][search][value]"].ToString()))
                {
                    searchData.filterData.Months = Convert.ToInt32(Request.Form["columns[8][search][value]"].ToString());
                }
            }

            if (Request.Form.ContainsKey("columns[9][search][value]"))
            {
                searchData.filterData.RMU = Request.Form["columns[9][search][value]"].ToString();
            }

            if (Request.Form.ContainsKey("columns[10][search][value]"))
            {
                searchData.filterData.RoadCode = Request.Form["columns[10][search][value]"].ToString();
            }

            FilteredPagingDefinition<FormIWSearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormIWSearchGridDTO>();

            filteredPagingDefinition.Filters = searchData.filterData;
            filteredPagingDefinition.RecordsPerPage = searchData.length;
            filteredPagingDefinition.StartPageNo = searchData.start;
            if (searchData.order != null)
            {
                filteredPagingDefinition.ColumnIndex = searchData.order[0].column;
                filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }
            var result = await _formW2Service.GetFilteredFormIWGrid(filteredPagingDefinition).ConfigureAwait(false);

            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });

        }

        public async Task<IActionResult> SearchFormIWList(FormIWSearchGridDTO filterData)
        {
            FormIWModel formObj = new FormIWModel();
            formObj.SearchObj = filterData;
            await LoadHeaderGridList(formObj);
            return PartialView("~/Views/InstructedWorks/_HeaderListGrid.cshtml", formObj);

        }

        [HttpPost]
        public async Task<IActionResult> LoadHeaderGridList(FormIWModel formIWModel)
        {
            FormIWSearchGridDTO searchObj = new FormIWSearchGridDTO();
            searchObj = formIWModel.SearchObj;

            ViewBag.CurrentSort = formIWModel.SearchObj.sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(formIWModel.SearchObj.sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = formIWModel.SearchObj.sortOrder == "Date" ? "date_desc" : "Date";
            if (formIWModel.SearchObj.searchString != null)
            {
                formIWModel.SearchObj.Page_No = 1;
            }
            else
            {
                formIWModel.SearchObj.searchString = formIWModel.SearchObj.currentFilter;
            }

            ViewBag.CurrentFilter = formIWModel.SearchObj.searchString;
            int Size_Of_Page = (formIWModel.SearchObj.pageSize ?? 1000);
            int No_Of_Page = (formIWModel.SearchObj.Page_No ?? 1);
            ViewBag.psize = Size_Of_Page;
            ViewBag.PageSize = new List<SelectListItem>()
            {
             new SelectListItem() { Value="5", Text= "5" },
             new SelectListItem() { Value="10", Text= "10" },
             new SelectListItem() { Value="15", Text= "15" },
             new SelectListItem() { Value="25", Text= "25" },
             new SelectListItem() { Value="50", Text= "50" }
            };


            FilteredPagingDefinition<FormIWSearchGridDTO> filteredPagingDefinition = new FilteredPagingDefinition<FormIWSearchGridDTO>();
            filteredPagingDefinition.Filters = searchObj;
            filteredPagingDefinition.RecordsPerPage = 5;
            filteredPagingDefinition.StartPageNo = 1; //TODO

            var result = await _formW2Service.GetFilteredFormIWGrid(filteredPagingDefinition);
            var obj = result.PageResult;
            IPagedList<FormIWResponseDTO> headerList = obj.ToPagedList(No_Of_Page, Size_Of_Page);

            _formIWModel.FormIWHeaderList = headerList;
            ViewBag.TotalNoRecords = headerList.TotalItemCount.ToString();
            int iPreDisplay = ((No_Of_Page) * Size_Of_Page);
            ViewBag.DisplayRecords = iPreDisplay;

            ViewBag.TotalPage = headerList.PageCount;
            var CurrentPage = (headerList.PageCount < headerList.PageNumber ? 0 : headerList.PageNumber);
            ViewBag.CurrentPage = CurrentPage;

            //Added for Temporary Count showing
            ViewBag.DisplayRecords = result.FilteredRecords;
            ViewBag.TotalNoRecords = result.TotalRecords;

            return View(_formIWModel);
        }

        #endregion

        #region FCEM
        public async Task<JsonResult> SaveFCEM(FormW2FECMResponseDTO formW2)
        {
            int refNo = 0;
            FormW2FECMResponseDTO saveRequestObj = new FormW2FECMResponseDTO();
            saveRequestObj = formW2;
            if (saveRequestObj.PkRefNo == 0)
                refNo = await _formW2Service.SaveFCEM(formW2);
            else
                refNo = await _formW2Service.UpdateFCEM(formW2);

            return Json(refNo);
        }
        #endregion

        #region WCWG

        public async Task<IActionResult> OpenWCWG(int id)
        {

            var _formWCWGModel = new FormWCWGModel();
            LoadLookupService("TECM_Status", "User");
            _formWCWGModel.FormW1 = await _formW2Service.GetFormW1ById(id);
            var spList = await _divisionService.GetServiceProviders();
            var serProv = spList.ServiceProviders.Find(s => s.Code == _formWCWGModel.FormW1.ServPropName);
            _formWCWGModel.FormW1.ServPropName = serProv.Name;
            _formWCWGModel.FormW1.ServAddress1 = serProv.Adress1;
            _formWCWGModel.FormW1.ServAddress2 = serProv.Adress2;
            _formWCWGModel.FormW1.ServAddress3 = serProv.Adress3;
            _formWCWGModel.FormW1.ServPhone = serProv.Phone;
            _formWCWGModel.FormW1.ServFax = serProv.Fax;
            
            _formWCWGModel.FormWC = new FormWCResponseDTO();
            _formWCWGModel.FormWG = new FormWGResponseDTO();
            _formWCWGModel.Division = await _divisionService.GetDivisions();

            return View("~/Views/InstructedWorks/FormWCWG.cshtml", _formWCWGModel);
        }

        public async Task<IActionResult> EditFormWCWG(int id)
        {

            var _formWCWGModel = new FormWCWGModel();
            LoadLookupService("TECM_Status", "User");
            var _formC = await _formWCService.FindWCByW1ID(id);
            _formWCWGModel.FormWC = _formC == null ? new FormWCResponseDTO() : _formC;
            var _formG = await _formWGService.FindWGByW1ID(id);

            _formWCWGModel.FormWG = _formG == null ? new FormWGResponseDTO() : _formG;

            _formWCWGModel.FormW1 = _formC != null ? _formWCWGModel.FormWC.Fw1PkRefNoNavigation : new FormW1ResponseDTO();

            var spList = await _divisionService.GetServiceProviders();
            var serProv = spList.ServiceProviders.Find(s => s.Code == _formWCWGModel.FormW1.ServPropName);
            _formWCWGModel.FormW1.ServPropName = serProv.Name;
            _formWCWGModel.FormW1.ServAddress1 = serProv.Adress1;
            _formWCWGModel.FormW1.ServAddress2 = serProv.Adress2;
            _formWCWGModel.FormW1.ServAddress3 = serProv.Adress3;
            _formWCWGModel.FormW1.ServPhone = serProv.Phone;
            _formWCWGModel.FormW1.ServFax = serProv.Fax;
            _formWCWGModel.Division = await _divisionService.GetDivisions();

            return View("~/Views/InstructedWorks/FormWCWG.cshtml", _formWCWGModel);
        }

        

        public async Task<JsonResult> SaveFormWC(FormWCResponseDTO formWC)
        {
            int refNo = 0;
            FormWCResponseDTO saveRequestObj = new FormWCResponseDTO();
            saveRequestObj = formWC;
            if (saveRequestObj.PkRefNo == 0)
                refNo = await _formWCService.Save(formWC);
            else
                refNo = await _formWCService.Update(formWC);

            return Json(refNo);
        }

       
        #endregion

        #region WDWN

        public async Task<IActionResult> OpenWDWN(int PkRefNo = 15)
        {
            var _formWDWNModel = new FormWDWNModel();
            LoadLookupService("TECM_Status","User");
            ViewData["ServiceProviderName"] = LookupService.LoadServiceProviderName().Result;
            _formWDWNModel.FormW1 = await _formW1Service.FindFormW1ByID(PkRefNo);
            _formWDWNModel.FormWD = new FormWDResponseDTO();
            _formWDWNModel.FormWN = new FormWNResponseDTO();
            return View("~/Views/InstructedWorks/FormWDWN.cshtml", _formWDWNModel);
        }

        public async Task<JsonResult> SaveFormWG(FormWGResponseDTO formWG)
        {
            int refNo = 0;
            FormWGResponseDTO saveRequestObj = new FormWGResponseDTO();
            saveRequestObj = formWG;
            if (saveRequestObj.PkRefNo == 0)
                refNo = await _formWGService.Save(formWG);
            else
                refNo = await _formWGService.Update(formWG);

            return Json(refNo);
        }

        #endregion

    }
}
