using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Repository.Interfaces
{
    public interface IAuditTransactionRepository : IRepositoryBase<RmAuditLogTransaction>
    {
        Task<long> GetFilteredRecordCount(FilteredPagingDefinition<AuditTransactionRequestDTO> filterOptions);
        Task<List<AuditTransactionRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<AuditTransactionRequestDTO> filterOptions);
    }
}
