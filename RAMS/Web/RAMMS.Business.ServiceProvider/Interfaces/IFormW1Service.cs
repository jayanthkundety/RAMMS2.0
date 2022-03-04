using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormW1Service
    {
        
        Task<List<FormIWImageResponseDTO>> GetImageList(string IwRefNo);
        Task<int> LastInsertedIMAGENO(string hederId, string type);

         Task<int> LastInsertedIMAGENO(int hederId, string type);
        Task<FormW1ResponseDTO> FindFormW1ByID(int id);
        Task<int> SaveFormW1(FormW1ResponseDTO FormW1);
        Task<int> Update(FormW1ResponseDTO FormW1);
        Task<int> SaveImage(List<FormIWImageResponseDTO> image);

        Task<int> DeActivateImage(int imageId);

        //Task<int> GetImageIdByW1Id(int formW1Id, string type);

    }
}
