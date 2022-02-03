using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RAMMS.Business.ServiceProvider.Interfaces;
//using RAMMS.Common.ServiceProvider;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Business.ServiceProvider;


namespace RAMMS.WebAPI.Controllers
{
    public class ERTController : Controller
    {        

        private readonly IFormXService _IFormXService;


        [Route("api/ERTsaveformhdr")]
        [HttpPost]
        public async Task<IActionResult> SaveFormAHdr([FromBody] object SaveObj)
        {
            try
            {
                FormXHeaderRequestDTO request = JsonConvert.DeserializeObject<FormXHeaderRequestDTO>(SaveObj.ToString());
                FormXHeaderResponseDTO response = await _IFormXService.SaveHeaderwithResponse(request);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
