using Microsoft.EntityFrameworkCore;
using RAMMS.Common;
using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace RAMMS.Repository
{
    public class RoadRepository :
      RepositoryBase<RmRoadMaster>,
      IRoadRepository,
      IRepositoryBase<RmRoadMaster>
    {
        public RoadRepository(RAMMSContext context)
          : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<long> GetFilteredRecordCount(FilteredPagingDefinition<RoadRequestDTO> filterOptions)
        {
            return await _context.RmRoadMaster.Select(s => s).LongCountAsync();
        }

        public async Task<GridWrapper<object>> GetFilteredRecordList(DataTableAjaxPostModel filterOptions)
        {
            var query = (from hdr in _context.RmRoadMaster
                         select new
                         {
                             pkRefNo = hdr.RdmPkRefNo,
                             featureId = hdr.RdmFeatureId,
                             divCode = hdr.RdmDivCode,
                             rmuCode = hdr.RdmRmuCode,
                             rmuName = hdr.RdmRmuName,
                             secCode = hdr.RdmSecCode,
                             secName = hdr.RdmSecName,
                             rdCatgCode = hdr.RdmRdCatgCode,
                             rdCatgName = hdr.RdmRdCatgName,
                             rdCode = hdr.RdmRdCode,
                             rdName = hdr.RdmRdName,
                             frmLoc = hdr.RdmFrmLoc,
                             toLoc = hdr.RdmToLoc,
                             frmCh = hdr.RdmFrmCh,
                             frmChDeci = hdr.RdmFrmChDeci,
                             toCh = hdr.RdmToCh,
                             toChDeci = hdr.RdmToChDeci,
                             lengthPaved = hdr.RdmLengthPaved,
                             lengthUnpaved = hdr.RdmLengthUnpaved,
                             owner = hdr.RdmOwner,
                             crDt = hdr.RdmCrDt,
                             modDt = hdr.RdmModDt,
                             crBy = hdr.RdmCrBy,
                             modBy = hdr.RdmModBy,
                         
                         });
            if (filterOptions.filter != null)
            {
                foreach (var item in filterOptions.filter.Where(x => !string.IsNullOrEmpty(x.Value)))
                {
                    string strVal = Utility.ToString(item.Value).Trim();
                    switch (item.Key)
                    {
                        case "KeySearch":
                            DateTime? dtSearch = Utility.ToDateTime(strVal);
                            query = query.Where(x =>
                                 (x.divCode ?? "").Contains(strVal)
                                 || (x.featureId ?? "").Contains(strVal)
                                 || (x.rmuCode ?? "").Contains(strVal)
                                 || (x.rmuName  ?? "").Contains(strVal)
                                 || (x.divCode ?? "").Contains(strVal)
                                 || (x.crBy ?? "").Contains(strVal)
                                 || (x.modBy ?? "").Contains(strVal)
                                 || (x.secName ?? "").Contains(strVal)
                                 || (x.secCode.HasValue ? x.secCode.Value.ToString() : "").Contains(strVal)
                                 || (x.rdCode ?? "").Contains(strVal)
                                 || (x.rdCatgCode ?? "").Contains(strVal)
                                 || (x.frmLoc ?? "").Contains(strVal)
                                 || (x.toLoc ?? "").Contains(strVal)
                                 || (x.owner ?? "").Contains(strVal)
                                 || (x.frmCh.HasValue ? x.frmCh.Value.ToString() : "").Contains(strVal)
                                 || (x.frmChDeci.HasValue ? x.frmChDeci.Value.ToString() : "").Contains(strVal)
                                 || (x.toCh.HasValue ? x.toCh.Value.ToString() : "").Contains(strVal)
                                 || (x.toChDeci.HasValue ? x.toChDeci.Value.ToString() : "").Contains(strVal)
                                 || (x.lengthPaved.HasValue ? x.lengthPaved.Value.ToString() : "").Contains(strVal)
                                 || (x.lengthUnpaved.HasValue ? x.lengthUnpaved.Value.ToString() : "").Contains(strVal)
                                 );
                            break;
                    }
                }
            }
            GridWrapper<object> grid = new GridWrapper<object>();
            grid.recordsTotal = await query.CountAsync();
            grid.recordsFiltered = grid.recordsTotal;
            grid.draw = filterOptions.draw;
            grid.data = await query.Order(filterOptions, query.OrderBy(s => s.pkRefNo)).Skip(filterOptions.start)
                                .Take(filterOptions.length)
                                .ToListAsync(); ;

            return grid;

        }
    }
}
