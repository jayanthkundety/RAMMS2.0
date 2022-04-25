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

        Task<int> UpdateFormV3Dtl(RmFormV3Dtl FormV3Dtl);

        int? DeleteFormV3(int id);
        int? DeleteFormV3Dtl(int id);
        #endregion

        #region FormV4
        Task<List<RmFormV4Hdr>> GetFilteredV4RecordList(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions);
        Task<int> GetFilteredV4RecordCount(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions);

        Task<RmFormV4Hdr> FindFormV4ByID(int id);

        Task<FormV4ResponseDTO> SaveFormV4(FormV4ResponseDTO Formv3);

        Task<int> UpdateFormV4(RmFormV4Hdr FormV4);

        int? DeleteFormV4(int id);

        #endregion

        #region FormV5
        //Task<List<RmFormV1Hdr>> GetFilteredV5RecordList(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions);

        //Task<int> GetFilteredV5RecordCount(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions);

        //Task<List<RmFormV5Dtl>> GetFormV5WorkScheduleGridList(FilteredPagingDefinition<FormV5WorkScheduleGridDTO> filterOptions, int V5PkRefNo);

        //Task<RmFormV5Hdr> FindFormV5ByID(int id);

        //int? SaveFormV5WorkSchedule(RmFormV5Dtl FormV5Dtl);
        //int? UpdateFormV5WorkSchedule(RmFormV5Dtl FormV5Dtl);

        //int? DeleteFormV5(int id);
        //int? DeleteFormV5WorkSchedule(int id);

    
        #endregion

    }
}
