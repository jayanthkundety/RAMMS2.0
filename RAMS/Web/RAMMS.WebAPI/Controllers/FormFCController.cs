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


    //[Route("api/[controller]")]
    //[ApiController]
    public class FormFCController : Controller
    {
        private readonly IFormFCService _formFCService;
        private readonly IDDLookUpService _ddLookupService;
        private readonly ISecurity _security;
        private readonly IUserService _userService;
        private readonly IRoadMasterService _roadMasterService;

        public FormFCController(IFormFCService formFCService, ISecurity security,IDDLookUpService dDLookUpService, IUserService userService, IRoadMasterService roadMasterService)
        {
            this._formFCService = formFCService;
            this._ddLookupService = dDLookUpService;
            this._security = security;
            this._userService = userService;
            this._roadMasterService = roadMasterService;
        }
        [Authorize]
        [Route("api/getFCGridData")]
        [HttpPost]
        public async Task<IActionResult> GetFilteredFormFCGrid([FromBody] object landingGrid)
        {
            DataTableAjaxPostModel requestDtl = JsonConvert.DeserializeObject<DataTableAjaxPostModel>(landingGrid.ToString());
            if (requestDtl.order != null && requestDtl.order.Count > 0)
            {
                // var orderlist = new Order(); 
                // requestDtl.order = requestDtl.order.Select(x => { if (x.column == 4 || x.column == 1 || x.column == 9) { x.column = 16; } return x; }).ToList();
                var column = new Column();
                column.data = "RoadId";
                column.name = null;
                column.orderable = false;
                column.search = null;
                column.searchable = false;

                requestDtl.columns.Add(column);
            foreach(var item in requestDtl.order)
                {

                    if (item.column == 1)
                    {

                        int[] list = { 12, 2, 3 };

                        foreach (var citem in list)
                        {
                            var orderlist = new Order();
                            orderlist.column = citem;
                            orderlist.dir = item.dir;
                            requestDtl.order.Add(orderlist);
                        }
                        requestDtl.order.RemoveAt(0);
                        break;
                    }

                    else if (item.column == 2)
                    {
                        int[] list = { 2, 3 };

                        foreach (var citem in list)
                        {
                            var orderlist = new Order();
                            orderlist.column = citem;
                            orderlist.dir = item.dir;
                            requestDtl.order.Add(orderlist);
                        }
                        requestDtl.order.RemoveAt(0);
                        break;
                    }
                    else if (item.column == 9)
                    {
                        int[] list = { 12 };

                        foreach (var citem in list)
                        {
                            var orderlist = new Order();
                            orderlist.column = citem;
                            orderlist.dir = item.dir;
                            requestDtl.order.Add(orderlist);

                        }
                        requestDtl.order.RemoveAt(0);
                        break;
                    }
                }
            }
            var respons = await _formFCService.GetHeaderGrid(requestDtl);
            return RAMMSApiSuccessResponse(respons);
        }
        [Authorize]
        [Route("api/findDetailsFC")]
        [HttpPost]
        public async Task<IActionResult> FindDetailsFC([FromBody] object frmFC)
        {
            FormFCDTO requestDtl = JsonConvert.DeserializeObject<FormFCDTO>(frmFC.ToString());
            if (requestDtl.RoadCode != null)
            {
                var roadData = await _roadMasterService.GetByRdCode(requestDtl.RoadCode);
                requestDtl.RmuName = roadData?.RmuName;
                requestDtl.DivCode = roadData?.DivisionCode;
                requestDtl.RoadId = roadData?.No;
                requestDtl.RoadName = roadData?.RoadName;
                //requestDtl.SecName = roadData.SecName;
            }
            return RAMMSApiSuccessResponse(await _formFCService.FindDetails(requestDtl, _security.UserID));
           
        }


        //[Authorize]
        [Route("api/AssetCheckFC")]
        [HttpPost]
        public async Task<IActionResult> AssetCheckFC(string roadCode)
        {

            bool assetCheck = await _formFCService.AssetsCheck(roadCode);
            if (assetCheck)
            {
                return RAMMSApiSuccessResponse(true);
            }
            else
            {
                return RAMMSApiSuccessResponse(false);
            }

        }

        [Authorize]
        [Route("api/updateFC")]
        [HttpPost]
        public async Task<IActionResult> UpdateFC([FromBody] object frmFC)
        {
            FormFCDTO requestDtl = JsonConvert.DeserializeObject<FormFCDTO>(frmFC.ToString());
            requestDtl.ModBy = _security.UserID;
            requestDtl.ModDt = DateTime.UtcNow;
            var result = await _formFCService.Save(requestDtl, requestDtl.SubmitSts);
            return RAMMSApiSuccessResponse(result.PkRefNo);
        }

        [Authorize]
        [Route("api/getFCById")]
        [HttpPost]
        public async Task<IActionResult> GetFCById(int id)
        {
            return RAMMSApiSuccessResponse(await _formFCService.FindByHeaderID(id));
        }


        [Authorize]
        [Route("api/deleteFC")]
        [HttpPost]
        public IActionResult DeActivateFC(int id)
        {
            return RAMMSApiSuccessResponse(_formFCService.Delete(id));
        }

      
    }
}
