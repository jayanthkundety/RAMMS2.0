using System;
using System.Threading.Tasks;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IAuditTransactionService
    {
        long LastAuditTransactionInsertedNo();
        Task<AuditTransactionRequestDTO> GetAuditTransactionById(int id);
        Task<long> SaveAuditTransaction(AuditTransactionRequestDTO model); Task<bool> RemoveAuditTransaction(int id);
        Task<PagingResult<AuditTransactionRequestDTO>> GetAuditTransactionList(FilteredPagingDefinition<AuditTransactionRequestDTO> filterOptions);
    }
}
