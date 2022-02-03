using Microsoft.EntityFrameworkCore;
using RAMMS.Common.Extensions;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RAMMS.Repository
{
    public interface IFormQa2DtlRepository : IRepositoryBase<RmFormQa2Dtl>
    {
        int SaveFormQa2Dtl(RmFormQa2Dtl rmFormQa2Dtl);
        IQueryable<RmFormQa2Hdr> GetGridHdrData(RmFormQa2Hdr rmFormQa2Dtl);
        Task<RmFormQa2Dtl> DetailView(RmFormQa2Dtl rmFormQa2Dtl);
        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormQa2SearchGridDTO> filterOptions, string id);
        Task<List<RmFormQa2Dtl>> GetFilteredRecordList(FilteredPagingDefinition<FormQa2SearchGridDTO> filterOptions, string id);
        Task<RmFormQa2Dtl> GetFormWithDetailsByNoAsync(int formNo);
        Task<RmFormS1Hdr> GetFormS1Data(int id);
        Task<int> GetMaxCount();
        Task<int?> GetSrNo(int headerNo);
        Task<List<RmFormQa2Dtl>> GetAllDtlById(int headerId);


    }
    public class FormQa2DtlRepository : RepositoryBase<RmFormQa2Dtl> , IFormQa2DtlRepository
    {
        public FormQa2DtlRepository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<RmFormQa2Dtl> DetailView(RmFormQa2Dtl rmFormQa2Dtl)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormQa2SearchGridDTO> filterOptions, string id)
        {
            int header = 0;
            int.TryParse(filterOptions.Filters.HeaderNo, out header);
            var query = (from x in _context.RmFormQa2Dtl where x.FqaiidFqaiihPkRefNo == header select x);
            PrepareFilterQuery(filterOptions, ref query);
            return await query.CountAsync();
        }

        public async Task<List<RmFormQa2Dtl>> GetFilteredRecordList(FilteredPagingDefinition<FormQa2SearchGridDTO> filterOptions, string id)
        {
            List<RmFormQa2Dtl> result = new List<RmFormQa2Dtl>();
            int header = 0;
            int.TryParse(filterOptions.Filters.HeaderNo, out header);
            var query = (from x in _context.RmFormQa2Dtl where x.FqaiidFqaiihPkRefNo == header  select x);


            PrepareFilterQuery(filterOptions, ref query);

            result = await query.OrderBy(s => s.FqaiidPkRefNo)
                                .Include(x => x.FqaiidFqaiihPkRefNoNavigation)
                                .Include(x => x.FqaiidFsidPkRefNoNavigation)
                                .Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            return result;
        }

        public async Task<RmFormQa2Dtl> GetFormWithDetailsByNoAsync(int formNo)
        {
            var data = await _context.RmFormQa2Dtl.FirstOrDefaultAsync(s => s.FqaiidPkRefNo == formNo);
            var n1Check = await _context.RmFormN1Hdr.Where(x => x.FnihFqaiidPkRefNo == formNo).CountAsync();
            if(n1Check != 0)
            {
                data.FqaiihNcnYn = true;
            }
            return data;
        }
       

        public IQueryable<RmFormQa2Hdr> GetGridHdrData(RmFormQa2Hdr rmFormQa2Dtl)
        {
            IQueryable<RmFormQa2Hdr> gridData;
            gridData = _context.RmFormQa2Hdr
                .AsNoTracking();

            return gridData;
        }

        public int SaveFormQa2Dtl(RmFormQa2Dtl rmFormQa2Dtl)
        {
            try
            {
                _context.Entry<RmFormQa2Dtl>(rmFormQa2Dtl).State = rmFormQa2Dtl.FqaiidPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                return _context.SaveChanges();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CheckwithRef(FormQa2DtlRequestDTO formNiHeader)
        {
            var data = await _context.RmFormQa2Dtl.Where(x => x.FqaiidPkRefNo == formNiHeader.No).FirstOrDefaultAsync();
            if (data != null)
            {
                return data.FqaiidPkRefNo;
            }
            else
            {
                return 0;
            }
        }

        public async Task<RmFormS1Hdr> GetFormS1Data(int id)
        {
            return await _context.RmFormS1Hdr.Where(x => x.FsihPkRefNo == id).FirstOrDefaultAsync();
        }

        public async Task<int> GetMaxCount()
        {
            var count = await _context.RmFormQa2Dtl.Select(m => m.FqaiidPkRefNo).CountAsync();
            if (count > 0)
            {
                var query = (from x in _context.RmFormQa2Dtl select x);
                return await query.MaxAsync(m => m.FqaiidPkRefNo);
            }
            return 0;
        }

        private void PrepareFilterQuery(FilteredPagingDefinition<FormQa2SearchGridDTO> filterOptions, ref IQueryable<RmFormQa2Dtl> query)
        {
            query = query.Where(x => x.FqaiidActiveYn == true);
            if (filterOptions.Filters != null)
            {
                if (!string.IsNullOrEmpty(filterOptions.Filters.Road_Code))
                {
                    query = query.Where(x => x.FqaiidFqaiihPkRefNoNavigation.FqaiihRoadCode == filterOptions.Filters.Road_Code);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RoadName))
                {
                    query = query.Where(x => x.FqaiidFqaiihPkRefNoNavigation.FqaiihRoadName == filterOptions.Filters.RoadName);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
                {
                    query = query.Where(x => x.FqaiidFqaiihPkRefNoNavigation.FqaiihRmu == filterOptions.Filters.RMU);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    query = query.Where(x => x.FqaiidFqaiihPkRefNoNavigation.FqaiihRmu.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FqaiidFqaiihPkRefNoNavigation.FqaiihRoadCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FqaiidFqaiihPkRefNoNavigation.FqaiihRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FqaiidRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FqaiidFqaiihPkRefNoNavigation.FqaiihUsernameQaIv.Contains(filterOptions.Filters.SmartInputValue)
                                        || (filterOptions.Filters.SmartInputValue.IsInt() && x.FqaiidFqaiihPkRefNoNavigation.FqaiihPkRefNo.Equals(filterOptions.Filters.SmartInputValue.AsInt())));

                }
            }
        }

        public async Task<int> Remove(int id)
        {
            var d = await _context.RmFormQa2Dtl.FirstOrDefaultAsync(s => s.FqaiidPkRefNo == id);
            if(d!=null)
            {
                d.FqaiidActiveYn = false;
               return await _context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<int?> GetSrNo(int headerNo)
        {
                int? srno = await _context.RmFormQa2Dtl.Where(x => x.FqaiidFqaiihPkRefNo == headerNo).Select(x => x.FqaiidSrno).CountAsync();
                srno += 1;
                return srno;
           
        }

        public async Task<List<RmFormQa2Dtl>> GetAllDtlById(int headerId)
        {
            return await _context.RmFormQa2Dtl.Where(x => x.FqaiidFqaiihPkRefNo == headerId && x.FqaiidActiveYn == true).ToListAsync();
        }
    }
}
