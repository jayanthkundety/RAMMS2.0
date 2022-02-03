using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormF2DetailRepository : IRepositoryBase<RmFormF2GrInsDtl>
    {
        Task<long> GetFilteredRecordCount(FilteredPagingDefinition<FormF2DetailRequestDTO> filterOptions);
        Task<List<FormF2DetailRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormF2DetailRequestDTO> filterOptions);
        Task<List<SelectListItem>> GetLocationCh(string roadcode);
        Task<List<SelectListItem>> GetStructureCode(string roadcode, string locationch);
        Task<List<SelectListItem>> GetAIBound(string roadcode, string locationch, string structurecode);
        Task<List<SelectListItem>> GetPostSpacing(string roadcode, string locationch, string structurecode, string bound);
        Task<List<RmFormF2GrInsDtl>> GetF2DetailList(int headerId);
    }
}
