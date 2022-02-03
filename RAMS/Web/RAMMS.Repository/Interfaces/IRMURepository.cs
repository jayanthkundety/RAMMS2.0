using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Repository.Interfaces
{
    public interface IRMURepository : IRepositoryBase<RmRmuMaster>
    {
        Task<long> GetFilteredRecordCount(FilteredPagingDefinition<RMURequestDTO> filterOptions);
        Task<List<RMURequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<RMURequestDTO> filterOptions);
    }
}
