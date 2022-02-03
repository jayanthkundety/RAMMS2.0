using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Business.ServiceProvider.Services;
using RAMMS.DTO.RequestBO;
using RAMMS.Repository.Interfaces;
using RAMMS.Web.UI.Models;
using System.Runtime.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using RAMMS.Domain.Models;
using RAMMS.Common;

namespace RAMMS.Web.UI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDDLookUpService _ddLookupService;
        private readonly IFormAService _formAService;
        private readonly IRoadMasterRepository _rmService;
        private readonly ILandingHomeService _landingHomeService;
        public HomeController(ILogger<HomeController> logger, IDDLookUpService ddLookupService, IFormAService formAService, IRoadMasterRepository rmService, ILandingHomeService landingHomeService)
        {
            _logger = logger;
            _ddLookupService = ddLookupService;
            _formAService = formAService;
            _rmService = rmService;
            _landingHomeService = landingHomeService;
        }


        public async Task<IActionResult> Index()
        {
            //Sample Logging Mechanism
            //_logger.LogInformation("This is Information Log");
            //_logger.LogWarning("This is Warning Log");
            //_logger.LogError("This is Error Log");
            //_logger.LogCritical("This is Critical Log");

            await LoadDropDowns();
            return View();
        }

        public async Task LoadDropDowns()
        {
            DDLookUpDTO ddlookup = new DDLookUpDTO();

            ddlookup.Type = "RMU";
            ViewData["RMU"] = await _ddLookupService.GetLookUpCodeDesc(ddlookup);
            ddlookup.Type = "Section Code";
            ViewData["Section"] = await _ddLookupService.GetDdLookup(ddlookup);
        }
        public async Task<IActionResult> GetRMUSection(string RMU)
        {
            try
            {
                var obj = await _formAService.GetSectionByRMU(RMU);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ERT()
        {
            return View();
        }
        public IActionResult Bridge()
        {
            return View("~/Views/Asset/Bridge.cshtml");
        }
        //[CAuthorize(GroupName = GroupName.ERT)]
        public IActionResult Section(string rmu)
        {
            ViewBag.rmu = rmu;
            var data = _rmService.GetAll().Where(x => x.RdmActiveYn == true).Select(x => new { x.RdmSecName, x.RdmRdName, x.RdmRdCode }).OrderBy(x => x.RdmRdName).ToList();
            IDictionary<string, object> lstData = new Dictionary<string, object>();
            var distCode = data.Select(x => x.RdmSecName).Distinct().ToList();
            foreach (string strSec in distCode)
            {
                lstData.Add(strSec.ToLower(), data.Where(x => x.RdmSecName == strSec).ToList());
            }

            return View("~/Views/Home/Section.cshtml", lstData);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult DevProgress()
        {
            return View("~/Views/DevelopmentProgress.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> GetClosedNODResult(LandingHomeRequestDTO landingHomeRequestDTO)
        {
            try
            {
                var result = await _landingHomeService.GetNodActiveCount(landingHomeRequestDTO);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> GetSectionbyRMU(LandingHomeRequestDTO landingHomeRequestDTO)
        {
            try
            {
                var result = await _landingHomeService.GetSectionByRMU(landingHomeRequestDTO);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> GetNCNToBeClosed()
        {
            try
            {
                int result = await _landingHomeService.getNCNActiveCount();
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> GetNCRToBeClosed()
        {
            try
            {
                int result = await _landingHomeService.getNCRActiveCount();
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> GetHomePageActiveCount(LandingHomeRequestDTO requestDTO)
        {
            try
            {
                var result = await _landingHomeService.GetHomeActiveCount(requestDTO);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GlobalSearchData([FromForm(Name = "query")] string query)
        {
            List<AutoComplete> lstAuto = new List<AutoComplete>();
            var result = await _landingHomeService.GlobalSearchData(query);
            result.ForEach((UvwSearchData data) =>
            {
                lstAuto.Add(new AutoComplete() { data = data, value = data.Display });
            });
            return Json(lstAuto, Utility.JsonOption);
        }
    }
}
