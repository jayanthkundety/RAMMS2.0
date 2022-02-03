using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormFSDetailRepository : IRepositoryBase<RmFormFsInsDtl>
    {
        Task<long> GetFilteredRecordCount(FilteredPagingDefinition<FormFSDetailRequestDTO> filterOptions);
        Task<List<FormFSDetailRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormFSDetailRequestDTO> filterOptions);
        int BulkInsert(int headerid, int userid, RmFormFsInsHdr hdr);
        Task<List<FormFSDetailRequestDTO>> GetRecordList(int headerId);
    }
}
