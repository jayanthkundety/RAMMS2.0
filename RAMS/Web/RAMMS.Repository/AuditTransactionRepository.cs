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
    public class AuditTransactionRepository : RepositoryBase<RmAuditLogTransaction>, IAuditTransactionRepository
    {
        public AuditTransactionRepository(RAMMSContext context) : base(context) { _context = context ?? throw new ArgumentNullException(nameof(context)); }
        public async Task<long> GetFilteredRecordCount(FilteredPagingDefinition<AuditTransactionRequestDTO> filterOptions)
        {
            return await (from s in _context.RmAuditLogTransaction where s.AltAlaPkRefNo == filterOptions.Filters.AlaPkRefNo select s).LongCountAsync();
        }
        public async Task<List<AuditTransactionRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<AuditTransactionRequestDTO> filterOptions)
        {
            var query = (from s in _context.RmAuditLogTransaction where s.AltAlaPkRefNo == filterOptions.Filters.AlaPkRefNo select s);
            var lst = await query.Skip(filterOptions.StartPageNo).Take(filterOptions.RecordsPerPage).ToListAsync();
            return lst.Select(s => new AuditTransactionRequestDTO
            {
                PkRefNo = s.AltPkRefNo,
                AlaPkRefNo = s.AltAlaPkRefNo,
                Transactionname = s.AltTransactionName,
                Tablename = s.AltTableName,
                Transactindetails = s.AltTransactinDetails,
            }).ToList();
        }
    }
}
