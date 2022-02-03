using System;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace RAMMS.Repository
{
    public class AuditActionRepository : RepositoryBase<RmAuditLogAction>, IAuditActionRepository
    {
        public AuditActionRepository(RAMMSContext context) : base(context) { _context = context ?? throw new ArgumentNullException(nameof(context)); }
        public async Task<long> GetFilteredRecordCount(FilteredPagingDefinition<AuditActionRequestDTO> filterOptions) { return await (from s in _context.RmAuditLogAction select s).LongCountAsync(); }
        public async Task<List<AuditActionRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<AuditActionRequestDTO> filterOptions)
        {
            var query = (from s in _context.RmAuditLogAction select s);
            var lst = await query.Skip(filterOptions.StartPageNo).Take(filterOptions.RecordsPerPage).ToListAsync();
            return lst.Select(s => new AuditActionRequestDTO
            {
                PkRefNo = s.AlaPkRefNo,
                Requestip = s.AlaRequestIp,
                Requester = s.AlaRequester,
                Actionname = s.AlaActionName,
                CrDt = s.AlaCrDt,
                CrBy = s.AlaCrBy,
            }).ToList();
        }
    }
}
