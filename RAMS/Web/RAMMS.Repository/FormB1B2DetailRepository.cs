using System;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RAMMS.DTO.RequestBO;
using System.Collections.Generic;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Repository
{
    public class FormB1B2DetailRepository : RepositoryBase<RmFormB1b2BrInsDtl>, IFormB1B2DetailRepository
    {
        public FormB1B2DetailRepository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<long> GetFilteredRecordCount(FilteredPagingDefinition<FormB1B2DetailRequestDTO> filterOptions)
        {
            return await (from s in _context.RmFormB1b2BrInsDtl where s.FbridActiveYn == true select s).LongCountAsync();
        }
        public async Task<List<FormB1B2DetailRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormB1B2DetailRequestDTO> filterOptions)
        {
            var query = (from s in _context.RmFormB1b2BrInsDtl where s.FbridActiveYn == true select s);
            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 1) { query = query.OrderBy(s => s.FbridPkRefNo); }
                if (filterOptions.ColumnIndex == 2) { query = query.OrderBy(s => s.FbridFbrihPkRefNo); }
                if (filterOptions.ColumnIndex == 3) { query = query.OrderBy(s => s.FbridAbutFoundMat); }
                if (filterOptions.ColumnIndex == 4) { query = query.OrderBy(s => s.FbridAbutFoundMatCode); }
                if (filterOptions.ColumnIndex == 5) { query = query.OrderBy(s => s.FbridAbutFoundInspCode); }
                if (filterOptions.ColumnIndex == 6) { query = query.OrderBy(s => s.FbridAbutFoundDistress); }
                if (filterOptions.ColumnIndex == 7) { query = query.OrderBy(s => s.FbridAbutFoundSeverity); }
                if (filterOptions.ColumnIndex == 8) { query = query.OrderBy(s => s.FbridPiersPrimCompMat); }
                if (filterOptions.ColumnIndex == 9) { query = query.OrderBy(s => s.FbridPiersPrimCompMatCode); }
                if (filterOptions.ColumnIndex == 10) { query = query.OrderBy(s => s.FbridPiersPrimCompInspCode); }
                if (filterOptions.ColumnIndex == 11) { query = query.OrderBy(s => s.FbridPiersPrimCompDistress); }
                if (filterOptions.ColumnIndex == 12) { query = query.OrderBy(s => s.FbridPiersPrimCompSeverity); }
                if (filterOptions.ColumnIndex == 13) { query = query.OrderBy(s => s.FbridBearingStDiaphgMat); }
                if (filterOptions.ColumnIndex == 14) { query = query.OrderBy(s => s.FbridBearingStDiaphgMatCode); }
                if (filterOptions.ColumnIndex == 15) { query = query.OrderBy(s => s.FbridBearingStDiaphgInspCode); }
                if (filterOptions.ColumnIndex == 16) { query = query.OrderBy(s => s.FbridBearingStDiaphgDistress); }
                if (filterOptions.ColumnIndex == 17) { query = query.OrderBy(s => s.FbridBearingStDiaphgSeverity); }
                if (filterOptions.ColumnIndex == 18) { query = query.OrderBy(s => s.FbridBeamsGridTrusArch); }
                if (filterOptions.ColumnIndex == 19) { query = query.OrderBy(s => s.FbridBeamsGridTrusArchCode); }
                if (filterOptions.ColumnIndex == 20) { query = query.OrderBy(s => s.FbridBeamsGridTrusArchInspCode); }
                if (filterOptions.ColumnIndex == 21) { query = query.OrderBy(s => s.FbridBeamsGridTrusArchDistress); }
                if (filterOptions.ColumnIndex == 22) { query = query.OrderBy(s => s.FbridBeamsGridTrusArchSeverity); }
                if (filterOptions.ColumnIndex == 23) { query = query.OrderBy(s => s.FbridDeckPavement); }
                if (filterOptions.ColumnIndex == 24) { query = query.OrderBy(s => s.FbridDeckPavementCode); }
                if (filterOptions.ColumnIndex == 25) { query = query.OrderBy(s => s.FbridDeckPavementCodeInspCode); }
                if (filterOptions.ColumnIndex == 26) { query = query.OrderBy(s => s.FbridDeckPavementDistress); }
                if (filterOptions.ColumnIndex == 27) { query = query.OrderBy(s => s.FbridDeckPavementSeverity); }
                if (filterOptions.ColumnIndex == 28) { query = query.OrderBy(s => s.FbridUtilities); }
                if (filterOptions.ColumnIndex == 29) { query = query.OrderBy(s => s.FbridUtilitiesCode); }
                if (filterOptions.ColumnIndex == 30) { query = query.OrderBy(s => s.FbridUtilitiesInspCode); }
                if (filterOptions.ColumnIndex == 31) { query = query.OrderBy(s => s.FbridUtilitiesDistress); }
                if (filterOptions.ColumnIndex == 32) { query = query.OrderBy(s => s.FbridUtilitiesSeverity); }
                if (filterOptions.ColumnIndex == 33) { query = query.OrderBy(s => s.FbridWaterway); }
                if (filterOptions.ColumnIndex == 34) { query = query.OrderBy(s => s.FbridWaterwayCode); }
                if (filterOptions.ColumnIndex == 35) { query = query.OrderBy(s => s.FbridWaterwayInspCode); }
                if (filterOptions.ColumnIndex == 36) { query = query.OrderBy(s => s.FbridWaterwayDistress); }
                if (filterOptions.ColumnIndex == 37) { query = query.OrderBy(s => s.FbridWaterwaySeverity); }
                if (filterOptions.ColumnIndex == 38) { query = query.OrderBy(s => s.FbridWaterDownpipe); }
                if (filterOptions.ColumnIndex == 39) { query = query.OrderBy(s => s.FbridWaterDownpipeCode); }
                if (filterOptions.ColumnIndex == 40) { query = query.OrderBy(s => s.FbridWaterDownpipeInspCode); }
                if (filterOptions.ColumnIndex == 41) { query = query.OrderBy(s => s.FbridWaterDownpipeDistress); }
                if (filterOptions.ColumnIndex == 42) { query = query.OrderBy(s => s.FbridWaterDownpipeSeverity); }
                if (filterOptions.ColumnIndex == 43) { query = query.OrderBy(s => s.FbridParapetRailing); }
                if (filterOptions.ColumnIndex == 44) { query = query.OrderBy(s => s.FbridParapetRailingCode); }
                if (filterOptions.ColumnIndex == 45) { query = query.OrderBy(s => s.FbridParapetRailingInspCode); }
                if (filterOptions.ColumnIndex == 46) { query = query.OrderBy(s => s.FbridParapetRailingDistress); }
                if (filterOptions.ColumnIndex == 47) { query = query.OrderBy(s => s.FbridParapetRailingSeverity); }
                if (filterOptions.ColumnIndex == 48) { query = query.OrderBy(s => s.FbridSidewalksAppSlab); }
                if (filterOptions.ColumnIndex == 49) { query = query.OrderBy(s => s.FbridSidewalksAppSlabCode); }
                if (filterOptions.ColumnIndex == 50) { query = query.OrderBy(s => s.FbridSidewalksAppSlabInspCode); }
                if (filterOptions.ColumnIndex == 51) { query = query.OrderBy(s => s.FbridSidewalksAppSlabDistress); }
                if (filterOptions.ColumnIndex == 52) { query = query.OrderBy(s => s.FbridSidewalksAppSlabSeverity); }
                if (filterOptions.ColumnIndex == 53) { query = query.OrderBy(s => s.FbridExpanJoint); }
                if (filterOptions.ColumnIndex == 54) { query = query.OrderBy(s => s.FbridExpanJointCode); }
                if (filterOptions.ColumnIndex == 55) { query = query.OrderBy(s => s.FbridExpanJointInspCode); }
                if (filterOptions.ColumnIndex == 56) { query = query.OrderBy(s => s.FbridExpanJointDistress); }
                if (filterOptions.ColumnIndex == 57) { query = query.OrderBy(s => s.FbridExpanJointSeverity); }
                if (filterOptions.ColumnIndex == 58) { query = query.OrderBy(s => s.FbridSlopeRetainWall); }
                if (filterOptions.ColumnIndex == 59) { query = query.OrderBy(s => s.FbridSlopeRetainWallCode); }
                if (filterOptions.ColumnIndex == 60) { query = query.OrderBy(s => s.FbridSlopeRetainWallInspCode); }
                if (filterOptions.ColumnIndex == 61) { query = query.OrderBy(s => s.FbridSlopeRetainWallDistress); }
                if (filterOptions.ColumnIndex == 62) { query = query.OrderBy(s => s.FbridSlopeRetainWallSeverity); }
                if (filterOptions.ColumnIndex == 63) { query = query.OrderBy(s => s.FbridModBy); }
                if (filterOptions.ColumnIndex == 64) { query = query.OrderBy(s => s.FbridModDt); }
                if (filterOptions.ColumnIndex == 65) { query = query.OrderBy(s => s.FbridCrBy); }
                if (filterOptions.ColumnIndex == 66) { query = query.OrderBy(s => s.FbridCrDt); }
                if (filterOptions.ColumnIndex == 67) { query = query.OrderBy(s => s.FbridSubmitSts); }
                if (filterOptions.ColumnIndex == 68) { query = query.OrderBy(s => s.FbridActiveYn); }
            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 1) { query = query.OrderByDescending(s => s.FbridPkRefNo); }
                if (filterOptions.ColumnIndex == 2) { query = query.OrderByDescending(s => s.FbridFbrihPkRefNo); }
                if (filterOptions.ColumnIndex == 3) { query = query.OrderByDescending(s => s.FbridAbutFoundMat); }
                if (filterOptions.ColumnIndex == 4) { query = query.OrderByDescending(s => s.FbridAbutFoundMatCode); }
                if (filterOptions.ColumnIndex == 5) { query = query.OrderByDescending(s => s.FbridAbutFoundInspCode); }
                if (filterOptions.ColumnIndex == 6) { query = query.OrderByDescending(s => s.FbridAbutFoundDistress); }
                if (filterOptions.ColumnIndex == 7) { query = query.OrderByDescending(s => s.FbridAbutFoundSeverity); }
                if (filterOptions.ColumnIndex == 8) { query = query.OrderByDescending(s => s.FbridPiersPrimCompMat); }
                if (filterOptions.ColumnIndex == 9) { query = query.OrderByDescending(s => s.FbridPiersPrimCompMatCode); }
                if (filterOptions.ColumnIndex == 10) { query = query.OrderByDescending(s => s.FbridPiersPrimCompInspCode); }
                if (filterOptions.ColumnIndex == 11) { query = query.OrderByDescending(s => s.FbridPiersPrimCompDistress); }
                if (filterOptions.ColumnIndex == 12) { query = query.OrderByDescending(s => s.FbridPiersPrimCompSeverity); }
                if (filterOptions.ColumnIndex == 13) { query = query.OrderByDescending(s => s.FbridBearingStDiaphgMat); }
                if (filterOptions.ColumnIndex == 14) { query = query.OrderByDescending(s => s.FbridBearingStDiaphgMatCode); }
                if (filterOptions.ColumnIndex == 15) { query = query.OrderByDescending(s => s.FbridBearingStDiaphgInspCode); }
                if (filterOptions.ColumnIndex == 16) { query = query.OrderByDescending(s => s.FbridBearingStDiaphgDistress); }
                if (filterOptions.ColumnIndex == 17) { query = query.OrderByDescending(s => s.FbridBearingStDiaphgSeverity); }
                if (filterOptions.ColumnIndex == 18) { query = query.OrderByDescending(s => s.FbridBeamsGridTrusArch); }
                if (filterOptions.ColumnIndex == 19) { query = query.OrderByDescending(s => s.FbridBeamsGridTrusArchCode); }
                if (filterOptions.ColumnIndex == 20) { query = query.OrderByDescending(s => s.FbridBeamsGridTrusArchInspCode); }
                if (filterOptions.ColumnIndex == 21) { query = query.OrderByDescending(s => s.FbridBeamsGridTrusArchDistress); }
                if (filterOptions.ColumnIndex == 22) { query = query.OrderByDescending(s => s.FbridBeamsGridTrusArchSeverity); }
                if (filterOptions.ColumnIndex == 23) { query = query.OrderByDescending(s => s.FbridDeckPavement); }
                if (filterOptions.ColumnIndex == 24) { query = query.OrderByDescending(s => s.FbridDeckPavementCode); }
                if (filterOptions.ColumnIndex == 25) { query = query.OrderByDescending(s => s.FbridDeckPavementCodeInspCode); }
                if (filterOptions.ColumnIndex == 26) { query = query.OrderByDescending(s => s.FbridDeckPavementDistress); }
                if (filterOptions.ColumnIndex == 27) { query = query.OrderByDescending(s => s.FbridDeckPavementSeverity); }
                if (filterOptions.ColumnIndex == 28) { query = query.OrderByDescending(s => s.FbridUtilities); }
                if (filterOptions.ColumnIndex == 29) { query = query.OrderByDescending(s => s.FbridUtilitiesCode); }
                if (filterOptions.ColumnIndex == 30) { query = query.OrderByDescending(s => s.FbridUtilitiesInspCode); }
                if (filterOptions.ColumnIndex == 31) { query = query.OrderByDescending(s => s.FbridUtilitiesDistress); }
                if (filterOptions.ColumnIndex == 32) { query = query.OrderByDescending(s => s.FbridUtilitiesSeverity); }
                if (filterOptions.ColumnIndex == 33) { query = query.OrderByDescending(s => s.FbridWaterway); }
                if (filterOptions.ColumnIndex == 34) { query = query.OrderByDescending(s => s.FbridWaterwayCode); }
                if (filterOptions.ColumnIndex == 35) { query = query.OrderByDescending(s => s.FbridWaterwayInspCode); }
                if (filterOptions.ColumnIndex == 36) { query = query.OrderByDescending(s => s.FbridWaterwayDistress); }
                if (filterOptions.ColumnIndex == 37) { query = query.OrderByDescending(s => s.FbridWaterwaySeverity); }
                if (filterOptions.ColumnIndex == 38) { query = query.OrderByDescending(s => s.FbridWaterDownpipe); }
                if (filterOptions.ColumnIndex == 39) { query = query.OrderByDescending(s => s.FbridWaterDownpipeCode); }
                if (filterOptions.ColumnIndex == 40) { query = query.OrderByDescending(s => s.FbridWaterDownpipeInspCode); }
                if (filterOptions.ColumnIndex == 41) { query = query.OrderByDescending(s => s.FbridWaterDownpipeDistress); }
                if (filterOptions.ColumnIndex == 42) { query = query.OrderByDescending(s => s.FbridWaterDownpipeSeverity); }
                if (filterOptions.ColumnIndex == 43) { query = query.OrderByDescending(s => s.FbridParapetRailing); }
                if (filterOptions.ColumnIndex == 44) { query = query.OrderByDescending(s => s.FbridParapetRailingCode); }
                if (filterOptions.ColumnIndex == 45) { query = query.OrderByDescending(s => s.FbridParapetRailingInspCode); }
                if (filterOptions.ColumnIndex == 46) { query = query.OrderByDescending(s => s.FbridParapetRailingDistress); }
                if (filterOptions.ColumnIndex == 47) { query = query.OrderByDescending(s => s.FbridParapetRailingSeverity); }
                if (filterOptions.ColumnIndex == 48) { query = query.OrderByDescending(s => s.FbridSidewalksAppSlab); }
                if (filterOptions.ColumnIndex == 49) { query = query.OrderByDescending(s => s.FbridSidewalksAppSlabCode); }
                if (filterOptions.ColumnIndex == 50) { query = query.OrderByDescending(s => s.FbridSidewalksAppSlabInspCode); }
                if (filterOptions.ColumnIndex == 51) { query = query.OrderByDescending(s => s.FbridSidewalksAppSlabDistress); }
                if (filterOptions.ColumnIndex == 52) { query = query.OrderByDescending(s => s.FbridSidewalksAppSlabSeverity); }
                if (filterOptions.ColumnIndex == 53) { query = query.OrderByDescending(s => s.FbridExpanJoint); }
                if (filterOptions.ColumnIndex == 54) { query = query.OrderByDescending(s => s.FbridExpanJointCode); }
                if (filterOptions.ColumnIndex == 55) { query = query.OrderByDescending(s => s.FbridExpanJointInspCode); }
                if (filterOptions.ColumnIndex == 56) { query = query.OrderByDescending(s => s.FbridExpanJointDistress); }
                if (filterOptions.ColumnIndex == 57) { query = query.OrderByDescending(s => s.FbridExpanJointSeverity); }
                if (filterOptions.ColumnIndex == 58) { query = query.OrderByDescending(s => s.FbridSlopeRetainWall); }
                if (filterOptions.ColumnIndex == 59) { query = query.OrderByDescending(s => s.FbridSlopeRetainWallCode); }
                if (filterOptions.ColumnIndex == 60) { query = query.OrderByDescending(s => s.FbridSlopeRetainWallInspCode); }
                if (filterOptions.ColumnIndex == 61) { query = query.OrderByDescending(s => s.FbridSlopeRetainWallDistress); }
                if (filterOptions.ColumnIndex == 62) { query = query.OrderByDescending(s => s.FbridSlopeRetainWallSeverity); }
                if (filterOptions.ColumnIndex == 63) { query = query.OrderByDescending(s => s.FbridModBy); }
                if (filterOptions.ColumnIndex == 64) { query = query.OrderByDescending(s => s.FbridModDt); }
                if (filterOptions.ColumnIndex == 65) { query = query.OrderByDescending(s => s.FbridCrBy); }
                if (filterOptions.ColumnIndex == 66) { query = query.OrderByDescending(s => s.FbridCrDt); }
                if (filterOptions.ColumnIndex == 67) { query = query.OrderByDescending(s => s.FbridSubmitSts); }
                if (filterOptions.ColumnIndex == 68) { query = query.OrderByDescending(s => s.FbridActiveYn); }
            }
            var lst = await query.Skip(filterOptions.StartPageNo).Take(filterOptions.RecordsPerPage).ToListAsync(); return lst.Select(s => new FormB1B2DetailRequestDTO
            {
                PkRefNo = s.FbridPkRefNo,
                FbrihPkRefNo = s.FbridFbrihPkRefNo,
                AbutFoundMat = s.FbridAbutFoundMat,
                AbutFoundMatCode = s.FbridAbutFoundMatCode,
                AbutFoundInspCode = s.FbridAbutFoundInspCode,
                AbutFoundDistress = s.FbridAbutFoundDistress,
                AbutFoundSeverity = s.FbridAbutFoundSeverity,
                PiersPrimCompMat = s.FbridPiersPrimCompMat,
                PiersPrimCompMatCode = s.FbridPiersPrimCompMatCode,
                PiersPrimCompInspCode = s.FbridPiersPrimCompInspCode,
                PiersPrimCompDistress = s.FbridPiersPrimCompDistress,
                PiersPrimCompSeverity = s.FbridPiersPrimCompSeverity,
                BearingStDiaphgMat = s.FbridBearingStDiaphgMat,
                BearingStDiaphgMatCode = s.FbridBearingStDiaphgMatCode,
                BearingStDiaphgInspCode = s.FbridBearingStDiaphgInspCode,
                BearingStDiaphgDistress = s.FbridBearingStDiaphgDistress,
                BearingStDiaphgSeverity = s.FbridBearingStDiaphgSeverity,
                BeamsGridTrusArch = s.FbridBeamsGridTrusArch,
                BeamsGridTrusArchCode = s.FbridBeamsGridTrusArchCode,
                BeamsGridTrusArchInspCode = s.FbridBeamsGridTrusArchInspCode,
                BeamsGridTrusArchDistress = s.FbridBeamsGridTrusArchDistress,
                BeamsGridTrusArchSeverity = s.FbridBeamsGridTrusArchSeverity,
                DeckPavement = s.FbridDeckPavement,
                DeckPavementCode = s.FbridDeckPavementCode,
                DeckPavementCodeInspCode = s.FbridDeckPavementCodeInspCode,
                DeckPavementDistress = s.FbridDeckPavementDistress,
                DeckPavementSeverity = s.FbridDeckPavementSeverity,
                Utilities = s.FbridUtilities,
                UtilitiesCode = s.FbridUtilitiesCode,
                UtilitiesInspCode = s.FbridUtilitiesInspCode,
                UtilitiesDistress = s.FbridUtilitiesDistress,
                UtilitiesSeverity = s.FbridUtilitiesSeverity,
                Waterway = s.FbridWaterway,
                WaterwayCode = s.FbridWaterwayCode,
                WaterwayInspCode = s.FbridWaterwayInspCode,
                WaterwayDistress = s.FbridWaterwayDistress,
                WaterwaySeverity = s.FbridWaterwaySeverity,
                WaterDownpipe = s.FbridWaterDownpipe,
                WaterDownpipeCode = s.FbridWaterDownpipeCode,
                WaterDownpipeInspCode = s.FbridWaterDownpipeInspCode,
                WaterDownpipeDistress = s.FbridWaterDownpipeDistress,
                WaterDownpipeSeverity = s.FbridWaterDownpipeSeverity,
                ParapetRailing = s.FbridParapetRailing,
                ParapetRailingCode = s.FbridParapetRailingCode,
                ParapetRailingInspCode = s.FbridParapetRailingInspCode,
                ParapetRailingDistress = s.FbridParapetRailingDistress,
                ParapetRailingSeverity = s.FbridParapetRailingSeverity,
                SidewalksAppSlab = s.FbridSidewalksAppSlab,
                SidewalksAppSlabCode = s.FbridSidewalksAppSlabCode,
                SidewalksAppSlabInspCode = s.FbridSidewalksAppSlabInspCode,
                SidewalksAppSlabDistress = s.FbridSidewalksAppSlabDistress,
                SidewalksAppSlabSeverity = s.FbridSidewalksAppSlabSeverity,
                ExpanJoint = s.FbridExpanJoint,
                ExpanJointCode = s.FbridExpanJointCode,
                ExpanJointInspCode = s.FbridExpanJointInspCode,
                ExpanJointDistress = s.FbridExpanJointDistress,
                ExpanJointSeverity = s.FbridExpanJointSeverity,
                SlopeRetainWall = s.FbridSlopeRetainWall,
                SlopeRetainWallCode = s.FbridSlopeRetainWallCode,
                SlopeRetainWallInspCode = s.FbridSlopeRetainWallInspCode,
                SlopeRetainWallDistress = s.FbridSlopeRetainWallDistress,
                SlopeRetainWallSeverity = s.FbridSlopeRetainWallSeverity,
                ModBy = s.FbridModBy,
                ModDt = s.FbridModDt,
                CrBy = s.FbridCrBy,
                CrDt = s.FbridCrDt,
                SubmitSts = s.FbridSubmitSts,
                ActiveYn = s.FbridActiveYn.Value,
            }).ToList();
        }
    }
}
