using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class AuditActionService : IAuditActionService
    {
        private readonly IRepositoryUnit _repoUnit; private readonly IMapper _mapper;
        private readonly ISecurity _security;
        public AuditActionService(IRepositoryUnit repoUnit, IMapper mapper, ISecurity security)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit)); _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security ?? throw new ArgumentNullException(nameof(security));
        }
        public long LastAuditActionInsertedNo()
        {
            var model = _repoUnit.AuditActionRepository.GetAll().OrderByDescending(s => s.AlaPkRefNo).FirstOrDefault();
            if (model != null) { return model.AlaPkRefNo; } else { return 0; }
        }
        public async Task<AuditActionRequestDTO> GetAuditActionById(int id)
        {
            var model = await _repoUnit.AuditActionRepository.FindAsync(s => s.AlaPkRefNo == id); if (model == null) { return null; }
            return _mapper.Map<Domain.Models.RmAuditLogAction, AuditActionRequestDTO>(model);
        }
        public async Task<long> SaveAuditAction(AuditActionRequestDTO model)
        {
            try
            {
                var form = _mapper.Map<Domain.Models.RmAuditLogAction>(model);
                if (form.AlaPkRefNo != 0) { _repoUnit.AuditActionRepository.Update(form); } else { _repoUnit.AuditActionRepository.Create(form); }
                await _repoUnit.CommitAsync();
                return form.AlaPkRefNo;
            }
            catch (Exception ex) { await _repoUnit.RollbackAsync(); throw ex; }
        }
        public async Task<bool> RemoveAuditAction(int id)
        {
            var model = _repoUnit.AuditActionRepository.Find(s => s.AlaPkRefNo == id);
            if (model != null) { return await _repoUnit.CommitAsync() != 0; } else { return false; }
        }
        public async Task<PagingResult<AuditActionRequestDTO>> GetAuditActionList(FilteredPagingDefinition<AuditActionRequestDTO> filterOptions)
        {
            PagingResult<AuditActionRequestDTO> result = new PagingResult<AuditActionRequestDTO>();
            result.PageResult = await _repoUnit.AuditActionRepository.GetFilteredRecordList(filterOptions);
            result.TotalRecords = await _repoUnit.AuditActionRepository.GetFilteredRecordCount(filterOptions);
            return result;
        }
    }
}

