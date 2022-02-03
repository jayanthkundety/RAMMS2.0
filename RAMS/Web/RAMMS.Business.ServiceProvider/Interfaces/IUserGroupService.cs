using System;
using System.Threading.Tasks;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IModuleGroupRightsService
    {
        long LastDetailInsertedNo();
        Task<ModuleGroupRightsRequestDTO> GetDetailById(int id);
        Task<int> SaveDetail(ModuleGroupRightsRequestDTO model); Task<bool> RemoveDetail(int id);
        Task<PagingResult<ModuleGroupRightsRequestDTO>> GetDetailList(FilteredPagingDefinition<ModuleGroupRightsRequestDTO> filterOptions);
    }
}
