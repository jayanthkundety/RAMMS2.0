using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{

    public interface IFormWNService
    {


        Task<FormWNResponseDTO> FindWNByW1ID(int id);
        Task<FormWNResponseDTO> FindFormWNByID(int id);
        Task<int> SaveFormWN(FormWNResponseDTO FormW1);
        Task<int> Update(FormWNResponseDTO FormW1);

    }

}
