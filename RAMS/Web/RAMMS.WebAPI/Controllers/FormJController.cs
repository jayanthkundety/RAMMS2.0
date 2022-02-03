using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.WebAPI.Controllers
{
    public class FormJController : Controller
    {
        private readonly IFormJServices _formJServices;
        private readonly IFormJImageService _formJImageService;
        public FormJController(IFormJServices formJService, IFormJImageService formJImageService)
        {
            _formJServices = formJService;
            _formJImageService = formJImageService;
        }


        ////[//Authorize]        // FormJ Landing Page Grid with Detail and Smart Search
        [Route("api/GetFormJLandingGrid")]
        [HttpPost]
        public async Task<IActionResult> GetFormJGridList([FromBody] object formJobj)
        {
            try
            {
                FilteredPagingDefinition<FormJSearchGridDTO> requestDtl = JsonConvert.DeserializeObject<FilteredPagingDefinition<FormJSearchGridDTO>>(formJobj.ToString());

                PagingResult<FormJHeaderResponseDTO> rst = await _formJServices.GetFilteredFormJGrid(requestDtl);

                if (rst.PageResult.Count > 0)
                {
                    for (int i = 0; i < rst.PageResult.Count; i++)
                    {
                        rst.PageResult[i].MonthYear = ((rst.PageResult[i].Month ?? 0) < 10 ? "0" : "") + (rst.PageResult[i].Month ?? 0) + "/" + (rst.PageResult[i].Year.HasValue ? rst.PageResult[i].Year.Value.ToString() : "2020");
                        rst.PageResult[i].Status = rst.PageResult[i].SubmitSts ? "Submitted" : "Saved";
                    }
                }

                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        //[Authorize]  // FormJ Detail Grid 
        [Route("api/GetFormJDetailGrid")]
        [HttpPost]
        public async Task<IActionResult> GetFormJDtlGridList([FromBody] object formJobj)
        {
            try
            {
                FilteredPagingDefinition<FormJDetailsRequestDTO> requestDtl = JsonConvert.DeserializeObject<FilteredPagingDefinition<FormJDetailsRequestDTO>>(formJobj.ToString());

                PagingResult<FormJDetailResponseDTO> rst = await _formJServices.GetFormADetailGrid(requestDtl);

                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        //[Authorize]
        [Route("api/FormJRefNo")]
        [HttpPost]
        public async Task<IActionResult> GetFormJRefNo(string roadCode, int month, int year, string assetGroup)
        {
            try
            {

                string isexist = _formJServices.CheckAlreadyExists(roadCode, month, year, assetGroup);
                assetGroup = _formJServices.GetAssetCodeByName(assetGroup);
                if (string.IsNullOrEmpty(isexist))
                {
                    isexist = (await _formJServices.GetLastInsertedHeader()).ToString();
                }
                if (!string.IsNullOrEmpty(assetGroup))
                {
                    var AutoRefNo = $"NOD/Form J/{roadCode}/{month}/{assetGroup}/{String.Format("{0:0000}", isexist)}-" + year;
                    return RAMMSApiSuccessResponse(AutoRefNo);
                }
                return RAMMSApiSuccessResponse("");
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        //[Authorize]  // save FormJ Header or return FormJ If already exists.
        [Route("api/SaveFormJHdr")]
        [HttpPost]
        public async Task<IActionResult> SaveFormJHdr([FromBody] object SaveObj)
        {
            try
            {
                FormJHeaderRequestDTO request = JsonConvert.DeserializeObject<FormJHeaderRequestDTO>(SaveObj.ToString());
                FormJHeaderResponseDTO response = await _formJServices.SaveHeaderWithResponse(request);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }


        //[Authorize]  // save FormJ Detail and return FormJ HeaderId.
        [Route("api/SaveFormJDtl")]
        [HttpPost]
        public async Task<IActionResult> SaveFormJDtl([FromBody] object SaveObj)
        {
            try
            {
                FormJDetailsRequestDTO request = JsonConvert.DeserializeObject<FormJDetailsRequestDTO>(SaveObj.ToString());
                int? response = await _formJServices.SaveDetailforHeaderV1(request);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }


        //[Authorize]  // Delete FormJ Header.
        [Route("api/DeleteFormJHdr")]
        [HttpPost]
        public async Task<IActionResult> DeleteFormJHdr(int formJHdr)
        {
            try
            {
                int? response = await _formJServices.DeActivateFormAAsync(formJHdr);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }


        //[Authorize]  // Delete FormJ Dtl.
        [Route("api/DeleteFormJDtl")]
        [HttpPost]
        public async Task<IActionResult> DeleteFormJDtl(int formJDtl)
        {
            try
            {
                int? response = await _formJServices.DeActivateDetail(formJDtl);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }


        //[Authorize]  // Get FormJ Details with Header
        [Route("api/GetFormJHdrandDtl")]
        [HttpPost]
        public async Task<IActionResult> GetFormJHdrandDtl(int formJHdr)
        {
            try
            {
                FormJHeaderRequestDTO response = await _formJServices.GetFormAWithDetailsByNoAsync(formJHdr);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }


        //[Authorize]  // Get FormJ Header By Id
        [Route("api/GetFormJHdrById")]
        [HttpPost]
        public async Task<IActionResult> GetFormJHdr(int formJHdr)
        {
            try
            {
                FormJHeaderResponseDTO response = await _formJServices.GetHeaderById(formJHdr);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }


        //[Authorize]  // Get FormJ Details By Id
        [Route("api/GetFormJDtlById")]
        [HttpPost]
        public async Task<IActionResult> GetFormJDtl(int formJDtl)
        {
            try
            {
                FormJDetailsRequestDTO response = await _formJServices.GetDetailById(formJDtl);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        //[Authorize]  // Get Asset Code By Name
        [Route("api/GetAssetCodeByNameJ")]
        [HttpPost]
        public async Task<IActionResult> AssetCodeByName(string name)
        {
            try
            {
                string response = _formJServices.GetAssetCodeByName(name);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        //[Authorize]  // Get Detail SRNO
        [Route("api/GetFormJDtlSrno")]
        [HttpPost]
        public async Task<IActionResult> GetDetailSrno(int headerId)
        {
            try
            {
                int response = await _formJServices.LastInsertedFormDetailSRNO(headerId);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        //[Authorize]  // Get FormJ DefectCode
        [Route("api/GetFormJDefectCode")]
        [HttpPost]
        public async Task<IActionResult> FormJDefectCode(string assetGroup)
        {
            try
            {
                IEnumerable<SelectListItem> response = await _formJServices.GetDefectCodeServiceConCat(assetGroup);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        //[Authorize]  // Save Signature
        [Route("api/FormJUpdateSignature")]
        [HttpPost]
        public async Task<IActionResult> UpdateSignatureFormJ([FromBody] object formJobj)
        {
            try
            {
                FormJHeaderRequestDTO requestDtl = JsonConvert.DeserializeObject<FormJHeaderRequestDTO>(formJobj.ToString());
                int rst = await _formJServices.UpdateFormJSignature(requestDtl);
                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        //Images
        [HttpPost] // FormJ Image Retrive from TAB
        [Route("api/FormJGetImage")]
        public async Task<IActionResult> GetImagesFormJ(string AssetId)
        {
            try
            {
                int assetId = Convert.ToInt32(AssetId);
                List<FormJImageListRequestDTO> response = new List<FormJImageListRequestDTO>();
                response = await _formJImageService.GetAllImageByAssetPK(assetId);
                return this.RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Route("api/deleteImageFormJ")]
        [HttpPost]
        public async Task<IActionResult> DeleteImageFormJ(int imageId)
        {
            try
            {
                int rst = await _formJImageService.DectivateAssetImage(imageId);
                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }
    }
}
