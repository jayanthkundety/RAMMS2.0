using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormFDRepository:IRepositoryBase<RmFormFdInsHdr>
    {
        Task<RmFormFdInsHdr> FindDetails(RmFormFdInsHdr frmFd);
        Task<RmFormFdInsHdr> Save(RmFormFdInsHdr frmFd, bool updateSubmit);
        Task<RmFormFdInsHdr> FindByHeaderID(int headerId);
        Task<GridWrapper<object>> GetFormFDGridHeader(DataTableAjaxPostModel searchData);
        int DeleteHeader(RmFormFdInsHdr frmFD);
        FormFDRpt GetReportData(int headerid);
    }
}
