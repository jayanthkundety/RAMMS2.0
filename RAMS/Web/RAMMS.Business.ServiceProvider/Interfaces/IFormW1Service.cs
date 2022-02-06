using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormW1Service
    {
        public Task<int> SaveFormW1(FormW1ResponseDTO FormW1);
    }
}
