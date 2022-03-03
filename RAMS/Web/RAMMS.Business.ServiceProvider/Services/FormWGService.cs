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
    public class FormWGService : IFormWGService
    {

        private readonly IFormWGRepository _repo;
        private readonly IFormW2FcemRepository _repoFCEM;
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        private readonly IProcessService processService;
        public FormWGService(IRepositoryUnit repoUnit, IFormWGRepository repo, IMapper mapper, ISecurity security, IProcessService process, IFormW2FcemRepository repoFCEM)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _repoFCEM = repoFCEM ?? throw new ArgumentNullException(nameof(repoFCEM));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security;
            processService = process;
        }


        public async Task<FormWGResponseDTO> FindWGByID(int id)
        {
            RmIwFormWg formWG = await _repo.FindWGByID(id);
            return _mapper.Map<FormWGResponseDTO>(formWG);
        }


        public async Task<FormWGResponseDTO> FindWGByW1ID(int id)
        {
            RmIwFormWg formWG = await _repo.FindWGByW1ID(id);
            return _mapper.Map<FormWGResponseDTO>(formWG);
        }

        public async Task<FormW1ResponseDTO> GetFormW1ById(int formWId)
        {
            RmIwFormW1 formW1 = await _repo.GetFormW1ById(formWId);
            return _mapper.Map<FormW1ResponseDTO>(formW1);
        }

        public async Task<int> Save(FormWGResponseDTO formWGBO)
        {
            FormWGResponseDTO formWGResponse;
            try
            {
                var domainModelFormWG = _mapper.Map<RmIwFormWg>(formWGBO);
                domainModelFormWG.FwgPkRefNo = 0;
                domainModelFormWG.FwgActiveYn = true;
                domainModelFormWG.FwgPkRefNo = formWGBO.PkRefNo;
                domainModelFormWG = UpdateStatus(domainModelFormWG);
                var entity = _repoUnit.FormWGRepository.CreateReturnEntity(domainModelFormWG);
                formWGResponse = _mapper.Map<FormWGResponseDTO>(entity);
                return formWGResponse.PkRefNo;

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public async Task<int> Update(FormWGResponseDTO formWGDTO)
        {
            int rowsAffected;
            try
            {
                var domainModelformWg = _mapper.Map<RmIwFormWg>(formWGDTO);
                domainModelformWg.FwgPkRefNo = formWGDTO.PkRefNo;
                domainModelformWg.FwgActiveYn = true;
                domainModelformWg = UpdateStatus(domainModelformWg);
                _repoUnit.FormWGRepository.Update(domainModelformWg);
                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public RmIwFormWg UpdateStatus(RmIwFormWg form)
        {
            if (form.FwgPkRefNo > 0)
            {
                var existsObj = _repoUnit.FormWGRepository._context.RmIwFormWg.Where(x => x.FwgPkRefNo == form.FwgPkRefNo).Select(x => new { Status = x.FwgStatus, Log = x.FwgAuditLog }).FirstOrDefault();
                if (existsObj != null)
                {
                    form.FwgAuditLog = existsObj.Log;
                    form.FwgStatus = existsObj.Status;
                }

            }
            if (form.FwgSubmitSts && (string.IsNullOrEmpty(form.FwgStatus) || form.FwgStatus == Common.StatusList.FormWGSaved ))
            {
                form.FwgStatus = Common.StatusList.FormWGSubmitted;
                form.FwgAuditLog = Utility.ProcessLog(form.FwgAuditLog, "Recorded By", "Approved", form.FwgUsernameIssu, string.Empty, form.FwgDtIssu , _security.UserName);
                processService.SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = _security.UserName,
                    RmNotGroup = GroupNames.OperationsExecutive,
                    RmNotMessage = "Recorded By:" + form.FwgUsernameIssu  + " - Form WG (" + form.FwgPkRefNo + ")",
                    RmNotOn = DateTime.Now,
                    RmNotUrl = "/InstructedWorks/EditFormWG?id=" + form.FwgPkRefNo.ToString() + "&view=1",
                    RmNotUserId = "",
                    RmNotViewed = ""
                }, true);
            }
            else if (string.IsNullOrEmpty(form.FwgStatus))
                form.FwgStatus = Common.StatusList.FormWGSaved;

            return form;
        }
    }
}
