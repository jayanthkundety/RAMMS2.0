using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Common;
using RAMMS.Common.RefNumber;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;

namespace RAMMS.Business.ServiceProvider.Services
{

    public class FormV1Service : IFormV1Service
    {
        private readonly IFormV1Repository _repo;
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        private readonly IProcessService processService;
        public FormV1Service(IRepositoryUnit repoUnit, IFormV1Repository repo, IMapper mapper, ISecurity security, IProcessService process)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security;
            processService = process;
            _repo = repo;
        }


        #region FormV1


        public async Task<PagingResult<FormV1ResponseDTO>> GetFilteredFormV1Grid(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions)
        {
            PagingResult<FormV1ResponseDTO> result = new PagingResult<FormV1ResponseDTO>();

            List<FormV1ResponseDTO> formDList = new List<FormV1ResponseDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormV1Repository.GetFilteredRecordList(filterOptions);

                result.TotalRecords = await _repoUnit.FormV1Repository.GetFilteredRecordCount(filterOptions).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    var _ = _mapper.Map<FormV1ResponseDTO>(listData);
                    // _.ProcessStatus = listData.FV1hStatus;

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


        public async Task<PagingResult<FormV1WorkScheduleGridDTO>> GetFormV1WorkScheduleGridList(FilteredPagingDefinition<FormV1WorkScheduleGridDTO> filterOptions, int V1PkRefNo)
        {
            PagingResult<FormV1WorkScheduleGridDTO> result = new PagingResult<FormV1WorkScheduleGridDTO>();

            List<FormV1WorkScheduleGridDTO> formV1WorkScheduleList = new List<FormV1WorkScheduleGridDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormV1Repository.GetFormV1WorkScheduleGridList(filterOptions, V1PkRefNo);

                result.TotalRecords = filteredRecords.Count();  // await _repoUnit.FormDRepository.GetFilteredRecordCount(filterOptions).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    FormV1WorkScheduleGridDTO obj = new FormV1WorkScheduleGridDTO();

                    obj.Fv1hPkRefNo = listData.Fv1dFv1hPkRefNo;
                    obj.Fs1dPkRefNo = listData.Fv1dS1dPkRefNo;
                    obj.Chainage = Convert.ToString(listData.Fv1dFrmChDeci);
                    obj.ChainageFrom = Convert.ToString(listData.Fv1dFrmCh);
                    obj.ChainageFromDec = Convert.ToString(listData.Fv1dFrmChDeci);
                    obj.ChainageTo = Convert.ToString(listData.Fv1dToCh);
                    obj.ChainageToDec = Convert.ToString(listData.Fv1dToChDeci);
                    obj.PkRefNo = listData.Fv1dPkRefNo;
                    obj.Remarks = listData.Fv1dRemarks;
                    obj.RoadCode = listData.Fv1dRoadCode;
                    obj.RoadName = listData.Fv1dRoadName;
                    obj.SiteRef = listData.Fv1dSiteRef;
                    obj.StartTime = listData.Fv1dStartTime;

                    formV1WorkScheduleList.Add(obj);
                }

                result.PageResult = formV1WorkScheduleList;

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



        public async Task<FormV1ResponseDTO> SaveFormV1(FormV1ResponseDTO FormV1)
        {
            try
            {
                var domainModelFormV1 = _mapper.Map<RmFormV1Hdr>(FormV1);
                domainModelFormV1.Fv1hPkRefNo = 0;

                //var obj = _repoUnit.FormV1Repository.FindAsync(x => x.Fv1hRmu == domainModelFormV1.Fv1hRmu && x.Fv1hActCode == domainModelFormV1.Fv1hActCode && x.Fv1hSecCode == domainModelFormV1.Fv1hSecCode && x.Fv1hCrew == domainModelFormV1.Fv1hCrew && x.Fv1hDt == domainModelFormV1.Fv1hDt && x.Fv1hActiveYn == true).Result;
                var obj = _repoUnit.FormV1Repository.FindAsync(x => x.Fv1hRmu == domainModelFormV1.Fv1hRmu && x.Fv1hActCode == domainModelFormV1.Fv1hActCode && x.Fv1hDt == domainModelFormV1.Fv1hDt && x.Fv1hCrew == domainModelFormV1.Fv1hCrew && x.Fv1hActiveYn == true).Result;
                if (obj != null)
                {
                    var res= _mapper.Map<FormV1ResponseDTO>(obj);
                    res.FormExist = true;
                    return res;
                }

                IDictionary<string, string> lstData = new Dictionary<string, string>();
                lstData.Add("YYYYMMDD", Utility.ToString(DateTime.Today.ToString("yyyyMMdd")));
                lstData.Add("Crew", domainModelFormV1.Fv1hCrew.ToString());
                lstData.Add("ActivityCode", domainModelFormV1.Fv1hActCode);
                domainModelFormV1.Fv1hRefId = FormRefNumber.GetRefNumber(RAMMS.Common.RefNumber.FormType.FormV1Header, lstData);

                var entity = _repoUnit.FormV1Repository.CreateReturnEntity(domainModelFormV1);
                FormV1.PkRefNo = _mapper.Map<FormV1ResponseDTO>(entity).PkRefNo;
                FormV1.RefId = domainModelFormV1.Fv1hRefId;
                FormV1.Status = domainModelFormV1.Fv1hStatus;

                return FormV1;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public int? SaveFormV1WorkSchedule(FormV1DtlResponseDTO FormV1Dtl)
        {

            try
            {
                var model = _mapper.Map<RmFormV1Dtl>(FormV1Dtl);
                model.Fv1dPkRefNo = 0;
                return _repo.SaveFormV1WorkSchedule(model);
            }
            catch (Exception ex)
            {
                _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public int? UpdateFormV1WorkSchedule(FormV1DtlResponseDTO FormV1Dtl)
        {

            try
            {
                int? Fv1hPkRefNo = FormV1Dtl.Fv1hPkRefNo;
                int Fv1dPkRefNo = FormV1Dtl.PkRefNo;
                var model = _mapper.Map<RmFormV1Dtl>(FormV1Dtl);
                model.Fv1dPkRefNo = Fv1dPkRefNo;
                model.Fv1dFv1hPkRefNo = Fv1hPkRefNo;
                return _repo.UpdateFormV1WorkSchedule(model);
            }
            catch (Exception ex)
            {
                _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public int? DeleteFormV1(int id)
        {
            int? rowsAffected;
            try
            {
                rowsAffected = _repo.DeleteFormV1(id);
            }
            catch (Exception ex)
            {
                _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }


        public int? DeleteFormV1WorkSchedule(int id)
        {
            int? rowsAffected;
            try
            {
                rowsAffected = _repo.DeleteFormV1WorkSchedule(id);
            }
            catch (Exception ex)
            {
                _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }


        public async Task<int> Update(FormV1ResponseDTO FormV1)
        {
            int rowsAffected;
            try
            {
                int PkRefNo = FormV1.PkRefNo;

                var domainModelFormV1 = _mapper.Map<RmFormV1Hdr>(FormV1);
                domainModelFormV1.Fv1hPkRefNo = PkRefNo;
                domainModelFormV1.Fv1hActiveYn = true;
                domainModelFormV1 = UpdateStatus(domainModelFormV1);
                _repoUnit.FormV1Repository.Update(domainModelFormV1);
                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public RmFormV1Hdr UpdateStatus(RmFormV1Hdr form)
        {
            if (form.Fv1hPkRefNo > 0)
            {
                var existsObj = _repoUnit.FormV1Repository._context.RmFormV1Hdr.Where(x => x.Fv1hPkRefNo == form.Fv1hPkRefNo).Select(x => new { Status = x.Fv1hStatus, Log = x.Fv1hAuditLog }).FirstOrDefault();
                if (existsObj != null)
                {
                    form.Fv1hAuditLog = existsObj.Log;
                    // form.Fv1hStatus = existsObj.Status;

                }

            }
            if (form.Fv1hSubmitSts && form.Fv1hStatus == "Saved")
            {
                form.Fv1hStatus = Common.StatusList.FormW2Submitted;
                form.Fv1hAuditLog = Utility.ProcessLog(form.Fv1hAuditLog, "Submitted By", "Submitted", form.Fv1hUsernameSch, string.Empty, form.Fv1hDtSch, _security.UserName);
                processService.SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = _security.UserName,
                    RmNotGroup = GroupNames.OperationsExecutive,
                    RmNotMessage = "Recorded By:" + form.Fv1hUsernameSch + " - Form WN (" + form.Fv1hPkRefNo + ")",
                    RmNotOn = DateTime.Now,
                    RmNotUrl = "/MAM/EditFormV1?id=" + form.Fv1hPkRefNo.ToString(),
                    RmNotUserId = "",
                    RmNotViewed = ""
                }, true);
            }

            return form;
        }

        public async Task<FormV1ResponseDTO> FindFormV1ByID(int id)
        {
            RmFormV1Hdr formV1 = await _repo.FindFormV1ByID(id);
            return _mapper.Map<FormV1ResponseDTO>(formV1);

        }

        public List<SelectListItem> FindRefNoFromS1(FormV1ResponseDTO FormV1)
        {
            return _repo.FindRefNoFromS1(FormV1);
        }

        public int LoadS1Data(int PKRefNo, int S1PKRefNo, string ActCode)
        {
            return _repo.LoadS1Data(PKRefNo, S1PKRefNo, ActCode);
        }


        public int PullS1Data(int PKRefNo, int S1PKRefNo, string ActCode)
        {
            return _repo.PullS1Data(PKRefNo, S1PKRefNo, ActCode);
        }

        #endregion

        #region FormV3


        public async Task<PagingResult<FormV3ResponseDTO>> GetFilteredFormV3Grid(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions)
        {
            PagingResult<FormV3ResponseDTO> result = new PagingResult<FormV3ResponseDTO>();

            List<FormV3ResponseDTO> formDList = new List<FormV3ResponseDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormV1Repository.GetFilteredV3RecordList(filterOptions);

                result.TotalRecords = await _repoUnit.FormV1Repository.GetFilteredV3RecordCount(filterOptions).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    var _ = _mapper.Map<FormV3ResponseDTO>(listData);

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



        public async Task<PagingResult<FormV3DtlGridDTO>> GetFormV3DtlGridList(FilteredPagingDefinition<FormV3DtlGridDTO> filterOptions, int V3PkRefNo)
        {
            PagingResult<FormV3DtlGridDTO> result = new PagingResult<FormV3DtlGridDTO>();

            List<FormV3DtlGridDTO> formV3DtlList = new List<FormV3DtlGridDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormV1Repository.GetFormv3DtlGridList(filterOptions, V3PkRefNo);

                result.TotalRecords = filteredRecords.Count();  // await _repoUnit.FormDRepository.GetFilteredRecordCount(filterOptions).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    FormV3DtlGridDTO obj = new FormV3DtlGridDTO();

                    obj.Fv3hPkRefNo = listData.Fv3dFv3hPkRefNo;
                    obj.PkRefNo = listData.Fv3dPkRefNo;
                    obj.FrmCh = listData.Fv3dFrmCh;
                    obj.ToCh = listData.Fv3dToCh;
                    obj.FrmChDeci = listData.Fv3dFrmChDeci;
                    obj.ToChDeci = listData.Fv3dToChDeci;
                    obj.ChainageTo = Convert.ToString(listData.Fv3dToCh) + "+" + Convert.ToString(listData.Fv3dToChDeci);
                    obj.ChainageFrom = Convert.ToString(listData.Fv3dFrmCh) + "+" + Convert.ToString(listData.Fv3dFrmChDeci);
                    obj.RoadCode = listData.Fv3dRoadCode;
                    obj.RoadName = listData.Fv3dRoadName;
                    obj.Length = listData.Fv3dLength;
                    obj.Width = listData.Fv3dWidth;
                    obj.TimetakenFrm = listData.Fv3dTimetakenFrm;
                    obj.TimeTakenTo = listData.Fv3dTimeTakenTo;
                    obj.TimeTakenTotal = listData.Fv3dTimeTakenTotal;
                    obj.Adp = listData.Fv3dAdp;
                    obj.TransitTimeFrm = listData.Fv3dTransitTimeFrm;
                    obj.TransitTimeTo = listData.Fv3dTransitTimeTo;
                    obj.TransitTimeTotal = listData.Fv3dTransitTimeTotal;

                    formV3DtlList.Add(obj);
                }

                result.PageResult = formV3DtlList;

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

        public async Task<FormV3ResponseDTO> FindFormV3ByID(int id)
        {
            RmFormV3Hdr formV3 = await _repo.FindFormV3ByID(id);
            return _mapper.Map<FormV3ResponseDTO>(formV3);

        }

        public async Task<FormV3ResponseDTO> SaveFormV3(FormV3ResponseDTO Formv3)
        {
            try
            {
                return await _repo.SaveFormV3(Formv3);

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public async Task<int> UpdateV3(FormV3ResponseDTO FormV3)
        {
            int rowsAffected;
            try
            {
                int PkRefNo = FormV3.PkRefNo;

                var domainModelFormV3 = _mapper.Map<RmFormV3Hdr>(FormV3);
                domainModelFormV3.Fv3hPkRefNo = PkRefNo;
                domainModelFormV3.Fv3hActiveYn = true;
                domainModelFormV3 = UpdateV3Status(domainModelFormV3);
                rowsAffected =  await  _repoUnit.FormV1Repository.UpdateFormV3(domainModelFormV3);
                
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public RmFormV3Hdr UpdateV3Status(RmFormV3Hdr form)
        {
            if (form.Fv3hPkRefNo > 0)
            {
                var existsObj = _repoUnit.FormV1Repository._context.RmFormV3Hdr.Where(x => x.Fv3hPkRefNo == form.Fv3hPkRefNo).Select(x => new { Status = x.Fv3hStatus, Log = x.Fv3hAuditLog }).FirstOrDefault();
                if (existsObj != null)
                {
                    form.Fv3hAuditLog = existsObj.Log;
                    // form.Fv3hStatus = existsObj.Status;

                }

            }
            if (form.Fv3hSubmitSts && form.Fv3hStatus == "Saved")
            {
                form.Fv3hStatus = Common.StatusList.FormW2Submitted;
                form.Fv3hAuditLog = Utility.ProcessLog(form.Fv3hAuditLog, "Submitted By", "Submitted", form.Fv3hUsernameRec, string.Empty, form.Fv3hDtRec, _security.UserName);
                processService.SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = _security.UserName,
                    RmNotGroup = GroupNames.OperationsExecutive,
                    RmNotMessage = "Recorded By:" + form.Fv3hUsernameRec + " - Form WN (" + form.Fv3hPkRefNo + ")",
                    RmNotOn = DateTime.Now,
                    RmNotUrl = "/MAM/EditFormv3?id=" + form.Fv3hPkRefNo.ToString(),
                    RmNotUserId = "",
                    RmNotViewed = ""
                }, true);
            }

            return form;
        }

        public async Task<int> UpdateFormV3Dtl(FormV3DtlGridDTO FormV3Dtl)
        {
            int? Fv1dPkRefNo = FormV3Dtl.Fv1dPkRefNo;
            int? Fv3hPkRefNo = FormV3Dtl.Fv3hPkRefNo;
            int Fv3dPkRefNo = FormV3Dtl.PkRefNo;
            var model = _mapper.Map<RmFormV3Dtl>(FormV3Dtl);
            model.Fv3dPkRefNo = Fv3dPkRefNo;
            model.Fv3dFv3hPkRefNo = Fv3hPkRefNo;
            model.Fv3dFv1dPkRefNo = Fv1dPkRefNo;
            return await _repo.UpdateFormV3Dtl(model);
        }


        public int? DeleteFormV3(int id)
        {
            int? rowsAffected;
            try
            {
                rowsAffected = _repo.DeleteFormV3(id);
            }
            catch (Exception ex)
            {
                _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }


        public int? DeleteFormV3Dtl(int id)
        {
            int? rowsAffected;
            try
            {
                rowsAffected = _repo.DeleteFormV3Dtl(id);
            }
            catch (Exception ex)
            {
                _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }





        #endregion

        #region FormV4


        public async Task<PagingResult<FormV4ResponseDTO>> GetFilteredFormV4Grid(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions)
        {
            PagingResult<FormV4ResponseDTO> result = new PagingResult<FormV4ResponseDTO>();

            List<FormV4ResponseDTO> formDList = new List<FormV4ResponseDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormV1Repository.GetFilteredV4RecordList(filterOptions);

                result.TotalRecords = await _repoUnit.FormV1Repository.GetFilteredV4RecordCount(filterOptions).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    var _ = _mapper.Map<FormV4ResponseDTO>(listData);

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



     
        public async Task<FormV4ResponseDTO> FindFormV4ByID(int id)
        {
            RmFormV4Hdr formV4 = await _repo.FindFormV4ByID(id);
            return _mapper.Map<FormV4ResponseDTO>(formV4);

        }

        public async Task<FormV4ResponseDTO> SaveFormV4(FormV4ResponseDTO Formv3)
        {
            try
            {
                return await _repo.SaveFormV4(Formv3);

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public async Task<int> UpdateV4(FormV4ResponseDTO FormV4)
        {
            int rowsAffected;
            try
            {
                int PkRefNo = FormV4.PkRefNo;

                var domainModelFormV4 = _mapper.Map<RmFormV4Hdr>(FormV4);
                domainModelFormV4.Fv4hPkRefNo = PkRefNo;
                domainModelFormV4.Fv4hActiveYn = true;
                domainModelFormV4 = UpdateV4Status(domainModelFormV4);
                rowsAffected = await _repoUnit.FormV1Repository.UpdateFormV4(domainModelFormV4);

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public RmFormV4Hdr UpdateV4Status(RmFormV4Hdr form)
        {
            if (form.Fv4hPkRefNo > 0)
            {
                var existsObj = _repoUnit.FormV1Repository._context.RmFormV4Hdr.Where(x => x.Fv4hPkRefNo == form.Fv4hPkRefNo).Select(x => new { Status = x.Fv4hStatus, Log = x.Fv4hAuditLog }).FirstOrDefault();
                if (existsObj != null)
                {
                    form.Fv4hAuditLog = existsObj.Log;
                    // form.Fv4hStatus = existsObj.Status;

                }

            }
            if (form.Fv4hSubmitSts && form.Fv4hStatus == "Saved")
            {
                form.Fv4hStatus = Common.StatusList.FormW2Submitted;
                form.Fv4hAuditLog = Utility.ProcessLog(form.Fv4hAuditLog, "Submitted By", "Submitted", form.Fv4hUsernameVet, string.Empty, form.Fv4hDtVet, _security.UserName);
                processService.SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = _security.UserName,
                    RmNotGroup = GroupNames.OperationsExecutive,
                    RmNotMessage = "Recorded By:" + form.Fv4hUsernameVet + " - Form WN (" + form.Fv4hPkRefNo + ")",
                    RmNotOn = DateTime.Now,
                    RmNotUrl = "/MAM/EditFormv4?id=" + form.Fv4hPkRefNo.ToString(),
                    RmNotUserId = "",
                    RmNotViewed = ""
                }, true);
            }

            return form;
        }


        public int? DeleteFormV4(int id)
        {
            int? rowsAffected;
            try
            {
                rowsAffected = _repo.DeleteFormV4(id);
            }
            catch (Exception ex)
            {
                _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }




        #endregion

        #region FormV5


        public async Task<PagingResult<FormV5ResponseDTO>> GetFilteredFormV5Grid(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions)
        {
            PagingResult<FormV5ResponseDTO> result = new PagingResult<FormV5ResponseDTO>();

            List<FormV5ResponseDTO> formDList = new List<FormV5ResponseDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormV1Repository.GetFilteredV5RecordList(filterOptions);

                result.TotalRecords = await _repoUnit.FormV1Repository.GetFilteredV5RecordCount(filterOptions).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    var _ = _mapper.Map<FormV5ResponseDTO>(listData);
                    // _.ProcessStatus = listData.FV5hStatus;

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


        public async Task<PagingResult<FormV5DtlResponseDTO>> GetFormV5DtlGridList(FilteredPagingDefinition<FormV5DtlResponseDTO> filterOptions, int V5PkRefNo)
        {
            PagingResult<FormV5DtlResponseDTO> result = new PagingResult<FormV5DtlResponseDTO>();

            List<FormV5DtlResponseDTO> formV5WorkScheduleList = new List<FormV5DtlResponseDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormV1Repository.GetFormv5DtlGridList(filterOptions, V5PkRefNo);

                result.TotalRecords = filteredRecords.Count();  // await _repoUnit.FormDRepository.GetFilteredRecordCount(filterOptions).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    FormV5DtlResponseDTO obj = new FormV5DtlResponseDTO();

                    obj.Fv5hPkRefNo = listData.Fv5dFv5hPkRefNo;
                    obj.PkRefNo = listData.Fv5dPkRefNo;
                    obj.FileNameFrm = listData.Fv5dFileNameFrm;
                    obj.FileNameTo = Convert.ToString(listData.Fv5dFileNameTo);
                    obj.Desc = Convert.ToString(listData.Fv5dDesc);
                    obj.ImageFilenameSys = Convert.ToString(listData.Fv5dImageFilenameSys);
                     

                    formV5WorkScheduleList.Add(obj);
                }

                result.PageResult = formV5WorkScheduleList;

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


        public async Task<FormV5ResponseDTO> SaveFormV5(FormV5ResponseDTO Formv5)
        {
            try
            {
                return await _repo.SaveFormV5(Formv5);

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public int? SaveFormV5Dtl(FormV5DtlResponseDTO FormV5Dtl)
        {

            try
            {
                var model = _mapper.Map<RmFormV5Dtl>(FormV5Dtl);
                model.Fv5dPkRefNo = 0;
                return _repo.SaveFormV5Dtl(model);
            }
            catch (Exception ex)
            {
                _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public Task<int> UpdateFormV5Dtl(FormV5DtlResponseDTO FormV5Dtl)
        {

            try
            {
                int? Fv5hPkRefNo = FormV5Dtl.Fv5hPkRefNo;
                var model = _mapper.Map<RmFormV5Dtl>(FormV5Dtl);
                model.Fv5dPkRefNo = 0;
                model.Fv5dFv5hPkRefNo = Fv5hPkRefNo;
                return _repo.UpdateFormV5Dtl(model);
            }
            catch (Exception ex)
            {
                _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public int? DeleteFormV5(int id)
        {
            int? rowsAffected;
            try
            {
                rowsAffected = _repo.DeleteFormV5(id);
            }
            catch (Exception ex)
            {
                _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }


        public int? DeleteFormV5Dtl(int id)
        {
            int? rowsAffected;
            try
            {
                rowsAffected = _repo.DeleteFormV5(id);
            }
            catch (Exception ex)
            {
                _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }


        public async Task<int> UpdateFormV5(FormV5ResponseDTO FormV5)
        {
            int rowsAffected;
            try
            {
                int PkRefNo = FormV5.PkRefNo;

                var domainModelFormV5 = _mapper.Map<RmFormV5Hdr>(FormV5);
                domainModelFormV5.Fv5hPkRefNo = PkRefNo;
                domainModelFormV5.Fv5hActiveYn = true;
                domainModelFormV5 = UpdateStatus(domainModelFormV5);
                await _repoUnit.FormV1Repository.UpdateFormV5(domainModelFormV5);
                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public RmFormV5Hdr UpdateStatus(RmFormV5Hdr form)
        {
            if (form.Fv5hPkRefNo > 0)
            {
                var existsObj = _repoUnit.FormV1Repository._context.RmFormV5Hdr.Where(x => x.Fv5hPkRefNo == form.Fv5hPkRefNo).Select(x => new { Status = x.Fv5hStatus, Log = x.Fv5hAuditLog }).FirstOrDefault();
                if (existsObj != null)
                {
                    form.Fv5hAuditLog = existsObj.Log;
                    // form.Fv5hStatus = existsObj.Status;

                }

            }
            if (form.Fv5hSubmitSts && form.Fv5hStatus == "Saved")
            {
                form.Fv5hStatus = Common.StatusList.FormW2Submitted;
                form.Fv5hAuditLog = Utility.ProcessLog(form.Fv5hAuditLog, "Submitted By", "Submitted", form.Fv5hUsernameRec, string.Empty, form.Fv5hDtRec, _security.UserName);
                processService.SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = _security.UserName,
                    RmNotGroup = GroupNames.OperationsExecutive,
                    RmNotMessage = "Recorded By:" + form.Fv5hUsernameRec + " - Form WN (" + form.Fv5hPkRefNo + ")",
                    RmNotOn = DateTime.Now,
                    RmNotUrl = "/MAM/EditFormV5?id=" + form.Fv5hPkRefNo.ToString(),
                    RmNotUserId = "",
                    RmNotViewed = ""
                }, true);
            }

            return form;
        }

        public async Task<FormV5ResponseDTO> FindFormV5ByID(int id)
        {
            RmFormV5Hdr formV5 = await _repo.FindFormV5ByID(id);
            return _mapper.Map<FormV5ResponseDTO>(formV5);

        }



        #endregion

    }
}
