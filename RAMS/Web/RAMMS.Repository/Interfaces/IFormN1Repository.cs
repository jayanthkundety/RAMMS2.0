using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormN1Repository : IRepositoryBase<RmFormN1Hdr>
    {
        int SaveFormN1Hdr(RmFormN1Hdr rmFormN1Hdr);
        RmFormN1Hdr GetRmFormN1Hdr(RmFormN1Hdr rmFormN1Hdr);


        Task<List<RmFormN1Hdr>> GetFilteredRecordList(FilteredPagingDefinition<FormN1SearchGridDTO> filterOptions);
        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormN1SearchGridDTO> filterOptions);
        Task<List<string>> GetSectionByRMU(string rmu);

        Task<RmFormN1Hdr> GetFormWithDetailsByNoAsync(int formNo);


        Task<int> CheckwithRef(FormN1HeaderRequestDTO formXHeader);
     
        Task<IEnumerable<RmRoadMaster>> GetRoadCodes();

        Task<IEnumerable<RmDdLookup>> GetDivisions();


        Task<IEnumerable<RmDdLookup>> GetSectionCode();

        Task<IEnumerable<RmDdLookup>> GetRMU();

        Task<bool> CheckHdrRefereceId(string id);
        Task<bool> CheckHdrRefereceNo(string id);
        Task<IEnumerable<RmRoadMaster>> GetRoadCodesByRMU(string rmu);

        Task<IEnumerable<RmFormS1Dtl>> GetFormS1ReferenceId(string rodeCode);

        Task<IEnumerable<RmFormQa2Dtl>> GetFormQA2ReferenceId(string rodeCode);

        (int id, bool aleadyExists) CheckExistence(string rdCode, int month, int year);

        Task<RmFormS1Hdr> GetFormS1Data(int id);

        Task<FormQa2HeaderResponseDTO> GetFormQa2Data(int id);

        Task<int> GetMaxCount();
        Task<int> GetActiveCount();
        Task<int> GetActiveRmuBasedCount(string rmu);
        Task<int> GetActiveRdCodeCount(List<string> rdCode);
    }
}
