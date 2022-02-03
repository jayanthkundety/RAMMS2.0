using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO;
using RAMMS.DTO.Report;
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
    public interface IFormHService
    {

        Task<FormHRequestDTO> SaveFormH(FormHRequestDTO formHDTO);
        Task<int> DeleteFormH(int formHId);
        Task<PagingResult<FormHResponseDTO>> GetFilteredFormHGrid(FilteredPagingDefinition<FormHSearchDTO> filterOptions);
        Task<FormHRequestDTO> GetByID(int formHId);
        Task<int> SaveFormHImage(List<FormHImageRequestDTO> formHImageList);
        Task<int> DeleteFormHImage(int imageId);
        Task<List<FormHImageResponseDTO>> GetFormHImageList(int formHeaderId);
        Task<int> GetImageSrNo(int headerId);
        Task<int> GetLastInsertedHeader();
        List<SelectListItem> GetReferenceNoByFormType(RequestFormReference lookUp);
        Task<FormHRequestDTO> GetByReferenceID(string id);
        FORMHRpt GetReportData(int headerId, int pageIndex, int pageCount);
        Byte[] FormDownload(string formName, int id, string filePath);
        (int id, bool alreadyExists) CheckAlreadyExists(FormType form, string roadCode, DateTime inspectionDate, string assetGroup, int locationFrom, int locationTo, int sourceRefNo);

    }
}
