using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IRoadMasterService
    {
        Task<RoadMasterResponseDTO> GetRMAllData(RoadMasterRequestDTO _Rmroad);
        Task<List<RoadMasterResponseDTO>> GetRMLookupData(RoadMasterRequestDTO _Rmroad);
        Task<List<RoadMasterResponseDTO>> GetRMUBasedData(RoadMasterRequestDTO _Rmroad);
        Task<List<RoadMasterResponseDTO>> GetRM_RoadCode_Service(RoadMasterRequestDTO _Rmroad);
        Task<RoadMasterResponseDTO> GetAllRoadCodeData(RoadMasterRequestDTO _Rmroad);
        Task<AssetDDLResponseDTO> GetAssetDDL(AssetDDLRequestDTO assetDDL);
        Task<IEnumerable<SelectListItem>> GetroadCodeValuByRMU(RoadMasterRequestDTO DdLookUp);

        Task<RoadMasterResponseDTO> GetAllRoadCodeDataBySectionCode(RoadMasterRequestDTO _Rmroad);
        Task<IEnumerable<CSelectListItem>> GetAllRoadCodeAndName(bool IsPKIDValue = false);
        Task<IEnumerable<SelectListItem>> GetAllRoadCodeAndNameTab();

        Task<RoadMasterResponseDTO> GetByRdCode(string roadCode);

    }
}
