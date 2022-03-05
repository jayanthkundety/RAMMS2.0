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

        Task<RmIwFormW2> FindFormW2ByPKRefNo(int PKRefNo);
        Task<List<RmIwformImage>> GetImagelist(string IwRefNo);
        Task<int> GetImageId(string iwRefNo, string type);

        Task<int> GetImageIdByW1Id(int formW1Id, string type);

        Task<RmIwformImage> GetImageById(int imageId);
        void SaveImage(IEnumerable<RmIwformImage> image);

        void UpdateImage(RmIwformImage image);
    }
}
