using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormMRepository: IRepositoryBase<RmFormMHdr>
    {
        Task<RmFormMHdr> FindDetails(RmFormMHdr frmM);
        Task<RmFormMHdr> FindByHeaderID(int headerId);
        Task<RmFormMHdr> Save(RmFormMHdr frm, bool updateSubmit);
        Task<RmFormMAuditDetails> SaveAD(RmFormMAuditDetails frmMAD, bool updateSubmit);
        Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData);
        int DeleteHeader(RmFormMHdr frmM);
        List<FormMRpt> GetReportData(int headerid);
        bool isF1Exist(int id);
    }
    public interface IFormMAuditDetails: IRepositoryBase<RmFormMAuditDetails>
    {
        Task<RmFormMAuditDetails> FindDetails(RmFormMAuditDetails frmMAD);
        Task<RmFormMAuditDetails> FindByHeaderID(int headerId);
        Task<RmFormMAuditDetails> Save(RmFormMAuditDetails frmMAD, bool updateSubmit);
        Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData);
        //Task<List<RmInspItemMas>> GetInspItemMaster();

        //int Delete(RmFormR2Hdr frmC1C2);
        int DeleteHeader(RmFormMAuditDetails frmMAD);
        //List<FormC1C2Rpt> GetReportData(int headerid);
        //Task<IEnumerable<SelectListItem>> GetCVId(AssetDDLRequestDTO request);
    }
}
