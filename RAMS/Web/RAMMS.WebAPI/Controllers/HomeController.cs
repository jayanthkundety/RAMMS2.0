using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RAMMS.Business.ServiceProvider.Services;
using RAMMS.DTO.RequestBO;

namespace RAMMS.WebAPI.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILandingHomeService _landingHomeService;
        public HomeController(ILandingHomeService landingHomeService)
        {
            _landingHomeService = landingHomeService;
        }

        [Authorize]
        [Route("api/homeSectionDrop")]
        [HttpPost]
        public async Task<IActionResult> GetSectionbyRMU([FromBody] object landingHomeRequestDTO)
        {
            try
            {
                LandingHomeRequestDTO request = JsonConvert.DeserializeObject<LandingHomeRequestDTO>(landingHomeRequestDTO.ToString());
                
                var result = await _landingHomeService.GetSectionByRMU(request);
                return RAMMSApiSuccessResponse(result);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/homeNodCount")]
        [HttpPost]
        public async Task<IActionResult> GetNodClosedResult([FromBody] object landingHomeRequestDTO)
        {
            try
            {
                LandingHomeRequestDTO request = JsonConvert.DeserializeObject<LandingHomeRequestDTO>(landingHomeRequestDTO.ToString());
                var result = await _landingHomeService.GetHomeActiveCount(request);
                return RAMMSApiSuccessResponse(result);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/homeNCNCount")]
        [HttpGet]
        public async Task<IActionResult> getNCNCount()
        {
            try
            {
                var result = await _landingHomeService.getNCNActiveCount();
                return RAMMSApiSuccessResponse(result);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/homeNCRCount")]
        [HttpGet]
        public async Task<IActionResult> getNCRCount()
        {
            try
            {
                var result = await _landingHomeService.getNCRActiveCount();
                return RAMMSApiSuccessResponse(result);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }
    }
}
