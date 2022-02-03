using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormF4Repository : IRepositoryBase<RmFormF4InsHdr>
    {
        Task<GridWrapper<object>> GetFormF4GridHeader(DataTableAjaxPostModel searchData);
        Task<GridWrapper<object>> GetFormF4GridDetail(int headerId, DataTableAjaxPostModel searchData);
        Task<RmFormF4InsHdr> saveFormF4Hdr(RmFormF4InsHdr header, bool updateSubmitSts);
        Task<int> saveDetail(IList<RmFormF4InsDtl> detail);
        Task<int> DeleteFormF4Hdr(RmFormF4InsHdr header);
        Task<int> DeleteFormF4Dtl(List<RmFormF4InsDtl> dtl);
        Task<List<RmFormF4InsDtl>> GetDetailList(int id);
        Task<FORMF4Rpt> GetReportData(int headerid);
    }
}
