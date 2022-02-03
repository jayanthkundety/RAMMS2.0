using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.SearchBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IAssetRepository : IRepositoryBase<RmAllassetInventory>
    {
        Task BulkImportAssets(List<RmAllassetInventory> assetsToImport);
        Task<List<RmAllassetInventory>> GetFilteredRecordList(FilteredPagingDefinition<AssetSearch> filterOptions);
        Task<List<RmAllassetInventory>> GetFilteredRecordListWithOthers(FilteredPagingDefinition<AssetSearch> filterOptions);
        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<AssetSearch> filterOptions);
        Task<(string, List<SelectListItem>)> SaveAssetImport(DataTable data, string exportLocation, string localLocation, string tempLocation, int userId);
        Task<List<AssetFieldDtl>> GetAssetFieldDtls(AssetFieldDtlReqDTO filterOptions);

        Task<List<AssetImport>> GetAssetImports(Guid fileId);
        Task<List<RmAssetImageDtl>> GetAssetImageDtls();
        Task<int> GetAssetPK(string assetID);
        Task<List<RmAssetImageDtl>> GetAllImageByAssetPK(int assetPK);
        Task<RmAssetImageDtl> GetDocById(int accUccId);
        Task<RmAssetImageDtl> GetImageByIdAsync(int assetImgId);
        void DeActivateAssetImage(RmAssetImageDtl assetImage);
        Task<int> GetId(int headerId, string type);
        Task<List<AssetFieldDtl>> GetAssetTemplate(string assetType);
        Task<RmAllassetInvOthers> GetOtherAssetByIdAsync(int id);
        void CreateOthers(RmAllassetInvOthers rmAllAsset);
        Task<RmAllassetInvOthers> CreateOtherReturnEntity(RmAllassetInvOthers rmAllAsset);
        void UpdateOthers(RmAllassetInvOthers rmAllAsset);
        string GetAssetCodeByName(string name);
        Task<int> CheckRefNo(string refNo);
        Task<List<DTO.ResponseBO.AssetId>> ListOfCulvertAssestIds();
        IQueryable<RmAllassetInventory> ListOfAssestByRoadCode(string roadCode);
    }
}
