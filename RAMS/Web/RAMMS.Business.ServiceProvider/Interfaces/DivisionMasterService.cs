using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IDivisionService
    {
        long LastInsertedNo();
        Task<DivisionRequestDTO> GetById(int id);
        Task<int> Save(DivisionRequestDTO model); Task<bool> Remove(int id);
        Task<PagingResult<DivisionRequestDTO>> GetList(FilteredPagingDefinition<DivisionRequestDTO> filterOptions);

        Task<PagingResult<DivRmuSectionRequestDTO>> GetList(FilteredPagingDefinition<DivRmuSectionRequestDTO> filterOptions);

        List<SelectListItem> GetList();

        Task<int> Save(DivRmuSectionRequestDTO model);

        Task<DivRmuSectionRequestDTO> GetDivRmuSectionById(int id);
    }
}
