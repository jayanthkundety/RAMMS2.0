using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.Models;
using RAMMS.DTO.ResponseBO;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository
{
    public class FormW1Repository : RepositoryBase<RmIwFormW1>, IFormW1Repository
    {
        public FormW1Repository(RAMMSContext context) : base(context)
        {
            _context = context;
        }
     
        public int SaveFormW1(RmIwFormW1 FormW1)
        {
            try
            {
                _context.Entry<RmIwFormW1>(FormW1).State = FormW1.Fw1PkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return FormW1.Fw1PkRefNo;
            }
            catch (Exception)
            {
                return 500;

            }
        }

        public async Task<RmIwFormW1> FindFormW1ByID(int Id)
        {
            return await _context.RmIwFormW1.Where(x => x.Fw1PkRefNo == Id && x.Fw1ActiveYn == true).FirstOrDefaultAsync();
        }

    }
}
