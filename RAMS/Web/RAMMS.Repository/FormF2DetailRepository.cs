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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RAMMS.Repository
{
    public class FormF2DetailRepository : RepositoryBase<RmFormF2GrInsDtl>, IFormF2DetailRepository
    {
        public FormF2DetailRepository(RAMMSContext context) : base(context) { _context = context ?? throw new ArgumentNullException(nameof(context)); }



        public async Task<long> GetFilteredRecordCount(FilteredPagingDefinition<FormF2DetailRequestDTO> filterOptions)
        {
            return await (from s in _context.RmFormF2GrInsDtl where s.FgridFgrihPkRefNo == filterOptions.Filters.FgrihPkRefNo && s.FgridActiveYn == true select s).LongCountAsync();
        }
        public async Task<List<FormF2DetailRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormF2DetailRequestDTO> filterOptions)
        {
            var query = from s in _context.RmFormF2GrInsDtl
                        join a in _context.RmAllassetInventory on s.FgrihAiPkRefNo equals a.AiPkRefNo
                        where s.FgridFgrihPkRefNo == filterOptions.Filters.FgrihPkRefNo
                        && s.FgridActiveYn == true
                        select new { s, a }; //.Skip(filterOptions.StartPageNo).Take(filterOptions.RecordsPerPage).ToListAsync();
            query = query.OrderBy(x => x.s.FgridStartingChKm).ThenBy(x => x.s.FgridStartingChM);

            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(s => s.a.AiAssetId);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(s => s.s.FgridStartingChKm).ThenBy(s => s.s.FgridStartingChM);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(s => s.a.AiLength);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(s => s.s.FgridRhsMLhs);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(s => s.s.FgridGrCondition1);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(s => s.s.FgridGrCondition2);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderBy(s => s.s.FgridGrCondition3);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderBy(s => s.a.AiBound);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderBy(s => s.s.FgridPostSpac);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderBy(s => s.s.FgridRemarks);

                //if (filterOptions.ColumnIndex == 0)
                //    query = query.OrderByDescending(s => s.s.FgrihPkRefNo);
            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(s => s.a.AiAssetId);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(s => s.s.FgridStartingChKm).ThenByDescending(s => s.s.FgridStartingChM);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(s => s.a.AiLength);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(s => s.s.FgridRhsMLhs);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(s => s.s.FgridGrCondition1);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(s => s.s.FgridGrCondition2);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderByDescending(s => s.s.FgridGrCondition3);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderByDescending(s => s.a.AiBound);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderByDescending(s => s.s.FgridPostSpac);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderByDescending(s => s.s.FgridRemarks);
                //if (filterOptions.ColumnIndex == 0)
                //    query = query.OrderByDescending(s => s.s.FgrihPkRefNo);
            }

            var list = await query.Skip(filterOptions.StartPageNo)
               .Take(filterOptions.RecordsPerPage)
               .ToListAsync();


            return list.Select(s => new FormF2DetailRequestDTO
            {
                PkRefNo = s.s.FgridPkRefNo,
                FgrihPkRefNo = s.s.FgridFgrihPkRefNo,
                FgrihAiPkRefNo = s.s.FgrihAiPkRefNo,
                StartingChKm = s.s.FgridStartingChKm,
                StartingChM = s.s.FgridStartingChM,
                GrCode = s.s.FgridGrCode,
                GrCondition1 = s.s.FgridGrCondition1,
                GrCondition2 = s.s.FgridGrCondition2,
                GrCondition3 = s.s.FgridGrCondition3,
                RhsMLhs = s.s.FgridRhsMLhs,
                PostSpac = s.s.FgridPostSpac,
                Remarks = s.s.FgridRemarks,
                ModBy = s.s.FgridModBy,
                ModDt = s.s.FgridModDt,
                CrBy = s.s.FgridCrBy,
                CrDt = s.s.FgridCrDt,
                SubmitSts = s.s.FgridSubmitSts,
                ActiveYn = s.s.FgridActiveYn.Value,
                Bound = s.a.AiBound,
                Length = s.s.FgridLength,
                AssetId = s.a.AiAssetId
            }).ToList();
        }

        public async Task<List<SelectListItem>> GetLocationCh(string roadcode)
        {
            return await _context.RmAllassetInventory.Where(s => s.AiActiveYn == true & s.AiRdCode == roadcode && s.AiAssetGrpCode == "GR")
                 .Select(s => new SelectListItem
                 {
                     Value = s.AiLocChKm + "+" + s.AiLocChM,
                     Text = s.AiLocChKm + "+" + s.AiLocChM
                 }).Distinct().ToListAsync();
        }

        public async Task<List<SelectListItem>> GetStructureCode(string roadcode, string locationch)
        {
            int.TryParse(locationch.Split("+")[0], out int km);

            if (locationch.Split("+").Length > 1)
            {
                locationch = locationch.Split("+")[1];
            }
            return await _context.RmAllassetInventory.Where(s => s.AiActiveYn == true && s.AiRdCode == roadcode && s.AiAssetGrpCode == "GR" && s.AiLocChKm == km && s.AiLocChM == locationch)
                .Select(s => new SelectListItem
                {
                    Value = s.AiStrucCode,
                    Text = s.AiStrucCode
                }).Distinct().ToListAsync();
        }

        public async Task<List<SelectListItem>> GetAIBound(string roadcode, string locationch, string structurecode)
        {
            int.TryParse(locationch.Split("+")[0], out int km);

            if (locationch.Split("+").Length > 1)
            {
                locationch = locationch.Split("+")[1];
            }
            return await _context.RmAllassetInventory
                .Where(s =>
                s.AiRdCode == roadcode
                && s.AiAssetGrpCode == "GR"
                && s.AiLocChKm == km && s.AiLocChM == locationch
                && s.AiStrucCode == structurecode)
               .Select(s => new SelectListItem
               {
                   Value = s.AiBound,
                   Text = s.AiBound
               }).Distinct().ToListAsync();
        }

        public async Task<List<SelectListItem>> GetPostSpacing(string roadcode, string locationch, string structurecode, string bound)
        {
            int.TryParse(locationch.Split("+")[0], out int km);

            if (locationch.Split("+").Length > 1)
            {
                locationch = locationch.Split("+")[1];
            }
            var lst = await _context.RmAllassetInventory
                .Where(s => s.AiRdCode == roadcode
                && s.AiAssetGrpCode == "GR"
                && s.AiLocChKm == km && s.AiLocChM == locationch
                && s.AiStrucCode == structurecode
                && s.AiBound == bound)
                .ToListAsync();

            return lst
               .Select(s => new SelectListItem
               {
                   Value = s.AiPkRefNo.ToString(),
                   Text = s.AiPostSpacing.ToString()
               }).Distinct().ToList();
        }

        public async Task<List<RmFormF2GrInsDtl>> GetF2DetailList(int headerId)
        {
            return await _context.RmFormF2GrInsDtl.Where(x => x.FgridFgrihPkRefNo == headerId).ToListAsync();
        }
    }
}
