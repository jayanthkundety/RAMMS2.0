using RAMMS.Domain.Models;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace RAMMS.Repository
{
    public class ModuleRightsRepository : RepositoryBase<RmUvModuleGroupRights>, IModuleRightsRepository
    {        
        public ModuleRightsRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }
        public IList<RmUvModuleGroupRights> GetAllModuleRights()
        {
            return _context.RmUvModuleGroupRights.ToListAsync().Result;
        }
    }
}
