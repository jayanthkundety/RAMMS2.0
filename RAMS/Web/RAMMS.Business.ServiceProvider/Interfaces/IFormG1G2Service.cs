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
    public interface IFormG1G2Service
    {
        Task<FormG1DTO> FindByHeaderID(int headerId);
        Task<FormG1DTO> FindDetails(FormG1DTO frmC1C2, int createdBy);
        Task<FormG1DTO> Save(FormG1DTO frmC1C2, bool updateSubmit);
        Task<List<FormG1G2PhotoTypeDTO>> GetExitingPhotoType(int headerId);
        Task<RmFormGImages> AddImage(FormGImagesDTO imageDTO);
        Task<(IList<RmFormGImages>, int)> AddMultiImage(IList<FormGImagesDTO> imagesDTO);
        List<FormGImagesDTO> ImageList(int headerId);
        Task<int> DeleteImage(int headerId, int imgId);
        Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData);
        int Delete(int id);
        //List<FormG1G2Rpt> GetReportData(int headerid);
        Byte[] FormDownload(string formname, int id, string basepath, string filepath);
        //Task<IEnumerable<SelectListItem>> GetCVIds(AssetDDLRequestDTO request);
    }
}
