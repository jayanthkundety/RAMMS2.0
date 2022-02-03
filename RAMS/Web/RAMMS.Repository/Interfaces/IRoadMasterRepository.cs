using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IRoadMasterRepository : IRepositoryBase<RmRoadMaster>
    {
        IEnumerable<SelectListItem> GetLoadData(RmRoadMaster _road);
        Task<RmRoadMaster> GetAllRMData(RoadMasterRequestDTO _RMAllDataDto);
        Task<IEnumerable<RmRoadMaster>> GetRMDataLookup(RoadMasterRequestDTO _RMAllData);
        IEnumerable<RmAllassetInventory> GetGridData();
        IEnumerable<AssetFieldDtl> GetAssetFieldDtls();
        IEnumerable<RmFormDownloadUse> GetAllAssetFormDownloadUse();

        IEnumerable<RmFormGenDtl> GetFormGenDtls();

        Task<List<RmRoadMaster>> GetRMUBasedData(RoadMasterRequestDTO requestDto);

        Task<AssetDDLResponseDTO> GetFilteredList(AssetDDLRequestDTO roadMaster);
        Task<List<string>> GetSectionByRMU(RoadMasterRequestDTO roadMaster);
        Task<IEnumerable<RmRoadMaster>> GetAllRoadCode();
        Task<List<string>> GetRdCodeBySection(string section);
        Task<int?> GetRoadNo(string roadCode);
        Task<RmRoadMaster> GetByRdCode(string roadCode);

  
    }
}
