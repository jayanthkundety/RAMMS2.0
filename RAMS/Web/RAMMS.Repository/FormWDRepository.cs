﻿using Microsoft.EntityFrameworkCore;
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
    public class FormWDRepository : RepositoryBase<RmIwFormWd>, IFormWDRepository
    {
        public FormWDRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }

        public int SaveFormWD(RmIwFormWd FormWD)
        {
            try
            {
                _context.Entry<RmIwFormWd>(FormWD).State = FormWD.FwdPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return FormWD.FwdPkRefNo;
            }
            catch (Exception)
            {
                return 500;

            }
        }

        public int? SaveFormWDClause(RmIwFormWdDtl FormWDDtl)
        {
            try
            {
              
                var res = (from r in _context.RmIwFormWdDtl where r.FwddPkRefNo == FormWDDtl.FwddPkRefNo select r).SingleOrDefault();
                if (res != null)
                {
                    _context.Entry(res).State = EntityState.Deleted;
                    _context.SaveChanges();
                }

                _context.Entry<RmIwFormWdDtl>(FormWDDtl).State = FormWDDtl.FwddPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return FormWDDtl.FwddFwdPkRefNo;
            }
            catch (Exception ex)
            {
                return 500;

            }
        }

        public async Task<RmIwFormWd> FindFormWDByID(int Id)
        {
            return await _context.RmIwFormWd.Where(x => x.FwdPkRefNo == Id && x.FwdActiveYn == true).FirstOrDefaultAsync();
        }

     

    }
}
