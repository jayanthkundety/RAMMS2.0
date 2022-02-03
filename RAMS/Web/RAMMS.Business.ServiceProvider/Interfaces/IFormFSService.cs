using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormFSService
    {
        long LastHeaderInsertedNo();
        Task<FormFSHeaderRequestDTO> GetHeaderById(int id);
        Task<int> SaveHeader(FormFSHeaderRequestDTO model);
        Task<int> FindDetail(FormFSHeaderRequestDTO model);
        Task<bool> RemoveHeader(int id);
        Task<PagingResult<FormFSHeaderRequestDTO>> GetHeaderList(FilteredPagingDefinition<FormFSHeaderRequestDTO> filterOptions);
        long LastDetailInsertedNo();
        Task<FormFSDetailRequestDTO> GetDetailById(int id);
        Task<int> SaveDetail(FormFSDetailRequestDTO model);
        Task<bool> RemoveDetail(int id);
        Task<PagingResult<FormFSDetailRequestDTO>> GetDetailList(FilteredPagingDefinition<FormFSDetailRequestDTO> filterOptions);
        FormFSRpt GetReportData(int headerid);
        Byte[] FormDownload(string formname, int id, string basepath, string filepath);
        Task<List<FormFSDetailRequestDTO>> GetRecordList(int headerId);
    }
}
