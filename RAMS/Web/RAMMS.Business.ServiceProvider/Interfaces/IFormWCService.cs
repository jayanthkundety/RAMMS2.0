using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormWCService
    {
        Task<int> Save(FormWCResponseDTO formWCHeaderBO);

        Task<FormWCResponseDTO> FindWCByID(int id);

        Task<FormW1ResponseDTO> GetFormW1ById(int formW1Id);

        Task<int> Update(FormWCResponseDTO formWCDTO);
    }
}
