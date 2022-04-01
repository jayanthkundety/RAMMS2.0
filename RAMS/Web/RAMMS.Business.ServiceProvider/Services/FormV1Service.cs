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


        public async Task<PagingResult<FormV1ResponseDTO>> GetFilteredFormV1Grid(FilteredPagingDefinition<FormV1SearchGridDTO> filterOptions)
        {
            PagingResult<FormV1ResponseDTO> result = new PagingResult<FormV1ResponseDTO>();

            List<FormV1ResponseDTO> formDList = new List<FormV1ResponseDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormV1Repository.GetFilteredRecordList(filterOptions);

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





        //public async Task<int> SaveFormWD(FormWDResponseDTO FormWD)
        //{
        //    FormWDResponseDTO formWDResponse;
        //    try
        //    {
        //        var domainModelFormWD = _mapper.Map<RmIwFormWd>(FormWD);
        //        domainModelFormWD.FwdPkRefNo = 0;
        //        var entity = _repoUnit.FormWDRepository.CreateReturnEntity(domainModelFormWD);
        //        formWDResponse = _mapper.Map<FormWDResponseDTO>(entity);
        //        return formWDResponse.PkRefNo;
        //    }
        //    catch (Exception ex)
        //    {
        //        await _repoUnit.RollbackAsync();
        //        throw ex;
        //    }
        //}

        //public async Task<int> DeActivateFormWD(int formNo)
        //{
        //    int rowsAffected;
        //    try
        //    {
        //        var domainModelFormWD = await _repoUnit.FormWDRepository.GetByIdAsync(formNo);
        //        domainModelFormWD.FwdActiveYn = false;
        //        _repoUnit.FormWDRepository.Update(domainModelFormWD);
        //        rowsAffected = await _repoUnit.CommitAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        await _repoUnit.RollbackAsync();
        //        throw ex;
        //    }

        //    return rowsAffected;
        //}

    }
}
