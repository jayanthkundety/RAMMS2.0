 
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace RAMMS.Repository.Interfaces
{
    public interface IFormF3Repository : IRepositoryBase<RmFormF3Hdr>
    {
        //   Task<IEnumerable<RmFormF3Dtl>> FindFormF3DtlByID(int Id);


        Task<long> GetFilteredRecordCount(FilteredPagingDefinition<FormF2SearchGridDTO> filterOptions);
        Task<List<FormF2HeaderRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormF2SearchGridDTO> filterOptions);
        Task<List<FormF3DtlGridDTO>> GetFormF3DtlGridList(FilteredPagingDefinition<FormF3DtlResponseDTO> filterOptions);

         List<RmAllassetInventory> GetAssetDetails(FormF3ResponseDTO FormF3);

        int LoadG1G2Data(FormF3ResponseDTO FormF3);

        int? DeleteFormF3(int id);
        int? DeleteFormF3Dtl(int Id);
        int? SaveFormF3Dtl(RmFormF3Dtl FormF3Dtl);
        int? UpdateFormF3Dtl(RmFormF3Dtl FormF3Dtl);

    }
}
