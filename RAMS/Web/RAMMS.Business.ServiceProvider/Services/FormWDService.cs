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

    public class FormWDService : IFormWDService
    {
        private readonly IFormWDRepository _repo;
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        private readonly IProcessService processService;
        public FormWDService(IRepositoryUnit repoUnit, IFormWDRepository repo, IMapper mapper, ISecurity security, IProcessService process)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security;
            processService = process;
            _repo = repo;
        }

        public async Task<FormWDResponseDTO> FindWDByW1ID(int id)
        {
            RmIwFormWd formWC = await _repo.FindWDByw1ID(id);
            return _mapper.Map<FormWDResponseDTO>(formWC);
        }

        public async Task<FormWDResponseDTO> FindFormWDByID(int id)
        {
            RmIwFormWd formWD = await _repo.FindFormWDByID(id);
            return _mapper.Map<FormWDResponseDTO>(formWD);
        }

        public async Task<IEnumerable<FormWDDtlResponseDTO>> FindFormWDDtlByID(int id)
        {
            IEnumerable<RmIwFormWdDtl> formWDDtl = await _repo.FindFormWDDtlByID(id);
            return _mapper.Map<IEnumerable<FormWDDtlResponseDTO>>(formWDDtl);
        }



        public async Task<int> SaveFormWD(FormWDResponseDTO FormWD)
        {
            FormWDResponseDTO formWDResponse;
            try
            {
                var domainModelFormWD = _mapper.Map<RmIwFormWd>(FormWD);
                domainModelFormWD.FwdPkRefNo = 0;
                var entity = _repoUnit.FormWDRepository.CreateReturnEntity(domainModelFormWD);
                formWDResponse = _mapper.Map<FormWDResponseDTO>(entity);
                return formWDResponse.PkRefNo;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }


        public int? DeleteFormWDClause(int Id)
        {
            try
            {
                return _repo.DeleteFormWDClause(Id);
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }

        public int? SaveFormWDClause(FormWDDtlResponseDTO FormWDDtl)
        {
            try
            {
                var model = _mapper.Map<RmIwFormWdDtl>(FormWDDtl);
                model.FwddPkRefNo = 0;
                return _repo.SaveFormWDClause(model);
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }

        public async Task<int> Update(FormWDResponseDTO FormWD)
        {
            int rowsAffected;
            try
            {
                int PkRefNo = FormWD.PkRefNo;
                int? Fw1PkRefNo = FormWD.Fw1PkRefNo;
               
                var domainModelformWD = _mapper.Map<RmIwFormWd>(FormWD);
                domainModelformWD.FwdPkRefNo = PkRefNo;
                domainModelformWD.FwdFw1PkRefNo = Fw1PkRefNo;
 
                domainModelformWD.FwdActiveYn = true;
                domainModelformWD = UpdateStatus(domainModelformWD);
                _repoUnit.FormWDRepository.Update(domainModelformWD);
                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }


        public RmIwFormWd UpdateStatus(RmIwFormWd form)
        {
            if (form.FwdPkRefNo > 0)
            {
                var existsObj = _repoUnit.FormWDRepository._context.RmIwFormWd.Where(x => x.FwdPkRefNo == form.FwdPkRefNo).Select(x => new { Status = x.FwdStatus, Log = x.FwdAuditLog }).FirstOrDefault();
                if (existsObj != null)
                {
                    form.FwdAuditLog = existsObj.Log;
                    form.FwdStatus = existsObj.Status;

                }

            }
            if (form.FwdSubmitSts && form.FwdStatus == "Saved")
            {
                form.FwdStatus = Common.StatusList.FormW2Submitted;
                form.FwdAuditLog = Utility.ProcessLog(form.FwdAuditLog, "Submitted By", "Submitted", form.FwdUsernameIssu, string.Empty, form.FwdDtIssu, _security.UserName);
                processService.SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = _security.UserName,
                    RmNotGroup = GroupNames.OperationsExecutive,
                    RmNotMessage = "Recorded By:" + form.FwdUsernameIssu + " - Form WD (" + form.FwdPkRefNo + ")",
                    RmNotOn = DateTime.Now,
                    RmNotUrl = "/InstructedWorks/EditFormWD?id=" + form.FwdPkRefNo.ToString(),
                    RmNotUserId = "",
                    RmNotViewed = ""
                }, true);
            }

            return form;
        }


        public async Task<int> DeActivateFormWD(int formNo)
        {
            int rowsAffected;
            try
            {
                var domainModelFormWD = await _repoUnit.FormWDRepository.GetByIdAsync(formNo);
                domainModelFormWD.FwdActiveYn = false;
                _repoUnit.FormWDRepository.Update(domainModelFormWD);
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
