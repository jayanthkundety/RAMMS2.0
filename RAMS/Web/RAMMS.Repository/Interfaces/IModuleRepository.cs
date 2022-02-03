using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.Repository.Interfaces
{
    public interface IModuleRepository:IRepositoryBase<RmModule>
    {
        IList<RmModule> GetAllModules();
    }
}
