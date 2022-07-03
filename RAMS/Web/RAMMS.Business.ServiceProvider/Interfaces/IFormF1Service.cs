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
    public interface IFormF1Service
    {
        //Task<FormF1ResponseDTO> FindF1ByW1ID(int id);
        //Task<FormF1ResponseDTO> FindFormF1ByID(int id);
        //Task<IEnumerable<FormF1DtlResponseDTO>> FindFormF1DtlByID(int id);

        Task<PagingResult<FormF1HeaderRequestDTO>> GetHeaderList(FilteredPagingDefinition<FormF1SearchGridDTO> filterOptions);

        Task<PagingResult<FormF1DtlGridDTO>> GetDetailList(FilteredPagingDefinition<FormF1DtlResponseDTO> filterOptions);

        IEnumerable<CSelectListItem> GetAssetDetails(FormF1ResponseDTO FormF1);
        Task<FormF1ResponseDTO> GetHeaderById(int id);
        Task<FormF1ResponseDTO> SaveFormF1(FormF1ResponseDTO FormF1);
        Task<int> Update(FormF1ResponseDTO FormW1);
        int? DeleteFormF1(int id);
        int? DeleteFormF1Dtl(int Id);
        int? SaveFormF1Dtl(FormF1DtlResponseDTO FormF1Dtl);
        int? UpdateFormF1Dtl(FormF1DtlResponseDTO FormF1Dtl);
        Task<int> DeActivateFormF1(int formNo);
        Task<byte[]> FormDownload(string formname, int id, string filepath);

    }
}
