using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormB1B2Service
    {
        long LastDetailInsertedNo();
        Task<FormB1B2DetailRequestDTO> GetDetailById(int id);
        Task<int> SaveDetail(FormB1B2DetailRequestDTO model);
        Task<bool> RemoveDetail(int id);
        Task<PagingResult<FormB1B2DetailRequestDTO>> GetDetailList(FilteredPagingDefinition<FormB1B2DetailRequestDTO> filterOptions);
        long LastHeaderInsertedNo();
        Task<FormB1B2HeaderRequestDTO> GetHeaderById(int id);
        Task<int> SaveHeader(FormB1B2HeaderRequestDTO model);
        Task<bool> RemoveHeader(int id);
        Task<PagingResult<FormB1B2HeaderRequestDTO>> GetHeaderList(FilteredPagingDefinition<FormB1B2SearchGridDTO> filterOptions);
        IEnumerable<SelectListItem> GetBridgeIds(AssetDDLRequestDTO request);
        Task<FormB1B2HeaderRequestDTO> GetBrideDetailById(long id);
        Task<(bool IsExist, int PkRefNo)> AlreadyExists(int assetid, int year);
        Task<int> SaveImageDtl(List<FormB1B2ImgRequestDTO> imagelist);
        Task<List<FormB1B2ImgRequestDTO>> GetAllImageByAssetPK(int assetPK);
        Task<int> DectivateAssetImage(int assetimgId);
        Task<int> ImageLastInsertedSRNO(int hederid, string type);
        List<FormB1B2Rpt> GetReportData(int headerid);
        Byte[] FormDownload(string formname, int id, string basepath, string filepath);
        Task<AssetDDLResponseDTO> GetAssetDDL(AssetDDLRequestDTO request);
        Task<int> UpdateB1B2(FormB1B2HeaderRequestDTO formB1B2); //Tab
    }
}
