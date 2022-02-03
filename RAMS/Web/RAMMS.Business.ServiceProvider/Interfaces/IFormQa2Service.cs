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
    public interface IFormQa2Service
    {

        Task<bool> CheckHdrRefereceId(string id);

        Task<FormQa2HeaderRequestDTO> SaveFormQa2Hdr(FormQa2HeaderRequestDTO formDetailBO);

        Task<int> SaveFormQa2Detail(FormQa2DtlRequestDTO formDetailBO);

        Task<FormQa2DtlRequestDTO> SaveDetailsWithResponse(FormQa2DtlRequestDTO headerReq);

        Task<FormQa2HeaderRequestDTO> SaveHeaderWithResponse(FormQa2HeaderRequestDTO headerReq);

        Task<PagingResult<FormQa2DtlRequestDTO>> GetFilteredFormQa2Grid(FilteredPagingDefinition<FormQa2SearchGridDTO> filterOptions);

        Task<FormQa2DtlRequestDTO> GetFormWithDetailsByNoAsync(int formNo);

        Task<IEnumerable<SelectListItem>> GetRoadCodeList();

        Task<IEnumerable<SelectListItem>> GetSectionCode();

        Task<IEnumerable<SelectListItem>> GetRMU();


        Task<IEnumerable<SelectListItem>> GetRoadCodesByRMU(string rmu);

        Task<(int id, bool aleadyExists)> CheckExistence(string rdCode, string rmu,string month,string year);
        Task<int> GetMaxCount();
        Task<int> LastInsertedRecord();
        Task<PagingResult<FormQa2DtlRequestDTO>> GetFilteredFormQa2DetailsGrid(FilteredPagingDefinition<FormQa2SearchGridDTO> filterOptions);

        Task<FormQa2HeaderRequestDTO> GetHeaderWithDetailById(int hdrNo);
        
        Task<int> DeActivateFormQA2Async(int hdrFormNo);
        Task<int> DeActivateFormQA2DtlAsync(int dtlFormNo);
        Task<int> RemoveDetail(int id);
        Task<FormQa2HeaderRequestDTO> GetRmFormQa2Hdr(int id);
        FORMQA2Rpt GetReportData(int headerId);
        Byte[] FormDownload(string formName, int id, string filePath);
        Task<int?> DtlSrNo(int headerNo);
        Task<FormQa2HeaderResponseDTO> UpdateQa2Hdr(FormQa2HeaderRequestDTO requestDTO);
        Task<string> GetFormADetailByIdAsync(int detailId);
    }
}
