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
    public interface IFormC1C2Repository : IRepositoryBase<RmFormCvInsHdr>
    {
        Task<RmFormCvInsHdr> FindDetails(RmFormCvInsHdr frmC1C2);
        Task<RmFormCvInsHdr> FindByHeaderID(int headerId);
        Task<RmFormCvInsHdr> Save(RmFormCvInsHdr frmC1C2, bool updateSubmit);
        Task<List<FormC1C2PhotoTypeDTO>> GetExitingPhotoType(int headerId);
        Task<RmFormCvInsImage> AddImage(RmFormCvInsImage image);
        Task<IList<RmFormCvInsImage>> AddMultiImage(IList<RmFormCvInsImage> images);
        Task<List<RmFormCvInsImage>> ImageList(int headerId);
        Task<int> DeleteImage(RmFormCvInsImage img);
        Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData);
        Task<List<RmInspItemMas>> GetInspItemMaster();

        //int Delete(RmFormCvInsHdr frmC1C2);
        int DeleteHeader(RmFormCvInsHdr frmC1C2);
        List<FormC1C2Rpt> GetReportData(int headerid);
        Task<IEnumerable<SelectListItem>> GetCVId(AssetDDLRequestDTO request);
        Task<int> ImageCount(string type, long headerId);
    }
}
