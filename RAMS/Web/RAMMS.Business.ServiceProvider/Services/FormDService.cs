using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.RequestBO;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RAMMS.Common;
using Serilog;
using AutoMapper;
using RAMMS.Domain.Models;
using RAMMS.DTO.Wrappers;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Globalization;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class FormDService : IFormDService
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        private readonly IProcessService processService;
        public FormDService(IRepositoryUnit repoUnit, IMapper mapper, ISecurity security, IProcessService process)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security;
            processService = process;
        }

        public async Task<int> DeActivateFormDAsync(int formNo)
        {
            int rowsAffected;
            try
            {
                var domainModelFormD = await _repoUnit.FormDRepository.GetByIdAsync(formNo);
                domainModelFormD.FdhActiveYn = false;
                _repoUnit.FormDRepository.Update(domainModelFormD);

                var formDLabour = await _repoUnit.FormDLabourRepository.GetAllLabourById(domainModelFormD.FdhPkRefNo);
                foreach (var labour in formDLabour)
                {
                    labour.FdldActiveYn = false;
                    _repoUnit.FormDLabourRepository.Update(labour);
                }

                var formDMaterial = await _repoUnit.FormDMaterialRepository.GetAllMaterialById(domainModelFormD.FdhPkRefNo);
                foreach (var material in formDMaterial)
                {
                    material.FdmdActiveYn = false;
                    _repoUnit.FormDMaterialRepository.Update(material);
                }

                var formDEquipment = await _repoUnit.FormDEquipmentRepository.GetAllEquipmentById(domainModelFormD.FdhPkRefNo);
                foreach (var equip in formDEquipment)
                {
                    equip.FdedActiveYn = false;
                    _repoUnit.FormDEquipmentRepository.Update(equip);
                }

                var formDDtl = await _repoUnit.FormDDtlRepository.GetAllDtlById(domainModelFormD.FdhPkRefNo);
                foreach (var dtl in formDDtl)
                {
                    dtl.FddActiveYn = false;
                    _repoUnit.FormDDtlRepository.Update(dtl);
                }

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> DeActivateFormDLabourAsync(int formNo)
        {
            int rowsAffected;
            try
            {
                var domainModelFormD = await _repoUnit.FormDLabourRepository.GetByIdAsync(formNo);
                domainModelFormD.FdldActiveYn = false;
                _repoUnit.FormDLabourRepository.Update(domainModelFormD);

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> DeActivateFormMaterialDAsync(int formNo)
        {
            int rowsAffected;
            try
            {
                var domainModelFormD = await _repoUnit.FormDMaterialRepository.GetByIdAsync(formNo);
                domainModelFormD.FdmdActiveYn = false;
                _repoUnit.FormDMaterialRepository.Update(domainModelFormD);

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> DeActivateFormDEquipmentAsync(int formNo)
        {
            int rowsAffected;
            try
            {
                var domainModelFormD = await _repoUnit.FormDEquipmentRepository.GetByIdAsync(formNo);
                domainModelFormD.FdedActiveYn = false;
                _repoUnit.FormDEquipmentRepository.Update(domainModelFormD);

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> DeActivateFormDDetailAsync(int formNo)
        {
            int rowsAffected;
            try
            {
                var domainModelFormD = await _repoUnit.FormDDtlRepository.GetByIdAsync(formNo);
                domainModelFormD.FddActiveYn = false;
                _repoUnit.FormDDtlRepository.Update(domainModelFormD);

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }
        public RmFormDHdr UpdateStatus(RmFormDHdr form)
        {
            if (form.FdhPkRefNo > 0)
            {
                var existsObj = _repoUnit.FormDRepository._context.RmFormDHdr.Where(x => x.FdhPkRefNo == form.FdhPkRefNo).Select(x => new { Status = x.FdhStatus, Log = x.FdhAuditLog }).FirstOrDefault();
                if (existsObj != null)
                {
                    form.FdhAuditLog = existsObj.Log;
                    form.FdhStatus = existsObj.Status;
                }

            }
            if (form.FdhStatus == Common.StatusList.Executive)
                form.FdhStatus = Common.StatusList.HeadMaintenance;
            else if (form.FdhStatus == Common.StatusList.HeadMaintenance)
                form.FdhStatus = Common.StatusList.VerifiedJKRSSuperior;
            else if (form.FdhStatus == Common.StatusList.VerifiedJKRSSuperior)
                form.FdhStatus = Common.StatusList.ProcessedJKRSSuperior;
            else if (form.FdhStatus == Common.StatusList.ProcessedJKRSSuperior)
                form.FdhStatus = Common.StatusList.AgreedJKRSSuperior;
            else if (form.FdhStatus == Common.StatusList.AgreedJKRSSuperior)
                form.FdhStatus = Common.StatusList.Completed;
            else if (form.FdhSubmitSts && (string.IsNullOrEmpty(form.FdhStatus) || form.FdhStatus == Common.StatusList.Supervisor))
            {
                form.FdhStatus = Common.StatusList.Executive;
                form.FdhAuditLog = Utility.ProcessLog(form.FdhAuditLog, "Recorded By", "Approved", form.FdhUsernamePrp, string.Empty, form.FdhDtPrp, _security.UserName);
                processService.SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = _security.UserName,
                    RmNotGroup = GroupNames.OperationsExecutive,
                    RmNotMessage = "Recorded By:" + form.FdhUsernamePrp + " - Form D (" + form.FdhRefId + ")",
                    RmNotOn = DateTime.Now,
                    RmNotUrl = "/ERT/EditFormD?id=" + form.FdhPkRefNo.ToString() + "&view=1",
                    RmNotUserId = "",
                    RmNotViewed = ""
                }, true);
            }
            else if (string.IsNullOrEmpty(form.FdhStatus))
                form.FdhStatus = Common.StatusList.Supervisor;

            return form;
        }
        public async Task<int> SaveFormDAsync(FormDHeaderRequestDTO FormDHeaderBO)
        {
            FormDHeaderRequestDTO formDRequest;
            try
            {
                var domainModelFormD = _mapper.Map<RmFormDHdr>(FormDHeaderBO);
                domainModelFormD = UpdateStatus(domainModelFormD);
                var entity = _repoUnit.FormDRepository.CreateReturnEntity(domainModelFormD);
                formDRequest = _mapper.Map<FormDHeaderRequestDTO>(entity);

                return formDRequest.No;

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public async Task<int> SaveFormDLabourAsync(FormDLabourDetailsRequestDTO FormDLabourBO)
        {

            FormDLabourDetailsRequestDTO formDRequest;
            try
            {
                var domainModelFormD = _mapper.Map<RmFormDLabourDtl>(FormDLabourBO);

                var entity = _repoUnit.FormDLabourRepository.CreateReturnEntity(domainModelFormD);
                formDRequest = _mapper.Map<FormDLabourDetailsRequestDTO>(entity);


            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return int.Parse(formDRequest.No.ToString());
        }

        public async Task<int> SaveFormDMaterialAsync(FormDMaterialDetailsRequestDTO FormDLabourBO)
        {

            FormDMaterialDetailsRequestDTO formDRequest;
            try
            {
                var domainModelFormD = _mapper.Map<RmFormDMaterialDtl>(FormDLabourBO);

                var entity = _repoUnit.FormDMaterialRepository.CreateReturnEntity(domainModelFormD);
                formDRequest = _mapper.Map<FormDMaterialDetailsRequestDTO>(entity);


            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return int.Parse(formDRequest.No.ToString());
        }

        public async Task<int> SaveFormDEquipmentAsync(FormDEquipRequestDTO FormDLabourBO)
        {

            FormDEquipRequestDTO formDRequest;
            try
            {
                var domainModelFormD = _mapper.Map<RmFormDEquipDtl>(FormDLabourBO);

                var entity = _repoUnit.FormDEquipmentRepository.CreateReturnEntity(domainModelFormD);
                formDRequest = _mapper.Map<FormDEquipRequestDTO>(entity);


            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return int.Parse(formDRequest.No.ToString());
        }

        public async Task<int> SaveFormDDetailAsync(FormDDetailsRequestDTO FormDLabourBO)
        {

            FormDDetailsRequestDTO formDRequest;
            try
            {
                var domainModelFormD = _mapper.Map<RmFormDDtl>(FormDLabourBO);

                if (FormDLabourBO.SiteRef_multiSelect?.Count > 0)
                {
                    domainModelFormD.FddSiteRef = string.Join<string>(",", FormDLabourBO.SiteRef_multiSelect);
                }

                var entity = _repoUnit.FormDDtlRepository.CreateReturnEntity(domainModelFormD);
                RmFormDHdr hdrData = await _repoUnit.FormDDtlRepository.getFromDhdr(Convert.ToInt32(entity.FddFdhPkRefNo));
                int dayOfWeek = getSchldDayOfWeek(hdrData.FdhDay);
                (int detId, int wkDtlId, int wkDtlPlanned, bool alreadyExists) isExist = await _repoUnit.FormDDtlRepository.CheckAlreadyExistsS1Det(entity.FddRoadCode, entity.FddActCode.ToString(), Convert.ToInt32(entity.FddFrmCh), entity.FddFrmChDeci.ToString(), Convert.ToInt32(entity.FddToCh), entity.FddToChDeci.ToString(), Convert.ToInt32(hdrData.FdhCrewUnit), Convert.ToInt32(hdrData.FdhWeekNo), dayOfWeek);
                if (isExist.detId != 0)
                {
                    if (isExist.alreadyExists)
                    {
                        RmFormS1WkDtl s1WkDtl = new RmFormS1WkDtl();
                        var date = GetDateString(hdrData.FdhYear.ToString(), hdrData.FdhWeekNo.ToString(), hdrData.FdhDay);
                        s1WkDtl.FsiwdSchldDate = Convert.ToDateTime(date);
                        s1WkDtl.FsiwdSchldDayOfWeek = getSchldDayOfWeek(hdrData.FdhDay);
                        s1WkDtl.FsiwdFsidPkRefNo = isExist.detId;
                        s1WkDtl.FsiwdPkRefNo = isExist.wkDtlId;
                        s1WkDtl.FsiwdPlanned = isExist.wkDtlPlanned;
                        if (entity.FddWorkSts == "Completed" && isExist.wkDtlPlanned == 0)
                        {
                            s1WkDtl.FsiwdActual = 2;
                        }
                        if (entity.FddWorkSts == "Completed" && isExist.wkDtlPlanned == 1)
                        {
                            s1WkDtl.FsiwdActual = 4;
                        }
                        if (entity.FddWorkSts == "Not Completed")
                        {
                            s1WkDtl.FsiwdActual = 3;
                        }
                        else if (entity.FddWorkSts == "Rescheduled")
                        {
                            s1WkDtl.FsiwdActual = 5;
                        }
                        _repoUnit.formS1Repository.UpdateWkdtl(s1WkDtl);
                    }
                    else
                    {
                        RmFormS1WkDtl s1WkDtl = new RmFormS1WkDtl();
                        var date = GetDateString(hdrData.FdhYear.ToString(), hdrData.FdhWeekNo.ToString(), hdrData.FdhDay);
                        s1WkDtl.FsiwdSchldDate = Convert.ToDateTime(date);
                        s1WkDtl.FsiwdSchldDayOfWeek = getSchldDayOfWeek(hdrData.FdhDay);
                        s1WkDtl.FsiwdFsidPkRefNo = isExist.detId;
                        if (entity.FddWorkSts == "Completed" && isExist.wkDtlPlanned == 0)
                        {
                            s1WkDtl.FsiwdActual = 2;
                        }
                        if (entity.FddWorkSts == "Completed" && isExist.wkDtlPlanned == 1)
                        {
                            s1WkDtl.FsiwdActual = 4;
                        }

                        if (entity.FddWorkSts == "Not Completed")
                        {
                            s1WkDtl.FsiwdActual = 3;
                        }
                        else if (entity.FddWorkSts == "Rescheduled")
                        {
                            s1WkDtl.FsiwdActual = 5;
                        }
                        _repoUnit.formS1Repository.CreateWkdtl(s1WkDtl);
                    }
                }

                formDRequest = _mapper.Map<FormDDetailsRequestDTO>(entity);


                var formXRefNo = formDRequest.FormXPKRefNo;
                var accUccImageList = await _repoUnit.FormXRepository.GetAccUccImagelist(Convert.ToInt32(formXRefNo));
                if (accUccImageList.Count() > 0)
                {
                    accUccImageList = accUccImageList.Select(x => { x.FauFddPkRefNo = formDRequest.No; x.FauFxhPkRefNo = null; x.FauPkRefNo = 0; return x; }).ToList();
                    _repoUnit.FormXRepository.SaveAccUccImage(accUccImageList);
                    await _repoUnit.CommitAsync();
                }
                var warImageList = await _repoUnit.FormXRepository.GetWarImagelist(Convert.ToInt32(formXRefNo));
                if (warImageList.Count() > 0)
                {
                    warImageList = warImageList.Select(x => { x.FwarFddPkRefNo = formDRequest.No; x.FwarFxhPkRefNo = null; x.FwarPkRefNo = 0; return x; }).ToList();
                    _repoUnit.FormXRepository.SaveWarImage(warImageList);
                    await _repoUnit.CommitAsync();
                }

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            await _repoUnit.CommitAsync();
            return int.Parse(formDRequest.No.ToString());
        }

        public async Task<FormDHeaderResponseDTO> SaveHeaderwithResponse(FormDHeaderRequestDTO headerReq)
        {
            FormDHeaderResponseDTO formD;

            int refCheckCount = await _repoUnit.FormDRepository.CheckwithRef(headerReq);
            if (refCheckCount != 0)
            {
                headerReq.No = refCheckCount;
                var currentFormD = await _repoUnit.FormDRepository.GetFormWithDetailsByNoAsync(Convert.ToInt32(headerReq.No)).ConfigureAwait(false);
                formD = _mapper.Map<FormDHeaderResponseDTO>(currentFormD);
                return formD;

            }
            else
            {
                var domainModelFormD = _mapper.Map<RmFormDHdr>(headerReq);

                _repoUnit.FormDRepository.Create(domainModelFormD);

                await _repoUnit.CommitAsync();

                return null;
            }
        }

        public async Task<List<string>> GetSectionByRMU(string rmu)
        {
            var data = await _repoUnit.FormDRepository.GetSectionByRMU(rmu).ConfigureAwait(false);

            return data;

        }

        public async Task<PagingResult<FormDHeaderResponseDTO>> GetFilteredFormDGrid(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions)
        {
            PagingResult<FormDHeaderResponseDTO> result = new PagingResult<FormDHeaderResponseDTO>();

            List<FormDHeaderResponseDTO> formDList = new List<FormDHeaderResponseDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormDRepository.GetFilteredRecordList(filterOptions);

                result.TotalRecords = await _repoUnit.FormDRepository.GetFilteredRecordCount(filterOptions).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    var _ = _mapper.Map<FormDHeaderResponseDTO>(listData);
                    _.ProcessStatus = listData.FdhStatus;

                    formDList.Add(_);
                }

                result.PageResult = formDList;

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

        public async Task<FormDHeaderRequestDTO> GetFormDWithDetailsByNoAsync(int formNo)
        {
            FormDHeaderRequestDTO formD;
            try
            {
                var currentFormD = await _repoUnit.FormDRepository.GetFormWithDetailsByNoAsync(formNo).ConfigureAwait(false);
                formD = _mapper.Map<FormDHeaderRequestDTO>(currentFormD);

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return formD;
        }

        public async Task<int> UpdateFormDAsync(FormDHeaderRequestDTO formDDtlDTO)
        {
            int rowsAffected;
            try
            {
                var domainModelformD = _mapper.Map<RmFormDHdr>(formDDtlDTO);
                domainModelformD.FdhActiveYn = true;
                domainModelformD = UpdateStatus(domainModelformD);
                _repoUnit.FormDRepository.Update(domainModelformD);

                if (domainModelformD.FdhSubmitSts)
                {
                    var formDLabour = await _repoUnit.FormDLabourRepository.GetAllLabourById(domainModelformD.FdhPkRefNo);
                    foreach (var labour in formDLabour)
                    {
                        labour.FdldSubmitSts = true;
                        _repoUnit.FormDLabourRepository.Update(labour);
                    }

                    var formDMaterial = await _repoUnit.FormDMaterialRepository.GetAllMaterialById(domainModelformD.FdhPkRefNo);
                    foreach (var material in formDMaterial)
                    {
                        material.FdmdSubmitSts = true;
                        _repoUnit.FormDMaterialRepository.Update(material);
                    }

                    var formDEquipment = await _repoUnit.FormDEquipmentRepository.GetAllEquipmentById(domainModelformD.FdhPkRefNo);
                    foreach (var equip in formDEquipment)
                    {
                        equip.FdedSubmitSts = true;
                        _repoUnit.FormDEquipmentRepository.Update(equip);
                    }

                    var formDDtl = await _repoUnit.FormDDtlRepository.GetAllDtlById(domainModelformD.FdhPkRefNo);
                    foreach (var dtl in formDDtl)
                    {
                        dtl.FddSubmitSts = true;
                        _repoUnit.FormDDtlRepository.Update(dtl);
                    }
                }

                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> SaveFormDWarImage(List<WarImageDtlRequestDTO> warImage)
        {
            int rowsAffected;
            try
            {
                var domainModelFormD = new List<RmWarImageDtl>();

                foreach (var list in warImage)
                {
                    domainModelFormD.Add(_mapper.Map<RmWarImageDtl>(list));
                }
                _repoUnit.FormDRepository.SaveWarImage(domainModelFormD);
                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> SaveFormDAccUccDtl(List<AccUccImageDtlRequestDTO> accUccImage)
        {
            int rowsAffected;
            try
            {
                var domainModelFormD = new List<RmAccUcuImageDtl>();
                foreach (var list in accUccImage)
                {
                    domainModelFormD.Add(_mapper.Map<RmAccUcuImageDtl>(list));
                }

                _repoUnit.FormDRepository.SaveAccUccImage(domainModelFormD);

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> DeActivateWarImage(int warId)
        {
            int rowsAffected;
            try
            {
                var domainModelformD = await _repoUnit.FormDRepository.GetWarImageByIdAsync(warId);
                domainModelformD.FwarActiveYn = false;
                _repoUnit.FormDRepository.UpdateWar(domainModelformD);

                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> DeActivateAccUCc(int accUccId)
        {
            int rowsAffected;
            try
            {
                var domainModelformD = await _repoUnit.FormDRepository.GetAccUccImageById(accUccId);
                domainModelformD.FauActiveYn = false;
                _repoUnit.FormDRepository.UpdateAccUcu(domainModelformD);

                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<List<WarImageDtlResponseDTO>> GetWarImageList(int formDId)
        {
            List<WarImageDtlResponseDTO> warImages = new List<WarImageDtlResponseDTO>();
            try
            {
                var getList = await _repoUnit.FormDRepository.GetWarImagelist(formDId);
                foreach (var listItem in getList)
                {
                    warImages.Add(_mapper.Map<WarImageDtlResponseDTO>(listItem));
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return warImages;
        }

        public async Task<List<AccUccImageDtlResponseDTO>> GetAccUccImageList(int formDId)
        {
            List<AccUccImageDtlResponseDTO> warImages = new List<AccUccImageDtlResponseDTO>();
            try
            {
                var getList = await _repoUnit.FormDRepository.GetAccUccImagelist(formDId);
                foreach (var listItem in getList)
                {
                    warImages.Add(_mapper.Map<AccUccImageDtlResponseDTO>(listItem));
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return warImages;
        }

        public async Task<AccUccImageDtlResponseDTO> GetAccUccImage(int formDId)
        {
            AccUccImageDtlResponseDTO warImages = new AccUccImageDtlResponseDTO();
            try
            {
                var getList = await _repoUnit.FormDRepository.GetAccUccImageById(formDId);
                warImages = _mapper.Map<AccUccImageDtlResponseDTO>(getList);
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return warImages;
        }

        public async Task<int> LastInsertedWARSRNO(int hederId, string type)
        {

            int imageCt = await _repoUnit.FormDRepository.GetWARId(hederId, type);
            return imageCt;
        }
        public async Task<int> LastInsertedUCUSRNO(int hederId, string type)
        {

            int imageCt = await _repoUnit.FormDRepository.GetUCUId(hederId, type);
            return imageCt;
        }

        public async Task<PagingResult<FormDEquipDetailsResponseDTO>> GetEquipmentFormDGrid(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id)
        {
            PagingResult<FormDEquipDetailsResponseDTO> result = new PagingResult<FormDEquipDetailsResponseDTO>();

            List<FormDEquipDetailsResponseDTO> formDEquipList = new List<FormDEquipDetailsResponseDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormDEquipmentRepository.GetFilteredRecordList(filterOptions, id);

                result.TotalRecords = await _repoUnit.FormDEquipmentRepository.GetFilteredRecordCount(filterOptions, id).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    formDEquipList.Add(_mapper.Map<FormDEquipDetailsResponseDTO>(listData));
                }

                result.PageResult = formDEquipList;

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

        public async Task<PagingResult<FormDMaterialDetailsResponseDTO>> GetMaterialFormDGrid(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id)
        {
            PagingResult<FormDMaterialDetailsResponseDTO> result = new PagingResult<FormDMaterialDetailsResponseDTO>();

            List<FormDMaterialDetailsResponseDTO> formDMaterialList = new List<FormDMaterialDetailsResponseDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormDMaterialRepository.GetFilteredRecordList(filterOptions, id);

                result.TotalRecords = await _repoUnit.FormDMaterialRepository.GetFilteredRecordCount(filterOptions, id).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    formDMaterialList.Add(_mapper.Map<FormDMaterialDetailsResponseDTO>(listData));
                }

                result.PageResult = formDMaterialList;

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

        public async Task<PagingResult<FormDLabourDetailsResponseDTO>> GetLabourFormDGrid(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id)
        {
            PagingResult<FormDLabourDetailsResponseDTO> result = new PagingResult<FormDLabourDetailsResponseDTO>();

            List<FormDLabourDetailsResponseDTO> formDLabourList = new List<FormDLabourDetailsResponseDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormDLabourRepository.GetFilteredRecordList(filterOptions, id);

                result.TotalRecords = await _repoUnit.FormDLabourRepository.GetFilteredRecordCount(filterOptions, id).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    formDLabourList.Add(_mapper.Map<FormDLabourDetailsResponseDTO>(listData));
                }

                result.PageResult = formDLabourList;

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

        public async Task<PagingResult<FormDDetailsResponseDTO>> GetFormDDetailsGrid(FilteredPagingDefinition<FormDSearchGridDTO> filterOptions, string id)
        {
            PagingResult<FormDDetailsResponseDTO> result = new PagingResult<FormDDetailsResponseDTO>();

            List<FormDDetailsResponseDTO> formDDtlList = new List<FormDDetailsResponseDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormDDtlRepository.GetFilteredRecordList(filterOptions, id);

                result.TotalRecords = await _repoUnit.FormDDtlRepository.GetFilteredRecordCount(filterOptions, id).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    formDDtlList.Add(_mapper.Map<FormDDetailsResponseDTO>(listData));
                }

                result.PageResult = formDDtlList;

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

        public async Task<int> UpdateFormDLabourAsync(FormDLabourDetailsRequestDTO FormDLabourBO)
        {
            int rowsAffected;
            try
            {
                var domainModelFormD = _mapper.Map<RmFormDLabourDtl>(FormDLabourBO);
                _repoUnit.FormDLabourRepository.Update(domainModelFormD);

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> UpdateFormDMaterialAsync(FormDMaterialDetailsRequestDTO FormDLabourBO)
        {
            int rowsAffected;
            try
            {
                var domainModelFormD = _mapper.Map<RmFormDMaterialDtl>(FormDLabourBO);
                _repoUnit.FormDMaterialRepository.Update(domainModelFormD);

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> UpdateFormDEquipmentAsync(FormDEquipRequestDTO FormDLabourBO)
        {
            int rowsAffected;
            try
            {
                var domainModelFormD = _mapper.Map<RmFormDEquipDtl>(FormDLabourBO);
                _repoUnit.FormDEquipmentRepository.Update(domainModelFormD);

                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }
        public int getSchldDayOfWeek(string day)
        {
            int dayNo = -1;
            switch (day.ToLower())
            {
                case "monday":
                    dayNo = 1;
                    break;
                case "tuesday":
                    dayNo = 2;
                    break;
                case "wednesday":
                    dayNo = 3;
                    break;
                case "thursday":
                    dayNo = 4;
                    break;
                case "friday":
                    dayNo = 5;
                    break;
                case "saturday":
                    dayNo = 6;
                    break;
                case "sunday":
                    dayNo = 0;
                    break;
            }

            return dayNo;

        }
        public async Task<int> UpdateFormDDetailAsync(FormDDetailsRequestDTO FormDLabourBO)
        {
            int rowsAffected;
            try
            {
                var domainModelFormD = _mapper.Map<RmFormDDtl>(FormDLabourBO);
                if (FormDLabourBO.SiteRef_multiSelect?.Count > 0)
                {
                    domainModelFormD.FddSiteRef = string.Join<string>(",", FormDLabourBO.SiteRef_multiSelect);
                }
                _repoUnit.FormDDtlRepository.Update(domainModelFormD);
                //(int crewunit, int weekno) hdrdata = await _repoUnit.FormDDtlRepository.getFromDhdr(Convert.ToInt32(domainmodelformD.FddFdhPkRefNo));
                RmFormDHdr hdrData = await _repoUnit.FormDDtlRepository.getFromDhdr(Convert.ToInt32(domainModelFormD.FddFdhPkRefNo));
                int dayofweek = getSchldDayOfWeek(hdrData.FdhDay);
                (int detId, int wkDtlId, int wkDtlPlanned, bool alreadyexists) isExist = await _repoUnit.FormDDtlRepository.CheckAlreadyExistsS1Det(domainModelFormD.FddRoadCode, domainModelFormD.FddActCode.ToString(), Convert.ToInt32(domainModelFormD.FddFrmCh), domainModelFormD.FddFrmChDeci.ToString(), Convert.ToInt32(domainModelFormD.FddToCh), domainModelFormD.FddToChDeci.ToString(), Convert.ToInt32(hdrData.FdhCrewUnit), Convert.ToInt32(hdrData.FdhWeekNo), dayofweek);
                if (isExist.detId != 0)
                {
                    if (isExist.alreadyexists)
                    {
                        RmFormS1WkDtl s1WkDtl = new RmFormS1WkDtl();
                        var date = GetDateString(hdrData.FdhYear.ToString(), hdrData.FdhWeekNo.ToString(), hdrData.FdhDay);
                        s1WkDtl.FsiwdSchldDate = Convert.ToDateTime(date);
                        s1WkDtl.FsiwdSchldDayOfWeek = getSchldDayOfWeek(hdrData.FdhDay);
                        s1WkDtl.FsiwdFsidPkRefNo = isExist.detId;
                        s1WkDtl.FsiwdPkRefNo = isExist.wkDtlId;
                        s1WkDtl.FsiwdPlanned = isExist.wkDtlPlanned;
                        if (domainModelFormD.FddWorkSts == "Completed" && isExist.wkDtlPlanned == 0)
                        {
                            s1WkDtl.FsiwdActual = 2;
                        }
                        if (domainModelFormD.FddWorkSts == "Completed" && isExist.wkDtlPlanned == 1)
                        {
                            s1WkDtl.FsiwdActual = 4;
                        }
                        if (domainModelFormD.FddWorkSts == "Not Completed")
                        {
                            s1WkDtl.FsiwdActual = 3;
                        }
                        else if (domainModelFormD.FddWorkSts == "Rescheduled")
                        {
                            s1WkDtl.FsiwdActual = 5;
                        }

                        _repoUnit.formS1Repository.UpdateWkdtl(s1WkDtl);
                    }
                    else
                    {
                        RmFormS1WkDtl s1WkDtl = new RmFormS1WkDtl();
                        var date = GetDateString(hdrData.FdhYear.ToString(), hdrData.FdhWeekNo.ToString(), hdrData.FdhDay);
                        s1WkDtl.FsiwdSchldDate = Convert.ToDateTime(date);
                        s1WkDtl.FsiwdSchldDayOfWeek = getSchldDayOfWeek(hdrData.FdhDay);
                        s1WkDtl.FsiwdFsidPkRefNo = isExist.detId;
                        if (domainModelFormD.FddWorkSts == "Completed" && isExist.wkDtlPlanned == 0)
                        {
                            s1WkDtl.FsiwdActual = 2;
                        }
                        if (domainModelFormD.FddWorkSts == "Completed" && isExist.wkDtlPlanned == 1)
                        {
                            s1WkDtl.FsiwdActual = 4;
                        }
                        if (domainModelFormD.FddWorkSts == "Not Completed")
                        {
                            s1WkDtl.FsiwdActual = 3;
                        }
                        else if (domainModelFormD.FddWorkSts == "Rescheduled")
                        {
                            s1WkDtl.FsiwdActual = 5;
                        }
                        _repoUnit.formS1Repository.CreateWkdtl(s1WkDtl);

                    }
                }


                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public string GetDateString(string year, string weekNo, string weekDay)
        {
            var obj = GetDateByWeekNo_WeeDay(year, weekNo, weekDay);
            return obj.ToString();
        }

        public string GetDateByWeekNo_WeeDay(string year, string weekNo, string weekDay)
        {
            var obj = FirstDateOfWeek(Convert.ToInt32(year), Convert.ToInt32(weekNo));
            DateTime retVal;

            switch (weekDay.ToLower())
            {
                case "monday":
                    retVal = obj.AddDays(0);
                    break;
                case "tuesday":
                    retVal = obj.AddDays(1);
                    break;
                case "wednesday":
                    retVal = obj.AddDays(2);
                    break;
                case "thursday":
                    retVal = obj.AddDays(3);
                    break;
                case "friday":
                    retVal = obj.AddDays(4);
                    break;
                case "saturday":
                    retVal = obj.AddDays(5);
                    break;
                case "sunday":
                    //retVal = obj;
                    retVal = obj.AddDays(6);
                    break;
                default:
                    retVal = obj;
                    break;
            }

            return retVal.ToString();
        }
        static DateTime FirstDateOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;

            DateTime firstMonday = jan1.AddDays(daysOffset);

            return firstMonday.AddDays(weekOfYear * 7);
        }

        public async Task<FormDLabourDetailsRequestDTO> GetFormDLabourDetailsByNoAsync(int formNo)
        {
            FormDLabourDetailsRequestDTO formD;
            try
            {
                var currentFormD = await _repoUnit.FormDLabourRepository.GetFormWithDetailsByNoAsync(formNo).ConfigureAwait(false);
                formD = _mapper.Map<FormDLabourDetailsRequestDTO>(currentFormD);

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return formD;
        }

        public async Task<FormDMaterialDetailsRequestDTO> GetFormDMaterialDetailsByNoAsync(int formNo)
        {
            FormDMaterialDetailsRequestDTO formD;
            try
            {
                var currentFormD = await _repoUnit.FormDMaterialRepository.GetFormWithDetailsByNoAsync(formNo).ConfigureAwait(false);
                formD = _mapper.Map<FormDMaterialDetailsRequestDTO>(currentFormD);

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return formD;
        }

        public async Task<FormDEquipRequestDTO> GetFormDEquipmentDetailsByNoAsync(int formNo)
        {
            FormDEquipRequestDTO formD;
            try
            {
                var currentFormD = await _repoUnit.FormDEquipmentRepository.GetFormWithDetailsByNoAsync(formNo).ConfigureAwait(false);
                formD = _mapper.Map<FormDEquipRequestDTO>(currentFormD);

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return formD;
        }

        public async Task<FormDDetailsRequestDTO> GetFormDDetailsByNoAsync(int formNo)
        {
            FormDDetailsRequestDTO formD;
            try
            {
                var currentFormD = await _repoUnit.FormDDtlRepository.GetFormWithDetailsByNoAsync(formNo).ConfigureAwait(false);
                formD = _mapper.Map<FormDDetailsRequestDTO>(currentFormD);

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return formD;
        }

        public async Task<IEnumerable<SelectListItem>> GetRoadCodeList()
        {
            try
            {
                var codes = await _repoUnit.FormDRepository.GetRoadCodes();

                return codes.OrderBy(s => s.RdmPkRefNo).Select(s => new SelectListItem
                {
                    Value = s.RdmRdCode.ToString(),
                    Text = s.RdmRdCode + "-" + s.RdmRdName.ToString()
                }).ToArray();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetRoadCodeBySectionCode(string secCode)
        {
            try
            {
                var codes = await _repoUnit.FormDRepository.GetRoadCodeBySectionCode(secCode);

                return codes.OrderBy(s => s.RdmPkRefNo).Select(s => new SelectListItem
                {
                    Value = s.RdmRdCode.ToString(),
                    Text = s.RdmRdCode + "-" + s.RdmRdName.ToString()
                }).ToArray();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }


        public async Task<IEnumerable<SelectListItem>> GetDivisions()
        {
            try
            {
                var codes = await _repoUnit.FormDRepository.GetDivisions();

                return codes.OrderBy(s => s.DdlPkRefNo).Select(s => new SelectListItem
                {
                    Value = s.DdlTypeCode.ToString(),
                    Text = s.DdlTypeCode + "-" + s.DdlTypeValue.ToString()
                }).ToArray();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetActivityMainTask()
        {

            try
            {
                var codes = await _repoUnit.FormDRepository.GetActivityMainTask();

                return codes.OrderBy(s => s.DdlPkRefNo).Select(s => new SelectListItem
                {
                    Value = s.DdlTypeCode.ToString(),
                    Text = s.DdlTypeCode + "-" + s.DdlTypeValue.ToString()
                }).ToArray();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetActivitySubTask()
        {

            try
            {
                var codes = await _repoUnit.FormDRepository.GetActivitySubTask();

                return codes.OrderBy(s => s.DdlPkRefNo).Select(s => new SelectListItem
                {
                    Value = s.DdlTypeCode.ToString(),
                    Text = s.DdlTypeCode + "-" + s.DdlTypeValue.ToString()
                }).ToArray();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetSectionCode()
        {
            try
            {
                var codes = await _repoUnit.FormDRepository.GetSectionCode();

                return codes.OrderBy(s => s.DdlPkRefNo).Select(s => new SelectListItem
                {
                    Value = s.DdlTypeCode.ToString(),
                    Text = s.DdlTypeCode + "-" + s.DdlTypeValue.ToString()
                }).ToArray();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetLabourCode()
        {
            try
            {
                var codes = await _repoUnit.FormDRepository.GetLabourCode();

                var result =  codes.OrderBy(s => s.DdlPkRefNo).Select(s => new SelectListItem
                {
                    Value = s.DdlTypeCode.ToString(),
                    Text = s.DdlTypeCode + "-" + s.DdlTypeValue.ToString()
                }).ToList();

                var other = new SelectListItem { Text = "99999999-Others", Value = "99999999" };
                result.Add(other);
                return result;
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetMaterialCode()
        {
            try
            {
                var codes = await _repoUnit.FormDRepository.GetMaterialCode();

                var result = codes.OrderBy(s => s.DdlPkRefNo).Select(s => new SelectListItem
                {
                    Value = s.DdlTypeCode.ToString(),
                    Text = s.DdlTypeCode + "-" + s.DdlTypeValue.ToString()
                }).ToList();

                var other = new SelectListItem { Text = "99999999-Others", Value = "99999999" };
                result.Add(other);
                return result;
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetEquipmentCode()
        {
            try
            {
                var codes = await _repoUnit.FormDRepository.GetEquipmentCode();

                var result = codes.OrderBy(s => s.DdlPkRefNo).Select(s => new SelectListItem
                {
                    Value = s.DdlTypeCode.ToString(),
                    Text = s.DdlTypeCode + "-" + s.DdlTypeValue.ToString()
                }).ToList();

                var other = new SelectListItem { Text = "99999999-Others", Value = "99999999" };
                result.Add(other);
                return result;
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetRMU()
        {
            try
            {
                var codes = await _repoUnit.FormDRepository.GetRMU();

                return codes.OrderBy(s => s.DdlPkRefNo).Select(s => new SelectListItem
                {
                    Value = s.DdlTypeCode.ToString(),
                    Text = s.DdlTypeCode + "-" + s.DdlTypeValue.ToString()
                }).ToArray();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetERTActivityCode()
        {
            try
            {
                var codes = await _repoUnit.FormDRepository.GetERTActivityCode();

                return codes.OrderBy(s => s.DdlPkRefNo).Select(s => new SelectListItem
                {
                    Value = s.DdlTypeCode.ToString(),
                    Text = s.DdlTypeCode + "-" + s.DdlTypeValue.ToString()
                }).ToArray();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }

        public async Task<bool> CheckHdrRefereceId(string id)
        {
            return await _repoUnit.FormDRepository.CheckHdrRefereceId(id);
        }
        public async Task<string> CheckAlreadyExists(int? WeekNo, int? year, string crewUnit, string day, string rmu, string secCode)
        {
            return await _repoUnit.FormDRepository.CheckAlreadyExists(WeekNo, year, crewUnit, day, rmu, secCode);
        }
        public async Task<bool> CheckDetailsRefereceId(string id)
        {
            return await _repoUnit.FormDRepository.CheckDetailsRefereceId(id);
        }
        public async Task<FormDHeaderRequestDTO> FindDetails(FormDHeaderRequestDTO headerDTO)
        {
            RmFormDHdr header = _mapper.Map<RmFormDHdr>(headerDTO);
            var obj = _repoUnit.FormDRepository.FindAsync(x => x.FdhRmu == header.FdhRmu && x.FdhRoadCode == header.FdhRoadCode && x.FdhYear == header.FdhYear && x.FdhWeekNo == header.FdhWeekNo && x.FdhDay == header.FdhDay && x.FdhCrewUnit == header.FdhCrewUnit && x.FdhActiveYn == true).Result;
            return _mapper.Map<FormDHeaderRequestDTO>(obj);
        }
        public async Task<IEnumerable<SelectListItem>> GetRoadCodesByRMU(string rmu)
        {
            try
            {
                var codes = await _repoUnit.FormDRepository.GetRoadCodesByRMU(rmu);

                return codes.OrderBy(s => s.RdmPkRefNo).Select(s => new SelectListItem
                {
                    Value = s.RdmRdCode.ToString(),
                    Text = s.RdmRdCode + "-" + s.RdmRdName.ToString()
                }).ToArray();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }

        }

        public async Task<IEnumerable<SelectListItem>> GetSectionCodesByRMU(string rmu)
        {
            try
            {
                var codes = await _repoUnit.FormDRepository.GetSectionCodesByRMU(rmu);

                return codes.OrderBy(s => s.RdsmPkRefNo).Select(s => new SelectListItem
                {

                    Value = s.RdsmSectionCode.ToString(),
                    Text = s.RdsmSectionCode + "-" + s.RdsmSectionName.ToString()
                }).ToArray();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }

        }


        public async Task<IEnumerable<SelectListItem>> GetFormXReferenceId(string rodeCode)
        {
            try
            {
                var codes = await _repoUnit.FormDRepository.GetFormXReferenceId(rodeCode);

                return codes.OrderBy(s => s.FxhPkRefNo).Select(s => new SelectListItem
                {
                    Value = s.FxhPkRefNo.ToString(),
                    Text = Convert.ToString(s.FxhRefId) == "" ? "No Refrence ID" : Convert.ToString(s.FxhRefId),
                }).ToArray();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }

        public async Task<string> GetMaxIdLength()
        {
            return await _repoUnit.FormDRepository.GetMaxIdLength();
        }

        public async Task<int?> GetLabourSrNo(int id)
        {
            return await _repoUnit.FormDRepository.GetLabourSRNO(id);
        }

        public async Task<int?> GetEqpSRNO(int id)
        {
            return await _repoUnit.FormDRepository.GetEqpSRNO(id);
        }

        public async Task<int?> GetMaterialSRNO(int id)
        {
            return await _repoUnit.FormDRepository.GetMatSrno(id);
        }

        public async Task<int?> GetDetailSRNO(int? id)
        {
            return await _repoUnit.FormDRepository.GetDtlSrno(id);
        }

        public async Task<int> UpdateFormDSignature(FormDHeaderRequestDTO formDDto)
        {
            int rowsAffected;
            try
            {

                var getHeader = await _repoUnit.FormDRepository.GetByIdAsync(formDDto.No);
                getHeader.FdhSubmitSts = formDDto.SubmitSts;
                getHeader.FdhSignAgrdSo = formDDto.SignAgrdSo ?? getHeader.FdhSignAgrdSo ?? null;
                getHeader.FdhSignPrcdSo = formDDto.SignPrcdSo ?? getHeader.FdhSignPrcdSo ?? null;
                getHeader.FdhSignPrp = formDDto.ReportedBySign ?? getHeader.FdhSignPrp ?? null;
                getHeader.FdhSignVer = formDDto.SignVer ?? getHeader.FdhSignVer ?? null;
                getHeader.FdhSignVerSo = formDDto.SignVerSo ?? getHeader.FdhSignVerSo ?? null;
                getHeader.FdhSignVet = formDDto.SignVet ?? getHeader.FdhSignVet ?? null;

                getHeader.FdhUseridAgrdSo = formDDto.UseridAgrdSo ?? getHeader.FdhUseridAgrdSo ?? null;
                getHeader.FdhUsernameAgrdSo = formDDto.UseridVer ?? getHeader.FdhUsernameAgrdSo ?? null;
                getHeader.FdhDesignationAgrdSo = formDDto.DesignationAgrdSo ?? getHeader.FdhDesignationAgrdSo ?? null;
                getHeader.FdhDtAgrdSo = formDDto.DtAgrdSo ?? getHeader.FdhDtAgrdSo ?? null;

                getHeader.FdhUseridPrcdSo = formDDto.UseridPrcdSo ?? getHeader.FdhUseridPrcdSo ?? null;
                getHeader.FdhUsernamePrcdSo = formDDto.UsernamePrcdSo ?? getHeader.FdhUsernamePrcdSo ?? null;
                getHeader.FdhDesignationPrcdSo = formDDto.DesignationPrcdSo ?? getHeader.FdhDesignationPrcdSo ?? null;
                getHeader.FdhDtPrcdSo = formDDto.DtPrcdSo ?? getHeader.FdhDtPrcdSo ?? null;

                if (formDDto.ReportedByUserId != null)
                {
                    getHeader.FdhUseridPrp = formDDto.ReportedByUserId.ToString() ?? getHeader.FdhUseridPrp ?? null;
                }
                getHeader.FdhUsernamePrp = formDDto.ReportedByUsername ?? getHeader.FdhUsernamePrp ?? null;
                getHeader.FdhDesignationPrp = formDDto.ReportedByDesignation ?? getHeader.FdhDesignationPrp ?? null;
                getHeader.FdhDtPrp = formDDto.DateReported ?? getHeader.FdhDtPrp ?? null;

                getHeader.FdhUseridVer = formDDto.UseridVer ?? getHeader.FdhUseridVer ?? null;
                getHeader.FdhUsernameVer = formDDto.UsernameVer ?? getHeader.FdhUsernameVer ?? null;
                getHeader.FdhDesignationVer = formDDto.DesignationVer ?? getHeader.FdhDesignationVer ?? null;
                getHeader.FdhDtVer = formDDto.DtVer ?? getHeader.FdhDtVer ?? null;

                getHeader.FdhUseridVerSo = formDDto.UseridVerSo ?? getHeader.FdhUseridVerSo ?? null;
                getHeader.FdhUsernameVerSo = formDDto.UsernameVerSo ?? getHeader.FdhUsernameVerSo ?? null;
                getHeader.FdhDesignationVerSo = formDDto.DesignationVerSo ?? getHeader.FdhDesignationVerSo ?? null;
                getHeader.FdhDtVerSo = formDDto.DtVerSo ?? getHeader.FdhDtVerSo ?? null;

                getHeader.FdhUseridVet = formDDto.UseridVet ?? getHeader.FdhUseridVet ?? null;
                getHeader.FdhUsernameVet = formDDto.UsernameVet ?? getHeader.FdhUsernameVet ?? null;
                getHeader.FdhDesignationVet = formDDto.DesignationVet ?? getHeader.FdhDesignationVet ?? null;
                getHeader.FdhDtVet = formDDto.DtVet ?? getHeader.FdhDtVet ?? null;


                var formD = _mapper.Map<RmFormDHdr>(getHeader);


                _repoUnit.FormDRepository.Update(formD);


                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }
        public async Task<FormDHeaderRequestDTO> FindAndSaveFormDHdr(FormDHeaderRequestDTO header, bool updateSubmit)
        {
            var formD = _mapper.Map<RmFormDHdr>(header);
            var response = await _repoUnit.FormDRepository.FindSaveFormDHdr(formD, updateSubmit);
            return _mapper.Map<FormDHeaderRequestDTO>(response);
        }
    }
}
