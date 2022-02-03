using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormF2Service
    {
        Task<(int id, bool aleadyexists, bool isSubmitted)> CheckHeaderExistence(string roadcode);
        int LastHeaderInsertedNo();
        Task<FormF2HeaderRequestDTO> GetHeaderById(int id);
        Task<bool> RemoveHeader(int id);
        Task<int> SaveHeader(FormF2HeaderRequestDTO model);
        Task<int> SaveDetail(FormF2DetailRequestDTO model);
        int LastInsertedDetailNo(int headerid);
        Task<FormF2DetailRequestDTO> GetDetailById(int id);
        Task<decimal?> TotalLength(string roadcode);
        Task<bool> RemoveDetail(int id);
        Task<PagingResult<FormF2HeaderRequestDTO>> GetHeaderList(FilteredPagingDefinition<FormF2SearchGridDTO> filterOptions);
        Task<PagingResult<FormF2DetailRequestDTO>> GetDetailList(FilteredPagingDefinition<FormF2DetailRequestDTO> filterOptions);
        Task<List<SelectListItem>> GetLocationCh(string roadcode);
        Task<List<SelectListItem>> GetStructureCode(string roadcode, string locationch);
        Task<List<SelectListItem>> GetAIBound(string roadcode, string locationch, string structurecode);
        Task<List<SelectListItem>> GetPostSpacing(string roadcode, string locationch, string structurecode, string bound);
        Task<AssetDDLResponseDTO> GetAssetDDL(AssetDDLRequestDTO request);
        FORMF2Rpt GetReportData(int headerid);
        Byte[] FormDownload(string formname, int id, string filepath);
        Task<int> UpdateF2Header(FormF2HeaderRequestDTO requestDTO);
        Task<List<FormF2DetailRequestDTO>> GetF2DetailList(int headerId);
    }
}
