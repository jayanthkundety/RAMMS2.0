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
   public interface IFormR1R2Repository : IRepositoryBase<RmFormR1Hdr>
    {
        Task<RmFormR1Hdr> FindDetails(RmFormR1Hdr frmR1R2);
        Task<RmFormR1Hdr> FindByHeaderID(int headerId);
        Task<RmFormR1Hdr> Save(RmFormR1Hdr frmR1R2, bool updateSubmit);
        Task<List<FormR1R2PhotoTypeDTO>> GetExitingPhotoType(int headerId);
        Task<RmFormRImages> AddImage(RmFormRImages image);
        Task<IList<RmFormRImages>> AddMultiImage(IList<RmFormRImages> images);
        Task<List<RmFormRImages>> ImageList(int headerId);
        Task<int> DeleteImage(RmFormRImages img);
        Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData);
        ///Task<List<RmInspItemMas>> GetInspItemMaster();

        //int Delete(RmFormG1Hdr frmC1C2);
        int DeleteHeader(RmFormR1Hdr frmR1R2);
        //Task<IEnumerable<SelectListItem>> GetCVId(AssetDDLRequestDTO request);
        Task<int> ImageCount(string type, long headerId);
    }

    public interface IFormR2Repository : IRepositoryBase<RmFormR2Hdr>
    {
        Task<RmFormR2Hdr> FindDetails(RmFormR2Hdr frmC1C2);
        Task<RmFormR2Hdr> FindByHeaderID(int headerId);
        Task<RmFormR2Hdr> Save(RmFormR2Hdr frmC1C2, bool updateSubmit);
        Task<List<FormR1R2PhotoTypeDTO>> GetExitingPhotoType(int headerId);
        Task<RmFormRImages> AddImage(RmFormRImages image);
        Task<IList<RmFormRImages>> AddMultiImage(IList<RmFormRImages> images);
        Task<List<RmFormRImages>> ImageList(int headerId);
        Task<int> DeleteImage(RmFormRImages img);
        Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData);
        //Task<List<RmInspItemMas>> GetInspItemMaster();

        //int Delete(RmFormR2Hdr frmC1C2);
        int DeleteHeader(RmFormR2Hdr frmC1C2);
        //List<FormC1C2Rpt> GetReportData(int headerid);
        //Task<IEnumerable<SelectListItem>> GetCVId(AssetDDLRequestDTO request);
        Task<int> ImageCount(string type, long headerId);
    }
}
