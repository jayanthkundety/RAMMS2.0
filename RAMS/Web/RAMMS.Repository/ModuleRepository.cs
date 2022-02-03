using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.Models;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.Repository
{
    public class ModuleRepository : RepositoryBase<RmModule>, IModuleRepository
    {
        public ModuleRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }

        public IList<RmModule> GetAllModules()
        {
            return _context.RmModule.Include(x=>x.RmModuleGroupRights).ToListAsync().Result;
        }
    }
}
