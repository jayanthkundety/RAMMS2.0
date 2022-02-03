using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.RequestBO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IAdministrationService
    {
        Task<GridWrapper<object>> GridList(DataTableAjaxPostModel searchData, string type);
        void Save(AdministratorDTO administratorDTO, string createdBy);
        void Delete(AdministratorDTO administratorDTO, string createdBy);
        Task<List<CSelectListItem>> AssetGroupList();
        Task<List<CSelectListItem>> DefectAssetGroupList();
    }
}
