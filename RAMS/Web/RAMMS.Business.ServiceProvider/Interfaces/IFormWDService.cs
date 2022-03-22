using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormWDService
    {
        Task<FormWDResponseDTO> FindWDByW1ID(int id);
        Task<FormWDResponseDTO> FindFormWDByID(int id);
        Task<IEnumerable<FormWDDtlResponseDTO>> FindFormWDDtlByID(int id);
        Task<int> SaveFormWD(FormWDResponseDTO FormW1);
        Task<int> Update(FormWDResponseDTO FormW1);
        int? DeleteFormWDClause(int Id);
        int? SaveFormWDClause(FormWDDtlResponseDTO FormWDDtl);
       Task<int> DeActivateFormWD(int formNo);

    }
}
