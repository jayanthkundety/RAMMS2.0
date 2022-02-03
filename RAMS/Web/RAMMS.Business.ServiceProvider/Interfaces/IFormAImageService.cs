using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAMMS.DTO.RequestBO;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormAImageService
    {
        Task<int> SaveImageDtl(List<FormAImageListRequestDTO> imageList);
        Task<List<FormAImageListRequestDTO>> GetAllImageByAssetPK(int assetPK);
        Task<int> DectivateAssetImage(int assetImgId);
        Task<int> LastInsertedSRNO(int hederId, string type);
    }
}
