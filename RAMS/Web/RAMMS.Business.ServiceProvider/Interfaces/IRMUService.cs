using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IRMUService
    {
        long LastInsertedNo();
        Task<RMURequestDTO> GetById(int id);
        Task<int> Save(RMURequestDTO model);
        Task<bool> Remove(int id);
        Task<PagingResult<RMURequestDTO>> GetList(FilteredPagingDefinition<RMURequestDTO> filterOptions);
        List<SelectListItem> GetList(string divcode);
    }
}
