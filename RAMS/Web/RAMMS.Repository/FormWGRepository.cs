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
    public class FormWGRepository : RepositoryBase<RmIwFormWg>, IFormWGRepository
    {
        public FormWGRepository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<RmIwFormWg> FindWGByID(int Id)
        {
            return await _context.RmIwFormWg.Include(x => x.FwgFw1PkRefNoNavigation).Where(x => x.FwgPkRefNo == Id && x.FwgActiveYn == true).FirstOrDefaultAsync();
        }

        public async Task<RmIwFormWg> FindWGByW1ID(int Id)
        {
            return await _context.RmIwFormWg.Include(x => x.FwgFw1PkRefNoNavigation).Where(x => x.FwgFw1PkRefNo  == Id && x.FwgActiveYn == true).FirstOrDefaultAsync();
        }


        public async Task<RmIwFormW1> GetFormW1ById(int formW1Id)
        {
            return await _context.RmIwFormW1.Where(x => x.Fw1ActiveYn == true && x.Fw1PkRefNo == formW1Id).FirstOrDefaultAsync();
        }
    }
}
