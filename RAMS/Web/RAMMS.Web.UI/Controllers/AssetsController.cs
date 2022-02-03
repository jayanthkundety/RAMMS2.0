using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using RAMMS.Business.ServiceProvider;
using X.PagedList;
using RAMMS.Web.UI.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.Business.ServiceProvider.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Domain.Models;
using RAMMS.DTO.Wrappers;
using System.Data;
using ExcelDataReader;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using ClosedXML.Excel;
using System.IO.Compression;
using RAMMS.DTO.JQueryModel;
using System.Reflection;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using RAMMS.DTO.SearchBO;
using RAMMS.Web.UI.Filters;
using Ionic.Zip;

namespace RAMMS.Web.UI.Controllers
{
    [CAuthorize(ModuleName = ModuleNameList.Asset_Inventory)]
    public class AssetsController : Controller
    {
        AssetsModel _allAssetsModel = new AssetsModel();
        private readonly IAssetsService _assetsService;
        private readonly IDDLookUpService _ddLookupService;
        private readonly IRoadMasterService _roadMasterService;
        private readonly IWebHostEnvironment _webhostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IImageService _imageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISecurity _security;

        public AssetsController(IAssetsService assetsService, IImageService imageService, IDDLookUpService ddLookupService, IRoadMasterService roadMasterService, 
            IWebHostEnvironment webhostenvironment, IConfiguration configuration, IHttpContextAccessor accessor, ISecurity security)

        {
            _assetsService = assetsService;
            _ddLookupService = ddLookupService;
            _roadMasterService = roadMasterService;
            _webhostEnvironment = webhostenvironment;
            _configuration = configuration;
            _imageService = imageService;
            _httpContextAccessor = accessor;
            _security = security;
        }

        [CAuthorize(ModuleName = ModuleNameList.Asset_Inventory, IsView = true)]
        public async Task<IActionResult> List(string id, [FromQuery(Name = "vid")] string viewId)
        {
            ViewBag.AssetType = id;
            ViewBag.AssetTypeName = _assetsService.GetAssetNameByCode(id);
            ViewBag.URL = id;
            ViewBag.ViewId = viewId;
            await LoadDropDowns(id);

            
            AssetListRequestDTO assetList = new AssetListRequestDTO();
            assetList.GroupCode = _assetsService.GetAssetNameByCode(id);
           
            _allAssetsModel.AssetListRequest = new AssetListRequestDTO();
            _allAssetsModel.AssetListRequest.GroupCode = ViewBag.AssetTypeName;
            return View("AssetList", _allAssetsModel);
        }

        #region LOAD DROPDOWNS
        public async Task LoadDropDowns(string assetType = "All")
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO
            {
                Type = "Asset Type",
                TypeCode = assetType
            };
            RoadMasterRequestDTO roadMasterReq = new RoadMasterRequestDTO();
            roadMasterReq.DivisionCode = "MIRI";
            ViewData["AssetFeatureId"] = new SelectList(await _roadMasterService.GetRMLookupData(roadMasterReq), "FeatureId", "FeatureId");
            ViewBag.AssetTypeList = await _ddLookupService.GetDdLookup(ddLookup);
           

            ddLookup.Type = "Structure Code";
            ViewBag.StructureCodeList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Parapet Type";
            ViewBag.ParapetList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Asset Group";
            ViewBag.AssetGroupList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Lane Number";
            ViewBag.LaneNumberList = await _ddLookupService.GetLookUpTextDescConcat(ddLookup);
            ddLookup.Type = "Superstructure";
            ViewBag.SuperStructureList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Bearing Type";
            ViewBag.BearingTypeList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Expansion Type";
            ViewBag.ExpansionList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Deck Type";
            ViewBag.DeckTypeList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Abutment Type";
            ViewBag.AbutmentTypeList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Pier Type";
            ViewBag.PierTypeList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Bound";
            ViewBag.BoundList = await _ddLookupService.GetDdDescValue(ddLookup);
            ddLookup.Type = "Abutment Walls, Foundation";
            ViewBag.AbutWallsFoundationList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Piers, Connectiong of primary components";
            ViewBag.PiersConnOfPrimaryComponentsList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Bearing, Bearing Seats, Bearing Diaphgrams";
            ViewBag.BearingSeatsDiaphgramsList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Beams, Girders, Trussess, Arches";
            ViewBag.BeamsGirdersTrussesArchesList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Deck Slab, Pavement";
            ViewBag.DeckSlabPavementList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Signboard, Utilities";
            ViewBag.SignboardUtilitiesList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Waterway";
            ViewBag.WaterwayList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Drain Water Down Pipe, Drainage";
            ViewBag.DrainWaterDownPipeDrainageList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Parapet, Railing";
            ViewBag.ParapetRailingList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Kerb, Sidewalks, Approaches, Approch Slab";
            ViewBag.KerbSidewalksApproachesSlabList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Expansion Joint";
            ViewBag.ExpansionJointList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Slope Protections, Retaining Wall";
            ViewBag.SlopeProtectionRetainingWallList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Inlet Structure";
            ViewBag.InletStrucList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Outlet Structure";
            ViewBag.OutletStrucList = await _ddLookupService.GetDdLookup(ddLookup);

            ddLookup.Type = "Culvert Type";
            ddLookup.TypeCode = "CV";
            ViewBag.CulvertType = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.TypeCode = "";

            ddLookup.Type = "Culvert Material";
            ViewBag.MaterialList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Precast/In-situ";
            ViewBag.PrecastInSuitList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Photo Type";
            //TODO Based on different Assets need to pass DDL_Type_code="BR"
            ViewBag.PhotoTypeBridge = await _ddLookupService.GetDdLookup(ddLookup);
            await LoadDropDownsWOT();
        }

        public async Task LoadDropDownsWOT()
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            ddLookup.Type = "RMU";
            ViewBag.RMUList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "RD_Code";
            ViewBag.RoadCodeList = await _ddLookupService.GetDdLookup(ddLookup);
            ddLookup.Type = "Section Code";
            ViewBag.SectionCodeList = await _ddLookupService.GetLookUpCodeText(ddLookup);
        }
        #endregion 

        [HttpPost]
        public async Task<IActionResult> LoadAssetList(DataTableAjaxPostModel<AssetSearch> assetFilter) 
        {
          
            if (!string.IsNullOrEmpty(Request.Form["columns[0][search][value]"].ToString()))
            {
                if (Request.Form.ContainsKey("columns[1][search][value]"))
                {
                    assetFilter.filterData.SmartInputValue = Request.Form["columns[1][search][value]"].ToString();
                }
                if (Request.Form.ContainsKey("columns[2][search][value]"))
                {
                    if (!string.IsNullOrEmpty(Request.Form["columns[2][search][value]"].ToString()))
                    {

                        string[] locCh = Request.Form["columns[2][search][value]"].ToString().Split('/');

                        if (!string.IsNullOrEmpty(locCh[0].ToString()))
                        {
                            assetFilter.filterData.FromCh = Convert.ToInt32(locCh[0].ToString());
                        }
                        if (!string.IsNullOrEmpty(locCh[1].ToString()))
                        {
                            assetFilter.filterData.FromChDesi = locCh[1].ToString();
                        }


                    }


                }
                if (Request.Form.ContainsKey("columns[3][search][value]"))
                {
                    if (!string.IsNullOrEmpty(Request.Form["columns[3][search][value]"].ToString()))
                    {
                        string[] ToCh = Request.Form["columns[3][search][value]"].ToString().Split('/');
                        if (!string.IsNullOrEmpty(ToCh[0].ToString()))
                        {
                            assetFilter.filterData.ToCh = Convert.ToInt32(ToCh[0].ToString());
                        }
                        if (!string.IsNullOrEmpty(ToCh[1].ToString()))
                        {
                            assetFilter.filterData.ToChDeci = ToCh[1].ToString();
                        }
                    }
                }
                if (Request.Form.ContainsKey("columns[4][search][value]"))
                {
                    assetFilter.filterData.SectionName = Request.Form["columns[4][search][value]"].ToString();
                }
                if (Request.Form.ContainsKey("columns[5][search][value]"))
                {
                    assetFilter.filterData.RoadName = Request.Form["columns[5][search][value]"].ToString();
                }
                if (Request.Form.ContainsKey("columns[6][search][value]"))
                {
                    assetFilter.filterData.GroupCode = Request.Form["columns[6][search][value]"].ToString();
                    assetFilter.filterData.GroupCode = _assetsService.GetAssetCodeByName(assetFilter.filterData.GroupCode);
                }
                if (Request.Form.ContainsKey("columns[7][search][value]"))
                {
                    assetFilter.filterData.GroupType = Request.Form["columns[7][search][value]"].ToString();
                }
                if (Request.Form.ContainsKey("columns[8][search][value]"))
                {
                    assetFilter.filterData.RoadCode = Request.Form["columns[8][search][value]"].ToString();
                }
                if (Request.Form.ContainsKey("columns[9][search][value]"))
                {
                    assetFilter.filterData.RMUName = Request.Form["columns[9][search][value]"].ToString();
                }
                if (Request.Form.ContainsKey("columns[10][search][value]"))
                {
                    assetFilter.filterData.Bound = Request.Form["columns[10][search][value]"].ToString();
                }
                if (Request.Form.ContainsKey("columns[11][search][value]"))
                {
                    if (!string.IsNullOrEmpty(Request.Form["columns[0][search][value]"].ToString()))
                    {
                        assetFilter.filterData.SectionCode = Request.Form["columns[11][search][value]"].ToString();
                    }
                }
            }
            FilteredPagingDefinition<AssetSearch> filteredPagingDefinition = new FilteredPagingDefinition<AssetSearch>();
            
            filteredPagingDefinition.Filters = assetFilter.filterData;
            filteredPagingDefinition.RecordsPerPage = assetFilter.length; 
            filteredPagingDefinition.StartPageNo = assetFilter.start;

            if (assetFilter.order != null)
            {
                filteredPagingDefinition.ColumnIndex = assetFilter.order[0].column;
                filteredPagingDefinition.sortOrder = assetFilter.order[0].SortOrder == SortDirection.Asc ? DTO.Wrappers.SortOrder.Ascending : DTO.Wrappers.SortOrder.Descending;
            }

            var result = await _assetsService.GetFilteredAssets(filteredPagingDefinition).ConfigureAwait(false);

            return Json(new { draw = assetFilter.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }

        [HttpPost]
        //[ValidateModel]//Uncomment this to apply validation
        public async Task<IActionResult> Add(AssetsModel assetModel)
        {
            int rowsAffected = 0;
            int refNo = 0;
            int otherPkId = 0;

            AssetListRequestDTO assetsListRequest = new AssetListRequestDTO();
            assetsListRequest = MultiSelectListToString(assetModel.AddAssetViewModel);
            string ID = _assetsService.GetAssetCodeByName(assetsListRequest.GroupCode);
            assetsListRequest.GroupCode = ID;

            DDLookUpDTO ddLookup = new DDLookUpDTO();
            ddLookup.TypeCode = ID;
            ddLookup.TypeDesc = assetsListRequest.GroupType;
            assetsListRequest.StructureCode = await _ddLookupService.GetDDLValueforTypeAndDesc(ddLookup);
            assetsListRequest.LocationChM = await GetLocChM(assetsListRequest.LocationChM);
            assetsListRequest.FromChDesi = await GetLocChM(assetsListRequest.FromChDesi);
            assetsListRequest.ToChDeci = await GetLocChM(assetsListRequest.ToChDeci);
            assetsListRequest.ModifiedBy = _security.UserID.ToString();
            assetsListRequest.ModifiedDt = DateTime.UtcNow;

            AssetInvOtherReqDto assetInvOtherReqDto = new AssetInvOtherReqDto();
            assetInvOtherReqDto = assetModel.addAssetOtherModel;
            assetInvOtherReqDto.ModifiedBy = _security.UserID.ToString();
            assetInvOtherReqDto.ModifiedDt = DateTime.UtcNow;


            if (assetInvOtherReqDto != null)// support only for particular assets like Bridge, Culvert etc.
            {
                assetInvOtherReqDto.AssetGrpCode = ID;
                if (assetModel.AddAssetViewModel.No == 0)
                {
                    assetsListRequest.CreatedBy = _security.UserID.ToString();
                    assetsListRequest.CreatedDate = DateTime.UtcNow;
                    refNo = await _assetsService.SaveAssetAsync(assetsListRequest);
                    assetInvOtherReqDto.PkRefNo = refNo;
                    assetModel.AddAssetViewModel.No = refNo;
                    assetInvOtherReqDto.CreatedBy = _security.UserID.ToString();
                    assetInvOtherReqDto.CreatedDt = DateTime.UtcNow;
                    otherPkId = await _assetsService.SaveOtherAssetAsync(assetInvOtherReqDto);
                }
                else
                {
                    rowsAffected = await _assetsService.UpdateAssetAsync(assetsListRequest);
                    assetInvOtherReqDto.PkRefNo = assetModel.AddAssetViewModel.No;
                    otherPkId = await _assetsService.SaveOtherAssetAsync(assetInvOtherReqDto);
                }



            }
            else // for other assets like Shoulder,
            {
                if (assetModel.AddAssetViewModel.No == 0)
                {
                    assetsListRequest.CreatedBy = _security.UserID.ToString();
                    assetsListRequest.CreatedDate = DateTime.UtcNow;
                    refNo = await _assetsService.SaveAssetAsync(assetsListRequest);
                    rowsAffected = (refNo != 0) ? rowsAffected = 1 : 0;
                }
                else
                {
                    rowsAffected = await _assetsService.UpdateAssetAsync(assetsListRequest);
                }
            }

            var obj = new
            {
                OtherPkId = otherPkId,
                RefNO = refNo
            };

            return Json(obj);
        }

        #region MultiSelect

        public AssetListRequestDTO MultiSelectListToString(AssetListRequestDTO assetListReq)
        {

            assetListReq.StrucSuper = (assetListReq.StrucSuper_multiSelect != null) ? String.Join(",", assetListReq.StrucSuper_multiSelect) : null;
            assetListReq.ParapetType = (assetListReq.ParapetType_multiSelect != null) ? String.Join(",", assetListReq.ParapetType_multiSelect) : null;
            assetListReq.BearingType = (assetListReq.BearingType_multiSelect != null) ? String.Join(",", assetListReq.BearingType_multiSelect) : null;
            assetListReq.ExpanType = (assetListReq.ExpanType_multiSelect != null) ? String.Join(",", assetListReq.ExpanType_multiSelect) : null;
            assetListReq.DeckType = (assetListReq.DeckType_multiSelect != null) ? String.Join(",", assetListReq.DeckType_multiSelect) : null;

            assetListReq.AbutType = (assetListReq.AbutType_multiSelect != null) ? String.Join(",", assetListReq.AbutType_multiSelect) : null;
            assetListReq.PierType = (assetListReq.PierType_multiSelect != null) ? String.Join(",", assetListReq.PierType_multiSelect) : null;
            assetListReq.AbutFound = (assetListReq.AbutFound_multiSelect != null) ? String.Join(",", assetListReq.AbutFound_multiSelect) : null;
            assetListReq.PiersPrimComp = (assetListReq.PiersPrimComp_multiSelect != null) ? String.Join(",", assetListReq.PiersPrimComp_multiSelect) : null;
            assetListReq.BearingSeatDiaphg = (assetListReq.BearingSeatDiaphg_multiSelect != null) ? String.Join(",", assetListReq.BearingSeatDiaphg_multiSelect) : null;

            assetListReq.BeamsGridTrusArch = (assetListReq.BeamsGridTrusArch_multiSelect != null) ? String.Join(",", assetListReq.BeamsGridTrusArch_multiSelect) : null;
            assetListReq.DeckPavement = (assetListReq.DeckPavement_multiSelect != null) ? String.Join(",", assetListReq.DeckPavement_multiSelect) : null;
            assetListReq.Utilities = (assetListReq.Utilities_multiSelect != null) ? String.Join(",", assetListReq.Utilities_multiSelect) : null;
            assetListReq.Waterway = (assetListReq.Waterway_multiSelect != null) ? String.Join(",", assetListReq.Waterway_multiSelect) : null;
            assetListReq.WaterDownpipe = (assetListReq.WaterDownpipe_multiSelect != null) ? String.Join(",", assetListReq.WaterDownpipe_multiSelect) : null;

            assetListReq.ParapetRailing = (assetListReq.ParapetRailing_multiSelect != null) ? String.Join(",", assetListReq.ParapetRailing_multiSelect) : null;
            assetListReq.SidewalksAppSlab = (assetListReq.SidewalksAppSlab_multiSelect != null) ? String.Join(",", assetListReq.SidewalksAppSlab_multiSelect) : null;
            assetListReq.ExpanJoint = (assetListReq.ExpanJoint_multiSelect != null) ? String.Join(",", assetListReq.ExpanJoint_multiSelect) : null;
            assetListReq.SlopeRetainWall = (assetListReq.SlopeRetainWall_multiSelect != null) ? String.Join(",", assetListReq.SlopeRetainWall_multiSelect) : null;

            assetListReq.InletStruc = (assetListReq.InletStruc_MultiSelect != null) ? String.Join(",", assetListReq.InletStruc_MultiSelect) : null;
            assetListReq.OutletStruc = (assetListReq.OutletStruc_MultiSelect != null) ? String.Join(",", assetListReq.OutletStruc_MultiSelect) : null;

            if (assetListReq.No != 0)
            {
                assetListReq.ActiveYn = true;
            }
            return assetListReq;

        }
     
        public AssetListRequestDTO GetStringToMultiSelectList(AssetListRequestDTO assetMultiSelectList)
        {

            assetMultiSelectList.ParapetType_multiSelect = MultiSelectListItem(assetMultiSelectList.ParapetType);
            assetMultiSelectList.BearingType_multiSelect = MultiSelectListItem(assetMultiSelectList.BearingType);
            assetMultiSelectList.ExpanType_multiSelect = MultiSelectListItem(assetMultiSelectList.ExpanType);
            assetMultiSelectList.DeckType_multiSelect = MultiSelectListItem(assetMultiSelectList.DeckType);
            assetMultiSelectList.AbutType_multiSelect = MultiSelectListItem(assetMultiSelectList.AbutType);
            assetMultiSelectList.AbutType_multiSelect = MultiSelectListItem(assetMultiSelectList.AbutType);
            assetMultiSelectList.AbutFound_multiSelect = MultiSelectListItem(assetMultiSelectList.AbutFound);
            assetMultiSelectList.StrucSuper_multiSelect = MultiSelectListItem(assetMultiSelectList.StrucSuper);
            assetMultiSelectList.PierType_multiSelect = MultiSelectListItem(assetMultiSelectList.PierType);
            assetMultiSelectList.BeamsGridTrusArch_multiSelect = MultiSelectListItem(assetMultiSelectList.BeamsGridTrusArch);
            assetMultiSelectList.PiersPrimComp_multiSelect = MultiSelectListItem(assetMultiSelectList.PiersPrimComp);
            assetMultiSelectList.BearingSeatDiaphg_multiSelect = MultiSelectListItem(assetMultiSelectList.BearingSeatDiaphg);
            assetMultiSelectList.ParapetType_multiSelect = MultiSelectListItem(assetMultiSelectList.ParapetType);
            assetMultiSelectList.DeckPavement_multiSelect = MultiSelectListItem(assetMultiSelectList.DeckPavement);
            assetMultiSelectList.Utilities_multiSelect = MultiSelectListItem(assetMultiSelectList.Utilities);
            assetMultiSelectList.Waterway_multiSelect = MultiSelectListItem(assetMultiSelectList.Waterway);
            assetMultiSelectList.WaterDownpipe_multiSelect = MultiSelectListItem(assetMultiSelectList.WaterDownpipe);
            assetMultiSelectList.ParapetRailing_multiSelect = MultiSelectListItem(assetMultiSelectList.ParapetRailing);
            assetMultiSelectList.SidewalksAppSlab_multiSelect = MultiSelectListItem(assetMultiSelectList.SidewalksAppSlab);
            assetMultiSelectList.ExpanJoint_multiSelect = MultiSelectListItem(assetMultiSelectList.ExpanJoint);
            assetMultiSelectList.SlopeRetainWall_multiSelect = MultiSelectListItem(assetMultiSelectList.SlopeRetainWall);
            assetMultiSelectList.InletStruc_MultiSelect = MultiSelectListItem(assetMultiSelectList.InletStruc);
            assetMultiSelectList.OutletStruc_MultiSelect = MultiSelectListItem(assetMultiSelectList.OutletStruc);
            return assetMultiSelectList;
        }
      
        public List<string> MultiSelectListItem(string assetData)
        {
            List<string> AssetListItem = new List<string>();
            if (!string.IsNullOrEmpty(assetData))
            {
                var AssetList = assetData.Split(',');
                foreach (var Item in AssetList)
                {
                    AssetListItem.Add(Item);
                }
            }
            return AssetListItem;
        }

        #endregion

        #region Edit Assets

        public async Task<IActionResult> AssetEdit(int editId, string assetTypeName)
        {
            AssetsModel assetModel = new AssetsModel();
            await LoadDropDowns(assetTypeName);
            ViewBag.AssetType = assetTypeName;
            ViewBag.AssetId = editId;
            assetModel.AddAssetViewModel = new AssetListRequestDTO();
            assetModel.AssetimageList = new List<ImageListRequestDTO>();
            assetModel.AddAssetViewModel.GroupCode = _assetsService.GetAssetNameByCode(assetTypeName);
            if (editId != 0)
            {
                var assetData = await _assetsService.GetAssetById(editId);
                assetModel.AddAssetViewModel = assetData;
                assetModel.AddAssetViewModel = GetStringToMultiSelectList(assetData);
                assetModel.AddAssetViewModel.GroupCode = _assetsService.GetAssetNameByCode(assetModel.AddAssetViewModel.GroupCode);
              
                var result = await _assetsService.GetOtherAssetByIdAsync(editId);
                assetModel.addAssetOtherModel = result;

                assetModel.AssetimageList = await _assetsService.GetAllImageByAssetPK(editId);
                assetModel.ImageTypeList = assetModel.AssetimageList.Select(c => c.ImageTypeCode).Distinct().ToList();
            }
            return PartialView("~/Views/Assets/_AddAssets.cshtml", assetModel);
        }

        #endregion

        #region Delete Asset and Image

        [HttpPost]
        public async Task<IActionResult> AssetDelete(int assetId)
        {
            int rowsAffected = 0;
            rowsAffected = await _assetsService.DeActivateAssetAsync(assetId);
            return Json(rowsAffected);
        }

        [HttpPost]
        public async Task<IActionResult> AssetDeleteImage(int assetPkId)
        {
            int rowsAffected = 0;
            rowsAffected = await _assetsService.DectivateAssetImage(assetPkId);
            return Json(rowsAffected);
        }

        #endregion

        #region ImportAsset

        [HttpPost]
        public async Task<IActionResult> ReadExcelFile(IFormFile importFile, string assetType, [FromServices] IWebHostEnvironment _environment)
        {
            string tempLocation = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string path = Path.Combine(_environment.WebRootPath, Path.Combine("importFiles"));
            string type = assetType;
            string host = _httpContextAccessor.HttpContext.Request.Path.Value;
            if (assetType == "BR") assetType = "Bridge";
            else if (assetType == "CLM") assetType = "Centre Line Marking";
            else if (assetType == "CV") assetType = "Culvert";
            else if (assetType == "CW") assetType = "Carriageway";
            else if (assetType == "DI") assetType = "Ditches";
            else if (assetType == "DR") assetType = "Drain";
            else if (assetType == "ELM") assetType = "Edge Line Marking";
            else if (assetType == "GR") assetType = "Guardrail";
            else if (assetType == "RS") assetType = "Road Stud";
            else if (assetType == "RW") assetType = "Retaining Wall";
            else if (assetType == "SG") assetType = "Signs";
            else if (assetType == "SH") assetType = "Shoulder";

            try
            {
                if (!Directory.Exists(path + "\\" + tempLocation))
                {
                    Directory.CreateDirectory(path + "\\" + tempLocation);
                }
                string fileName = path + "\\" + tempLocation + "\\" + importFile.FileName;
                FileInfo fileInfo = new FileInfo(fileName);
                if (IsFileLocked(fileInfo))
                {
                    using (FileStream stream = System.IO.File.Create(fileName))
                    {
                        importFile.CopyTo(stream);
                        stream.Flush();
                    }
                }

                string _filename = await Extractfile(fileName, importFile.FileName, assetType);
                string d = "";

                if (_filename != "0,0, Excel file Is missing in uploaded file" && _filename != "0,0, Incorrect template")
                {
                    Task<DataTable> res = GetAssetListImport(_filename, assetType, type);
                    res.Wait();
                    var data = res.Result;
                    string Exportlocation = _webhostEnvironment.WebRootPath + _configuration.GetValue<string>("FileUploadLocation");
                    string localLocation = path + "\\" + tempLocation;
                    if (data != null && data.Rows.Count > 0 && data.Rows.Count < 10000)
                    {
                        (string, List<SelectListItem>) asset = await _assetsService.SaveAssetImport(data, Exportlocation, localLocation, tempLocation, _security.UserID);
                       
                        d = asset.Item1;
                        List<SelectListItem> errorList = new List<SelectListItem>();
                        errorList = asset.Item2;
                        if (d != "Failed")
                        {
                            List<AssetImport> invalid = await _assetsService.GetAssetImports(Guid.Parse(data.Rows[0]["AI_File_No"].ToString()));
                            if ((invalid != null && invalid.Count > 0) || errorList.Count() > 0)
                            {
                                string _ErrorFilename = await ErrorExcelfileName(_filename, data, invalid, assetType, errorList);
                                var obj1 = new
                                {
                                    Status = 1,
                                    payload = _ErrorFilename,
                                    errorMessage = d,
                                };
                                return Ok(obj1);
                            }
                        }
                        else
                        {
                            List<AssetImport> invalid = await _assetsService.GetAssetImports(Guid.Parse(data.Rows[0]["AI_File_No"].ToString()));
                            if (invalid != null && invalid.Count > 0)
                            {
                                string _ErrorFilename = await ErrorExcelfileName(_filename, data, invalid, assetType, errorList);
                                var obj1 = new
                                {
                                    Status = 1,
                                    payload = _ErrorFilename,
                                };
                                return Ok(obj1);
                            }

                        }

                    }
                    else if (data.Rows.Count >= 10000)
                    {
                        var obj1 = new
                        {
                            Status = 0,
                            payload = "0,0,0,0, An error has occured: The Bulk upload functionality supports upload/update upto 9999 records. We request you to remove additional records from the template and re-upload the template.",
                        };
                        return Ok(obj1);
                    }
                    else
                    {
                        var obj1 = new
                        {
                            Status = 0,
                            payload = "0,0,0,0, No Valid Data in excel file",
                        };
                        return Ok(obj1);

                    }
                   
                    Console.WriteLine($"{"RecordsAffectedBit===============================>" + d}");
                }
                else
                {
                    d = _filename;
                }
                var obj = new
                {
                    Status = 0,
                    payload = d,
                };
                return Ok(obj);
            }
          catch (Exception ex)
            {
                var obj = new
                {
                    Status = 0,
                    payload = ex.Message,
                };
                return Ok(obj);
            }
        }

        protected virtual bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {

                return true;
            }


            return false;
        }

        public async Task<string> ErrorExcelfileName(string _filename, DataTable data, List<AssetImport> invalid, string assetType, List<SelectListItem> errorList)
        {
            string filename = "";
            try
            {
                filename = await ErrorExcelfileGenerator(_filename, invalid, data, assetType, errorList);
                return filename;
            }
            catch (Exception ex)
            {
                return filename;
            }
        }

        public async Task<string> ErrorExcelfileGenerator(string filename, List<AssetImport> invalid, DataTable dt, string assetType, List<SelectListItem> errorList)
        {
            try
            {
               
                using (var fstream = System.IO.File.Open(filename, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(fstream))
                    {
                        var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                        {
                            ConfigureDataTable = _ => new ExcelDataTableConfiguration
                            {
                                UseHeaderRow = true // To set First Row As Column Names  
                            }
                        });
                        dt = dataSet.Tables[0];
                    }
                }
                int k = 0;
                foreach (DataRow row in dt.Rows)
                {
                    k = k + 1;
                    row["No"] = k;
                }
               
                FileInfo file = new FileInfo(filename);

                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string Outputfilename = file.FullName.Replace(file.Extension, "_Error" + file.Extension);

                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("sheet1");
                    int i = 1;
                    foreach (DataColumn hdr in dt.Columns)
                    {
                        worksheet.Cell(1, i).Value = hdr.ColumnName;
                        i = i + 1;
                        if (hdr.ColumnName == dt.Columns[dt.Columns.Count - 1].ColumnName)
                        {
                            worksheet.Cell(1, dt.Columns.Count + 1).Value = "Error Description";
                        }
                    }

                    int j = 2;
                    foreach (DataRow data in dt.Rows)
                    {
                       

                        if (invalid.Where(x => x.AiSrno == Convert.ToInt32(data["No"])).Count() > 0)
                        {

                            if (invalid.Where(x => x.AiSrno == Convert.ToInt32(data["No"])).FirstOrDefault().AiImportErrorDesc != null)
                            {
                                for (int x = 0; x < data.ItemArray.Count(); x++)
                                {
                                    worksheet.Cell(j, x + 1).Value = data.ItemArray[x].ToString();
                                }
                                SelectListItem imgError = new SelectListItem();
                                if (errorList != null)
                                {
                                    imgError = errorList.Where(x => x.Value == data["No"].ToString()).FirstOrDefault();
                                }
                                if (imgError != null)
                                {
                                    if (imgError.Text != null)
                                    {
                                        worksheet.Cell(j, data.ItemArray.Count() + 1).Value = (invalid.Where(x => x.AiSrno == Convert.ToInt32(data["No"])).FirstOrDefault().AiImportErrorDesc) + " , " + imgError.Text.ToString();
                                    }
                                    else
                                    {
                                        worksheet.Cell(j, data.ItemArray.Count() + 1).Value = (invalid.Where(x => x.AiSrno == Convert.ToInt32(data["No"])).FirstOrDefault().AiImportErrorDesc)+" Error In Importing the record";
                                    }
                                }
                                else
                                {
                                    worksheet.Cell(j, data.ItemArray.Count() + 1).Value = invalid.Where(x => x.AiSrno == Convert.ToInt32(data["No"])).FirstOrDefault().AiImportErrorDesc;
                                }
                                j = j + 1;                            
                            }

                        }
                        else if (errorList.Count() > 0)
                        {
                            var imgErrorList = errorList.Where(x => x.Value == data["No"].ToString()).FirstOrDefault();
                            if (imgErrorList != null)
                            {
                                if (imgErrorList.Text != null)
                                {
                                    for (int x = 0; x < data.ItemArray.Count(); x++)
                                    {
                                        worksheet.Cell(j, x + 1).Value = data.ItemArray[x].ToString();
                                    }
                                    if (invalid.Where(x => x.AiSrno == Convert.ToInt32(data["No"])).Count() > 0)
                                    {
                                        if (invalid.Where(x => x.AiSrno == Convert.ToInt32(data["No"])).FirstOrDefault().AiImportErrorDesc != null)
                                        {
                                            worksheet.Cell(j, data.ItemArray.Count() + 1).Value = (invalid.Where(x => x.AiSrno == Convert.ToInt32(data["No"])).FirstOrDefault().AiImportErrorDesc) + " , " + imgErrorList.Text.ToString();
                                        }
                                    }
                                    else
                                    {
                                        worksheet.Cell(j, data.ItemArray.Count() + 1).Value = imgErrorList.Text.ToString();
                                    }

                                    j = j + 1;
                                }
                            }
                        }
                    }

                    workbook.SaveAs(Outputfilename);
                    return Outputfilename;

                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public async Task<IActionResult> ReadExcelErrorFile(string fileID)
        {
            try
            {
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string ErrorfileName = fileID;
                byte[] content = System.IO.File.ReadAllBytes(fileID);
                return File(content, contentType, "Errorfile.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public Byte[] ExportErrorfile(string filename, int count)
        {
            try
            {
                DataTable dt = new DataTable();
                using (var fstream = System.IO.File.Open(filename, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(fstream))
                    {
                        var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                        {
                            ConfigureDataTable = _ => new ExcelDataTableConfiguration
                            {
                                UseHeaderRow = true // To set First Row As Column Names  
                            }
                        });
                        dt = dataSet.Tables[0];
                    }
                }


                FileInfo file = new FileInfo(filename);

                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string Outputfilename = file.FullName.Replace(file.Extension, "_Error" + file.Extension);

                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("sheet1");
                    int i = 1;
                    foreach (DataColumn hdr in dt.Columns)
                    {
                        worksheet.Cell(1, i).Value = hdr.ColumnName;
                        i = i + 1;
                        if (hdr.ColumnName == dt.Columns[dt.Columns.Count - 1].ColumnName)
                        {
                            worksheet.Cell(1, dt.Columns.Count + 1).Value = "Error Description";
                        }
                    }

                    int j = 2;
                    foreach (DataRow data in dt.Rows)
                    {
                        
                        if ((data["Asset Id"].ToString() != string.Empty) &&
                            (!data["Asset Id"].ToString().Split('/')[2].ToString().Contains("MRI")
                            || !data["Asset Id"].ToString().Split('/')[2].ToString().Contains("BTN")) ||
                            data["Asset Id"].ToString() == "" ||
                            data["Asset Id"].ToString().Split('/').Count() < 7 ||
                            data["Asset Id"].ToString().Contains("//") ||
                            data["Road Feature ID"].ToString() == "")
                        {
                            for (int x = 0; x < data.ItemArray.Count(); x++)
                            {
                                worksheet.Cell(j, x + 1).Value = data.ItemArray[x].ToString();
                            }

                            if ((data["Asset Id"].ToString() == "" || data["Asset Id"].ToString().Split('/').Count() < 7) && data["Road Feature ID"].ToString() == "")
                            {
                                worksheet.Cell(j, data.ItemArray.Count() + 1).Value = "Feature ID ,Asset Id Missing";
                              
                            }
                            else if (data["Road Feature ID"].ToString() == "")
                            {
                                worksheet.Cell(j, data.ItemArray.Count() + 1).Value = "Feature ID Missing";
                               
                            }
                            else if (data["Asset Id"].ToString() == "" || data["Asset Id"].ToString().Split('/').Count() < 7)
                            {
                                worksheet.Cell(j, data.ItemArray.Count() + 1).Value = "Asset Id Missing";
                               
                            }
                            else if (!data["Asset Id"].ToString().Split('/')[2].ToString().Contains("MRI")
                            || !data["Asset Id"].ToString().Split('/')[2].ToString().Contains("BTN") || data["Asset Id"].ToString().Contains("//"))
                            {
                                worksheet.Cell(j, data.ItemArray.Count() + 1).Value = "Asset Id is not Valid";
                              
                            }

                            j = j + 1;
                        }

                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return content;
                    }


                }
            }
            catch (Exception ex)
            {
                using (var workbook = new XLWorkbook())
                {
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return content;
                    }
                }
            }
        }

        private async Task<string[]> GetAllfields()
        {
            string[] fieldlist = { "AI_Asset_ID","AI_Div_Code","AI_Dist","AI_RMU_Code","AI_Sec_Code","AI_Sec_name","AI_Rd_Code","AI_Rd_name","AI_Asset_GRP_Code","AI_Grp_type",
                                  "AI_LOC_CH_KM","AI_LOC_CH_M","AI_Bound","AI_Struc_Code","AI_Ref_No","AI_Feature_ID","AI_Diameter","AI_Width","AI_Height","AI_Material","AI_Fin_Rd_level",
                                  "AI_Catch_Area","AI_Skew","AI_Design_Flow","AI_Length","AI_Precast_Situ","AI_Barrel_No","AI_Intel_Level","AI_Intel_Struc","AI_Outlet_Level","AI_Outlet_Struc","AI_Owner","AI_maintained_By",
                                  "AI_GPS_Easting","AI_GPS_Northing","AI_River_Name","AI_Width_Lane","AI_length_Span","AI_Bridge_Name","AI_Lane_Cnt","AI_Span_Cnt","AI_median","Ai_Walkway","AI_Struc_Super",
                                  "AI_Parapet_Type","AI_Bearing_Type","AI_Expand_Type","AI_Abut_Type","AI_Deck_Type","AI_Pier_Type","AI_Expan_Joint_Count","AI_Expan_Joint_Space","AI_Abut_Found","AI_Bearing_Seat_Diaphg",
                                  "AI_Beams_Grid_Trus_Arch","AI_Deck_pavement","AI_Utilities","AI_Waterway","AI_Water_Downpipe","AI_Parapet_Railing","AI_Sidewalks_App_Slab","AI_Expan_Joint","AI_Slope_Retain_wall",
                                  "AI_Built_year","AI_FRM_CH","AI_FRM_CH_Deci","AI_To_CH","AI_To_CH_Deci","AI_Lane_No","AI_Post_Spacing","AI_Tier","AI_Bot_Width","AIO_Asset_ID","AIO_AssetGRP_Code","AIO_Struc_Code_Others",
                                  "AIO_Abut_Found_Others","AIO_Piers_Prim_Comp_Others","AIO_Material_Others","AIO_Bearing_Seat_Diaphf_Others","AIO_Beams_Grid_trus_Arch_Others","AIO_Deck_pavement_Others","AIO_Utilities_Others",
                                  "AIO_Waterway_Others","AIO_Water_Downpipe_Others","AIO_Parapet_RAIOling_Others","AIO_Sidewalks_App_Slab_Others","AIO_Expan_Joint_Others","AIO_Slope_RetAIO_wall_Others","RM_Allasset_inv_Others",
                                  "AI_Has_Image","AI_Piers_Prim_Comp"};
            return fieldlist;
        }

        private async Task<DataTable> GetAssetListImport(string fname, string assetType, string type)
        {
            DataTable dataTable = new DataTable();
            var filename = fname;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            FileInfo info = new FileInfo(filename);
            if (!IsFileLocked(info))
            {
                using (var fstream = System.IO.File.Open(filename, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(fstream))
                    {
                        var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                        {
                            ConfigureDataTable = _ => new ExcelDataTableConfiguration
                            {
                                UseHeaderRow = true // To set First Row As Column Names  
                            }
                        });

                        dataTable = dataSet.Tables[0];

                        List<AssetFieldDtlResDTO> assetFieldRes = new List<AssetFieldDtlResDTO>();
                        AssetFieldDtlReqDTO assetFieldDtlReq = new AssetFieldDtlReqDTO();
                        assetFieldDtlReq.Code = _assetsService.GetAssetCodeByName(type);
                        assetFieldRes = await _assetsService.GetAssetFieldByCode(assetFieldDtlReq);
                        assetFieldRes = assetFieldRes.Where(x => x.AssetType == assetType + "_Upload").ToList();

                        string columnlist = "";

                        foreach (AssetFieldDtlResDTO assetField in assetFieldRes)
                        {
                            if (dataTable.Columns.Contains(assetField.DisplayName))
                            {
                                dataTable.Columns[assetField.DisplayName].ColumnName = assetField.FieldName;
                            }
                           
                            else
                            {
                                dataTable.Columns.Add(assetField.FieldName);
                            }
                        }


                        dataTable.Columns.Add("AI_File_No", typeof(Guid));

                        dataTable.Columns.Add("AI_FRM_CH_Deci");
                        dataTable.Columns.Add("AI_To_CH_Deci");
                        dataTable.Columns.Add("AI_Asset_ID_actual");
                        Guid AI_File_No = Guid.NewGuid();

                        int i = 0;
                        foreach (DataRow row in dataTable.Rows)
                        {
                            i = i + 1;
                            row["AI_SRNO"] = i;
                            row["AI_File_No"] = AI_File_No;
                            row["AI_Asset_Grp_Code"] = assetFieldDtlReq.Code;
                            if (assetType != "Culvert" && assetType != "Bridge")
                            {
                                await UpdateRowValues(row, "AI_FRM_CH", "AI_FRM_CH_Deci");
                                await UpdateRowValues(row, "AI_To_CH", "AI_To_CH_Deci");
                            }
                            if (row["AI_Asset_ID"].ToString() != "" && row["AI_Asset_ID"].ToString().Contains("/"))
                            {
                                string[] AI_Asset_ID = row["AI_Asset_ID"].ToString().Split('/');


                                row["AI_Loc_CH_KM"] = row["Location CH"];
                                await UpdateRowValues(row, "AI_Loc_CH_KM", "AI_Loc_CH_M");

                                
                                if (AI_Asset_ID.Count() >= 7)
                                {
                                    if (row["Location CH"].ToString() != "")
                                    {
                                        decimal dLoc = Convert.ToDecimal(row["Location CH"]);
                                        string[] AILoc = dLoc.ToString("0.#####").Split('.');
                                        Regex regex = new Regex("^[0-9]*$");
                                        bool isSpecialChar = regex.IsMatch(AILoc[0]);//Method to Cross check any special char in Loc Km
                                        if (AILoc.Count() > 0 && AILoc[0] != "" && isSpecialChar)
                                        {
                                            if (AI_Asset_ID[5].ToString() != "")
                                            {
                                                decimal dAssetIdLoc = Convert.ToDecimal(AI_Asset_ID[5]);
                                                string[] assetLoc = dAssetIdLoc.ToString("0.#####").Split('.');
                                                isSpecialChar = regex.IsMatch(assetLoc[0]);
                                                string assetId = AI_Asset_ID[0] + "/" + AI_Asset_ID[1] + "/" + AI_Asset_ID[2] + "/" + AI_Asset_ID[3] + "/" + AI_Asset_ID[4] + "/" + row["AI_Loc_CH_KM"] + "." + row["AI_Loc_CH_M"] + "/" + AI_Asset_ID[6];

                                                //Condition to Check Loc KM & M Matches Loc in Asset Id only then Asset ID will be updated with  row["AI_Loc_CH_KM"] + "." + row["AI_Loc_CH_M"]
                                                if (assetLoc.Count() > 1 && AILoc.Count() > 1 && isSpecialChar)
                                                {
                                                    if (AILoc[0] == assetLoc[0] && AILoc[1] == assetLoc[1].ToString())
                                                    {
                                                        row["AI_Asset_ID_actual"] = row["AI_Asset_ID"];
                                                        row["AI_Asset_ID"] = assetId;
                                                        if (AI_Asset_ID.Count() == 8 && (assetFieldDtlReq.Code == "CW" || assetFieldDtlReq.Code == "SG"))
                                                        {
                                                            row["AI_Asset_ID"] = assetId + "/" + AI_Asset_ID[7];
                                                        }
                                                    }
                                                }
                                                else if (AILoc[0] == assetLoc[0] && isSpecialChar)
                                                {
                                                    row["AI_Asset_ID_actual"] = row["AI_Asset_ID"];
                                                    row["AI_Asset_ID"] = assetId;
                                                    if (AI_Asset_ID.Count() == 8 && (assetFieldDtlReq.Code == "CW" || assetFieldDtlReq.Code == "SG"))
                                                    {
                                                        row["AI_Asset_ID"] = assetId + "/" + AI_Asset_ID[7];
                                                    }
                                                }
                                            }

                                        }
                                    }
                                    
                                    row["AI_Div_Code"] = row["AI_Feature_ID"].ToString().Split('/')[0].ToString();
                                    
                                }
                            }
                        }

                        if (dataTable != null && dataTable.Rows.Count >= 1)
                        {
                            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("RAMMSDatabase"));
                            //create object of SqlBulkCopy which help to insert  
                            SqlBulkCopy objbulk = new SqlBulkCopy(con);
                            //assign destination table name  
                            objbulk.DestinationTableName = "Asset_import";
                            foreach (AssetFieldDtlResDTO assetField in assetFieldRes)
                            {
                                objbulk.ColumnMappings.Add(assetField.FieldName, assetField.FieldName);
                            }
                            objbulk.ColumnMappings.Add("AI_File_No", "AI_File_No");
                            objbulk.ColumnMappings.Add("AI_FRM_CH_Deci", "AI_FRM_CH_Deci");
                            objbulk.ColumnMappings.Add("AI_To_CH_Deci", "AI_To_CH_Deci");
                            objbulk.ColumnMappings.Add("AI_Asset_ID_actual", "AI_Asset_ID_actual");
                            try
                            {
                                con.Open();
                                //insert bulk Records into DataBase.  
                                objbulk.WriteToServer(dataTable);
                                con.Close();
                            }
                            catch (Exception ex)
                            {
                                throw ex;

                            }
                        }

                        dataTable.AcceptChanges();


                    }
                }
            }
            return dataTable;
        }

        private async Task UpdateRowValues(DataRow row, string rowName, string rowName1)
        {
            if (!row[rowName].ToString().Contains('.') && row[rowName].ToString() != "")
            {
                if (row[rowName].ToString() != "")
                {
                    row[rowName] = row[rowName];
                }
                else
                {
                    row[rowName] = 0;
                }
                row[rowName1] = "000";
            }
            else if (row[rowName].ToString() != "")
            {
                string[] CH_From = row[rowName].ToString().Split(".");
                if (CH_From.Count() > 1)
                {
                    row[rowName] = CH_From[0];
                    row[rowName1] = await GetLocChM(CH_From[1]);
                }
                else
                {
                    row[rowName] = CH_From[0];
                    row[rowName1] = "000";
                }
                if (CH_From[0] == "")
                {
                    row[rowName] = 0;
                }
            }

        }

        private async Task<string> GetLocChM(string locM)
        {

            if (locM != null)
            {
                if (locM.Length == 3)
                    locM = locM.ToString();
                else if (locM.Length == 2)
                    locM = locM.ToString() + "0";
                else if (locM.Length == 1)
                    locM = locM.ToString() + "00";
                else if (locM.Length == 0)
                    locM = locM.ToString() + "000";
                
            }
            return locM;
        }

        private async Task<string> Extractfile(string filename, string folderName, string assetType)
        {
            string Excelfilename = "";
            try
            {
                Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(filename);
                if (!Directory.Exists(filename.Replace(folderName, "")))
                {
                    Directory.CreateDirectory(filename.Replace(folderName, ""));
                }
                foreach (ZipEntry e in zip)
                {
                    
                    e.Extract(filename.Replace(folderName, ""), ExtractExistingFileAction.OverwriteSilently);
                    if (e.FileName.StartsWith(assetType))
                    {
                        if (e.FileName.EndsWith(".xlsx") || e.FileName.EndsWith(".xls"))
                        {
                            Excelfilename = e.FileName.Replace("/", @"\");
                        }
                    }
                }

                zip.Dispose();

                if (Excelfilename != null && Excelfilename != "")
                    Excelfilename = filename.Replace(folderName, "") + Excelfilename.ToString();
                else
                    Excelfilename = "0,0, Incorrect template";


            }
            catch (Exception ex)
            {
                Excelfilename = "0,0, Excel file Is missing in uploaded file";
            }
            return Excelfilename;
        }

        #endregion ImportAsset

        [HttpPost]
        public async Task<IActionResult> GetAssetDataId(AssetListRequestDTO assetsData)
        {
            RoadMasterRequestDTO _Rmroad = new RoadMasterRequestDTO();
            string groupType = "";
            string locationCH = "";
            int pkNo = 0;
            _Rmroad.FeatureId = assetsData.FeatureId;
            var _rmallData = await _roadMasterService.GetRMAllData(_Rmroad);

            DDLookUpDTO ddLookup = new DDLookUpDTO();
            assetsData.GroupCode = _assetsService.GetAssetCodeByName(assetsData.GroupCode);
            ddLookup.TypeCode = assetsData.GroupCode;
            ddLookup.TypeDesc = assetsData.GroupType;
            if (ddLookup.TypeDesc != null)
            {
                groupType = await _ddLookupService.GetDDLValueforTypeAndDesc(ddLookup);

            }

            if (assetsData.FeatureId != null)
            {
                string locM = assetsData.LocationChM;
                locM = await GetLocChM(locM);
                if (assetsData.LocationChKm.HasValue && locM != null)
                {
                    locationCH = assetsData.LocationChKm.ToString() + "." + locM;
                }
                if (assetsData.LocationChKm.HasValue && locM != null)
                {
                    locationCH = assetsData.LocationChKm.ToString() + "." + locM;
                }
                if (assetsData.LocationChKm.HasValue && assetsData.LocationChM != null)
                {
                    locationCH = assetsData.LocationChKm.ToString() + "." + locM;
                }
                else if (assetsData.LocationChKm.HasValue)
                {
                    locationCH = assetsData.LocationChKm.ToString();
                }
                else
                {
                    locationCH = assetsData.LocationChM;
                }

                var autoGenId = assetsData.GroupCode + "/" + groupType + "/" + _rmallData.RmuCode + "/" + _rmallData.SecCode + "/" + _rmallData.RoadCode + "/" + locationCH + "/" + assetsData.Bound;
                if (assetsData.AssetNumber != null)
                {
                    autoGenId = autoGenId + "/" + assetsData.AssetNumber;
                }
                if (assetsData.GroupCode == "CW" && assetsData.LaneNo != null && assetsData.LaneNo != "")
                {
                    autoGenId = autoGenId + "/" + assetsData.LaneNo;
                }
                _rmallData.AssetId = "";
                _rmallData.AssetId = autoGenId;
                pkNo = await _assetsService.CheckAssetRefNo(autoGenId);
            }
            var obj = new
            {
                _RMAllData = _rmallData,
                pkNo = pkNo
            };
            return Json(obj);
        }

        #region Asset Excel Download

        [HttpPost]
        public async Task<IActionResult> Downloadgridresults(AssetSearch assetReqData)
        {
            var hostedUrl = Request.Scheme.ToString() + "://" + Request.Host.ToString();
            FilteredPagingDefinition<AssetSearch> filteredPagingDefinition = new FilteredPagingDefinition<AssetSearch>();
            List<AssetsListResponseDTO> assetsListResponseDTO = new List<AssetsListResponseDTO>();
            AssetFieldDtlReqDTO assetFieldDtlReq = new AssetFieldDtlReqDTO();
            List<AssetFieldDtlResDTO> assetFieldRes = new List<AssetFieldDtlResDTO>();
            string worksheetTitle = "";

            //Data to Export
            filteredPagingDefinition.Filters = assetReqData;
            filteredPagingDefinition.RecordsPerPage = 1000;
            filteredPagingDefinition.StartPageNo = 0;

            var result = await _assetsService.GetFilteredAssets(filteredPagingDefinition);
            assetsListResponseDTO = result.PageResult.ToList();


            //Heading for Excel
            assetFieldDtlReq.Code = assetReqData.GroupCode;
            assetFieldRes = await _assetsService.GetAssetFieldByCode(assetFieldDtlReq);
            var GroupName = _assetsService.GetAssetNameByCode(assetFieldDtlReq.Code);
            assetFieldRes = assetFieldRes.Where(x => x.AssetType == GroupName + "_Download").ToList();

            List<AssetImageDtlDTO> AssetRefNoList = new List<AssetImageDtlDTO>();
            AssetRefNoList = await _assetsService.GetAssetImageDtls();
            //Image path update
            assetsListResponseDTO = assetsListResponseDTO.Select(c => { if (!string.IsNullOrEmpty(c.AssetId)) { c.AssetImgPath = hostedUrl + @"/assets/download/" + c.AssetId.Replace(@"/", "_").Replace(@"+", "_"); } return c; }).ToList();
            
            worksheetTitle = assetFieldDtlReq.Code + DateTime.Now.ToString("_ddMMyyyy_HHmmssfffff");
            //Export to Excel Save data in Server pass Excel Name as Response

            worksheetTitle = await ExportToExcel(assetsListResponseDTO, assetFieldRes, worksheetTitle, assetReqData.AssetImageType, AssetRefNoList);


            return Json(worksheetTitle);

        }

        public async Task<string> ExportToExcel(List<AssetsListResponseDTO> assetsListResponseDTO, List<AssetFieldDtlResDTO> assetFieldDtlRes, string worksheetTitle, 
            string imgType, List<AssetImageDtlDTO> assetRefNoList)
        {
            var wb = new XLWorkbook(); //create workbook
            var ws = wb.Worksheets.Add(worksheetTitle); //add worksheet to workbook
            int cell = 0;
            bool IsImagedownload = false;
            for (int i = 0; i < assetFieldDtlRes.Count; i++)
            {         
                ws.Cell(1, cell + 1).Value = assetFieldDtlRes[i].DisplayName;
                cell++;             
            }

            if (imgType != "WOI")
            {
                IsImagedownload = true;
            }

            var titlesStyle = wb.Style;
            titlesStyle.Font.Bold = true;

            if (imgType == "WOI")
            {
               
                var foundImagePath = ws.Search("Image Path", System.Globalization.CompareOptions.OrdinalIgnoreCase);
                var monthRow = foundImagePath.Last().Address;
                ws.Column(monthRow.ColumnNumber).Clear();
            }
            else
            {
                ws.Column(82).Unhide();
            }

            if (assetsListResponseDTO != null && assetsListResponseDTO.Count() > 0)
            {
                Type _type = Type.GetType("RAMMS.Web.UI.Models.ExportExcel");
                PropertyInfo[] propertyInfos = _type.GetProperties();
                int rownumber = 1;
                for (int i = 0; i < assetsListResponseDTO.Count; i++)
                {
                    if (i == 0)
                    {
                        rownumber = i + 1;
                    }
                    int cel = 0;
                    for (int j = 0; j < assetFieldDtlRes.Count; j++)
                    {
                        foreach (PropertyInfo _propertyInfo in propertyInfos)
                        {
                            if (_propertyInfo.Name.ToLower() == assetFieldDtlRes[j].FieldName.Replace("AI_", "").Replace("_", "").ToLower())
                            {
                                if (assetsListResponseDTO[i].GetType().GetProperty(_propertyInfo.Name) != null && assetsListResponseDTO[i].GetType().GetProperty(_propertyInfo.Name).GetValue(assetsListResponseDTO[i], null) != null)
                                {
                                    if (_propertyInfo.Name != "AssetImgPath")
                                    {
                                        ws.Cell(rownumber + 1, cel + 1).Value = assetsListResponseDTO[i].GetType().GetProperty(_propertyInfo.Name).GetValue(assetsListResponseDTO[i], null).ToString();
                                        cel++;
                                    }
                                    else if (_propertyInfo.Name == "AssetImgPath" && imgType != "WOI")
                                    {
                                        bool hasRefNo = assetRefNoList.Any(c => c.No == assetsListResponseDTO[i].No);
                                        if (hasRefNo)
                                        {
                                            ws.Cell(rownumber + 1, cel + 1).Value = assetsListResponseDTO[i].GetType().GetProperty(_propertyInfo.Name).GetValue(assetsListResponseDTO[i], null).ToString();
                                            ws.Cell(rownumber + 1, cel + 1).Hyperlink = new XLHyperlink(ws.Cell(rownumber + 1, cel + 1).Value.ToString());
                                            cel++;
                                        }
                                        else
                                        {
                                            ws.Cell(rownumber + 1, cel + 1).Value = "";
                                            cel++;
                                        }

                                    }
                                }

                                else
                                {
                                    ws.Cell(rownumber + 1, cel + 1).Value = "";
                                    cel++;
                                }
                            }
                        }
                        ws.Cell(rownumber + 1, 1).Value = rownumber;
                    }
                    rownumber = rownumber + 1;
                }
            }

            if (IsImagedownload)
            {
                string tempName = DateTime.Now.ToString("yyyyMMddHHmmssfff");

                if (!Directory.Exists(_webhostEnvironment.WebRootPath + "\\" + _configuration.GetValue<string>("FileUploadLocation") + "\\" + tempName))
                {
                    Directory.CreateDirectory(_webhostEnvironment.WebRootPath + "\\" + _configuration.GetValue<string>("FileUploadLocation") + "\\" + tempName);
                }

                using (var ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    wb.SaveAs(_webhostEnvironment.WebRootPath + "\\" + _configuration.GetValue<string>("FileUploadLocation") + "\\" + tempName + "\\" + worksheetTitle + ".xlsx");

                }

                string path = _webhostEnvironment.WebRootPath + "\\" + _configuration.GetValue<string>("FileUploadLocation");
                string filename = await Converttozip(assetsListResponseDTO, path, tempName);

                return filename;

            }


            using (var ms = new MemoryStream())
            {
                wb.SaveAs(ms);
                wb.SaveAs(worksheetTitle + ".xlsx");
                return worksheetTitle;
            }
        }

        public async Task<string> Converttozip(List<AssetsListResponseDTO> assetsListResponseDTO, string fileLocation, string tempName)
        {
            string zipname = "";
            string tempLocation = Path.Combine(fileLocation.Replace("\\\\", "\\"), tempName);
            string fileLocation_rep = fileLocation.Replace("\\\\", "\\");

            try
            {
                if (!Directory.Exists(tempLocation))
                {
                    Directory.CreateDirectory(tempLocation);
                }

                foreach (AssetsListResponseDTO assets in assetsListResponseDTO)
                {
                    if (assets.No != null && assets.No != 0)
                    {
                        List<RmAssetImageDtl> assetImageDtls = await _assetsService.GetImageDTLByAssetId(assets.No);
                        foreach (RmAssetImageDtl imageDtl in assetImageDtls)
                        {
                            if (!Directory.Exists(tempLocation + "\\" + imageDtl.AidImageFilenameUpload.Replace(imageDtl.AidImageUserFilePath, "")))
                            {
                                Directory.CreateDirectory(tempLocation + "\\" + imageDtl.AidImageFilenameUpload.Replace(imageDtl.AidImageUserFilePath, ""));
                            }
                            if (!System.IO.File.Exists(tempLocation + "\\" + imageDtl.AidImageFilenameUpload))
                            {
                                FilemovetoUploadLocation(fileLocation_rep + "\\" + imageDtl.AidImageFilenameUpload, tempLocation + "\\" + imageDtl.AidImageFilenameUpload);
                            }

                        }

                        using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile
                        {
                            CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression

                        })
                        {
                            var files = Directory.GetFiles(tempLocation, "*",
                                SearchOption.AllDirectories).
                                Where(f => Path.GetExtension(f).
                                    ToLowerInvariant() != ".zip").ToArray();

                            foreach (var f in files)
                            {
                                zip.AddFile(f,
                                    Path.GetDirectoryName(f).
                                    Replace(tempLocation, string.Empty));
                            }
                            zipname = tempLocation + "\\" + tempName + ".zip";
                            zip.Save(zipname);
                        }
                    }

                }
            }

            catch (Exception ex)
            {

            }
            return zipname;
        }

        private static byte[] data;

        static void FilemovetoUploadLocation(string source, string destination)
        {
            if (System.IO.File.Exists(source))
            {
                using (var stmr = System.IO.File.Open(source, FileMode.Open)) // open a file using filestream.
                {
                    int size = (int)stmr.Length; // size of the file.
                    data = new byte[size];
                    int totalbyte = 0;
                    while (size > 0) // loop until file' size is not 0.
                    {
                        int read = stmr.Read(data, totalbyte, size); // reading file's data.
                        size -= read;
                        totalbyte += read;
                    }
                    using (var stm = System.IO.File.Create(destination)) // create a new file.
                    {
                        byte[] bytes = data;
                        stm.Write(data, 0, data.Length); // writing data into created file.
                        stm.Flush();
                    }
                }
            }


        }

        public IActionResult DowloadExcel(string id)
        {
            FileContentResult result;
            if (id.ToLower().Contains(".zip"))
            {
                HttpContext.Response.ContentType = "application/zip";
                FileInfo file = new FileInfo(id);
                result = new FileContentResult(System.IO.File.ReadAllBytes(id), HttpContext.Response.ContentType)
                {
                    FileDownloadName = file.Name
                };
                if (System.IO.Directory.Exists(file.DirectoryName))
                {
                    Directory.Delete(file.DirectoryName, true);
                }

            }
            else
            {
                id = id + ".xlsx";
                HttpContext.Response.ContentType = "application/xlsx";
                result = new FileContentResult(System.IO.File.ReadAllBytes(id), HttpContext.Response.ContentType)
                {
                    FileDownloadName = $"{id}.xlsx"
                };
                if (System.IO.File.Exists(id))
                {
                    System.IO.File.Delete(id);
                }
            }
            return result;

        }

        #endregion

        #region imageupload

        [HttpGet]
        public async Task<IActionResult> GetImageList()
        {
            ImageListRequestDTO assetList = new ImageListRequestDTO();
            _allAssetsModel.AssetimageList = await _assetsService.GetAllImageList();
            return PartialView("_PhotoSectionPage", _allAssetsModel);

        }

        [HttpPost]
        public async Task<IActionResult> GetFilterImagelist(string imageTypeCode)
        {
            var imagelist = await _assetsService.GetFilterImageList(imageTypeCode);
            var imagefilename = imagelist.Select(a => a.ImageFilenameSys).ToList();
            return Json(imagefilename);
        }

        [HttpPost]
        public async Task<IActionResult> ImageUploaded(IList<IFormFile> formFile, string assetId, List<string> photoTypes)
        {
            try
            {
                string[] assetGrpCode = assetId.Split('/');
                assetGrpCode[0] = _assetsService.GetAssetNameByCode(assetGrpCode[0]);

                string wwwPath = this._webhostEnvironment.WebRootPath;
                string contentPath = this._webhostEnvironment.ContentRootPath;
                string id = Regex.Replace(assetId, @"[^0-9a-zA-Z]+", "");
              
                int j = 0;
                foreach (IFormFile postedFile in formFile)
                {
                    List<ImageListRequestDTO> uploadedFiles = new List<ImageListRequestDTO>();
                    string photoType = Regex.Replace(photoTypes[j], @"[^a-zA-Z]", "");
                    string path = Path.Combine(wwwPath, Path.Combine("Uploads", assetGrpCode[0], id, photoType));
                    int i = await _assetsService.LastInsertedSRNO(await GetAssetPK(assetId), photoTypes[j]);
                    i++;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    ImageListRequestDTO _rmAssetImageDtl = new ImageListRequestDTO();
                    string fileName = Path.GetFileName(postedFile.FileName);
                    string fileRename = i + "_" + photoType + "_" + fileName;

                    using (FileStream stream = new FileStream(Path.Combine(path, fileRename), FileMode.Create))
                    {
                        _rmAssetImageDtl.AssetId = await GetAssetPK(assetId);
                        _rmAssetImageDtl.ImageTypeCode = photoTypes[j];
                        if (_rmAssetImageDtl.AssetId == 0)
                        {
                            _rmAssetImageDtl.AssetId = int.Parse(assetId);
                        }
                        _rmAssetImageDtl.SNO = i;
                        _rmAssetImageDtl.FileName = postedFile.FileName;
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
                        _rmAssetImageDtl.ImageFilename = assetGrpCode[0] + "\\" + id + "\\" + photoType + "\\" + fileRename;

                        postedFile.CopyTo(stream);

                       
                    }
                    _rmAssetImageDtl.CreatedBy = _security.UserID.ToString();
                    _rmAssetImageDtl.ModifyBy = _security.UserID.ToString();
                    _rmAssetImageDtl.CreatedDate = DateTime.UtcNow;
                    _rmAssetImageDtl.ModifyDate = DateTime.UtcNow;
                    uploadedFiles.Add(_rmAssetImageDtl);
                    await _imageService.SaveImageDtl(uploadedFiles);
                    j = j + 1;
                }
                
                return Json("");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        private async Task<int> GetAssetPK(string assetID)
        {
            return await _assetsService.GetAssetPK(assetID);
        }

        #endregion

        #region Asset Image Modal Reload

        [HttpPost]
        public async Task<IActionResult> GetImageList(int assetPk, string assetId, string location)
        {
            DDLookUpDTO ddLookup = new DDLookUpDTO();
            AssetsModel assetsModel = new AssetsModel();

            assetsModel.AssetimageList = new List<ImageListRequestDTO>();

            assetsModel.ImageTypeList = new List<string>();
            await LoadDropDowns();
            var configUpload = _configuration.GetValue<string>("FileUploadLocation");
            if (assetId != null)
            {
                string[] AssetGroupType = assetId.Split('/');
                ddLookup.Type = "Photo Type";
                ddLookup.TypeCode = AssetGroupType[0];
                ViewBag.PhotoTypeBridge = await _ddLookupService.GetDdLookup(ddLookup);
                assetsModel.AssetimageList = await _assetsService.GetAllImageByAssetPK(await GetAssetPK(assetId));
                assetsModel.AssetimageList = assetsModel.AssetimageList.Select(c => { c.ImageFilename = configUpload + "\\" + c.ImageFilename; return c; }).ToList();
            }
            else
            {
                assetsModel.AssetimageList = await _assetsService.GetAllImageByAssetPK(assetPk);
                assetsModel.AssetimageList = assetsModel.AssetimageList.Select(c => { c.ImageFilename = configUpload + "\\" + c.ImageFilename; return c; }).ToList();
            }

            assetsModel.ImageTypeList = assetsModel.AssetimageList.Select(c => c.ImageTypeCode).Distinct().ToList();
            List<ImageListRequestDTO> TempList = new List<ImageListRequestDTO>();
            TempList = assetsModel.AssetimageList.ToList();
            if (TempList.Count != 0)
            {
                for (int i = 0; i < TempList.Count; i++)
                {
                    TempList[i].FileFullPath = location + "\\" + TempList[i].ImageFilename;
                }
            }
            return PartialView("~/Views/Assets/_PhotoSectionPage.cshtml", assetsModel);
        }

        #endregion

        #region AssetTemplateDownload

        public async Task<IActionResult> StaticAssetTemplateDownload(string assetType)
        {
            try
            {
                string webRoot = _webhostEnvironment.WebRootPath;
                string path = "";
                var fileName = "_Template" + ".zip";
                path = Path.Combine(webRoot, "AssetImportTemplates", assetType, assetType + fileName);
                string contentType = "application/.zip";
                return PhysicalFile(path, contentType, assetType + fileName);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> AssetTemplateDownload(string assetType)
        {
            try
            {
           
                List<AssetFieldDtl> rmAllasset = await _assetsService.GetAssetTemplate(assetType + "_Upload");
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = assetType + "_Template" + ".xlsx";
                byte[] content = GetDownloadFileHdr(rmAllasset);
                return File(content, contentType, fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        private Byte[] GetDownloadFileHdr(List<AssetFieldDtl> rmAllasset)
        {

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("sheet1");
                for (int i1 = 0; i1 < rmAllasset.Count; i1++)
                {
                    worksheet.Cell(1, i1 + 1).Value = rmAllasset[i1].HdrDisplayName;
                }


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return content;
                }
            }
        }
      
        #endregion AssetTemplateDownload

        public async Task<IActionResult> OpenDocs(int id)
        {
            AssetsModel assetModel = new AssetsModel();
            assetModel.AssetDoc = new ImageListRequestDTO();
            byte[] FileBytes;
            assetModel.AssetDoc = await _assetsService.GetDocById(id);
            string wwwPath = this._webhostEnvironment.WebRootPath;
            try
            {
                FileBytes = System.IO.File.ReadAllBytes(wwwPath + @"\" + assetModel.AssetDoc.ImageFilename);
            }
            catch
            {
                return View("~/Views/Assets/AssetImageDownload.cshtml");
            }
            return File(FileBytes, "application/docx");
        }

        #region DetailSearch DDLookUp

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

        #endregion

       
    }
}
