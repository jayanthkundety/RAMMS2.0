using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RAMMS.Common;
using RAMMS.Common.Extensions;
using RAMMS.Common.RefNumber;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository
{
    public class FormW2FcemRepository : RepositoryBase<RmIwFormW2Fecm>, IFormW2FcemRepository
    {
        public FormW2FcemRepository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<RmIwFormW2Fecm> FindFCEM2ByW2ID(int Id)
        {
            return await _context.RmIwFormW2Fecm.Where(x => x.FecmFw2PkRefNo == Id && x.FecmActiveYn == true).FirstOrDefaultAsync();
        }


        public async Task<RmIwFormW2Fecm> FindFCEM2ByID(int Id)
        {
            return await _context.RmIwFormW2Fecm.Where(x => x.FecmPkRefNo == Id && x.FecmActiveYn == true).FirstOrDefaultAsync();
        }
    }
}
