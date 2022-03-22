using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormWGService
    {
        Task<int> Save(FormWGResponseDTO formWGHeaderBO);

        Task<FormWGResponseDTO> FindWGByID(int id);

        Task<FormWGResponseDTO> FindWGByW1ID(int id);

        Task<FormW1ResponseDTO> GetFormW1ById(int formW1Id);

        Task<int> Update(FormWGResponseDTO formWGDTO);

        Task<int> Delete(int id);
    }
}
