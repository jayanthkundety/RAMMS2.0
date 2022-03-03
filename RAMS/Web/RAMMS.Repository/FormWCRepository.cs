using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.Models;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAMMS.Repository
{
    public class FormWCRepository : RepositoryBase<RmIwFormWc>, IFormWCRepository
    {
        public FormWCRepository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<RmIwFormWc> FindWCByID(int Id)
        {
            return await _context.RmIwFormWc.Include(x => x.FwcFw1PkRefNoNavigation).Where(x => x.FwcPkRefNo == Id && x.FwcActiveYn == true && x.FwcFw1PkRefNoNavigation.Fw1PkRefNo == x.FwcFw1PkRefNo).FirstOrDefaultAsync();
        }

        public async Task<RmIwFormWc> FindWCByw1ID(int Id)
        {
            return await _context.RmIwFormWc.Include(x => x.FwcFw1PkRefNoNavigation).Where(x => x.FwcFw1PkRefNo == Id && x.FwcActiveYn == true && x.FwcFw1PkRefNoNavigation.Fw1PkRefNo == x.FwcFw1PkRefNo).FirstOrDefaultAsync();
        }

        public async Task<RmIwFormW1> GetFormW1ById(int formW1Id)
        {
            return await _context.RmIwFormW1.Where(x => x.Fw1ActiveYn == true && x.Fw1PkRefNo == formW1Id).FirstOrDefaultAsync();
        }
    }
}
