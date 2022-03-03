using RAMMS.Domain.Models;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormWCRepository : IRepositoryBase<RmIwFormWc>
    {
        public Task<RmIwFormWc> FindWCByID(int Id);

        Task<RmIwFormWc> FindWCByw1ID(int Id);
        Task<RmIwFormW1> GetFormW1ById(int formW1Id);
    }
}
