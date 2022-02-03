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
    public interface IFormN2Repository : IRepositoryBase<RmFormN2Hdr>
    {
        int SaveFormN2Hdr(RmFormN2Hdr rmFormN2Hdr);
        RmFormN2Hdr GetRmFormN2Hdr(RmFormN2Hdr rmFormN2Hdr);


        Task<List<RmFormN2Hdr>> GetFilteredRecordList(FilteredPagingDefinition<FormN2SearchGridDTO> filterOptions);
        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormN2SearchGridDTO> filterOptions);
        Task<List<string>> GetSectionByRMU(string rmu);

        Task<RmFormN2Hdr> GetFormWithDetailsByNoAsync(int formNo);


        Task<int> CheckWithRef(FormN2HeaderRequestDTO formXHeader);

        Task<IEnumerable<RmRoadMaster>> GetRoadCodes();

        Task<IEnumerable<RmDdLookup>> GetDivisions();


        Task<IEnumerable<RmDdLookup>> GetSectionCode();

        Task<IEnumerable<RmDdLookup>> GetRMU();

        Task<bool> CheckHdrRefereceId(string id);

        Task<IEnumerable<RmRoadMaster>> GetRoadCodesByRMU(string rmu);

        Task<IEnumerable<RmFormN1Hdr>> GetFormN1ReferenceId(bool active);

        (int id, bool aleadyExists) CheckExistence(string rdCode, int month, int year);

        Task<int> GetMaxCount();
        Task<int> GetActiveCount();
        Task<int> GetActiveRmuBasedCount(string rmu);
        Task<int> GetActiveRdCodeCount(List<string> rdCode);
    }
}
