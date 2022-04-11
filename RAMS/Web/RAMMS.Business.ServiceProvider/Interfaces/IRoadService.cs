using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.RequestBO;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
  public interface IRoadService
  {
    long LastRoadInsertedNo();

    Task<RoadRequestDTO> GetRoadById(int id);

    Task<int> SaveRoad(RoadRequestDTO model);

    Task<bool> RemoveRoad(int id);

    Task<GridWrapper<object>> GetRoadList(DataTableAjaxPostModel filterOptions);
  }
}
