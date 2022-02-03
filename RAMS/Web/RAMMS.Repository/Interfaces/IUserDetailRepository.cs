using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Repository.Interfaces
{
    public interface IUserDetailRepository : IRepositoryBase<RmUsers>
    {
        Task<long> GetFilteredRecordCount(FilteredPagingDefinition<UserDetailRequestDTO> filterOptions);
        Task<List<UserDetailRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<UserDetailRequestDTO> filterOptions);
    }
}
