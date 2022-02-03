using Microsoft.EntityFrameworkCore;
using RAMMS.Common.Extensions;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.SearchBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RAMS.Repository
{
    public class FormXRepository : RepositoryBase<RmFormXHdr>, IFormXRepository
    {
        public FormXRepository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }



        public int SaveFormXHdr(RmFormXHdr rmFormXHdr)
        {
            try
            {
                _context.Entry<RmFormXHdr>(rmFormXHdr).State = rmFormXHdr.FxhPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return rmFormXHdr.FxhPkRefNo;
            }
            catch (Exception)
            {
                return 500;

            }
        }

        public RmFormXHdr GetRmFormXHdr(RmFormXHdr rmFormXHdr)
        {
            var usrInst = new RmFormXHdr();
            var userInst = _context.Set<RmFormXHdr>()
                     .AsNoTracking();

            return usrInst;
        }

        public async Task<RmFormXHdr> DetailView(RmFormXHdr rmFormXHdr)
        {
            var editDetail = await _context.Set<RmFormXHdr>().FirstOrDefaultAsync(a => a.FxhPkRefNo == rmFormXHdr.FxhPkRefNo);
            return editDetail;
        }

        public async Task<RmFormXHdr> GetFormWithDetailsByNoAsync(int formNo)
        {
            return await _context.RmFormXHdr
                        .FirstOrDefaultAsync(x => x.FxhPkRefNo == formNo);
        }

        public async Task<List<RmFormXHdr>> GetFilteredRecordList(FilteredPagingDefinition<FormXSearchDTO> filterOptions)
        {
            List<RmFormXHdr> result = new List<RmFormXHdr>();
            var query = (from x in _context.RmFormXHdr select x);
            PrepareFilterQuery(filterOptions, ref query);

            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(s => s.FxhRefId);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(s => s.FxhRmu);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(s => s.FxhRoadCode);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(s => s.FxhLoc);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(s => s.FxhUsernamePrp);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(s => s.FxhUsernameAttnTo);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderBy(s => s.FxhModComType);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderBy(s => s.FxhUsernameVer);
            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(s => s.FxhRefId);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(s => s.FxhRmu);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(s => s.FxhRoadCode);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(s => s.FxhLoc);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(s => s.FxhUsernamePrp);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(s => s.FxhUsernameAttnTo);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderByDescending(s => s.FxhModComType);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderByDescending(s => s.FxhUsernameVer);
            }
            result = await query
                                .Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();

            return result;
        }

        private void PrepareFilterQuery(FilteredPagingDefinition<FormXSearchDTO> filterOptions, ref IQueryable<RmFormXHdr> query)
        {
            query = query.Where(x => x.FxhActiveYn == true);
            if (filterOptions.Filters != null)
            {
                if (filterOptions.Filters.ActMainCode.HasValue && filterOptions.Filters.ActMainCode.Value > 0)
                {
                    query = query.Where(x => x.FxhActMainCode == filterOptions.Filters.ActMainCode);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ActMainName))
                {
                    query = query.Where(x => x.FxhActMainName == filterOptions.Filters.ActMainName);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RoadCode))
                {
                    query = query.Where(x => x.FxhRoadCode == filterOptions.Filters.RoadCode);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.Rmu))
                {
                    query = query.Where(x => x.FxhRmu == filterOptions.Filters.Rmu);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ActSubCode))
                {
                    query = query.Where(x => x.FxhActSubCode == filterOptions.Filters.ActSubCode);
                }

                if (filterOptions.Filters.WorkScheduleDt.HasValue)
                {
                    query = query.Where(x => x.FxhWrkSc.Value.Date == filterOptions.Filters.WorkScheduleDt.Value.Date);
                }

                if (filterOptions.Filters.WorkCompltDt.HasValue)
                {
                    query = query.Where(x => x.FxhWrkCmpld.Value.Date == filterOptions.Filters.WorkCompltDt.Value.Date);
                }

                if (filterOptions.Filters.CaseClosedDt.HasValue)
                {
                    query = query.Where(x => (x.FxhClsd.Value.Date == filterOptions.Filters.CaseClosedDt.Value.Date));
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    query = query.Where(x =>
                                        (x.FxhRefId ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.FxhRmu ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.FxhRoadCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.FxhUsernameAttnTo ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.FxhUsernamePrp ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.FxhUsernameVer ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.FxhModComType ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                        || (x.FxhLoc ?? "").Contains(filterOptions.Filters.SmartInputValue));
                }
            }
        }

        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormXSearchDTO> filterOptions)
        {
            var query = (from x in _context.RmFormXHdr select x);
            PrepareFilterQuery(filterOptions, ref query);
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<string>> GetSectionByRMU(string rmu)
        {

            var query = (from section in _context.RmFormAHdr
                         where section.FahRmu == rmu && section.FahActiveYn == true
                         select section.FahSection).ToListAsync().ConfigureAwait(false);
            return await query;
        }

        public async Task<IEnumerable<RmAssetDefectCode>> GetDefectCode(string assetGroup)
        {
            return await _context.RmAssetDefectCode.Where(a => a.AdcAssetGrpCode == assetGroup).ToListAsync();
        }

        public async Task<int> CheckwithRef(FormXHeaderRequestDTO formXHeader)
        {
            var data = await _context.RmFormXHdr.Where(x => (x.FxhPkRefNo == formXHeader.No || x.FxhRefId == formXHeader.ReferenceId)).FirstOrDefaultAsync();
            if (data != null)
            {
                return data.FxhPkRefNo;
            }
            else
            {
                return 0;
            }

        }

        public void SaveWarImage(IEnumerable<RmWarImageDtl> rmWarImage)
        {
            _context.RmWarImageDtl.AddRange(rmWarImage);
        }

        public void SaveAccUccImage(IEnumerable<RmAccUcuImageDtl> rmAccUcuImage)
        {
            _context.RmAccUcuImageDtl.AddRange(rmAccUcuImage);
        }
        public void UpdateWarList(IEnumerable<RmWarImageDtl> rmWarImage)
        {
            _context.RmWarImageDtl.UpdateRange(rmWarImage);
        }

        public void UpdateWar(RmWarImageDtl rmWarImage)
        {
            _context.Set<RmWarImageDtl>().Attach(rmWarImage);
            _context.Entry(rmWarImage).State = EntityState.Modified;
        }

        public void UpdateAccUcu(RmAccUcuImageDtl rmAccUcuImage)
        {
            _context.Set<RmAccUcuImageDtl>().Attach(rmAccUcuImage);
            _context.Entry(rmAccUcuImage).State = EntityState.Modified;
        }

        public Task<RmWarImageDtl> GetWarImageByIdAsync(int warId)
        {
            return _context.RmWarImageDtl.Where(x => x.FwarPkRefNo == warId).FirstOrDefaultAsync();
        }
        public Task<List<RmWarImageDtl>> GetWarImageListAsync(RmWarImageDtl warImageDtl)
        {
            return _context.RmWarImageDtl.Where(x => x.FwarImageFilenameSys == warImageDtl.FwarImageFilenameSys && x.FwarImageFilenameUpload == warImageDtl.FwarImageFilenameUpload && x.FwarActiveYn == true && x.FwarFddPkRefNo != null).ToListAsync();
        }
        public Task<List<RmAccUcuImageDtl>> GetAccUcuImageListAsync(RmAccUcuImageDtl accUcuImageDtl)
        {
            return _context.RmAccUcuImageDtl.Where(x => x.FauImageFilenameSys == accUcuImageDtl.FauImageFilenameSys && x.FauImageFilenameUpload == accUcuImageDtl.FauImageFilenameUpload && x.FauActiveYn == true && x.FauFddPkRefNo != null).ToListAsync();
        }
        public Task<RmAccUcuImageDtl> GetAccUccImageById(int accUccId)
        {
            return _context.RmAccUcuImageDtl.Where(x => x.FauPkRefNo == accUccId).FirstOrDefaultAsync();
        }
        public Task<RmWarImageDtl> GetWarDocById(int accUccId)
        {
            return _context.RmWarImageDtl.Where(x => x.FwarPkRefNo == accUccId).FirstOrDefaultAsync();
        }
        public Task<List<RmWarImageDtl>> GetWarImagelist(int formXId)
        {
            return _context.RmWarImageDtl.Where(x => x.FwarFxhPkRefNo == formXId && x.FwarActiveYn == true).ToListAsync();
        }

        public Task<List<RmAccUcuImageDtl>> GetAccUccImagelist(int formXId)
        {
            return _context.RmAccUcuImageDtl.Where(x => x.FauFxhPkRefNo == formXId && x.FauActiveYn == true).ToListAsync();
        }
        public async Task<int> GetWARId(int headerId, string type)
        {
            int? result = await _context.RmWarImageDtl.Where(x => x.FwarFxhPkRefNo == headerId && x.FwarImageTypeCode == type).Select(x => x.FwarImageSrno).MaxAsync();
            return result.HasValue ? result.Value : 0;
        }
        public async Task<int> GetUCUId(int headerId, string type)
        {
            int? result = await _context.RmAccUcuImageDtl.Where(x => x.FauFxhPkRefNo == headerId && x.FauAccUcu == type).Select(x => x.FauImageSrno).MaxAsync();
            return result.HasValue ? result.Value : 0;
        }

        public (int id, bool aleadyExists) CheckExistence(string rdCode, int month, int year, DateTime rpDate)
        {
            var result = _context.RmFormXHdr
                .FirstOrDefault(s =>
            s.FxhRoadCode == rdCode && s.FxhActiveYn == true &&
           s.FxhDtPrp.Value.Date == rpDate.Date);
            if (result != null)
            {
                return (result.FxhPkRefNo, true);
            }
            else
            {
                var d = _context.RmFormXHdr.Where(s => s.FxhActiveYn == true).OrderByDescending(s => s.FxhPkRefNo).FirstOrDefault();
                if (d != null)
                    return (d.FxhPkRefNo + 1, false);
                else
                {
                    return (1, false);
                }
            }
        }

        public async Task<List<RmWarImageDtl>> GetFilteredWARImagesFormX(int primaryNo)
        {
            List<RmWarImageDtl> rmWarImageDtls = new List<RmWarImageDtl>();
            var result = await _context.RmWarImageDtl.Where(x => x.FwarFxhPkRefNo == primaryNo && x.FwarActiveYn == true).ToListAsync().ConfigureAwait(false);
            return result;
        }

        public async Task<int> GetWARTypeCodeCount(string type, int id)
        {
            return await _context.RmWarImageDtl.CountAsync(t => t.FwarImageTypeCode == type && t.FwarFxhPkRefNo == id && t.FwarActiveYn == true);
        }

        public async Task<string> GetSectionByRoadCodeAndRMU(string rdCode, string rmu)
        {
            var d = await _context.RmRoadMaster.FirstOrDefaultAsync(s => (s.RdmRdCode == rdCode || s.RdmRdName == rdCode) &&
            (s.RdmRmuCode == rmu || s.RdmRmuName == rmu) && s.RdmActiveYn == true);
            if (d != null)
                return d.RdmSecName;
            else
                return "";
        }
    }
}
