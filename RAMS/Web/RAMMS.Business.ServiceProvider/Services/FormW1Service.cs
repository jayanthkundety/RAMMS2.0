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

    public class FormW1Service : IFormW1Service
    {
        private readonly IFormW1Repository _repo;
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        private readonly IProcessService processService;
        public FormW1Service(IRepositoryUnit repoUnit, IFormW1Repository repo, IMapper mapper, ISecurity security, IProcessService process)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security;
            processService = process;
            _repo = repo;
        }

        public async Task<FormW1ResponseDTO> FindFormW1ByID(int id)
        {
            RmIwFormW1 formW1 = await _repo.FindFormW1ByID(id);
            return _mapper.Map<FormW1ResponseDTO>(formW1);
        }

        public async Task<int> LastInsertedIMAGENO(string hederId, string type)
        {
            int imageCt = await _repoUnit.FormW1Repository.GetImageId(hederId, type);
            return imageCt;
        }

        public async Task<int> LastInsertedIMAGENO(int hederId, string type)
        {
            int imageCt = await _repoUnit.FormW1Repository.GetImageIdByW1Id(hederId, type);
            return imageCt;
        }


        public async Task<int> SaveFormW1(FormW1ResponseDTO FormW1)
        {
            FormW1ResponseDTO formW1Response;
            try
            {
                var domainModelFormW1 = _mapper.Map<RmIwFormW1>(FormW1);
                // domainModelFormW1 = UpdateStatus(domainModelFormW1);
                //  var entity = _repoUnit.FormW1Repository.SaveFormW1(domainModelFormW1);

                var entity = _repoUnit.FormW1Repository.CreateReturnEntity(domainModelFormW1);
                formW1Response = _mapper.Map<FormW1ResponseDTO>(entity);
                return formW1Response.PkRefNo;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public RmIwFormW1 UpdateStatus(RmIwFormW1 form)
        {
            if (form.Fw1PkRefNo > 0)
            {
                var existsObj = _repoUnit.FormW1Repository._context.RmIwFormW1.Where(x => x.Fw1PkRefNo == form.Fw1PkRefNo).Select(x => new { Status = x.Fw1Status, Log = x.Fw1AuditLog }).FirstOrDefault();
                if (existsObj != null)
                {
                    form.Fw1AuditLog = existsObj.Log;
                    form.Fw1Status = existsObj.Status;

                }

            }
            if (form.Fw1SubmitSts && form.Fw1Status == "Saved")
            {
                form.Fw1Status = Common.StatusList.FormW2Submitted;
                form.Fw1AuditLog = Utility.ProcessLog(form.Fw1AuditLog, "Submitted By", "Submitted", form.Fw1UsernameReq, string.Empty, form.Fw1DtReq, _security.UserName);
                processService.SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = _security.UserName,
                    RmNotGroup = GroupNames.OperationsExecutive,
                    RmNotMessage = "Recorded By:" + form.Fw1UsernameReq + " - Form W1 (" + form.Fw1PkRefNo + ")",
                    RmNotOn = DateTime.Now,
                    RmNotUrl = "/InstructedWorks/EditFormW1?id=" + form.Fw1PkRefNo.ToString(),
                    RmNotUserId = "",
                    RmNotViewed = ""
                }, true);
            }

            return form;
        }

        public async Task<List<FormIWImageResponseDTO>> GetImageList(string IwRefNo)
        {
            List<FormIWImageResponseDTO> images = new List<FormIWImageResponseDTO>();
            try
            {
                var getList = await _repoUnit.FormW1Repository.GetImagelist(IwRefNo);
                foreach (var listItem in getList)
                {
                    images.Add(_mapper.Map<FormIWImageResponseDTO>(listItem));
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return images;
        }


        public async Task<int> SaveImage(List<FormIWImageResponseDTO> image)
        {
            int rowsAffected;
            try
            {
                var domainModelFormW1 = new List<RmIwformImage>();

                foreach (var list in image)
                {
                    domainModelFormW1.Add(_mapper.Map<RmIwformImage>(list));
                }
                _repoUnit.FormW1Repository.SaveImage(domainModelFormW1);
                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> DeActivateImage(int imageId)
        {
            int rowsAffected;
            try
            {
                var domainModelIw = await _repoUnit.FormW1Repository.GetImageById(imageId);
                domainModelIw.FiwiActiveYn = false;
                _repoUnit.FormW1Repository.UpdateImage(domainModelIw);

                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }



        public async Task<int> Update(FormW1ResponseDTO FormW1)
        {
            int rowsAffected;
            try
            {
                var domainModelformW1 = _mapper.Map<RmIwFormW1>(FormW1);
                domainModelformW1.Fw1ActiveYn = true;
                domainModelformW1 = UpdateStatus(domainModelformW1);
                _repoUnit.FormW1Repository.Update(domainModelformW1);
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
