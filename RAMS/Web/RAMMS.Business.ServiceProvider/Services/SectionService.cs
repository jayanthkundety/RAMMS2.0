using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class SectionService : ISectionService
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;

        public SectionService(IRepositoryUnit repoUnit, IMapper mapper, ISecurity security)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security ?? throw new ArgumentNullException(nameof(security));
        }

        public long LastSectionInsertedNo()
        {
            RmDivRmuSecMaster rmDivRmuSecMaster = _repoUnit.SectionRepository.GetAll().OrderByDescending(s => s.RdsmPkRefNo).FirstOrDefault();
            return rmDivRmuSecMaster != null ? rmDivRmuSecMaster.RdsmPkRefNo : 0;
        }

        public async Task<SectionRequestDTO> GetSectionById(int id)
        {
            RmDivRmuSecMaster async = await _repoUnit.SectionRepository.FindAsync(s => s.RdsmPkRefNo == id);
            return async != null ? _mapper.Map<RmDivRmuSecMaster, SectionRequestDTO>(async) : null;
        }

        public async Task<int> SaveSection(SectionRequestDTO model)
        {
            int pkRef = 0;
            try
            {
                RmDivRmuSecMaster form = _mapper.Map<RmDivRmuSecMaster>(model);
                if (form.RdsmPkRefNo != 0)
                {
                    RmDivRmuSecMaster divrmusec = await _repoUnit.SectionRepository.FindAsyncNoTracking(s => s.RdsmPkRefNo == form.RdsmPkRefNo);
                    form.RdsmModBy = _security.UserName;
                    form.RdsmModDt = new DateTime?(DateTime.UtcNow);
                    _repoUnit.SectionRepository.Update(form);
                    RmDdLookup asyncNoTracking1 = await _repoUnit.DDLookUpRepository.FindAsyncNoTracking(s => s.DdlType == "Division" && s.DdlTypeCode == divrmusec.RdsmDivCode && s.DdlTypeValue == divrmusec.RdsmDivision);
                    _repoUnit.DDLookUpRepository.Update(_mapper.Map<RmDdLookup>(new DDLookUpDTO()
                    {
                        Type = asyncNoTracking1.DdlType,
                        Active = new bool?(true),
                        CreateDate = new DateTime?(DateTime.UtcNow),
                        CreatedBy = _security.UserName,
                        ModifiedBy = _security.UserName,
                        ModifiedDate = new DateTime?(DateTime.UtcNow),
                        No = asyncNoTracking1.DdlPkRefNo,
                        TypeCode = model.DivCode,
                        TypeDesc = model.Division,
                        TypeRemarks = null,
                        TypeValue = model.Division
                    }));
                    RmDdLookup asyncNoTracking2 = await _repoUnit.DDLookUpRepository.FindAsyncNoTracking(s => s.DdlType == "RMU" && s.DdlTypeCode == divrmusec.RdsmRmuCode && s.DdlTypeValue == divrmusec.RdsmRmuName);
                    _repoUnit.DDLookUpRepository.Update(_mapper.Map<RmDdLookup>(new DDLookUpDTO()
                    {
                        Type = asyncNoTracking2.DdlType,
                        Active = new bool?(true),
                        CreateDate = new DateTime?(DateTime.UtcNow),
                        CreatedBy = _security.UserName,
                        ModifiedBy = _security.UserName,
                        ModifiedDate = new DateTime?(DateTime.UtcNow),
                        No = asyncNoTracking2.DdlPkRefNo,
                        TypeCode = model.RmuCode,
                        TypeDesc = model.RmuName,
                        TypeRemarks = null,
                        TypeValue = model.RmuName
                    }));
                    RmDdLookup asyncNoTracking3 = await _repoUnit.DDLookUpRepository.FindAsyncNoTracking(s => s.DdlType == "Section Code" && s.DdlTypeCode == divrmusec.RdsmSectionCode && s.DdlTypeValue == divrmusec.RdsmSectionName);
                    _repoUnit.DDLookUpRepository.Update(_mapper.Map<RmDdLookup>(new DDLookUpDTO()
                    {
                        Type = asyncNoTracking3.DdlType,
                        Active = new bool?(true),
                        CreateDate = new DateTime?(DateTime.UtcNow),
                        CreatedBy = _security.UserName,
                        ModifiedBy = _security.UserName,
                        ModifiedDate = new DateTime?(DateTime.UtcNow),
                        No = asyncNoTracking3.DdlPkRefNo,
                        TypeCode = model.SectionCode,
                        TypeDesc = model.SectionName,
                        TypeRemarks = null,
                        TypeValue = model.SectionName
                    }));
                }
                else
                {
                    if (_repoUnit.SectionRepository.Find(s => s.RdsmDivCode == model.DivCode && s.RdsmDivision == model.Division && s.RdsmRmuCode == model.RmuCode && s.RdsmRmuName == model.RmuName && s.RdsmSectionCode == model.SectionCode && s.RdsmSectionName == model.SectionName) != null)
                        pkRef = -1;
                    form.RdsmCrBy = _security.UserName;
                    form.RdsmCrDt = new DateTime?(DateTime.UtcNow);
                    _repoUnit.SectionRepository.Create(form);
                    _repoUnit.DDLookUpRepository.Create(_mapper.Map<RmDdLookup>(new DDLookUpDTO()
                    {
                        Type = "Division",
                        TypeCode = model.DivCode,
                        TypeDesc = model.Division,
                        TypeValue = model.Division,
                        CreatedBy = _security.UserName,
                        CreateDate = new DateTime?(DateTime.UtcNow),
                        ModifiedBy = _security.UserName,
                        ModifiedDate = new DateTime?(DateTime.UtcNow),
                        Active = new bool?(true)
                    }));
                    _repoUnit.DDLookUpRepository.Create(_mapper.Map<RmDdLookup>(new DDLookUpDTO()
                    {
                        Type = "RMU",
                        TypeCode = model.RmuCode,
                        TypeDesc = model.RmuName,
                        TypeValue = model.RmuName,
                        CreatedBy = _security.UserName,
                        CreateDate = new DateTime?(DateTime.UtcNow),
                        ModifiedBy = _security.UserName,
                        ModifiedDate = new DateTime?(DateTime.UtcNow),
                        Active = new bool?(true)
                    }));
                    _repoUnit.DDLookUpRepository.Create(_mapper.Map<RmDdLookup>(new DDLookUpDTO()
                    {
                        Type = "Section Code",
                        TypeCode = model.SectionCode,
                        TypeDesc = model.SectionName,
                        TypeValue = model.SectionName,
                        CreatedBy = _security.UserName,
                        CreateDate = new DateTime?(DateTime.UtcNow),
                        ModifiedBy = _security.UserName,
                        ModifiedDate = new DateTime?(DateTime.UtcNow),
                        Active = new bool?(true)
                    }));
                }
                await _repoUnit.CommitAsync();
                pkRef = form.RdsmPkRefNo;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return pkRef;
        }

        public async Task<bool> RemoveSection(int id)
        {
            RmDivRmuSecMaster rmDivRmuSecMaster = _repoUnit.SectionRepository.Find(s => s.RdsmPkRefNo == id);
            if (rmDivRmuSecMaster == null)
                return false;
            rmDivRmuSecMaster.RdsmActiveYn = new bool?(false);
            return (uint)await _repoUnit.CommitAsync() > 0U;
        }

        public async Task<PagingResult<SectionRequestDTO>> GetSectionList(
          FilteredPagingDefinition<SectionRequestDTO> filterOptions)
        {
            PagingResult<SectionRequestDTO> result = new PagingResult<SectionRequestDTO>();
            PagingResult<SectionRequestDTO> pagingResult = result;
            pagingResult.PageResult = await _repoUnit.SectionRepository.GetFilteredRecordList(filterOptions);
            pagingResult = (PagingResult<SectionRequestDTO>)null;
            pagingResult = result;
            pagingResult.TotalRecords = await _repoUnit.SectionRepository.GetFilteredRecordCount(filterOptions);
            pagingResult = (PagingResult<SectionRequestDTO>)null;
            PagingResult<SectionRequestDTO> sectionList = result;
            result = (PagingResult<SectionRequestDTO>)null;
            return sectionList;
        }

        public List<SelectListItem> GetList(string div, string rmu) => _repoUnit.SectionRepository.FindAll(s => s.RdsmDivCode == div && s.RdsmRmuCode == rmu && s.RdsmActiveYn == true).Select(s => new SelectListItem()
        {
            Text = s.RdsmSectionName,
            Value = s.RdsmSectionCode
        }).ToList();
    }
}
