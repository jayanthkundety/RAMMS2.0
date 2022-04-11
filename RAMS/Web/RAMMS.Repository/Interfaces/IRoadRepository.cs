using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
  public interface IRoadRepository : IRepositoryBase<RmRoadMaster>
  {
    Task<long> GetFilteredRecordCount(
      FilteredPagingDefinition<RoadRequestDTO> filterOptions);

    Task<GridWrapper<object>> GetFilteredRecordList(
      DataTableAjaxPostModel filterOptions);
  }
}
