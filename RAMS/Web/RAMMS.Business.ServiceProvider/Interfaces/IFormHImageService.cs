using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAMMS.DTO.RequestBO;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormHImageService
    {
        Task<int> SaveImageDtl(List<FormHImageListRequestDTO> imageList);
        Task<List<FormHImageListRequestDTO>> GetAllImageByAssetPK(int assetPK);
        Task<int> DectivateAssetImage(int assetImgId);
        Task<int> LastInsertedSRNO(int hederId, string type);
    }
}
