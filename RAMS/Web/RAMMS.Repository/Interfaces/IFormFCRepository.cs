using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormFCRepository : IRepositoryBase<RmFormFcInsHdr>
    {
        Task<RmFormFcInsHdr> FindDetails(RmFormFcInsHdr frmFC);
        Task<RmFormFcInsHdr> FindByHeaderID(int headerId);
        Task<RmFormFcInsHdr> Save(RmFormFcInsHdr frmFC, bool updateSubmit);
        Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData);
        int DeleteHeader(RmFormFcInsHdr frmFC);
        FormFCRpt GetReportData(int headerid);
    }
}
