using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IAdministratorRepository
    {
        Task<GridWrapper<object>> GridList(DataTableAjaxPostModel searchData, string type);
        RmDivRmuSecMaster SaveSection(RmDivRmuSecMaster section);
        void SaveAssetType(RmAssetGroupType asset);
        void SaveLookup(RmDdLookup lookup);
        void SaveDefect(RmAssetDefectCode defect);
        Task<List<RmAssetGroupType>> AssetGroupList();
        Task<List<RmAssetGroupType>> DefectAssetGroupList();
    }
}
