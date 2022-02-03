using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Repository.Interfaces
{
    public interface IModuleGroupRepository : IRepositoryBase<RmModuleGroupRights>
    {
        Task<long> GetFilteredRecordCount(FilteredPagingDefinition<ModuleGroupRightsRequestDTO> filterOptions);
        Task<List<ModuleGroupRightsRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<ModuleGroupRightsRequestDTO> filterOptions);
    }
}
