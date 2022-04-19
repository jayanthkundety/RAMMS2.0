using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Business.ServiceProvider.Interfaces;
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

        public FormQa1Service(IRepositoryUnit repoUnit, IMapper mapper)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
            var obj =  _repoUnit.FormQa1Repository.FindAsync(x =>  x.Fqa1hPkRefNo == pkRefNo && x.Fqa1hActiveYn == true).Result;
            return _mapper.Map<FormQa1HeaderDTO>(obj);
        }

        public async Task<FormQa1HeaderDTO> FindAndSaveFormQA1Hdr(FormQa1HeaderDTO header, bool updateSubmit)
        {
            var formQa1 = _mapper.Map<RmFormQa1Hdr>(header);
            formQa1.Fqa1hPkRefNo = header.PkRefNo;
            var response = await _repoUnit.FormQa1Repository.FindSaveFormQa1Hdr(formQa1, updateSubmit);
            return _mapper.Map<FormQa1HeaderDTO>(response);
        }

        public async Task<List<FormQa1LabDTO>> InsertLabourDetails(int qa1Id)
        {
            List<FormQa1LabDTO> _input = new List<FormQa1LabDTO>();
            List<FormQa1LabDTO> result = new List<FormQa1LabDTO>();
            FormQa1LabDTO labDTO = new FormQa1LabDTO();
            labDTO.Labour = "Crew Supervisor";
            _input.Add(labDTO);
            labDTO = new FormQa1LabDTO();
            labDTO.Labour = "Operator";
            _input.Add(labDTO);
            labDTO = new FormQa1LabDTO();
            labDTO.Labour = "Driver";
            _input.Add(labDTO);
            labDTO = new FormQa1LabDTO();
            labDTO.Labour = "Workmates";
            _input.Add(labDTO);
            labDTO = new FormQa1LabDTO();
            labDTO.Labour = "Others";
            _input.Add(labDTO);

            foreach (var lab in _input)
            {
                lab.Fqa1hPkRefNo = qa1Id;
                var obj = _mapper.Map<RmFormQa1Lab>(lab);
                var labourctx = await _repoUnit.FormQa1Repository.SaveLabour(obj);
                var labour = _mapper.Map<FormQa1LabDTO>(labourctx);
                result.Add(labour);
            }

            return result;
        }

        public async Task<PagingResult<FormQa1EqVhDTO>> GetEquipmentFormQa1Grid(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id)
        {
            PagingResult <FormQa1EqVhDTO > result = new PagingResult<FormQa1EqVhDTO>();

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
    }
}
