using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormS2Service
    {
        Task<FormS2HeaderRequestDto> SaveHeader(FormS2HeaderRequestDto request);
        int SaveDetail(FormS2DetailRequestDto request);
        Task<PagingResult<S2HeaderResponse>> GetHeaderList(FilteredPagingDefinition<S2HeaderSearchRequestDTO> filterOptions);
        Task<PagingResult<FormS2DetailResponseDTO>> GetDetailList(FilteredPagingDefinition<FormS2DetailSearchDto> filterOptions);
        int LastHeaderInsertedNo();
        int LastDetailInsertedNo(int headerId);

        Task<(int id, bool aleadyExists, bool isSubmitted)> CheckHeaderExistence(string rmu, int activityCode, int year, int quarter);
        Task<FormS2HeaderRequestDto> GetHeaderById(int id);
        Task<FormS2DetailRequestDto> GetDetailById(int id);
        List<SelectListItem> GetYears();
        List<SelectListItem> GetQuarter(int year);
        List<SelectListItem> GetMonth(int year, int quarter);
        List<WeekS2ViewDto> GetWeek(int year, int quarter);
        int GetId(int year, int quarter, int month, int week);
        FORMS2Rpt GetReportData(int headerId);
        Byte[] FormDownload(string formName, int id, string filePath);
        bool RemoveHeader(int id);
        bool RemoveDetail(int id);
        Task<IEnumerable<object>> GetActiveRefId(int activityCode, int roadCodeId);
        Task<bool> CheckS2DtlExistance(int headerId, int rdCode);
    }
}
