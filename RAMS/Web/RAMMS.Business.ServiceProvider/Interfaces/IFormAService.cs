using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Common;
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
    public interface IFormAService
    {
        Task<int> SaveFormAAsync(FormAHeaderRequestDTO formAHeaderBO);
        Task<FormAHeaderRequestDTO> GetFormAWithDetailsByNoAsync(int formNo);        
        Task<int> UpdateFormAAsync(FormADetailsRequestDTO fornmADtlDTO);
        Task<int> DeActivateFormAAsync(int formNo);
        Task<int> DeActivateDetail(int detailId);
        Task<PagingResult<FormAHeaderResponseDTO>> GetFilteredFormAGrid(FilteredPagingDefinition<FormASearchGridDTO> filterOptions);
        Task<List<string>> GetSectionByRMU(string rmu);
        Task<IEnumerable<SelectListItem>> GetDefectCodeService(string assetGroup);
        Task<FormAHeaderResponseDTO> SaveHeaderwithResponse(FormAHeaderRequestDTO headerReq);
        Task<int?> SaveDetailforHeader(FormADetailsRequestDTO detailDTO);
        Task<FormADetailsRequestDTO> GetDetailById(int detailId);
        Task<int?> SaveDetailforHeaderV1(FormADetailsRequestDTO detailDTO);
        Task<PagingResult<FormADetailResponseDTO>> GetFormADetailGrid(FilteredPagingDefinition<FormADetailsRequestDTO> detailList);
        Task<int> LastInsertedFormDetailSRNO(int headerId);
        Task<FormAHeaderResponseDTO> GetHeaderById(int headerId);
        Task<int> UpdateSignature(FormAHeaderRequestDTO formA);
        Task<int> GetLastInsertedHeader();
        FormASearchDropdown GetDropdown(RequestDropdownFormA request);
        string CheckAlreadyExists(string roadCode, int month, int year, string assetGroup);
        string GetAssetCodeByName(string name);
        FORMARpt GetReportData(int headerid,int pageindex,int pagecount);
        Byte[] FormDownload(string formname, int id, string filepath);
        Task<IEnumerable<object>> GetActiveRefIDs(string activityCode, string roadCode, int fromCHKM, string fromCHM, int toCHKM, string toCHM);
    }
}
