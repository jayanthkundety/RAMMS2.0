using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormW1Service
    {

        Task<FormW1ResponseDTO> FindFormW1ByID(int id);
        Task<int> SaveFormW1(FormW1ResponseDTO FormW1);
        Task<int> Update(FormW1ResponseDTO FormW1);
    }
}
