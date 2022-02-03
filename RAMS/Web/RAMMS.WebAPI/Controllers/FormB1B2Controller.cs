using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAMMS.WebAPI.Controllers
{
    public class FormB1B2Controller : Controller
    {
        private readonly IFormB1B2Service _formB1B2Service;
        private readonly IDDLookUpService _ddLookupService;
        public FormB1B2Controller(IFormB1B2Service formB1B2Service, IDDLookUpService dDLookUpService)
        {
            this._formB1B2Service = formB1B2Service;
            this._ddLookupService = dDLookUpService;
        }

        [Authorize]
        [Route("api/getB1B2GridData")]
        [HttpPost]
        public async Task<IActionResult> GetFilteredFormB1B2Grid([FromBody] object landingGrid)
        {
            try
            {
                FilteredPagingDefinition<FormB1B2SearchGridDTO> requestDtl = JsonConvert.DeserializeObject<FilteredPagingDefinition<FormB1B2SearchGridDTO>>(landingGrid.ToString());

                PagingResult<FormB1B2HeaderRequestDTO> rst = await _formB1B2Service.GetHeaderList(requestDtl);

                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize] // Bridge list
        [Route("api/getB1B2BridgeData")]
        [HttpPost]
        public IActionResult GetBridgeData([FromBody] object request)
        {
            try
            {
                AssetDDLRequestDTO requestDtl = JsonConvert.DeserializeObject<AssetDDLRequestDTO>(request.ToString());
                IEnumerable<SelectListItem> listItems = _formB1B2Service.GetBridgeIds(requestDtl);
                return RAMMSApiSuccessResponse(listItems);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize] // Bridge details list    //Not in use
        [Route("api/getB1B2BridgeById")]
        [HttpPost]
        public async Task<IActionResult> GetBridgeDataById(int BridgeId)
        {
            try
            {
                FormB1B2HeaderRequestDTO b1b2Dto = await _formB1B2Service.GetBrideDetailById(BridgeId);
                return RAMMSApiSuccessResponse(b1b2Dto);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize] //Save Header
        [Route("api/saveB1B2Header")]
        [HttpPost]
        public async Task<IActionResult> SaveB1B2HeaderData([FromBody] object data)
        {
            try
            {
                FormB1B2HeaderRequestDTO model = JsonConvert.DeserializeObject<FormB1B2HeaderRequestDTO>(data.ToString());
                //if (model.PkRefNo != 0)
                //{
                //    //var response = await _formB1B2Service.GetHeaderById(model.PkRefNo);
                //    //model.SignpathAud = model.SignpathAud ?? response.SignpathAud ?? null;
                //    //model.SignpathSerProvider = model.SignpathSerProvider ?? response.SignpathSerProvider ?? null;
                //    int _id = await _formB1B2Service.SaveHeader(model);
                //    return RAMMSApiSuccessResponse(model);
                //}
                //else
                //if()
                //{
                    var exist = await _formB1B2Service.AlreadyExists(model.AiPkRefNo.Value, model.YearOfInsp.Value);
                    if (!exist.IsExist && model.AiPkRefNo.HasValue)
                    {
                        var _model = await _formB1B2Service.GetBrideDetailById(model.AiPkRefNo.Value);
                        _model.DtOfInsp = model.DtOfInsp;
                        _model.RecordNo = model.RecordNo;
                        _model.YearOfInsp = model.YearOfInsp;
                        _model.DisplayAssetId = model.DisplayAssetId;
                        int _id = await _formB1B2Service.SaveHeader(_model);
                        var response = await _formB1B2Service.GetHeaderById(_id);
                        return RAMMSApiSuccessResponse(response);
                    }
                    else
                    {
                        var response = await _formB1B2Service.GetHeaderById(exist.PkRefNo);
                        return RAMMSApiSuccessResponse(response);
                    }
                //}
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        //[Authorize] // Get Header by Id
        [Route("api/getB1B2ById")]
        [HttpPost]
        public async Task<IActionResult> GetB1B2ById(int BridgeId)
        {
            try
            {
                FormB1B2HeaderRequestDTO b1b2Dto = await _formB1B2Service.GetHeaderById(BridgeId);
                return RAMMSApiSuccessResponse(b1b2Dto);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }


        [Authorize] // DectivateAssetImage
        [Route("api/deactivateb1b2Image")]
        [HttpPost]
        public async Task<IActionResult> DectivateB1B2Image(int imageId)
        {
            try
            {
                int rowsAffected = await _formB1B2Service.DectivateAssetImage(imageId);
                return RAMMSApiSuccessResponse(rowsAffected);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize] // 
        [Route("api/deleteHdrB1B2")]
        [HttpPost]
        public async Task<IActionResult> RemoveHeaderB1B2(int id)
        {
            return RAMMSApiSuccessResponse(await _formB1B2Service.RemoveHeader(id));
        }


        //[Authorize] // DectivateAssetImage
        [Route("api/getB1B2Images")]
        [HttpPost]
        public async Task<IActionResult> GetAllImageByAssetPK(int assetId)
        {
            try
            {
                List<FormB1B2ImgRequestDTO>  response = await _formB1B2Service.GetAllImageByAssetPK(assetId);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        //[Authorize]
        [Route("api/updateB1B2")]
        [HttpPost]
        public async Task<IActionResult> UpdateB1B2([FromBody] object data)
        {
            FormB1B2HeaderRequestDTO model = JsonConvert.DeserializeObject<FormB1B2HeaderRequestDTO>(data.ToString());
            if (model.PkRefNo != 0)
            {
                //var response = await _formB1B2Service.GetHeaderById(model.PkRefNo);
                //model.SignpathAud = model.SignpathAud ?? response.SignpathAud ?? null;
                //model.SignpathSerProvider = model.SignpathSerProvider ?? response.SignpathSerProvider ?? null;
                int _id = await _formB1B2Service.UpdateB1B2(model);
                return RAMMSApiSuccessResponse(_id);
            }
            return RAMMSApiSuccessResponse(0);
        }

        [Route("api/formB1B2Dropdown")]
        [HttpPost]
        public async Task<IActionResult> B1B2RMUSecRoad([FromBody] object request)
        {
            AssetDDLRequestDTO request1 = JsonConvert.DeserializeObject<AssetDDLRequestDTO>(request.ToString());
            AssetDDLResponseDTO assetDDLResponseDTO = await _formB1B2Service.GetAssetDDL(request1);
            if (assetDDLResponseDTO.RMU != null)
            {
                foreach (var _rmu in assetDDLResponseDTO.RMU)
                {
                    _rmu.Text = _rmu?.Value + "-" + _rmu?.Text;
                }
            }
            return RAMMSApiSuccessResponse(assetDDLResponseDTO);
        }
    }
}
