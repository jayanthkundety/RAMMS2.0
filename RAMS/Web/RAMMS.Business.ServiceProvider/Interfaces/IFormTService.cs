using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormTService
    {
        //Task<FormTResponseDTO> FindTByW1ID(int id);
        //Task<FormTResponseDTO> FindFormTByID(int id);
        //Task<IEnumerable<FormTDtlResponseDTO>> FindFormTDtlByID(int id);

        Task<PagingResult<FormTHeaderRequestDTO>> GetHeaderList(FilteredPagingDefinition<FormTSearchGridDTO> filterOptions);

        Task<PagingResult<FormTDtlGridDTO>> GetDetailList(FilteredPagingDefinition<FormTDtlResponseDTO> filterOptions);

        Task<FormTResponseDTO> GetHeaderById(int id);
        Task<FormTDtlResponseDTO> GetFormTDtlById(int id);
        Task<FormTResponseDTO> SaveFormT(FormTResponseDTO FormT);
        Task<int> Update(FormTResponseDTO FormW1);
        int? DeleteFormT(int id);
        int? DeleteFormTDtl(int Id);
        int? SaveFormTDtl(FormTDtlResponseDTO FormTDtl);
        int? UpdateFormTDtl(FormTDtlResponseDTO FormTDtl);
        Task<int> DeActivateFormT(int formNo);
        //Task<byte[]> FormDownload(string formname, int id, string filepath);

    }
}
