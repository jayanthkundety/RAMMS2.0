using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.SearchBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormJRepository : IRepositoryBase<RmFormJHdr>
    {
        Task<int> SaveFormAAsync(RmFormJHdr formAHeaderBO);
        int SaveFormAHdr(RmFormJHdr rmFormJHdr);
        RmFormJHdr GetRmFormJHdr(RmFormJHdr rmFormJHdr);

        Task<bool> RemoveFormDetail(int detailsId);
        Task<RmFormJHdr> DetailView(RmFormJHdr rmFormJHdr);

        Task<RmFormJHdr> GetFormWithDetailsByNoAsync(int formNo);
        Task<RmFormJHdr> GetFAHRefIDById(int id);

        Task<List<RmFormJHdr>> GetFilteredRecordList(FilteredPagingDefinition<FormJSearchGridDTO> filterOptions);
        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormJSearchGridDTO> filterOptions);
        Task<List<string>> GetSectionByRMU(string rmu);
        Task<List<RmFormJDtl>> GetDetailRecordList(FilteredPagingDefinition<FormJDetailsRequestDTO> detailList);
        Task<int> GetDetailRecordCount(FilteredPagingDefinition<FormJDetailsRequestDTO> filterOptions);
        Task<IEnumerable<RmAssetDefectCode>> GetDefectCode(string assetGroup);
        Task<int> CheckWithRef(FormJHeaderRequestDTO formAHeader);
        Task<int?> CreateDtl(RmFormJDtl formADetails);
        Task<int?> CreateDtlV1(RmFormJDtl domainModelFormA);
        Task<RmFormJDtl> GetDetailByIdAsync(int detailId);
        void UpdateDetail(RmFormJDtl request);
        Task<int> GetSrNo(int headerId);
        Task<int> GetNodActiveRMUCount(string searchobj);
        Task<int> GetActiveSectionCount(string searchobj);
        Task<int> GetActiveFormJRecord();
        Task<GridWrapper<object>> GetFormJHeaderGrid(DataTableAjaxPostModel searchData);
        Task<List<RmFormJDtl>> GetAllDtlById(int headerId);
    }
}