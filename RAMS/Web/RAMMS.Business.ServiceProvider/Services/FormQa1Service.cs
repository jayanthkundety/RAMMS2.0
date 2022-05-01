using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Common;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class FormQa1Service : IFormQa1Service
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly IProcessService processService;
        private readonly ISecurity _security;

        public FormQa1Service(IRepositoryUnit repoUnit, IMapper mapper, IProcessService process, ISecurity security)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            processService = process;
            _security = security;
        }

        public async Task<PagingResult<FormQa1HeaderDTO>> GetFilteredFormQa1Grid(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions)
        {
            PagingResult<FormQa1HeaderDTO> result = new PagingResult<FormQa1HeaderDTO>();

            List<FormQa1HeaderDTO> formList = new List<FormQa1HeaderDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormQa1Repository.GetFilteredRecordList(filterOptions);

                result.TotalRecords = await _repoUnit.FormQa1Repository.GetFilteredRecordCount(filterOptions).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    formList.Add(_mapper.Map<FormQa1HeaderDTO>(listData));
                }

                result.PageResult = formList;

                result.PageNo = filterOptions.StartPageNo;
                result.FilteredRecords = result.PageResult != null ? result.PageResult.Count : 0;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return result;
        }

        public async Task<FormQa1HeaderDTO> FindQa1Details(FormQa1HeaderDTO header)
        {
            var obj = _repoUnit.FormQa1Repository.FindAsync(x => x.Fqa1hRmu == header.Rmu && x.Fqa1hSecCode == header.SecCode && x.Fqa1hActCode == header.ActCode && x.Fqa1hDt.Value.Year == header.Dt.Value.Year && x.Fqa1hDt.Value.Month == header.Dt.Value.Month && x.Fqa1hDt.Value.Day == header.Dt.Value.Day && x.Fqa1hCrew == header.Crew && x.Fqa1hActiveYn == true).Result;
            return _mapper.Map<FormQa1HeaderDTO>(obj);
        }

        public async Task<FormQa1HeaderDTO> FindQa1Details(int pkRefNo)
        {
            var obj = _repoUnit.FormQa1Repository.FindAsync(x => x.Fqa1hPkRefNo == pkRefNo && x.Fqa1hActiveYn == true).Result;
            return _mapper.Map<FormQa1HeaderDTO>(obj);
        }

        public async Task<FormQa1HeaderDTO> FindAndSaveFormQA1Hdr(FormQa1HeaderDTO header, bool updateSubmit)
        {
            var formQa1 = _mapper.Map<RmFormQa1Hdr>(header);
            formQa1.Fqa1hPkRefNo = header.PkRefNo;
            var response = await _repoUnit.FormQa1Repository.FindSaveFormQa1Hdr(formQa1, updateSubmit);
            return _mapper.Map<FormQa1HeaderDTO>(response);
        }

        public async Task<PagingResult<FormQa1EqVhDTO>> GetEquipmentFormQa1Grid(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id)
        {
            PagingResult<FormQa1EqVhDTO> result = new PagingResult<FormQa1EqVhDTO>();

            List<FormQa1EqVhDTO> formQa1EquipList = new List<FormQa1EqVhDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormQa1Repository.GetFilteredEqpRecordList(filterOptions, id);

                result.TotalRecords = await _repoUnit.FormQa1Repository.GetFilteredEqpRecordCount(filterOptions, id).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    formQa1EquipList.Add(_mapper.Map<FormQa1EqVhDTO>(listData));
                }

                result.PageResult = formQa1EquipList;

                result.PageNo = filterOptions.StartPageNo;
                result.FilteredRecords = result.PageResult != null ? result.PageResult.Count : 0;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return result;
        }

        public async Task<PagingResult<FormQa1MatDTO>> GetMaterialFormQa1Grid(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id)
        {
            PagingResult<FormQa1MatDTO> result = new PagingResult<FormQa1MatDTO>();

            List<FormQa1MatDTO> formQa1MatList = new List<FormQa1MatDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormQa1Repository.GetFilteredMatRecordList(filterOptions, id);

                result.TotalRecords = await _repoUnit.FormQa1Repository.GetFilteredMatRecordCount(filterOptions, id).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    formQa1MatList.Add(_mapper.Map<FormQa1MatDTO>(listData));
                }

                result.PageResult = formQa1MatList;

                result.PageNo = filterOptions.StartPageNo;
                result.FilteredRecords = result.PageResult != null ? result.PageResult.Count : 0;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return result;
        }

        public async Task<PagingResult<FormQa1GenDTO>> GetGeneralFormQa1Grid(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id)
        {
            PagingResult<FormQa1GenDTO> result = new PagingResult<FormQa1GenDTO>();

            List<FormQa1GenDTO> formQa1GenList = new List<FormQa1GenDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormQa1Repository.GetFilteredGenRecordList(filterOptions, id);

                result.TotalRecords = await _repoUnit.FormQa1Repository.GetFilteredEqpRecordCount(filterOptions, id).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    formQa1GenList.Add(_mapper.Map<FormQa1GenDTO>(listData));
                }

                result.PageResult = formQa1GenList;

                result.PageNo = filterOptions.StartPageNo;
                result.FilteredRecords = result.PageResult != null ? result.PageResult.Count : 0;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return result;
        }


        public async Task<FormQa1GenDTO> GetGenDetails(int pkRefNo)
        {
            var response = await _repoUnit.FormQa1Repository.GetGenDetails(pkRefNo);
            return _mapper.Map<FormQa1GenDTO>(response);
        }


        public async Task<FormQa1EqVhDTO> GetEquipDetails(int pkRefNo)
        {
            var response = await _repoUnit.FormQa1Repository.GetEquipDetails(pkRefNo);
            return _mapper.Map<FormQa1EqVhDTO>(response);
        }

        public async Task<FormQa1MatDTO> GetMatDetails(int pkRefNo)
        {
            var response = await _repoUnit.FormQa1Repository.GetMatDetails(pkRefNo);
            return _mapper.Map<FormQa1MatDTO>(response);
        }

        public async Task<FormQa1HeaderDTO> GetFormQA1(int pkRefNo)
        {
            var _formQA1 = await _repoUnit.FormQa1Repository.GetFormQA1(pkRefNo);
            var response = _mapper.Map<FormQa1HeaderDTO>(_formQA1);
            response.EqVh = new List<FormQa1EqVhDTO>();
            foreach (var listData in _formQA1.RmFormQa1EqVh)
            {
                response.EqVh.Add(_mapper.Map<FormQa1EqVhDTO>(listData));
            }

            response.Mat = new List<FormQa1MatDTO>();
            foreach (var listData in _formQA1.RmFormQa1Mat)
            {
                response.Mat.Add(_mapper.Map<FormQa1MatDTO>(listData));
            }

            response.Gen = new List<FormQa1GenDTO>();
            foreach (var listData in _formQA1.RmFormQa1Gen)
            {
                response.Gen.Add(_mapper.Map<FormQa1GenDTO>(listData));
            }

            response.Ssc = new FormQa1SscDTO();
            if (_formQA1.RmFormQa1Ssc.Count > 0)
                response.Ssc = _mapper.Map<FormQa1SscDTO>(_formQA1.RmFormQa1Ssc.FirstOrDefault());

            response.Tes = new FormQa1TesDTO();
            if (_formQA1.RmFormQa1Tes.Count > 0)
                response.Tes = _mapper.Map<FormQa1TesDTO>(_formQA1.RmFormQa1Tes.FirstOrDefault());

            response.Wcq = new FormQa1WcqDTO();
            if (_formQA1.RmFormQa1Wcq.Count > 0)
                response.Wcq = _mapper.Map<FormQa1WcqDTO>(_formQA1.RmFormQa1Wcq.FirstOrDefault());

            response.We = new FormQa1WeDTO();
            if (_formQA1.RmFormQa1We.Count > 0)
                response.We = _mapper.Map<FormQa1WeDTO>(_formQA1.RmFormQa1We.FirstOrDefault());

            response.Gc = new FormQa1GCDTO();
            if (_formQA1.RmFormQa1Gc.Count > 0)
                response.Gc = _mapper.Map<FormQa1GCDTO>(_formQA1.RmFormQa1Gc.FirstOrDefault());

            response.Lab = new FormQa1LabDTO();
            if (_formQA1.RmFormQa1Lab.Count > 0)
                response.Lab = _mapper.Map<FormQa1LabDTO>(_formQA1.RmFormQa1Lab.FirstOrDefault());

            return response;
        }

        public async Task<FormQa1LabDTO> GetLabourDetails(int pkRefNo)
        {
            var response = await _repoUnit.FormQa1Repository.GetLabourDetails(pkRefNo);
            return _mapper.Map<FormQa1LabDTO>(response);
        }

        public int? SaveMaterial(FormQa1MatDTO formQa1Mat)
        {
            try
            {
                var res = _mapper.Map<RmFormQa1Mat>(formQa1Mat);
                res.Fqa1mPkRefNo = formQa1Mat.PkRefNo;
                res.Fqa1mFqa1hPkRefNo = formQa1Mat.Fqa1hPkRefNo;
                return _repoUnit.FormQa1Repository.SaveMaterial(res);
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        public int? DeleteMaterial(int id)
        {
            try
            {
                return _repoUnit.FormQa1Repository.DeleteMaterial(id);
            }
            catch (Exception ex)
            {
                return 500;
            }
        }


        public int? SaveGeneral(FormQa1GenDTO formQa1Gen)
        {
            try
            {
                var res = _mapper.Map<RmFormQa1Gen>(formQa1Gen);
                res.Fqa1genPkRefNo = formQa1Gen.PkRefNo;
                res.Fqa1genFqa1hPkRefNo = formQa1Gen.Fqa1hPkRefNo;
                return _repoUnit.FormQa1Repository.SaveGeneral(res);
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        public int? DeleteGeneral(int id)
        {
            try
            {
                return _repoUnit.FormQa1Repository.DeleteGeneral(id);
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        public int? SaveEquipment(FormQa1EqVhDTO formQa1EqVh)
        {
            try
            {
                var res = _mapper.Map<RmFormQa1EqVh>(formQa1EqVh);
                res.Fqa1evPkRefNo = formQa1EqVh.PkRefNo;
                res.Fqa1evFqa1hPkRefNo = formQa1EqVh.Fqa1hPkRefNo;
                return _repoUnit.FormQa1Repository.SaveEquipment(res);
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        public int? DeleteEquipment(int id)
        {
            try
            {
                return _repoUnit.FormQa1Repository.DeleteEquipment(id);
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        public async Task<int> SaveFormQA1(FormQa1HeaderDTO formQa1Header, bool updateSubmit)

        {
            var formQA1 = _mapper.Map<RmFormQa1Hdr>(formQa1Header);
            formQA1.Fqa1hPkRefNo = formQa1Header.PkRefNo;
            formQA1 = UpdateStatus(formQA1);
            _repoUnit.FormQa1Repository.Update(formQA1);


            var formGc = _mapper.Map<RmFormQa1Gc>(formQa1Header.Gc);
            formGc.Fqa1gcPkRefNo = formQa1Header.Lab.PkRefNo;
            formGc.Fqa1gcFqa1hPkRefNo = formQa1Header.PkRefNo;
            await _repoUnit.FormQa1Repository.SaveGC(formGc);

            var formLab = _mapper.Map<RmFormQa1Lab>(formQa1Header.Lab);
            formLab.Fqa1lPkRefNo = formQa1Header.Lab.PkRefNo;
            formLab.Fqa1lFqa1hPkRefNo = formQa1Header.PkRefNo;
            await _repoUnit.FormQa1Repository.SaveLabour(formLab);

            var formSsc = _mapper.Map<RmFormQa1Ssc>(formQa1Header.Ssc);
            formSsc.Fqa1sscPkRefNo = formQa1Header.Ssc.PkRefNo;
            formSsc.Fqa1sscFqa1hPkRefNo = formQa1Header.PkRefNo;
            await _repoUnit.FormQa1Repository.SaveSSC(formSsc);

            var formTes = _mapper.Map<RmFormQa1Tes>(formQa1Header.Tes);
            formTes.Fqa1tesPkRefNo = formQa1Header.Tes.PkRefNo;
            formTes.Fqa1tesFqa1hPkRefNo = formQa1Header.PkRefNo;
            await _repoUnit.FormQa1Repository.SaveTES(formTes);

            var formWcq = _mapper.Map<RmFormQa1Wcq>(formQa1Header.Wcq);
            formWcq.Fqa1wcqPkRefNo = formQa1Header.Wcq.PkRefNo;
            formWcq.Fqa1wcqFqa1hPkRefNo = formQa1Header.PkRefNo;
            await _repoUnit.FormQa1Repository.SaveWCQ(formWcq);

            var formWe = _mapper.Map<RmFormQa1We>(formQa1Header.We);
            formWe.Fqa1wPkRefNo = formQa1Header.We.PkRefNo;
            formWe.Fqa1wFqa1hPkRefNo = formQa1Header.PkRefNo;
            await _repoUnit.FormQa1Repository.SaveWE(formWe);

            await _repoUnit.CommitAsync();
            return 1;
        }

        public RmFormQa1Hdr UpdateStatus(RmFormQa1Hdr form)
        {
            if (form.Fqa1hPkRefNo > 0)
            {
                var existsObj = _repoUnit.FormQa1Repository._context.RmFormQa1Hdr.Where(x => x.Fqa1hPkRefNo == form.Fqa1hPkRefNo).Select(x => new { Status = x.Fqa1hStatus, Log = x.Fqa1hAuditLog }).FirstOrDefault();
                if (existsObj != null)
                {
                    form.Fqa1hAuditLog = existsObj.Log;
                    form.Fqa1hStatus = existsObj.Status;
                }

            }


            if (form.Fqa1hSubmitSts && (string.IsNullOrEmpty(form.Fqa1hStatus) || form.Fqa1hStatus == Common.StatusList.FormQA1Saved || form.Fqa1hStatus == Common.StatusList.FormQA1Rejected))
            {
                form.Fqa1hUseridExec = _security.UserID;
                form.Fqa1hUsernameExec = _security.UserName;
                form.Fqa1hDtExec = DateTime.Today;
                form.Fqa1hStatus = Common.StatusList.FormQA1Submitted;
                form.Fqa1hAuditLog = Utility.ProcessLog(form.Fqa1hAuditLog, "Submitted", "Submitted", form.Fqa1hUsernameExec, string.Empty, form.Fqa1hDtExec, _security.UserName);
                processService.SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = _security.UserName,
                    RmNotGroup = GroupNames.OperationsExecutive,
                    RmNotMessage = "Executed By:" + form.Fqa1hUsernameExec + " - Form QA1 (" + form.Fqa1hPkRefNo + ")",
                    RmNotOn = DateTime.Now,
                    RmNotUrl = "/FormQA1/EditFormQa1?id=" + form.Fqa1hPkRefNo.ToString() + "&view=1",
                    RmNotUserId = "",
                    RmNotViewed = ""
                }, true);
            }
            else if (string.IsNullOrEmpty(form.Fqa1hStatus) || form.Fqa1hStatus == "Initialize")
                form.Fqa1hStatus = Common.StatusList.FormQA1Saved;

            return form;
        }


        public async Task<int> DeleteFormQA1(int id)
        {
            try
            {
                return _repoUnit.FormQa1Repository.DeleteFormQA1(id);
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        public async Task<int> SaveImage(List<FormQa1AttachmentDTO> files)
        {
            int rowsAffected;
            try
            {
                var domainModelFormQA1 = new List<RmFormQa1Image>();

                foreach (var list in files)
                {
                    var dt = _mapper.Map<RmFormQa1Image>(list);
                    dt.Fqa1iPkRefNo = list.PkRefNo;
                    dt.Fqa1iFqa1TesPkRefNo = Convert.ToInt32(list.Fqa1TesPkRefNo);

                    domainModelFormQA1.Add(dt);

                }
                _repoUnit.FormQa1Repository.SaveImage(domainModelFormQA1);
                //_repoUnit.FormQa1Repository.SaveChanges();

                //_repoUnit.FormQa1Repository.UpdateTesImage(domainModelFormQA1);
                //rowsAffected = _repoUnit.FormQa1Repository.SaveChanges();
                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }


        public async Task<List<FormQa1AttachmentDTO>> GetImages(int tesPkRefNo, int row = 0)
        {
            var attachments = new List<FormQa1AttachmentDTO>();
            var files = new List<RmFormQa1Image>();
            if (row == 0)
                files = await _repoUnit.FormQa1Repository.GetImages(tesPkRefNo);
            else
                files = await _repoUnit.FormQa1Repository.GetImages(tesPkRefNo, row);

            foreach (var list in files)
            {
                attachments.Add(_mapper.Map<FormQa1AttachmentDTO>(list));
            }
            return attachments;
        }


        public async Task<int> DeActivateImage(int imageId)
        {
            int rowsAffected;
            try
            {
                var domainModel = await _repoUnit.FormQa1Repository.GetImageById(imageId);
                domainModel.Fqa1iActiveYn = false;
                _repoUnit.FormQa1Repository.UpdateImage(domainModel);

                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<FormQa1TesDTO> GetTes(int tesPkRefNo)
        {
            var ret = await _repoUnit.FormQa1Repository.GetTes(tesPkRefNo);
            return _mapper.Map<FormQa1TesDTO>(ret);

        }
    }

}
