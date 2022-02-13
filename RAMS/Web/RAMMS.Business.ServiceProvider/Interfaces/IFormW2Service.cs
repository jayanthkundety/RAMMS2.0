using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Common;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormW2Service
    {
        Task<int> Save(FormW2ResponseDTO formN1HeaderBO);

        Task<FormW2ResponseDTO> FindW2ByID(int id);

        int Delete(int id);

        Task<int> Update(FormW2ResponseDTO formW2DTO);

        Task<int> SaveImage(List<FormW2ImageResponseDTO> image);

        Task<List<FormW2ImageResponseDTO>> GetImageList(int formW2Id);

        Task<int> UpdateFormW2Signature(FormW2ResponseDTO formW2DTO);

        Task<int> LastInsertedIMAGENO(int hederId, string type);

        Task<int> DeActivateImage(int warId);

        Task<List<FormW1ResponseDTO>> GetFormW1List();
        Task<FormW1ResponseDTO> GetFormW1ById(int formW1Id);

        Task<IEnumerable<CSelectListItem>> GetFormW1DDL();

        Task<FormW1ResponseDTO> GetFormW1ByRoadCode(string roadCode);

        Task<PagingResult<FormIWResponseDTO>> GetFilteredFormIWGrid(FilteredPagingDefinition<FormIWSearchGridDTO> filterOptions);

        Task<int> SaveFCEM(FormW2FCEMResponseDTO formW2BO);

        Task<int> UpdateFCEM(FormW2FCEMResponseDTO formW2Bo);

        Task<FormW2FCEMResponseDTO> FindFCEM2ByW2ID(int Id);

        Task<FormW2FCEMResponseDTO> FindFCEM2ByID(int Id);
    }
}
