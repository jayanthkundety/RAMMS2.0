using RAMMS.DTO.JQueryModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Report;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormF4Service
    {
        Task<GridWrapper<object>> GetFormF4HeaderGrid(DataTableAjaxPostModel searchData);
        Task<GridWrapper<object>> GetFormF4DetailGrid(int headerId, DataTableAjaxPostModel searchData);
        Task<FormF4HeaderRequestDTO> FindDetails(FormF4HeaderRequestDTO headerDTO);
        Task<FormF4HeaderRequestDTO> SaveHeader(FormF4HeaderRequestDTO headerDto, bool updateSubmitSts);
        Task<T> SaveHeader<T>(T header, bool UpdateSubmitSts);
        Task<int> SaveDetail(FormF4HeaderRequestDTO header);
        Task<FormF4HeaderRequestDTO> FindHeaderById(int headerId);
        Task<int> DeleteFormF4Hdr(int id);
        Task<FORMF4Rpt> GetReportData(int headerid);
        Task<byte[]> FormDownload(string formname, int id, string filepath);
    }
}
