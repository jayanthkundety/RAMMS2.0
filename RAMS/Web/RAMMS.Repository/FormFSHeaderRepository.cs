using System;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using RAMMS.DTO.Report;

namespace RAMMS.Repository
{
    public class FormFSHeaderRepository : RepositoryBase<RmFormFsInsHdr>, IFormFSHeaderRepository
    {
        public FormFSHeaderRepository(RAMMSContext context) : base(context) { _context = context ?? throw new ArgumentNullException(nameof(context)); }
        public async Task<long> GetFilteredRecordCount(FilteredPagingDefinition<FormFSHeaderRequestDTO> filterOptions)
        {
            var query = (from s in _context.RmFormFsInsHdr
                         where s.FshActiveYn == true
                         select s);
            var search = filterOptions.Filters;
            if (search.SecCode != 0)
            {
                query = query.Where(s => s.FshRoad.RdmSecCode == search.SecCode);
            }
            if (!string.IsNullOrEmpty(search.RmuCode))
            {
                query = query.Where(s => s.FshRmuName == search.RmuCode);
            }
            if (!string.IsNullOrEmpty(search.RoadCode))
            {
                query = query.Where(s => s.FshRoad.RdmRdCode == search.RoadCode);
            }
            if (search.FromYear != 0)
            {
                query = query.Where(s => s.FshYearOfInsp >= search.FromYear);
            }
            if (search.ToYear != 0)
            {
                query = query.Where(s => s.FshYearOfInsp <= search.ToYear);
            }
            if (search.SecCode != 0)
            {
                query = query.Where(s => s.FshRoad.RdmSecCode == search.SecCode);
            }
            if (!string.IsNullOrEmpty(search.RmuCode))
            {
                query = query.Where(s => s.FshRmuName == search.RmuCode);
            }
            if (!string.IsNullOrEmpty(search.RoadCode))
            {
                query = query.Where(s => s.FshRoad.RdmRdCode == search.RoadCode);
            }
            if (search.FromYear != 0)
            {
                query = query.Where(s => s.FshYearOfInsp >= search.FromYear);
            }
            if (search.ToYear != 0)
            {
                query = query.Where(s => s.FshYearOfInsp <= search.ToYear);
            }

            if (!string.IsNullOrEmpty(search.SmartSearch))
            {
                query = query.Where(s =>
                s.FshYearOfInsp.ToString().Contains(search.SmartSearch)
                || (s.FshRoadLength.HasValue ? s.FshRoadLength.Value.ToString() : "").Contains(search.SmartSearch)
                || s.FshRoad.RdmRdCode.Contains(search.SmartSearch)
                || s.FshRoad.RdmRdName.Contains(search.SmartSearch)
                || s.FshCrewLeaderName.Contains(search.SmartSearch)
                || (s.FshSubmitSts ? "Submitted" : "Saved").Contains(search.SmartSearch)
                || s.FshRoad.RdmRmuName.Contains(search.SmartSearch)
                || s.FshRmuName.Contains(search.SmartSearch)
                || s.FshRoad.RdmSecName.Contains(search.SmartSearch));
            }
            return await query.LongCountAsync();
        }
        public async Task<List<FormFSHeaderRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormFSHeaderRequestDTO> filterOptions)
        {
            var query = (from s in _context.RmFormFsInsHdr
                         where s.FshActiveYn == true
                         select s);
            var search = filterOptions.Filters;
            if (search.SecCode != 0)
            {
                query = query.Where(s => s.FshRoad.RdmSecCode == search.SecCode);
            }
            if (!string.IsNullOrEmpty(search.RmuCode))
            {
                query = query.Where(s => s.FshRmuName == search.RmuCode);
            }
            if (!string.IsNullOrEmpty(search.RoadCode))
            {
                query = query.Where(s => s.FshRoad.RdmRdCode == search.RoadCode);
            }
            if (search.FromYear != 0)
            {
                query = query.Where(s => s.FshYearOfInsp >= search.FromYear);
            }
            if (search.ToYear != 0)
            {
                query = query.Where(s => s.FshYearOfInsp <= search.ToYear);
            }

            if (!string.IsNullOrEmpty(search.SmartSearch))
            {
                query = query.Where(s =>
               s.FshYearOfInsp.ToString().Contains(search.SmartSearch)
               || (s.FshRoadLength.HasValue ? s.FshRoadLength.Value.ToString() : "").Contains(search.SmartSearch)
               || s.FshRoad.RdmRdCode.Contains(search.SmartSearch)
               || s.FshRoad.RdmRdName.Contains(search.SmartSearch)
               || s.FshCrewLeaderName.Contains(search.SmartSearch)
               || (s.FshSubmitSts ? "Submitted" : "Saved").Contains(search.SmartSearch)
               || s.FshRoad.RdmRmuName.Contains(search.SmartSearch)
               || s.FshRmuName.Contains(search.SmartSearch)
               || s.FshRoad.RdmSecName.Contains(search.SmartSearch));
            }

            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 0)
                { query = query.OrderByDescending(s => s.FshModDt); }
                if (filterOptions.ColumnIndex == 2) { query = query.OrderBy(s => s.FshDivCode); }
                if (filterOptions.ColumnIndex == 3) { query = query.OrderBy(s => s.FshDist); }
                if (filterOptions.ColumnIndex == 4) { query = query.OrderBy(s => s.FshRmuName); }
                if (filterOptions.ColumnIndex == 5) { query = query.OrderBy(s => s.FshRoadId); }
                if (filterOptions.ColumnIndex == 6) { query = query.OrderBy(s => s.FshRoad.RdmRdCdSort); }
                if (filterOptions.ColumnIndex == 7) { query = query.OrderBy(s => s.FshRoadName); }
                if (filterOptions.ColumnIndex == 8) { query = query.OrderBy(s => s.FshRoadLength); }
                if (filterOptions.ColumnIndex == 9) { query = query.OrderBy(s => s.FshYearOfInsp); }
                if (filterOptions.ColumnIndex == 10) { query = query.OrderBy(s => s.FshUserIdInspBy); }
                if (filterOptions.ColumnIndex == 11) { query = query.OrderBy(s => s.FshUserNameInspBy); }
                if (filterOptions.ColumnIndex == 12) { query = query.OrderBy(s => s.FshUserDesignationInspY); }
                if (filterOptions.ColumnIndex == 13) { query = query.OrderBy(s => s.FshDtInspBy); }
                if (filterOptions.ColumnIndex == 14) { query = query.OrderBy(s => s.FshSignpathInspBy); }
                if (filterOptions.ColumnIndex == 15) { query = query.OrderBy(s => s.FshFormRefId); }
                if (filterOptions.ColumnIndex == 16) { query = query.OrderBy(s => s.FshCrewLeaderId); }
                if (filterOptions.ColumnIndex == 17) { query = query.OrderBy(s => s.FshCrewLeaderName); }
                if (filterOptions.ColumnIndex == 18) { query = query.OrderBy(s => s.FshUserIdSmzdBy); }
                if (filterOptions.ColumnIndex == 19) { query = query.OrderBy(s => s.FshUserNameSmzdBy); }
                if (filterOptions.ColumnIndex == 20) { query = query.OrderBy(s => s.FshUserDesignationSmzdY); }
                if (filterOptions.ColumnIndex == 21) { query = query.OrderBy(s => s.FshDtSmzdBy); }
                if (filterOptions.ColumnIndex == 22) { query = query.OrderBy(s => s.FshSignpathSmzdBy); }
                if (filterOptions.ColumnIndex == 23) { query = query.OrderBy(s => s.FshUserIdChckdBy); }
                if (filterOptions.ColumnIndex == 24) { query = query.OrderBy(s => s.FshUserNameChckdBy); }
                if (filterOptions.ColumnIndex == 25) { query = query.OrderBy(s => s.FshUserDesignationChckdBy); }
                if (filterOptions.ColumnIndex == 26) { query = query.OrderBy(s => s.FshDtChckdBy); }
                if (filterOptions.ColumnIndex == 27) { query = query.OrderBy(s => s.FshSignpathChckdBy); }
                if (filterOptions.ColumnIndex == 28) { query = query.OrderBy(s => s.FshModBy); }
                if (filterOptions.ColumnIndex == 29) { query = query.OrderBy(s => s.FshModDt); }
                if (filterOptions.ColumnIndex == 30) { query = query.OrderBy(s => s.FshCrBy); }
                if (filterOptions.ColumnIndex == 31) { query = query.OrderBy(s => s.FshCrDt); }
                if (filterOptions.ColumnIndex == 32) { query = query.OrderBy(s => s.FshSubmitSts); }
                if (filterOptions.ColumnIndex == 33) { query = query.OrderBy(s => s.FshActiveYn); }
            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 1) { query = query.OrderByDescending(s => s.FshModDt); }
                if (filterOptions.ColumnIndex == 2) { query = query.OrderByDescending(s => s.FshDivCode); }
                if (filterOptions.ColumnIndex == 3) { query = query.OrderByDescending(s => s.FshDist); }
                if (filterOptions.ColumnIndex == 4) { query = query.OrderByDescending(s => s.FshRmuName); }
                if (filterOptions.ColumnIndex == 5) { query = query.OrderByDescending(s => s.FshRoadId); }
                if (filterOptions.ColumnIndex == 6) { query = query.OrderByDescending(s => s.FshRoad.RdmRdCdSort); }
                if (filterOptions.ColumnIndex == 7) { query = query.OrderByDescending(s => s.FshRoadName); }
                if (filterOptions.ColumnIndex == 8) { query = query.OrderByDescending(s => s.FshRoadLength); }
                if (filterOptions.ColumnIndex == 9) { query = query.OrderByDescending(s => s.FshYearOfInsp); }
                if (filterOptions.ColumnIndex == 10) { query = query.OrderByDescending(s => s.FshUserIdInspBy); }
                if (filterOptions.ColumnIndex == 11) { query = query.OrderByDescending(s => s.FshUserNameInspBy); }
                if (filterOptions.ColumnIndex == 12) { query = query.OrderByDescending(s => s.FshUserDesignationInspY); }
                if (filterOptions.ColumnIndex == 13) { query = query.OrderByDescending(s => s.FshDtInspBy); }
                if (filterOptions.ColumnIndex == 14) { query = query.OrderByDescending(s => s.FshSignpathInspBy); }
                if (filterOptions.ColumnIndex == 15) { query = query.OrderByDescending(s => s.FshFormRefId); }
                if (filterOptions.ColumnIndex == 16) { query = query.OrderByDescending(s => s.FshCrewLeaderId); }
                if (filterOptions.ColumnIndex == 17) { query = query.OrderByDescending(s => s.FshCrewLeaderName); }
                if (filterOptions.ColumnIndex == 18) { query = query.OrderByDescending(s => s.FshUserIdSmzdBy); }
                if (filterOptions.ColumnIndex == 19) { query = query.OrderByDescending(s => s.FshUserNameSmzdBy); }
                if (filterOptions.ColumnIndex == 20) { query = query.OrderByDescending(s => s.FshUserDesignationSmzdY); }
                if (filterOptions.ColumnIndex == 21) { query = query.OrderByDescending(s => s.FshDtSmzdBy); }
                if (filterOptions.ColumnIndex == 22) { query = query.OrderByDescending(s => s.FshSignpathSmzdBy); }
                if (filterOptions.ColumnIndex == 23) { query = query.OrderByDescending(s => s.FshUserIdChckdBy); }
                if (filterOptions.ColumnIndex == 24) { query = query.OrderByDescending(s => s.FshUserNameChckdBy); }
                if (filterOptions.ColumnIndex == 25) { query = query.OrderByDescending(s => s.FshUserDesignationChckdBy); }
                if (filterOptions.ColumnIndex == 26) { query = query.OrderByDescending(s => s.FshDtChckdBy); }
                if (filterOptions.ColumnIndex == 27) { query = query.OrderByDescending(s => s.FshSignpathChckdBy); }
                if (filterOptions.ColumnIndex == 28) { query = query.OrderByDescending(s => s.FshModBy); }
                if (filterOptions.ColumnIndex == 29) { query = query.OrderByDescending(s => s.FshModDt); }
                if (filterOptions.ColumnIndex == 30) { query = query.OrderByDescending(s => s.FshCrBy); }
                if (filterOptions.ColumnIndex == 31) { query = query.OrderByDescending(s => s.FshCrDt); }
                if (filterOptions.ColumnIndex == 32) { query = query.OrderByDescending(s => s.FshSubmitSts); }
                if (filterOptions.ColumnIndex == 33) { query = query.OrderByDescending(s => s.FshActiveYn); }
            }
            var lst = await query.Skip(filterOptions.StartPageNo).Take(filterOptions.RecordsPerPage).ToListAsync();
            return lst.Select(s => new FormFSHeaderRequestDTO
            {
                PkRefNo = s.FshPkRefNo,
                DivCode = s.FshDivCode,
                Dist = s.FshDist,
                RmuName = s.FshRmuName,
                RoadId = s.FshRoadId,
                RoadCode = s.FshRoadCode,
                RoadName = s.FshRoadName,
                RoadLength = s.FshRoadLength,
                YearOfInsp = s.FshYearOfInsp,
                UserIdInspBy = s.FshUserIdInspBy,
                UserNameInspBy = s.FshUserNameInspBy,
                UserDesignationInspY = s.FshUserDesignationInspY,
                DtInspBy = s.FshDtInspBy,
                SignpathInspBy = s.FshSignpathInspBy,
                FormRefId = s.FshFormRefId,
                CrewLeaderId = s.FshCrewLeaderId,
                CrewLeaderName = s.FshCrewLeaderName,
                UserIdSmzdBy = s.FshUserIdSmzdBy,
                UserNameSmzdBy = s.FshUserNameSmzdBy,
                UserDesignationSmzdY = s.FshUserDesignationSmzdY,
                DtSmzdBy = s.FshDtSmzdBy,
                SignpathSmzdBy = s.FshSignpathSmzdBy,
                UserIdChckdBy = s.FshUserIdChckdBy,
                UserNameChckdBy = s.FshUserNameChckdBy,
                UserDesignationChckdBy = s.FshUserDesignationChckdBy,
                DtChckdBy = s.FshDtChckdBy,
                SignpathChckdBy = s.FshSignpathChckdBy,
                ModBy = s.FshModBy,
                ModDt = s.FshModDt,
                CrBy = s.FshCrBy,
                CrDt = s.FshCrDt,
                SubmitSts = s.FshSubmitSts,
                ActiveYn = s.FshActiveYn.Value,
            }).ToList();
        }

        public FormFSRpt GetReportData(int headerid)
        {
            Func<string, string, FormFSDetailRpt> dRpt = (type, code) =>
              {
                  return (from o in _context.RmFormFsInsDtl
                          where o.FsdFshPkRefNo == headerid
                         && o.FsdGrpType == type && o.FsdGrpCode == code
                          select new FormFSDetailRpt
                          {
                              AverageWidth = o.FsdWidth.HasValue && o.FsdWidth != 0 ? o.FsdWidth.Value : (double?)null,
                              TotalLength = o.FsdLength.HasValue && o.FsdLength != 0 ? o.FsdLength.Value : (double?)null,
                              Condition1 = o.FsdCondition1.HasValue && o.FsdCondition1 != 0 ? o.FsdCondition1.Value : (decimal?)null,
                              Condition2 = o.FsdCondition2.HasValue && o.FsdCondition2 != 0 ? o.FsdCondition2.Value : (decimal?)null,
                              Condition3 = o.FsdCondition3.HasValue && o.FsdCondition3 != 0 ? o.FsdCondition3.Value : (decimal?)null,
                              Needed = o.FsdNeeded,
                              Remarks = o.FsdRemarks
                          }).FirstOrDefault() ?? new FormFSDetailRpt();
              };
            FormFSRpt rpt = (from o in _context.RmFormFsInsHdr
                             where o.FshPkRefNo == headerid
                             select new FormFSRpt
                             {
                                 RMU = o.FshRmuName,
                                 District = o.FshDist,
                                 RoadCode = o.FshRoadCode,
                                 RoadName = o.FshRoadName,
                                 Division = o.FshRoad.RdmDivCode,
                                 DateOfInspection = o.FshDtInspBy,
                                 SummarizedBy = o.FshUserNameSmzdBy,
                                 CheckedBy = o.FshUserNameChckdBy,
                                 CrewLeader = o.FshCrewLeaderName
                             }).FirstOrDefault();

            rpt.CWAsphaltic = dRpt("Asphalt", "CW");
            rpt.CWConcrete = dRpt("Concrete", "CW");
            rpt.CWEarth = dRpt("Earth", "CW");
            rpt.CWGravel = dRpt("Gravel", "CW");
            rpt.CWSand = dRpt("Sand", "CW");
            rpt.CWSurfaceDressed = dRpt("Surface Dressed", "CW");
            rpt.CLMPaint = dRpt("Paint", "CLM");
            rpt.CLMThermoplastic = dRpt("Thermoplastic", "CLM");
            rpt.LELMPaint = dRpt("Paint", "ELM_L");
            rpt.LELMThermoplastic = dRpt("Thermoplastic", "ELM_L");
            rpt.LDitchGravel = dRpt("Gravel/Sand/Earth", "DI_L");
            rpt.LDrainEarth = dRpt("Earth", "DR_L");
            rpt.LDrainBlockstone = dRpt("Block Stone", "DR_L");
            rpt.LDrainConcreate = dRpt("Concrete", "DR_L");
            rpt.LShoulderAsphalt = dRpt("Asphalt", "SH_L");
            rpt.LShoulderConcrete = dRpt("Concrete", "SH_L");
            rpt.LShoulderEarth = dRpt("Earth", "SH_L");
            rpt.LShoulderGravel = dRpt("Gravel", "SH_L");
            rpt.LShoulderFootpathkerb = dRpt("Footpath/Kerb", "SH_L");
            rpt.RELMPaint = dRpt("Paint", "ELM_R");
            rpt.RELMThermoplastic = dRpt("Thermoplastic", "ELM_R");
            rpt.RDitchGravel = dRpt("Gravel/Sand/Earth", "DI_R");
            rpt.RDrainEarth = dRpt("Earth", "DR_R");
            rpt.RDrainBlockstone = dRpt("Block Stone", "DR_R");
            rpt.RDrainConcreate = dRpt("Concrete", "DR_R");
            rpt.RShoulderAsphalt = dRpt("Asphalt", "SH_R");
            rpt.RShoulderConcrete = dRpt("Concrete", "SH_R");
            rpt.RShoulderEarth = dRpt("Earth", "SH_R");
            rpt.RShoulderGravel = dRpt("Gravel", "SH_R");
            rpt.RShoulderFootpathkerb = dRpt("Footpath/Kerb", "SH_R");

            rpt.RSLeft = dRpt("Left", "R_L");
            rpt.RSCenter = dRpt("Centre", "R_C");
            rpt.RSRight = dRpt("Right", "R_R");

            rpt.SignsDelineator = dRpt("Delineator", "SG");
            rpt.SignsWarning = dRpt("Warning", "SG");
            rpt.SignsGantrySign = dRpt("Gantry Sign", "SG");
            rpt.SignsGuideSign = dRpt("Guide Sign", "SG");

            rpt.CVConcreatePipe = dRpt("Concrete Pipe", "CP");
            rpt.CVConcreteBox = dRpt("Concrete Box", "CB");
            rpt.CVMetal = dRpt("Metal", "M");
            rpt.CVHDPE = dRpt("HDPE", "H");
            rpt.CVOthers = dRpt("Others", "O");

            rpt.BRConcConc = dRpt("Concrete - Concrete", "CC");
            rpt.BRConcSteel = dRpt("Concrete - Steel", "CS");
            rpt.BRSteelTimber = dRpt("Steel -Timber", "ST");
            rpt.BRSteelSteel = dRpt("Steel - Steel", "SS");
            rpt.BRTimberTimber = dRpt("Timber - Timber", "TT");
            rpt.BRTimberSteel = dRpt("Timber - Steel", "TS");
            rpt.BRMansonry = dRpt("Masonry", "M");
            rpt.BRElevatedViaduct = dRpt("Elevated Viaduct", "EV");
            rpt.BRLongBridge = dRpt("Long Bridge > 100m", "LB");

            rpt.GRSteel = dRpt("Steel", "S");
            rpt.GRWire = dRpt("Wire", "W");
            rpt.GRPedestrialRailing = dRpt("Pedestrian Railing", "R");
            rpt.GRParapetWall = dRpt("Parapet Wall", "PW");
            rpt.GROthers = dRpt("Others", "O");

            rpt.RWReinforceConc = dRpt("Reinforced Concrete", "RW");
            rpt.RWSteelMetal = dRpt("Steel/Metal", "RW");
            rpt.RWMasonryGabion = dRpt("Masonry/Gabion", "RW");
            rpt.RWPrecastPanel = dRpt("Precast Panel", "RW");
            rpt.RWTimber = dRpt("Timber", "RW");
            rpt.RWSoliNail = dRpt("Soil Nail", "RW");
            rpt.RWOthers = dRpt("Others", "RW");
            return rpt;
        }

    }
}
