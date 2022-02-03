using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.RequestBO;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Repository;
using RAMMS.Domain.Models;
using RAMMS.Repository.Interfaces;
using AutoMapper;
using RAMMS.DTO;
using RAMMS.DTO.Wrappers;
using System.Data;
using RAMMS.DTO.SearchBO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class AssetsService : IAssetsService
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private List<AssetMaster> assetList = new List<AssetMaster>();
        private readonly ISecurity _security;
        public AssetsService(IRepositoryUnit repoUnit, IMapper mapper,ISecurity security)
        {
            _repoUnit = repoUnit;
            _mapper = mapper;
            _security = security;
            InitializeAssets();
        }

        private void InitializeAssets()
        {
            assetList.Add(new AssetMaster("BR", "Bridge"));
            assetList.Add(new AssetMaster("CV", "Culvert"));
            assetList.Add(new AssetMaster("CLM", "Centre Line Marking"));
            assetList.Add(new AssetMaster("CW", "Carriageway"));
            assetList.Add(new AssetMaster("DI", "Ditches"));
            assetList.Add(new AssetMaster("DR", "Drain"));
            assetList.Add(new AssetMaster("ELM", "Edge Line Marking"));
            assetList.Add(new AssetMaster("GR", "Guardrail"));
            assetList.Add(new AssetMaster("RS", "Road Stud"));
            assetList.Add(new AssetMaster("RW", "Retaining Wall"));
            assetList.Add(new AssetMaster("SG", "Signs"));
            assetList.Add(new AssetMaster("SH", "Shoulder"));
            assetList.Add(new AssetMaster("RLM", "Roadline Marking"));
        }
        public async Task<PagingResult<AssetsListResponseDTO>> GetFilteredAssets(FilteredPagingDefinition<AssetSearch> filterOptions)
        {
            PagingResult<AssetsListResponseDTO> result = new PagingResult<AssetsListResponseDTO>();
            try
            {

                var filteredRecords = await _repoUnit.AllAssetRepository.GetFilteredRecordList(filterOptions);

                result.TotalRecords = await _repoUnit.AllAssetRepository.GetFilteredRecordCount(filterOptions).ConfigureAwait(false);

                result.PageResult = _mapper.Map<List<AssetsListResponseDTO>>(filteredRecords);

                result.PageNo = filterOptions.StartPageNo;

                result.FilteredRecords = result.PageResult != null ? result.PageResult.Count : 0;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return result;
        }

        public async Task<int> SaveAssetAsync(AssetListRequestDTO assetsListRequest)
        {
            int refNO;
            try
            {
                assetsListRequest.RoadMasterNo = await _repoUnit.RoadmasterRepository.GetRoadNo(assetsListRequest.RoadCode);
                var domainModelAsset = _mapper.Map<RmAllassetInventory>(assetsListRequest);

                if(domainModelAsset.AiFrmCh == null && domainModelAsset.AiFrmChDeci != null)
                {
                    domainModelAsset.AiFrmCh = 0;
                }
                if (domainModelAsset.AiToCh == null && domainModelAsset.AiToChDeci != null)
                {
                    domainModelAsset.AiToCh = 0;
                }
                if (domainModelAsset.AiLocChKm == null && domainModelAsset.AiLocChM != null)
                {
                    domainModelAsset.AiLocChKm = 0;
                }

                var result = _repoUnit.AllAssetRepository.CreateReturnEntity(domainModelAsset);              
                await _repoUnit.CommitAsync();
                refNO = result.AiPkRefNo;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return refNO;
        }

        public async Task<int> SaveOtherAssetAsync(AssetInvOtherReqDto assetsListRequest)
        {
            int rowsAffected = 0;
            AssetInvOtherResDTO assetInvOtherResDTO = new AssetInvOtherResDTO();
            int id = 0;
            try
            {
                var domainModelAsset = _mapper.Map<RmAllassetInvOthers>(assetsListRequest);

                if (domainModelAsset.AioPkRefNo != 0)
                {
                    domainModelAsset.AioActiveYn = true;
                    _repoUnit.AllAssetRepository.UpdateOthers(domainModelAsset);
                    await _repoUnit.CommitAsync();
                }
                else
                {
                    domainModelAsset.AioActiveYn = true;
                    var result=await _repoUnit.AllAssetRepository.CreateOtherReturnEntity(domainModelAsset);
                    id = result.AioPkRefNo;
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return id;
        }

        public async Task<AssetInvOtherReqDto> GetOtherAssetByIdAsync(int assetId)
        {

            var asset = await _repoUnit.AllAssetRepository.GetOtherAssetByIdAsync(assetId).ConfigureAwait(false);
            return _mapper.Map<AssetInvOtherReqDto>(asset);
        }

        public async Task<int> UpdateAssetAsync(AssetListRequestDTO assetsListRequest)
        {
            int rowsAffected;
            assetsListRequest.RoadMasterNo = await _repoUnit.RoadmasterRepository.GetRoadNo(assetsListRequest.RoadCode);
            try
            {
                var domainModelAsset = _mapper.Map<RmAllassetInventory>(assetsListRequest);

                if (domainModelAsset.AiFrmCh == null && domainModelAsset.AiFrmChDeci != null)
                {
                    domainModelAsset.AiFrmCh = 0;
                }
                if (domainModelAsset.AiToCh == null && domainModelAsset.AiToChDeci != null)
                {
                    domainModelAsset.AiToCh = 0;
                }
                if (domainModelAsset.AiLocChKm == null && domainModelAsset.AiLocChM != null)
                {
                    domainModelAsset.AiLocChKm = 0;
                }

                _repoUnit.AllAssetRepository.Update(domainModelAsset);

                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return rowsAffected;
        }
        public async Task<int> DeActivateAssetAsync(int assetId)
        {
            int rowsAffected;
            try
            {
                var domainModelAsset = await _repoUnit.AllAssetRepository.GetByIdAsync(assetId);

                domainModelAsset.AiActiveYn = false;
                domainModelAsset.AiModBy = _security.UserID.ToString();
                domainModelAsset.AiModDt = DateTime.Now;
                _repoUnit.AllAssetRepository.Update(domainModelAsset);
                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return rowsAffected;
        }
        public async Task<AssetListRequestDTO> GetAssetById(int assetId)
        {

            var asset = await _repoUnit.AllAssetRepository.GetByIdAsync(assetId).ConfigureAwait(false);
            return _mapper.Map<AssetListRequestDTO>(asset);
        }

        public string GetAssetNameByCode(string assetCode)
        {
            return _repoUnit.AllAssetRepository.GetAssetNameByCode(assetCode);
        }

        public string GetAssetCodeByName(string assetName)
        {

            return _repoUnit.AllAssetRepository.GetAssetCodeByName(assetName);
           
        }

        public async Task<(string, List<SelectListItem>)> SaveAssetImport(DataTable data, string exportLocation, string localLocation,string  tempLocation,int userId)
        {
            return await _repoUnit.AllAssetRepository.SaveAssetImport(data, exportLocation, localLocation, tempLocation,userId);
        }

        public async Task<bool> AssetimportRemove(Guid fileId)
        {
            return await _repoUnit.AllAssetRepository.AssetImportRemove(fileId);
        }

        public async Task<List<AssetFieldDtlResDTO>> GetAssetFieldByCode(AssetFieldDtlReqDTO assetFieldReq)
        {
            List<AssetFieldDtlResDTO> result = new List<AssetFieldDtlResDTO>();
            try
            {
                var assetFielList = await _repoUnit.AllAssetRepository.GetAssetFieldDtls(assetFieldReq);
                foreach (var listData in assetFielList)
                {
                    result.Add(_mapper.Map<AssetFieldDtlResDTO>(listData));
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return result;
        }

        public async Task<List<RmAssetImageDtl>> GetImageDTLByAssetId(int id)
        {
            List<RmAssetImageDtl> result = new List<RmAssetImageDtl>();
            try
            {
                var assetImageDtls = await _repoUnit.AllAssetRepository.GetImageDTLByAssetId(id);

                foreach (var listData in assetImageDtls)
                {
                    result.Add(_mapper.Map<RmAssetImageDtl>(listData));
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return result;
        }

        public async Task<List<AssetImport>> GetAssetImports(Guid fileId)
        {
            List<AssetImport> result = new List<AssetImport>();
            try
            {
                result = await _repoUnit.AllAssetRepository.GetAssetImports(fileId);
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return result;
        }
        public async Task<List<AssetImageDtlDTO>> GetAssetImageDtls()
        {
            List<AssetImageDtlDTO> assetImageListRefNo = new List<AssetImageDtlDTO>();
            try
            {
                var assetFielList = await _repoUnit.AllAssetRepository.GetAssetImageDtls();
                foreach (var listData in assetFielList)
                {
                    assetImageListRefNo.Add(_mapper.Map<AssetImageDtlDTO>(listData));
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return assetImageListRefNo;
        }

        public async Task<List<ImageListRequestDTO>> GetAllImageList()
        {
            List<ImageListRequestDTO> imageList = new List<ImageListRequestDTO>();
            try
            {
                var image = await _repoUnit.AllAssetRepository.GetAllImageList().ConfigureAwait(false);
                foreach (var listData in image)
                {
                    imageList.Add(_mapper.Map<ImageListRequestDTO>(listData));
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return imageList;
        }

        public async Task<List<ImageListRequestDTO>> GetFilterImageList(string imageTypeCode)
        {
            List<ImageListRequestDTO> imageList = new List<ImageListRequestDTO>();
            try
            {
                var image = await _repoUnit.AllAssetRepository.GetFilterImageList(imageTypeCode).ConfigureAwait(false);
                foreach (var listData in image)
                {
                    imageList.Add(_mapper.Map<ImageListRequestDTO>(listData));
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return imageList;
        }

        public async Task<int> GetAssetPK(string assetId)
        {
            return await _repoUnit.AllAssetRepository.GetAssetPK(assetId);
        }

        public async Task<List<ImageListRequestDTO>> GetAllImageByAssetPK(int assetPK)
        {
            List<ImageListRequestDTO> imageList = new List<ImageListRequestDTO>();
            try
            {
                var images = await _repoUnit.AllAssetRepository.GetAllImageByAssetPK(assetPK).ConfigureAwait(false);
                foreach (var image in images)
                {
                    imageList.Add(_mapper.Map<ImageListRequestDTO>(image));
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return imageList;
        }
        public async Task<ImageListRequestDTO> GetDocById(int formXId)
        {
            ImageListRequestDTO assetDoc = new ImageListRequestDTO();
            try
            {
                var getList = await _repoUnit.AllAssetRepository.GetDocById(formXId);
                assetDoc = _mapper.Map<ImageListRequestDTO>(getList);
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return assetDoc;
        }
        public async Task<int> DectivateAssetImage(int assetImgId)
        {
            int rowsAffected;
            try
            {
                var assetImg = await _repoUnit.AllAssetRepository.GetImageByIdAsync(assetImgId).ConfigureAwait(false);
                assetImg.AidActiveYn = false;
                assetImg.AidModBy = _security.UserID.ToString();
                assetImg.AidModDt = DateTime.Now;
                _repoUnit.AllAssetRepository.DeActivateAssetImage(assetImg);
                rowsAffected = await _repoUnit.CommitAsync();

                return rowsAffected;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }            
        }

        public async Task<int> LastInsertedSRNO(int hederId, string type)
        {
            int imageCt = await _repoUnit.AllAssetRepository.GetId(hederId, type);
            return imageCt;
        }

        public async Task<List<AssetFieldDtl>> GetAssetTemplate(string assetType)
        {
            List<AssetFieldDtl> assetFieldDtls = await _repoUnit.AllAssetRepository.GetAssetTemplate(assetType);
            return assetFieldDtls;
        }

        public async Task<int> CheckAssetRefNo(string refNo)
        {
            try
            {
                return await _repoUnit.AllAssetRepository.CheckRefNo(refNo);
            }
            catch(Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public async Task<PagingResult<AssetsListResponseDTO>> GetFilteredAssetswithOthers(FilteredPagingDefinition<AssetSearch> filterOptions)
        {
            PagingResult<AssetsListResponseDTO> result = new PagingResult<AssetsListResponseDTO>();
            try
            {

                var filteredRecords = await _repoUnit.AllAssetRepository.GetFilteredRecordListWithOthers(filterOptions);

                result.TotalRecords = await _repoUnit.AllAssetRepository.GetFilteredRecordCount(filterOptions).ConfigureAwait(false);

                result.PageResult = _mapper.Map<List<AssetsListResponseDTO>>(filteredRecords);

                result.PageNo = filterOptions.StartPageNo;

                result.FilteredRecords = result.PageResult != null ? result.PageResult.Count : 0;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return result;
        }

        public async Task<List<DTO.ResponseBO.AssetId>> ListOfCulvertAssestIds()
        {
            return await _repoUnit.AllAssetRepository.ListOfCulvertAssestIds();
        }
    }
}
