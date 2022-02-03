using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
///using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace RAMMS.Repository
{

    public class RoadmasterRepository : RepositoryBase<RmRoadMaster>, IRoadMasterRepository
    {
        public RoadmasterRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetLoadData(RmRoadMaster _roadmast)
        {
            IQueryable<RmRoadMaster> _AssetListingRepos;
            _AssetListingRepos = _context.RmRoadMaster
                   .AsNoTracking().Where(x => x.RdmDivCode == _roadmast.RdmDivCode); ;

            var selectList = new List<SelectListItem>();
            foreach (var element in _AssetListingRepos)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.RdmFeatureId.ToString(),
                    Text = element.RdmFeatureId.ToString()
                });
            }
            return selectList.ToList();
        }
        public async Task<IEnumerable<RmRoadMaster>> GetRMDataLookup(RoadMasterRequestDTO _RMAllData)
        {
            IEnumerable<RmRoadMaster> _GetAllRMDataRepos;
            _GetAllRMDataRepos = await _context.RmRoadMaster.ToListAsync();
            //.AsNoTracking().Where(x => x.DdlType == _GetAddLookupData.DdlType);
            //.Where(x => x.RdmFeatureId == _RMAllData.RdmFeatureId);
            return _GetAllRMDataRepos;
        }
        public async Task<RmRoadMaster> GetAllRMData(RoadMasterRequestDTO _RMAllDatDto)
        {
            return await _context.RmRoadMaster.Where(x => x.RdmFeatureId == _RMAllDatDto.FeatureId).FirstOrDefaultAsync();

        }

        //BridgeMainGridRepo
        public IEnumerable<RmAllassetInventory> GetGridData()
        {
            //DataTable yet to replace with SP
            IEnumerable<RmAllassetInventory> _GridData;
            _GridData = _context.RmAllassetInventory.AsNoTracking();

            //IEnumerable<allAssets> _GridData1;
            //_GridData1 = _context.allAsset.FromSqlRaw("proc_grid_allassets @assetgroup={0}", "cv");
            //var a = _context.Database.ExecuteSqlCommand("proc_grid_allassets @assetgroup={0}", "cv");
            //_GridData = _context.RmAllassetInventory.FromSqlRaw<RmAllassetInventory>("proc_grid_allassets @assetgroup={0}", "cv");
            return _GridData;
        }

        public IEnumerable<AssetFieldDtl> GetAssetFieldDtls()
        {
            IEnumerable<AssetFieldDtl> _GetAllRMDataRepos;
            _GetAllRMDataRepos = _context.AssetFieldDtl
                                .AsNoTracking();
            //       //.AsNoTracking().Where(x => x.DdlType == _GetAddLookupData.DdlType);
            //       .AsNoTracking();//.Where(x => x.RdmFeatureId == _RMAllData.RdmFeatureId);

            return _GetAllRMDataRepos;// _GetAllRMDataRepos;
        }

        public IEnumerable<RmFormDownloadUse> GetAllAssetFormDownloadUse()
        {
            IEnumerable<RmFormDownloadUse> _GetAllRMDataRepos;
            _GetAllRMDataRepos = _context.RmFormDownloadUse
                   //.AsNoTracking().Where(x => x.DdlType == _GetAddLookupData.DdlType);
                   .AsNoTracking();//.Where(x => x.RdmFeatureId == _RMAllData.RdmFeatureId);

            return _GetAllRMDataRepos;
        }

        public IEnumerable<RmFormGenDtl> GetFormGenDtls()
        {
            IEnumerable<RmFormGenDtl> _GetFormGenDtls;
            _GetFormGenDtls = _context.RmFormGenDtl
                   //.AsNoTracking().Where(x => x.DdlType == _GetAddLookupData.DdlType);
                   .AsNoTracking();//.Where(x => x.RdmFeatureId == _RMAllData.RdmFeatureId);

            return _GetFormGenDtls;
        }

        public async Task<List<RmRoadMaster>> GetRMUBasedData(RoadMasterRequestDTO requestDto)
        {
            var query = (from o in _context.RmRoadMaster where o.RdmActiveYn==true select o);
            if (!string.IsNullOrEmpty(requestDto.DivisionCode))
                query = query.Where(s => s.RdmDivCode == requestDto.DivisionCode);
            if (!string.IsNullOrEmpty(requestDto.RoadCode))
                query = query.Where(s => s.RdmRdCode == requestDto.RoadCode);
            if (!string.IsNullOrEmpty(requestDto.RmuCode))
                query = query.Where(s => s.RdmRmuCode == requestDto.RmuCode);
            if (!string.IsNullOrEmpty(requestDto.RmuName))
                query = query.Where(s => s.RdmRmuName == requestDto.RmuName);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<RmRoadMaster>> GetRM_RoadCode_Data(RoadMasterRequestDTO requestDto)
        {
            return await _context.RmRoadMaster.ToListAsync();
        }

        public async Task<IEnumerable<RmRoadMaster>> GetAllRoadCode()
        {
            return await _context.RmRoadMaster.ToListAsync();
        }
        public async Task<RmRoadMaster> GetAllRoadCodeData(RoadMasterRequestDTO _RMAllDatDto)
        {
            return await _context.RmRoadMaster.Where(x => x.RdmRdCode == _RMAllDatDto.RoadCode && x.RdmPkRefNo == (_RMAllDatDto.No != 0 ? _RMAllDatDto.No : x.RdmPkRefNo)).FirstOrDefaultAsync();

        }

        public async Task<RmRoadMaster> GetAllRoadCodeDataBySectionCode(RoadMasterRequestDTO _RMAllDatDto)
        {
            return await _context.RmRoadMaster.Where(x => x.RdmSecCode == _RMAllDatDto.SecCode).FirstOrDefaultAsync();

        }


        public async Task<AssetDDLResponseDTO> GetFilteredList(AssetDDLRequestDTO roadMaster)
        {
            var result = new AssetDDLResponseDTO();
            if (string.IsNullOrWhiteSpace(roadMaster.RMU) && roadMaster.SectionCode == 0 && string.IsNullOrWhiteSpace(roadMaster.RdCode))
            {
                var rmu = await (from x in _context.RmRoadMaster
                                 where x.RdmActiveYn == true
                                 select new AssetDDLResponseDTO.DropDown
                                 {
                                     Value = x.RdmRmuName,
                                     Text = x.RdmRmuName
                                 }).Distinct().OrderByDescending(x => x.Value).ToListAsync();

                var section = await (from x in _context.RmRoadMaster
                                     where x.RdmActiveYn == true
                                     select new AssetDDLResponseDTO.DropDown
                                     {
                                         Value = x.RdmSecCode.ToString(),
                                         Text = x.RdmSecCode.ToString() + "-" + x.RdmSecName.ToString()
                                     }).Distinct().ToListAsync();

                var roadCode = await (from x in _context.RmRoadMaster
                                      where x.RdmActiveYn == true
                                      select new AssetDDLResponseDTO.DropDown
                                      {
                                          Value = x.RdmRdCode,
                                          Text = x.RdmRdCode + "-" + x.RdmRdName.ToString()
                                      }).Distinct().ToListAsync();
                result.RMU = rmu;
                result.Section = section;
                result.RdCode = roadCode;

            }
            else if (!string.IsNullOrWhiteSpace(roadMaster.RMU) && roadMaster.SectionCode > 0 && string.IsNullOrWhiteSpace(roadMaster.RdCode))
            {
                var roadCode = await (from x in _context.RmRoadMaster
                                      where (x.RdmRmuCode == roadMaster.RMU || x.RdmRmuName == roadMaster.RMU) && x.RdmSecCode == roadMaster.SectionCode && x.RdmActiveYn == true
                                      select new AssetDDLResponseDTO.DropDown
                                      {
                                          Value = x.RdmRdCode,
                                          Text = x.RdmRdCode + "-" + x.RdmRdName.ToString()
                                      }).Distinct().ToListAsync();
                result.RdCode = roadCode;
            }
            else if (string.IsNullOrWhiteSpace(roadMaster.RMU) && roadMaster.SectionCode > 0 && !string.IsNullOrWhiteSpace(roadMaster.RdCode))
            {
                var rmu = await (from x in _context.RmRoadMaster
                                 where x.RdmRdCode == roadMaster.RdCode && x.RdmSecCode == roadMaster.SectionCode && x.RdmActiveYn==true
                                 select new AssetDDLResponseDTO.DropDown
                                 {
                                     Value = x.RdmRmuName,
                                     Text = x.RdmRmuName
                                 }).Distinct().OrderByDescending(x => x.Value).ToListAsync();
                result.RMU = rmu;
            }
            else if (!string.IsNullOrWhiteSpace(roadMaster.RMU) && roadMaster.SectionCode == 0 && !string.IsNullOrWhiteSpace(roadMaster.RdCode))
            {
                var section = await (from x in _context.RmRoadMaster
                                     where (x.RdmRmuCode == roadMaster.RMU || x.RdmRmuName == roadMaster.RMU) && x.RdmRdCode == roadMaster.RdCode && x.RdmActiveYn == true
                                     select new AssetDDLResponseDTO.DropDown
                                     {
                                         Value = x.RdmSecCode.ToString(),
                                         Text = x.RdmSecCode.ToString() + "-" + x.RdmSecName.ToString()
                                     }).Distinct().ToListAsync();
                result.Section = section;
            }
            else if (!string.IsNullOrWhiteSpace(roadMaster.RMU) && roadMaster.SectionCode == 0 && string.IsNullOrWhiteSpace(roadMaster.RdCode))
            {
                var section = await (from x in _context.RmRoadMaster
                                     where (x.RdmRmuCode == roadMaster.RMU || x.RdmRmuName == roadMaster.RMU) && x.RdmActiveYn == true
                                     select new AssetDDLResponseDTO.DropDown
                                     {
                                         Value = x.RdmSecCode.ToString(),
                                         Text = x.RdmSecCode.ToString() + "-" + x.RdmSecName.ToString()
                                     }).Distinct().ToListAsync();

                var roadCode = await (from x in _context.RmRoadMaster
                                      where (x.RdmRmuCode == roadMaster.RMU || x.RdmRmuName == roadMaster.RMU) && x.RdmActiveYn == true
                                      select new AssetDDLResponseDTO.DropDown
                                      {
                                          Value = x.RdmRdCode,
                                          Text = x.RdmRdCode.ToString() + "-" + x.RdmRdName.ToString()
                                      }).Distinct().ToListAsync();
                result.Section = section;
                result.RdCode = roadCode;
            }
            else if (string.IsNullOrWhiteSpace(roadMaster.RMU) && roadMaster.SectionCode > 0 && string.IsNullOrWhiteSpace(roadMaster.RdCode))
            {
                var rmu = await (from x in _context.RmRoadMaster
                                 where x.RdmSecCode == roadMaster.SectionCode && x.RdmActiveYn == true
                                 select new AssetDDLResponseDTO.DropDown
                                 {
                                     Value = x.RdmRmuName,
                                     Text = x.RdmRmuName
                                 }).Distinct().OrderByDescending(x => x.Value).ToListAsync();

                var roadCode = await (from x in _context.RmRoadMaster
                                      where x.RdmSecCode == roadMaster.SectionCode && x.RdmActiveYn == true
                                      select new AssetDDLResponseDTO.DropDown
                                      {
                                          Value = x.RdmRdCode,
                                          Text = x.RdmRdCode + "-" + x.RdmRdName.ToString()
                                      }).Distinct().ToListAsync();
                result.RMU = rmu;
                result.RdCode = roadCode;
            }
            else if (string.IsNullOrWhiteSpace(roadMaster.RMU) && roadMaster.SectionCode == 0 && !string.IsNullOrWhiteSpace(roadMaster.RdCode))
            {
                var section = await (from x in _context.RmRoadMaster
                                     where x.RdmRdCode == roadMaster.RdCode && x.RdmActiveYn == true
                                     select new AssetDDLResponseDTO.DropDown
                                     {
                                         Value = x.RdmSecCode.ToString(),
                                         Text = x.RdmSecCode.ToString() + "-" + x.RdmSecName.ToString()
                                     }).Distinct().ToListAsync();

                var rmu = await (from x in _context.RmRoadMaster
                                 where x.RdmRdCode == roadMaster.RdCode && x.RdmActiveYn == true
                                 select new AssetDDLResponseDTO.DropDown
                                 {
                                     Value = x.RdmRmuName,
                                     Text = x.RdmRmuName
                                 }).Distinct().OrderByDescending(x => x.Value).ToListAsync();
                result.Section = section;
                result.RMU = rmu;
            }
            return result;
        }

        public async Task<List<string>> GetSectionByRMU(RoadMasterRequestDTO roadMaster)
        {
            if (roadMaster.RmuCode != null)
            {
                return await _context.RmRoadMaster.Where(x => x.RdmRmuCode == roadMaster.RmuCode).Select(x => x.RdmSecName).Distinct().ToListAsync();
            }
            else
            {
                return await _context.RmRoadMaster.Select(x => x.RdmSecName).Distinct().ToListAsync();
            }
        }

        public async Task<List<string>> GetRdCodeBySection(string section)
        {
            return await _context.RmRoadMaster.Where(x => x.RdmSecName == section).Select(x => x.RdmRdCode).ToListAsync();
        }
        public async Task<RmRoadMaster> GetByRdCode(string roadCode)
        {
            return await _context.RmRoadMaster.Where(x => x.RdmRdCode == roadCode).FirstOrDefaultAsync();
        }
        public async Task<int?> GetRoadNo(string roadCode)
        {
            return await _context.RmRoadMaster.Where(x => x.RdmRdCode == roadCode).Select(x => x.RdmPkRefNo).FirstOrDefaultAsync();
        }

        ////public IQueryable<RmDdLookup> GetLoadData(RmDdLookup _lookupData)
        ////{
        ////    //if (_lookupData.DdlType == "ASSET LISTING")
        ////    //{  //List dd = _dDLookupBO.ddLookup(_rmDdLookup);
        ////    //var result = lookup.ToList();
        ////    //List<SelectListItem> objResult = new List<SelectListItem>();
        ////    //foreach (var item in result)
        ////    //{
        ////    //    SelectListItem temp = new SelectListItem();
        ////    //    temp.Text = item.DdlTypeDesc;
        ////    //    temp.Value = item.DdlTypeValue;
        ////    //    objResult.Add(temp);
        ////    //}
        ////        IQueryable<RmDdLookup> _AssetListingRepos;
        ////        _AssetListingRepos = _context.RmDdLookup
        ////               .AsNoTracking().Where(x => x.DdlType == _lookupData.DdlType);//.Select(x=> new { Value=x.DdlTypeValue.ToString(), Text=x.DdlTypeDesc} );

        ////        return _AssetListingRepos;
        ////    // }
        ////    //if (_lookupData.DdlType == "Distress Code")
        ////    //{
        ////    //    IQueryable<RmDdLookup> _AssetListingRepos;
        ////    //    _AssetListingRepos = _context.RmDdLookup
        ////    //           .AsNoTracking().Where(x => x.DdlType == "Distress Code");

        ////    //    return _AssetListingRepos;
        ////    //}
        ////    //if (_lookupData.DdlType == "Site Ref")
        ////    //{
        ////    //    IQueryable<RmDdLookup> _AssetListingRepos;
        ////    //    _AssetListingRepos = _context.RmDdLookup
        ////    //           .AsNoTracking().Where(x => x.DdlType == "Site Ref");

        ////    //    return _AssetListingRepos;
        ////    //}
        ////    //if (_lookupData.DdlType == "Priority")
        ////    //{
        ////    //    IQueryable<RmDdLookup> _AssetListingRepos;
        ////    //    _AssetListingRepos = _context.RmDdLookup
        ////    //           .AsNoTracking().Where(x => x.DdlType == "Priority");

        ////    //    return _AssetListingRepos;
        ////    //}
        ////    //if (_lookupData.DdlType == "Unit")
        ////    //{
        ////    //    IQueryable<RmDdLookup> _AssetListingRepos;
        ////    //    _AssetListingRepos = _context.RmDdLookup
        ////    //           .AsNoTracking().Where(x => x.DdlType == "Unit");

        ////    //    return _AssetListingRepos;
        ////    //}
        ////   // return null;
        ////}
        //public IQueryable<RmDdLookup> GetAddLookupData(RmDdLookup _GetAddLookupData)
        //{
        //    IQueryable<RmDdLookup> _GetLookupRepos;
        //    _GetLookupRepos = _context.RmDdLookup
        //           //.AsNoTracking().Where(x => x.DdlType == _GetAddLookupData.DdlType);
        //           .AsNoTracking().Where(x => x.DdlType == "Distress Code");

        //    return _GetLookupRepos;
        //}
        //public IQueryable<RmDdLookup> GetSiteRef(RmDdLookup _GetAddLookupData)
        //{
        //    IQueryable<RmDdLookup> _GetLookupRepos;
        //    _GetLookupRepos = _context.RmDdLookup
        //           .AsNoTracking().Where(x => x.DdlType == "Site Ref");

        //    return _GetLookupRepos;
        //}
        //public IQueryable<RmDdLookup> GetPriority(RmDdLookup _GetAddLookupData)
        //{
        //    IQueryable<RmDdLookup> _GetLookupRepos;
        //    _GetLookupRepos = _context.RmDdLookup
        //           .AsNoTracking().Where(x => x.DdlType == "Priority");

        //    return _GetLookupRepos;
        //}
        //public IQueryable<RmDdLookup> GetUnit(RmDdLookup _GetAddLookupData)
        //{
        //    IQueryable<RmDdLookup> _GetLookupRepos;
        //    _GetLookupRepos = _context.RmDdLookup
        //           .AsNoTracking().Where(x => x.DdlType == "Unit");

        //    return _GetLookupRepos;
        //}
    }
    //class BridgeRepository
    //{
    //}
}
