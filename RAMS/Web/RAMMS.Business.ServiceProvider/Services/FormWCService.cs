using System;
using System.Collections;
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
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;


namespace RAMMS.Business.ServiceProvider.Services
{
    public class FormWCService : IFormWCService
    {

        private readonly IFormWCRepository _repo;
        private readonly IFormW2FcemRepository _repoFCEM;
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        private readonly IProcessService processService;
        public FormWCService(IRepositoryUnit repoUnit, IFormWCRepository repo, IMapper mapper, ISecurity security, IProcessService process, IFormW2FcemRepository repoFCEM)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _repoFCEM = repoFCEM ?? throw new ArgumentNullException(nameof(repoFCEM));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security;
            processService = process;
        }


        public async Task<FormWCResponseDTO> FindWCByID(int id)
        {
            RmIwFormWc formWC = await _repo.FindWCByID(id);
            return _mapper.Map<FormWCResponseDTO>(formWC);
        }

        public async Task<FormW1ResponseDTO> GetFormW1ById(int formW1Id)
        {
            RmIwFormW1 formW1 = await _repo.GetFormW1ById(formW1Id);
            return _mapper.Map<FormW1ResponseDTO>(formW1);
        }

        public async Task<int> Save(FormWCResponseDTO formWCBO)
        {
            FormWCResponseDTO formWCResponse;
            try
            {
                var domainModelFormWC = _mapper.Map<RmIwFormWc>(formWCBO);
                domainModelFormWC.FwcPkRefNo = 0;
                domainModelFormWC.FwcActiveYn = true;
                domainModelFormWC = UpdateStatus(domainModelFormWC);
                var entity = _repoUnit.FormWCRepository.CreateReturnEntity(domainModelFormWC);
                formWCResponse = _mapper.Map<FormWCResponseDTO>(entity);
                return formWCResponse.PkRefNo;

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public Task<int> Update(FormWCResponseDTO formWCDTO)
        {
            throw new NotImplementedException();
        }

        public RmIwFormWc UpdateStatus(RmIwFormWc form)
        {
            if (form.FwcPkRefNo > 0)
            {
                var existsObj = _repoUnit.FormWCRepository._context.RmIwFormWc.Where(x => x.FwcPkRefNo == form.FwcPkRefNo).Select(x => new { Status = x.FwcStatus, Log = x.FwcAuditLog }).FirstOrDefault();
                if (existsObj != null)
                {
                    form.FwcAuditLog = existsObj.Log;
                    form.FwcStatus = existsObj.Status;
                }

            }
            if (form.FwcSubmitSts && (string.IsNullOrEmpty(form.FwcStatus) || form.FwcStatus == Common.StatusList.FormWCIssued ))
            {
                form.FwcStatus = Common.StatusList.FormWCSubmitted;
                form.FwcAuditLog = Utility.ProcessLog(form.FwcAuditLog, "Recorded By", "Approved", form.FwcUsernameIssu, string.Empty, form.FwcDtIssu , _security.UserName);
                processService.SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = _security.UserName,
                    RmNotGroup = GroupNames.OperationsExecutive,
                    RmNotMessage = "Recorded By:" + form.FwcUsernameIssu  + " - Form WC (" + form.FwcFw1PkRefNo + ")",
                    RmNotOn = DateTime.Now,
                    RmNotUrl = "/InstructedWorks/EditFormWC?id=" + form.FwcPkRefNo.ToString() + "&view=1",
                    RmNotUserId = "",
                    RmNotViewed = ""
                }, true);
            }
            else if (string.IsNullOrEmpty(form.FwcStatus))
                form.FwcStatus = Common.StatusList.FormWCIssued;

            return form;
        }
    }
}
