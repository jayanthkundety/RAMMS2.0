using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Repository.Interfaces
{
    public interface IDivRmuSectionRepository : IRepositoryBase<RmDivRmuSecMaster>
    {
        Task<long> GetFilteredRecordCount(FilteredPagingDefinition<DivRmuSectionRequestDTO> filterOptions);
        Task<List<DivRmuSectionRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<DivRmuSectionRequestDTO> filterOptions);
    }
}
