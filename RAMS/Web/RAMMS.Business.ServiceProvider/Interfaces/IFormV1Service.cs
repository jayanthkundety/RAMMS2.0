using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormV1Service
    {
        #region Formv1
        Task<PagingResult<FormV1ResponseDTO>> GetFilteredFormV1Grid(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions);
        Task<PagingResult<FormV1WorkScheduleGridDTO>> GetFormV1WorkScheduleGridList(FilteredPagingDefinition<FormV1WorkScheduleGridDTO> filterOptions, int V1PkRefNo);
      
        Task<FormV1ResponseDTO> SaveFormV1(FormV1ResponseDTO FormV1);
        Task<int> Update(FormV1ResponseDTO FormV1);

        Task<FormV1ResponseDTO> FindFormV1ByID(int id);
        //Task<int> DeActivateFormWD(int formNo);

        int? SaveFormV1WorkSchedule(FormV1DtlResponseDTO FormV1Dtl);

        int? UpdateFormV1WorkSchedule(FormV1DtlResponseDTO FormV1Dtl);

        int? DeleteFormV1(int id);
        int? DeleteFormV1WorkSchedule(int id);



        List<SelectListItem> FindRefNoFromS1(FormV1ResponseDTO FormV1);

        int LoadS1Data(int PKRefNo, int S1PKRefNo, string ActCode);

        int PullS1Data(int PKRefNo, int S1PKRefNo, string ActCode);
        #endregion

        #region FormV3

        Task<PagingResult<FormV3ResponseDTO>> GetFilteredFormV3Grid(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions);

        Task<PagingResult<FormV3DtlGridDTO>> GetFormV3DtlGridList(FilteredPagingDefinition<FormV3DtlGridDTO> filterOptions, int V3PkRefNo);

        Task<FormV3ResponseDTO> FindFormV3ByID(int id);

        Task<FormV3ResponseDTO> SaveFormV3(FormV3ResponseDTO FormV3);
        Task<int> UpdateV3(FormV3ResponseDTO FormV3);
        Task<int> UpdateFormV3Dtl(FormV3DtlGridDTO FormV3Dtl);

        int? DeleteFormV3(int id);

        int? DeleteFormV3Dtl(int id);


        #endregion

        #region FormV4

        Task<PagingResult<FormV4ResponseDTO>> GetFilteredFormV4Grid(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions);

       
        Task<FormV4ResponseDTO> FindFormV4ByID(int id);

        Task<FormV4ResponseDTO> SaveFormV4(FormV4ResponseDTO FormV4);
        Task<int> UpdateV4(FormV4ResponseDTO FormV4);
        int? DeleteFormV4(int id);

        #endregion

        #region Formv5
        //Task<PagingResult<FormV5ResponseDTO>> GetFilteredFormV5Grid(FilteredPagingDefinition<FormV5SearchGridDTO> filterOptions);
        //Task<PagingResult<FormV5WorkScheduleGridDTO>> GetFormV5WorkScheduleGridList(FilteredPagingDefinition<FormV5WorkScheduleGridDTO> filterOptions, int V5PkRefNo);

        //Task<FormV5ResponseDTO> SaveFormV5(FormV5ResponseDTO FormV5);
        //Task<int> Update(FormV5ResponseDTO FormV5);

        //Task<FormV5ResponseDTO> FindFormV5ByID(int id);
        ////Task<int> DeActivateFormWD(int formNo);

        //int? SaveFormV5WorkSchedule(FormV5DtlResponseDTO FormV5Dtl);

        //int? UpdateFormV5WorkSchedule(FormV5DtlResponseDTO FormV5Dtl);

        //int? DeleteFormV5(int id);
        //int? DeleteFormV5WorkSchedule(int id);


 
        #endregion

    }
}
