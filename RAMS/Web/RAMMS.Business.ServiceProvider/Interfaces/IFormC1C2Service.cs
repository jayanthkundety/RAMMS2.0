using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO.RequestBO;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormC1C2Service
    {
        Task<FormC1C2DTO> FindByHeaderID(int headerId);
        Task<FormC1C2DTO> FindDetails(FormC1C2DTO frmC1C2, int createdBy);
        Task<FormC1C2DTO> Save(FormC1C2DTO frmC1C2, bool updateSubmit);
        Task<List<FormC1C2PhotoTypeDTO>> GetExitingPhotoType(int headerId);
        Task<RmFormCvInsImage> AddImage(FormC1C2ImageDTO imageDTO);
        Task<(IList<RmFormCvInsImage>, int)> AddMultiImage(IList<FormC1C2ImageDTO> imagesDTO);
        List<FormC1C2ImageDTO> ImageList(int headerId);
        Task<int> DeleteImage(int headerId, int imgId);
        Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData);
        int Delete(int id);
        List<FormC1C2Rpt> GetReportData(int headerid);
        Byte[] FormDownload(string formname, int id, string basepath, string filepath);
        Task<IEnumerable<SelectListItem>> GetCVIds(AssetDDLRequestDTO request);
    }
}
