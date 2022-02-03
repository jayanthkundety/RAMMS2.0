using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class RMUService : IRMUService
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        public RMUService(IRepositoryUnit repoUnit, IMapper mapper, ISecurity security) { _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit)); _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); _security = security ?? throw new ArgumentNullException(nameof(security)); }
        public long LastInsertedNo() { var model = _repoUnit.RMURepository.GetAll().OrderByDescending(s => s.RmuPkRefNo).FirstOrDefault(); if (model != null) { return model.RmuPkRefNo; } else { return 0; } }
        public async Task<RMURequestDTO> GetById(int id)
        {
            var model = await _repoUnit.RMURepository.FindAsync(s => s.RmuPkRefNo == id); if (model == null) { return null; }
            return _mapper.Map<Domain.Models.RmRmuMaster, RMURequestDTO>(model);
        }
        public async Task<int> Save(RMURequestDTO model)
        {
            int rowsAffected; try { var form = _mapper.Map<Domain.Models.RmRmuMaster>(model); if (form.RmuPkRefNo != 0) { _repoUnit.RMURepository.Update(form); } else { _repoUnit.RMURepository.Create(form); } await _repoUnit.CommitAsync(); return form.RmuPkRefNo; } catch (Exception ex) { await _repoUnit.RollbackAsync(); throw ex; }
            return rowsAffected;
        }
        public async Task<bool> Remove(int id)
        {
            var model = _repoUnit.RMURepository.Find(s => s.RmuPkRefNo == id);
            if (model != null)
            {
                model.RmuIsActive = false;
                return await _repoUnit.CommitAsync() != 0;
            }
            else { return false; }
        }
        public async Task<PagingResult<RMURequestDTO>> GetList(FilteredPagingDefinition<RMURequestDTO> filterOptions)
        {
            PagingResult<RMURequestDTO> result = new PagingResult<RMURequestDTO>();
            result.PageResult = await _repoUnit.RMURepository.GetFilteredRecordList(filterOptions);
            result.TotalRecords = await _repoUnit.RMURepository.GetFilteredRecordCount(filterOptions);
            return result;
        }

        public List<SelectListItem> GetList(string divcode)
        {
            return _repoUnit.RMURepository.FindAll(s => s.DivCode == divcode && s.RmuIsActive == true).Select(s => new SelectListItem
            {
                Text = s.RmuName,
                Value = s.RmuCode
            }).ToList();
        }
    }
}
