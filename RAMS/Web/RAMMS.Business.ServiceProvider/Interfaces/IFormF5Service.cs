using RAMMS.DTO.JQueryModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RAMMS.DTO.RequestBO;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormF5Service
    {
        Task<GridWrapper<object>> GetFormF5HeaderGrid(DataTableAjaxPostModel searchData);
        Task<GridWrapper<object>> GetFormF5DetailGrid(int headerId, DataTableAjaxPostModel searchData);
        Task<FormF5HeaderRequestDTO> FindDetails(FormF5HeaderRequestDTO headerDTO);
        Task<FormF5HeaderRequestDTO> SaveHeader(FormF5HeaderRequestDTO headerDto, bool updateSubmitSts);
        Task<T> SaveHeader<T>(T header, bool UpdateSubmitSts);
        Task<int> SaveDetail(FormF5HeaderRequestDTO header);
        Task<FormF5HeaderRequestDTO> FindHeaderById(int headerId);
        Task<int> DeleteFormF5Hdr(int id);
        Task<byte[]> FormDowload(string formName, int id, string filePath);
    }
}
