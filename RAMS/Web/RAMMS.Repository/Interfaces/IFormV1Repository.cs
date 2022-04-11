using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormV1Repository : IRepositoryBase<RmFormV1Hdr>
    {

        Task<List<RmFormV1Hdr>> GetFilteredRecordList(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions);

        Task<List<RmFormV1Dtl>> GetFormV1WorkScheduleGridList(FilteredPagingDefinition<FormV1WorkScheduleGridDTO> filterOptions, int V1PkRefNo);

        Task<RmFormV1Hdr> FindFormV1ByID(int id);

        int? SaveFormV1WorkSchedule(RmFormV1Dtl FormV1Dtl);
        int? UpdateFormV1WorkSchedule(RmFormV1Dtl FormV1Dtl);

        int? DeleteFormV1WorkSchedule(int id);

        //int SaveFormWD(RmIwFormWd FormWD);
        //int? DeleteFormWDClause(int Id);


    }
}
