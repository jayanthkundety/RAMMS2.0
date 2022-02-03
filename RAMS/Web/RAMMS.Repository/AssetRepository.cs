using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.Models;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.Domain;
using RAMS.Domain;
using EFCore.BulkExtensions;
using RAMMS.DTO.Wrappers;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using RAMMS.DTO.SearchBO;
using RAMMS.Common.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace RAMMS.Repository
{

    public class AssetRepository : RepositoryBase<RmAllassetInventory>, IAssetRepository
    {
        public AssetRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }
        private static byte[] data;
        public async Task<List<RmAllassetInventory>> GetFilteredRecordList(FilteredPagingDefinition<AssetSearch> filterOptions)
        {
            List<RmAllassetInventory> result = new List<RmAllassetInventory>();

            var query = (from x in _context.RmAllassetInventory where x.AiActiveYn == true select x);
            PrepareFilterQuery(filterOptions, ref query);

            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderBy(s => s.AiRdmPkRefNoNavigation.RdmRdCdSort);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(s => s.AiRmuName);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(s => s.AiRmuCode);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(s => s.AiSecCode);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(s => s.AiSecName);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(s => s.AiRdmPkRefNoNavigation.RdmRdCdSort);

                if (filterOptions.Filters.GroupCode == "CV")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderBy(s => s.AiLocChKm).ThenBy(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderBy(s => s.AiDiameter);
                    if (filterOptions.ColumnIndex == 9)
                        query = query.OrderBy(s => s.AiWidth);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderBy(s => s.AiHeight);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderBy(s => s.AiLength);
                    if (filterOptions.ColumnIndex == 12)
                        query = query.OrderBy(s => s.AiGrpType);
                    if (filterOptions.ColumnIndex == 13)
                        query = query.OrderBy(s => s.AiMaterial);
                    if (filterOptions.ColumnIndex == 14)
                        query = query.OrderBy(s => s.AiBarrelNo);
                    if (filterOptions.ColumnIndex == 15)
                        query = query.OrderBy(s => s.AiBound);
                }

                if (filterOptions.Filters.GroupCode == "BR")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderBy(s => s.AiRdName);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderBy(s => s.AiLocChKm).ThenBy(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 9)
                        query = query.OrderBy(s => s.AiBound);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderBy(s => s.AiBridgeName);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderBy(s => s.AiLength);
                    if (filterOptions.ColumnIndex == 12)
                        query = query.OrderBy(s => s.AiStrucCode);
                }
                if (filterOptions.Filters.GroupCode == "DR")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderBy(s => s.AiRdName);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderBy(s => s.AiLocChKm).ThenBy(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 9)
                        query = query.OrderBy(s => s.AiBound);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderBy(s => s.AiGrpType);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderBy(s => s.AiLength);
                }
                if (filterOptions.Filters.GroupCode == "DI")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderBy(s => s.AiRdName);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderBy(s => s.AiLocChKm).ThenBy(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 9)
                        query = query.OrderBy(s => s.AiBound);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderBy(s => s.AiGrpType);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderBy(s => s.AiLength);
                }
                if (filterOptions.Filters.GroupCode == "RS")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderBy(s => s.AiLocChKm).ThenBy(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderBy(s => s.AiFrmCh).ThenBy(s => s.AiFrmChDeci);
                    if (filterOptions.ColumnIndex == 9)
                        query = query.OrderBy(s => s.AiToCh).ThenBy(s => s.AiToChDeci);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderBy(s => s.AiGrpType);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderBy(s => s.AiBound);
                }
                if (filterOptions.Filters.GroupCode == "ELM" || filterOptions.Filters.GroupCode == "CLM")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderBy(s => s.AiLocChKm).ThenBy(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderBy(s => s.AiWidth);
                    if (filterOptions.ColumnIndex == 9)
                        query = query.OrderBy(s => s.AiLength);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderBy(s => s.AiGrpType);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderBy(s => s.AiBound);
                }
                if (filterOptions.Filters.GroupCode == "SH")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderBy(s => s.AiLocChKm).ThenBy(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderBy(s => s.AiFrmCh).ThenBy(s => s.AiFrmChDeci);
                    if (filterOptions.ColumnIndex == 9)
                        query = query.OrderBy(s => s.AiToCh).ThenBy(s => s.AiToChDeci);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderBy(s => s.AiBound);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderBy(s => s.AiGrpType);
                }
                if (filterOptions.Filters.GroupCode == "CW")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderBy(s => s.AiLocChKm).ThenBy(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderBy(s => s.AiFrmCh).ThenBy(s => s.AiFrmChDeci);
                    if (filterOptions.ColumnIndex == 9)
                        query = query.OrderBy(s => s.AiToCh).ThenBy(s => s.AiToChDeci);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderBy(s => s.AiBound);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderBy(s => s.AiGrpType);
                    if (filterOptions.ColumnIndex == 12)
                        query = query.OrderBy(s => s.AiLaneNo);
                }
                if (filterOptions.Filters.GroupCode == "GR")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderBy(s => s.AiLocChKm).ThenBy(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderBy(s => s.AiFrmCh).ThenBy(s => s.AiFrmChDeci);
                    if (filterOptions.ColumnIndex == 9)
                        query = query.OrderBy(s => s.AiToCh).ThenBy(s => s.AiToChDeci);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderBy(s => s.AiGrpType);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderBy(s => s.AiPostSpacing);
                    if (filterOptions.ColumnIndex == 12)
                        query = query.OrderBy(s => s.AiBound);
                }
                if (filterOptions.Filters.GroupCode == "SG")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderBy(s => s.AiLocChKm).ThenBy(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderBy(s => s.AiFrmCh).ThenBy(s => s.AiFrmChDeci);
                    if (filterOptions.ColumnIndex == 9)
                        query = query.OrderBy(s => s.AiToCh).ThenBy(s => s.AiToChDeci);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderBy(s => s.AiGrpType);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderBy(s => s.AiBound);
                }
                if (filterOptions.Filters.GroupCode == "RW")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderBy(s => s.AiLocChKm).ThenBy(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderBy(s => s.AiFrmCh).ThenBy(s => s.AiFrmChDeci);
                    if (filterOptions.ColumnIndex == 9)
                        query = query.OrderBy(s => s.AiToCh).ThenBy(s => s.AiToChDeci);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderBy(s => s.AiGrpType);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderBy(s => s.AiLength);
                    if (filterOptions.ColumnIndex == 12)
                        query = query.OrderBy(s => s.AiBound);
                }

            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 1)
                    query = query.OrderByDescending(s => s.AiRdmPkRefNoNavigation.RdmRdCdSort);
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(s => s.AiRmuName);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(s => s.AiRmuCode);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(s => s.AiSecCode);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(s => s.AiSecName);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(s => s.AiRdmPkRefNoNavigation.RdmRdCdSort);

                if (filterOptions.Filters.GroupCode == "CV")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderByDescending(s => s.AiLocChKm).ThenByDescending(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderByDescending(s => s.AiDiameter);
                    if (filterOptions.ColumnIndex == 9)
                        query = query.OrderByDescending(s => s.AiWidth);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderByDescending(s => s.AiHeight);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderByDescending(s => s.AiLength);
                    if (filterOptions.ColumnIndex == 12)
                        query = query.OrderByDescending(s => s.AiGrpType);
                    if (filterOptions.ColumnIndex == 13)
                        query = query.OrderByDescending(s => s.AiMaterial);
                    if (filterOptions.ColumnIndex == 14)
                        query = query.OrderByDescending(s => s.AiBarrelNo);
                    if (filterOptions.ColumnIndex == 15)
                        query = query.OrderByDescending(s => s.AiBound);
                }

                if (filterOptions.Filters.GroupCode == "BR")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderByDescending(s => s.AiRdName);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderByDescending(s => s.AiLocChKm).ThenByDescending(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 9)
                        query = query.OrderByDescending(s => s.AiBound);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderByDescending(s => s.AiBridgeName);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderByDescending(s => s.AiLength);
                    if (filterOptions.ColumnIndex == 12)
                        query = query.OrderByDescending(s => s.AiStrucCode);
                }
                if (filterOptions.Filters.GroupCode == "DR")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderByDescending(s => s.AiRdName);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderByDescending(s => s.AiLocChKm).ThenByDescending(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 9)
                        query = query.OrderByDescending(s => s.AiBound);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderByDescending(s => s.AiGrpType);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderByDescending(s => s.AiLength);
                }
                if (filterOptions.Filters.GroupCode == "DI")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderByDescending(s => s.AiRdName);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderByDescending(s => s.AiLocChKm).ThenByDescending(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 9)
                        query = query.OrderByDescending(s => s.AiBound);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderByDescending(s => s.AiGrpType);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderByDescending(s => s.AiLength);
                }
                if (filterOptions.Filters.GroupCode == "RS")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderByDescending(s => s.AiLocChKm).ThenByDescending(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderByDescending(s => s.AiFrmCh).ThenByDescending(s => s.AiFrmChDeci);
                    if (filterOptions.ColumnIndex == 9)
                        query = query.OrderByDescending(s => s.AiToCh).ThenByDescending(s => s.AiToChDeci);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderByDescending(s => s.AiGrpType);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderByDescending(s => s.AiBound);
                }
                if (filterOptions.Filters.GroupCode == "ELM" || filterOptions.Filters.GroupCode == "CLM")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderByDescending(s => s.AiLocChKm).ThenByDescending(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderByDescending(s => s.AiWidth);
                    if (filterOptions.ColumnIndex == 9)
                        query = query.OrderByDescending(s => s.AiLength);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderByDescending(s => s.AiGrpType);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderByDescending(s => s.AiBound);
                }
                if (filterOptions.Filters.GroupCode == "SH")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderByDescending(s => s.AiLocChKm).ThenByDescending(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderByDescending(s => s.AiFrmCh).ThenByDescending(s => s.AiFrmChDeci);
                    if (filterOptions.ColumnIndex == 9)
                        query = query.OrderByDescending(s => s.AiToCh).ThenByDescending(s => s.AiToChDeci);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderByDescending(s => s.AiBound);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderByDescending(s => s.AiGrpType);
                }
                if (filterOptions.Filters.GroupCode == "CW")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderByDescending(s => s.AiLocChKm).ThenByDescending(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderByDescending(s => s.AiFrmCh).ThenByDescending(s => s.AiFrmChDeci);
                    if (filterOptions.ColumnIndex == 9)
                        query = query.OrderByDescending(s => s.AiToCh).ThenByDescending(s => s.AiToChDeci);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderByDescending(s => s.AiBound);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderByDescending(s => s.AiGrpType);
                    if (filterOptions.ColumnIndex == 12)
                        query = query.OrderByDescending(s => s.AiLaneNo);
                }
                if (filterOptions.Filters.GroupCode == "GR")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderByDescending(s => s.AiLocChKm).ThenByDescending(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderByDescending(s => s.AiFrmCh).ThenByDescending(s => s.AiFrmChDeci);
                    if (filterOptions.ColumnIndex == 9)
                        query = query.OrderByDescending(s => s.AiToCh).ThenByDescending(s => s.AiToChDeci);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderByDescending(s => s.AiGrpType);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderByDescending(s => s.AiPostSpacing);
                    if (filterOptions.ColumnIndex == 12)
                        query = query.OrderByDescending(s => s.AiBound);
                }
                if (filterOptions.Filters.GroupCode == "SG")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderByDescending(s => s.AiLocChKm).ThenByDescending(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderByDescending(s => s.AiFrmCh).ThenByDescending(s => s.AiFrmChDeci);
                    if (filterOptions.ColumnIndex == 9)
                        query = query.OrderByDescending(s => s.AiToCh).ThenByDescending(s => s.AiToChDeci);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderByDescending(s => s.AiGrpType);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderByDescending(s => s.AiBound);
                }
                if (filterOptions.Filters.GroupCode == "RW")
                {
                    if (filterOptions.ColumnIndex == 7)
                        query = query.OrderByDescending(s => s.AiLocChKm).ThenByDescending(s => s.AiLocChM);
                    if (filterOptions.ColumnIndex == 8)
                        query = query.OrderByDescending(s => s.AiFrmCh).ThenByDescending(s => s.AiFrmChDeci);
                    if (filterOptions.ColumnIndex == 9);
                        query = query.OrderByDescending(s => s.AiToCh).ThenByDescending(s => s.AiToChDeci);
                    if (filterOptions.ColumnIndex == 10)
                        query = query.OrderByDescending(s => s.AiGrpType);
                    if (filterOptions.ColumnIndex == 11)
                        query = query.OrderByDescending(s => s.AiLength);
                    if (filterOptions.ColumnIndex == 12)
                        query = query.OrderByDescending(s => s.AiBound);
                }

            }

            result = await query.Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            return result;
        }

        public async Task<AssetDDLResponseDTO> GetFilteredList(AssetDDLRequestDTO roadMaster)
        {
            var result = new AssetDDLResponseDTO();
            if (string.IsNullOrWhiteSpace(roadMaster.RMU) && roadMaster.SectionCode == 0 && string.IsNullOrWhiteSpace(roadMaster.RdCode))
            {
                var rmu = await (from x in _context.RmAllassetInventory
                                 where x.AiAssetGrpCode == ((roadMaster.GrpCode == null || roadMaster.GrpCode == "") ? x.AiAssetGrpCode : x.AiAssetGrpCode) && x.AiActiveYn == true
                                 select new AssetDDLResponseDTO.DropDown
                                 {
                                     Value = x.AiRmuCode,
                                     Text = x.AiRmuCode + "-" + (x.AiRmuName == "MIRI" ? "Miri" : (x.AiRmuName == "BATU NIAH" ? "Batu Niah" : x.AiRmuName))
                                 }).Distinct().OrderByDescending(x => x.Value).ToListAsync();

                var section = await (from x in _context.RmAllassetInventory
                                     where x.AiAssetGrpCode == ((roadMaster.GrpCode == null || roadMaster.GrpCode == "") ? x.AiAssetGrpCode : x.AiAssetGrpCode) && x.AiActiveYn == true
                                     select new AssetDDLResponseDTO.DropDown
                                     {
                                         Value = x.AiSecName,
                                         Text = x.AiSecCode + "-" + x.AiSecName,
                                         Code = x.AiSecCode
                                     }).Distinct().OrderBy(x => x.Code).ToListAsync();

                var roadCode = await (from x in _context.RmAllassetInventory
                                      let rd = _context.RmRoadMaster.FirstOrDefault(r => r.RdmRdCode == x.AiRdCode)
                                      where x.AiAssetGrpCode == ((roadMaster.GrpCode == null || roadMaster.GrpCode == "") ? x.AiAssetGrpCode : x.AiAssetGrpCode) && x.AiActiveYn == true
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
            else if (!string.IsNullOrWhiteSpace(roadMaster.RMU) && roadMaster.SectionCode > 0 && string.IsNullOrWhiteSpace(roadMaster.RdCode))
            {
                var roadCode = await (from x in _context.RmAllassetInventory
                                      let rd = _context.RmRoadMaster.FirstOrDefault(r => r.RdmRdCode == x.AiRdCode)
                                      where x.AiAssetGrpCode == ((roadMaster.GrpCode == null || roadMaster.GrpCode == "") ? x.AiAssetGrpCode : x.AiAssetGrpCode) && x.AiActiveYn == true
                                      && x.AiRmuCode == roadMaster.RMU && x.AiSecCode == roadMaster.SectionCode.ToString()
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
            else if (string.IsNullOrWhiteSpace(roadMaster.RMU) && roadMaster.SectionCode > 0 && !string.IsNullOrWhiteSpace(roadMaster.RdCode))
            {
                var rmu = await (from x in _context.RmAllassetInventory
                                 where x.AiAssetGrpCode == ((roadMaster.GrpCode == null || roadMaster.GrpCode == "") ? x.AiAssetGrpCode : x.AiAssetGrpCode) && x.AiActiveYn == true &&
                                 x.AiRdCode == roadMaster.RdCode && x.AiSecCode == roadMaster.SectionCode.ToString()
                                 select new AssetDDLResponseDTO.DropDown
                                 {
                                     Value = x.AiRmuCode,
                                     Text = x.AiRmuCode + "-" + (x.AiRmuName == "MIRI" ? "Miri" : (x.AiRmuName == "BATU NIAH" ? "Batu Niah" : x.AiRmuName))
                                 }).Distinct().OrderByDescending(x => x.Value).ToListAsync();
                result.RMU = rmu;
            }
            else if (!string.IsNullOrWhiteSpace(roadMaster.RMU) && roadMaster.SectionCode == 0 && !string.IsNullOrWhiteSpace(roadMaster.RdCode))
            {
                var section = await (from x in _context.RmAllassetInventory
                                     where x.AiAssetGrpCode == ((roadMaster.GrpCode == null || roadMaster.GrpCode == "") ? x.AiAssetGrpCode : x.AiAssetGrpCode) && x.AiActiveYn == true && x.AiRmuCode == roadMaster.RMU && x.AiRdCode == roadMaster.RdCode
                                     select new AssetDDLResponseDTO.DropDown
                                     {
                                         Value = x.AiSecName,
                                         Text = x.AiSecCode + "-" + x.AiSecName,
                                         Code = x.AiSecCode
                                     }).Distinct().OrderBy(x => x.Code).ToListAsync();
                result.Section = section;
            }
            else if (!string.IsNullOrWhiteSpace(roadMaster.RMU) && roadMaster.SectionCode == 0 && string.IsNullOrWhiteSpace(roadMaster.RdCode))
            {
                var section = await (from x in _context.RmAllassetInventory
                                     where x.AiAssetGrpCode == ((roadMaster.GrpCode == null || roadMaster.GrpCode == "") ? x.AiAssetGrpCode : x.AiAssetGrpCode) && x.AiActiveYn == true && x.AiRmuCode == roadMaster.RMU
                                     select new AssetDDLResponseDTO.DropDown
                                     {
                                         Value = x.AiSecName,
                                         Text = x.AiSecCode + "-" + x.AiSecName,
                                         Code = x.AiSecCode
                                     }).Distinct().OrderBy(x => x.Code).ToListAsync();

                var roadCode = await (from x in _context.RmAllassetInventory
                                      let rd = _context.RmRoadMaster.FirstOrDefault(r => r.RdmRdCode == x.AiRdCode)
                                      where x.AiAssetGrpCode == ((roadMaster.GrpCode == null || roadMaster.GrpCode == "") ? x.AiAssetGrpCode : x.AiAssetGrpCode) && x.AiActiveYn == true && x.AiRmuCode == roadMaster.RMU
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
            else if (string.IsNullOrWhiteSpace(roadMaster.RMU) && roadMaster.SectionCode > 0 && string.IsNullOrWhiteSpace(roadMaster.RdCode))
            {
                var rmu = await (from x in _context.RmAllassetInventory
                                 where x.AiAssetGrpCode == ((roadMaster.GrpCode == null || roadMaster.GrpCode == "") ? x.AiAssetGrpCode : x.AiAssetGrpCode) && x.AiActiveYn == true && x.AiSecCode == roadMaster.SectionCode.ToString()
                                 select new AssetDDLResponseDTO.DropDown
                                 {
                                     Value = x.AiRmuCode,
                                     Text = x.AiRmuCode + "-" + (x.AiRmuName == "MIRI" ? "Miri" : (x.AiRmuName == "BATU NIAH" ? "Batu Niah" : x.AiRmuName))
                                 }).Distinct().OrderByDescending(x => x.Value).ToListAsync();

                var roadCode = await (from x in _context.RmAllassetInventory
                                      let rd = _context.RmRoadMaster.FirstOrDefault(r => r.RdmRdCode == x.AiRdCode)
                                      where x.AiAssetGrpCode == ((roadMaster.GrpCode == null || roadMaster.GrpCode == "") ? x.AiAssetGrpCode : x.AiAssetGrpCode) && x.AiActiveYn == true && x.AiSecCode == roadMaster.SectionCode.ToString()
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
            else if (string.IsNullOrWhiteSpace(roadMaster.RMU) && roadMaster.SectionCode == 0 && !string.IsNullOrWhiteSpace(roadMaster.RdCode))
            {
                var section = await (from x in _context.RmAllassetInventory
                                     where x.AiAssetGrpCode == ((roadMaster.GrpCode == null || roadMaster.GrpCode == "") ? x.AiAssetGrpCode : x.AiAssetGrpCode) && x.AiActiveYn == true && x.AiRdCode == roadMaster.RdCode
                                     select new AssetDDLResponseDTO.DropDown
                                     {
                                         Value = x.AiSecName,
                                         Text = x.AiSecCode + "-" + x.AiSecName,
                                         Code = x.AiSecCode
                                     }).Distinct().OrderBy(x => x.Code).ToListAsync();

                var rmu = await (from x in _context.RmAllassetInventory
                                 where x.AiAssetGrpCode == ((roadMaster.GrpCode == null || roadMaster.GrpCode == "") ? x.AiAssetGrpCode : x.AiAssetGrpCode) && x.AiActiveYn == true && x.AiRdCode == roadMaster.RdCode
                                 select new AssetDDLResponseDTO.DropDown
                                 {
                                     Value = x.AiRmuCode,
                                     Text = x.AiRmuCode + "-" + (x.AiRmuName == "MIRI" ? "Miri" : (x.AiRmuName == "BATU NIAH" ? "Batu Niah" : x.AiRmuName))
                                 }).Distinct().OrderByDescending(x => x.Value).ToListAsync();
                result.Section = section;
                result.RMU = rmu;
            }
            return result;
        }

        public async Task<List<RmAllassetInventory>> GetFilteredRecordListWithOthers(FilteredPagingDefinition<AssetSearch> filterOptions)
        {
            List<RmAllassetInventory> result = new List<RmAllassetInventory>();

            var query = await _context.RmAllassetInventory
                                .Where(x => x.AiActiveYn == true && x.AiAssetGrpCode == filterOptions.Filters.GroupCode)
                                .Include(x => x.RmAllassetInvOthers).ToListAsync();

            result = query.Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage).ToList();

            return result;
        }

        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<AssetSearch> filterOptions)
        {
            var query = (from x in _context.RmAllassetInventory select x);

            PrepareFilterQuery(filterOptions, ref query);

            return await query.CountAsync().ConfigureAwait(false);
        }

        private void PrepareFilterQuery(FilteredPagingDefinition<AssetSearch> filterOptions, ref IQueryable<RmAllassetInventory> query)
        {
            string groupCode = filterOptions.Filters.GroupCode;
            query = query.Where(x => x.AiActiveYn == true).OrderByDescending(x => x.AiPkRefNo); 
            if (filterOptions.Filters != null)
            {
                if (!string.IsNullOrEmpty(filterOptions.Filters.GroupCode))
                {
                    query = query.Where(x => x.AiAssetGrpCode == filterOptions.Filters.GroupCode);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.AssetId))
                {
                    query = query.Where(x => x.AiAssetId == filterOptions.Filters.AssetId);
                }

                if (filterOptions.Filters.FromCh.HasValue || filterOptions.Filters.FromChDesi != null)

                {
                    query = query.Where(x => Convert.ToDouble(x.AiLocChKm.ToString() + '.' + x.AiLocChM) >= Convert.ToDouble((filterOptions.Filters.FromCh.ToString() + '.' + filterOptions.Filters.FromChDesi)));
                }

                if (filterOptions.Filters.ToCh.HasValue || filterOptions.Filters.ToChDeci != null)
                {
                    query = query.Where(x => Convert.ToDouble(x.AiLocChKm.ToString() + '.' + x.AiLocChM) <= Convert.ToDouble(filterOptions.Filters.ToCh.ToString() + '.' + filterOptions.Filters.ToChDeci));
                }
                if (!string.IsNullOrEmpty(filterOptions.Filters.RMUCode))
                {
                    query = query.Where(x => x.AiRmuCode == filterOptions.Filters.RMUCode);
                }
                if (!string.IsNullOrEmpty(filterOptions.Filters.RMUName))
                {
                    query = query.Where(x => x.AiRmuName == filterOptions.Filters.RMUName);
                }
                if (!string.IsNullOrEmpty(filterOptions.Filters.GroupType))
                {
                    query = query.Where(x => x.AiGrpType == filterOptions.Filters.GroupType);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.SectionName))
                {
                    query = query.Where(x => x.AiSecName == filterOptions.Filters.SectionName);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.SectionCode))
                {
                    query = query.Where(x => x.AiSecCode == filterOptions.Filters.SectionCode);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RoadName))
                {
                    query = query.Where(x => x.AiRdName == filterOptions.Filters.RoadName);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RoadCode))
                {
                    query = query.Where(x => x.AiRdCode == filterOptions.Filters.RoadCode);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.Bound))
                {
                    query = query.Where(x => x.AiBound == filterOptions.Filters.Bound);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    if (groupCode == "BR" || groupCode == "DR" || groupCode == "DI")
                    {
                        query = query.Where(x => x.AiAssetId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.AiRmuCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.AiRmuName.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.AiSecCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.AiSecName.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.AiRdCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.AiRdName.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.AiBridgeName.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.AiGrpType.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.AiBound.Contains(filterOptions.Filters.SmartInputValue)
                                        || (filterOptions.Filters.SmartInputValue.IsDouble() && x.AiLength.Equals(filterOptions.Filters.SmartInputValue.AsDouble()))
                                        || ((x.AiLocChKm.HasValue ? x.AiLocChKm.Value : 0).ToString() +
                                        "."
                                        + x.AiLocChM)
                                        .Contains(filterOptions.Filters.SmartInputValue)
                                        );
                    }
                    else if (groupCode == "ELM" || groupCode == "CLM")
                    {
                        query = query.Where(x => x.AiAssetId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.AiRmuCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.AiRmuName.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.AiSecCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.AiSecName.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.AiRdCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.AiGrpType.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.AiBound.Contains(filterOptions.Filters.SmartInputValue)
                                        || (filterOptions.Filters.SmartInputValue.IsDouble() && x.AiWidth.Equals(filterOptions.Filters.SmartInputValue.AsDouble()))
                                        || (filterOptions.Filters.SmartInputValue.IsDouble() && x.AiLength.Equals(filterOptions.Filters.SmartInputValue.AsDouble()))
                                        || ((x.AiLocChKm.HasValue ? x.AiLocChKm.Value : 0).ToString() +
                                        "."
                                        + x.AiLocChM)
                                        .Contains(filterOptions.Filters.SmartInputValue)
                                        );
                    }
                    else if (groupCode == "GR" || groupCode == "SG" || groupCode == "RS" || groupCode == "SH" || groupCode == "CW")
                    {
                        query = query.Where(x => x.AiAssetId.Contains(filterOptions.Filters.SmartInputValue)
                                     || x.AiRmuCode.Contains(filterOptions.Filters.SmartInputValue)
                                     || x.AiRmuName.Contains(filterOptions.Filters.SmartInputValue)
                                     || x.AiSecCode.Contains(filterOptions.Filters.SmartInputValue)
                                     || x.AiSecName.Contains(filterOptions.Filters.SmartInputValue)
                                     || x.AiRdCode.Contains(filterOptions.Filters.SmartInputValue)
                                     || x.AiGrpType.Contains(filterOptions.Filters.SmartInputValue)
                                     || x.AiBound.Contains(filterOptions.Filters.SmartInputValue)
                                     || (filterOptions.Filters.SmartInputValue.IsDouble() && x.AiPostSpacing.Equals(filterOptions.Filters.SmartInputValue.AsDouble()))
                                        || ((x.AiFrmCh.HasValue ? x.AiFrmCh.Value : 0).ToString() +
                                        "."
                                        + x.AiFrmChDeci)
                                        .Contains(filterOptions.Filters.SmartInputValue)

                                        || ((x.AiLocChKm.HasValue ? x.AiLocChKm.Value : 0).ToString() +
                                        "."
                                        + x.AiLocChM)
                                        .Contains(filterOptions.Filters.SmartInputValue)

                                     || ((x.AiToCh.HasValue ? x.AiToCh.Value : 0).ToString() +
                                     "."
                                     + x.AiToChDeci)
                                     .Contains(filterOptions.Filters.SmartInputValue)

                                     );
                    }
                    else if (groupCode == "RW")
                    {
                        query = query.Where(x => x.AiAssetId.Contains(filterOptions.Filters.SmartInputValue)
                                   || x.AiRmuCode.Contains(filterOptions.Filters.SmartInputValue)
                                   || x.AiRmuName.Contains(filterOptions.Filters.SmartInputValue)
                                   || x.AiSecCode.Contains(filterOptions.Filters.SmartInputValue)
                                   || x.AiSecName.Contains(filterOptions.Filters.SmartInputValue)
                                   || x.AiRdCode.Contains(filterOptions.Filters.SmartInputValue)
                                   || x.AiGrpType.Contains(filterOptions.Filters.SmartInputValue)
                                   || x.AiBound.Contains(filterOptions.Filters.SmartInputValue)
                                   || (filterOptions.Filters.SmartInputValue.IsDouble() && x.AiLength.Equals(filterOptions.Filters.SmartInputValue.AsDouble()))
                                   || ((x.AiFrmCh.HasValue ? x.AiFrmCh.Value : 0).ToString() +
                                      "."
                                      + x.AiFrmChDeci)
                                      .Contains(filterOptions.Filters.SmartInputValue)

                                   || ((x.AiLocChKm.HasValue ? x.AiLocChKm.Value : 0).ToString() +
                                      "."
                                      + x.AiLocChM)
                                      .Contains(filterOptions.Filters.SmartInputValue)

                                   || ((x.AiToCh.HasValue ? x.AiToCh.Value : 0).ToString() +
                                   "."
                                   + x.AiToChDeci)
                                   .Contains(filterOptions.Filters.SmartInputValue)

                                   );
                    }
                    else if (groupCode == "CV")
                    {
                        query = query.Where(x => x.AiAssetId.Contains(filterOptions.Filters.SmartInputValue)
                                   || x.AiRmuCode.Contains(filterOptions.Filters.SmartInputValue)
                                   || x.AiRmuName.Contains(filterOptions.Filters.SmartInputValue)
                                   || x.AiSecCode.Contains(filterOptions.Filters.SmartInputValue)
                                   || x.AiSecName.Contains(filterOptions.Filters.SmartInputValue)
                                   || x.AiRdCode.Contains(filterOptions.Filters.SmartInputValue)
                                   || x.AiGrpType.Contains(filterOptions.Filters.SmartInputValue)
                                   || x.AiBound.Contains(filterOptions.Filters.SmartInputValue)
                                   || (filterOptions.Filters.SmartInputValue.IsDouble() && x.AiLength.Equals(filterOptions.Filters.SmartInputValue.AsDouble()))
                                   || (filterOptions.Filters.SmartInputValue.IsDouble() && x.AiDiameter.Equals(filterOptions.Filters.SmartInputValue.AsDouble()))
                                   || (filterOptions.Filters.SmartInputValue.IsDouble() && x.AiWidth.Equals(filterOptions.Filters.SmartInputValue.AsDouble()))
                                   || (filterOptions.Filters.SmartInputValue.IsDouble() && x.AiHeight.Equals(filterOptions.Filters.SmartInputValue.AsDouble()))
                                   || x.AiMaterial.Contains(filterOptions.Filters.SmartInputValue)
                                   || (filterOptions.Filters.SmartInputValue.IsInt() && x.AiBarrelNo.Equals(filterOptions.Filters.SmartInputValue.AsInt()))
                                   || ((x.AiLocChKm.HasValue ? x.AiLocChKm.Value : 0).ToString() +
                                      "."
                                      + x.AiLocChM)
                                      .Contains(filterOptions.Filters.SmartInputValue)

                                   );
                    }
                }
            }
        }

        public string GetAssetNameByCode(string assetCode)
        {
            var dt = _context.RmDdLookup.FirstOrDefault(s => s.DdlTypeCode == "Asset_Display" && s.DdlTypeValue == assetCode);
            if (dt != null)
                return dt.DdlTypeDesc;
            else
                return "";
        }

        public async Task<(string, List<SelectListItem>)> SaveAssetImport(DataTable data, string exportLocation, string localLocation, string tempLocation, int userId)
        {
            try
            {
                string rowsAffected = "";
                List<AssertImportDescription> importDescription = new List<AssertImportDescription>();
                List<ProcessedDescription> processedDescription = new List<ProcessedDescription>();
                List<ProcessedIdentification> processedIdentifications = new List<ProcessedIdentification>();
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                       {"@userid", userId },
                       {"@AI_SUBMIT_STS", true },
                       { "@AI_Active_YN", true },
                       {"@file_no",data.Rows[0]["AI_File_No"] }
                };
                var dataSet = GetDataSet("proc_iu_allassets_demo", System.Data.CommandType.StoredProcedure, param);
                if (dataSet.Tables != null && dataSet.Tables.Count > 0)
                {

                    importDescription = dataSet.Tables[0].ToObject<AssertImportDescription>().ToList();
                    processedDescription = dataSet.Tables[1].ToObject<ProcessedDescription>().ToList();
                    processedIdentifications = dataSet.Tables[2].ToObject<ProcessedIdentification>().ToList();
                }
                rowsAffected = rowsAffected + Convert.ToString(processedDescription[0].is_success) + "," + Convert.ToString(processedDescription[0].is_failure) + "," + Convert.ToString(processedDescription[0].is_Inserted + "," + Convert.ToString(processedDescription[0].is_Updated + ","));
                String cellValue = Regex.Replace(data.Rows[0]["AI_Asset_ID_actual"].ToString(), @"[^0-9a-zA-Z]+", "");
                List<SelectListItem> item = new List<SelectListItem>();
                foreach (DataRow row in data.Rows)
                {
                    if (row["AI_Has_Image"] != null && row["AI_Has_Image"].ToString().ToLower() == "y")
                    {
                        var res = row["AI_Asset_ID"].ToString();
                        if (res != null && res != "")
                        {
                            string assertGroupCode = "";
                            if (row["AI_Asset_GRP_Code"].ToString() == "BR") assertGroupCode = "Bridge";
                            else if (row["AI_Asset_GRP_Code"].ToString() == "CLM") assertGroupCode = "Centre Line Marking";
                            else if (row["AI_Asset_GRP_Code"].ToString() == "CV") assertGroupCode = "Culvert";
                            else if (row["AI_Asset_GRP_Code"].ToString() == "CW") assertGroupCode = "Carriageway";
                            else if (row["AI_Asset_GRP_Code"].ToString() == "DI") assertGroupCode = "Ditch";
                            else if (row["AI_Asset_GRP_Code"].ToString() == "DR") assertGroupCode = "Drain";
                            else if (row["AI_Asset_GRP_Code"].ToString() == "ELM") assertGroupCode = "Edge Line Marking";
                            else if (row["AI_Asset_GRP_Code"].ToString() == "GR") assertGroupCode = "Guardrail";
                            else if (row["AI_Asset_GRP_Code"].ToString() == "RS") assertGroupCode = "Road Stud";
                            else if (row["AI_Asset_GRP_Code"].ToString() == "RW") assertGroupCode = "Retaining Wall";
                            else if (row["AI_Asset_GRP_Code"].ToString() == "SG") assertGroupCode = "Signs";
                            else if (row["AI_Asset_GRP_Code"].ToString() == "SH") assertGroupCode = "Shoulder";
                            string tempResult = "";
                            ProcessedIdentification t1 = processedIdentifications.Where(x => x.AI_Asset_ID == row["AI_Asset_ID"].ToString()).FirstOrDefault();
                            {
                                if (t1 != null)
                                {
                                    tempResult = await FilesUpload(exportLocation, Regex.Replace(row["AI_Asset_ID_actual"].ToString(), @"[^0-9a-zA-Z]+", ""), assertGroupCode, localLocation, t1.AI_PK_Ref_No, tempLocation, row["AI_Asset_GRP_Code"].ToString());
                                }
                            }
                            if (tempResult != "Files Uploaded" && tempResult != null && tempResult != "")
                            {

                                item.Add(new SelectListItem() { Text = tempResult, Value = row["AI_SRNO"].ToString() });
                            }
                        }
                    }
                }

                return (rowsAffected, item);
            }
            catch (Exception ex)
            {
                return ("Failed", null);
            }
        }
        public async Task<List<AssetFieldDtl>> GetAssetFieldDtls(AssetFieldDtlReqDTO filterOptions)
        {
            return await _context.AssetFieldDtl.Where(x => x.Code == filterOptions.Code).OrderBy(x => x.AssetPkId).ToListAsync().ConfigureAwait(false);
        }


        public async Task<bool> AssetImportRemove(Guid fileId)
        {

            return true;
        }

        public async Task<List<RmAssetImageDtl>> GetImageDTLByAssetId(int id)
        {
            return await _context.RmAssetImageDtl.Where(x => x.AidAiPkRefNo == id && x.AidActiveYn == true).ToListAsync().ConfigureAwait(false);
        }


        public async Task<List<AssetImport>> GetAssetImports(Guid fileId)
        {
            try
            {
                return await _context.AssetImport.Where(x => x.AiFileNo == fileId).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<RmAssetImageDtl>> GetAssetImageDtls()
        {
            return await _context.RmAssetImageDtl.ToListAsync().ConfigureAwait(false);
        }
        private async Task<string> FilesUpload(string exportLocation, string cellValue, string assertGroupCode, string localLocation, int assetId, string tempLocation, string typeCode)
        {
            try
            {
                RmAssetImgDtlRepository rmAssetImgDtlRepository;
                List<ImageListRequestDTO> uploadedFiles = new List<ImageListRequestDTO>();

                string[] allFiles = System.IO.Directory.GetFiles(localLocation + "\\" + assertGroupCode + "\\" + cellValue, "*.*", System.IO.SearchOption.AllDirectories);

                List<RmDdLookup> ddLookUps = _context.RmDdLookup.Where(x => x.DdlType == "Photo Type" && x.DdlTypeCode == typeCode).ToList();

                string res = "";
                int i = 1;
                if (allFiles != null && allFiles.Count() > 0)
                {
                    foreach (var sFile in allFiles)
                    {
                        int j = 0;
                        FileInfo _file = new FileInfo(sFile);
                        string type = _file.Name.Split('.')[1].ToLower();
                        string imgTypes = ".png.jpg.jpeg";
                        string othersTypes = ".png.jpg.jpeg.csv.docx.doc.xlsx.xls.pdf";
                        string photoType = _file.Directory.ToString().Replace(localLocation + "\\" + assertGroupCode + "\\" + cellValue + "\\", "").ToLower();
                        double MbSize = (_file.Length / 1024f) / 1024f;
                        if ((photoType != "others" && MbSize < 5) || (photoType == "others" && MbSize < 25) || (photoType == "others" && imgTypes.Contains(type) && MbSize < 5))
                        {
                            foreach (RmDdLookup ddLookUp in ddLookUps)
                            {
                                if (sFile.ToLower().Contains(localLocation.ToLower() + "\\" + assertGroupCode.ToLower() + "\\" + cellValue.ToLower() + "\\" + ddLookUp.DdlTypeDesc.ToLower() + "\\"))
                                {
                                    RmAssetImageDtl rmAssetImageDtl = new RmAssetImageDtl();
                                    rmAssetImageDtl.AidAiPkRefNo = assetId;
                                    if ((imgTypes.Contains(type) && photoType != "others") || (othersTypes.Contains(type) && photoType == "others"))
                                    {
                                        _file.DirectoryName.Replace(localLocation.Replace(tempLocation, ""), exportLocation);
                                        if (!System.IO.Directory.Exists(_file.DirectoryName.Replace(localLocation + "\\" + tempLocation, exportLocation)))
                                        {
                                            System.IO.Directory.CreateDirectory(_file.DirectoryName.Replace(localLocation + "\\" + tempLocation, exportLocation));
                                        }
                                        rmAssetImageDtl.AidImageTypeCode = _file.Directory.ToString().Replace(localLocation + "\\" + assertGroupCode + "\\" + cellValue + "\\", "");
                                        rmAssetImageDtl.AidImageSrno = i;
                                        if (i < 10)
                                        {
                                            rmAssetImageDtl.AidImageFilenameSys = assetId + "_" + rmAssetImageDtl.AidImageTypeCode + "_" + "00" + i;
                                        }
                                        else if (i >= 10 && i < 100)
                                        {
                                            rmAssetImageDtl.AidImageFilenameSys = assetId + "_" + rmAssetImageDtl.AidImageTypeCode + "_" + "0" + i;
                                        }
                                        else
                                        {
                                            rmAssetImageDtl.AidImageFilenameSys = assetId + "_" + rmAssetImageDtl.AidImageTypeCode + "_" + i;
                                        }
                                        rmAssetImageDtl.AidImageFilenameUpload = _file.FullName.Replace(localLocation + "\\", "");
                                        rmAssetImageDtl.AidSubmitSts = false;
                                        rmAssetImageDtl.AidActiveYn = true;
                                        rmAssetImageDtl.AidImageUserFilePath = _file.Name;
                                        rmAssetImageDtl.AidCrDt = DateTime.UtcNow;
                                        if (System.IO.File.Exists(_file.FullName.Replace(localLocation, exportLocation)))
                                        {
                                            System.IO.File.Delete(_file.FullName.Replace(localLocation, exportLocation));
                                        }

                                        if (!System.IO.Directory.Exists(_file.DirectoryName.Replace(localLocation, exportLocation)))
                                        {
                                            System.IO.Directory.CreateDirectory(_file.DirectoryName.Replace(localLocation, exportLocation));
                                        }

                                        await FilemovetoUploadLocation(_file.FullName, _file.FullName.Replace(localLocation, exportLocation));
                                        _context.RmAssetImageDtl.Add(rmAssetImageDtl);
                                        await _context.SaveChangesAsync();
                                        i = i + 1;
                                        j = j + 1;
                                    }
                                    else
                                    {
                                        if (photoType != "others") res += "Invalid extension! On the Type " + photoType + " You can only upload jpg/jpeg/png files. ";
                                        else res += "Invalid extension! On the Type " + photoType + " You can only upload pdf/excel/doc/csv/jpg/jpeg/png files.";
                                    }

                                }

                            }
                        }
                        else
                        {
                            res += "The file(" + _file.Name + ") does not match the upload conditions, The maximum file size for uploads should not exceed " + (photoType == "others" ? (imgTypes.Contains(type) ? "5" : "25") : "5") + " MB. ";
                            j++;
                        }
                        if (j == 0)
                        {
                            if (!res.Contains(photoType))
                            {
                                res += photoType + " Photo Type Folder Not Found In dropdown Value. ";
                            }
                        }
                    }
                    if (i > 1 && res == "")
                    {
                        res = "Files Uploaded";
                    }
                    else if (res == "")
                    {
                        res = "Photo Type Folder Not Found ";
                    }
                }
                else
                {
                    res += "we were unable to find folder or folder is empty. ";
                }
                return res;
            }
            catch (Exception ex)
            {
                return "Kindly Check the Folder Exists in your Location.";
            }

        }


        static async Task FilemovetoUploadLocation(string source, string destination)
        {
            try
            {
                using (var stmr = File.Open(source, FileMode.Open)) 
                {
                    int size = (int)stmr.Length;
                    data = new byte[size];
                    int totalbyte = 0;
                    while (size > 0) 
                    {
                        int read = await stmr.ReadAsync(data, totalbyte, size); 
                        size -= read;
                        totalbyte += read;
                    }
                    using (var stm = File.Create(destination)) 
                    {
                        byte[] bytes = data;
                        await stm.WriteAsync(data, 0, data.Length); 
                        stm.Flush();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        static void readfile(string path)
        {
            FileStream stm = File.Open(path, FileMode.Open); 
            int size = (int)stm.Length; 
            data = new byte[size];
            int totalByte = 0;
            while (size > 0) 
            {
                int read = stm.Read(data, totalByte, size);
                size -= read;
                totalByte += read;
            }
            stm.Flush();
        }

        static void writefile(string fileName)
        {
            FileStream stm = File.Create(fileName); 
            byte[] bytes = data;
            stm.Write(data, 0, data.Length);
            stm.Flush();

        }
        public async Task<List<RmAssetImageDtl>> GetAllImageList()
        {
            return await _context.RmAssetImageDtl.ToListAsync().ConfigureAwait(false);
        }
        public async Task<List<RmAssetImageDtl>> GetFilterImageList(string imageTypeCode)
        {
            return await _context.RmAssetImageDtl.Where(a => a.AidImageTypeCode == imageTypeCode).ToListAsync().ConfigureAwait(false);
        }

        public async Task<int> GetAssetPK(string assetId)
        {

            return await _context.RmAllassetInventory.Where(x => x.AiAssetId == assetId && x.AiActiveYn == true)
                .Select(y => y.AiPkRefNo).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<List<RmAssetImageDtl>> GetAllImageByAssetPK(int assetPK)
        {
            return await _context.RmAssetImageDtl.Where(img => img.AidAiPkRefNo == assetPK && img.AidActiveYn == true).ToListAsync().ConfigureAwait(false);
        }
        public Task<RmAssetImageDtl> GetDocById(int accUccId)
        {
            return _context.RmAssetImageDtl.Where(x => x.AidPkRefNo == accUccId).FirstOrDefaultAsync();
        }

        public async Task BulkImportAssets(List<RmAllassetInventory> assetsToImport)
        {
            await _context.BulkInsertAsync(assetsToImport,
                options =>
                {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = asset => asset.AiPkRefNo;
                }).ConfigureAwait(false);
        }

        public async Task<RmAssetImageDtl> GetImageByIdAsync(int assetImgId)
        {
            return await _context.RmAssetImageDtl.Where(x => x.AidPkRefNo == assetImgId).FirstOrDefaultAsync();
        }

        public void DeActivateAssetImage(RmAssetImageDtl assetImg)
        {
            _context.Set<RmAssetImageDtl>().Attach(assetImg);
            _context.Entry(assetImg).State = EntityState.Modified;
        }

        public async Task<int> GetId(int headerId, string type)
        {
            int? result = await _context.RmAssetImageDtl.Where(x => x.AidAiPkRefNo == headerId && x.AidImageTypeCode == type).Select(x => x.AidImageSrno).MaxAsync();
            return result.HasValue ? result.Value : 0;
        }

        public async Task<List<AssetFieldDtl>> GetAssetTemplate(string assetType)
        {
            return await _context.AssetFieldDtl.Where(x => x.AssetType == assetType).ToListAsync().ConfigureAwait(false);
        }

        public async Task<RmAllassetInvOthers> GetOtherAssetByIdAsync(int id)
        {
            return await _context.RmAllassetInvOthers.Where(x => x.AioAiPkRefNo == id).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public void CreateOthers(RmAllassetInvOthers rmAllAsset)
        {
            _context.RmAllassetInvOthers.Add(rmAllAsset);
        }
        public async Task<RmAllassetInvOthers> CreateOtherReturnEntity(RmAllassetInvOthers rmAllAsset)
        {
            _context.Set<RmAllassetInvOthers>().Add(rmAllAsset);
            await _context.SaveChangesAsync();
            return rmAllAsset;
        }
        public void UpdateOthers(RmAllassetInvOthers rmAllAsset)
        {
            _context.Set<RmAllassetInvOthers>().Attach(rmAllAsset);
            _context.Entry(rmAllAsset).State = EntityState.Modified;
        }


        public string GetAssetCodeByName(string name)
        {
            var dt = _context.RmDdLookup.FirstOrDefault(s => s.DdlTypeCode == "Asset_Display" && s.DdlTypeDesc == name);
            if (dt != null)
                return dt.DdlTypeValue;
            else
                return "";
        }

        public async Task<int> CheckRefNo(string refNo)
        {
            return await _context.RmAllassetInventory.Where(x => x.AiAssetId == refNo && x.AiActiveYn == true).Select(x => x.AiPkRefNo).FirstOrDefaultAsync();
        }
        public async Task<List<DTO.ResponseBO.AssetId>> ListOfCulvertAssestIds()
        {
            return await _context.RmAllassetInventory.Where(x => x.AiAssetGrpCode == "CV" && x.AiActiveYn == true).OrderBy(x => x.AiLocChKm).ThenBy(x => x.AiLocChM).Select(x => new AssetId { RefId = x.AiPkRefNo, AssestyID = x.AiAssetId, Rmu = x.AiRmuCode, SectionCode = x.AiSecCode, RoadCode = x.AiRdCode }).ToListAsync();
        }
        public IQueryable<RmAllassetInventory> ListOfAssestByRoadCode(string roadCode)
        {
            return _context.RmAllassetInventory.Where(x => x.AiRdCode == roadCode && x.AiActiveYn == true);
        }
    }
}
