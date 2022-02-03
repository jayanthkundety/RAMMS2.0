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
    public class AuditTransactionService : IAuditTransactionService
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        public AuditTransactionService(IRepositoryUnit repoUnit, IMapper mapper, ISecurity security)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit)); _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); _security = security ?? throw new ArgumentNullException(nameof(security));
        }
        public long LastAuditTransactionInsertedNo()
        {
            var model = _repoUnit.AuditTransactionRepository.GetAll().OrderByDescending(s => s.AltPkRefNo).FirstOrDefault();
            if (model != null) { return model.AltPkRefNo; } else { return 0; }
        }
        public async Task<AuditTransactionRequestDTO> GetAuditTransactionById(int id)
        {
            var model = await _repoUnit.AuditTransactionRepository.FindAsync(s => s.AltPkRefNo == id); if (model == null) { return null; }
            return _mapper.Map<Domain.Models.RmAuditLogTransaction, AuditTransactionRequestDTO>(model);
        }
        public async Task<long> SaveAuditTransaction(AuditTransactionRequestDTO model)
        {
            try
            {
                var form = _mapper.Map<Domain.Models.RmAuditLogTransaction>(model);
                if (form.AltPkRefNo != 0) { _repoUnit.AuditTransactionRepository.Update(form); } else { _repoUnit.AuditTransactionRepository.Create(form); }
                await _repoUnit.CommitAsync();
                return form.AltPkRefNo;
            }
            catch (Exception ex) { await _repoUnit.RollbackAsync(); throw ex; }
        }
        public async Task<bool> RemoveAuditTransaction(int id)
        {
            var model = _repoUnit.AuditTransactionRepository.Find(s => s.AltPkRefNo == id); if (model != null)
            {
                return await _repoUnit.CommitAsync() != 0;
            }
            else { return false; }
        }
        public async Task<PagingResult<AuditTransactionRequestDTO>> GetAuditTransactionList(FilteredPagingDefinition<AuditTransactionRequestDTO> filterOptions)
        {
            PagingResult<AuditTransactionRequestDTO> result = new PagingResult<AuditTransactionRequestDTO>();
            result.PageResult = await _repoUnit.AuditTransactionRepository.GetFilteredRecordList(filterOptions);
            result.TotalRecords = await _repoUnit.AuditTransactionRepository.GetFilteredRecordCount(filterOptions);
            return result;
        }
    }
}
