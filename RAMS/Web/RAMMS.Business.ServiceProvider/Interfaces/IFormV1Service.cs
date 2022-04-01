using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormV1Service
    {

        Task<PagingResult<FormV1ResponseDTO>> GetFilteredFormV1Grid(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions);

        //Task<int> SaveFormWD(FormWDResponseDTO FormW1);
        //Task<int> DeActivateFormWD(int formNo);

    }
}
