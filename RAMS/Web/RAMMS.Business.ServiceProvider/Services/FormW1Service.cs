using AutoMapper;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Domain.EF;
using RAMMS.DTO.ResponseBO;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Services
{

    public class FormW1Service : IFormW1Service
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        private readonly IProcessService processService;
        public FormW1Service(IRepositoryUnit repoUnit, IMapper mapper, ISecurity security, IProcessService process)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security;
            processService = process;
        }

        public async Task<int> SaveFormW1(FormW1ResponseDTO FormW1)
        {
            FormW1ResponseDTO formW1Response;
            try
            {
                var domainModelFormW1 = _mapper.Map<RmIwFormW1>(FormW1);
               // domainModelFormW1 = UpdateStatus(domainModelFormW1);
                var entity = _repoUnit.FormW1Repository.SaveFormW1(domainModelFormW1);
                formW1Response = _mapper.Map<FormW1ResponseDTO>(entity);
                return formW1Response.PkRefNo;

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

    }
}
