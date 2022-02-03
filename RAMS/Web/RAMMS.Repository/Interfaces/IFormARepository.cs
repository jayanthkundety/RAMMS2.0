using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormARepository : IRepositoryBase<RmFormAHdr>
    {
        Task<int> SaveFormAAsync(RmFormAHdr formAHeaderBO);
        int SaveFormAHdr(RmFormAHdr rmFormAHdr);
        RmFormAHdr GetRmFormAHdr(RmFormAHdr rmFormAHdr);

        Task<bool> RemoveFormDetail(int detailsId);
        Task<RmFormAHdr> DetailView(RmFormAHdr rmFormAHdr);

        Task<RmFormAHdr> GetFormWithDetailsByNoAsync(int formNo);
        Task<RmFormAHdr> GetFAHRefIDById(int id);

        Task<List<RmFormAHdr>> GetFilteredRecordList(FilteredPagingDefinition<FormASearchGridDTO> filterOptions);
        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormASearchGridDTO> filterOptions);
        Task<List<string>> GetSectionByRMU(string rmu);
        Task<List<RmFormADtl>> GetDetailRecordList(FilteredPagingDefinition<FormADetailsRequestDTO> detailList);
        Task<int> GetDetailRecordCount(FilteredPagingDefinition<FormADetailsRequestDTO> filterOptions);
        Task<IEnumerable<RmAssetDefectCode>> GetDefectCode(string assetGroup);
        Task<int> CheckwithRef(FormAHeaderRequestDTO formAHeader);
        Task<int?> CreateDtl(RmFormADtl formADetails);
        Task<int?> CreateDtlV1(RmFormADtl domainModelFormA);
        Task<RmFormADtl> GetDetailByIdAsync(int detailId);
        void UpdateDetail(RmFormADtl request);
        Task<int> GetSrNo(int headerId);
        Task<int> GetLastInsertedHeader();
        Task<int> GetNodActiveRMUCount(string searchObj);
        Task<int> GetNodActiveSectionCount(string searchObj);
        Task<int> GetActiveFormARecord();
    }
}
