using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormFSHeaderRepository : IRepositoryBase<RmFormFsInsHdr>
    {
        Task<long> GetFilteredRecordCount(FilteredPagingDefinition<FormFSHeaderRequestDTO> filterOptions);
        Task<List<FormFSHeaderRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormFSHeaderRequestDTO> filterOptions);
        FormFSRpt GetReportData(int headerid);
    }
}
