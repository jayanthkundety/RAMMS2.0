using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormW2Repository : IRepositoryBase<RmIwFormW2>
    {
        RmIwFormW2 SaveFormW2(RmIwFormW2 formS1Header, bool updateSubmit);

        public Task<RmIwFormW2> FindW2ByID(int Id);
    }
}
