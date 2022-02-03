using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO;
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
    public class FormF2Controller : Controller
    {
        private readonly IFormF2Service _formF2Service;
        private readonly IDDLookUpService _ddLookupService;
        public FormF2Controller(IFormF2Service formF2Service, IDDLookUpService dDLookUpService)
        {
            this._formF2Service = formF2Service;
            this._ddLookupService = dDLookUpService;
        }

        [Authorize]
        [Route("api/getF2GridData")]
        [HttpPost]
        public async Task<IActionResult> GetFilteredF2Grid([FromBody] object landingGrid)
        {
            try
            {
                FilteredPagingDefinition<FormF2SearchGridDTO> requestDtl = JsonConvert.DeserializeObject<FilteredPagingDefinition<FormF2SearchGridDTO>>(landingGrid.ToString());

                PagingResult<FormF2HeaderRequestDTO> rst = await _formF2Service.GetHeaderList(requestDtl);

                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }


        //Dropdown
        [Route("api/formF2Dropdown")]
        [HttpPost]
        public async Task<IActionResult> RMUSecRoad([FromBody] object request)
        {
            AssetDDLRequestDTO request1 = JsonConvert.DeserializeObject<AssetDDLRequestDTO>(request.ToString());
            AssetDDLResponseDTO assetDDLResponseDTO = await _formF2Service.GetAssetDDL(request1);
            return RAMMSApiSuccessResponse(assetDDLResponseDTO);
        }


        //GetHeaderById
        [Route("api/GetF2HeaderById")]
        [HttpPost]
        public async Task<IActionResult> GetF2HeaderById(int id)
        {
            FormF2HeaderRequestDTO _model = new FormF2HeaderRequestDTO();
            if (id > 0)
            {
                _model = await _formF2Service.GetHeaderById(id);
                _model = _model ?? new FormF2HeaderRequestDTO();
            }
            return RAMMSApiSuccessResponse(_model);
        }

        //Detail Grid
        [Route("api/GetF2DetailList")]
        [HttpPost]
        public async Task<IActionResult> GetF2DetailList([FromBody] object searchData)
        {
            FilteredPagingDefinition<FormF2DetailRequestDTO> requestDtl = JsonConvert.DeserializeObject<FilteredPagingDefinition<FormF2DetailRequestDTO>>(searchData.ToString());            
            var result = await _formF2Service.GetDetailList(requestDtl);
            return RAMMSApiSuccessResponse(result);
        }

        [Route("api/GetF2AIBound")]
        [HttpPost]
        public async Task<IActionResult> GetAIBound(string roadcode, string locationch, string structurecode)
        {
            return RAMMSApiSuccessResponse(await _formF2Service.GetAIBound(roadcode, locationch, structurecode));
        }

        [Route("api/SaveF2Dtl")]
        [HttpPost]
        public async Task<IActionResult> SaveF2Detail([FromBody] object  model)
        {
            FormF2DetailRequestDTO data = JsonConvert.DeserializeObject<FormF2DetailRequestDTO>(model.ToString());
            return RAMMSApiSuccessResponse(await _formF2Service.SaveDetail(data));
        }

        [Route("api/deleteHeader")]
        [HttpPost]
        public async Task<IActionResult> RemoveHeader(int id)
        {
            return RAMMSApiSuccessResponse(await _formF2Service.RemoveHeader(id));
        }

        [Route("api/roadLength")]
        [HttpPost]
        public async Task<IActionResult> GetRoadLength(string roadcode)
        {
            return RAMMSApiSuccessResponse(await _formF2Service.TotalLength(roadcode));
        }

        [Route("api/formF2FindDetails")]
        [HttpPost]
        public async Task<IActionResult> SaveHeader([FromBody] object model)
        {
            FormF2HeaderRequestDTO data = JsonConvert.DeserializeObject<FormF2HeaderRequestDTO>(model.ToString());
            FormF2HeaderRequestDTO _model = new FormF2HeaderRequestDTO();
            int res = await _formF2Service.SaveHeader(data);
            if (res != 0)
            {
                _model = await _formF2Service.GetHeaderById(res);
                _model = _model ?? new FormF2HeaderRequestDTO();              
            }
            return RAMMSApiSuccessResponse(_model);
        }

        [Route("api/updateF2Header")]
        [HttpPost]
        public async Task<IActionResult> UpdateF2Header([FromBody] object model)
        {
            FormF2HeaderRequestDTO data = JsonConvert.DeserializeObject<FormF2HeaderRequestDTO>(model.ToString());
            if (data.SubmitSts)
            {
                var list = await _formF2Service.GetF2DetailList(data.PkRefNo);
                if (list.All(x => x.GrCondition1 != null || x.GrCondition2 != null || x.GrCondition3 != null))
                {
                    return RAMMSApiSuccessResponse(await _formF2Service.UpdateF2Header(data));
                }
                else
                {
                    return RAMMSApiSuccessResponse(-1);
                }
            }
            else
            {
                return RAMMSApiSuccessResponse(await _formF2Service.UpdateF2Header(data));
            }

        }

        [Route("api/deleteF2Dtl")]
        [HttpPost]
        public async Task<IActionResult> RemoveF2Dtl(int id)
        {
            return RAMMSApiSuccessResponse(await _formF2Service.RemoveDetail(id));
        }
    }
}
