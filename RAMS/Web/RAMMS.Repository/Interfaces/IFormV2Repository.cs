using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormV2Repository : IRepositoryBase<RmFormV2Hdr>
    {
        int SaveFormV2Hdr(RmFormV2Hdr rmFormV2Hdr);
        RmFormV2Hdr GetRmFormV2Hdr(RmFormV2Hdr rmFormV2Hdr);

        Task<RmFormV2Hdr> DetailView(RmFormV2Hdr rmFormV2Hdr);

        Task<RmFormV2Hdr> GetFormWithDetailsByNoAsync(int formNo);

        Task<List<RmFormV2Hdr>> GetFilteredRecordList(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions);
        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions);
        Task<List<string>> GetSectionByRMU(string rmu);

        Task<IEnumerable<RmAssetDefectCode>> GetDefectCode(string assetGroup);
        Task<int> CheckwithRef(FormV2HeaderResponseDTO formXHeader);
      

        Task<IEnumerable<RmRoadMaster>> GetRoadCodes();

        Task<IEnumerable<RmDdLookup>> GetDivisions();

        Task<IEnumerable<RmDdLookup>> GetActivityMainTask();

        Task<IEnumerable<RmDdLookup>> GetActivitySubTask();

        Task<IEnumerable<RmDdLookup>> GetSectionCode();

        Task<IEnumerable<RmDdLookup>> GetLabourCode();

        Task<IEnumerable<RmDdLookup>> GetMaterialCode();

        Task<IEnumerable<RmDdLookup>> GetEquipmentCode();

        Task<IEnumerable<RmDdLookup>> GetRMU();

        Task<IEnumerable<RmDdLookup>> GetERTActivityCode();

        Task<bool> CheckHdrRefereceId(string id);

        Task<IEnumerable<RmRoadMaster>> GetRoadCodesByRMU(string rmu);

        Task<IEnumerable<RmFormXHdr>> GetFormXReferenceId(string rodeCode);

        Task<string> GetMaxIdLength();

        Task<IEnumerable<RmDivRmuSecMaster>> GetSectionCodesByRMU(string rmu);

        Task<IEnumerable<RmRoadMaster>> GetRoadCodeBySectionCode(string secCode);

        Task<string> CheckAlreadyExists(DateTime? date, string crewUnit, string day, string rmu, string secCode);

        Task<RmFormV2Hdr> FindSaveFormV2Hdr(RmFormV2Hdr formDHeader, bool updateSubmit);
    }
}
