using System;
using System.Collections.Generic;
using System.Text;
using RAMMS.Domain.Models;
namespace RAMMS.Repository.Interfaces
{
    public interface IGroupRepository : IRepositoryBase<RmGroup>
    {
        IList<RmGroup> GetAllGroups();
    }
}
