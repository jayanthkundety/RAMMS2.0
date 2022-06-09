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
    public interface IFormG1Repository : IRepositoryBase<RmFormG1Hdr>
    {
        Task<RmFormG1Hdr> FindDetails(RmFormG1Hdr frmC1C2);
        Task<RmFormG1Hdr> FindByHeaderID(int headerId);
        Task<RmFormG1Hdr> Save(RmFormG1Hdr frmC1C2, bool updateSubmit);
        Task<List<FormG1G2PhotoTypeDTO>> GetExitingPhotoType(int headerId);
        Task<RmFormGImages> AddImage(RmFormGImages image);
        Task<IList<RmFormGImages>> AddMultiImage(IList<RmFormGImages> images);
        Task<List<RmFormGImages>> ImageList(int headerId);
        Task<int> DeleteImage(RmFormGImages img);
        Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData);
        ///Task<List<RmInspItemMas>> GetInspItemMaster();

        //int Delete(RmFormG1Hdr frmC1C2);
        int DeleteHeader(RmFormG1Hdr frmC1C2);
        //Task<IEnumerable<SelectListItem>> GetCVId(AssetDDLRequestDTO request);
        Task<int> ImageCount(string type, long headerId);

    }

    public interface IFormG2Repository : IRepositoryBase<RmFormG2Hdr>
    {
        Task<RmFormG2Hdr> FindDetails(RmFormG2Hdr frmC1C2);
        Task<RmFormG2Hdr> FindByHeaderID(int headerId);
        Task<RmFormG2Hdr> Save(RmFormG2Hdr frmC1C2, bool updateSubmit);
        Task<List<FormC1C2PhotoTypeDTO>> GetExitingPhotoType(int headerId);
        Task<RmFormGImages> AddImage(RmFormGImages image);
        Task<IList<RmFormGImages>> AddMultiImage(IList<RmFormGImages> images);
        Task<List<RmFormGImages>> ImageList(int headerId);
        Task<int> DeleteImage(RmFormGImages img);
        Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData);
        //Task<List<RmInspItemMas>> GetInspItemMaster();

        //int Delete(RmFormG2Hdr frmC1C2);
        int DeleteHeader(RmFormG2Hdr frmC1C2);
        //List<FormC1C2Rpt> GetReportData(int headerid);
        //Task<IEnumerable<SelectListItem>> GetCVId(AssetDDLRequestDTO request);
        Task<int> ImageCount(string type, long headerId);
    }

}
