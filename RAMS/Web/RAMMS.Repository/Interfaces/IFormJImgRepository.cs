using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAMMS.Domain.Models;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormJImgRepository : IRepositoryBase<RmFormjImageDtl>
    {
        List<RmFormjImageDtl> SaveFormAImageDtl(List<RmFormjImageDtl> rmFormJImageDtls);
        Task<List<RmFormjImageDtl>> GetAllImageByAssetPK(int assetPK);
        Task<int> DectivateAssetImage(int assetImgId);
        Task<int> LastInsertedSRNO(int hederId, string type);
    }
}
