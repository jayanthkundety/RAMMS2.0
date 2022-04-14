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
    }
}
