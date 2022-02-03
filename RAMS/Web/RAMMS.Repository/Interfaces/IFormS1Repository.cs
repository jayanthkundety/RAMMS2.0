using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static RAMMS.DTO.RequestBO.FormS1DetailDTO;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormS1Repository : IRepositoryBase<RmFormS1Hdr>
    {
        RmFormS1Hdr SaveFormS1Hdr(RmFormS1Hdr formS1Header, bool updateSubmit);
        RmFormS1Dtl SaveDetails(RmFormS1Dtl formS1Details);
        Task<RmFormS1Dtl> FindDetailsById(int detailPKId);
        Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData);
        int DeleteFormS1Hdr(RmFormS1Hdr formS1Header);
        FORMS1Rpt GetReportData(int headerId);
        Task<GridWrapper<object>> GetDetailsGrid(int headerId, DataTableAjaxPostModel searchData);
        int DeleteDetail(RmFormS1Dtl rmFormS1Dtl);
        Task<RmFormS1Dtl> GetDetailsById(int dtlId);
        Task<List<ActWeekDtl>> GetFormDDtls(string roadCode, string actCode, string frmCh, string frmChDeci, string toCh, string toChDeci, string crewSupervisor, string weekNo);
        void CreateWkdtl(RmFormS1WkDtl s1WkDtl);
        void UpdateWkdtl(RmFormS1WkDtl s1WkDtl);
        (int id, bool alreadyExists) CheckAlreadyExists(string roadCode, string activityCode, int fromChKm, string fromChM, int toChKm, string toChM, int weekNo);
    }
}
