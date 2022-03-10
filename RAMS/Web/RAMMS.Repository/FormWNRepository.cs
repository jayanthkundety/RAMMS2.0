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
    public class FormWNRepository : RepositoryBase<RmIwFormWn>, IFormWNRepository
    {
        public FormWNRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }

        public int SaveFormWN(RmIwFormWn FormWN)
        {
            try
            {
                _context.Entry<RmIwFormWn>(FormWN).State = FormWN.FwnPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return FormWN.FwnPkRefNo;
            }
            catch (Exception)
            {
                return 500;

            }
        }

        public async Task<RmIwFormWn> FindFormWNByID(int Id)
        {
            return await _context.RmIwFormWn.Where(x => x.FwnPkRefNo == Id && x.FwnActiveYn == true).FirstOrDefaultAsync();
        }
         
    }
}
