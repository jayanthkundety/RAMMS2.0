using System;
using RAMMS.Business.ServiceProvider.Interfaces;
using System.Linq;
using RAMMS.Repository.Interfaces;
using AutoMapper;
using RAMMS.DTO.RequestBO;
using System.Threading.Tasks;
using RAMMS.DTO.Wrappers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class DivisionService : IDivisionService
    {
        private readonly IRepositoryUnit _repoUnit; private readonly IMapper _mapper; private readonly ISecurity _security;
        public DivisionService(IRepositoryUnit repoUnit, IMapper mapper, ISecurity security)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit)); _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); _security = security ?? throw new ArgumentNullException(nameof(security));
        }
        public long LastInsertedNo()
        {
            var model = _repoUnit.DivisonRepository.GetAll().OrderByDescending(s => s.DivPkRefNo).FirstOrDefault(); if (model != null) { return model.DivPkRefNo; } else { return 0; }
        }
        public async Task<DivisionRequestDTO> GetById(int id)
        {
            var model = await _repoUnit.DivisonRepository.FindAsync(s => s.DivPkRefNo == id); if (model == null) { return null; }
            return _mapper.Map<Domain.Models.RmDivisionMaster, DivisionRequestDTO>(model);
        }
        public async Task<int> Save(DivisionRequestDTO model)
        {
            try
            {
                var form = _mapper.Map<Domain.Models.RmDivisionMaster>(model);
                if (form.DivPkRefNo != 0)
                {
                    _repoUnit.DivisonRepository.Update(form);
                }
                else { _repoUnit.DivisonRepository.Create(form); }
                await _repoUnit.CommitAsync(); return form.DivPkRefNo;
            }
            catch (Exception ex) { await _repoUnit.RollbackAsync(); throw ex; }
        }
        public async Task<bool> Remove(int id)
        {
            var model = _repoUnit.DivisonRepository.Find(s => s.DivPkRefNo == id);
            if (model != null) { model.DivIsActive = false; return await _repoUnit.CommitAsync() != 0; } else { return false; }
        }
        public async Task<PagingResult<DivisionRequestDTO>> GetList(FilteredPagingDefinition<DivisionRequestDTO> filterOptions)
        {
            PagingResult<DivisionRequestDTO> result = new PagingResult<DivisionRequestDTO>();
            result.PageResult = await _repoUnit.DivisonRepository.GetFilteredRecordList(filterOptions);
            result.TotalRecords = await _repoUnit.DivisonRepository.GetFilteredRecordCount(filterOptions); return result;
        }

        public List<SelectListItem> GetList()
        {
            return _repoUnit.DivisonRepository.FindAll(s => s.DivIsActive == true).Select(s => new SelectListItem
            {
                Text = s.DivName,
                Value = s.DivCode
            }).ToList();
        }

        public async Task<PagingResult<DivRmuSectionRequestDTO>> GetList(FilteredPagingDefinition<DivRmuSectionRequestDTO> filterOptions)
        {
            PagingResult<DivRmuSectionRequestDTO> result = new PagingResult<DivRmuSectionRequestDTO>();
            result.PageResult = await _repoUnit.DivRmuSectionRepository.GetFilteredRecordList(filterOptions);
            result.TotalRecords = await _repoUnit.DivRmuSectionRepository.GetFilteredRecordCount(filterOptions); 
            return result;
        }

        public async Task<int> Save(DivRmuSectionRequestDTO model)
        {
            try
            {
                var form = _mapper.Map<Domain.Models.RmDivRmuSecMaster>(model);
                
                if (form.RdsmPkRefNo != 0)
                {
                    _repoUnit.DivRmuSectionRepository.Update(form);
                }
                else { 
                    _repoUnit.DivRmuSectionRepository.Create(form); 
                }
                await _repoUnit.CommitAsync(); return form.RdsmPkRefNo;
            }
            catch (Exception ex) { await _repoUnit.RollbackAsync(); throw ex; }
        }

        public async Task<DivRmuSectionRequestDTO> GetDivRmuSectionById(int id)
        {
            var model = await _repoUnit.DivRmuSectionRepository.FindAsync(s => s.RdsmPkRefNo == id); if (model == null) { return null; }
            return _mapper.Map<Domain.Models.RmDivRmuSecMaster, DivRmuSectionRequestDTO>(model);
        }
    }
}
