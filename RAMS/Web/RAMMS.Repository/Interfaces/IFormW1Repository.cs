using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormW1Repository : IRepositoryBase<RmIwFormW1>
    {
        int SaveFormW1(RmIwFormW1 FormW1);
        Task<RmIwFormW1> FindFormW1ByID(int Id);
        Task<List<RmIwformImage>> GetImagelist(string IwRefNo);
        Task<int> GetImageId(int formW2Id, string type);
        void SaveImage(IEnumerable<RmIwformImage> image);
    }
}
