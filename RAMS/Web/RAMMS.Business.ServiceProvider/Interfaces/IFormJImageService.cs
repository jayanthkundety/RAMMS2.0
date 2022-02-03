using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAMMS.DTO.RequestBO;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormJImageService
    {
        Task<int> SaveImageDtl(List<FormJImageListRequestDTO> imageList);
        Task<List<FormJImageListRequestDTO>> GetAllImageByAssetPK(int assetPK);
        Task<int> DectivateAssetImage(int assetImgId);
        Task<int> LastInsertedSRNO(int hederId, string type);
    }
}
