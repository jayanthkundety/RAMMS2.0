using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
  public interface ISectionRepository : IRepositoryBase<RmDivRmuSecMaster>
  {
    Task<long> GetFilteredRecordCount(
      FilteredPagingDefinition<SectionRequestDTO> filterOptions);

    Task<List<SectionRequestDTO>> GetFilteredRecordList(
      FilteredPagingDefinition<SectionRequestDTO> filterOptions);
  }
}
