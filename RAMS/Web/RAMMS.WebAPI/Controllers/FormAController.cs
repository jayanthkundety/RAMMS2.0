using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.WebAPI
{
    public class FormAController : Controller
    {
        //private readonly IFormAProvider _IFormAProvider;

        private readonly IFormAService _IFormAService;
        private readonly IAssetsService _assetsService;
        private readonly IRoadMasterService _roadMaster;
        private readonly IWebHostEnvironment _environment;
        private readonly IFormAImageService _formAImage;

        public FormAController( IFormAService formAService, IAssetsService assetsService, IRoadMasterService roadMasterService, IWebHostEnvironment webHostEnvironment, IFormAImageService formAImage)
        {
            //_IFormAProvider = formAProvider;
            _IFormAService = formAService;
            _assetsService = assetsService;
            _roadMaster = roadMasterService;
            _environment = webHostEnvironment;
            _formAImage = formAImage;
        }

        /// <summary>
        /// FormA Save Services
        /// </summary>
        /// <param name="GetFormDtlObj"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/saveform")]
        [HttpPost]
        public async Task<IActionResult> GetSaveFormAGridview([FromBody] object GetFormDtlObj)
        {
            try
            {

                FormAHeaderRequestDTO requestDtl = JsonConvert.DeserializeObject<FormAHeaderRequestDTO>(GetFormDtlObj.ToString());
                int rst = await _IFormAService.SaveFormAAsync(requestDtl);
                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }
        /// <summary>
        /// FormA Update Services
        /// </summary>
        /// <param name="GetFormDtlObj"></param>
        /// <returns></returns>
        [Route("api/updateform")]
        [HttpPost]
        public async Task<IActionResult> GetUpdateFormAGridview([FromBody] object GetFormDtlObj)
        {
            try
            {
                FormADetailsRequestDTO requestDtl = JsonConvert.DeserializeObject<FormADetailsRequestDTO>(GetFormDtlObj.ToString());
                int rst = await _IFormAService.UpdateFormAAsync(requestDtl);
                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }
        /// <summary>
        /// Get FormA Grid List Landing Page Services
        /// </summary>
        /// <param name="GetFormhdrObj"></param>
        /// <returns></returns>
        [Route("api/getformagridlist")]
        [HttpPost]
        public async Task<IActionResult> GetFormAGridList([FromBody] object GetFormhdrObj)
        {
            try
            {
                FilteredPagingDefinition<FormASearchGridDTO> requestDtl = JsonConvert.DeserializeObject<FilteredPagingDefinition<FormASearchGridDTO>>(GetFormhdrObj.ToString());

                PagingResult<FormAHeaderResponseDTO> rst = await _IFormAService.GetFilteredFormAGrid(requestDtl);

                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }
        /// <summary>
        /// FormA GetDetailsGrid Edit and View for Landing Page
        /// </summary>
        /// <param name="formNo"></param>
        /// <returns></returns>
        [Route("api/getdetailgrid")]
        [HttpPost]
        public async Task<IActionResult> GetFormAWithDetailsByNoAsync([FromBody] int formNo)
        {
            try
            {
                //FormAHeaderRequestDTO requesthdr = JsonConvert.DeserializeObject<FormAHeaderRequestDTO>(formNo.ToString());
                FormAHeaderRequestDTO rst = await _IFormAService.GetFormAWithDetailsByNoAsync(formNo);
                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }





        // New Services Call.

        //Landing page GrigList with Filter
        [Authorize]
        [Route("api/formALandingGrid")]
        [HttpPost]
        public async Task<IActionResult> GetSaveFormADetails([FromBody] object GetFormhdrObj)
        {
            try
            {
                FilteredPagingDefinition<FormASearchGridDTO> requestDtl = JsonConvert.DeserializeObject<FilteredPagingDefinition<FormASearchGridDTO>>(GetFormhdrObj.ToString());
                PagingResult<FormAHeaderResponseDTO> result = await _IFormAService.GetFilteredFormAGrid(requestDtl);
                return RAMMSApiSuccessResponse(result);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }


        ////Old FormA Save
        //[Route("api/saveFormA")]
        //[HttpPost]
        //public async Task<IActionResult> FormAOverSave([FromBody] object SaveObj)
        //{
        //    try
        //    {
        //        FormAHeaderRequestDTO request = JsonConvert.DeserializeObject<FormAHeaderRequestDTO>(SaveObj.ToString());
        //        int savefrm = await _IFormAService.SaveFormAAsync(request);
        //        return RAMMSApiSuccessResponse(savefrm);
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.RAMMSApiErrorResponse(ex.Message);
        //    }
        //}


        // Update Detail
        [Authorize]
        [Route("api/updateFormA")]
        [HttpPost]
        public async Task<IActionResult> FormAUpdate([FromBody] object SaveObj)
        {
            try
            {
                FormADetailsRequestDTO request = JsonConvert.DeserializeObject<FormADetailsRequestDTO>(SaveObj.ToString());
                int savefrm = await _IFormAService.UpdateFormAAsync(request);
                return RAMMSApiSuccessResponse(savefrm);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }


        //Return FormA Header with Details for Edit and View //OLD one
        [Authorize]
        [Route("api/getFormADetails")]
        [HttpPost]
        public async Task<IActionResult> GetFormAUpdateAndView(int SaveObj)
        {
            try
            {
                FormAHeaderRequestDTO savefrm = await _IFormAService.GetFormAWithDetailsByNoAsync(SaveObj);
                return RAMMSApiSuccessResponse(savefrm);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        //FormA Hdr Save
        [Authorize]
        [Route("api/SaveFormAHdr")]
        [HttpPost]
        public async Task<IActionResult> SaveFormAHdr([FromBody] object SaveObj)
        {
            try
            {
                FormAHeaderRequestDTO request = JsonConvert.DeserializeObject<FormAHeaderRequestDTO>(SaveObj.ToString());
                FormAHeaderResponseDTO response = await _IFormAService.SaveHeaderwithResponse(request);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        //Save Detail Record and return HeaderId
        [Authorize]
        [Route("api/SaveFormADtl")]
        [HttpPost]
        public async Task<IActionResult> SaveFormADtl([FromBody] object SaveObj)
        {
            try
            {
                FormADetailsRequestDTO request = JsonConvert.DeserializeObject<FormADetailsRequestDTO>(SaveObj.ToString());
                int? response = await _IFormAService.SaveDetailforHeaderV1(request);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        // Reference No
        [Authorize]
        [Route("api/RefNo")]
        [HttpPost]
        public async Task<IActionResult> GetReferenceNOData(string roadCode, int month, string year, string assetGroup)
        {
            try
            {
                if (roadCode != null && assetGroup != null)
                {
                    
                    string isexist = _IFormAService.CheckAlreadyExists(roadCode, month, int.Parse(year), assetGroup);
                    assetGroup = _IFormAService.GetAssetCodeByName(assetGroup);
                    if (string.IsNullOrEmpty(isexist))
                    {
                        isexist = (await _IFormAService.GetLastInsertedHeader()).ToString();
                    }
                    if (!string.IsNullOrEmpty(assetGroup))
                    {
                        var AutoRefNo = $"NOD/Form A/{roadCode}/{month}/{assetGroup}/{String.Format("{0:0000}", isexist)}-" + year;
                        return RAMMSApiSuccessResponse(AutoRefNo);
                    }
                    else
                    {
                        return RAMMSApiSuccessResponse("");
                    }
                   
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }

        }



        // Delete Header
        [Authorize]
        [Route("api/deleteHdr")]
        [HttpPost]
        public async Task<IActionResult> DeleteDetail(int headerNo)
        {
            try
            {
                int listItems = await _IFormAService.DeActivateFormAAsync(headerNo);
                return RAMMSApiSuccessResponse(listItems);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        //Delete Detail
        [Authorize]
        [Route("api/deleteDetail")]
        [HttpPost]
        public async Task<IActionResult> DeleteHdr(int detailId)
        {
            try
            {
                int listItems = await _IFormAService.DeActivateDetail(detailId);
                return RAMMSApiSuccessResponse(listItems);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }


        // Get Max SRNO
        [Authorize]
        [Route("api/detailSrNo")]
        [HttpPost]
        public async Task<IActionResult> DetailSrNo(int headerRef)
        {
            try
            {
                int srNo = await _IFormAService.LastInsertedFormDetailSRNO(headerRef);
                return RAMMSApiSuccessResponse(srNo);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }


        // Detail Grid Edit and View
        [Authorize]
        [Route("api/DetailGrid")]
        [HttpPost]
        public async Task<IActionResult> GetDetails([FromBody] object GetFormhdrObj)
        {
            try
            {
                FilteredPagingDefinition<FormADetailsRequestDTO> requestDtl = JsonConvert.DeserializeObject<FilteredPagingDefinition<FormADetailsRequestDTO>>(GetFormhdrObj.ToString());
                PagingResult<FormADetailResponseDTO> result = await _IFormAService.GetFormADetailGrid(requestDtl);
                return RAMMSApiSuccessResponse(result);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        // Get Single Detail For Edit and View
        [Authorize]
        [Route("api/GetFormADetailById")]
        [HttpPost]
        public async Task<IActionResult> GetFormADetail(int detailId)
        {
            try
            {
                FormADetailsRequestDTO listItems = await _IFormAService.GetDetailById(detailId);
                return RAMMSApiSuccessResponse(listItems);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        // Get Header with Header ID
        [Authorize]
        [Route("api/GetFormAHeaderById")]
        [HttpPost]
        public async Task<IActionResult> GetFormAHdrById(int headerId)
        {
            try
            {
                FormAHeaderResponseDTO listItems = await _IFormAService.GetHeaderById(headerId);
                return RAMMSApiSuccessResponse(listItems);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/updateSignature")]
        [HttpPost]
        public async Task<IActionResult> UpdateSignature([FromBody] object GetFormDtlObj)
        {
            try
            {
                FormAHeaderRequestDTO requestDtl = JsonConvert.DeserializeObject<FormAHeaderRequestDTO>(GetFormDtlObj.ToString());
                int rst = await _IFormAService.UpdateSignature(requestDtl);
                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }


        [Authorize]
        [Route("api/deleteImage")]
        [HttpPost]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            try
            {
                int rst = await _formAImage.DectivateAssetImage(imageId);
                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }


        [Authorize]
        [Route("api/AssetGroupByNameFormA")]
        [HttpPost]
        public async Task<IActionResult> GetAssetCodeByNameFormA(string assetGroup)
        {
            try
            {
                string rst =  _IFormAService.GetAssetCodeByName(assetGroup);
                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }


    }
}

