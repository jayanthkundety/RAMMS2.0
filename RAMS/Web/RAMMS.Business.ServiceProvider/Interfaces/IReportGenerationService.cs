using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IReportGenerationService
    {
        List<SelectListItem> LoadProdData(RmRoadMaster _rmroad);
        List<RmRoadMaster> LoadAllRMDataProd(RmRoadMaster _rmroad);
        List<RmRoadMaster> LoadRMLookupDataProd(RmRoadMaster _rmroad);
        //BridgeMainGridprov
        List<RmAllassetInventory> GetBridgeGridProv();
        List<RmAllassetInventory> GetBridgeGridP();

        List<AssetFieldDtl> GetAssetFieldDtls();

        List<RmFormDownloadUse> GetAllRmFormDownloadUses();

        List<RmFormGenDtl> GetFormGenDtls();
        List<RAMMS.Domain.Models.FormDownloadHeader> GetDownloadHeaders(string Formtype, int id, string Hdr_DTL);

        List<RmAssetImageDtl> SaveAssetImageDtlProvider(List<RmAssetImageDtl> rmAssetImageDtls);
        IEnumerable<RmAssetImageDtl> GetUploadedImageProvider();

        List<SearchBridge> GetBridgeGrid();
        List<SearchBridge> SearchBridgeGridProv(string assetGroup, string InputValue);


        List<RAMMS.Domain.Models.FormDownloadHeader> GetDownloadDetails(string Formtype, int id, string Hdr_DTL);
    }
}
