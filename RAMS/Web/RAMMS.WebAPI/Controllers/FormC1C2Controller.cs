using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAMMS.WebAPI.Controllers
{
    public class FormC1C2Controller : Controller
    {

        private readonly IFormC1C2Service _formC1C2Service;
        private readonly IDDLookUpService _ddLookupService;
        private readonly ISecurity _security;
        public FormC1C2Controller(IFormC1C2Service formC1C2Service, ISecurity security, IDDLookUpService dDLookUpService)
        {
            this._formC1C2Service = formC1C2Service;
            this._ddLookupService = dDLookUpService;
            this._security = security;
        }

        [Authorize]
        [Route("api/getC1C2GridData")]
        [HttpPost]
        public async Task<IActionResult> GetFilteredFormC1C22Grid([FromBody] object landingGrid)
        {
            DataTableAjaxPostModel requestDtl = JsonConvert.DeserializeObject<DataTableAjaxPostModel>(landingGrid.ToString());
            if (requestDtl.order != null && requestDtl.order.Count > 0)
            {
                requestDtl.order = requestDtl.order.Select(x => { if (x.column == 4 || x.column == 1 || x.column == 9) { x.column = 16; } return x; }).ToList();
            }
            var respons = await _formC1C2Service.GetHeaderGrid(requestDtl);
            return RAMMSApiSuccessResponse(respons);
        }

        [Authorize]
        [Route("api/deleteC1C2")]
        [HttpPost]
        public IActionResult DeActivateC1C2(int id)
        {
            return RAMMSApiSuccessResponse(_formC1C2Service.Delete(id));            
        }

        [Authorize]
        [Route("api/findDetailsC1C2")]
        [HttpPost]
        public async Task<IActionResult> FindDetailsC1C2([FromBody] object frmC1C2)
        {
            FormC1C2DTO requestDtl = JsonConvert.DeserializeObject<FormC1C2DTO>(frmC1C2.ToString());
            return RAMMSApiSuccessResponse(await _formC1C2Service.FindDetails(requestDtl, _security.UserID));
        }

        [Authorize]
        [Route("api/updateC1C2")]
        [HttpPost]
        public async Task<IActionResult> UpdateC1C2([FromBody] object frmC1C2)
        {
            FormC1C2DTO requestDtl = JsonConvert.DeserializeObject<FormC1C2DTO>(frmC1C2.ToString());
            requestDtl.ModBy = _security.UserID;
            requestDtl.ModDt = DateTime.UtcNow;
            var result = await _formC1C2Service.Save(requestDtl, requestDtl.SubmitSts);
            return RAMMSApiSuccessResponse(result.PkRefNo);
        }

        [Authorize]
        [Route("api/getC1C2ById")]
        [HttpPost]
        public async Task<IActionResult> GetC1C2ById(int id)
        {
            return RAMMSApiSuccessResponse(await _formC1C2Service.FindByHeaderID(id));
        }

        [Authorize]
        [Route("api/getC1C2List")]
        [HttpPost]
        public async Task<IActionResult> GetC1C2([FromBody] object request)
        {
            AssetDDLRequestDTO requestDtl = JsonConvert.DeserializeObject<AssetDDLRequestDTO>(request.ToString());
            IEnumerable<SelectListItem> listItems = await _formC1C2Service.GetCVIds(requestDtl);
            return RAMMSApiSuccessResponse(listItems);
        }

        [Authorize]
        [Route("api/getC1C2ImageList")]
        [HttpPost]
        public IActionResult GetC1C2Image(int id)
        {
            List<FormC1C2ImageDTO> listItems = _formC1C2Service.ImageList(id);
            return RAMMSApiSuccessResponse(listItems);
        }

        [Authorize]
        [Route("api/deleteC1C2Image")]
        [HttpPost]
        public async Task<IActionResult> DeleteC1C2Image(int headerid, int imgId)
        {
            int response = await _formC1C2Service.DeleteImage(headerid, imgId);
            return RAMMSApiSuccessResponse(response);
        }
    }
}
