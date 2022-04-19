using Microsoft.AspNetCore.Mvc.Rendering;
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

        #region FormV1
        Task<List<RmFormV1Hdr>> GetFilteredRecordList(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions);

        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions);

        Task<List<RmFormV1Dtl>> GetFormV1WorkScheduleGridList(FilteredPagingDefinition<FormV1WorkScheduleGridDTO> filterOptions, int V1PkRefNo);

        Task<RmFormV1Hdr> FindFormV1ByID(int id);

        int? SaveFormV1WorkSchedule(RmFormV1Dtl FormV1Dtl);
        int? UpdateFormV1WorkSchedule(RmFormV1Dtl FormV1Dtl);

        int? DeleteFormV1(int id);
        int? DeleteFormV1WorkSchedule(int id);

        List<SelectListItem> FindRefNoFromS1(FormV1ResponseDTO FormV1);

        int LoadS1Data(int PKRefNo, int S1PKRefNo, string ActCode);

        int PullS1Data(int PKRefNo, int S1PKRefNo, string ActCode);


        #endregion
        #region FormV3
        Task<List<RmFormV3Hdr>> GetFilteredV3RecordList(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions);
        Task<int> GetFilteredV3RecordCount(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions);

        Task<RmFormV3Hdr> FindFormV3ByID(int id);

        Task<FormV3ResponseDTO> SaveFormV3(FormV3ResponseDTO Formv3);

        Task<int> UpdateFormV3(RmFormV3Hdr FormV3);
        #endregion

    }
}
