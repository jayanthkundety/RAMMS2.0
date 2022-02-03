using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Common;
using RAMMS.Common.RefNumber;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.WebAPI.Controllers
{
    
    public class FormDController : Controller
    {
        private readonly IFormDService _formDservice;
        private readonly ISecurity _security;
        public FormDController(IFormDService formDService, ISecurity security)
        {
            _formDservice = formDService;
            _security = security;
        }


        [Authorize]
        [Route("api/FormXRefForFormD")]
        [HttpPost]
        public async Task<IActionResult> GetFormXRefForFormD(string rodeCode)
        {
            try
            {
                IEnumerable<SelectListItem> response = await _formDservice.GetFormXReferenceId(rodeCode);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        //[ActionName("getformDgridlist")]
        [Route("api/getformDgridlist")]
        [HttpPost]
        public async Task<IActionResult> GetFormDGridList([FromBody] object formDobj)
        {
            try
            {
                FilteredPagingDefinition<FormDSearchGridDTO> requestDtl = JsonConvert.DeserializeObject<FilteredPagingDefinition<FormDSearchGridDTO>>(formDobj.ToString());

                PagingResult<FormDHeaderResponseDTO> rst = await _formDservice.GetFilteredFormDGrid(requestDtl);

                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/SaveFormDHdr")]
        [HttpPost]
        public async Task<IActionResult> SaveFormDHdr([FromBody] object SaveObj)
        {
            try
            {
                int? response;
                FormDHeaderRequestDTO request = JsonConvert.DeserializeObject<FormDHeaderRequestDTO>(SaveObj.ToString());
                var isExist = await _formDservice.CheckAlreadyExists(request.WeekNo, request.Year, request.CrewUnit, request.Day, request.Rmu, request.RoadCode);
                if (isExist == null)
                {
                    response = await _formDservice.SaveFormDAsync(request);
                }
                else
                {
                    response = Convert.ToInt32(isExist);
                }
                
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/DeleteFormDHdr")]
        [HttpPost]
        public async Task<IActionResult> DeleteFormDHdr(int formDNo)
        {
            try
            {
                int? response = await _formDservice.DeActivateFormDAsync(formDNo);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/UpdateFormDHdr")]
        [HttpPost]
        public async Task<IActionResult> UpdateFormDHdr([FromBody] object SaveObj)
        {
            try
            {
                FormDHeaderRequestDTO request = JsonConvert.DeserializeObject<FormDHeaderRequestDTO>(SaveObj.ToString());                
                int? response = await _formDservice.UpdateFormDAsync(request);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }


        [Authorize]
        [Route("api/ErtActivityCode")]
        [HttpGet]
        public async Task<IActionResult> GetErtActivityCode()
        {
            try
            {
                IEnumerable<SelectListItem> response = await _formDservice.GetERTActivityCode();
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/CheckFormDHdrRefId")]
        [HttpPost]
        public async Task<IActionResult> CheckFormDHdrRefid(string formdId)
        {
            try
            {
                bool response = await _formDservice.CheckHdrRefereceId(formdId);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/EditViewFormDHdr")]
        [HttpPost]
        public async Task<IActionResult> FormDGetById(int formdId)
        {
            try
            {
                FormDHeaderRequestDTO response = await _formDservice.GetFormDWithDetailsByNoAsync(formdId);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/FormDRefNo")]
        [HttpPost]
        public async Task<IActionResult> GetFormDRefNo(int? weekNo, int? monthNo,int? year, string crewUnit, string day, string rmu, string secCode)
        {
            try
            {
                int max;
                string AutoRefNo;
                string existId = await _formDservice.CheckAlreadyExists(weekNo, year, crewUnit, day, rmu, secCode);

                string maxcount = await _formDservice.GetMaxIdLength();

                if (string.IsNullOrEmpty(existId))
                {
                    max = Convert.ToInt32(maxcount) + 1;
                    maxcount = max.ToString();

                    AutoRefNo = "ERT/FORM D/" + weekNo + "-" + monthNo + "-" + year + "/" + crewUnit + "/" + maxcount.PadLeft(4, '0');
                }
                else
                {
                    AutoRefNo = "ERT/FORM D/" + weekNo + "-" + monthNo + "-" + year + "/" + crewUnit + "/" + existId.PadLeft(4, '0');
                }

                return RAMMSApiSuccessResponse(AutoRefNo);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }



        //labour

        [Authorize]
        [Route("api/getformDLabourgridlist")]
        [HttpPost]
        public async Task<IActionResult> GetFormDLabourGridList([FromBody] object formDobj, string HdrId)
        {
            try
            {
                FilteredPagingDefinition<FormDSearchGridDTO> requestDtl = JsonConvert.DeserializeObject<FilteredPagingDefinition<FormDSearchGridDTO>>(formDobj.ToString());

                PagingResult<FormDLabourDetailsResponseDTO> rst = await _formDservice.GetLabourFormDGrid(requestDtl, HdrId);

                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/SaveFormDLabourHdr")]
        [HttpPost]
        public async Task<IActionResult> SaveFormDLabourHdr([FromBody] object SaveObj)
        {
            try
            {
                FormDLabourDetailsRequestDTO request = JsonConvert.DeserializeObject<FormDLabourDetailsRequestDTO>(SaveObj.ToString());
                int? response = await _formDservice.SaveFormDLabourAsync(request);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/DeleteFormDLabourHdr")]
        [HttpPost]
        public async Task<IActionResult> DeleteFormDLabourHdr(int formDNo)
        {
            try
            {
                int? response = await _formDservice.DeActivateFormDLabourAsync(formDNo);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/UpdateFormDLabourHdr")]
        [HttpPost]
        public async Task<IActionResult> UpdateFormDLabourHdr([FromBody] object SaveObj)
        {
            try
            {
                FormDLabourDetailsRequestDTO request = JsonConvert.DeserializeObject<FormDLabourDetailsRequestDTO>(SaveObj.ToString());
                int? response = await _formDservice.UpdateFormDLabourAsync(request);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Route("api/LabourCode")]
        [HttpGet]
        public async Task<IActionResult> GetFormDLabourCode()
        {
            try
            {
                IEnumerable<SelectListItem> listItems = await _formDservice.GetLabourCode();
                ; return RAMMSApiSuccessResponse(listItems);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/EditViewFormDLabourHdr")]
        [HttpPost]
        public async Task<IActionResult> FormDLabourGetById(int formdId)
        {
            try
            {
                FormDLabourDetailsRequestDTO response = await _formDservice.GetFormDLabourDetailsByNoAsync(formdId);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/LabourSrNo")]
        [HttpPost]
        public async Task<IActionResult> FormDLabourSrNo(int HdrId)
        {
            try
            {
                int? response = await _formDservice.GetLabourSrNo(HdrId);
                response = response + 1;
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }




        //Equipment

        [Authorize]
        [Route("api/getformDEqpgridlist")]
        [HttpPost]
        public async Task<IActionResult> GetFormDEqpGridList([FromBody] object formDobj, string HdrId)
        {
            try
            {
                FilteredPagingDefinition<FormDSearchGridDTO> requestDtl = JsonConvert.DeserializeObject<FilteredPagingDefinition<FormDSearchGridDTO>>(formDobj.ToString());

                PagingResult<FormDEquipDetailsResponseDTO> rst = await _formDservice.GetEquipmentFormDGrid(requestDtl, HdrId);

                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/SaveFormDEqpHdr")]
        [HttpPost]
        public async Task<IActionResult> SaveFormDEqpHdr([FromBody] object SaveObj)
        {
            try
            {
                FormDEquipRequestDTO request = JsonConvert.DeserializeObject<FormDEquipRequestDTO>(SaveObj.ToString());
                int? response = await _formDservice.SaveFormDEquipmentAsync(request);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/DeleteFormDEqpHdr")]
        [HttpPost]
        public async Task<IActionResult> DeleteFormDEqpHdr(int formDNo)
        {
            try
            {
                int? response = await _formDservice.DeActivateFormDEquipmentAsync(formDNo);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/UpdateFormDEqpHdr")]
        [HttpPost]
        public async Task<IActionResult> UpdateFormDEqpHdr([FromBody] object SaveObj)
        {
            try
            {
                FormDEquipRequestDTO request = JsonConvert.DeserializeObject<FormDEquipRequestDTO>(SaveObj.ToString());
                int? response = await _formDservice.UpdateFormDEquipmentAsync(request);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Route("api/EqpCode")]
        [HttpGet]
        public async Task<IActionResult> GetFormDEqpCode()
        {
            try
            {
                IEnumerable<SelectListItem> listItems = await _formDservice.GetEquipmentCode();
                ; return RAMMSApiSuccessResponse(listItems);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/EditViewFormDEqpHdr")]
        [HttpPost]
        public async Task<IActionResult> FormDEqpGetById(int formdId)
        {
            try
            {
                FormDEquipRequestDTO response = await _formDservice.GetFormDEquipmentDetailsByNoAsync(formdId);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/EqpSrNo")]
        [HttpPost]
        public async Task<IActionResult> FormDEqpSrNo(int HdrId)
        {
            try
            {
                int? response = await _formDservice.GetEqpSRNO(HdrId);
                response = response + 1;
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }



        //Material

        [Authorize]
        [Route("api/getformDMaterialgridlist")]
        [HttpPost]
        public async Task<IActionResult> GetFormDMaterialGridList([FromBody] object formDobj, string HdrId)
        {
            try
            {
                FilteredPagingDefinition<FormDSearchGridDTO> requestDtl = JsonConvert.DeserializeObject<FilteredPagingDefinition<FormDSearchGridDTO>>(formDobj.ToString());

                PagingResult<FormDMaterialDetailsResponseDTO> rst = await _formDservice.GetMaterialFormDGrid(requestDtl, HdrId);

                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/SaveFormDMaterialHdr")]
        [HttpPost]
        public async Task<IActionResult> SaveFormDMaterialHdr([FromBody] object SaveObj)
        {
            try
            {
                FormDMaterialDetailsRequestDTO request = JsonConvert.DeserializeObject<FormDMaterialDetailsRequestDTO>(SaveObj.ToString());
                int? response = await _formDservice.SaveFormDMaterialAsync(request);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/DeleteFormDMaterialHdr")]
        [HttpPost]
        public async Task<IActionResult> DeleteFormDmaterialHdr(int formDNo)
        {
            try
            {
                int? response = await _formDservice.DeActivateFormMaterialDAsync(formDNo);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/UpdateFormDMaterialHdr")]
        [HttpPost]
        public async Task<IActionResult> UpdateFormDMaterialHdr([FromBody] object SaveObj)
        {
            try
            {
                FormDMaterialDetailsRequestDTO request = JsonConvert.DeserializeObject<FormDMaterialDetailsRequestDTO>(SaveObj.ToString());
                int? response = await _formDservice.UpdateFormDMaterialAsync(request);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Route("api/MaterialCode")]
        [HttpGet]
        public async Task<IActionResult> GetFormDMaterialCode()
        {
            try
            {
                IEnumerable<SelectListItem> listItems = await _formDservice.GetMaterialCode();
                ; return RAMMSApiSuccessResponse(listItems);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/EditViewFormDMaterialHdr")]
        [HttpPost]
        public async Task<IActionResult> FormDMaterialGetById(int formdId)
        {
            try
            {
                FormDMaterialDetailsRequestDTO response = await _formDservice.GetFormDMaterialDetailsByNoAsync(formdId);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/MaterialSrNo")]
        [HttpPost]
        public async Task<IActionResult> FormDMaterialSrNo(int HdrId)
        {
            try
            {
                int? response = await _formDservice.GetMaterialSRNO(HdrId);
                response = response + 1;
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }




        //Details

        [Authorize]
        [Route("api/getformDDtlgridlist")]
        [HttpPost]
        public async Task<IActionResult> GetFormDDtlGridList([FromBody] object formDobj, string HdrId)
        {
            try
            {
                FilteredPagingDefinition<FormDSearchGridDTO> requestDtl = JsonConvert.DeserializeObject<FilteredPagingDefinition<FormDSearchGridDTO>>(formDobj.ToString());

                PagingResult<FormDDetailsResponseDTO> rst = await _formDservice.GetFormDDetailsGrid(requestDtl, HdrId);

                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/SaveFormDDtl")]
        [HttpPost]
        public async Task<IActionResult> SaveFormDDtl([FromBody] object SaveObj)
        {
            try
            {
                FormDDetailsRequestDTO request = JsonConvert.DeserializeObject<FormDDetailsRequestDTO>(SaveObj.ToString());
                int? response = await _formDservice.SaveFormDDetailAsync(request);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/DeleteFormDDtl")]
        [HttpPost]
        public async Task<IActionResult> DeleteFormDDtl(int formDNo)
        {
            try
            {
                int? response = await _formDservice.DeActivateFormDDetailAsync(formDNo);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/UpdateFormDDtl")]
        [HttpPost]
        public async Task<IActionResult> UpdateFormDDtl([FromBody] object SaveObj)
        {
            try
            {
                FormDDetailsRequestDTO request = JsonConvert.DeserializeObject<FormDDetailsRequestDTO>(SaveObj.ToString());
                int? response = await _formDservice.UpdateFormDDetailAsync(request);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        
        [Authorize]
        [Route("api/EditViewFormDDtl")]
        [HttpPost]
        public async Task<IActionResult> FormDDtlGetById(int formdId)
        {
            try
            {
                FormDDetailsRequestDTO response = await _formDservice.GetFormDDetailsByNoAsync(formdId);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Authorize]
        [Route("api/DtlFromDSrNo")]
        [HttpPost]
        public async Task<IActionResult> FormDDtlSrNo(int HdrId)
        {
            try
            {
                int? response = await _formDservice.GetDetailSRNO(HdrId);
                response = response + 1;
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }



        //User

        [Route("api/FormDupdateSignature")]
        [HttpPost]
        public async Task<IActionResult> UpdateSignatureFormD([FromBody] object GetFormDtlObj)
        {
            try
            {
                FormDHeaderRequestDTO requestDtl = JsonConvert.DeserializeObject<FormDHeaderRequestDTO>(GetFormDtlObj.ToString());
                int rst = await _formDservice.UpdateFormDSignature(requestDtl);
                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        //Image
        //Images
        [HttpPost] // FormD Image Retrive from TAB
        [Route("api/FormDGetImage")]
        public async Task<IActionResult> GetImagesFormD(string AssetId)
        {
            try
            {
                int assetId = Convert.ToInt32(AssetId);
                List<WarImageDtlResponseDTO> response = new List<WarImageDtlResponseDTO>();
                 response = await _formDservice.GetWarImageList(assetId);
                return this.RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [HttpPost] // FormD Pdf Retrive from TAB
        [Route("api/FormDGetAccUcc")]
        public async Task<IActionResult> GetAccUccFormD(string AssetId)
        {
            try
            {
                int assetId = Convert.ToInt32(AssetId);
                List<AccUccImageDtlResponseDTO> response = new List<AccUccImageDtlResponseDTO>();
                response = await _formDservice.GetAccUccImageList(assetId);
                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Route("api/deleteImageFormD")]
        [HttpPost]
        public async Task<IActionResult> DeleteImageFormD(int imageId)
        {
            try
            {
                int rst = await _formDservice.DeActivateWarImage(imageId);
                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Route("api/deleteACCUCCFormD")]
        [HttpPost]
        public async Task<IActionResult> DeleteAccUccFormD(int accUccId)
        {
            try
            {
                int rst = await _formDservice.DeActivateAccUCc(accUccId);
                return RAMMSApiSuccessResponse(rst);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }

        [Route("api/formDRefNoGeneration")]
        [HttpPost]
        public IActionResult RefNoGeneration([FromBody] object formDHdr)
        {
            try
            {
                FormDHeaderRequestDTO requestHdr = JsonConvert.DeserializeObject<FormDHeaderRequestDTO>(formDHdr.ToString());

                IDictionary<string, string> lstData = new Dictionary<string, string>();
                lstData.Add("Year", Utility.ToString(requestHdr.Year));
                lstData.Add("MonthNo", Utility.ToString(requestHdr.Month));
                lstData.Add("WeekNo", Utility.ToString(requestHdr.WeekNo));
                lstData.Add("CrewUnit", requestHdr.CrewUnit);
                
                var response = FormRefNumber.GetRefNumber(RAMMS.Common.RefNumber.FormType.FormDHeader, lstData);


                return RAMMSApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }



        [Route("api/formDFindDetails")]
        [HttpPost]
        public async Task<IActionResult> FormDFindDetails([FromBody] object formDHdr)
        {
            try
            {
                FormDHeaderRequestDTO formDRes = new FormDHeaderRequestDTO();
                FormDHeaderRequestDTO formD = JsonConvert.DeserializeObject<FormDHeaderRequestDTO>(formDHdr.ToString());                
                
                formDRes = await _formDservice.FindDetails(formD);
                if (formDRes == null || formDRes.No == 0)
                {

                    formD.CreatedBy = _security.UserID.ToString();
                    formD.ModifeidBy = _security.UserID.ToString();
                    formD.ModifiedDate = DateTime.Now;
                    formD.CreatedDate = DateTime.Now;
                    
                    formDRes = await _formDservice.FindAndSaveFormDHdr(formD, false);
                }
                
                return RAMMSApiSuccessResponse(formDRes);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }
        }
    }
}
