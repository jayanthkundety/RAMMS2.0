using System;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO.Report;
using RAMMS.Common.Extensions;
using RAMMS.Common;
using RAMMS.DTO.ResponseBO;

namespace RAMMS.Repository
{
    public class FormB1B2HeaderRepository : RepositoryBase<RmFormB1b2BrInsHdr>, IFormB1B2HeaderRepository
    {
        public FormB1B2HeaderRepository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<SelectListItem> GetBridgeIds(AssetDDLRequestDTO request)
        {
            var lst = _context.RmAllassetInventory.Where(s => s.AiAssetGrpCode == "BR" && (request.IncludeInActive ? true : s.AiActiveYn == true));
            if (!string.IsNullOrEmpty(request.RMU))
                lst = lst.Where(s => (s.AiRmuCode == request.RMU || s.AiRmuName == request.RMU));
            if (!string.IsNullOrEmpty(request.RdCode))
                lst = lst.Where(s => s.AiRdCode == request.RdCode);
            if (request.SectionCode > 0)
            {
                string code = request.SectionCode.ToString();
                lst = lst.Where(s => s.AiSecCode == code);
            }

            var resultlst = lst.ToArray().OrderBy(x => x.AiLocChKm).ThenBy(x => x.AiLocChM)
                .Select(s => new SelectListItem
                {
                    Value = s.AiPkRefNo.ToString(),
                    Text = s.AiAssetId
                });
            return resultlst;
        }

        public async Task<long> GetFilteredRecordCount(FilteredPagingDefinition<FormB1B2SearchGridDTO> filterOptions)
        {
            var query = (from s in _context.RmFormB1b2BrInsHdr
                         join d in _context.RmAllassetInventory on s.FbrihAiPkRefNo equals d.AiPkRefNo
                         where s.FbrihActiveYn == true
                         select new { s, d });
            var search = filterOptions.Filters;

            if (search.SecCode.HasValue)
            {
                query = query.Where(s => s.d.AiSecCode == search.SecCode.Value.ToString());
            }
            if (!string.IsNullOrEmpty(search.RmuCode))
            {
                query = query.Where(s => s.d.AiRmuCode == search.RmuCode);
            }
            if (!string.IsNullOrEmpty(search.RoadCode))
            {
                query = query.Where(s => s.s.FbrihAiRdCode == search.RoadCode);
            }
            if (search.Year.HasValue)
            {
                query = query.Where(s => s.s.FbrihYearOfInsp == search.Year.Value);
            }

            if (search.FromYear.HasValue)
            {
                query = query.Where(s => s.s.FbrihYearOfInsp >= search.FromYear);
            }
            if (search.ToYear.HasValue)
            {
                query = query.Where(s => s.s.FbrihYearOfInsp <= search.ToYear);
            }
            if (!string.IsNullOrEmpty(search.AssetType))
            {
                query = query.Where(s => s.s.FbrihAiStrucCode == search.AssetType);
            }
            if (search.locchFromKm.HasValue || !string.IsNullOrEmpty(search.locchFromM))

            {
                query = query.Where(x => Convert.ToDouble(x.s.FbrihAiLocChKm.ToString() + '.' + x.s.FbrihAiLocChM) >= Convert.ToDouble((search.locchFromKm.ToString() + '.' + search.locchFromM)));
            }

            if (search.locchToKm.HasValue || !string.IsNullOrEmpty(search.locchToM))
            {
                query = query.Where(x => Convert.ToDouble(x.s.FbrihAiLocChKm.ToString() + '.' + x.s.FbrihAiLocChM) <= Convert.ToDouble((search.locchToKm.ToString() + '.' + search.locchToM)));
            }
            if (!string.IsNullOrEmpty(search.SmartSearch))
            {
                DateTime dt;
                if (DateTime.TryParseExact(search.SmartSearch, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                {
                    query = query.Where(s =>
                     s.s.FbrihCInspRefNo.Contains(search.SmartSearch) ||
                     s.s.FbrihAiRdCode.Contains(search.SmartSearch) ||
                     s.d.AiRmuCode.Contains(search.SmartSearch) ||
                     s.d.AiRmuName.Contains(search.SmartSearch) ||
                     s.d.AiDivCode.Contains(search.SmartSearch) ||
                     s.s.FbrihAiRdName.Contains(search.SmartSearch) ||
                     s.s.FbrihAiAssetId.Contains(search.SmartSearch) ||
                     s.d.AiSecCode.Contains(search.SmartSearch) ||
                     s.s.FbrihSerProviderUserName.Contains(search.SmartSearch) ||
                     s.s.FbrihUserNameAud.Contains(search.SmartSearch)
                     || (s.s.FbrihDtOfInsp.HasValue ? (s.s.FbrihDtOfInsp.Value.Year == dt.Year && s.s.FbrihDtOfInsp.Value.Month == dt.Month && s.s.FbrihDtOfInsp.Value.Day == dt.Day) : true)
                     || (s.s.FbrihSubmitSts ? "Submitted" : "Saved").Contains(search.SmartSearch));
                }
                else
                {
                    query = query.Where(s =>
                 s.s.FbrihCInspRefNo.Contains(search.SmartSearch) ||
                 s.s.FbrihAiRdCode.Contains(search.SmartSearch) ||
                 s.d.AiRmuCode.Contains(search.SmartSearch) ||
                 s.d.AiRmuName.Contains(search.SmartSearch) ||
                 s.d.AiDivCode.Contains(search.SmartSearch) ||
                 s.s.FbrihAiRdName.Contains(search.SmartSearch) ||
                 s.s.FbrihAiAssetId.Contains(search.SmartSearch) ||
                 s.d.AiSecCode.Contains(search.SmartSearch) ||
                 s.s.FbrihSerProviderUserName.Contains(search.SmartSearch) ||
                 s.s.FbrihUserNameAud.Contains(search.SmartSearch)
                 || (s.s.FbrihSubmitSts ? "Submitted" : "Saved").Contains(search.SmartSearch));
                }

            }
            return await query.LongCountAsync();
        }
        public async Task<List<FormB1B2HeaderRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormB1B2SearchGridDTO> filterOptions)
        {
            var query = (from s in _context.RmFormB1b2BrInsHdr
                         join d in _context.RmAllassetInventory on s.FbrihAiPkRefNo equals d.AiPkRefNo
                         where s.FbrihActiveYn == true
                         select new { s, d });
            var search = filterOptions.Filters;

            if (search.SecCode.HasValue)
            {
                query = query.Where(s => s.d.AiSecCode == search.SecCode.Value.ToString());
            }
            if (!string.IsNullOrEmpty(search.RmuCode))
            {
                query = query.Where(s => s.d.AiRmuCode == search.RmuCode);
            }
            if (!string.IsNullOrEmpty(search.RoadCode))
            {
                query = query.Where(s => s.s.FbrihAiRdCode == search.RoadCode);
            }
            if (search.Year.HasValue)
            {
                query = query.Where(s => s.s.FbrihYearOfInsp == search.Year.Value);
            }

            if (search.FromYear.HasValue)
            {
                query = query.Where(s => s.s.FbrihYearOfInsp >= search.FromYear);
            }
            if (search.ToYear.HasValue)
            {
                query = query.Where(s => s.s.FbrihYearOfInsp <= search.ToYear);
            }
            if (!string.IsNullOrEmpty(search.AssetType))
            {
                query = query.Where(s => s.s.FbrihAiStrucCode == search.AssetType);
            }
            if (search.locchFromKm.HasValue || !string.IsNullOrEmpty(search.locchFromM))

            {
                query = query.Where(x => Convert.ToDouble(x.s.FbrihAiLocChKm.ToString() + '.' + x.s.FbrihAiLocChM) >= Convert.ToDouble((search.locchFromKm.ToString() + '.' + search.locchFromM)));
            }

            if (search.locchToKm.HasValue || !string.IsNullOrEmpty(search.locchToM))
            {
                query = query.Where(x => Convert.ToDouble(x.s.FbrihAiLocChKm.ToString() + '.' + x.s.FbrihAiLocChM) <= Convert.ToDouble((search.locchToKm.ToString() + '.' + search.locchToM)));
            }
            if (!string.IsNullOrEmpty(search.SmartSearch))
            {
                DateTime dt;
                if (DateTime.TryParseExact(search.SmartSearch, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                {
                    query = query.Where(s =>
                     s.s.FbrihCInspRefNo.Contains(search.SmartSearch) ||
                     s.s.FbrihAiRdCode.Contains(search.SmartSearch) ||
                     s.d.AiRmuCode.Contains(search.SmartSearch) ||
                     s.d.AiRmuName.Contains(search.SmartSearch) ||
                     s.d.AiDivCode.Contains(search.SmartSearch) ||
                     s.s.FbrihAiRdName.Contains(search.SmartSearch) ||
                     s.s.FbrihAiAssetId.Contains(search.SmartSearch) ||
                     s.d.AiSecCode.Contains(search.SmartSearch) ||
                     s.s.FbrihSerProviderUserName.Contains(search.SmartSearch) ||
                     s.s.FbrihUserNameAud.Contains(search.SmartSearch)
                     || (s.s.FbrihDtOfInsp.HasValue ? (s.s.FbrihDtOfInsp.Value.Year == dt.Year && s.s.FbrihDtOfInsp.Value.Month == dt.Month && s.s.FbrihDtOfInsp.Value.Day == dt.Day) : false)
                     || (s.s.FbrihSubmitSts ? "Submitted" : "Saved").Contains(search.SmartSearch));
                }
                else
                {
                    query = query.Where(s =>
                 s.s.FbrihCInspRefNo.Contains(search.SmartSearch) ||
                 s.s.FbrihAiRdCode.Contains(search.SmartSearch) ||
                 s.d.AiRmuCode.Contains(search.SmartSearch) ||
                 s.d.AiRmuName.Contains(search.SmartSearch) ||
                 s.d.AiDivCode.Contains(search.SmartSearch) ||
                 s.s.FbrihAiRdName.Contains(search.SmartSearch) ||
                 s.s.FbrihAiAssetId.Contains(search.SmartSearch) ||
                 s.d.AiSecCode.Contains(search.SmartSearch) ||
                 s.s.FbrihSerProviderUserName.Contains(search.SmartSearch) ||
                 s.s.FbrihUserNameAud.Contains(search.SmartSearch)
                 || (s.s.FbrihSubmitSts ? "Submitted" : "Saved").Contains(search.SmartSearch));
                }

            }

            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 0)
                    query = query.OrderByDescending(s => s.s.FbrihPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(s => s.s.FbrihCInspRefNo);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(s => s.s.FbrihYearOfInsp);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(s => s.s.FbrihDtOfInsp);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(s => s.s.FbrihAiAssetId);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(s => s.d.AiDivCode);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(s => s.d.AiRmuCode);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderBy(s => s.d.AiRmuName);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderBy(s => s.d.AiSecCode);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderBy(s => s.d.AiSecName);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderBy(s => s.s.FbrihAiRdCode);
                if (filterOptions.ColumnIndex == 12)
                    query = query.OrderBy(s => s.s.FbrihAiRdName);
                if (filterOptions.ColumnIndex == 13)
                    query = query.OrderBy(s => s.s.FbrihSubmitSts);
                if (filterOptions.ColumnIndex == 14)
                    query = query.OrderBy(s => s.s.FbrihSerProviderUserName);
                if (filterOptions.ColumnIndex == 15)
                    query = query.OrderBy(s => s.s.FbrihUserNameAud);
            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 0)
                    query = query.OrderByDescending(s => s.s.FbrihPkRefNo);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(s => s.s.FbrihCInspRefNo);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(s => s.s.FbrihYearOfInsp);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(s => s.s.FbrihDtOfInsp);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(s => s.s.FbrihAiAssetId);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(s => s.s.FbrihAiDivCode);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(s => s.d.AiRmuCode);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderByDescending(s => s.d.AiRmuName);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderByDescending(s => s.d.AiSecCode);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderByDescending(s => s.d.AiSecName);
                if (filterOptions.ColumnIndex == 11)
                    query = query.OrderByDescending(s => s.s.FbrihAiRdCode);
                if (filterOptions.ColumnIndex == 12)
                    query = query.OrderByDescending(s => s.s.FbrihAiRdName);
                if (filterOptions.ColumnIndex == 13)
                    query = query.OrderByDescending(s => s.s.FbrihSubmitSts);
                if (filterOptions.ColumnIndex == 14)
                    query = query.OrderByDescending(s => s.s.FbrihSerProviderUserName);
                if (filterOptions.ColumnIndex == 15)
                    query = query.OrderByDescending(s => s.s.FbrihUserNameAud);
            }
            var lst = await query.Skip(filterOptions.StartPageNo).Take(filterOptions.RecordsPerPage).ToListAsync();
            return lst.Select(s => new FormB1B2HeaderRequestDTO
            {
                PkRefNo = s.s.FbrihPkRefNo,
                AiPkRefNo = s.s.FbrihAiPkRefNo,
                AiAssetId = s.s.FbrihAiAssetId,
                AiLocChKm = s.s.FbrihAiLocChKm,
                AiLocChM = s.s.FbrihAiLocChM,
                AiStrucCode = s.s.FbrihAiStrucCode,
                AiGpsEasting = s.s.FbrihAiGpsEasting,
                AiGpsNorthing = s.s.FbrihAiGpsNorthing,
                AiRdCode = s.s.FbrihAiRdCode,
                AiRdName = s.s.FbrihAiRdName,
                AiRiverName = s.s.FbrihAiRiverName,
                AiDivCode = s.s.FbrihAiDivCode,
                AiRmuName = s.s.FbrihAiRmuName,
                AiStrucSuper = s.s.FbrihAiStrucSuper,
                AiParapetType = s.s.FbrihAiParapetType,
                AiBearingType = s.s.FbrihAiBearingType,
                AiExpanType = s.s.FbrihAiExpanType,
                AiDeckType = s.s.FbrihAiDeckType,
                AiAbutType = s.s.FbrihAiAbutType,
                AiPierType = s.s.FbrihAiPierType,
                AiExpanJointCount = s.s.FbrihAiExpanJointCount,
                AiWidthLane = s.s.FbrihAiWidthLane,
                AiLengthSpan = s.s.FbrihAiLengthSpan,
                AiLength = s.s.FbrihAiLength,
                AiWidth = s.s.FbrihAiWidth,
                AiLaneCnt = s.s.FbrihAiLaneCnt,
                AiSpanCnt = s.s.FbrihAiSpanCnt,
                AiMedian = s.s.FbrihAiMedian,
                AiWalkway = s.s.FbrihAiWalkway,
                CInspRefNo = s.s.FbrihCInspRefNo,
                YearOfInsp = s.s.FbrihYearOfInsp,
                DtOfInsp = s.s.FbrihDtOfInsp,
                RecordNo = s.s.FbrihRecordNo,
                SerProviderDefObs = s.s.FbrihSerProviderDefObs,
                AuthDefObs = s.s.FbrihAuthDefObs,
                SerProviderDefGenCom = s.s.FbrihSerProviderDefGenCom,
                AuthDefGenCom = s.s.FbrihAuthDefGenCom,
                SerProviderDefFeedback = s.s.FbrihSerProviderDefFeedback,
                AuthDefFeedback = s.s.FbrihAuthDefFeedback,
                SerProviderUserId = s.s.FbrihSerProviderUserId,
                SerProviderUserName = s.s.FbrihSerProviderUserName,
                SerProviderUserDesignation = s.s.FbrihSerProviderUserDesignation,
                SerProviderInsDt = s.s.FbrihSerProviderInsDt,
                SignpathSerProvider = s.s.FbrihSignpathSerProvider,
                UserIdAud = s.s.FbrihUserIdAud,
                UserNameAud = s.s.FbrihUserNameAud,
                UserDesignationAud = s.s.FbrihUserDesignationAud,
                DtAud = s.s.FbrihDtAud,
                SignpathAud = s.s.FbrihSignpathAud,
                BridgeConditionRat = s.s.FbrihBridgeConditionRat,
                ReqFurtherInv = s.s.FbrihReqFurtherInv,
                ModBy = s.s.FbrihModBy,
                ModDt = s.s.FbrihModDt,
                CrBy = s.s.FbrihCrBy,
                CrDt = s.s.FbrihCrDt,
                SubmitSts = s.s.FbrihSubmitSts,
                ActiveYn = s.s.FbrihActiveYn.Value,
                SectionCode = s.d.AiSecCode,
                SectionName = s.d.AiSecName,
                RmuCode = s.d.AiRmuCode
            }).ToList();
        }

        public string GetMaterialType(string type, string value)
        {
            string data = "";
            var ddl = _context.RmDdLookup.Where(s => s.DdlActiveYn == true && s.DdlTypeCode == "BR" && (s.DdlType == type || s.DdlTypeDesc == type)).ToArray();
            if (ddl != null && value != null)
            {
                foreach (var d in ddl)
                {
                    if (d.DdlTypeValue.Trim().ToLower() == value.Trim().ToLower())
                    {
                        data += $" ☑ {d.DdlTypeValue}";
                    }
                    else
                    {
                        data += $" ☐ {d.DdlTypeValue}";
                    }
                }
            }
            return data;
        }

        /// <summary>
        /// To Get bridge details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<FormB1B2HeaderRequestDTO> GetBrideDetailById(long id)
        {
            Func<string, string, string> code = (type, a) =>
             {
                 string[] arrType = new string[0];
                 if (a != null)
                 {
                     arrType = a.Split(',');
                 }
                 string result = "";
                 foreach (var typ in arrType)
                 {
                     var ddl = _context.RmDdLookup.Where(s => s.DdlActiveYn == true && s.DdlTypeCode == "BR" && (s.DdlType == typ || s.DdlTypeDesc == typ)).FirstOrDefault();
                     if (ddl != null)
                     {
                         if (result != "")
                         {
                             result = result != null ? result + "," + ddl.DdlTypeValue : ddl.DdlTypeValue;
                         }
                         else
                         {
                             result = ddl.DdlTypeValue;
                         }
                     }

                 }
                 return result;
             };

            FormB1B2HeaderRequestDTO result =
                await (from s in _context.RmAllassetInventory
                       where s.AiPkRefNo == id && s.AiActiveYn == true
                       select new FormB1B2HeaderRequestDTO
                       {
                           ActiveYn = true,
                           AiPkRefNo = s.AiPkRefNo,
                           AiAssetId = s.AiAssetId,
                           AiRdCode = s.AiRdCode,
                           AiRdName = s.AiRdName,
                           AiDivCode = s.AiDivCode,
                           RmuCode = s.AiRmuCode,
                           AiRmuName = s.AiRmuName,
                           AiStrucCode = s.AiStrucCode,
                           AiLocChKm = s.AiLocChKm,
                           AiLocChM = s.AiLocChM,
                           AiRiverName = s.AiRiverName,
                           AiWidthLane = s.AiWidthLane,
                           AiLengthSpan = s.AiLengthSpan,
                           AiLength = s.AiLength,
                           AiWidth = s.AiWidth,
                           AiLaneCnt = s.AiLaneCnt,
                           AiSpanCnt = s.AiSpanCnt,
                           AiMedian = s.AiMedian,
                           AiWalkway = s.AiWalkway,
                           AiStrucSuper = s.AiStrucSuper,
                           AiParapetType = s.AiParapetType,
                           AiBearingType = s.AiBearingType,
                           AiExpanType = s.AiExpanType,
                           AiDeckType = s.AiDeckType,
                           AiAbutType = s.AiAbutType,
                           AiPierType = s.AiPierType,
                           AiExpanJointCount = s.AiExpanJointCount,
                           AiGpsEasting = s.AiGpsEasting,
                           AiGpsNorthing = s.AiGpsNorthing,
                           Detail = new FormB1B2DetailRequestDTO
                           {
                               AbutFoundMat = s.AiAbutFound,
                               //AbutFoundMatCode = code("Abutment Walls, Foundation", s.AiAbutFound),
                               BeamsGridTrusArch = s.AiBeamsGridTrusArch,
                               //BeamsGridTrusArchCode = code("Beams, Girders, Trussess, Arches", s.AiBeamsGridTrusArch),
                               BearingStDiaphgMat = s.AiBearingSeatDiaphg,
                               //BearingStDiaphgMatCode = code("Bearing, Bearing Seats, Bearing Diaphgrams", s.AiBearingSeatDiaphg),
                               DeckPavement = s.AiDeckPavement,
                               //DeckPavementCode = code("Deck Slab, Pavement", s.AiDeckPavement),
                               ExpanJoint = s.AiExpanJoint,
                               //ExpanJointCode = code("Expansion Joint", s.AiExpanJoint),
                               ParapetRailing = s.AiParapetRailing,
                               //ParapetRailingCode = code("Parapet, Railing", s.AiParapetRailing),
                               PiersPrimCompMat = s.AiPiersPrimComp,
                               //PiersPrimCompMatCode = code("Piers, Connectiong of primary components", s.AiPiersPrimComp),
                               SidewalksAppSlab = s.AiSidewalksAppSlab,
                               //SidewalksAppSlabCode = code("Kerb, Sidewalks, Approaches, Approch Slab", s.AiSidewalksAppSlab),
                               SlopeRetainWall = s.AiSlopeRetainWall,
                               //SlopeRetainWallCode = code("Slope Protections, Retaining Wall", s.AiSlopeRetainWall),
                               Utilities = s.AiUtilities,
                               //UtilitiesCode = code("Signboard, Utilities", s.AiUtilities),
                               WaterDownpipe = s.AiWaterDownpipe,
                               //WaterDownpipeCode = code("Drain Water Down Pipe, Drainage", s.AiWaterDownpipe),
                               Waterway = s.AiWaterway,
                               //WaterwayCode = code("Waterway", s.AiWaterway),
                           }
                       }).FirstOrDefaultAsync();
            result.Detail.AbutFoundMatCode = code("Abutment Walls, Foundation", result.Detail.AbutFoundMat);
            result.Detail.BeamsGridTrusArchCode = code("Beams, Girders, Trussess, Arches", result.Detail.BeamsGridTrusArch);
            result.Detail.BearingStDiaphgMatCode = code("Bearing, Bearing Seats, Bearing Diaphgrams", result.Detail.BearingStDiaphgMat);
            result.Detail.DeckPavementCode = code("Deck Slab, Pavement", result.Detail.DeckPavement);
            result.Detail.ExpanJointCode = code("Expansion Joint", result.Detail.ExpanJoint);
            result.Detail.ParapetRailingCode = code("Parapet, Railing", result.Detail.ParapetRailing);
            result.Detail.PiersPrimCompMatCode = code("Piers, Connectiong of primary components", result.Detail.PiersPrimCompMat);
            result.Detail.SidewalksAppSlabCode = code("Kerb, Sidewalks, Approaches, Approch Slab", result.Detail.SidewalksAppSlab);
            result.Detail.SlopeRetainWallCode = code("Slope Protections, Retaining Wall", result.Detail.SlopeRetainWall);
            result.Detail.UtilitiesCode = code("Signboard, Utilities", result.Detail.Utilities);
            result.Detail.WaterDownpipeCode = code("Drain Water Down Pipe, Drainage", result.Detail.WaterDownpipe);
            result.Detail.WaterwayCode = code("Waterway", result.Detail.Waterway);
            return result;
        }

        public List<FormB1B2Rpt> GetReportData(int id)
        {
            //var roadcode = (from h in _context.RmFormB1b2BrInsHdr
            //                where h.FbrihPkRefNo == id
            //                select new
            //                {
            //                    h.FbrihAiRdCode,
            //                    h.FbrihDtOfInsp
            //                }).FirstOrDefault();
            var type = (from ty in _context.RmDdLookup
                        where ty.DdlType == "Photo Type" && ty.DdlTypeCode == "BR"
                        orderby ty.DdlTypeRemarks ascending
                        select ty).ToList();

            var detail = (from h in _context.RmFormB1b2BrInsHdr
                          join d in _context.RmFormB1b2BrInsDtl on h.FbrihPkRefNo equals d.FbridFbrihPkRefNo
                          where
                       h.FbrihPkRefNo == id
                          orderby h.FbrihDtOfInsp descending
                          select new FormB1B2Rpt
                          {
                              Year = h.FbrihYearOfInsp,
                              AbutmentType = h.FbrihAiAbutType,
                              AbutmentWall_Foundation_Material = d.FbridAbutFoundMatCode,
                              AuditedByDate = h.FbrihDtAud,
                              AuditedByDesignation = h.FbrihUserDesignationAud,
                              AuditedByName = h.FbrihUserNameAud,
                              Beam_Material = d.FbridBeamsGridTrusArchCode,
                              BearingType = h.FbrihAiBearingType,
                              AbutmentWall_Foundation_Distress_Severity = new InspectionRpt
                              {
                                  Day = h.FbrihDtOfInsp.Value.Day,
                                  Month = h.FbrihDtOfInsp.Value.Month,
                                  Year = h.FbrihDtOfInsp.Value.Year,
                                  Distress = d.FbridAbutFoundDistress,
                                  Severity = d.FbridAbutFoundSeverity

                              },
                              Beam_Distress_Severity = new InspectionRpt
                              {
                                  Day = h.FbrihDtOfInsp.Value.Day,
                                  Month = h.FbrihDtOfInsp.Value.Month,
                                  Year = h.FbrihDtOfInsp.Value.Year,
                                  Distress = d.FbridBeamsGridTrusArchDistress,
                                  Severity = d.FbridBeamsGridTrusArchSeverity
                              },
                              Bearing_Distress_Severity = new InspectionRpt
                              {
                                  Day = h.FbrihDtOfInsp.Value.Day,
                                  Month = h.FbrihDtOfInsp.Value.Month,
                                  Year = h.FbrihDtOfInsp.Value.Year,
                                  Distress = d.FbridBearingStDiaphgDistress,
                                  Severity = d.FbridBearingStDiaphgSeverity
                              },
                              Bearing_Material = d.FbridBearingStDiaphgMatCode,
                              BridgeConditionRating = h.FbrihBridgeConditionRat,
                              BridgeLength = h.FbrihAiLength,
                              BridgeWidth = h.FbrihAiWidth,
                              ChainageKm = h.FbrihAiLocChKm,
                              ChainageM = h.FbrihAiLocChM,
                              DeckType = h.FbrihAiDeckType,
                              Deck_Distress_Severity = new InspectionRpt
                              {
                                  Day = h.FbrihDtOfInsp.Value.Day,
                                  Month = h.FbrihDtOfInsp.Value.Month,
                                  Year = h.FbrihDtOfInsp.Value.Year,
                                  Distress = d.FbridDeckPavementDistress,
                                  Severity = d.FbridDeckPavementSeverity
                              },
                              Deck_Material = d.FbridDeckPavementCode,
                              Division = h.FbrihAiDivCode,
                              Drainwater_Distress_Severity = new InspectionRpt
                              {
                                  Day = h.FbrihDtOfInsp.Value.Day,
                                  Month = h.FbrihDtOfInsp.Value.Month,
                                  Year = h.FbrihDtOfInsp.Value.Year,
                                  Distress = d.FbridWaterDownpipeDistress,
                                  Severity = d.FbridWaterDownpipeSeverity
                              },
                              Drainwater_Material = d.FbridWaterDownpipeCode,
                              ExpansionType = h.FbrihAiExpanType,
                              Expansion_Distress_Severity = new InspectionRpt
                              {
                                  Day = h.FbrihDtOfInsp.Value.Day,
                                  Month = h.FbrihDtOfInsp.Value.Month,
                                  Year = h.FbrihDtOfInsp.Value.Year,
                                  Distress = d.FbridExpanJointDistress,
                                  Severity = d.FbridExpanJointSeverity
                              },
                              Expansion_Material = d.FbridExpanJointCode,
                              GPSEasting = h.FbrihAiGpsEasting,
                              GPSNorthing = h.FbrihAiGpsNorthing,
                              InspectedByDate = h.FbrihSerProviderInsDt,
                              InspectedByDesignation = h.FbrihSerProviderUserDesignation,
                              InspectedByName = h.FbrihSerProviderUserName,
                              Kerb_Distress_Severity = new InspectionRpt
                              {
                                  Day = h.FbrihDtOfInsp.Value.Day,
                                  Month = h.FbrihDtOfInsp.Value.Month,
                                  Year = h.FbrihDtOfInsp.Value.Year,
                                  Distress = d.FbridSidewalksAppSlabDistress,
                                  Severity = d.FbridSidewalksAppSlabSeverity
                              },
                              Kerb_Material = d.FbridSidewalksAppSlabCode,
                              LaneWidth = h.FbrihAiWidthLane,
                              Median = h.FbrihAiMedian,
                              NoOfLane = h.FbrihAiLaneCnt,
                              NoOfSpan = h.FbrihAiSpanCnt,
                              NumberOfExpansion = h.FbrihAiExpanJointCount,
                              ParapetType = h.FbrihAiParapetType,
                              Parapet_Distress_Severity = new InspectionRpt
                              {
                                  Day = h.FbrihDtOfInsp.Value.Day,
                                  Month = h.FbrihDtOfInsp.Value.Month,
                                  Year = h.FbrihDtOfInsp.Value.Year,
                                  Distress = d.FbridParapetRailingDistress,
                                  Severity = d.FbridParapetRailingSeverity
                              },
                              Parapet_Material = d.FbridParapetRailingCode,
                              PartB_Serviceprovider = h.FbrihSerProviderDefObs,
                              PartC_Serviceprovider = h.FbrihSerProviderDefGenCom,
                              PartD_Feedback = h.FbrihSerProviderDefFeedback,
                              PartB_Consultant = h.FbrihAuthDefObs,
                              PartC_Consultant = h.FbrihAuthDefGenCom,
                              PartD_Consultant = h.FbrihAuthDefFeedback,
                              Piers_Connection_of_primary_components_Distresss_Severity = new InspectionRpt
                              {
                                  Day = h.FbrihDtOfInsp.Value.Day,
                                  Month = h.FbrihDtOfInsp.Value.Month,
                                  Year = h.FbrihDtOfInsp.Value.Year,
                                  Distress = d.FbridPiersPrimCompDistress,
                                  Severity = d.FbridPiersPrimCompSeverity
                              },
                              Piers_Connection_of_primary_components_Material = d.FbridPiersPrimCompMatCode,
                              PierType = h.FbrihAiPierType,
                              ReferenceNo = h.FbrihCInspRefNo,
                              RequireFurtherInvestigation = h.FbrihReqFurtherInv.HasValue ? h.FbrihReqFurtherInv.Value ? "Y" : "N" : "",
                              RiverName = h.FbrihAiRiverName,
                              Rmu = h.FbrihAiRmuName,
                              RoadCode = h.FbrihAiRdCode,
                              RoadName = h.FbrihAiRdName,
                              Signboard_Distress_Severity = new InspectionRpt
                              {
                                  Day = h.FbrihDtOfInsp.Value.Day,
                                  Month = h.FbrihDtOfInsp.Value.Month,
                                  Year = h.FbrihDtOfInsp.Value.Year,
                                  Distress = d.FbridUtilitiesDistress,
                                  Severity = d.FbridUtilitiesSeverity
                              },
                              Signboard_Material = d.FbridUtilitiesCode,
                              Slope_Distress_Severity = new InspectionRpt
                              {
                                  Day = h.FbrihDtOfInsp.Value.Day,
                                  Month = h.FbrihDtOfInsp.Value.Month,
                                  Year = h.FbrihDtOfInsp.Value.Year,
                                  Distress = d.FbridSlopeRetainWallDistress,
                                  Severity = d.FbridSlopeRetainWallSeverity
                              },
                              Slope_Material = d.FbridSlopeRetainWallCode,
                              SpanLength = h.FbrihAiLengthSpan,
                              StructureCode = h.FbrihAiStrucCode,
                              Superstructure = h.FbrihAiStrucSuper,
                              Walkway = h.FbrihAiWalkway,
                              Waterway_Distress_Severity = new InspectionRpt
                              {
                                  Day = h.FbrihDtOfInsp.Value.Day,
                                  Month = h.FbrihDtOfInsp.Value.Month,
                                  Year = h.FbrihDtOfInsp.Value.Year,
                                  Distress = d.FbridWaterwayDistress,
                                  Severity = d.FbridWaterwaySeverity
                              },
                              RatingRecordNo = h.FbrihRecordNo,
                              Waterway_Material = d.FbridWaterwayCode,
                              DateOfInspection = h.FbrihDtOfInsp,
                              PkRefNo = h.FbrihPkRefNo
                          }).ToList();
            string[] str = type.Select(s => s.DdlTypeDesc).ToArray();
            foreach (var d in detail)
            {
                d.Pictures = new List<Pictures>();
                var p = (from o in _context.RmFormB1b2BrInsImage
                         where o.FbriFbrihPkRefNo == d.PkRefNo && o.FbriActiveYn == true
                         && str.Contains(o.FbriImageTypeCode)
                         select new Pictures
                         {
                             ImageUrl = o.FbriImageUserFilePath,
                             Type = o.FbriImageTypeCode
                         }).ToList();
                foreach (var t in type)
                {
                    var picktures = p.Where(s => s.Type == t.DdlTypeDesc).ToList();
                    if (picktures == null || (picktures != null && picktures.Count == 0))
                    {
                        d.Pictures.Add(new Pictures { Type = t.DdlTypeDesc });
                        d.Pictures.Add(new Pictures { Type = t.DdlTypeDesc });
                    }
                    else if (picktures.Count < 2)
                    {
                        d.Pictures.AddRange(picktures);
                        d.Pictures.Add(new Pictures { Type = t.DdlTypeDesc });
                    }
                    else
                    {
                        d.Pictures.AddRange(picktures);
                    }
                }
            }
            return detail;
        }

        public async Task<AssetDDLResponseDTO> GetAssetDDL(AssetDDLRequestDTO request)
        {
            var result = new AssetDDLResponseDTO();
            if (string.IsNullOrWhiteSpace(request.RMU) && request.SectionCode == 0 && string.IsNullOrWhiteSpace(request.RdCode))
            {
                var rmu = await (from x in _context.RmAllassetInventory
                                 where x.AiAssetGrpCode == "BR" && x.AiActiveYn == true
                                 select new AssetDDLResponseDTO.DropDown
                                 {
                                     Value = x.AiRmuCode,
                                     Text = x.AiRmuName
                                 }).Distinct().OrderByDescending(x => x.Value).ToListAsync();

                var section = await (from x in _context.RmAllassetInventory
                                     where x.AiAssetGrpCode == "BR" && x.AiActiveYn == true
                                     select new AssetDDLResponseDTO.DropDown
                                     {
                                         Value = x.AiSecCode,
                                         Text = x.AiSecCode + "-" + x.AiSecName,
                                         Code = x.AiSecCode
                                     }).Distinct().ToListAsync();

                var roadCode = await (from x in _context.RmAllassetInventory
                                      let rd = _context.RmRoadMaster.FirstOrDefault(r => r.RdmRdCode == x.AiRdCode)
                                      where x.AiAssetGrpCode == "BR" && x.AiActiveYn == true
                                      select new AssetDDLResponseDTO.DropDown
                                      {
                                          Value = x.AiRdCode,
                                          Text = x.AiRdCode + "-" + x.AiRdName,
                                          CValue = x.AiRmuCode,
                                          Item1 = x.AiRdName,
                                          Code = x.AiRdCode,
                                          PKId = Convert.ToInt32(x.AiRdmPkRefNo),
                                          Item2 = x.AiSecCode.ToString(),
                                          Item3 = (rd.RdmLengthPaved + rd.RdmLengthUnpaved).ToString(),
                                      }).Distinct().ToListAsync();
                result.RMU = rmu;
                result.Section = section;
                result.RdCode = roadCode;

            }
            else if (!string.IsNullOrWhiteSpace(request.RMU) && request.SectionCode > 0 && string.IsNullOrWhiteSpace(request.RdCode))
            {
                var roadCode = await (from x in _context.RmAllassetInventory
                                      let rd = _context.RmRoadMaster.FirstOrDefault(r => r.RdmRdCode == x.AiRdCode)
                                      where x.AiAssetGrpCode == "BR" && x.AiActiveYn == true
                                      && (x.AiRmuCode == request.RMU || x.AiRmuName == request.RMU) && x.AiSecCode == request.SectionCode.ToString()
                                      select new AssetDDLResponseDTO.DropDown
                                      {
                                          Value = x.AiRdCode,
                                          Text = x.AiRdCode + "-" + x.AiRdName,
                                          CValue = x.AiRmuCode,
                                          Item1 = x.AiRdName,
                                          Code = x.AiRdCode,
                                          PKId = Convert.ToInt32(x.AiRdmPkRefNo),
                                          Item2 = x.AiSecCode.ToString(),
                                          Item3 = (rd.RdmLengthPaved + rd.RdmLengthUnpaved).ToString(),
                                      }).Distinct().ToListAsync();
                result.RdCode = roadCode;
            }
            else if (string.IsNullOrWhiteSpace(request.RMU) && request.SectionCode > 0 && !string.IsNullOrWhiteSpace(request.RdCode))
            {
                var rmu = await (from x in _context.RmAllassetInventory
                                 where x.AiAssetGrpCode == "BR" && x.AiActiveYn == true &&
                                 x.AiRdCode == request.RdCode && x.AiSecCode == request.SectionCode.ToString()
                                 select new AssetDDLResponseDTO.DropDown
                                 {
                                     Value = x.AiRmuCode,
                                     Text = x.AiRmuName
                                 }).Distinct().OrderByDescending(x => x.Value).ToListAsync();
                result.RMU = rmu;
            }
            else if (!string.IsNullOrWhiteSpace(request.RMU) && request.SectionCode == 0 && !string.IsNullOrWhiteSpace(request.RdCode))
            {
                var section = await (from x in _context.RmAllassetInventory
                                     where x.AiAssetGrpCode == "BR" && x.AiActiveYn == true && (x.AiRmuCode == request.RMU || x.AiRmuName == request.RMU) && x.AiRdCode == request.RdCode
                                     select new AssetDDLResponseDTO.DropDown
                                     {
                                         Value = x.AiSecCode,
                                         Text = x.AiSecCode.ToString() + "-" + x.AiSecName,
                                         Code = x.AiSecCode
                                     }).Distinct().ToListAsync();
                result.Section = section;
            }
            else if (!string.IsNullOrWhiteSpace(request.RMU) && request.SectionCode == 0 && string.IsNullOrWhiteSpace(request.RdCode))
            {
                var section = await (from x in _context.RmAllassetInventory
                                     where x.AiAssetGrpCode == "BR" && x.AiActiveYn == true && (x.AiRmuCode == request.RMU || x.AiRmuName == request.RMU)
                                     select new AssetDDLResponseDTO.DropDown
                                     {
                                         Value = x.AiSecCode,
                                         Text = x.AiSecCode.ToString() + "-" + x.AiSecName,
                                         Code = x.AiSecCode
                                     }).Distinct().ToListAsync();

                var roadCode = await (from x in _context.RmAllassetInventory
                                      let rd = _context.RmRoadMaster.FirstOrDefault(r => r.RdmRdCode == x.AiRdCode)
                                      where x.AiAssetGrpCode == "BR" && x.AiActiveYn == true && (x.AiRmuCode == request.RMU || x.AiRmuName == request.RMU)
                                      select new AssetDDLResponseDTO.DropDown
                                      {
                                          Value = x.AiRdCode,
                                          Text = x.AiRdCode + "-" + x.AiRdName,
                                          CValue = x.AiRmuCode,
                                          Item1 = x.AiRdName,
                                          Code = x.AiRdCode,
                                          PKId = Convert.ToInt32(x.AiRdmPkRefNo),
                                          Item2 = x.AiSecCode.ToString(),
                                          Item3 = (rd.RdmLengthPaved + rd.RdmLengthUnpaved).ToString(),
                                      }).Distinct().ToListAsync();
                result.Section = section;
                result.RdCode = roadCode;
            }
            else if (string.IsNullOrWhiteSpace(request.RMU) && request.SectionCode > 0 && string.IsNullOrWhiteSpace(request.RdCode))
            {
                var rmu = await (from x in _context.RmAllassetInventory
                                 where x.AiAssetGrpCode == "BR" && x.AiActiveYn == true && x.AiSecCode == request.SectionCode.ToString()
                                 select new AssetDDLResponseDTO.DropDown
                                 {
                                     Value = x.AiRmuCode,
                                     Text = x.AiRmuName
                                 }).Distinct().OrderByDescending(x => x.Value).ToListAsync();

                var roadCode = await (from x in _context.RmAllassetInventory
                                      let rd = _context.RmRoadMaster.FirstOrDefault(r => r.RdmRdCode == x.AiRdCode)
                                      where x.AiAssetGrpCode == "BR" && x.AiActiveYn == true && x.AiSecCode == request.SectionCode.ToString()
                                      select new AssetDDLResponseDTO.DropDown
                                      {
                                          Value = x.AiRdCode,
                                          Text = x.AiRdCode + "-" + x.AiRdName,
                                          CValue = x.AiRmuCode,
                                          Item1 = x.AiRdName,
                                          Code = x.AiRdCode,
                                          PKId = Convert.ToInt32(x.AiRdmPkRefNo),
                                          Item2 = x.AiSecCode.ToString(),
                                          Item3 = (rd.RdmLengthPaved + rd.RdmLengthUnpaved).ToString(),
                                      }).Distinct().ToListAsync();
                result.RMU = rmu;
                result.RdCode = roadCode;
            }
            else if (string.IsNullOrWhiteSpace(request.RMU) && request.SectionCode == 0 && !string.IsNullOrWhiteSpace(request.RdCode))
            {
                var section = await (from x in _context.RmAllassetInventory
                                     where x.AiAssetGrpCode == "BR" && x.AiActiveYn == true && x.AiRdCode == request.RdCode
                                     select new AssetDDLResponseDTO.DropDown
                                     {
                                         Value = x.AiSecCode.ToString(),
                                         Text = x.AiSecCode.ToString() + "-" + x.AiSecName
                                     }).Distinct().ToListAsync();

                var rmu = await (from x in _context.RmAllassetInventory
                                 where x.AiAssetGrpCode == "BR" && x.AiActiveYn == true && x.AiRdCode == request.RdCode
                                 select new AssetDDLResponseDTO.DropDown
                                 {
                                     Value = x.AiRmuCode,
                                     Text = x.AiRmuName
                                 }).Distinct().OrderByDescending(x => x.Value).ToListAsync();
                result.Section = section;
                result.RMU = rmu;
            }
            return result;
        }
    }
}
