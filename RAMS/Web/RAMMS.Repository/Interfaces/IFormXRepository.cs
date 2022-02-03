using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.SearchBO;
using RAMMS.DTO.Wrappers;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormXRepository : IRepositoryBase<RmFormXHdr>
    {
        int SaveFormXHdr(RmFormXHdr rmFormXHdr);
        RmFormXHdr GetRmFormXHdr(RmFormXHdr rmFormXHdr);

        Task<RmFormXHdr> DetailView(RmFormXHdr rmFormXHdr);

        Task<RmFormXHdr> GetFormWithDetailsByNoAsync(int formNo);

        Task<List<RmFormXHdr>> GetFilteredRecordList(FilteredPagingDefinition<FormXSearchDTO> filterOptions);
        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormXSearchDTO> filterOptions);
        Task<List<string>> GetSectionByRMU(string rmu);

        Task<IEnumerable<RmAssetDefectCode>> GetDefectCode(string assetGroup);
        Task<int> CheckwithRef(FormXHeaderRequestDTO formXHeader);
        void SaveWarImage(IEnumerable<RmWarImageDtl> rmWarImage);
        void SaveAccUccImage(IEnumerable<RmAccUcuImageDtl> rmAccUcuImage);
        void UpdateWar(RmWarImageDtl rmWarImage);
        void UpdateWarList(IEnumerable<RmWarImageDtl> rmWarImage);
        void UpdateAccUcu(RmAccUcuImageDtl rmAccUcuImage);
        Task<RmWarImageDtl> GetWarImageByIdAsync(int warId);
        Task<List<RmWarImageDtl>> GetWarImageListAsync(RmWarImageDtl warImageDtl);
        Task<List<RmAccUcuImageDtl>> GetAccUcuImageListAsync(RmAccUcuImageDtl accUcuImageDtl);
        Task<RmAccUcuImageDtl> GetAccUccImageById(int warId);
        Task<RmWarImageDtl> GetWarDocById(int accUccId);
        Task<List<RmWarImageDtl>> GetWarImagelist(int formXId);
        Task<List<RmAccUcuImageDtl>> GetAccUccImagelist(int formXId);
        Task<int> GetWARId(int headerId, string type);

        Task<int> GetUCUId(int headerId, string type);

        Task<List<RmWarImageDtl>> GetFilteredWARImagesFormX(int primaryNo);

        Task<int> GetWARTypeCodeCount(string type, int id);
    }
}
