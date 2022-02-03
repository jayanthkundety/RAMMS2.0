using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormF5Repository : IRepositoryBase<RmFormF5InsHdr>
    {
        Task<GridWrapper<object>> GetFormF5GridHeader(DataTableAjaxPostModel searchData);
        Task<GridWrapper<object>> GetFormF5GridDetail(int headerId, DataTableAjaxPostModel searchData);
        Task<RmFormF5InsHdr> saveFormF5Hdr(RmFormF5InsHdr header, bool updateSubmitSts);
        Task<int> saveDetail(IList<RmFormF5InsDtl> detail);
        Task<int> DeleteFormF5Hdr(RmFormF5InsHdr header);
        Task<int> DeleteFormF5Dtl(List<RmFormF5InsDtl> dtl);
        Task<List<RmFormF5InsDtl>> GetDetailList(int id);
        Task<FORMF5Rpt> GetReportData(int headerid);
    }
}
