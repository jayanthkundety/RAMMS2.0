using System;
using System.Collections.Generic;
using System.Text;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.RequestBO;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.Wrappers;
using System.Data;
using RAMMS.DTO.SearchBO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IAssetsService
    {
        Task<AssetListRequestDTO> GetAssetById(int assetId);
        Task<AssetInvOtherReqDto> GetOtherAssetByIdAsync(int assetId);
        Task<int> SaveAssetAsync(AssetListRequestDTO assetsListResponse);
        Task<int> SaveOtherAssetAsync(AssetInvOtherReqDto assetsListRequest);
        Task<int> UpdateAssetAsync(AssetListRequestDTO assetsListResponse);
        Task<int> DeActivateAssetAsync(int assetId);
        Task<PagingResult<AssetsListResponseDTO>> GetFilteredAssets(FilteredPagingDefinition<AssetSearch> filterOptions);
        Task<PagingResult<AssetsListResponseDTO>> GetFilteredAssetswithOthers(FilteredPagingDefinition<AssetSearch> filterOptions);
        string GetAssetNameByCode(string assetCode);
        string GetAssetCodeByName(string assetName);
        Task<(string, List<SelectListItem>)> SaveAssetImport(DataTable data, string exportLocation, string localLocation,string tempLocation,int userId);
        Task<bool> AssetimportRemove(Guid fileId);
        Task<List<AssetFieldDtlResDTO>> GetAssetFieldByCode(AssetFieldDtlReqDTO assetFieldReq);
        Task<List<RmAssetImageDtl>> GetImageDTLByAssetId(int id);

        Task<List<AssetImport>> GetAssetImports(Guid fileId);
        Task<List<AssetImageDtlDTO>> GetAssetImageDtls();
        Task<List<ImageListRequestDTO>> GetFilterImageList(string imageTypeCode);
        Task<List<ImageListRequestDTO>> GetAllImageList();
        Task<int> GetAssetPK(string assetId);
        Task<List<ImageListRequestDTO>> GetAllImageByAssetPK(int assetPK);
        Task<ImageListRequestDTO> GetDocById(int formXId);
        Task<int> DectivateAssetImage(int assetImgId);
        Task<int> LastInsertedSRNO(int hederId, string type);
        Task<List<AssetFieldDtl>> GetAssetTemplate(string assetType);
        Task<int> CheckAssetRefNo(string refNo);
        Task<List<DTO.ResponseBO.AssetId>> ListOfCulvertAssestIds();
    }
}
