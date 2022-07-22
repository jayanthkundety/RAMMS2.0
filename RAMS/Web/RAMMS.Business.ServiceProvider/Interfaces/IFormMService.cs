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
   public interface IFormMService
    {
        Task<FormMDTO> FindByHeaderID(int headerId);
        Task<FormMDTO> FindDetails(FormMDTO frmM, int createdBy);
        Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData);
        Task<FormMDTO> Save(FormMDTO frmMs, bool updateSubmit);
        int Delete(int id);
        Byte[] FormDownload(string formname, int id, string basepath, string filepath);
    }
}
