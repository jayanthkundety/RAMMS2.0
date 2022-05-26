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

    public class FormF3Service : IFormF3Service
    {
        private readonly IFormF3Repository _repo;
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        private readonly IProcessService processService;
        public FormF3Service(IRepositoryUnit repoUnit, IFormF3Repository repo, IMapper mapper, ISecurity security, IProcessService process)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security;
            processService = process;
            _repo = repo;
        }

        //public async Task<FormF3ResponseDTO> FindF3ByW1ID(int id)
        //{
        //    RmIwFormF3 formWC = await _repo.FindF3Byw1ID(id);
        //    return _mapper.Map<FormF3ResponseDTO>(formWC);
        //}

        //public async Task<FormF3ResponseDTO> FindFormF3ByID(int id)
        //{
        //    RmIwFormF3 formF3 = await _repo.FindFormF3ByID(id);
        //    return _mapper.Map<FormF3ResponseDTO>(formF3);
        //}

        //public async Task<IEnumerable<FormF3DtlResponseDTO>> FindFormF3DtlByID(int id)
        //{
        //    IEnumerable<RmFormF3Dtl> formF3Dtl = await _repo.FindFormF3DtlByID(id);
        //    return _mapper.Map<IEnumerable<FormF3DtlResponseDTO>>(formF3Dtl);
        //}


        public async Task<FormF3ResponseDTO> GetHeaderById(int id)
        {
            var header = await _repoUnit.FormF3Repository.FindAsync(s => s.Ff3hPkRefNo == id && s.Ff3hActiveYn == true);
            if (header == null)
            {
                return null;
            }
            return _mapper.Map<RmFormF3Hdr, FormF3ResponseDTO>(header);
        }


        public async Task<int> SaveFormF3(FormF3ResponseDTO FormF3)
        {
            FormF3ResponseDTO formF3Response;
            try
            {
                var domainModelFormF3 = _mapper.Map<RmFormF3Hdr>(FormF3);
                domainModelFormF3.Ff3hPkRefNo = 0;
                var entity = _repoUnit.FormF3Repository.CreateReturnEntity(domainModelFormF3);
                formF3Response = _mapper.Map<FormF3ResponseDTO>(entity);
                return formF3Response.PkRefNo;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }


        public int? DeleteFormF3Dtl(int Id)
        {
            try
            {
                return _repo.DeleteFormF3Dtl(Id);
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }

        public int? SaveFormF3Dtl(FormF3DtlResponseDTO FormF3Dtl)
        {
            try
            {
                var model = _mapper.Map<RmFormF3Dtl>(FormF3Dtl);
                model.Ff3dPkRefNo = 0;
                return _repo.SaveFormF3Dtl(model);
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }

        public async Task<int> Update(FormF3ResponseDTO FormF3)
        {
            int rowsAffected;
            try
            {
                int PkRefNo = FormF3.PkRefNo;
                int? Fw1PkRefNo = FormF3.PkRefNo;
               
                var domainModelformF3 = _mapper.Map<RmFormF3Hdr>(FormF3);
                domainModelformF3.Ff3hPkRefNo = PkRefNo;
               // domainModelformF3.FF3Fw1PkRefNo = Fw1PkRefNo;
 
                domainModelformF3.Ff3hActiveYn = true;
                domainModelformF3 = UpdateStatus(domainModelformF3);
                _repoUnit.FormF3Repository.Update(domainModelformF3);
                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }


        public RmFormF3Hdr UpdateStatus(RmFormF3Hdr form)
        {
            if (form.Ff3hPkRefNo > 0)
            {
                var existsObj = _repoUnit.FormF3Repository._context.RmFormF3Hdr.Where(x => x.Ff3hPkRefNo == form.Ff3hPkRefNo).Select(x => new { Status = x.Ff3hStatus, Log = x.Ff3hAuditLog }).FirstOrDefault();
                if (existsObj != null)
                {
                    form.Ff3hAuditLog = existsObj.Log;
                    form.Ff3hStatus = existsObj.Status;

                }

            }
            if (form.Ff3hSubmitSts && form.Ff3hStatus == "Saved")
            {
                form.Ff3hStatus = Common.StatusList.FormW2Submitted;
                form.Ff3hAuditLog = Utility.ProcessLog(form.Ff3hAuditLog, "Submitted By", "Submitted", form.Ff3hInspectedName, string.Empty, form.Ff3hInspectedDate, _security.UserName);
                processService.SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = _security.UserName,
                    RmNotGroup = GroupNames.OperationsExecutive,
                    RmNotMessage = "Recorded By:" + form.Ff3hInspectedName + " - Form F3 (" + form.Ff3hPkRefNo + ")",
                    RmNotOn = DateTime.Now,
                    RmNotUrl = "/InstructedWorks/EditFormF3?id=" + form.Ff3hPkRefNo.ToString(),
                    RmNotUserId = "",
                    RmNotViewed = ""
                }, true);
            }

            return form;
        }


        public async Task<int> DeActivateFormF3(int formNo)
        {
            int rowsAffected;
            try
            {
                var domainModelFormF3 = await _repoUnit.FormF3Repository.GetByIdAsync(formNo);
                domainModelFormF3.Ff3hActiveYn = false;
                _repoUnit.FormF3Repository.Update(domainModelFormF3);
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
