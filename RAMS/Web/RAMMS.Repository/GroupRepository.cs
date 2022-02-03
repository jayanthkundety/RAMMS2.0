using RAMMS.Domain.Models;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace RAMMS.Repository
{
    public class GroupRepository : RepositoryBase<RmGroup>, IGroupRepository
    {
        public GroupRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }

        public IList<RmGroup> GetAllGroups()
        {
            return _context.RmGroup.ToListAsync().Result;
        }
    }
}
