using RAMMS.Domain.Models;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormW2Repository : IRepositoryBase<RmIwFormW2>
    {
        RmIwFormW2 SaveFormW2(RmIwFormW2 formS1Header, bool updateSubmit);
        public Task<RmIwFormW2> FindW2ByID(int Id);
        void SaveImage(IEnumerable<RmIwFormW2Image> image);
        void UpdateImage(RmIwFormW2Image image);
        Task<List<RmIwformImage>> GetImagelist(int formW2Id);
        Task<RmIwFormW2Image> GetImageById(int imageId);
        Task<int> GetImageId(int formW2Id, string type);
        Task<List<RmIwFormW1>> GetFormW1List();
        Task<RmIwFormW1> GetFormW1ById(int formW1Id);

        Task<RmIwFormW1> GetFormW1ByRoadCode(string roadCode);

        Task<List<FormIWResponseDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormIWSearchGridDTO> filterOptions);

        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormIWSearchGridDTO> filterOptions);

        Task<IEnumerable<RmRoadMaster>> GetRoadCodesByRMU(string rmu);

    }
}
