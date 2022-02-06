using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Common;
using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.Repository.Interfaces;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class FormW2Service : IFormW2Service
    {
        private readonly IFormW2Repository _repo;
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        private readonly IProcessService processService;
        public FormW2Service(IRepositoryUnit repoUnit, IFormW2Repository repo, IMapper mapper, ISecurity security, IProcessService process)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security;
            processService = process;
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<FormW2ResponseDTO> FindW2ByID(int id)
        {
            RmIwFormW2 formW2 = await _repo.FindW2ByID(id);
            return _mapper.Map<FormW2ResponseDTO>(formW2);
        }

        public async Task<int> Save(FormW2ResponseDTO formW2BO)
        {
            FormW2ResponseDTO formW2Response;
            try
            {
                var domainModelFormW2 = _mapper.Map<RmIwFormW2>(formW2BO);
                //domainModelFormW2 = UpdateStatus(domainModelFormW2);
                var entity = _repoUnit.FormW2Repository.CreateReturnEntity(domainModelFormW2);
                formW2Response = _mapper.Map<FormW2ResponseDTO>(entity);
                return formW2Response.PkRefNo;

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public async Task<int> Update(FormW2ResponseDTO formW2Bo)
        {
            int rowsAffected;
            try
            {
                var domainModelformW2 = _mapper.Map<RmIwFormW2>(formW2Bo);
                domainModelformW2.Fw2ActiveYn = true;
                //domainModelformW2 = UpdateStatus(domainModelformW2);
                _repoUnit.FormW2Repository.Update(domainModelformW2);
                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }
    }
}
