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
    public class FormF3Repository : RepositoryBase<RmFormF3Hdr>, IFormF3Repository
    {
        public FormF3Repository(RAMMSContext context) : base(context)
        {
            _context = context;
        }

        public int SaveFormF3(RmFormF3Hdr FormF3)
        {
            try
            {
                _context.Entry<RmFormF3Hdr>(FormF3).State = FormF3.Ff3hPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return FormF3.Ff3hPkRefNo;
            }
            catch (Exception)
            {
                return 500;

            }
        }

        public int? DeleteFormF3Dtl(int Id)
        {
            var res = (from r in _context.RmFormF3Dtl where r.Ff3dPkRefNo == Id select r).SingleOrDefault();
            if (res != null)
            {
                _context.Entry(res).State = EntityState.Deleted;
                _context.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int? SaveFormF3Dtl(RmFormF3Dtl FormF3Dtl)
        {
            try
            {


                _context.Entry<RmFormF3Dtl>(FormF3Dtl).State = FormF3Dtl.Ff3dPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return FormF3Dtl.Ff3dPkRefNo;
            }
            catch (Exception ex)
            {
                return 500;

            }
        }


        //public async Task<RmFormF3Hdr> FindF3Byw1ID(int Id)
        //{
        //    return await _context.RmFormF3Hdr.Where(x => x.FF3Fw1PkRefNo == Id && x.FF3ActiveYn == true).FirstOrDefaultAsync();
        //}

        //public async Task<RmFormF3Hdr> FindFormF3ByID(int Id)
        //{
        //    return await _context.RmFormF3Hdr.Where(x => x.FF3PkRefNo == Id && x.FF3ActiveYn == true).FirstOrDefaultAsync();
        //}

        //public async Task<IEnumerable<RmFormF3HdrDtl>> FindFormF3DtlByID(int Id)
        //{
        //    return await _context.RmFormF3HdrDtl.Where(x => x.FF3dFF3PkRefNo == Id).ToListAsync();
        //}



    }
}
