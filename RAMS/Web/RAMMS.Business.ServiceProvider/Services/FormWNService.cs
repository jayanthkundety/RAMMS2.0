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

    public class FormWNService : IFormWNService
    {
        private readonly IFormWNRepository _repo;
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        private readonly IProcessService processService;
        public FormWNService(IRepositoryUnit repoUnit, IFormWNRepository repo, IMapper mapper, ISecurity security, IProcessService process)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security;
            processService = process;
            _repo = repo;
        }



        public async Task<FormWNResponseDTO> FindWNByW1ID(int id)
        {
            RmIwFormWn formWN = await _repo.FindWNByW1ID(id);
            return _mapper.Map<FormWNResponseDTO>(formWN);
        }

        public async Task<FormWNResponseDTO> FindFormWNByID(int id)
        {
            RmIwFormWn formWN = await _repo.FindFormWNByID(id);
            return _mapper.Map<FormWNResponseDTO>(formWN);
        }



        public async Task<int> SaveFormWN(FormWNResponseDTO FormWN)
        {
            FormWNResponseDTO formWNResponse;
            try
            {
                var domainModelFormWN = _mapper.Map<RmIwFormWn>(FormWN);
                domainModelFormWN.FwnPkRefNo = 0;

                var entity = _repoUnit.FormWNRepository.CreateReturnEntity(domainModelFormWN);
                formWNResponse = _mapper.Map<FormWNResponseDTO>(entity);
                return formWNResponse.PkRefNo;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }


        public async Task<int> Update(FormWNResponseDTO FormWN)
        {
            int rowsAffected;
            try
            {
                int PkRefNo = FormWN.PkRefNo;
                int? Fw1PkRefNo = FormWN.Fw1PkRefNo;
                var domainModelformWN = _mapper.Map<RmIwFormWn>(FormWN);
                domainModelformWN.FwnPkRefNo = PkRefNo;
                domainModelformWN.FwnFw1PkRefNo = Fw1PkRefNo;
                domainModelformWN.FwnActiveYn = true;
                domainModelformWN = UpdateStatus(domainModelformWN);
                _repoUnit.FormWNRepository.Update(domainModelformWN);
                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }



        public RmIwFormWn UpdateStatus(RmIwFormWn form)
        {
            if (form.FwnPkRefNo > 0)
            {
                var existsObj = _repoUnit.FormWNRepository._context.RmIwFormWn.Where(x => x.FwnPkRefNo == form.FwnPkRefNo).Select(x => new { Status = x.FwnStatus, Log = x.FwnAuditLog }).FirstOrDefault();
                if (existsObj != null)
                {
                    form.FwnAuditLog = existsObj.Log;
                    form.FwnStatus = existsObj.Status;

                }

            }
            if (form.FwnSubmitSts && form.FwnStatus == "Saved")
            {
                form.FwnStatus = Common.StatusList.FormW2Submitted;
                form.FwnAuditLog = Utility.ProcessLog(form.FwnAuditLog, "Submitted By", "Submitted", form.FwnUsernameIssu, string.Empty, form.FwnDtIssu, _security.UserName);
                processService.SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = _security.UserName,
                    RmNotGroup = GroupNames.OperationsExecutive,
                    RmNotMessage = "Recorded By:" + form.FwnUsernameIssu + " - Form WN (" + form.FwnPkRefNo + ")",
                    RmNotOn = DateTime.Now,
                    RmNotUrl = "/InstructedWorks/EditFormWN?id=" + form.FwnPkRefNo.ToString(),
                    RmNotUserId = "",
                    RmNotViewed = ""
                }, true);
            }

            return form;
        }


        public async Task<int> DeActivateFormWN(int formNo)
        {
            int rowsAffected;
            try
            {
                var domainModelFormWN = await _repoUnit.FormWNRepository.GetByIdAsync(formNo);
                domainModelFormWN.FwnActiveYn = false;
                _repoUnit.FormWNRepository.Update(domainModelFormWN);
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
