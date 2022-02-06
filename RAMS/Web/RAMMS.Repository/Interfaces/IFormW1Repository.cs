using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormW1Repository : IRepositoryBase<RmIwFormW1>
    {
        public int SaveFormW1(RmIwFormW1 FormW1);
        public  Task<RmIwFormW1> FindFormW1ByID(int Id);
    }
}
