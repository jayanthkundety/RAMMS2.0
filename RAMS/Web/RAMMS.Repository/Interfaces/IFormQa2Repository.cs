using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormQa2Repository : IRepositoryBase<RmFormQa2Hdr>
    {
        Task<RmFormQa2Hdr> SaveFormQa2Hdr(RmFormQa2Hdr rmFormQa2Hdr);

        Task<List<RmFormQa2Hdr>> GetFilteredRecordList(FilteredPagingDefinition<FormQa2SearchGridDTO> filterOptions);
        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormQa2SearchGridDTO> filterOptions);
        Task<List<string>> GetSectionByRMU(string rmu);

        Task<RmFormQa2Hdr> GetFormWithDetailsByNoAsync(int formNo);

        Task<int> CheckwithRef(FormQa2HeaderRequestDTO formHeader);

        Task<IEnumerable<RmRoadMaster>> GetRoadCodes();

        Task<IEnumerable<RmDdLookup>> GetSectionCode();

        Task<IEnumerable<RmDdLookup>> GetRMU();

        Task<IEnumerable<RmRoadMaster>> GetRoadCodesByRMU(string rmu);

        (int id, bool aleadyExists) CheckExistence(string rdCode, int month, int year);

        Task<int> GetMaxCount();

        Task<GridWrapper<object>> GetFormQa2HeaderGrid(DataTableAjaxPostModel searchData);
        Task<RmFormQa2Hdr> GetFormQa2WithDetailsAsync(int formNo);
    }
}
