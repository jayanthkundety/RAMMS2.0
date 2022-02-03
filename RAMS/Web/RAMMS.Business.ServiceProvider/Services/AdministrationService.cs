using AutoMapper;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Common;
using RAMMS.Domain.Models;
using RAMMS.DTO.ResponseBO;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using ClosedXML.Excel;
using System.IO;
using RAMMS.DTO.RequestBO;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class AdministrationService : IAdministrationService
    {
        private readonly IAdministratorRepository _repo;
        public AdministrationService(IAdministratorRepository repo)
        {
            _repo = repo;
        }
        public async Task<GridWrapper<object>> GridList(DataTableAjaxPostModel searchData, string type)
        {
            return await _repo.GridList(searchData, type);
        }
        public void Save(AdministratorDTO administratorDTO, string createdBy)
        {
            switch (administratorDTO.PageName)
            {
                case "section":
                    _repo.SaveSection(new RmDivRmuSecMaster()
                    {
                        RdsmActiveYn = true,
                        RdsmCrBy = createdBy,
                        RdsmModBy = createdBy,
                        RdsmCrDt = DateTime.Now,
                        RdsmModDt = DateTime.Now,
                        RdsmPkRefNo = administratorDTO.Id,
                        RdsmDivCode = administratorDTO.DivCode,
                        RdsmDivision = administratorDTO.Division,
                        RdsmRmuCode = administratorDTO.RMUCode,
                        RdsmRmuName = administratorDTO.RMUName,
                        RdsmSectionCode = administratorDTO.SectionCode,
                        RdsmSectionName = administratorDTO.SectionName
                    });
                    break;
                case "rmu":
                    _repo.SaveLookup(new RmDdLookup()
                    {
                        DdlActiveYn = true,
                        DdlPkRefNo = administratorDTO.Id,
                        DdlType = "RMU",
                        DdlTypeCode = administratorDTO.RMUCode,
                        DdlTypeDesc = administratorDTO.RMUName,
                        DdlCrBy = createdBy,
                        DdlModBy = createdBy,
                        DdlCrDt = DateTime.Now,
                        DdlModDt = DateTime.Now,
                        DdlTypeValue = administratorDTO.RMUName
                    });
                    break;
                case "assettype":
                    _repo.SaveAssetType(new RmAssetGroupType()
                    {
                        AgtActiveYn = true,
                        AgtAssetGrpCode = administratorDTO.AssestGroupCode,
                        AgtAssetGrpName = administratorDTO.AssestGroupName,
                        AgtAssetGrpTypeCode = administratorDTO.AssestTypeCode,
                        AgtAssetGrpTypeName = administratorDTO.AssestTypeDesc,
                        AgtGrpTypeContractCode = administratorDTO.AssestTypeContractCode,
                        AgtCrBy = createdBy,
                        AgtCrDt = DateTime.Now,
                        AgtModBy = createdBy,
                        AgtModDt = DateTime.Now,
                        AgtPkRefNo = administratorDTO.Id
                    });
                    break;
                case "defect":
                    _repo.SaveDefect(new RmAssetDefectCode()
                    {
                        AdcActiveYn = true,
                        AdcAssetGrpCode = administratorDTO.AssestGroupCode,
                        AdcAssetGrpTypeName = administratorDTO.AssestGroupName,
                        AdcDefCode = administratorDTO.AssestTypeCode,
                        AdcDefName = administratorDTO.AssestTypeDesc,
                        AdcDefContractCode = administratorDTO.AssestTypeContractCode,
                        AdcCrBy = createdBy,
                        AdcCrDt = DateTime.Now,
                        AdcModBy = createdBy,
                        AdcModDt = DateTime.Now,
                        AdcPkRefNo = administratorDTO.Id,
                        AdcFormNo = administratorDTO.FormNo
                    });
                    break;
            }
        }
        public void Delete(AdministratorDTO administratorDTO, string createdBy)
        {
            switch (administratorDTO.PageName)
            {
                case "section":
                    _repo.SaveSection(new RmDivRmuSecMaster()
                    {
                        RdsmActiveYn = false,
                        RdsmModBy = createdBy,
                        RdsmModDt = DateTime.Now,
                        RdsmPkRefNo = administratorDTO.Id
                    });
                    break;
                case "rmu":
                    _repo.SaveLookup(new RmDdLookup()
                    {
                        DdlActiveYn = false,
                        DdlModBy = createdBy,
                        DdlModDt = DateTime.Now,
                        DdlPkRefNo = administratorDTO.Id
                    });
                    break;
                case "assettype":
                    _repo.SaveAssetType(new RmAssetGroupType()
                    {
                        AgtActiveYn = false,
                        AgtModBy = createdBy,
                        AgtModDt = DateTime.Now,
                        AgtPkRefNo = administratorDTO.Id
                    });
                    break;
                case "defect":
                    _repo.SaveDefect(new RmAssetDefectCode()
                    {
                        AdcActiveYn = false,
                        AdcModBy = createdBy,
                        AdcModDt = DateTime.Now,
                        AdcPkRefNo = administratorDTO.Id
                    });
                    break;
            }
        }
        public async Task<List<CSelectListItem>> AssetGroupList()
        {
            List<CSelectListItem> result = new List<CSelectListItem>();
            var obj = await _repo.AssetGroupList();
            obj.ForEach((data) =>
            {
                result.Add(new CSelectListItem()
                {
                    Text = data.AgtAssetGrpName,
                    Value = data.AgtAssetGrpName,
                    CValue = data.AgtAssetGrpCode
                });
            });
            return result;
        }
        public async Task<List<CSelectListItem>> DefectAssetGroupList()
        {
            List<CSelectListItem> result = new List<CSelectListItem>();
            var obj = await _repo.DefectAssetGroupList();
            obj.ForEach((data) =>
            {
                result.Add(new CSelectListItem()
                {
                    Text = data.AgtAssetGrpName,
                    Value = data.AgtAssetGrpName,
                    CValue = data.AgtAssetGrpCode
                });
            });
            return result;
        }
    }
}
