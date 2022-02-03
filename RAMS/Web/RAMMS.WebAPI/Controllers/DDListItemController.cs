using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RAMMS.Business.ServiceProvider;
using RAMMS.Business.ServiceProvider.Interfaces;
//using RAMMS.Common.ServiceProvider;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;

namespace RAMMS.WebAPI
{
    public class DDListItemController : Controller
    {
        private readonly IDDLookupBO _ddLookupBO;
        private readonly IDDLookUpService _DDLookUpService;
        private readonly IFormAService _formAService;
        private readonly IUserService _userService;
        private readonly IRoadMasterService _roadMasterService;
        private readonly IFormDService _formDservice;
        private readonly IFormJServices _formJservice;
        public DDListItemController(IDDLookupBO _ddLookup, IDDLookUpService DDLookUpService, IFormAService formAService, IUserService userService, IRoadMasterService roadMasterService, IFormDService formDService, IFormJServices formJservice)
        {
            _DDLookUpService = DDLookUpService;
            _ddLookupBO = _ddLookup;
            _formAService = formAService;
            _userService = userService;
            _roadMasterService = roadMasterService;
            _formDservice = formDService;
            _formJservice = formJservice;

        }

        //Get User List
        [Route("api/userList")]
        [HttpGet]
        public async Task<IActionResult> userList()
        {
            try
            {
                //IEnumerable<SelectListItem> listItems = await _userService.GetUserList();
                IEnumerable<SelectListItem> listItems = _userService.GetUserSelectList(null);
                return RAMMSApiSuccessResponse(listItems);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }



        // Get Desc with Type
        [Route("api/ddlist")]
        [HttpPost]
        public async Task<IActionResult> AllDDListItems([FromBody] object ddListObj)
        {
            try
            {
                DDLookUpDTO request = JsonConvert.DeserializeObject<DDLookUpDTO>(ddListObj.ToString());
                IEnumerable<SelectListItem> listItems = await _DDLookUpService.GetDdLookup(request)
                ; return RAMMSApiSuccessResponse(listItems);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        // Get Activity Code // Get Type Code And Tpye Value by type
        [Route("api/ddlcodeDesc")]
        [HttpPost]
        public async Task<IActionResult> GetTypeCodeAndValue([FromBody] object ddListObj)
        {
            try
            {
                DDLookUpDTO request = JsonConvert.DeserializeObject<DDLookUpDTO>(ddListObj.ToString());
                IEnumerable<SelectListItem> listItems = await _DDLookUpService.GetLookUpCodeDesc(request);
                return RAMMSApiSuccessResponse(listItems);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Route("api/ddlRoadCodeConcat")]
        [HttpGet]
        public async Task<IActionResult> GetConcatCodeValue()
        {
            try
            {
                IEnumerable<SelectListItem> listItems = await _roadMasterService.GetAllRoadCodeAndNameTab();
                return RAMMSApiSuccessResponse(listItems);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        //[Authorize]
        [Route("api/RodeCodeByRmu")]
        [HttpPost]
        public async Task<IActionResult> GetRdCodeByRmu(string rmu)
        {
            try
            {
                IEnumerable<SelectListItem> response = await _formDservice.GetRoadCodesByRMU(rmu);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }


        // Get Distress Code from Distress Table
        [Route("api/distressCode")]
        [HttpPost]
        public async Task<IActionResult> GetDistressCode(string assetGroup)
        {
            try
            {                
                IEnumerable<SelectListItem> listItems = await _formAService.GetDefectCodeService(assetGroup);
                return RAMMSApiSuccessResponse(listItems);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }


        [Route("api/ddMonthlist")]
        [HttpGet]
        public IActionResult GetDDMonthList()
        {
            try
            {
                List<SelectListItem> monthList = _ddLookupBO.GetMonth();

                return RAMMSApiSuccessResponse(monthList);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Route("api/ddWeeklist")]
        [HttpGet]
        public IActionResult GetDDWeekList()
        {
            try
            {
                List<SelectListItem> weekList = _ddLookupBO.GetWeekNo();

                return RAMMSApiSuccessResponse(weekList);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Route("api/sectionByRmuMaster")]
        [HttpPost]
        public async Task<IActionResult> GetSectionByRmu(string rmu)
        {
            try
            {
                IEnumerable<SelectListItem> response = await _formDservice.GetSectionCodesByRMU(rmu);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Route("api/landingDropdown")]
        [HttpPost]
        public async Task<IActionResult> GetLandingDropDown([FromBody] object formA)
        {
            try
            {
                //RequestDropdownFormA requestDtl = JsonConvert.DeserializeObject<RequestDropdownFormA>(formA.ToString());
                //FormASearchDropdown ddvalues = _formJservice.GetDropdown(requestDtl);
                AssetDDLRequestDTO request = JsonConvert.DeserializeObject<AssetDDLRequestDTO>(formA.ToString());
                AssetDDLResponseDTO result = await _roadMasterService.GetAssetDDL(request);
                return RAMMSApiSuccessResponse(result);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }
        

        [Route("api/getrmroadcode")]
        [HttpPost]
        public async Task<IActionResult> GetRM_RoadCode_Service([FromBody] object _Rmroad)
        {
            try
            {
                RoadMasterRequestDTO request = JsonConvert.DeserializeObject<RoadMasterRequestDTO>(_Rmroad.ToString());
                RoadMasterResponseDTO listItems = await _roadMasterService.GetAllRoadCodeData(request);
                return RAMMSApiSuccessResponse(listItems);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Route("api/ddlistvalue")]
        [HttpPost]
        public async Task<IActionResult> AllDDListItemsValue([FromBody] object ddListObj)
        {
            try
            {
                DDLookUpDTO request = JsonConvert.DeserializeObject<DDLookUpDTO>(ddListObj.ToString());
                IEnumerable<SelectListItem> listItems = await _DDLookUpService.GetDdLookupValue(request)
                ; return RAMMSApiSuccessResponse(listItems);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Route("api/ddlcodeValueConcat")]
        [HttpPost]
        public async Task<IActionResult> GetTypeCodeAndValueConcat([FromBody] object ddListObj)
        {
            try
            {
                DDLookUpDTO request = JsonConvert.DeserializeObject<DDLookUpDTO>(ddListObj.ToString());
                IEnumerable<SelectListItem> listItems = await _DDLookUpService.GetLookUpCodeTextConcat(request);
                return RAMMSApiSuccessResponse(listItems);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Route("api/crewSupervisor")]
        [HttpGet]
        public async Task<IActionResult> GetSupervisor()
        {
            try
            {
                IEnumerable<SelectListItem> listItems = await _userService.GetSupervisor();
                return RAMMSApiSuccessResponse(listItems);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Route("api/roadcodebySection")]
        [HttpPost]
        public async Task<IActionResult> GetRoadCodeBySection(string seccode)
        {
            try
            {
                IEnumerable<SelectListItem> listItems = await _formDservice.GetRoadCodeBySectionCode(seccode);
                return RAMMSApiSuccessResponse(listItems);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }


        [Route("api/ddldescvalue")]
        [HttpPost]
        public async Task<IActionResult> GetDescValue([FromBody] object ddListObj)
        {
            try
            {
                DDLookUpDTO request = JsonConvert.DeserializeObject<DDLookUpDTO>(ddListObj.ToString());
                IEnumerable<SelectListItem> listItems = await _DDLookUpService.GetDdDescValue(request);
                return RAMMSApiSuccessResponse(listItems);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Route("api/ddldescvalueconcat")]
        [HttpPost]
        public async Task<IActionResult> GetDescValueConcat([FromBody] object ddListObj)
        {
            try
            {
                DDLookUpDTO request = JsonConvert.DeserializeObject<DDLookUpDTO>(ddListObj.ToString());
                IEnumerable<SelectListItem> listItems = await _DDLookUpService.GetLookUpTextDescConcat(request);
                return RAMMSApiSuccessResponse(listItems);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

       
        [Route("api/GetInsUser")]
        [HttpGet]
        public async Task<IActionResult> GetFCUser()
        {
            var user = await _userService.GetUser();
            var response = new List<SelectListItem>();
            foreach (var iUser in user)
            {
                var data = new SelectListItem() { Text = iUser.Text, Value = iUser.Value };
                response.Add(data);
            }
            return RAMMSApiSuccessResponse(response);
        }
    }
}