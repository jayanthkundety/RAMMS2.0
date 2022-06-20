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
    public interface IFormF3Service
    {
        //Task<FormF3ResponseDTO> FindF3ByW1ID(int id);
        //Task<FormF3ResponseDTO> FindFormF3ByID(int id);
        //Task<IEnumerable<FormF3DtlResponseDTO>> FindFormF3DtlByID(int id);

        Task<PagingResult<FormF2HeaderRequestDTO>> GetHeaderList(FilteredPagingDefinition<FormF2SearchGridDTO> filterOptions);

        Task<PagingResult<FormF3DtlGridDTO>> GetDetailList(FilteredPagingDefinition<FormF3DtlResponseDTO> filterOptions);

        IEnumerable<CSelectListItem> GetAssetDetails(FormF3ResponseDTO FormF3);
        Task<FormF3ResponseDTO> GetHeaderById(int id);
        Task<FormF3ResponseDTO> SaveFormF3(FormF3ResponseDTO FormF3);
        Task<int> Update(FormF3ResponseDTO FormW1);
        int? DeleteFormF3(int id);
        int? DeleteFormF3Dtl(int Id);
        int? SaveFormF3Dtl(FormF3DtlResponseDTO FormF3Dtl);
        int? UpdateFormF3Dtl(FormF3DtlResponseDTO FormF3Dtl);
        Task<int> DeActivateFormF3(int formNo);
      //  Byte[] FormDownload(string formname, int id, string basepath, string filepath);

    }
}
