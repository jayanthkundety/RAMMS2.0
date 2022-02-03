using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Domain.Models;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormB1B2HeaderRepository : IRepositoryBase<RmFormB1b2BrInsHdr>
    {
        Task<long> GetFilteredRecordCount(FilteredPagingDefinition<FormB1B2SearchGridDTO> filterOptions);
        Task<List<FormB1B2HeaderRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormB1B2SearchGridDTO> filterOptions);
        IEnumerable<SelectListItem> GetBridgeIds(AssetDDLRequestDTO request);
        Task<FormB1B2HeaderRequestDTO> GetBrideDetailById(long id);
        List<FormB1B2Rpt> GetReportData(int id);
        Task<AssetDDLResponseDTO> GetAssetDDL(AssetDDLRequestDTO request);
        string GetMaterialType(string type, string value);
    }
}
