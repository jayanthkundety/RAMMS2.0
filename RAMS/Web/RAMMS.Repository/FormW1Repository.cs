using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.EF;
using RAMMS.DTO.ResponseBO;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository
{
    public class FormW1Repository : RepositoryBase<FormW1ResponseDTO>, IFormW1Repository
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
    }
}
