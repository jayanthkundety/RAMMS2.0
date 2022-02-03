using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.SearchBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormHRepository : IRepositoryBase<RmFormHHdr>
    {
        Task<List<RmFormHHdr>> GetFilteredRecordList(FilteredPagingDefinition<FormHSearchDTO> filterOptions);
        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormHSearchDTO> filterOptions);
        void SaveImageList(IEnumerable<RmFormhImageDtl> imageDtl);
        Task<RmFormhImageDtl> GetFormHImageByIdAsync(int imageId);
        void UpdateImage(RmFormhImageDtl imageDtl);
        Task<List<RmFormhImageDtl>> GetFormHImageListAsync(int headerId);
        Task<int> GetSrNo(int headerId);
        Task<int> GetNodActiveRMUCount(string searchObj);
        Task<int> GetNodActiveSectionCount(string searchObj);
        Task<int> GetActiveFormHRecord();
    }
}
