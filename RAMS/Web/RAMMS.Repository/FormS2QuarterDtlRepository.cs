using System;
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
    }
}
