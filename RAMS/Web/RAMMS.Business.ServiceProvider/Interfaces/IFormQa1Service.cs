using RAMMS.DTO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormQa1Service
    {
        Task<PagingResult<FormQa1HeaderDTO>> GetFilteredFormQa1Grid(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions);

    }
}
