using Microsoft.EntityFrameworkCore;
using RAMMS.Common;
using RAMMS.Common.RefNumber;
using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository
{
    public class AdministratorRepository : RepositoryBase<RmFormFcInsHdr>, IAdministratorRepository
    {
        public AdministratorRepository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void SaveLookup(RmDdLookup lookup)
        {
            if (lookup.DdlPkRefNo == 0)
            {
                if (_context.RmDdLookup.Where(x => x.DdlType == lookup.DdlType && x.DdlActiveYn == true && x.DdlTypeCode == lookup.DdlTypeCode).Count() > 0)
                {
                    throw new Exception(lookup.DdlType + " Code is already exists");
                }
                else if (_context.RmDdLookup.Where(x => x.DdlType == lookup.DdlType && x.DdlActiveYn == true && x.DdlTypeDesc == lookup.DdlTypeDesc).Count() > 0)
                {
                    throw new Exception(lookup.DdlType + " Description is already exists");
                }
                _context.RmDdLookup.Add(lookup);
            }
            else
            {
                if (lookup.DdlActiveYn == true)
                {
                    if (_context.RmDdLookup.Where(x => x.DdlPkRefNo != lookup.DdlPkRefNo && x.DdlType == lookup.DdlType && x.DdlActiveYn == true && x.DdlTypeCode == lookup.DdlTypeCode).Count() > 0)
                    {
                        throw new Exception(lookup.DdlType + " Code is already exists");
                    }
                    else if (_context.RmDdLookup.Where(x => x.DdlPkRefNo != lookup.DdlPkRefNo && x.DdlType == lookup.DdlType && x.DdlActiveYn == true && x.DdlTypeDesc == lookup.DdlTypeDesc).Count() > 0)
                    {
                        throw new Exception(lookup.DdlType + " Description is already exists");
                    }
                    _context.RmDdLookup.Update(lookup);
                    var entry = _context.Entry(lookup);
                    entry.State = EntityState.Modified;
                }
                else
                {
                    var obj = _context.RmDdLookup.Where(x => x.DdlPkRefNo == lookup.DdlPkRefNo).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.DdlActiveYn = false;
                        obj.DdlModBy = lookup.DdlModBy;
                        obj.DdlModDt = lookup.DdlModDt;
                    }
                }
            }
            _context.SaveChanges();
        }
        public void SaveAssetType(RmAssetGroupType asset)
        {
            if (asset.AgtPkRefNo == 0)
            {
                if (_context.RmAssetGroupType.Where(x => x.AgtAssetGrpCode == asset.AgtAssetGrpCode && x.AgtAssetGrpTypeCode == asset.AgtAssetGrpTypeCode && x.AgtActiveYn == true).Count() > 0)
                {
                    throw new Exception("Asset Type code is already exists");
                }
                else if (_context.RmAssetGroupType.Where(x => x.AgtAssetGrpCode == asset.AgtAssetGrpCode && x.AgtAssetGrpTypeName == asset.AgtAssetGrpTypeName && x.AgtActiveYn == true).Count() > 0)
                {
                    throw new Exception("Asset Type Description is already exists");
                }
                _context.RmAssetGroupType.Add(asset);

                _context.RmDdLookup.Add(new RmDdLookup()
                {
                    DdlActiveYn = true,
                    DdlCrBy = asset.AgtCrBy,
                    DdlCrDt = asset.AgtCrDt,
                    DdlModBy = asset.AgtModBy,
                    DdlModDt = asset.AgtModDt,
                    DdlPkRefNo = 0,
                    DdlType = "Asset Type",
                    DdlTypeCode = asset.AgtAssetGrpCode,
                    DdlTypeDesc = asset.AgtAssetGrpTypeName,
                    DdlTypeValue = asset.AgtAssetGrpTypeCode
                });
            }
            else
            {
                if (asset.AgtActiveYn == true)
                {
                    if (_context.RmAssetGroupType.Where(x => x.AgtPkRefNo != asset.AgtPkRefNo && x.AgtAssetGrpCode == asset.AgtAssetGrpCode && x.AgtAssetGrpTypeCode == asset.AgtAssetGrpTypeCode && x.AgtActiveYn == true).Count() > 0)
                    {
                        throw new Exception("Asset Type code is already exists");
                    }
                    else if (_context.RmAssetGroupType.Where(x => x.AgtPkRefNo != asset.AgtPkRefNo && x.AgtAssetGrpCode == asset.AgtAssetGrpCode && x.AgtAssetGrpTypeName == asset.AgtAssetGrpTypeName && x.AgtActiveYn == true).Count() > 0)
                    {
                        throw new Exception("Asset Type Description is already exists");
                    }
                    var obj = _context.RmAssetGroupType.Where(x => x.AgtPkRefNo == asset.AgtPkRefNo).FirstOrDefault();
                    if (obj != null)
                    {
                        var objLookup = _context.RmDdLookup.Where(x => x.DdlType == "Asset Type" && x.DdlTypeCode == obj.AgtAssetGrpCode && x.DdlTypeValue == obj.AgtAssetGrpTypeCode).FirstOrDefault();
                        if (objLookup != null)
                        {
                            objLookup.DdlModBy = asset.AgtModBy;
                            objLookup.DdlModDt = asset.AgtModDt;
                            objLookup.DdlTypeCode = asset.AgtAssetGrpCode;
                            objLookup.DdlTypeDesc = asset.AgtAssetGrpTypeName;
                            objLookup.DdlTypeValue = asset.AgtAssetGrpTypeCode;
                        }

                        if (obj != null)
                        {
                            obj.AgtActiveYn = asset.AgtActiveYn;
                            obj.AgtModBy = asset.AgtModBy;
                            obj.AgtModDt = asset.AgtModDt;
                            obj.AgtGrpTypeContractCode = asset.AgtGrpTypeContractCode;
                            obj.AgtAssetGrpCode = asset.AgtAssetGrpCode;
                            obj.AgtAssetGrpName = asset.AgtAssetGrpName;
                            obj.AgtAssetGrpTypeCode = asset.AgtAssetGrpTypeCode;
                            obj.AgtAssetGrpTypeName = asset.AgtAssetGrpTypeName;
                        }
                    }

                }
                else
                {
                    var obj = _context.RmAssetGroupType.Where(x => x.AgtPkRefNo == asset.AgtPkRefNo).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.AgtActiveYn = false;
                        obj.AgtModBy = asset.AgtModBy;
                        obj.AgtModDt = asset.AgtModDt;

                        var objLookup = _context.RmDdLookup.Where(x => x.DdlType == "Asset Type" && x.DdlTypeCode == obj.AgtAssetGrpCode && x.DdlTypeValue == obj.AgtAssetGrpTypeCode).FirstOrDefault();
                        if (objLookup != null)
                        {
                            objLookup.DdlActiveYn = false;
                            objLookup.DdlModBy = asset.AgtModBy;
                            objLookup.DdlModDt = asset.AgtModDt;
                        }
                    }
                }
            }
            _context.SaveChanges();
        }
        public void SaveDefect(RmAssetDefectCode defect)
        {
            if (defect.AdcPkRefNo == 0)
            {
                if (_context.RmAssetDefectCode.Where(x => x.AdcAssetGrpCode == defect.AdcAssetGrpCode && x.AdcDefCode == defect.AdcDefCode && x.AdcActiveYn == true).Count() > 0)
                {
                    throw new Exception("Defect code is already exists");
                }
                else if (_context.RmAssetDefectCode.Where(x => x.AdcAssetGrpCode == defect.AdcAssetGrpCode && x.AdcDefName == defect.AdcDefName && x.AdcActiveYn == true).Count() > 0)
                {
                    throw new Exception("Defect Description is already exists");
                }
                _context.RmAssetDefectCode.Add(defect);
            }
            else
            {
                if (defect.AdcActiveYn == true)
                {
                    if (_context.RmAssetDefectCode.Where(x => x.AdcPkRefNo != defect.AdcPkRefNo && x.AdcAssetGrpCode == defect.AdcAssetGrpCode && x.AdcDefCode == defect.AdcDefCode && x.AdcActiveYn == true).Count() > 0)
                    {
                        throw new Exception("Defect code is already exists");
                    }
                    else if (_context.RmAssetDefectCode.Where(x => x.AdcPkRefNo != defect.AdcPkRefNo && x.AdcAssetGrpCode == defect.AdcAssetGrpCode && x.AdcDefName == defect.AdcDefName && x.AdcActiveYn == true).Count() > 0)
                    {
                        throw new Exception("Defect Description is already exists");
                    }
                    var obj = _context.RmAssetDefectCode.Where(x => x.AdcPkRefNo == defect.AdcPkRefNo).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.AdcActiveYn = defect.AdcActiveYn;
                        obj.AdcModBy = defect.AdcModBy;
                        obj.AdcModDt = defect.AdcModDt;
                        obj.AdcDefContractCode = defect.AdcDefContractCode;
                        obj.AdcAssetGrpCode = defect.AdcAssetGrpCode;
                        obj.AdcAssetGrpTypeName = defect.AdcAssetGrpTypeName;
                        obj.AdcDefCode = defect.AdcDefCode;
                        obj.AdcDefName = defect.AdcDefName;
                        obj.AdcFormNo = defect.AdcFormNo;
                    }
                }
                else
                {
                    var obj = _context.RmAssetDefectCode.Where(x => x.AdcPkRefNo == defect.AdcPkRefNo).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.AdcActiveYn = false;
                        obj.AdcModBy = defect.AdcModBy;
                        obj.AdcModDt = defect.AdcModDt;                       
                    }
                }
            }
            _context.SaveChanges();
        }
        public RmDivRmuSecMaster SaveSection(RmDivRmuSecMaster section)
        {
            if (section.RdsmPkRefNo == 0)
            {
                if (_context.RmDivRmuSecMaster.Where(x => x.RdsmSectionCode == section.RdsmSectionCode && x.RdsmActiveYn == true).Count() > 0)
                {
                    throw new Exception("Section code is already exists");
                }
                else if (_context.RmDivRmuSecMaster.Where(x => x.RdsmSectionName == section.RdsmSectionName && x.RdsmActiveYn == true).Count() > 0)
                {
                    throw new Exception("Section Name is already exists");
                }
                _context.RmDivRmuSecMaster.Add(section);

                SaveLookup(new RmDdLookup()
                {
                    DdlActiveYn = true,
                    DdlCrBy = section.RdsmCrBy,
                    DdlCrDt = section.RdsmCrDt,
                    DdlModBy = section.RdsmModBy,
                    DdlModDt = section.RdsmModDt,
                    DdlPkRefNo = 0,
                    DdlType = "Section Code",
                    DdlTypeCode = section.RdsmSectionCode,
                    DdlTypeDesc = section.RdsmSectionName,
                    DdlTypeValue = section.RdsmRmuName
                });
            }
            else
            {
                if (section.RdsmActiveYn == true)
                {
                    if (_context.RmDivRmuSecMaster.Where(x => x.RdsmPkRefNo != section.RdsmPkRefNo && x.RdsmActiveYn == true && x.RdsmSectionCode == section.RdsmSectionCode).Count() > 0)
                    {
                        throw new Exception("Section code is already exists");
                    }
                    else if (_context.RmDivRmuSecMaster.Where(x => x.RdsmPkRefNo != section.RdsmPkRefNo && x.RdsmActiveYn == true && x.RdsmSectionName == section.RdsmSectionName).Count() > 0)
                    {
                        throw new Exception("Section Name is already exists");
                    }
                    var obj = _context.RmDivRmuSecMaster.Where(x => x.RdsmPkRefNo == section.RdsmPkRefNo).FirstOrDefault();
                    if (obj != null)
                    {
                        var objLookup = _context.RmDdLookup.Where(x => x.DdlType == "Section Code" && x.DdlTypeCode == obj.RdsmSectionCode).FirstOrDefault();
                        if (objLookup != null)
                        {
                            objLookup.DdlModBy = section.RdsmModBy;
                            objLookup.DdlModDt = section.RdsmModDt;
                            objLookup.DdlTypeCode = section.RdsmSectionCode;
                            objLookup.DdlTypeDesc = section.RdsmSectionName;
                            objLookup.DdlTypeValue = section.RdsmRmuName;
                        }

                        if (obj != null)
                        {
                            obj.RdsmActiveYn = section.RdsmActiveYn;
                            obj.RdsmDivCode = section.RdsmDivCode;
                            obj.RdsmDivision = section.RdsmDivision;
                            obj.RdsmModBy = section.RdsmModBy;
                            obj.RdsmModDt = section.RdsmModDt;
                            obj.RdsmRmuCode = section.RdsmRmuCode;
                            obj.RdsmRmuName = section.RdsmRmuName;
                            obj.RdsmSectionCode = section.RdsmSectionCode;
                            obj.RdsmSectionName = section.RdsmSectionName;
                        }
                    }

                }
                else
                {
                    var obj = _context.RmDivRmuSecMaster.Where(x => x.RdsmPkRefNo == section.RdsmPkRefNo).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.RdsmActiveYn = false;
                        obj.RdsmModBy = section.RdsmModBy;
                        obj.RdsmModDt = section.RdsmModDt;

                        var objLookup = _context.RmDdLookup.Where(x => x.DdlType == "Section Code" && x.DdlTypeCode == obj.RdsmSectionCode).FirstOrDefault();
                        if (objLookup != null)
                        {
                            objLookup.DdlActiveYn = false;
                            objLookup.DdlModBy = section.RdsmModBy;
                            objLookup.DdlModDt = section.RdsmModDt;
                        }
                    }
                }
            }
            _context.SaveChanges();
            return section;
        }
        public async Task<GridWrapper<object>> GridList(DataTableAjaxPostModel searchData, string type)
        {
            switch (type.ToLower())
            {
                case "division":
                    return await LookUpGridList(searchData, "Division");
                case "rmu":
                    return await LookUpGridList(searchData, "RMU");
                case "section":
                    return await SectionGridList(searchData);
                case "road":
                    return await RoadGridList(searchData);
                case "assettype":
                    return await AssetTypeGridList(searchData);
                case "defect":
                    return await DefectGridList(searchData);
                default:
                    return null;
            }
        }
        public async Task<GridWrapper<object>> RoadGridList(DataTableAjaxPostModel searchData)
        {
            var query = (from hdr in _context.RmRoadMaster
                         select new
                         {
                             Id = hdr.RdmPkRefNo,
                             Code = hdr.RdmRdCode,
                             Type = "Road List",
                             Desc = hdr.RdmRdName,
                             RMUCode = hdr.RdmRmuCode,
                             RMU = hdr.RdmRmuName,
                             Div = hdr.RdmDivCode,
                             Active = hdr.RdmActiveYn,
                             CreatedOn = hdr.RdmCrDt,
                             ModifiedOn = hdr.RdmModDt,
                             CreatedBy = hdr.RdmCrBy,
                             ModifiedBy = hdr.RdmModBy,
                             Section = hdr.RdmSecName,
                             SectionCode = hdr.RdmSecCode,
                             RoadCategory = hdr.RdmRdCatgName,
                             RoadCategoryCode = hdr.RdmRdCatgCode,
                             LocationFrom = hdr.RdmFrmLoc,
                             LocationTo = hdr.RdmToLoc,
                             ChFrom = hdr.RdmFrmCh,
                             ChFromDeci = hdr.RdmFrmChDeci,
                             ChTo =hdr.RdmToCh,
                             ChToDeci = hdr.RdmToChDeci,
                             PavedLength = hdr.RdmLengthPaved,
                             UnpavedLength = hdr.RdmLengthUnpaved,
                             Owner = hdr.RdmOwner
                         });
            if (searchData.filter != null)
            {
                foreach (var item in searchData.filter.Where(x => !string.IsNullOrEmpty(x.Value)))
                {
                    string strVal = Utility.ToString(item.Value).Trim();
                    switch (item.Key)
                    {
                        case "KeySearch":
                            DateTime? dtSearch = Utility.ToDateTime(strVal);
                            query = query.Where(x =>
                                 (x.Code ?? "").Contains(strVal)
                                 || (x.Desc ?? "").Contains(strVal)
                                 || (x.RMUCode ?? "").Contains(strVal)
                                 || (x.RMU ?? "").Contains(strVal)
                                 || (x.Div ?? "").Contains(strVal)
                                 || (x.CreatedBy ?? "").Contains(strVal)
                                 || (x.ModifiedBy ?? "").Contains(strVal)
                                 || (x.Section ?? "").Contains(strVal)
                                 || (x.SectionCode.HasValue ? x.SectionCode.Value.ToString() : "").Contains(strVal)
                                 || (x.RoadCategory ?? "").Contains(strVal)
                                 || (x.RoadCategoryCode ?? "").Contains(strVal)
                                 || (x.LocationFrom ?? "").Contains(strVal)
                                 || (x.LocationTo ?? "").Contains(strVal)
                                 || (x.Owner ?? "").Contains(strVal)
                                 || (x.ChFrom.HasValue ? x.ChFrom.Value.ToString() : "").Contains(strVal)
                                 || (x.ChFromDeci.HasValue ? x.ChFromDeci.Value.ToString() : "").Contains(strVal)
                                 || (x.ChTo.HasValue ? x.ChTo.Value.ToString() : "").Contains(strVal)
                                 || (x.ChToDeci.HasValue ? x.ChToDeci.Value.ToString() : "").Contains(strVal)
                                 || (x.PavedLength.HasValue ? x.PavedLength.Value.ToString() : "").Contains(strVal)
                                 || (x.UnpavedLength.HasValue ? x.UnpavedLength.Value.ToString() : "").Contains(strVal)
                                 );
                            break;
                    }
                }
            }
            GridWrapper<object> grid = new GridWrapper<object>();
            grid.recordsTotal = await query.CountAsync();
            grid.recordsFiltered = grid.recordsTotal;
            grid.draw = searchData.draw;
            grid.data = await query.Order(searchData, query.OrderBy(s => s.Code)).Skip(searchData.start)
                                .Take(searchData.length)
                                .ToListAsync(); ;

            return grid;
        }
        public async Task<GridWrapper<object>> SectionGridList(DataTableAjaxPostModel searchData)
        {
            var query = (from hdr in _context.RmDivRmuSecMaster
                         select new
                         {
                             Id = hdr.RdsmPkRefNo,
                             Code = hdr.RdsmSectionCode,
                             Type = "Section Code",
                             Desc = hdr.RdsmSectionName,
                             RMUCode = hdr.RdsmRmuCode,
                             RMU = hdr.RdsmRmuName,
                             Div = hdr.RdsmDivision,
                             Active = hdr.RdsmActiveYn,
                             CreatedOn = hdr.RdsmCrDt,
                             ModifiedOn = hdr.RdsmModDt,
                             CreatedBy = hdr.RdsmCrBy,
                             ModifiedBy = hdr.RdsmModBy
                         });
            query = query.Where(x => x.Active == true);
            if (searchData.filter != null)
            {
                foreach (var item in searchData.filter.Where(x => !string.IsNullOrEmpty(x.Value)))
                {
                    string strVal = Utility.ToString(item.Value).Trim();
                    switch (item.Key)
                    {
                        case "KeySearch":
                            DateTime? dtSearch = Utility.ToDateTime(strVal);
                            query = query.Where(x =>
                                 (x.Code ?? "").Contains(strVal)
                                 || (x.Desc ?? "").Contains(strVal)
                                 || (x.RMUCode ?? "").Contains(strVal)
                                 || (x.RMU ?? "").Contains(strVal)
                                 || (x.Div ?? "").Contains(strVal)
                                 || (x.CreatedBy ?? "").Contains(strVal)
                                 || (x.ModifiedBy ?? "").Contains(strVal)
                                 );
                            break;
                    }
                }
            }
            GridWrapper<object> grid = new GridWrapper<object>();
            grid.recordsTotal = await query.CountAsync();
            grid.recordsFiltered = grid.recordsTotal;
            grid.draw = searchData.draw;
            grid.data = await query.Order(searchData, query.OrderBy(s => s.Code)).Skip(searchData.start)
                                .Take(searchData.length)
                                .ToListAsync(); ;

            return grid;
        }
        public async Task<List<RmAssetGroupType>> AssetGroupList()
        {
            var query = (from hdr in _context.RmAssetGroupType
                         select new RmAssetGroupType()
                         {
                             AgtAssetGrpCode = hdr.AgtAssetGrpCode,
                             AgtAssetGrpName = hdr.AgtAssetGrpName
                         });
            return await query.Distinct().OrderBy(x => x.AgtAssetGrpName).ToListAsync();
        }
        public async Task<List<RmAssetGroupType>> DefectAssetGroupList()
        {
            var query = (from hdr in _context.RmAssetDefectCode
                         select new RmAssetGroupType()
                         {
                             AgtAssetGrpCode = hdr.AdcAssetGrpCode,
                             AgtAssetGrpName = hdr.AdcAssetGrpTypeName
                         });
            return await query.Distinct().OrderBy(x => x.AgtAssetGrpName).ToListAsync();
        }
        public async Task<GridWrapper<object>> AssetTypeGridList(DataTableAjaxPostModel searchData)
        {
            var query = (from hdr in _context.RmAssetGroupType
                         select new
                         {
                             Id = hdr.AgtPkRefNo,
                             Code = hdr.AgtAssetGrpTypeCode,
                             ContractCode = hdr.AgtGrpTypeContractCode,
                             Type = "Asset Type",
                             Desc = hdr.AgtAssetGrpTypeName,
                             GrpCode = hdr.AgtAssetGrpCode,
                             GrpName = hdr.AgtAssetGrpName,
                             Active = hdr.AgtActiveYn,
                             CreatedOn = hdr.AgtCrDt,
                             ModifiedOn = hdr.AgtModDt,
                             CreatedBy = hdr.AgtCrBy,
                             ModifiedBy = hdr.AgtModBy
                         });
            query = query.Where(x => x.Active == true);
            if (searchData.filter != null)
            {
                foreach (var item in searchData.filter.Where(x => !string.IsNullOrEmpty(x.Value)))
                {
                    string strVal = Utility.ToString(item.Value).Trim();
                    switch (item.Key)
                    {
                        case "KeySearch":
                            DateTime? dtSearch = Utility.ToDateTime(strVal);
                            query = query.Where(x =>
                                 (x.Code ?? "").Contains(strVal)
                                 || (x.Desc ?? "").Contains(strVal)
                                 || (x.ContractCode ?? "").Contains(strVal)
                                 || (x.GrpCode ?? "").Contains(strVal)
                                 || (x.GrpName ?? "").Contains(strVal)
                                 || (x.CreatedBy ?? "").Contains(strVal)
                                 || (x.ModifiedBy ?? "").Contains(strVal)
                                 );
                            break;
                    }
                }
            }
            GridWrapper<object> grid = new GridWrapper<object>();
            grid.recordsTotal = await query.CountAsync();
            grid.recordsFiltered = grid.recordsTotal;
            grid.draw = searchData.draw;
            grid.data = await query.Order(searchData, query.OrderBy(s => s.Code)).Skip(searchData.start)
                                .Take(searchData.length)
                                .ToListAsync(); ;

            return grid;
        }
        public async Task<GridWrapper<object>> DefectGridList(DataTableAjaxPostModel searchData)
        {
            var query = (from hdr in _context.RmAssetDefectCode
                         select new
                         {
                             Id = hdr.AdcPkRefNo,
                             Code = hdr.AdcDefCode,
                             ContractCode = hdr.AdcDefContractCode,
                             Type = "Defect",
                             Desc = hdr.AdcDefName,
                             GrpCode = hdr.AdcAssetGrpCode,
                             GrpName = hdr.AdcAssetGrpTypeName,
                             Active = hdr.AdcActiveYn,
                             CreatedOn = hdr.AdcCrDt,
                             ModifiedOn = hdr.AdcModDt,
                             CreatedBy = hdr.AdcCrBy,
                             ModifiedBy = hdr.AdcModBy,
                             FormNo = hdr.AdcFormNo
                         });
            query = query.Where(x => x.Active == true);
            if (searchData.filter != null)
            {
                foreach (var item in searchData.filter.Where(x => !string.IsNullOrEmpty(x.Value)))
                {
                    string strVal = Utility.ToString(item.Value).Trim();
                    switch (item.Key)
                    {
                        case "KeySearch":
                            DateTime? dtSearch = Utility.ToDateTime(strVal);
                            query = query.Where(x =>
                                 (x.Code ?? "").Contains(strVal)
                                 || (x.Desc ?? "").Contains(strVal)
                                 || (x.ContractCode ?? "").Contains(strVal)
                                 || (x.GrpCode ?? "").Contains(strVal)
                                 || (x.GrpName ?? "").Contains(strVal)
                                 || (x.CreatedBy ?? "").Contains(strVal)
                                 || (x.ModifiedBy ?? "").Contains(strVal)
                                 );
                            break;
                    }
                }
            }
            GridWrapper<object> grid = new GridWrapper<object>();
            grid.recordsTotal = await query.CountAsync();
            grid.recordsFiltered = grid.recordsTotal;
            grid.draw = searchData.draw;
            grid.data = await query.Order(searchData, query.OrderBy(s => s.Code)).Skip(searchData.start)
                                .Take(searchData.length)
                                .ToListAsync(); ;

            return grid;
        }
        public async Task<GridWrapper<object>> LookUpGridList(DataTableAjaxPostModel searchData, string type)
        {
            var query = (from hdr in _context.RmDdLookup
                         select new
                         {
                             Id = hdr.DdlPkRefNo,
                             Code = hdr.DdlTypeCode,
                             Type = hdr.DdlType,
                             Value = hdr.DdlTypeValue,
                             Desc = hdr.DdlTypeDesc,
                             Active = hdr.DdlActiveYn,
                             Remarks = hdr.DdlTypeRemarks,
                             CreatedOn = hdr.DdlCrDt,
                             ModifiedOn = hdr.DdlModDt,
                             CreatedBy = hdr.DdlCrBy,
                             ModifiedBy = hdr.DdlModBy
                         });
            query = query.Where(x => x.Type == type && x.Active == true);
            if (searchData.filter != null)
            {
                foreach (var item in searchData.filter.Where(x => !string.IsNullOrEmpty(x.Value)))
                {
                    string strVal = Utility.ToString(item.Value).Trim();
                    switch (item.Key)
                    {
                        case "KeySearch":
                            DateTime? dtSearch = Utility.ToDateTime(strVal);
                            query = query.Where(x =>
                                 (x.Code ?? "").Contains(strVal)
                                 || (x.Value ?? "").Contains(strVal)
                                 || (x.Desc ?? "").Contains(strVal)
                                 || (x.Remarks ?? "").Contains(strVal)
                                 || (x.CreatedBy ?? "").Contains(strVal)
                                 || (x.ModifiedBy ?? "").Contains(strVal)
                                 );
                            break;
                    }
                }
            }
            GridWrapper<object> grid = new GridWrapper<object>();
            grid.recordsTotal = await query.CountAsync();
            grid.recordsFiltered = grid.recordsTotal;
            grid.draw = searchData.draw;
            grid.data = await query.Order(searchData, query.OrderBy(s => s.Code)).Skip(searchData.start)
                                .Take(searchData.length)
                                .ToListAsync(); ;

            return grid;
        }
    }
}
