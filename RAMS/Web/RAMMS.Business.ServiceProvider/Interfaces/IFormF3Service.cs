using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormF3Service
    {
        //Task<FormF3ResponseDTO> FindF3ByW1ID(int id);
        //Task<FormF3ResponseDTO> FindFormF3ByID(int id);
        //Task<IEnumerable<FormF3DtlResponseDTO>> FindFormF3DtlByID(int id);

        // Task<PagingResult<FormF2HeaderRequestDTO>> GetHeaderList(FilteredPagingDefinition<FormF2SearchGridDTO> filterOptions);

        Task<FormF3ResponseDTO> GetHeaderById(int id);
        Task<int> SaveFormF3(FormF3ResponseDTO FormW1);
        Task<int> Update(FormF3ResponseDTO FormW1);
        int? DeleteFormF3Dtl(int Id);
        int? SaveFormF3Dtl(FormF3DtlResponseDTO FormF3Dtl);
       Task<int> DeActivateFormF3(int formNo);

    }
}
