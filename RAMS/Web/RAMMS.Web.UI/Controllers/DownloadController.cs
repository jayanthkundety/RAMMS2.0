using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RAMMS.Business.ServiceProvider;
using RAMMS.Domain.Models;
using System.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using static RAMMS.Business.ServiceProvider.IBridgeBO;
using X.PagedList;
//using RAMMS.Common.ServiceProvider;
using ClosedXML.Excel;
using System.IO;
using RAMMS.DTO;
using System.Reflection;
using RAMMS.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RAMMS.Web.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using System.Configuration;
using ExcelDataReader;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace RAMMS.Web.UI.Controllers
{
    public class DownloadController : Controller
    {
        //readonly IWebHostEnvironment webHostEnvironment;
        private readonly IDDLookupBO _dDLookupBO;
        private readonly IBridgeBO _bridgeBO;
        private readonly IAllAssetBO _allAssetBO;
        private IWebHostEnvironment environment;
        private IConfiguration _configuration;
        RMAutoData rMAutoData = new RMAutoData();
        MltyModelBO _mltyModelBO = new MltyModelBO();
        public DownloadController(IDDLookupBO _DDLookupBO, IBridgeBO _BridgeBO, IAllAssetBO _AllAssetBO, IWebHostEnvironment _environment, IConfiguration configuration)
        {
            _dDLookupBO = _DDLookupBO;
            _bridgeBO = _BridgeBO;
            _allAssetBO = _AllAssetBO;
            environment = _environment;
            _configuration = configuration;
        }

        public IActionResult Index(RmDdLookup _rmDdLookup, RmRoadMaster _road, RmAllassetInventory rmAllassetInventory1, int? pageSize)
        {
           // pageSize = 10;
            //Pagination
            string sortOrder = null;
            string currentFilter = null;
            string searchString = null;
            int? Page_No = null;
            int Size_Of_Page = (pageSize ?? 5);
            int No_Of_Page = (Page_No ?? 1);

             _mltyModelBO._BridgeInvSave = new RmAllassetInventory();
            // _mltyModelBO._BridgeInvGrid = _bridgeBO.GetBridgeGridBO().ToPagedList(No_Of_Page, Size_Of_Page);

            //_bridgeBO._rmAllasset = _bridgeBO._rmAllasset;
            //_bridgeBO.bridgeObj = _bridgeBO.GetBridgeGridBO();//.ToPagedList(No_Of_Page, Size_Of_Page);

            GridView(sortOrder, currentFilter, searchString, Page_No, Size_Of_Page, _rmDdLookup, _road);


            GetUploadedImage();

            return View("~/Views/Asset/LandingPage.cshtml", _mltyModelBO);

        }
        //[HttpPost]
        //public IActionResult Add(RAMMS.Business.ServiceProvider.MltyModelBO mltyModel, RmDdLookup _rmDdLookup, RmRoadMaster _road, string sortOrder, string currentFilter, string searchString, int? Page_No, int? pageSize)
        //{
        //    //_mltyModelBO._BridgeInvSave = new RmAllassetInventory();
        //    //var str = String.Join(",", mltyModel._multiSelectDropDownViewModel.AiStrucSuper_mltySelect);
        //    ////var _rmAllasset;
        //    if (ModelState.IsValid)
        //    {
        //        if (mltyModel._rmAllassetInventory.AiPkRefNo == 0)
        //        {
        //            //mltyModel._rmAllassetInventory.AiStrucSuper = String.Join(",", mltyModel._multiSelectDropDownViewModel.AiStrucSuper_mltySelect.Where(s => !string.IsNullOrEmpty(s)));

        //            mltyModel._rmAllassetInventory.AiStrucSuper = (mltyModel._multiSelectDropDownViewModel.AiStrucSuper_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiStrucSuper_mltySelect) : null;

        //            mltyModel._rmAllassetInventory.AiParapetType = (mltyModel._multiSelectDropDownViewModel.AiParapetType_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiParapetType_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiBearingType = (mltyModel._multiSelectDropDownViewModel.AiBearingType_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiBearingType_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiExpanType = (mltyModel._multiSelectDropDownViewModel.AiExpanType_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiExpanType_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiDeckType = (mltyModel._multiSelectDropDownViewModel.AiDeckType_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiDeckType_mltySelect) : null;

        //            mltyModel._rmAllassetInventory.AiAbutType = (mltyModel._multiSelectDropDownViewModel.AiAbutType_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiAbutType_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiPierType = (mltyModel._multiSelectDropDownViewModel.AiPierType_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiPierType_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiAbutFound = (mltyModel._multiSelectDropDownViewModel.AiAbutFound_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiAbutFound_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiPiersPrimComp = (mltyModel._multiSelectDropDownViewModel.AiPiersPrimComp_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiPiersPrimComp_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiBearingSeatDiaphg = (mltyModel._multiSelectDropDownViewModel.AiBearingSeatDiaphg_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiBearingSeatDiaphg_mltySelect) : null;

        //            mltyModel._rmAllassetInventory.AiBeamsGridTrusArch = (mltyModel._multiSelectDropDownViewModel.AiBeamsGridTrusArch_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiBeamsGridTrusArch_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiDeckPavement = (mltyModel._multiSelectDropDownViewModel.AiDeckPavement_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiDeckPavement_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiUtilities = (mltyModel._multiSelectDropDownViewModel.AiUtilities_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiUtilities_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiWaterway = (mltyModel._multiSelectDropDownViewModel.AiWaterway_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiWaterway_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiWaterDownpipe = (mltyModel._multiSelectDropDownViewModel.AiWaterDownpipe_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiWaterDownpipe_mltySelect) : null;

        //            mltyModel._rmAllassetInventory.AiParapetRailing = (mltyModel._multiSelectDropDownViewModel.AiParapetRailing_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiParapetRailing_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiSidewalksAppSlab = (mltyModel._multiSelectDropDownViewModel.AiSidewalksAppSlab_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiSidewalksAppSlab_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiExpanJoint = (mltyModel._multiSelectDropDownViewModel.AiExpanJoint_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiExpanJoint_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiSlopeRetainWall = (mltyModel._multiSelectDropDownViewModel.AiSlopeRetainWall_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiSlopeRetainWall_mltySelect) : null;



        //             int iSaveAllAsset = _allAssetBO.SaveAllAssetBO(mltyModel._rmAllassetInventory);
        //        }
        //        else
        //        {
        //            mltyModel._rmAllassetInventory.AiStrucSuper = (mltyModel._multiSelectDropDownViewModel.AiStrucSuper_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiStrucSuper_mltySelect) : null;

        //            mltyModel._rmAllassetInventory.AiParapetType = (mltyModel._multiSelectDropDownViewModel.AiParapetType_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiParapetType_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiBearingType = (mltyModel._multiSelectDropDownViewModel.AiBearingType_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiBearingType_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiExpanType = (mltyModel._multiSelectDropDownViewModel.AiExpanType_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiExpanType_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiDeckType = (mltyModel._multiSelectDropDownViewModel.AiDeckType_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiDeckType_mltySelect) : null;

        //            mltyModel._rmAllassetInventory.AiAbutType = (mltyModel._multiSelectDropDownViewModel.AiAbutType_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiAbutType_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiPierType = (mltyModel._multiSelectDropDownViewModel.AiPierType_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiPierType_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiAbutFound = (mltyModel._multiSelectDropDownViewModel.AiAbutFound_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiAbutFound_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiPiersPrimComp = (mltyModel._multiSelectDropDownViewModel.AiPiersPrimComp_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiPiersPrimComp_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiBearingSeatDiaphg = (mltyModel._multiSelectDropDownViewModel.AiBearingSeatDiaphg_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiBearingSeatDiaphg_mltySelect) : null;

        //            mltyModel._rmAllassetInventory.AiBeamsGridTrusArch = (mltyModel._multiSelectDropDownViewModel.AiBeamsGridTrusArch_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiBeamsGridTrusArch_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiDeckPavement = (mltyModel._multiSelectDropDownViewModel.AiDeckPavement_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiDeckPavement_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiUtilities = (mltyModel._multiSelectDropDownViewModel.AiUtilities_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiUtilities_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiWaterway = (mltyModel._multiSelectDropDownViewModel.AiWaterway_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiWaterway_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiWaterDownpipe = (mltyModel._multiSelectDropDownViewModel.AiWaterDownpipe_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiWaterDownpipe_mltySelect) : null;

        //            mltyModel._rmAllassetInventory.AiParapetRailing = (mltyModel._multiSelectDropDownViewModel.AiParapetRailing_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiParapetRailing_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiSidewalksAppSlab = (mltyModel._multiSelectDropDownViewModel.AiSidewalksAppSlab_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiSidewalksAppSlab_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiExpanJoint = (mltyModel._multiSelectDropDownViewModel.AiExpanJoint_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiExpanJoint_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiSlopeRetainWall = (mltyModel._multiSelectDropDownViewModel.AiSlopeRetainWall_mltySelect != null) ? String.Join(",", mltyModel._multiSelectDropDownViewModel.AiSlopeRetainWall_mltySelect) : null;
        //            mltyModel._rmAllassetInventory.AiActiveYn = true;

        //            int iUpdateAllAsset = _allAssetBO.UpdateAllAssetBO(mltyModel._rmAllassetInventory);
        //        }
        //        ViewBag.CurrentSort = sortOrder;
        //        ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //        ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
        //        if (searchString != null)
        //        {
        //            Page_No = 1;
        //        }
        //        else
        //        {
        //            searchString = currentFilter;
        //        }

        //        ViewBag.CurrentFilter = searchString;
        //        int Size_Of_Page = (pageSize ?? 5);
        //        int No_Of_Page = (Page_No ?? 1);

        //        //_bridgeBO.bridgeObj = _bridgeBO.GetBridgeGridBO().ToPagedList(No_Of_Page, Size_Of_Page);
        //        GridView(sortOrder, currentFilter, searchString, Page_No, Size_Of_Page, _rmDdLookup, _road);

        //        return View("~/Views/Asset/LandingPage.cshtml", _mltyModelBO);
        //    }
        //    else
        //    {

        //        return RedirectToAction("Index");
        //        // return null;
        //    }

        //}
        public JsonResult GetRMAllData(string featureid, string assetId, string boundId)
        {
            RmAllassetInventory _rmAllassetInventory = new RmAllassetInventory();
            RmRoadMaster _rmRoadMaster = new RmRoadMaster();
            _rmRoadMaster.RdmFeatureId = featureid;
            var _RMAllData = _bridgeBO.GetRMAllData(_rmRoadMaster);
            rMAutoData.sDivision = _RMAllData[0].RdmDivCode;
            rMAutoData.sRUMCode = _RMAllData[0].RdmRmuCode;

            rMAutoData.sSecCode = _RMAllData[0].RdmSecCode.ToString();
            rMAutoData.sSecName = _RMAllData[0].RdmSecName;
            rMAutoData.sRoadCode = _RMAllData[0].RdmRdCode;
            rMAutoData.sRoadName = _RMAllData[0].RdmRdName;
            if (boundId != null)
            {
                GetAutoID(featureid, assetId, boundId);
            }
            return Json(rMAutoData);
        }
        public JsonResult GetAutoID(string featureid, string assetId, string boundId)
        {
            RmRoadMaster _rmRoadMaster = new RmRoadMaster();
            _rmRoadMaster.RdmFeatureId = featureid;
            var _RMAllData = _bridgeBO.GetRMAllData(_rmRoadMaster);
            rMAutoData.sDivision = _RMAllData[0].RdmDivCode;
            rMAutoData.sRUMCode = _RMAllData[0].RdmRmuCode;

            rMAutoData.sSecCode = _RMAllData[0].RdmSecCode.ToString();
            rMAutoData.sSecName = _RMAllData[0].RdmSecName;
            rMAutoData.sRoadCode = _RMAllData[0].RdmRdCode;
            rMAutoData.sRoadName = _RMAllData[0].RdmRdName;
            rMAutoData.sChFrom = _RMAllData[0].RdmFrmCh.ToString();
            rMAutoData.sChFromDsl = _RMAllData[0].RdmFrmChDeci.ToString();
            string chFrom = rMAutoData.sChFrom + "." + rMAutoData.sChFromDsl;

            rMAutoData.sAssetId = "";
            var sAutoId = "BR/" + assetId + "/" + rMAutoData.sRUMCode + "/" + rMAutoData.sSecCode + "/" + rMAutoData.sRoadCode + "/" + chFrom + "/" + boundId;
            rMAutoData.sAssetId = sAutoId;


            return Json(rMAutoData);
        }

        //Bridge Main Grid
        public ActionResult GridView(string sortOrder, string currentFilter, string searchString, int? Page_No, int? pageSize, RmDdLookup _rmDdLookup, RmRoadMaster _road)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                Page_No = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            int Size_Of_Page = (pageSize ?? 5);
            int No_Of_Page = (Page_No ?? 1);
            ViewBag.psize = Size_Of_Page;

            //_rmDdLookup.DdlType = "Asset Type";
            //ViewData["AssetType1"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Structure Code";
            //ViewData["StructureCode"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Parapet Type";
            //ViewData["ParapetType"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_road.RdmDivCode = "MIRI";
            ////ViewData["AssetType"] = _bridgeBO.GetddLookup(_road);
            //ViewData["AssetType"] = new SelectList(_bridgeBO.GetRMLookupData(_road), "RdmFeatureId", "RdmFeatureId");

            //_rmDdLookup.DdlType = "Asset Group";
            //ViewData["AssetGroup"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Superstructure";
            //ViewData["Superstructure"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Superstructure";
            //ViewData["Superstructure1"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Bearing Type";
            //ViewData["BearingType"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Expansion Type";
            //ViewData["ExpansionType"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Deck Type";
            //ViewData["DeckType"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Abutment Type";
            //ViewData["AbutmentType"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Pier Type";
            //ViewData["PierType"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Bound";
            //ViewData["Bound"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Abutment Walls, Foundation";
            //ViewData["AbutmentWalls,Foundation"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Piers, Connectiong of primary components";
            //ViewData["Piers,Connectiongofprimarycomponents"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Bearing, Bearing Seats, Bearing Diaphgrams";
            //ViewData["Bearing,BearingSeats,BearingDiaphgram"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Beams, Girders, Trussess, Arches";
            //ViewData["Beams,Grider,Trusses,Arches"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Deck Slab, Pavement";
            //ViewData["DeckSlab,Pavement"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Signboard, Utilities";
            //ViewData["Signboard,Utilities"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Waterway";
            //ViewData["Waterway"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Drain Water Down Pipe, Drainage";
            //ViewData["DrainWaterDownPipe,Drainage"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Parapet, Railing";
            //ViewData["Parapet,Railing"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Kerb, Sidewalks, Approaches, Approch Slab";
            //ViewData["Kerb,Sidewalks,Approaches,ApproachesSlab"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Expansion Joint";
            //ViewData["ExpansionJoint"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            //_rmDdLookup.DdlType = "Slope Protections, Retaining Wall";
            //ViewData["Slopeprotection,RetainingWall"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            ViewBag.PageSize = new List<SelectListItem>()
         {
             new SelectListItem() { Value="5", Text= "5" },
             new SelectListItem() { Value="10", Text= "10" },
             new SelectListItem() { Value="15", Text= "15" },
             new SelectListItem() { Value="25", Text= "25" },
             new SelectListItem() { Value="50", Text= "50" },
         };

            IPagedList<SearchBridge> rms = _bridgeBO.GetBridgeGrid().ToPagedList(No_Of_Page, Size_Of_Page);
            _mltyModelBO.searchObj = rms;//_bridgeBO.GetBridgeGridBO().ToPagedList(No_Of_Page, Size_Of_Page);

            ViewBag.TotalNoRecords = rms.TotalItemCount.ToString();
            int iPreDisplay = ((No_Of_Page) * Size_Of_Page);
            ViewBag.DisplayRecords = iPreDisplay;

            ViewBag.TotalPage = rms.PageCount;
            var CurrentPage = (rms.PageCount < rms.PageNumber ? 0 : rms.PageNumber);
            ViewBag.CurrentPage = CurrentPage;
            return View("~/Views/Asset/LandingPage.cshtml", _mltyModelBO);
        }
      
        public ActionResult SearchBridge(string? sortOrder, string? currentFilter, string Inputvalue, int? Page_No, RmDdLookup _rmDdLookup, RmRoadMaster _road, SearchBridge? search, int? pageSize)
        {
            string searchString = Inputvalue;
            string assetgroup = "BR";
            // string textboxValue = Request.Form["txtOne"];
            //{
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                Page_No = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            int Size_Of_Page = (pageSize ?? 5);
            int No_Of_Page = (Page_No ?? 1);

            _rmDdLookup.DdlType = "Asset Type";
            ViewData["AssetType1"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Structure Code";
            ViewData["StructureCode"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Parapet Type";
            ViewData["ParapetType"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _road.RdmDivCode = "MIRI";
            //ViewData["AssetType"] = _bridgeBO.GetddLookup(_road);
            ViewData["AssetType"] = new SelectList(_bridgeBO.GetRMLookupData(_road), "RdmFeatureId", "RdmFeatureId");

            _rmDdLookup.DdlType = "Asset Group";
            ViewData["AssetGroup"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Superstructure";
            ViewData["Superstructure"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Superstructure";
            ViewData["Superstructure1"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Bearing Type";
            ViewData["BearingType"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Expansion Type";
            ViewData["ExpansionType"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Deck Type";
            ViewData["DeckType"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Abutment Type";
            ViewData["AbutmentType"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Pier Type";
            ViewData["PierType"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Bound";
            ViewData["Bound"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Abutment Walls, Foundation";
            ViewData["AbutmentWalls,Foundation"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Piers, Connectiong of primary components";
            ViewData["Piers,Connectiongofprimarycomponents"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Bearing, Bearing Seats, Bearing Diaphgrams";
            ViewData["Bearing,BearingSeats,BearingDiaphgram"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Beams, Girders, Trussess, Arches";
            ViewData["Beams,Grider,Trusses,Arches"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Deck Slab, Pavement";
            ViewData["DeckSlab,Pavement"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Signboard, Utilities";
            ViewData["Signboard,Utilities"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Waterway";
            ViewData["Waterway"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Drain Water Down Pipe, Drainage";
            ViewData["DrainWaterDownPipe,Drainage"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Parapet, Railing";
            ViewData["Parapet,Railing"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Kerb, Sidewalks, Approaches, Approch Slab";
            ViewData["Kerb,Sidewalks,Approaches,ApproachesSlab"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Expansion Joint";
            ViewData["ExpansionJoint"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Slope Protections, Retaining Wall";
            ViewData["Slopeprotection,RetainingWall"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            ViewBag.PageSize = new List<SelectListItem>()
         {
             new SelectListItem() { Value="5", Text= "5" },
             new SelectListItem() { Value="10", Text= "10" },
             new SelectListItem() { Value="15", Text= "15" },
             new SelectListItem() { Value="25", Text= "25" },
             new SelectListItem() { Value="50", Text= "50" },
         };         //   _rmDdLookup.DdlType = "Asset Type";
            
            //_mltyModelBO.searchObj = _bridgeBO.SearchBridgeGridBO(assetgroup, Inputvalue).ToPagedList(No_Of_Page, Size_Of_Page);

            IPagedList<SearchBridge> rms = _bridgeBO.SearchBridgeGridBO(assetgroup, Inputvalue).ToPagedList(No_Of_Page, Size_Of_Page);
            _mltyModelBO.searchObj = rms;//_bridgeBO.GetBridgeGridBO().ToPagedList(No_Of_Page, Size_Of_Page);

            ViewBag.TotalNoRecords = rms.TotalItemCount.ToString();
            int iPreDisplay = ((No_Of_Page) * Size_Of_Page);
            ViewBag.DisplayRecords = iPreDisplay;

            ViewBag.TotalPage = rms.PageCount;
            var CurrentPage = (rms.PageCount < rms.PageNumber ? 0 : rms.PageNumber);
            ViewBag.CurrentPage = CurrentPage;
            return View("~/Views/Asset/LandingPage.cshtml", _mltyModelBO);


        }
        
      
        public ActionResult Downloadgridresults()
        {
            //_bridgeBO.assetFieldDtlsobj = _bridgeBO.GetAssetFieldDtls();


            _bridgeBO.bridgeObj = _bridgeBO.GetBridgeGridBO();
            var RmAllassetInventoryDTO = _bridgeBO.bridgeObj.ToList();




            var RmAllasset = _bridgeBO.GetAssetFieldDtls("Bridge").ToList();

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "Assertdetails.xlsx";
            var content = _bridgeBO.GetFile(RmAllasset, RmAllassetInventoryDTO);
            return File(content, contentType, fileName);
        }

        public ActionResult PrintForm(int id, string formname)
        {
            try
            {
                _bridgeBO.bridgeObj = _bridgeBO.GetBridgeGridBO();
                var RmAllassetInventoryDTO = _bridgeBO.bridgeObj.ToList();
                string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";                
                string filepath = environment.WebRootPath + _configuration.GetValue<string>("FormTemplateLocation");
                var content1 = _bridgeBO.formdownload(formname, id, filepath);
                return File(content1, contentType1, formname + ".xlsx");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult DownloadFormJ()
        {
            _bridgeBO.bridgeObj = _bridgeBO.GetBridgeGridBO();
            var RmAllassetInventoryDTO = _bridgeBO.bridgeObj.ToList();
            string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName1 = "FormJ";
            var content1 = _bridgeBO.formdownload(fileName1,3,"");
            return File(content1, contentType1, fileName1 + ".xlsx");
        }

        public ActionResult DownloadFormH()
        {
            _bridgeBO.bridgeObj = _bridgeBO.GetBridgeGridBO();
            var RmAllassetInventoryDTO = _bridgeBO.bridgeObj.ToList();
            string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName1 = "FormH";
            var content1 = _bridgeBO.formdownload(fileName1,5,"");
            return File(content1, contentType1, fileName1 + ".xlsx");
        }

        [HttpPost]
        public ActionResult UploadFile(List<IFormFile> FormFile)
        {
            int i = 1;
            string wwwPath = this.environment.WebRootPath;
            string contentPath = this.environment.ContentRootPath;
            string path = Path.Combine(wwwPath, Path.Combine("Uploads", "Bridge", "Waterway (Upstream)"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            List<RmAssetImageDtl> uploadedFiles = new List<RmAssetImageDtl>();
            foreach (IFormFile postedFile in FormFile)
            {
                RmAssetImageDtl _rmAssetImageDtl = new RmAssetImageDtl();
                string fileName = Path.GetFileName(postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    _rmAssetImageDtl.AidImageTypeCode = "Waterway";
                    _rmAssetImageDtl.AidImageSrno = i;
                    _rmAssetImageDtl.AidImageFilenameSys = i + "_" + _rmAssetImageDtl.AidImageTypeCode + "_" + fileName;
                    _rmAssetImageDtl.AidImageFilenameUpload = stream.Name;
                    postedFile.CopyTo(stream);

                    //ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                }
                uploadedFiles.Add(_rmAssetImageDtl);
                i = i + 1;
            }
            _bridgeBO.SaveAssetImageDtlBO(uploadedFiles);
            return RedirectToAction("GetUploadedImage");
        }

        [HttpGet]
        public dynamic GetUploadedImage()
        {
            _mltyModelBO._assetImg = _bridgeBO.GetUploadedImageBO().ToList();
            return _mltyModelBO._assetImg;
        }

        [HttpPost]
        public ActionResult AssetImportFile(IFormFile importFile)
        {

            try
            {
                //var path = Request.Form.Files;

                List<RmAllassetInventory> _assetImportExcel = new List<RmAllassetInventory>();
                // string fileName = Path.GetFileName(importFile.FileName);

                var fileName = "d:\\myFolder\\tests-example.xls";
                //var fileName = "d:\\myFolder\\AssertdetailsNew.xlsx";
                // For .net core, the next line requires the NuGet package, 
                // System.Text.Encoding.CodePages
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                        {
                            ConfigureDataTable = _ => new ExcelDataTableConfiguration
                            {
                                UseHeaderRow = true // To set First Row As Column Names  
                            }
                        });
                        var dataTable = dataSet.Tables[0];
                        foreach (DataRow objDataRow in dataTable.Rows)
                        {
                            _assetImportExcel.Add(new RmAllassetInventory()
                            {

                                AiAbutType = objDataRow[0].ToString(),
                                AiAbutFound = objDataRow[1].ToString(),//objDataRow["Abutment Walls, Foundation "] also do
                                AiAssetGrpCode = objDataRow[2].ToString(),
                                AiGrpType = objDataRow[3].ToString(),
                                AiBeamsGridTrusArch = objDataRow[4].ToString(),
                                AiBearingType = objDataRow[5].ToString(),
                                AiBearingSeatDiaphg = objDataRow[6].ToString(),
                                AiBound = objDataRow[7].ToString(),
                                AiBridgeName = objDataRow[8].ToString(),
                                AiDeckPavement = objDataRow[9].ToString(),
                                //AI_Deck_Type
                                AiDeckType = objDataRow[10].ToString(),
                                //AI_Water_Downpipe
                                AiWaterDownpipe = objDataRow[11].ToString(),
                                //AI_Expan_Joint
                                AiExpanJoint = objDataRow[12].ToString(),
                                //AI_Expan_Type
                                AiExpanType = objDataRow[13].ToString(),
                                //AI_GPS_Easting
                                // AiGpsEasting = Convert.ToDouble(objDataRow[14].ToString()),

                                AiGpsEasting = Convert.ToDouble(string.IsNullOrEmpty(objDataRow[14].ToString()) ? "0" : objDataRow[14].ToString()),

                                //AI_GPS_Northing
                                AiGpsNorthing = Convert.ToDouble(string.IsNullOrEmpty(objDataRow[15].ToString()) ? "0" : objDataRow[15].ToString()),
                                //AI_Sidewalks_App_Slab
                                AiSidewalksAppSlab = objDataRow[16].ToString(),
                                //AI_Width_Lane
                                AiWidthLane = Convert.ToDouble(objDataRow[17].ToString()),
                                //AI_Length
                                AiLength = Convert.ToDouble(objDataRow[18].ToString()),
                                //AI_maintained_By
                                AiMaintainedBy = objDataRow[19].ToString(),
                                //AI_median
                                AiMedian = Convert.ToDouble(objDataRow[20].ToString()),
                                //AI_Lane_Cnt
                                AiLaneCnt = Convert.ToInt32(objDataRow[21].ToString()),
                                //AI_Span_Cnt
                                AiSpanCnt = Convert.ToInt32(objDataRow[22].ToString()),
                                //AI_Expan_Joint_Count
                                AiExpanJointCount = Convert.ToInt32(objDataRow[23].ToString()),
                                //AI_Owner
                                AiOwner = objDataRow[24].ToString(),
                                //AI_Parapet_Type
                                AiParapetType = objDataRow[25].ToString(),
                                //AI_Parapet_Railing
                                AiParapetRailing = objDataRow[26].ToString(),
                                //AI_Pier_Type
                                AiPierType = objDataRow[27].ToString(),
                                //AI_Piers_Prim_Comp
                                AiPiersPrimComp = objDataRow[28].ToString(),
                                //AI_Feature_ID
                                AiFeatureId = objDataRow[29].ToString(),
                                //AI_River_Name
                                AiRiverName = objDataRow[30].ToString(),
                                //AI_RMU_Code
                                AiRmuCode = objDataRow[31].ToString(),
                                //AI_Rd_Code
                                AiRdCode = objDataRow[32].ToString(),
                                //AI_Rd_name
                                AiRdName = objDataRow[33].ToString(),
                                //AI_Sec_Code
                                AiSecCode = objDataRow[34].ToString(),
                                //AI_Sec_name
                                AiSecName = objDataRow[35].ToString(),
                                //AI_Utilities
                                AiUtilities = objDataRow[36].ToString(),
                                //AI_Slope_Retain_wall
                                AiSlopeRetainWall = objDataRow[37].ToString(),
                                //AI_Expan_Joint_Space
                                AiExpanJointSpace = Convert.ToDouble(objDataRow[38].ToString()),
                                //AI_length_Span
                                AiLengthSpan = Convert.ToDouble(objDataRow[39].ToString()),
                                //AI_Struc_Code
                                AiStrucCode = objDataRow[40].ToString(),
                                //AI_Struc_Super
                                AiStrucSuper = objDataRow[41].ToString(),
                                //AI_Walkway
                                AiWalkway = Convert.ToDouble(objDataRow[42].ToString()),
                                //AI_Waterway
                                AiWaterway = objDataRow[43].ToString(),
                                //AI_Width
                                AiWidth = Convert.ToDouble(objDataRow[44].ToString()),
                                //AI_Asset_ID
                                AiAssetId = objDataRow[45].ToString(),
                                //AI_Div_Code
                                AiDivCode = objDataRow[48].ToString(),
                            });
                        }
                          int iImportExcelSave = _allAssetBO.ImportExcelAssetBO( _assetImportExcel);



                        //    while (reader.Read()) //Each row of the file
                        //{
                        //    users.Add(new RmAllassetInventory
                        //    {
                        //        AiAbutType = reader.GetValue(0).ToString(),
                        //        AiAbutFound = reader.GetValue(1).ToString(),
                        //    });
                        //}

                    }
                }


            }
            catch (Exception ex)
            {
                return null;
            }
            //return View("~/Views/Asset/LandingPage.cshtml");
            return RedirectToAction("Index");
        }

        public ActionResult DownloadgridHeader()
        {

            var RmAllasset = _bridgeBO.GetAssetFieldDtls("Bridge_Upload").ToList();

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "Assertdetails.xlsx";
            var content = _bridgeBO.GetDownloadFileHdr(RmAllasset);
            return File(content, contentType, fileName);
        }
        public ActionResult BridgeEdit(int EditID, RmDdLookup _rmDdLookup, RmRoadMaster _road)
        {
            //EditID = 1027;
            MultiSelectDropDownViewModel multiSelect = new MultiSelectDropDownViewModel();// { SelectedMultiId = new List<string>(), FullSelectedList = new List<RmDdLookup>() };

            _mltyModelBO._multiSelectDropDownViewModel = new MultiSelectDropDownViewModel();
            _mltyModelBO._rmAllassetInventory = new RmAllassetInventory();


            _rmDdLookup.DdlType = "Asset Type";
            ViewData["AssetType1"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Structure Code";
            ViewData["StructureCode"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Parapet Type";
            ViewBag.ParapetType = _dDLookupBO.GetddLookup(_rmDdLookup);

            _road.RdmDivCode = "MIRI";
            //ViewData["AssetType"] = _bridgeBO.GetddLookup(_road);
            ViewData["AssetType"] = new SelectList(_bridgeBO.GetRMLookupData(_road), "RdmFeatureId", "RdmFeatureId");

            _rmDdLookup.DdlType = "Asset Group";
            ViewData["AssetGroup"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Superstructure";
            ViewData["Superstructure"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Superstructure";
            ViewBag.Superstructure1 = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Bearing Type";
            ViewData["BearingType"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Expansion Type";
            ViewData["ExpansionType"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Deck Type";
            ViewData["DeckType"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Abutment Type";
            ViewData["AbutmentType"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Pier Type";
            ViewData["PierType"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Bound";
            ViewData["Bound"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Abutment Walls, Foundation";
            ViewData["AbutmentWalls,Foundation"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Piers, Connectiong of primary components";
            ViewData["Piers,Connectiongofprimarycomponents"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Bearing, Bearing Seats, Bearing Diaphgrams";
            ViewData["Bearing,BearingSeats,BearingDiaphgram"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Beams, Girders, Trussess, Arches";
            ViewData["Beams,Grider,Trusses,Arches"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Deck Slab, Pavement";
            ViewData["DeckSlab,Pavement"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Signboard, Utilities";
            ViewData["Signboard,Utilities"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Waterway";
            ViewData["Waterway"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Drain Water Down Pipe, Drainage";
            ViewData["DrainWaterDownPipe,Drainage"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Parapet, Railing";
            ViewData["Parapet,Railing"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Kerb, Sidewalks, Approaches, Approch Slab";
            ViewData["Kerb,Sidewalks,Approaches,ApproachesSlab"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Expansion Joint";
            ViewData["ExpansionJoint"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            _rmDdLookup.DdlType = "Slope Protections, Retaining Wall";
            ViewData["Slopeprotection,RetainingWall"] = _dDLookupBO.GetddLookup(_rmDdLookup);

            RmAllassetInventory EditDataById = new RmAllassetInventory();

            if (EditID != 0)
            {
                EditDataById = _allAssetBO.BridgeEditByIdBO(EditID);

                _mltyModelBO._rmAllassetInventory = EditDataById;

                _mltyModelBO._multiSelectDropDownViewModel.AiStrucSuper_mltySelect = _allAssetBO.mltySelectedItems(EditDataById.AiStrucSuper);

                _mltyModelBO._multiSelectDropDownViewModel.AiParapetType_mltySelect = _allAssetBO.mltySelectedItems(EditDataById.AiParapetType);
                _mltyModelBO._multiSelectDropDownViewModel.AiBearingType_mltySelect = _allAssetBO.mltySelectedItems(EditDataById.AiBearingType);
                _mltyModelBO._multiSelectDropDownViewModel.AiExpanType_mltySelect = _allAssetBO.mltySelectedItems(EditDataById.AiExpanType);
                _mltyModelBO._multiSelectDropDownViewModel.AiExpanType_mltySelect = _allAssetBO.mltySelectedItems(EditDataById.AiDeckType);
                _mltyModelBO._multiSelectDropDownViewModel.AiAbutType_mltySelect = _allAssetBO.mltySelectedItems(EditDataById.AiAbutType);
                _mltyModelBO._multiSelectDropDownViewModel.AiAbutType_mltySelect = _allAssetBO.mltySelectedItems(EditDataById.AiAbutType);
                _mltyModelBO._multiSelectDropDownViewModel.AiAbutFound_mltySelect = _allAssetBO.mltySelectedItems(EditDataById.AiAbutFound);
                _mltyModelBO._multiSelectDropDownViewModel.AiPiersPrimComp_mltySelect = _allAssetBO.mltySelectedItems(EditDataById.AiPiersPrimComp);
                _mltyModelBO._multiSelectDropDownViewModel.AiBearingSeatDiaphg_mltySelect = _allAssetBO.mltySelectedItems(EditDataById.AiBearingSeatDiaphg);
                _mltyModelBO._multiSelectDropDownViewModel.AiParapetType_mltySelect = _allAssetBO.mltySelectedItems(EditDataById.AiParapetType);
                _mltyModelBO._multiSelectDropDownViewModel.AiDeckPavement_mltySelect = _allAssetBO.mltySelectedItems(EditDataById.AiDeckPavement);
                _mltyModelBO._multiSelectDropDownViewModel.AiUtilities_mltySelect = _allAssetBO.mltySelectedItems(EditDataById.AiUtilities);
                _mltyModelBO._multiSelectDropDownViewModel.AiWaterway_mltySelect = _allAssetBO.mltySelectedItems(EditDataById.AiWaterway);
                _mltyModelBO._multiSelectDropDownViewModel.AiWaterDownpipe_mltySelect = _allAssetBO.mltySelectedItems(EditDataById.AiWaterDownpipe);
                _mltyModelBO._multiSelectDropDownViewModel.AiParapetRailing_mltySelect = _allAssetBO.mltySelectedItems(EditDataById.AiParapetRailing);
                _mltyModelBO._multiSelectDropDownViewModel.AiSidewalksAppSlab_mltySelect = _allAssetBO.mltySelectedItems(EditDataById.AiSidewalksAppSlab);
                _mltyModelBO._multiSelectDropDownViewModel.AiExpanJoint_mltySelect = _allAssetBO.mltySelectedItems(EditDataById.AiExpanJoint);
                _mltyModelBO._multiSelectDropDownViewModel.AiSlopeRetainWall_mltySelect = _allAssetBO.mltySelectedItems(EditDataById.AiSlopeRetainWall);
            }

            return PartialView("~/Views/Asset/_AddBridge.cshtml", _mltyModelBO);

        }




        //Tab

        [HttpGet]
        public IActionResult file(string id, [FromQuery(Name = "path")] string path, [FromQuery(Name = "ref")] string refrence)
        {
            string strDownloadType = id;
            string strPath = environment.WebRootPath;
            switch (strDownloadType.ToLower())
            {
                case "forma":
                    strPath = Path.Combine(strPath, "Uploads", "FormADetail", path);
                    break;
                case "formj":
                    strPath = Path.Combine(strPath, "Uploads", "FormJDetail", path);
                    break;
                case "formd":
                    strPath = Path.Combine(strPath, "Uploads", "FormD", path);
                    break;
                case "formx":
                    strPath = Path.Combine(strPath, path);
                    break;
                case "formb1b2":
                    strPath = Path.Combine(strPath, path);
                    break;
                case "formc1c2":
                    strPath = Path.Combine(strPath, path);
                    break;
                default:
                    strPath = Path.Combine(strPath, "Uploads", path);
                    break;
            }
            if (System.IO.File.Exists(strPath))
            {
                byte[] filebyte = System.IO.File.ReadAllBytes(strPath);
                return File(filebyte, "application/octet-stream", Path.GetFileName(path));
            }
            else
            {
                return NotFound("File Not exists.");// ForbidResult();
            }
        }
    }
       
}
