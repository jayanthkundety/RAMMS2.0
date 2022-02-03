using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Repository.Interfaces
{
    public interface IDivisonRepository : IRepositoryBase<RmDivisionMaster>
    {
        Task<long> GetFilteredRecordCount(FilteredPagingDefinition<DivisionRequestDTO> filterOptions);
        Task<List<DivisionRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<DivisionRequestDTO> filterOptions);

       
    }
}
