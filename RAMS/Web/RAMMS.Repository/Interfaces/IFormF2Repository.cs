using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormF2Repository : IRepositoryBase<RmFormF2GrInsHdr>
    {
        Task<List<FormF2HeaderRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormF2SearchGridDTO> filterOptions);
        Task<long> GetFilteredRecordCount(FilteredPagingDefinition<FormF2SearchGridDTO> filterOptions);
        RmFormF2GrInsHdr IsExists(string st);
        Task<long> BulkDetailInsert(int headerid, int createdBy);
        Task<decimal?> TotalLength(string roadcode);
        FORMF2Rpt GetReportData(int headerid);
    }
}
