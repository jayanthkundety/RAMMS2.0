using RAMMS.DTO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormQa1Service
    {
        Task<PagingResult<FormQa1HeaderDTO>> GetFilteredFormQa1Grid(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions);

        Task<FormQa1HeaderDTO> FindQa1Details(FormQa1HeaderDTO header);

        Task<FormQa1HeaderDTO> FindAndSaveFormQA1Hdr(FormQa1HeaderDTO header, bool updateSubmit);

        Task<FormQa1LabDTO> SaveLabour(FormQa1LabDTO labDTO);

        Task<FormQa1HeaderDTO> FindQa1Details(int pkRefNo);

        Task<PagingResult<FormQa1EqVhDTO>> GetEquipmentFormQa1Grid(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id);

        Task<PagingResult<FormQa1MatDTO>> GetMaterialFormQa1Grid(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id);

        Task<PagingResult<FormQa1GenDTO>> GetGeneralFormQa1Grid(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id);

        Task<FormQa1GenDTO> GetGenDetails(int pkRefNo);

        Task<FormQa1EqVhDTO> GetEquipDetails(int pkRefNo);

        Task<FormQa1MatDTO> GetMatDetails(int pkRefNo);

        Task<FormQa1HeaderDTO> GetFormQA1(int pkRefNo);

        Task<FormQa1LabDTO> GetLabourDetails(int pkRefNo);
    }
}
