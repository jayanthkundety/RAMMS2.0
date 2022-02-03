using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormFDService
    {
        Task<FormFDDTO> FindDetails(FormFDDTO frmFd, int createdBy);
        Task<FormFDDTO> Save(FormFDDTO frmFD, bool updateSubmit);
        Task<FormFDDTO> FindByHeaderID(int headerId);
        Task<GridWrapper<object>> GetFormFDHeaderGrid(DataTableAjaxPostModel searchData);
        int Delete(int id);
        FormFDRpt GetReportData(int headerid);
        byte[] FormDownload(string formname, int id, string filepath);
        Task<bool> AssetsCheck(string roadCode);
    }
}
