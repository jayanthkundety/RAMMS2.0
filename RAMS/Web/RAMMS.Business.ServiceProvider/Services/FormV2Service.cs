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
    public class FormV2Service : IFormV2Service
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        private readonly IProcessService processService;
        public FormV2Service(IRepositoryUnit repoUnit, IMapper mapper, ISecurity security, IProcessService process)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security;
            processService = process;
        }

        public async Task<int> DeActivateFormV2Async(int formNo)
        {
            int rowsAffected;
            try
            {
                var domainModelFormV2 = await _repoUnit.FormV2Repository.GetByIdAsync(formNo);
                domainModelFormV2.Fv2hActiveYn = false;
                _repoUnit.FormV2Repository.Update(domainModelFormV2);

                var formDLabour = await _repoUnit.FormV2LabourRepository.GetAllLabourById(domainModelFormV2.Fv2hPkRefNo);
                foreach (var labour in formDLabour)
                {
                    labour.Fv2lActiveYn = false;
                    _repoUnit.FormV2LabourRepository.Update(labour);
                }

                var formDMaterial = await _repoUnit.FormV2MaterialRepository.GetAllMaterialById(domainModelFormV2.Fv2hPkRefNo);
                foreach (var material in formDMaterial)
                {
                    material.Fv2mActiveYn = false;
                    _repoUnit.FormV2MaterialRepository.Update(material);
                }

                var formDEquipment = await _repoUnit.FormV2EquipmentRepository.GetAllEquipmentById(domainModelFormV2.Fv2hPkRefNo);
                foreach (var equip in formDEquipment)
                {
                    equip.Fv2eActiveYn = false;
                    _repoUnit.FormV2EquipmentRepository.Update(equip);
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

        public async Task<int> DeActivateFormV2LabourAsync(int formNo)
        {
            int rowsAffected;
            try
            {
                var domainModelFormV2 = await _repoUnit.FormV2LabourRepository.GetByIdAsync(formNo);
                domainModelFormV2.Fv2lActiveYn = false;
                _repoUnit.FormV2LabourRepository.Update(domainModelFormV2);

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
                var domainModelFormV2 = await _repoUnit.FormV2MaterialRepository.GetByIdAsync(formNo);
                domainModelFormV2.Fv2mActiveYn = false;
                _repoUnit.FormV2MaterialRepository.Update(domainModelFormV2);

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> DeActivateFormV2EquipmentAsync(int formNo)
        {
            int rowsAffected;
            try
            {
                var domainModelFormV2 = await _repoUnit.FormV2EquipmentRepository.GetByIdAsync(formNo);
                domainModelFormV2.Fv2eActiveYn = false;
                _repoUnit.FormV2EquipmentRepository.Update(domainModelFormV2);

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public RmFormV2Hdr UpdateStatus(RmFormV2Hdr form)
        {
            if (form.Fv2hPkRefNo > 0)
            {
                var existsObj = _repoUnit.FormDRepository._context.RmFormV2Hdr.Where(x => x.Fv2hPkRefNo == form.Fv2hPkRefNo).Select(x => new { Status = x.Fv2hStatus, Log = x.Fv2hAuditLog }).FirstOrDefault();
                if (existsObj != null)
                {
                    form.Fv2hAuditLog = existsObj.Log;
                    form.Fv2hStatus = existsObj.Status;
                }
            }

            if (form.Fv2hStatus == "Initialize" || string.IsNullOrEmpty(form.Fv2hStatus))
                form.Fv2hStatus = Common.StatusList.FormV2Saved;
            else if (form.Fv2hSubmitSts)
            {
                form.Fv2hStatus = Common.StatusList.FormV2Submitted;
                form.Fv2hAuditLog = Utility.ProcessLog(form.Fv2hAuditLog, "Recorded By", "Submitted", form.Fv2hUsernameSch, string.Empty, form.Fv2hDtSch, _security.UserName);
                processService.SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = _security.UserName,
                    RmNotGroup = GroupNames.OperationsExecutive,
                    RmNotMessage = "Recorded By:" + form.Fv2hUsernameSch + " - Form V2 (" + form.Fv2hRefId + ")",
                    RmNotOn = DateTime.Now,
                    RmNotUrl = "/MAM/EditFormV2?id=" + form.Fv2hPkRefNo.ToString() + "&view=1",
                    RmNotUserId = "",
                    RmNotViewed = ""
                }, true);
            }
            return form;
        }
        public async Task<int> SaveFormV2Async(FormV2HeaderResponseDTO FormV2HeaderBO)
        {
            FormV2HeaderResponseDTO formDRequest;
            try
            {
                var domainModelFormV2 = _mapper.Map<RmFormV2Hdr>(FormV2HeaderBO);
                domainModelFormV2.Fv2hPkRefNo = FormV2HeaderBO.PkRefNo;
                domainModelFormV2.Fv2hFv1hPkRefNo = FormV2HeaderBO.Fv1hPkRefNo;
                domainModelFormV2 = UpdateStatus(domainModelFormV2);
                var entity = _repoUnit.FormV2Repository.CreateReturnEntity(domainModelFormV2);
                formDRequest = _mapper.Map<FormV2HeaderResponseDTO>(entity);

                return formDRequest.PkRefNo;

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public async Task<int> SaveFormV2LabourAsync(FormV2LabourDetailsResponseDTO FormV2LabourBO)
        {

            FormV2LabourDetailsResponseDTO formV2Request;
            try
            {
                var domainModelFormV2 = _mapper.Map<RmFormV2Lab>(FormV2LabourBO);
                domainModelFormV2.Fv2lPkRefNo = FormV2LabourBO.PkRefNo;
                var entity = _repoUnit.FormV2LabourRepository.CreateReturnEntity(domainModelFormV2);
                formV2Request = _mapper.Map<FormV2LabourDetailsResponseDTO>(entity);


            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return int.Parse(formV2Request.PkRefNo.ToString());
        }

        public async Task<int> SaveFormV2MaterialAsync(FormV2MaterialDetailsResponseDTO FormV2MatBO)
        {

            FormV2MaterialDetailsResponseDTO formV2Request;
            try
            {
                var domainModelFormV2 = _mapper.Map<RmFormV2Mat>(FormV2MatBO);
                domainModelFormV2.Fv2mPkRefNo = FormV2MatBO.PkRefNo;
                var entity = _repoUnit.FormV2MaterialRepository.CreateReturnEntity(domainModelFormV2);
                formV2Request = _mapper.Map<FormV2MaterialDetailsResponseDTO>(entity);


            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return int.Parse(formV2Request.PkRefNo.ToString());
        }

        public async Task<int> SaveFormV2EquipmentAsync(FormV2EquipDetailsResponseDTO FormV2EqpBO)
        {

            FormV2EquipDetailsResponseDTO formV2Request;
            try
            {
                var domainModelFormV2 = _mapper.Map<RmFormV2Eqp>(FormV2EqpBO);
                domainModelFormV2.Fv2ePkRefNo = FormV2EqpBO.PkRefNo;
                var entity = _repoUnit.FormV2EquipmentRepository.CreateReturnEntity(domainModelFormV2);
                formV2Request = _mapper.Map<FormV2EquipDetailsResponseDTO>(entity);


            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return int.Parse(formV2Request.PkRefNo.ToString());
        }


        public async Task<FormV2HeaderResponseDTO> SaveHeaderwithResponse(FormV2HeaderResponseDTO headerReq)
        {
            FormV2HeaderResponseDTO formD;

            int refCheckCount = await _repoUnit.FormV2Repository.CheckwithRef(headerReq);
            if (refCheckCount != 0)
            {
                headerReq.PkRefNo = refCheckCount;
                var currentFormV2 = await _repoUnit.FormV2Repository.GetFormWithDetailsByNoAsync(Convert.ToInt32(headerReq.PkRefNo)).ConfigureAwait(false);
                formD = _mapper.Map<FormV2HeaderResponseDTO>(currentFormV2);
                return formD;

            }
            else
            {
                var domainModelFormV2 = _mapper.Map<RmFormV2Hdr>(headerReq);

                _repoUnit.FormV2Repository.Create(domainModelFormV2);

                await _repoUnit.CommitAsync();

                return null;
            }
        }

        public async Task<List<string>> GetSectionByRMU(string rmu)
        {
            var data = await _repoUnit.FormV2Repository.GetSectionByRMU(rmu).ConfigureAwait(false);

            return data;

        }

        public async Task<PagingResult<FormV2HeaderResponseDTO>> GetFilteredFormV2Grid(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions)
        {
            PagingResult<FormV2HeaderResponseDTO> result = new PagingResult<FormV2HeaderResponseDTO>();

            List<FormV2HeaderResponseDTO> formDList = new List<FormV2HeaderResponseDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormV2Repository.GetFilteredRecordList(filterOptions);

                result.TotalRecords = await _repoUnit.FormV2Repository.GetFilteredRecordCount(filterOptions).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    var _ = _mapper.Map<FormV2HeaderResponseDTO>(listData);
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

        public async Task<FormV2HeaderResponseDTO> GetFormV2WithDetailsByNoAsync(int formNo)
        {
            FormV2HeaderResponseDTO formD;
            try
            {
                var currentFormV2 = await _repoUnit.FormV2Repository.GetFormWithDetailsByNoAsync(formNo).ConfigureAwait(false);
                formD = _mapper.Map<FormV2HeaderResponseDTO>(currentFormV2);

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return formD;
        }

        public async Task<int> UpdateFormV2Async(FormV2HeaderResponseDTO formV2DTO)
        {
            int rowsAffected;
            try
            {
                var domainModelformV2 = _mapper.Map<RmFormV2Hdr>(formV2DTO);
                domainModelformV2.Fv2hPkRefNo = formV2DTO.PkRefNo;
                //domainModelformV2.Fv2hFv1hPkRefNo= formV2DTO.PkRefNo;
                domainModelformV2.Fv2hModBy = _security.UserID;
                domainModelformV2.Fv2hModDt = DateTime.Today;
                domainModelformV2.Fv2hActiveYn = true;
                domainModelformV2 = UpdateStatus(domainModelformV2);
                _repoUnit.FormV2Repository.Update(domainModelformV2);

                if (domainModelformV2.Fv2hSubmitSts)
                {
                    var formDLabour = await _repoUnit.FormV2LabourRepository.GetAllLabourById(domainModelformV2.Fv2hPkRefNo);
                    foreach (var labour in formDLabour)
                    {
                        labour.Fv2lSubmitSts = true;
                        _repoUnit.FormV2LabourRepository.Update(labour);
                    }

                    var formDMaterial = await _repoUnit.FormV2MaterialRepository.GetAllMaterialById(domainModelformV2.Fv2hPkRefNo);
                    foreach (var material in formDMaterial)
                    {
                        material.Fv2mSubmitSts = true;
                        _repoUnit.FormV2MaterialRepository.Update(material);
                    }

                    var formDEquipment = await _repoUnit.FormV2EquipmentRepository.GetAllEquipmentById(domainModelformV2.Fv2hPkRefNo);
                    foreach (var equip in formDEquipment)
                    {
                        equip.Fv2eSubmitSts = true;
                        _repoUnit.FormV2EquipmentRepository.Update(equip);
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


        public async Task<PagingResult<FormV2EquipDetailsResponseDTO>> GetEquipmentFormV2Grid(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions, string id)
        {
            PagingResult<FormV2EquipDetailsResponseDTO> result = new PagingResult<FormV2EquipDetailsResponseDTO>();

            List<FormV2EquipDetailsResponseDTO> formDEquipList = new List<FormV2EquipDetailsResponseDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormV2EquipmentRepository.GetFilteredRecordList(filterOptions, id);

                result.TotalRecords = await _repoUnit.FormV2EquipmentRepository.GetFilteredRecordCount(filterOptions, id).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    formDEquipList.Add(_mapper.Map<FormV2EquipDetailsResponseDTO>(listData));
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

        public async Task<PagingResult<FormV2MaterialDetailsResponseDTO>> GetMaterialFormV2Grid(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions, string id)
        {
            PagingResult<FormV2MaterialDetailsResponseDTO> result = new PagingResult<FormV2MaterialDetailsResponseDTO>();

            List<FormV2MaterialDetailsResponseDTO> formDMaterialList = new List<FormV2MaterialDetailsResponseDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormV2MaterialRepository.GetFilteredRecordList(filterOptions, id);

                result.TotalRecords = await _repoUnit.FormV2MaterialRepository.GetFilteredRecordCount(filterOptions, id).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    formDMaterialList.Add(_mapper.Map<FormV2MaterialDetailsResponseDTO>(listData));
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

        public async Task<PagingResult<FormV2LabourDetailsResponseDTO>> GetLabourFormV2Grid(FilteredPagingDefinition<FormV2SearchGridDTO> filterOptions, string id)
        {
            PagingResult<FormV2LabourDetailsResponseDTO> result = new PagingResult<FormV2LabourDetailsResponseDTO>();

            List<FormV2LabourDetailsResponseDTO> formDLabourList = new List<FormV2LabourDetailsResponseDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormV2LabourRepository.GetFilteredRecordList(filterOptions, id);

                result.TotalRecords = await _repoUnit.FormV2LabourRepository.GetFilteredRecordCount(filterOptions, id).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    formDLabourList.Add(_mapper.Map<FormV2LabourDetailsResponseDTO>(listData));
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

        public async Task<int> UpdateFormV2LabourAsync(FormV2LabourDetailsResponseDTO FormV2LabourBO)
        {
            int rowsAffected;
            try
            {
                var domainModelFormV2 = _mapper.Map<RmFormV2Lab>(FormV2LabourBO);
                domainModelFormV2.Fv2lPkRefNo = FormV2LabourBO.PkRefNo;
                domainModelFormV2.Fv2lFv2hPkRefNo = FormV2LabourBO.Fv2hPkRefNo;
                _repoUnit.FormV2LabourRepository.Update(domainModelFormV2);

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> UpdateFormV2MaterialAsync(FormV2MaterialDetailsResponseDTO FormV2MatBO)
        {
            int rowsAffected;
            try
            {
                var domainModelFormV2 = _mapper.Map<RmFormV2Mat>(FormV2MatBO);
                domainModelFormV2.Fv2mPkRefNo = FormV2MatBO.PkRefNo;
                domainModelFormV2.Fv2mFv2hPkRefNo = FormV2MatBO.Fv2hPkRefNo;
                _repoUnit.FormV2MaterialRepository.Update(domainModelFormV2);

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> UpdateFormV2EquipmentAsync(FormV2EquipDetailsResponseDTO FormV2LabourBO)
        {
            int rowsAffected;
            try
            {
                var domainModelFormV2 = _mapper.Map<RmFormV2Eqp>(FormV2LabourBO);
                domainModelFormV2.Fv2ePkRefNo = FormV2LabourBO.PkRefNo;
                domainModelFormV2.Fv2eFv2hPkRefNo = FormV2LabourBO.Fv2hPkRefNo;
                _repoUnit.FormV2EquipmentRepository.Update(domainModelFormV2);

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

        public async Task<FormV2LabourDetailsResponseDTO> GetFormV2LabourDetailsByNoAsync(int formNo)
        {
            FormV2LabourDetailsResponseDTO formD;
            try
            {
                var currentFormV2 = await _repoUnit.FormV2LabourRepository.GetFormWithDetailsByNoAsync(formNo).ConfigureAwait(false);
                formD = _mapper.Map<FormV2LabourDetailsResponseDTO>(currentFormV2);

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return formD;
        }

        public async Task<FormV2MaterialDetailsResponseDTO> GetFormV2MaterialDetailsByNoAsync(int formNo)
        {
            FormV2MaterialDetailsResponseDTO formD;
            try
            {
                var currentFormV2 = await _repoUnit.FormV2MaterialRepository.GetFormWithDetailsByNoAsync(formNo).ConfigureAwait(false);
                formD = _mapper.Map<FormV2MaterialDetailsResponseDTO>(currentFormV2);

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return formD;
        }

        public async Task<FormV2EquipDetailsResponseDTO> GetFormV2EquipmentDetailsByNoAsync(int formNo)
        {
            FormV2EquipDetailsResponseDTO formD;
            try
            {
                var currentFormV2 = await _repoUnit.FormV2EquipmentRepository.GetFormWithDetailsByNoAsync(formNo).ConfigureAwait(false);
                formD = _mapper.Map<FormV2EquipDetailsResponseDTO>(currentFormV2);

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
                var codes = await _repoUnit.FormV2Repository.GetRoadCodes();

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
                var codes = await _repoUnit.FormV2Repository.GetRoadCodeBySectionCode(secCode);

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
                var codes = await _repoUnit.FormV2Repository.GetDivisions();

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
                var codes = await _repoUnit.FormV2Repository.GetActivityMainTask();

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
                var codes = await _repoUnit.FormV2Repository.GetActivitySubTask();

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
                var codes = await _repoUnit.FormV2Repository.GetSectionCode();

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
                var codes = await _repoUnit.FormV2Repository.GetLabourCode();

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

        public async Task<IEnumerable<SelectListItem>> GetMaterialCode()
        {
            try
            {
                var codes = await _repoUnit.FormV2Repository.GetMaterialCode();

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
                var codes = await _repoUnit.FormV2Repository.GetEquipmentCode();

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
                var codes = await _repoUnit.FormV2Repository.GetRMU();

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
                var codes = await _repoUnit.FormV2Repository.GetERTActivityCode();

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
            return await _repoUnit.FormV2Repository.CheckHdrRefereceId(id);
        }
        public async Task<string> CheckAlreadyExists(DateTime? date, string crewUnit, string day, string rmu, string secCode)
        {
            return await _repoUnit.FormV2Repository.CheckAlreadyExists(date, crewUnit, day, rmu, secCode);
        }

        public async Task<FormV2HeaderResponseDTO> FindDetails(FormV2HeaderResponseDTO headerDTO)
        {
            RmFormV2Hdr header = _mapper.Map<RmFormV2Hdr>(headerDTO);
            var obj = _repoUnit.FormV2Repository.FindAllAsync(x => x.Fv2hRmu == header.Fv2hRmu && x.Fv2hSecCode == header.Fv2hSecCode && x.Fv2hActCode == header.Fv2hActCode && x.Fv2hDt == header.Fv2hDt && x.Fv2hCrew == header.Fv2hCrew && x.Fv2hActiveYn == true).Result;
            return _mapper.Map<FormV2HeaderResponseDTO>(obj.FirstOrDefault());
        }

        public async Task<FormV1ResponseDTO> FindV1Details(FormV2HeaderResponseDTO header)
        {
            var obj = _repoUnit.FormV1Repository.FindAsync(x => x.Fv1hRmu == header.Rmu && x.Fv1hSecCode == header.SecCode && x.Fv1hActCode == header.ActCode && x.Fv1hDt.Value.Year == header.Dt.Value.Year && x.Fv1hDt.Value.Month == header.Dt.Value.Month && x.Fv1hDt.Value.Day == header.Dt.Value.Day && x.Fv1hCrew == header.Crew && x.Fv1hActiveYn == true).Result;
            return _mapper.Map<FormV1ResponseDTO>(obj);
        }

        public async Task<IEnumerable<SelectListItem>> GetRoadCodesByRMU(string rmu)
        {
            try
            {
                var codes = await _repoUnit.FormV2Repository.GetRoadCodesByRMU(rmu);

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
                var codes = await _repoUnit.FormV2Repository.GetSectionCodesByRMU(rmu);

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
                var codes = await _repoUnit.FormV2Repository.GetFormXReferenceId(rodeCode);

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
            return await _repoUnit.FormV2Repository.GetMaxIdLength();
        }


        public async Task<int> UpdateFormV2Signature(FormV2HeaderResponseDTO formDDto)
        {
            int rowsAffected;
            try
            {

                var getHeader = await _repoUnit.FormV2Repository.GetByIdAsync(formDDto.PkRefNo);
                getHeader.Fv2hSubmitSts = formDDto.SubmitSts;
                getHeader.Fv2hSignAck = formDDto.SignAck ?? getHeader.Fv2hSignAck ?? null;
                getHeader.Fv2hSignSch = formDDto.SignSch ?? getHeader.Fv2hSignSch ?? null;
                getHeader.Fv2hSignAgr = formDDto.SignAgr ?? getHeader.Fv2hSignAgr ?? null;


                getHeader.Fv2hUseridAck = formDDto.UseridAck ?? getHeader.Fv2hUseridAck ?? null;
                getHeader.Fv2hUsernameAck = formDDto.UsernameAck ?? getHeader.Fv2hUsernameAck ?? null;
                getHeader.Fv2hDesignationAck = formDDto.DesignationAck ?? getHeader.Fv2hDesignationAck ?? null;
                getHeader.Fv2hDtAck = formDDto.DtAck ?? getHeader.Fv2hDtAck ?? null;

                getHeader.Fv2hUseridSch = formDDto.UseridSch ?? getHeader.Fv2hUseridSch ?? null;
                getHeader.Fv2hUsernameSch = formDDto.UsernameSch ?? getHeader.Fv2hUsernameSch ?? null;
                getHeader.Fv2hDesignationSch = formDDto.DesignationSch ?? getHeader.Fv2hDesignationSch ?? null;
                getHeader.Fv2hDtSch = formDDto.DtSch ?? getHeader.Fv2hDtSch ?? null;

                getHeader.Fv2hUseridAgr = formDDto.UseridAgr ?? getHeader.Fv2hUseridAgr ?? null;
                getHeader.Fv2hUsernameAgr = formDDto.UsernameAgr ?? getHeader.Fv2hUsernameAgr ?? null;
                getHeader.Fv2hDesignationAgr = formDDto.DesignationAgr ?? getHeader.Fv2hDesignationAgr ?? null;
                getHeader.Fv2hDtAgr = formDDto.DtAgr ?? getHeader.Fv2hDtAgr ?? null;


                var formD = _mapper.Map<RmFormV2Hdr>(getHeader);


                _repoUnit.FormV2Repository.Update(formD);


                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }
        public async Task<FormV2HeaderResponseDTO> FindAndSaveFormV2Hdr(FormV2HeaderResponseDTO header, bool updateSubmit)
        {
            var formV2 = _mapper.Map<RmFormV2Hdr>(header);
            formV2.Fv2hPkRefNo = header.PkRefNo;
            formV2.Fv2hFv1hPkRefNo = header.Fv1hPkRefNo;
            var response = await _repoUnit.FormV2Repository.FindSaveFormV2Hdr(formV2, updateSubmit);
            return _mapper.Map<FormV2HeaderResponseDTO>(response);
        }
    }
}
