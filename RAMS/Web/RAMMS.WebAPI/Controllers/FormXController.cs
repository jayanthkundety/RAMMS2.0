using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RAMMS.Business.ServiceProvider.Interfaces;
//using RAMMS.Common.ServiceProvider;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.SearchBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.WebAPI
{
    public class FormXController : Controller
    {
        //private readonly IFormXProvider _IFormXProvider;

        private readonly IFormXService _IFormXService;

        public FormXController(IFormXService formXService)
        {
            _IFormXService = formXService;
        }

        //FormX Landing Grid
        //[Authorize]
        [Route("api/getformxgridlist")]
        [HttpPost]
        public async Task<IActionResult> GetFormXGridList([FromBody] object formxsearchgrid)
        {
            try
            {
                FilteredPagingDefinition<FormXSearchDTO> requestDtl = JsonConvert.DeserializeObject<FilteredPagingDefinition<FormXSearchDTO>>(formxsearchgrid.ToString());

                PagingResult<FormXHeaderResponseDTO> rst = await _IFormXService.GetFilteredFormXGrid(requestDtl);

                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        //FormX GetBy ID
        [Authorize]
        [Route("api/getFormXById")]
        [HttpPost]
        public async Task<IActionResult> GetFormXDetailsById(int formXId)
        {
            try
            {
                FormXHeaderRequestDTO response = await _IFormXService.GetFormXWithDetailsByNoAsync(formXId);
                return RAMMSApiSuccessResponse(response);
            }
            catch(Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }


        [Authorize]
        [Route("api/getFormXWarImageList")]
        [HttpPost]
        public async Task<IActionResult> GetFormXWar(int formXId)
        {
            try
            {
                List<WarImageDtlResponseDTO> response = await _IFormXService.GetWarImageList(formXId);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/getFormXUSeeU")]
        [HttpPost]
        public async Task<IActionResult> GetFormXUSeeU(int formXId)
        {
            try
            {
                List<AccUccImageDtlResponseDTO> response = await _IFormXService.GetAccUccImageList(formXId);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }
    }
}
