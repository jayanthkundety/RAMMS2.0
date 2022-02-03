using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.SearchBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
namespace RAMMS.Repository.Interfaces
{
    public interface IFieldRightsRepository : IRepositoryBase<RmUvModuleGroupFieldRights>
    {
        IList<RmUvModuleGroupFieldRights> GetAllFieldRights();
    }
}
