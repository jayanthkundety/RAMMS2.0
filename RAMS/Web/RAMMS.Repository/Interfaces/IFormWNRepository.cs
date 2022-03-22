using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormWNRepository : IRepositoryBase<RmIwFormWn>
    {
        int SaveFormWN(RmIwFormWn FormWN);
        Task<RmIwFormWn> FindWNByW1ID(int Id);
        Task<RmIwFormWn> FindFormWNByID(int Id);
    }
}
