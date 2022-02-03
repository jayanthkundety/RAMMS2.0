using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.SearchBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormJServices
    {
        Task<int> SaveFormJAsync(FormJHeaderRequestDTO formAHeaderBO);
        Task<FormJHeaderRequestDTO> GetFormAWithDetailsByNoAsync(int formNo);
        Task<int> UpdateFormAAsync(FormJDetailsRequestDTO fornmADtlDTO);
        Task<int> DeActivateFormAAsync(int formNo);
        Task<int> DeActivateDetail(int detailId);
        Task<PagingResult<FormJHeaderResponseDTO>> GetFilteredFormJGrid(FilteredPagingDefinition<FormJSearchGridDTO> filterOptions);
        Task<List<string>> GetSectionByRMU(string rmu);
        Task<IEnumerable<SelectListItem>> GetDefectCodeService(string assetGroup);

        Task<IEnumerable<SelectListItem>> GetDefectCodeServiceConCat(string assetGroup);
        Task<FormJHeaderResponseDTO> SaveHeaderWithResponse(FormJHeaderRequestDTO headerReq);
        Task<int?> SaveDetailforHeader(FormJDetailsRequestDTO detailDTO);
        Task<FormJDetailsRequestDTO> GetDetailById(int detailId);
        Task<int?> SaveDetailforHeaderV1(FormJDetailsRequestDTO detailDTO);
        Task<PagingResult<FormJDetailResponseDTO>> GetFormADetailGrid(FilteredPagingDefinition<FormJDetailsRequestDTO> detailList);
        Task<int> LastInsertedFormDetailSRNO(int headerId);
        Task<FormJHeaderResponseDTO> GetHeaderById(int headerId);
        string CheckAlreadyExists(string roadCode, int v1, int v2, string assetGroup);
        Task<int> GetLastInsertedHeader();
        string GetAssetCodeByName(string name);
        FormASearchDropdown GetDropdown(RequestDropdownFormA requestDropDownFormA);
        FORMJRpt GetReportData(int headerId, int pageIndex, int pageCount);
        Byte[] FormDownload(string formName, int id, string filePath);
        Task<int> UpdateFormJSignature(FormJHeaderRequestDTO formJDTO);
    }
}
