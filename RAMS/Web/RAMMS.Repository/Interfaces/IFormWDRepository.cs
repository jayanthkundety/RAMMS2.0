using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormWDRepository : IRepositoryBase<RmIwFormWd>
    {
        int SaveFormWD(RmIwFormWd FormWD);
        Task<RmIwFormWd> FindWDByw1ID(int Id);
        Task<RmIwFormWd> FindFormWDByID(int Id);
        Task<IEnumerable<RmIwFormWdDtl>> FindFormWDDtlByID(int Id);
        int? DeleteFormWDClause(int Id);
        int? SaveFormWDClause(RmIwFormWdDtl FormWDDtl);

        
    }
}
