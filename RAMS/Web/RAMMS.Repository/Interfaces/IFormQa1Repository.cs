using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormQa1Repository : IRepositoryBase<RmFormQa1Hdr>
    {
        Task<List<RmFormQa1Hdr>> GetFilteredRecordList(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions);
        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions);
    }
}
