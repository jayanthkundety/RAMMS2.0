using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Common;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.SearchBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormXService
    {
        Task<int> SaveFormXAsync(FormXHeaderRequestDTO formAHeaderBO);
        Task<FormXHeaderResponseDTO> SaveHeaderwithResponse(FormXHeaderRequestDTO headerReq);

       Task<int> UpdateFormXAsync(FormXHeaderRequestDTO fornmXDtlDTO);
        Task<int> DeActivateFormXAsync(int formNo);
        Task<PagingResult<FormXHeaderResponseDTO>> GetFilteredFormXGrid(FilteredPagingDefinition<FormXSearchDTO> filterOptions);
        Task<FormXHeaderRequestDTO> GetFormXWithDetailsByNoAsync(int formNo);
        Task<int> SaveFormXWarImage(List<WarImageDtlRequestDTO> warImage);
        Task<int> SaveFormXAccUccDtl(List<AccUccImageDtlRequestDTO> accUccImage);
        Task<int> DeActivateWarImage(int warId);
        Task<int> DeActivateAccUCc(int accUccId);
        Task<List<WarImageDtlResponseDTO>> GetWarImageList(int formXId);
        Task<List<AccUccImageDtlResponseDTO>> GetAccUccImageList(int formXId);
        Task<AccUccImageDtlResponseDTO> GetAccUccImage(int formXId);
        Task<WarImageDtlResponseDTO> GetWarDocById(int formXId);
        Task<int> LastInsertedWARSRNO(int hederId, string type);
        Task<int> LastInsertedUCUSRNO(int hederId, string type);
        (int id, bool isExists) CheckExistence(string rdCode, int month, int year, DateTime rpDate);
        Task<string> GetSectionByRoadCodeAndRMU(string rdCode, string rmu);
        Task<List<RmWarImageDtl>> GetFilteredWARImagesFormX(int primaryNo);
        Task<int> GetWARTypeCodeCount(string type , int id);
    }
}
