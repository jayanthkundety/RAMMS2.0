
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
    public interface IFormF1Repository : IRepositoryBase<RmFormF1Hdr>
    {
        //   Task<IEnumerable<RmFormF1Dtl>> FindFormF1DtlByID(int Id);


        Task<long> GetFilteredRecordCount(FilteredPagingDefinition<FormF1SearchGridDTO> filterOptions);
        Task<List<FormF1HeaderRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormF1SearchGridDTO> filterOptions);
        Task<List<FormF1DtlGridDTO>> GetFormF1DtlGridList(FilteredPagingDefinition<FormF1DtlResponseDTO> filterOptions);

        List<RmAllassetInventory> GetAssetDetails(FormF1ResponseDTO FormF1);

      //  int LoadG1G2Data(FormF1ResponseDTO FormF1);

        Task<RmFormF1Hdr> SaveFormF1(RmFormF1Hdr domainModelFormF1);
        int? DeleteFormF1(int id);
        int? DeleteFormF1Dtl(int Id);
        int? SaveFormF1Dtl(RmFormF1Dtl FormF1Dtl);
        int? UpdateFormF1Dtl(RmFormF1Dtl FormF1Dtl);

        Task<FORMF1Rpt> GetReportData(int headerid);

    }
}
