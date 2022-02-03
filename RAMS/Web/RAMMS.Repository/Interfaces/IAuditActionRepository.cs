using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Repository.Interfaces
{
    public interface IAuditActionRepository : IRepositoryBase<RmAuditLogAction>
    {
        Task<long> GetFilteredRecordCount(FilteredPagingDefinition<AuditActionRequestDTO> filterOptions);
        Task<List<AuditActionRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<AuditActionRequestDTO> filterOptions);
    }
}
