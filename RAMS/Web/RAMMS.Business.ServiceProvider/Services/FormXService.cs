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
using RAMMS.DTO.SearchBO;
using System.Linq;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class FormXService : IFormXService
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly IProcessService processService;
        private readonly ISecurity security;
        public FormXService(IRepositoryUnit repoUnit, IMapper mapper, IProcessService proService, ISecurity sec)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            processService = proService;
            security = sec;
        }

        public async Task<int> DeActivateFormXAsync(int formNo)
        {
            int rowsAffected;
            try
            {
                var domainModelFormX = await _repoUnit.FormXRepository.GetByIdAsync(formNo);
                domainModelFormX.FxhActiveYn = false;
                _repoUnit.FormXRepository.Update(domainModelFormX);

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> SaveFormXAsync(FormXHeaderRequestDTO FormXHeaderBO)
        {

            FormXHeaderRequestDTO formXRequest;
            try
            {
                var domainModelFormX = _mapper.Map<RmFormXHdr>(FormXHeaderBO);

                var entity = _repoUnit.FormXRepository.CreateReturnEntity(domainModelFormX);
                formXRequest = _mapper.Map<FormXHeaderRequestDTO>(entity);

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return int.Parse(formXRequest.No.ToString());
        }

        public async Task<FormXHeaderResponseDTO> SaveHeaderwithResponse(FormXHeaderRequestDTO headerReq)
        {
            FormXHeaderResponseDTO formX;

            int refCheckCount = await _repoUnit.FormXRepository.CheckwithRef(headerReq);
            if (refCheckCount != 0)
            {
                headerReq.No = refCheckCount;
                var currentFormX = await _repoUnit.FormXRepository.GetFormWithDetailsByNoAsync(Convert.ToInt32(headerReq.No)).ConfigureAwait(false);
                formX = _mapper.Map<FormXHeaderResponseDTO>(currentFormX);
            }
            else
            {

                var detail = _repoUnit.FormDDtlRepository.GetTop();

                var domainModelFormX = _mapper.Map<RmFormXHdr>(headerReq);
                if (detail != null)
                {
                    domainModelFormX.FxhFddPkRefNo = detail.FddPkRefNo;
                }

                _repoUnit.FormXRepository.Create(domainModelFormX);

                await _repoUnit.CommitAsync();

                formX = _mapper.Map<FormXHeaderResponseDTO>(domainModelFormX);

            }
            if (formX != null && formX.UseridAssgn.HasValue)
            {
                int iResult = processService.Save(new ProcessDTO()
                {
                    ApproveDate = DateTime.Now,
                    Form = "FormX",
                    IsApprove = true,
                    RefId = formX.No.Value,
                    Remarks = "",
                    Stage = StatusList.FormXInit
                }).Result;
            }
            if (formX != null && formX.WorkCompleted.HasValue)
            {
                int iResult = processService.Save(new ProcessDTO()
                {
                    ApproveDate = DateTime.Now,
                    Form = "FormX",
                    IsApprove = true,
                    RefId = formX.No.Value,
                    Remarks = "",
                    Stage = StatusList.FormXWorkCompleted
                }).Result;
            }
            return formX;
        }

        public async Task<List<string>> GetSectionByRMU(string rmu)
        {
            var data = await _repoUnit.FormARepository.GetSectionByRMU(rmu).ConfigureAwait(false);

            return data;

        }

        public async Task<PagingResult<FormXHeaderResponseDTO>> GetFilteredFormXGrid(FilteredPagingDefinition<FormXSearchDTO> filterOptions)
        {
            PagingResult<FormXHeaderResponseDTO> result = new PagingResult<FormXHeaderResponseDTO>();

            List<FormXHeaderResponseDTO> formAList = new List<FormXHeaderResponseDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormXRepository.GetFilteredRecordList(filterOptions);

                result.TotalRecords = await _repoUnit.FormXRepository.GetFilteredRecordCount(filterOptions).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    formAList.Add(_mapper.Map<FormXHeaderResponseDTO>(listData));
                }

                result.PageResult = formAList;

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

        public async Task<FormXHeaderRequestDTO> GetFormXWithDetailsByNoAsync(int formNo)
        {
            FormXHeaderRequestDTO formX;
            try
            {
                var currentFormX = await _repoUnit.FormXRepository.GetFormWithDetailsByNoAsync(formNo).ConfigureAwait(false);
                formX = _mapper.Map<FormXHeaderRequestDTO>(currentFormX);

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return formX;
        }

        public async Task<int> UpdateFormXAsync(FormXHeaderRequestDTO fornmXDtlDTO)
        {
            int rowsAffected;
            try
            {
                var domainModelformX = _mapper.Map<RmFormXHdr>(fornmXDtlDTO);
                var exists = _repoUnit._context.RmFormXHdr.Where(x => x.FxhPkRefNo == domainModelformX.FxhPkRefNo).Select(x => new
                {
                    FxhUseridAssgn = x.FxhUseridAssgn,
                    FxhWrkCmpld = x.FxhWrkCmpld,
                    FxhUseridVer = x.FxhUseridVer,
                    FxhUseridVet = x.FxhUseridVet
                }).FirstOrDefault();
                var d = _repoUnit.FormDDtlRepository.GetTop();
                domainModelformX.FxhFddPkRefNo = d.FddPkRefNo;
                _repoUnit.FormXRepository.Update(domainModelformX);

                rowsAffected = await _repoUnit.CommitAsync();
                if (fornmXDtlDTO != null && exists != null && ((!exists.FxhUseridAssgn.HasValue && fornmXDtlDTO.UseridAssgn.HasValue) || (fornmXDtlDTO.UseridAssgn.HasValue && exists.FxhUseridAssgn != fornmXDtlDTO.UseridAssgn)))
                {
                    int iResult = processService.Save(new ProcessDTO()
                    {
                        ApproveDate = DateTime.Now,
                        Form = "FormX",
                        IsApprove = true,
                        RefId = fornmXDtlDTO.No.Value,
                        Remarks = "",
                        Stage = StatusList.FormXInit
                    }).Result;
                }
                if (fornmXDtlDTO != null && exists != null && ((!exists.FxhWrkCmpld.HasValue && fornmXDtlDTO.WorkCompleted.HasValue) || (fornmXDtlDTO.WorkCompleted.HasValue && exists.FxhWrkCmpld != fornmXDtlDTO.WorkCompleted)))
                {
                    int iResult = processService.Save(new ProcessDTO()
                    {
                        ApproveDate = DateTime.Now,
                        Form = "FormX",
                        IsApprove = true,
                        RefId = fornmXDtlDTO.No.Value,
                        Remarks = "",
                        Stage = StatusList.FormXWorkCompleted
                    }).Result;
                }

                if (fornmXDtlDTO != null && exists != null && ((!exists.FxhUseridVer.HasValue && fornmXDtlDTO.UseridVer.HasValue) || (fornmXDtlDTO.UseridVer.HasValue && exists.FxhUseridVer != fornmXDtlDTO.UseridVer)))
                {
                    int iResult = processService.Save(new ProcessDTO()
                    {
                        ApproveDate = DateTime.Now,
                        Form = "FormX",
                        IsApprove = true,
                        RefId = fornmXDtlDTO.No.Value,
                        Remarks = "",
                        Stage = StatusList.FormXVerified
                    }).Result;
                }
                if (fornmXDtlDTO != null && exists != null && ((!exists.FxhUseridVet.HasValue && fornmXDtlDTO.UseridVet.HasValue) || (fornmXDtlDTO.UseridVet.HasValue && exists.FxhUseridVet != fornmXDtlDTO.UseridVet)))
                {
                    int iResult = processService.Save(new ProcessDTO()
                    {
                        ApproveDate = DateTime.Now,
                        Form = "FormX",
                        IsApprove = true,
                        RefId = fornmXDtlDTO.No.Value,
                        Remarks = "",
                        Stage = StatusList.FormXVetted
                    }).Result;
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> SaveFormXWarImage(List<WarImageDtlRequestDTO> warImage)
        {
            int rowsAffected;
            try
            {
                var domainModelFormX = new List<RmWarImageDtl>();
                var domainModelFormD = new List<RmWarImageDtl>();
                foreach (var list in warImage)
                {
                    domainModelFormX.Add(_mapper.Map<RmWarImageDtl>(list));
                    var formXRefNo = domainModelFormX[0].FwarFxhPkRefNo;
                    var formDDtlList = await _repoUnit.FormDDtlRepository.FindAllAsync(x => x.FddFxhPkRefNo == formXRefNo && x.FddActiveYn == true);
                    foreach (var item in formDDtlList)
                    {
                        list.HeaderId = item.FddPkRefNo;
                        list.FxhPkRefNo = null;
                        domainModelFormD.Add(_mapper.Map<RmWarImageDtl>(list));
                    }
                }
                _repoUnit.FormXRepository.SaveWarImage(domainModelFormD);
                _repoUnit.FormXRepository.SaveWarImage(domainModelFormX);
                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> SaveFormXAccUccDtl(List<AccUccImageDtlRequestDTO> accUccImage)
        {
            int rowsAffected;
            try
            {
                var domainModelFormX = new List<RmAccUcuImageDtl>();
                var domainModelFormD = new List<RmAccUcuImageDtl>();
                foreach (var list in accUccImage)
                {
                    domainModelFormX.Add(_mapper.Map<RmAccUcuImageDtl>(list));
                    var formXRefNo = domainModelFormX[0].FauFxhPkRefNo;
                    var formDDtlList = await _repoUnit.FormDDtlRepository.FindAllAsync(x => x.FddFxhPkRefNo == formXRefNo && x.FddActiveYn == true);
                    foreach (var item in formDDtlList)
                    {
                        list.HeaderId = item.FddPkRefNo;
                        list.FxhPkRefNo = null;
                        domainModelFormD.Add(_mapper.Map<RmAccUcuImageDtl>(list));
                    }

                }
                _repoUnit.FormXRepository.SaveAccUccImage(domainModelFormX);
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
                var domainModelFormX = await _repoUnit.FormXRepository.GetWarImageByIdAsync(warId);
                var domainModelFormD = new List<RmWarImageDtl>();
                var imageList = await _repoUnit.FormXRepository.GetWarImageListAsync(domainModelFormX);
                foreach (var item in imageList)
                {
                    item.FwarActiveYn = false;
                    domainModelFormD.Add(item);
                }
                domainModelFormX.FwarActiveYn = false;
                _repoUnit.FormXRepository.UpdateWarList(domainModelFormD);
                _repoUnit.FormXRepository.UpdateWar(domainModelFormX);

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
                var domainModelFormX = await _repoUnit.FormXRepository.GetAccUccImageById(accUccId);
                var domainModelFormD = new List<RmAccUcuImageDtl>();
                var fileList = await _repoUnit.FormXRepository.GetAccUcuImageListAsync(domainModelFormX);
                foreach (var item in fileList)
                {
                    item.FauActiveYn = false;
                    domainModelFormD.Add(item);
                }

                _repoUnit.FormXRepository._context.RmAccUcuImageDtl.UpdateRange(domainModelFormD);
                domainModelFormX.FauActiveYn = false;
                _repoUnit.FormXRepository.UpdateAccUcu(domainModelFormX);

                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<List<WarImageDtlResponseDTO>> GetWarImageList(int formXId)
        {
            List<WarImageDtlResponseDTO> warImages = new List<WarImageDtlResponseDTO>();
            try
            {
                var getList = await _repoUnit.FormXRepository.GetWarImagelist(formXId);
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

        public async Task<List<AccUccImageDtlResponseDTO>> GetAccUccImageList(int formXId)
        {
            List<AccUccImageDtlResponseDTO> warImages = new List<AccUccImageDtlResponseDTO>();
            try
            {
                var getList = await _repoUnit.FormXRepository.GetAccUccImagelist(formXId);
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

        public async Task<AccUccImageDtlResponseDTO> GetAccUccImage(int formXId)
        {
            AccUccImageDtlResponseDTO warImages = new AccUccImageDtlResponseDTO();
            try
            {
                var getList = await _repoUnit.FormXRepository.GetAccUccImageById(formXId);
                warImages = _mapper.Map<AccUccImageDtlResponseDTO>(getList);
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return warImages;
        }
        public async Task<WarImageDtlResponseDTO> GetWarDocById(int formXId)
        {
            WarImageDtlResponseDTO warImages = new WarImageDtlResponseDTO();
            try
            {
                var getList = await _repoUnit.FormXRepository.GetWarDocById(formXId);
                warImages = _mapper.Map<WarImageDtlResponseDTO>(getList);
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

            int imageCt = await _repoUnit.FormXRepository.GetWARId(hederId, type);
            return imageCt;
        }
        public async Task<int> LastInsertedUCUSRNO(int hederId, string type)
        {

            int imageCt = await _repoUnit.FormXRepository.GetUCUId(hederId, type);
            return imageCt;
        }

        public async Task<List<RmWarImageDtl>> GetFilteredWARImagesFormX(int primaryNo)
        {
            return await _repoUnit.FormXRepository.GetFilteredWARImagesFormX(primaryNo);
        }

        public async Task<int> GetWARTypeCodeCount(string type, int id)
        {
            return await _repoUnit.FormXRepository.GetWARTypeCodeCount(type, id);
        }

        public (int id, bool isExists) CheckExistence(string rdCode, int month, int year, DateTime rpDate)
        {
            return _repoUnit.FormXRepository.CheckExistence(rdCode, month, year, rpDate);
        }

        public async Task<string> GetSectionByRoadCodeAndRMU(string rdCode, string rmu)
        {
            return await _repoUnit.FormXRepository.GetSectionByRoadCodeAndRMU(rdCode, rmu);
        }
    }
}
