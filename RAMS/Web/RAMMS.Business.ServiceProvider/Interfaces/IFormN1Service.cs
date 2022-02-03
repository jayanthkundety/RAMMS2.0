using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Common;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormN1Service
    {
        Task<int> SaveFormN1Async(FormN1HeaderRequestDTO formN1HeaderBO);

        Task<FormN1HeaderRequestDTO> SaveHeaderwithResponse(FormN1HeaderRequestDTO headerReq);

        Task<int> UpdateFormN1Async(FormN1HeaderRequestDTO fornmDDtlDTO);

        Task<int> DeActivateFormN1Async(int formNo);

        Task<PagingResult<FormN1HeaderRequestDTO>> GetFilteredFormN1Grid(FilteredPagingDefinition<FormN1SearchGridDTO> filterOptions);

        Task<IEnumerable<SelectListItem>> GetRoadCodeList();

        Task<IEnumerable<SelectListItem>> GetSectionCode();

        Task<IEnumerable<SelectListItem>> GetRMU();

        Task<bool> CheckHdrRefereceId(string id);
        Task<bool> CheckHdrRefereceNo(string refNo);
        Task<FormN1HeaderRequestDTO> GetFormN1WithDetailsByNoAsync(int formNo);

        (int id, bool isExists) CheckExistence(string rdCode, int month, int year);

        Task<IEnumerable<SelectListItem>> GetRoadCodesByRMU(string rmu);

        Task<IEnumerable<SelectListItem>> GetSectionCodesByRMU(string rmu);

        Task<IEnumerable<SelectListItem>> GetFormS1ReferenceId(string rodeCode);

        Task<IEnumerable<SelectListItem>> GetFormQA2ReferenceId(string rodeCode);

        Task<RmFormS1Hdr> GetFormS1Data(int id);

        Task<FormQa2HeaderResponseDTO> GetFormQa2Data(int id);

        Task<int> GetMaxCount();
        FORMN1Rpt GetReportData(int headerId, int pageIndex, int pageCount);
        Byte[] FormDownload(string formName, int id, string filePath);
    }
}
