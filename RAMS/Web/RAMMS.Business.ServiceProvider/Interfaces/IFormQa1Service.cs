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

        Task<FormQa1HeaderDTO> FindQa1Details(int pkRefNo);

        Task<PagingResult<FormQa1EqVhDTO>> GetEquipmentFormQa1Grid(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id);

        Task<PagingResult<FormQa1MatDTO>> GetMaterialFormQa1Grid(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id);

        Task<PagingResult<FormQa1GenDTO>> GetGeneralFormQa1Grid(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id);

        Task<FormQa1GenDTO> GetGenDetails(int pkRefNo);

        Task<FormQa1EqVhDTO> GetEquipDetails(int pkRefNo);

        Task<FormQa1MatDTO> GetMatDetails(int pkRefNo);

        Task<FormQa1HeaderDTO> GetFormQA1(int pkRefNo);

        Task<FormQa1LabDTO> GetLabourDetails(int pkRefNo);

        int? SaveMaterial(FormQa1MatDTO formQa1Mat);

        Task<int?> DeleteMaterial(int id);

        int? SaveGeneral(FormQa1GenDTO formQa1Gen);

        Task<int?> DeleteGeneral(int id);

        Task<int> SaveFormQA1(FormQa1HeaderDTO formQa1Header, bool updateSubmit);

        Task<int> DeleteFormQA1(int id);

        int? SaveEquipment(FormQa1EqVhDTO formQa1EqVh);

        Task<int?> DeleteEquipment(int id);

        Task<int> SaveImage(List<FormQa1AttachmentDTO> image);

        Task<List<FormQa1AttachmentDTO>> GetImages(int tesPkRefNo, int row =0 );

        Task<int> DeActivateImage(int imageId);

        Task<FormQa1TesDTO> GetTes(int tesPkRefNo);

    }
}
