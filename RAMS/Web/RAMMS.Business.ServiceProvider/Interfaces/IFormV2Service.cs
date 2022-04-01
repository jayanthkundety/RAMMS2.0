using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Common;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;


namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormV2Service
    {
        Task<FormV2HeaderResponseDTO> FindAndSaveFormV2Hdr(FormV2HeaderResponseDTO header, bool updateSubmit);
        Task<int> SaveFormV2Async(FormV2HeaderResponseDTO formDHeaderBO);

        Task<int> SaveFormV2LabourAsync(FormV2LabourDetailsResponseDTO FormV2LabourBO);

        Task<int> SaveFormV2MaterialAsync(FormV2MaterialDetailsResponseDTO FormV2LabourBO);

        Task<int> SaveFormV2EquipmentAsync(FormV2EquipDetailsResponseDTO FormV2LabourBO);

        Task<FormV2HeaderResponseDTO> SaveHeaderwithResponse(FormV2HeaderResponseDTO headerReq);

        Task<int> UpdateFormV2Async(FormV2HeaderResponseDTO fornmDDtlDTO);

        Task<int> UpdateFormV2LabourAsync(FormV2LabourDetailsResponseDTO FormV2LabourBO);

        Task<int> UpdateFormV2MaterialAsync(FormV2MaterialDetailsResponseDTO FormV2LabourBO);

        Task<int> UpdateFormV2EquipmentAsync(FormV2EquipDetailsResponseDTO FormV2LabourBO);

        Task<int> DeActivateFormV2Async(int formNo);

        Task<int> DeActivateFormV2LabourAsync(int formNo);

        Task<int> DeActivateFormMaterialDAsync(int formNo);

        Task<int> DeActivateFormV2EquipmentAsync(int formNo);

        Task<PagingResult<FormV2HeaderResponseDTO>> GetFilteredFormV2Grid(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions);

        Task<PagingResult<FormV2EquipDetailsResponseDTO>> GetEquipmentFormV2Grid(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions, string id);

        Task<PagingResult<FormV2MaterialDetailsResponseDTO>> GetMaterialFormV2Grid(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions, string id);

        Task<PagingResult<FormV2LabourDetailsResponseDTO>> GetLabourFormV2Grid(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions, string id);
   
        Task<FormV2LabourDetailsResponseDTO> GetFormV2LabourDetailsByNoAsync(int formNo);

        Task<FormV2MaterialDetailsResponseDTO> GetFormV2MaterialDetailsByNoAsync(int formNo);

        Task<FormV2EquipDetailsResponseDTO> GetFormV2EquipmentDetailsByNoAsync(int formNo);

        Task<FormV2HeaderResponseDTO> GetFormV2WithDetailsByNoAsync(int formNo);

      
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

        Task<FormV2HeaderResponseDTO> FindDetails(FormV2HeaderResponseDTO headerDTO);
        Task<IEnumerable<SelectListItem>> GetRoadCodesByRMU(string rmu);


        Task<string> GetMaxIdLength();

        Task<IEnumerable<SelectListItem>> GetSectionCodesByRMU(string rmu);

        Task<IEnumerable<SelectListItem>> GetRoadCodeBySectionCode(string secCode);

        Task<int> UpdateFormV2Signature(FormV2HeaderResponseDTO formDDTO);

        public Task<string> CheckAlreadyExists(DateTime? date, string crewUnit, string day, string rmu, string secCode);
    }
}
