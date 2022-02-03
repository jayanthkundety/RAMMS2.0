using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormB1B2DetailRepository : IRepositoryBase<RmFormB1b2BrInsDtl>
    {
        Task<long> GetFilteredRecordCount(FilteredPagingDefinition<FormB1B2DetailRequestDTO> filterOptions);
        Task<List<FormB1B2DetailRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormB1B2DetailRequestDTO> filterOptions);
    }
}
