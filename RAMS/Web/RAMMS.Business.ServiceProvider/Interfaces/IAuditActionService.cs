using System;
using System.Threading.Tasks;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IAuditActionService
    {
        long LastAuditActionInsertedNo();
        Task<AuditActionRequestDTO> GetAuditActionById(int id);
        Task<long> SaveAuditAction(AuditActionRequestDTO model); Task<bool> RemoveAuditAction(int id);
        Task<PagingResult<AuditActionRequestDTO>> GetAuditActionList(FilteredPagingDefinition<AuditActionRequestDTO> filterOptions);
    }
}
