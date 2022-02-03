using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using static RAMMS.DTO.RequestBO.FormS1DetailDTO;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormS1Service
    {
        FormS1HeaderRequestDTO SaveHeader(FormS1HeaderRequestDTO headerDTO, bool updateSubmit);
        T SaveHeader<T>(T headerDTO, bool updateSubmit);

        FormS1HeaderRequestDTO FindDetails(FormS1HeaderRequestDTO headerDTO);
        Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData);
        Task<GridWrapper<object>> GetDetailsGrid(int headerId, DataTableAjaxPostModel searchData);
        FormS1HeaderRequestDTO FindHeaderByID(int headerId);
        T FindHeaderByID<T>(int headerId);
        int DeleteFormS1Hdr(int id);
        FormS1DetailDTO SaveDetails(FormS1DetailDTO formS1DetailsDTO);
        FormS1DetailDTO FindDetailsById(int detailPKId);
        int DeleteDetails(int id);
        Task<RmFormS1Dtl> FindS1Details(int dtlId);

        FORMS1Rpt GetReportData(int headerId);
        Byte[] FormDownload(string formName, int id, string basePath, string filePath);

        Task<List<ActWeekDtl>> GetFormDDetails(string roadCode, string actCode, string frmCh, string frmChDeci, string toCh, string toChDeci, string crewSupervisor, string weekNo);
    }
}
