using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RAMMS.DTO.RequestBO;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IImageService
    {
        Task<int> SaveImageDtl(List<ImageListRequestDTO> imagelist);
    }
}
