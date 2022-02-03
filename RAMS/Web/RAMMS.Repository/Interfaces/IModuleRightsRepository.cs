using RAMMS.Domain.Models;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.Repository.Interfaces
{
    public interface IModuleRightsRepository : IRepositoryBase<RmUvModuleGroupRights>
    {
        IList<RmUvModuleGroupRights> GetAllModuleRights();
    }
}
