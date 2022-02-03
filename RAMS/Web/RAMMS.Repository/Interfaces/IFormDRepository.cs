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
    public interface IFormDRepository : IRepositoryBase<RmFormDHdr>
    {
        int SaveFormDHdr(RmFormDHdr rmFormDHdr);
        RmFormDHdr GetRmFormDHdr(RmFormDHdr rmFormDHdr);

        Task<RmFormDHdr> DetailView(RmFormDHdr rmFormDHdr);

        Task<RmFormDHdr> GetFormWithDetailsByNoAsync(int formNo);

        Task<List<RmFormDHdr>> GetFilteredRecordList(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions);
        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions);
        Task<List<string>> GetSectionByRMU(string rmu);

        Task<IEnumerable<RmAssetDefectCode>> GetDefectCode(string assetGroup);
        Task<int> CheckwithRef(FormDHeaderRequestDTO formXHeader);
        void SaveWarImage(IEnumerable<RmWarImageDtl> rmWarImage);
        void SaveAccUccImage(IEnumerable<RmAccUcuImageDtl> rmAccUcuImage);
        void UpdateWar(RmWarImageDtl rmWarImage);
        void UpdateAccUcu(RmAccUcuImageDtl rmAccUcuImage);
        Task<RmWarImageDtl> GetWarImageByIdAsync(int warId);
        Task<RmAccUcuImageDtl> GetAccUccImageById(int warId);
        Task<List<RmWarImageDtl>> GetWarImagelist(int formXId);
        Task<List<RmAccUcuImageDtl>> GetAccUccImagelist(int formXId);
        Task<int> GetWARId(int headerId, string type);

        Task<int> GetUCUId(int headerId, string type);

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

        Task<bool> CheckDetailsRefereceId(string id);

        Task<IEnumerable<RmRoadMaster>> GetRoadCodesByRMU(string rmu);

        Task<IEnumerable<RmFormXHdr>> GetFormXReferenceId(string rodeCode);

        Task<string> GetMaxIdLength();

        Task<IEnumerable<RmDivRmuSecMaster>> GetSectionCodesByRMU(string rmu);

        Task<IEnumerable<RmRoadMaster>> GetRoadCodeBySectionCode(string secCode);

        Task<int?> GetLabourSRNO(int id);
        Task<int?> GetEqpSRNO(int id);
        Task<int?> GetMatSrno(int id);
        Task<int?> GetDtlSrno(int? id);

        public Task<string> CheckAlreadyExists(int? weekNo, int? year, string crewUnit, string day, string rmu, string secCode);

        Task<RmFormDHdr> FindSaveFormDHdr(RmFormDHdr formDHeader, bool updateSubmit);
    }
}
