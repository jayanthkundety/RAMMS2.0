using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Common;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormDService
    {
        Task<FormDHeaderRequestDTO> FindAndSaveFormDHdr(FormDHeaderRequestDTO header, bool updateSubmit);
        Task<int> SaveFormDAsync(FormDHeaderRequestDTO formDHeaderBO);

        Task<int> SaveFormDLabourAsync(FormDLabourDetailsRequestDTO FormDLabourBO);

        Task<int> SaveFormDMaterialAsync(FormDMaterialDetailsRequestDTO FormDLabourBO);

        Task<int> SaveFormDEquipmentAsync(FormDEquipRequestDTO FormDLabourBO);

        Task<int> SaveFormDDetailAsync(FormDDetailsRequestDTO FormDLabourBO);

        Task<FormDHeaderResponseDTO> SaveHeaderwithResponse(FormDHeaderRequestDTO headerReq);

        Task<int> UpdateFormDAsync(FormDHeaderRequestDTO fornmDDtlDTO);

        Task<int> UpdateFormDLabourAsync(FormDLabourDetailsRequestDTO FormDLabourBO);

        Task<int> UpdateFormDMaterialAsync(FormDMaterialDetailsRequestDTO FormDLabourBO);

        Task<int> UpdateFormDEquipmentAsync(FormDEquipRequestDTO FormDLabourBO);

        Task<int> UpdateFormDDetailAsync(FormDDetailsRequestDTO FormDLabourBO);
        Task<int> DeActivateFormDAsync(int formNo);

        Task<int> DeActivateFormDLabourAsync(int formNo);

        Task<int> DeActivateFormMaterialDAsync(int formNo);

        Task<int> DeActivateFormDEquipmentAsync(int formNo);

        Task<int> DeActivateFormDDetailAsync(int formNo);
        Task<PagingResult<FormDHeaderResponseDTO>> GetFilteredFormDGrid(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions);

        Task<PagingResult<FormDEquipDetailsResponseDTO>> GetEquipmentFormDGrid(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id);

        Task<PagingResult<FormDMaterialDetailsResponseDTO>> GetMaterialFormDGrid(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id);

        Task<PagingResult<FormDLabourDetailsResponseDTO>> GetLabourFormDGrid(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id);

        Task<PagingResult<FormDDetailsResponseDTO>> GetFormDDetailsGrid(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id);


        Task<FormDLabourDetailsRequestDTO> GetFormDLabourDetailsByNoAsync(int formNo);

        Task<FormDMaterialDetailsRequestDTO> GetFormDMaterialDetailsByNoAsync(int formNo);

        Task<FormDEquipRequestDTO> GetFormDEquipmentDetailsByNoAsync(int formNo);

        Task<FormDDetailsRequestDTO> GetFormDDetailsByNoAsync(int formNo);

        Task<FormDHeaderRequestDTO> GetFormDWithDetailsByNoAsync(int formNo);

        Task<int> SaveFormDWarImage(List<WarImageDtlRequestDTO> warImage);
        Task<int> SaveFormDAccUccDtl(List<AccUccImageDtlRequestDTO> accUccImage);
        Task<int> DeActivateWarImage(int warId);
        Task<int> DeActivateAccUCc(int accUccId);
        Task<List<WarImageDtlResponseDTO>> GetWarImageList(int formDId);
        Task<List<AccUccImageDtlResponseDTO>> GetAccUccImageList(int formDId);
        Task<AccUccImageDtlResponseDTO> GetAccUccImage(int formDId);
        Task<int> LastInsertedWARSRNO(int hederId, string type);
        Task<int> LastInsertedUCUSRNO(int hederId, string type);

        Task<IEnumerable<SelectListItem>> GetRoadCodeList();

        Task<IEnumerable<SelectListItem>> GetDivisions();

        Task<IEnumerable<SelectListItem>> GetActivityMainTask();

        Task<IEnumerable<SelectListItem>> GetActivitySubTask();

        Task<IEnumerable<SelectListItem>> GetSectionCode();

        Task<IEnumerable<SelectListItem>> GetLabourCode();

        Task<IEnumerable<SelectListItem>> GetMaterialCode();

        Task<IEnumerable<SelectListItem>> GetEquipmentCode();

        Task<IEnumerable<SelectListItem>> GetRMU();

        Task<IEnumerable<SelectListItem>> GetERTActivityCode();

        Task<bool> CheckHdrRefereceId(string id);

        Task<bool> CheckDetailsRefereceId(string id);
        Task<FormDHeaderRequestDTO> FindDetails(FormDHeaderRequestDTO headerDTO);
        Task<IEnumerable<SelectListItem>> GetRoadCodesByRMU(string rmu);

        Task<IEnumerable<SelectListItem>> GetFormXReferenceId(string rodeCode);

        Task<string> GetMaxIdLength();

        Task<IEnumerable<SelectListItem>> GetSectionCodesByRMU(string rmu);

        Task<IEnumerable<SelectListItem>> GetRoadCodeBySectionCode(string secCode);

        Task<int?> GetLabourSrNo(int id);
        Task<int?> GetEqpSRNO(int id);
        Task<int?> GetMaterialSRNO(int id);
        Task<int?> GetDetailSRNO(int? id);

        Task<int> UpdateFormDSignature(FormDHeaderRequestDTO formDDTO);

        public Task<string> CheckAlreadyExists(int? weekNo, int? year, string crewUnit, string day, string rmu, string secCode);
    }
}
