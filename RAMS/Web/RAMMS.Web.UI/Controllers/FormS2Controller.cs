using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RAMMS.Business.ServiceProvider;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using RAMMS.Web.UI.Models;
using Serilog;
namespace RAMMS.Web.UI.Controllers
{
    public class FormS2Controller : Models.BaseController
    {
        private readonly IFormS2Service _formS2Service;

        private IHostingEnvironment Environment;
        private readonly ILogger _logger;
        private readonly IDDLookUpService _ddLookupService;
        private readonly ISecurity _security;

        private readonly IRoadMasterService _roadMasterService;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        // private readonly IConfiguration _configuration;

        public FormS2Controller(IDDLookupBO _DDLookupBO,
        IFormABO _FormABO,
        IHostingEnvironment _environment,
        IFormN1Service formN1Service,
        IFormN2Service formN2Service,
        IFormJServices formJServices,
        IDDLookUpService ddLookupService,
        IFormQa2Service formQa2Service,
        IFormS2Service formS2Service,
        ISecurity security,
        ILogger logger, IRoadMasterService roadMaster, IUserService userService, IWebHostEnvironment webhostenvironment,
        IBridgeBO bridgeBO, IFormQa2Repository mAMQA2Repository)
        {
            Environment = _environment;
            _formS2Service = formS2Service;
            _ddLookupService = ddLookupService;
            _roadMasterService = roadMaster;
            _userService = userService;
            _webHostEnvironment = webhostenvironment;
            _security = security;
        }

        
        
        public IActionResult Index()
        {
            LoadLookupService("RMU");
            return View("~/Views/MAM/FormS2/FormS2.cshtml", new S2HeaderSearchRequestDTO());
        }
        public IActionResult FormS2Download(int id)
        {
            var content1 = _formS2Service.FormDownload("FORMS2", id, Environment.WebRootPath + "/Templates/FORMS2.xlsx");
            string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(content1, contentType1, "FORMS2" + ".xlsx");
        }
        public async Task<IActionResult> AddS2(int id, bool isview)
        {
            FormS2HeaderRequestDto _model = new FormS2HeaderRequestDto();
            if (id > 0)
            {
                _model = await _formS2Service.GetHeaderById(id);
                _model = _model ?? new FormS2HeaderRequestDto();
            }
            _model.IsViewMode = _model.SubmitSts ? true : isview;
            LoadLookupService("RMU", "User");
            return PartialView("~/Views/MAM/FormS2/_AddFormS2.cshtml", _model);
        }

        public async Task<IActionResult> GetRoadListByRmu(string rmu)
        {
            return Json(await _roadMasterService.GetRMUBasedData(new RoadMasterRequestDTO
            {
                RmuCode = rmu
            }));
        }
        public IActionResult GetQuarter(int year) => Json(_formS2Service.GetQuarter(year));
        public async Task<IActionResult> GetHeaderLastInsertedNo(string rmu, int activitycode, int year, int quarter)
        {
            var d = await _formS2Service.CheckHeaderExistence(rmu, activitycode, year, quarter);
            return Json(new { id = d.id, aleadyExists = d.aleadyExists,isSubmitted= d.isSubmitted });
        }
        public async Task<IActionResult> SaveS2Header(FormS2HeaderRequestDto request)
        {
            var id = 0;
            var refid = request.RefId;
            if (request.PkRefNo == 0)
            {
                request.CrBy = _security.UserID;
                //request.RefId = request.RefId.Replace("???", (_formS2Service.LastHeaderInsertedNo()+1).ToString());
            }
            request.ModBy = _security.UserID;
            var result = await _formS2Service.SaveHeader(request);
            if (result != null)
            {
                id = result.PkRefNo;
                refid = result.RefId;
            }           
            return   Json(new { id =id, refid = refid });
        }
        public IActionResult SaveS2Detail(FormS2DetailRequestDto request)
        {
            if(request.PkRefNo == 0)
            {
                request.CrBy = _security.UserID;
            }
            request.ModBy = _security.UserID;
          return   Json(_formS2Service.SaveDetail(request));
        }
        public IActionResult RemoveS2Header(int id) => Json(_formS2Service.RemoveHeader(id));
        public IActionResult RemoveS2Detail(int id) => Json(_formS2Service.RemoveDetail(id));
        public async Task<IActionResult> GetFilteredS2HeaderDetails(DataTableAjaxPostModel<S2HeaderSearchRequestDTO> searchData)
        {
            int _id = 0;
            FilteredPagingDefinition<S2HeaderSearchRequestDTO> filteredPagingDefinition = new FilteredPagingDefinition<S2HeaderSearchRequestDTO>();
            if (Request.Form.ContainsKey("columns[0][search][value]"))
            {
                searchData.filterData.SmartInput = Request.Form["columns[0][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[1][search][value]"))
            {
                searchData.filterData.Rmu = Request.Form["columns[1][search][value]"].ToString();
            }
            if (Request.Form.ContainsKey("columns[2][search][value]"))
            {
                if (int.TryParse(Request.Form["columns[2][search][value]"].ToString(), out _id))
                {
                    searchData.filterData.ActivityCode = _id;
                }

            }
            if (Request.Form.ContainsKey("columns[3][search][value]"))
            {
                if (int.TryParse(Request.Form["columns[3][search][value]"].ToString(), out _id))
                {
                    searchData.filterData.Year = _id;
                }
            }

            if (Request.Form.ContainsKey("columns[4][search][value]"))
            {
                if (int.TryParse(Request.Form["columns[4][search][value]"].ToString(), out _id))
                {
                    searchData.filterData.Quarter = _id;
                }
            }

            filteredPagingDefinition.Filters = searchData.filterData;
            if (searchData.order != null)
            {
                filteredPagingDefinition.ColumnIndex = searchData.order[0].column;
                filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }
            filteredPagingDefinition.RecordsPerPage = searchData.length; //Convert.ToInt32(Request.Form["length"]);
            filteredPagingDefinition.StartPageNo = searchData.start; //Convert.ToInt32(Request.Form["start"]); //TODO
            var result = await _formS2Service.GetHeaderList(filteredPagingDefinition).ConfigureAwait(false);
            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });

        }

        public async Task<IActionResult> GetFilteredS2Details(DataTableAjaxPostModel<FormS2DetailSearchDto> searchData)
        {
            int _id = 0;
            FilteredPagingDefinition<FormS2DetailSearchDto> filteredPagingDefinition = new FilteredPagingDefinition<FormS2DetailSearchDto>();


            filteredPagingDefinition.Filters = searchData.filterData;
            filteredPagingDefinition.RecordsPerPage = searchData.length; //Convert.ToInt32(Request.Form["length"]);
            filteredPagingDefinition.StartPageNo = searchData.start; //Convert.ToInt32(Request.Form["start"]); //TODO
            var result = await _formS2Service.GetDetailList(filteredPagingDefinition).ConfigureAwait(false);
            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });

        }
        public IActionResult GetMonths(int year, int quarter) => Json(_formS2Service.GetMonth(year, quarter));
        public IActionResult GetWeeks(int year, int quarter) => Json(_formS2Service.GetWeek(year, quarter));
        public async Task<IActionResult> GetS2DetailById(int id)
        {
            FormS2DetailRequestDto obj = new FormS2DetailRequestDto();
            if (id > 0)
            {
                obj = await _formS2Service.GetDetailById(id);
                obj = obj ?? new FormS2DetailRequestDto();
            }
            return Json(obj);
        }

        public IActionResult GetDetailLastInsertedNo(int header) => Json(_formS2Service.LastDetailInsertedNo(header));

        public async Task<IActionResult> GetDetailsActiveRefIDs(int activityCode, int roadCode)
        {
            return Json(await _formS2Service.GetActiveRefId(activityCode, roadCode), JsonOption());
        }

        public async Task<bool> CheckS2DtlExist(int headerId, int RdCode)
        {
            var isExist = await _formS2Service.CheckS2DtlExistance(headerId, RdCode);
            return isExist;
        }

    }
}
