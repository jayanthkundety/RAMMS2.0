using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAMMS.Domain.Models;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormB1B2ImgRepository : IRepositoryBase<RmFormB1b2BrInsImage>
    {
        List<RmFormB1b2BrInsImage> SaveImageDtl(List<RmFormB1b2BrInsImage> _RmFormB1b2BrInsImages);
        Task<List<RmFormB1b2BrInsImage>> GetAllImageByHeaderId(int dtlId);
        Task<int> DectivateAssetImage(int assetimgId);
        Task<int> LastInsertedSRNO(int hederid, string type);
        bool CheckImageExistforAllType(string[] lookup, int headerid);
    }
}
