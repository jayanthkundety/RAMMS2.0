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
    public interface IFormR1R2Service
    {
        Task<FormR1DTO> FindByHeaderID(int headerId);
        Task<FormR1DTO> FindDetails(FormR1DTO frmR1R2, int createdBy);
        Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData);
        Task<FormR1DTO> Save(FormR1DTO frmR1R2, bool updateSubmit);
        Task<List<FormR1R2PhotoTypeDTO>> GetExitingPhotoType(int headerId);
        Task<RmFormRImages> AddImage(FormRImagesDTO imageDTO);
        Task<(IList<RmFormRImages>, int)> AddMultiImage(IList<FormRImagesDTO> imagesDTO);
        List<FormRImagesDTO> ImageList(int headerId);
        Task<int> DeleteImage(int headerId, int imgId);
        int Delete(int id);
        Byte[] FormDownload(string formname, int id, string basepath, string filepath);
    }
}
