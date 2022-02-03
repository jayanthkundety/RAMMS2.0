using System;
using System.Threading.Tasks;
using AutoMapper;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using System.Linq;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class ModuleGroupRightsService : IModuleGroupRightsService
    {
        private readonly IRepositoryUnit _repoUnit; private readonly IMapper _mapper;
        private readonly ISecurity _security; public ModuleGroupRightsService(IRepositoryUnit repoUnit, IMapper mapper, ISecurity security) { _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit)); _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); _security = security ?? throw new ArgumentNullException(nameof(security)); }
        public long LastDetailInsertedNo()
        {
            var model = _repoUnit.ModuleGroupRepository.GetAll().OrderByDescending(s => s.MgrPkId).FirstOrDefault();
            if (model != null) { return model.MgrPkId; } else { return 0; }
        }
        public async Task<ModuleGroupRightsRequestDTO> GetDetailById(int id)
        {
            var model = await _repoUnit.ModuleGroupRepository.FindAsync(s => s.MgrPkId == id);
            if (model == null) { return null; }
            return _mapper.Map<Domain.Models.RmModuleGroupRights, ModuleGroupRightsRequestDTO>(model);
        }
        public async Task<int> SaveDetail(ModuleGroupRightsRequestDTO model)
        {
            try
            {
                var form = _mapper.Map<Domain.Models.RmModuleGroupRights>(model);
                if (form.MgrPkId != 0) { _repoUnit.ModuleGroupRepository.Update(form); } else { _repoUnit.ModuleGroupRepository.Create(form); }
                await _repoUnit.CommitAsync(); return form.MgrPkId;
            }
            catch (Exception ex) { await _repoUnit.RollbackAsync(); throw ex; }
        }
        public async Task<bool> RemoveDetail(int id)
        {
            var model = _repoUnit.ModuleGroupRepository.Find(s => s.MgrPkId == id);
            if (model != null) { return await _repoUnit.CommitAsync() != 0; } else { return false; }
        }
        public async Task<PagingResult<ModuleGroupRightsRequestDTO>> GetDetailList(FilteredPagingDefinition<ModuleGroupRightsRequestDTO> filterOptions)
        {
            PagingResult<ModuleGroupRightsRequestDTO> result = new PagingResult<ModuleGroupRightsRequestDTO>();
            result.PageResult = await _repoUnit.ModuleGroupRepository.GetFilteredRecordList(filterOptions);
            result.TotalRecords = await _repoUnit.ModuleGroupRepository.GetFilteredRecordCount(filterOptions); return result;
        }
    }
}

