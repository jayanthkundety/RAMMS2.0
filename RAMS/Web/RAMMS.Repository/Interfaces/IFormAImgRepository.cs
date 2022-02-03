using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAMMS.Domain.Models;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormAImgRepository : IRepositoryBase<RmFormaImageDtl>
    {
        List<RmFormaImageDtl> SaveFormAImageDtl(List<RmFormaImageDtl> rmFormAImageDtls);
        Task<List<RmFormaImageDtl>> GetAllImageByAssetPK(int assetPK);
        Task<int> DectivateAssetImage(int assetImgId);
        Task<int> LastInsertedSRNO(int hederId, string type);
    }
}
