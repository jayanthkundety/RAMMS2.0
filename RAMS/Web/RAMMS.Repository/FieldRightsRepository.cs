using System;
using System.Collections.Generic;
using System.Text;
using RAMMS.Repository.Interfaces;
using RAMMS.Domain.Models;
using RAMS.Repository;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace RAMMS.Repository
{
    public class FieldRightsRepository : RepositoryBase<RmUvModuleGroupFieldRights>,IFieldRightsRepository
    {
        public FieldRightsRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }
        public IList<RmUvModuleGroupFieldRights> GetAllFieldRights()
        {
            return _context.RmUvModuleGroupFieldRights.ToListAsync().Result;
        }
    }
}
