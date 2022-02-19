using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace RAMMS.Repository.Interfaces
{
    public interface IFormW2FcemRepository : IRepositoryBase<RmIwFormW2Fecm>
    {
        Task<RmIwFormW2Fecm> FindFCEM2ByW2ID(int Id);

        Task<RmIwFormW2Fecm> FindFCEM2ByID(int Id);

    }
}
