using RAMMS.Domain.Models;
using RAMMS.DTO;
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

        //int SaveFormWD(RmIwFormWd FormWD);
        //int? DeleteFormWDClause(int Id);


    }
}
