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
    public class FormFDController : Controller
    {
        private readonly IFormFDService _formFDService;
        private readonly IDDLookUpService _ddLookupService;
        private readonly ISecurity _security;
        private readonly IRoadMasterService _roadMasterService;

        public FormFDController(IFormFDService formFDService, ISecurity security, IDDLookUpService dDLookUpService, IRoadMasterService roadMasterService)
        {
            this._formFDService = formFDService;
            this._ddLookupService = dDLookUpService;
            this._security = security;
            this._roadMasterService = roadMasterService;
        }
        //[Authorize]
        [Route("api/getFDGridData")]
        [HttpPost]
        public async Task<IActionResult> GetFilteredFormFDGrid([FromBody] object landingGrid)
        {
            DataTableAjaxPostModel requestDtl = JsonConvert.DeserializeObject<DataTableAjaxPostModel>(landingGrid.ToString());
            if (requestDtl.order != null && requestDtl.order.Count > 0)
            {
                var column = new Column();
                column.data = "RoadId";
                column.name = null;
                column.orderable = false;
                column.search = null;
                column.searchable = false;

                requestDtl.columns.Add(column);
                foreach (var item in requestDtl.order)
                {

                    if (item.column == 1)
                    {

                        int[] list = { 12, 2 };

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
                    else if (item.column == 8)
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
            var respons = await _formFDService.GetFormFDHeaderGrid(requestDtl);
            return RAMMSApiSuccessResponse(respons);
        }
        [Authorize]
        [Route("api/findDetailsFD")]
        [HttpPost]
        public async Task<IActionResult> FindDetailsFD([FromBody] object frmFD)
        {
            FormFDDTO requestDtl = JsonConvert.DeserializeObject<FormFDDTO>(frmFD.ToString());
            if(requestDtl.RoadCode !=null)
            {
                var roadData = await _roadMasterService.GetByRdCode(requestDtl.RoadCode);
                requestDtl.RmuName = roadData?.RmuName;
                requestDtl.DivCode = roadData?.DivisionCode;
                requestDtl.RoadId = roadData?.No;
                requestDtl.RoadName = roadData?.RoadName;
                //requestDtl.SecName = roadData.SecName;
            }
            return RAMMSApiSuccessResponse(await _formFDService.FindDetails(requestDtl, _security.UserID));
        }

        [Authorize]
        [Route("api/updateFD")]
        [HttpPost]
        public async Task<IActionResult> UpdateFD([FromBody] object frmFD)
        {
            FormFDDTO requestDtl = JsonConvert.DeserializeObject<FormFDDTO>(frmFD.ToString());
            requestDtl.ModBy = _security.UserID;
            requestDtl.ModDt = DateTime.UtcNow;
            var result = await _formFDService.Save(requestDtl, requestDtl.SubmitSts);
            return RAMMSApiSuccessResponse(result.PkRefNo);
        }

        //[Authorize]
        [Route("api/getFDById")]
        [HttpPost]
        public async Task<IActionResult> GetFDById(int id)
        {
            return RAMMSApiSuccessResponse(await _formFDService.FindByHeaderID(id));
        }


        [Authorize]
        [Route("api/deleteFD")]
        [HttpPost]
        public IActionResult DeActivateFD(int id)
        {
            return RAMMSApiSuccessResponse(_formFDService.Delete(id));
        }

        [Authorize]
        [Route("api/AssetCheckFD")]
        [HttpPost]
        public async Task<IActionResult> AssetCheckFD(string roadCode)
        {

            bool assetCheck = await _formFDService.AssetsCheck(roadCode);
            if (assetCheck)
            {
                return RAMMSApiSuccessResponse(true);
            }
            else
            {
                return RAMMSApiSuccessResponse(false);
            }

        }
    }
}
