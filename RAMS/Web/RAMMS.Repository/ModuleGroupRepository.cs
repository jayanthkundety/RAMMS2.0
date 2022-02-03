using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RAMMS.Repository
{
    public class ModuleGroupRepository : RepositoryBase<RmModuleGroupRights>, IModuleGroupRepository
    {
        public ModuleGroupRepository(RAMMSContext context) : base(context) { _context = context ?? throw new ArgumentNullException(nameof(context)); }
        public async Task<long> GetFilteredRecordCount(FilteredPagingDefinition<ModuleGroupRightsRequestDTO> filterOptions)
        {
            return await (from s in _context.RmModuleGroupRights
                          join m in _context.RmModule on s.ModPkId equals m.ModPkId
                          join g in _context.RmGroup on s.UgPkId equals g.UgPkId
                          select s).LongCountAsync();
        }
        public async Task<List<ModuleGroupRightsRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<ModuleGroupRightsRequestDTO> filterOptions)
        {
            var query = (from s in _context.RmModuleGroupRights
                         join m in _context.RmModule on s.ModPkId equals m.ModPkId
                         join g in _context.RmGroup on s.UgPkId equals g.UgPkId
                         select new { s, m, g });
            query = query.OrderBy(s => s.g.UgGroupName);
            var lst = await query.Skip(filterOptions.StartPageNo).Take(filterOptions.RecordsPerPage).ToListAsync();
            return lst.Select(s => new ModuleGroupRightsRequestDTO
            {
                MgrPkid = s.s.MgrPkId,
                ModPkid = s.s.ModPkId,
                Ugpkid = s.s.UgPkId,
                DvIsview = s.s.DvIsView,
                DvIsmodify = s.s.DvIsModify,
                DvIsdelete = s.s.DvIsDelete,
                PcIsview = s.s.PcIsView,
                PcIsmodify = s.s.PcIsModify,
                PcIsdelete = s.s.PcIsDelete,
                MgrCreatedby = s.s.MgrCreatedBy,
                MgrCreatedon = s.s.MgrCreatedOn,
                MgrModifiedby = s.s.MgrModifiedBy,
                MgrModifiedon = s.s.MgrModifiedOn,
                DvIsadd = s.s.DvIsAdd,
                PcIsadd = s.s.PcIsAdd,
                Module = s.m.ModName,
                Group = s.g.UgGroupName
            }).ToList();
        }
    }
}
