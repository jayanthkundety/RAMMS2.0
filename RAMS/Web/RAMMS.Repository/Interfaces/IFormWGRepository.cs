using RAMMS.Domain.Models;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormWGRepository : IRepositoryBase<RmIwFormWg>
    {
        public Task<RmIwFormWg> FindWGByID(int Id);

        Task<RmIwFormWg> FindWGByW1ID(int Id);

        Task<RmIwFormW1> GetFormW1ById(int formW1Id);
    }
}
