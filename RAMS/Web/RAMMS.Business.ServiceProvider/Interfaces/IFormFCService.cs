using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormFCService
    {
        Task<FormFCDTO> FindByHeaderID(int headerId);
        Task<FormFCDTO> FindDetails(FormFCDTO frmFC, int createdBy);
        Task<FormFCDTO> Save(FormFCDTO frmFC, bool updateSubmit);
        Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData);
        int Delete(int id);
        FormFCRpt GetReportData(int headerid);
        Byte[] FormDownload(string formname, int id, string filepath);

        Task<bool> AssetsCheck(string roadCode);
    }
}
