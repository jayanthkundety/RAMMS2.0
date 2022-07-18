
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace RAMMS.Repository.Interfaces
{
    public interface IFormTRepository : IRepositoryBase<RmFormTHdr>
    {
        //   Task<IEnumerable<RmFormTDailyInspection>> FindFormTDtlByID(int Id);

         Task<List<FormTHeaderRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormTSearchGridDTO> filterOptions);
        Task<List<FormTDtlGridDTO>> GetFormTDtlGridList(FilteredPagingDefinition<FormTDtlResponseDTO> filterOptions);

        RmFormTDailyInspection GetFormTDtlById(int id);


        int? DeleteFormT(int id);
        int? DeleteFormTDtl(int Id);
        int? SaveFormTDtl(RmFormTDailyInspection FormTDtl, List<RmFormTVechicle> Vechicles);
        int? UpdateFormTDtl(RmFormTDailyInspection FormTDtl, List<RmFormTVechicle> Vechicles);

        //Task<FORMTRpt> GetReportData(int headerid);

    }
}
