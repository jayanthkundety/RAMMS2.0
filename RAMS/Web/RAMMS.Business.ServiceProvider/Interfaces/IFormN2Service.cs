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
    public interface IFormN2Service
    {
        Task<int> SaveFormN2Async(FormN2HeaderRequestDTO formN2HeaderBO);

        Task<FormN2HeaderRequestDTO> SaveHeaderWithResponse(FormN2HeaderRequestDTO headerReq);

        Task<int> UpdateFormN2Async(FormN2HeaderRequestDTO fornmDDtlDTO);

        Task<int> DeActivateFormN2Async(int formNo);

        Task<PagingResult<FormN2HeaderRequestDTO>> GetFilteredFormN2Grid(FilteredPagingDefinition<FormN2SearchGridDTO> filterOptions);

        Task<IEnumerable<SelectListItem>> GetRoadCodeList();

        Task<IEnumerable<SelectListItem>> GetSectionCode();

        Task<IEnumerable<SelectListItem>> GetRMU();

        Task<bool> CheckHdrRefereceId(string id);

        Task<FormN2HeaderRequestDTO> GetFormN2WithDetailsByNoAsync(int formNo);

        (int id, bool isExists) CheckExistence(string rdCode, int month, int year);

        Task<IEnumerable<SelectListItem>> GetRoadCodesByRMU(string rmu);

        Task<IEnumerable<SelectListItem>> GetSectionCodesByRMU(string rmu);

        Task<IEnumerable<SelectListItem>> GetFormN1ReferenceId(string rodeCode);

        Task<int> GetMaxCount();
        FORMN2Rpt GetReportData(int headerId, int pageIndex, int pageCount);
        Byte[] FormDownload(string formName, int id, string filePath);

    }
}
