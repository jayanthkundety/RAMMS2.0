using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.Models;
using RAMS.Repository;

namespace RAMMS.Repository
{
    public class FormS2QuarterDtlRepository : RepositoryBase<RmFormS2QuarDtl>
    {

        public FormS2QuarterDtlRepository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<RmFormS2QuarDtl>> SaveQuarDtl(IEnumerable<RmFormS2QuarDtl> rmFormS2QuarDtls, int detailId)
        {
            _context.RmFormS2QuarDtl.AddRange(rmFormS2QuarDtls);
            await _context.SaveChangesAsync();
            return _context.RmFormS2QuarDtl.Where(s => s.FsiiqdFsiidPkRefNo == detailId).ToList();
        }
    }
}
