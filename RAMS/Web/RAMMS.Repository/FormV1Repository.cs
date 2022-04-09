using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
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
    public class FormV1Repository : RepositoryBase<RmFormV1Hdr>, IFormV1Repository
    {
        public FormV1Repository(RAMMSContext context) : base(context)
        {
            _context = context;
        }


        public async Task<List<RmFormV1Hdr>> GetFilteredRecordList(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions)
        {
            List<RmFormV1Hdr> result = new List<RmFormV1Hdr>();
            var query = (from x in _context.RmFormV1Hdr
                         let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeCode == x.Fv1hRmu || s.DdlTypeDesc == x.Fv1hRmu))
                         let div= _context.RmDivRmuSecMaster.FirstOrDefault(s => s.RdsmSectionCode == x.Fv1hSecCode)
                         let sec = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "Section Code" && (s.DdlTypeDesc == x.Fv1hSecCode || s.DdlTypeCode == x.Fv1hSecCode))
                         select new { rmu, sec, x ,div});



            query = query.Where(x => x.x.Fv1hActiveYn == true).OrderByDescending(x => x.x.Fv1hPkRefNo);
            if (filterOptions.Filters != null)
            {

                if (!string.IsNullOrEmpty(filterOptions.Filters.Section_Code))
                {
                    query = query.Where(x => x.sec.DdlTypeDesc == filterOptions.Filters.Section_Code || x.sec.DdlTypeCode == filterOptions.Filters.Section_Code);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
                {
                    query = query.Where(x => x.rmu.DdlTypeCode == filterOptions.Filters.RMU || x.rmu.DdlTypeDesc == filterOptions.Filters.RMU);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.Crew_Supervisor))
                {
                    query = query.Where(x => x.x.Fv1hCrew  == Convert.ToInt32(filterOptions.Filters.Crew_Supervisor));
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    query = query.Where(x => x.x.Fv1hRmu.Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.rmu.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || (x.sec.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                        || x.x.Fv1hCrewname.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.x.Fv1hRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        );

                }
            }


            if (filterOptions.sortOrder == SortOrder.Ascending)
            {

                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderBy(x => x.x.Fv1hRefId);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(x => x.x.Fv1hRmu);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(x => x.div.RdsmDivision);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(x => x.x.Fv1hActCode);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(x => x.x.Fv1hSecCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(x => x.x.Fv1hCrew);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(x => x.x.Fv1hDt);
 
            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderByDescending(x => x.x.Fv1hRefId);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(x => x.x.Fv1hRmu);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(x => x.div.RdsmDivision);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(x => x.x.Fv1hActCode);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(x => x.x.Fv1hSecCode);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(x => x.x.Fv1hCrew);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(x => x.x.Fv1hDt);
            }


            result = await query.Select(s => s.x)
                    .Skip(filterOptions.StartPageNo)
                    .Take(filterOptions.RecordsPerPage)
                    .ToListAsync();
            return result;
        }


        //public int SaveFormWD(RmIwFormWd FormWD)
        //{
        //    try
        //    {
        //        _context.Entry<RmIwFormWd>(FormWD).State = FormWD.FwdPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
        //        _context.SaveChanges();

        //        return FormWD.FwdPkRefNo;
        //    }
        //    catch (Exception)
        //    {
        //        return 500;

        //    }
        //}

        //public int? DeleteFormWDClause(int Id)
        //{
        //    var res = (from r in _context.RmIwFormWdDtl where r.FwddFwdPkRefNo == Id select r).SingleOrDefault();
        //    if (res != null)
        //    {
        //        _context.Entry(res).State = EntityState.Deleted;
        //        _context.SaveChanges();
        //        return 1;
        //    }
        //    return 0;
        //}

    }
}
