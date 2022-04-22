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

        public async Task<FormQa1LabDTO> SaveLabour(FormQa1LabDTO labDTO)
        {
            var lab = _mapper.Map<RmFormQa1Lab>(labDTO);
            var labourctx = await _repoUnit.FormQa1Repository.SaveLabour(lab);
            var labour = _mapper.Map<FormQa1LabDTO>(labourctx);
            return labour;
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
                response.Ssc = _mapper.Map<FormQa1SscDTO>(_formQA1.RmFormQa1Ssc);

            response.Tes = new FormQa1TesDTO();
            if (_formQA1.RmFormQa1Tes.Count > 0)
                response.Tes = _mapper.Map<FormQa1TesDTO>(_formQA1.RmFormQa1Tes);

            response.Wcq = new FormQa1WcqDTO();
            if (_formQA1.RmFormQa1Wcq.Count > 0)
                response.Wcq = _mapper.Map<FormQa1WcqDTO>(_formQA1.RmFormQa1Wcq);

            response.We = new FormQa1WeDTO();
            if (_formQA1.RmFormQa1We.Count > 0)
                response.We = _mapper.Map<FormQa1WeDTO>(_formQA1.RmFormQa1We);

            response.Gc = new FormQa1GCDTO();
            if (_formQA1.RmFormQa1Gc.Count > 0)
                response.Gc = _mapper.Map<FormQa1GCDTO>(_formQA1.RmFormQa1Gc);

            response.Lab = new FormQa1LabDTO();
            if (_formQA1.RmFormQa1Lab.Count > 0)
                response.Lab = _mapper.Map<FormQa1LabDTO>(_formQA1.RmFormQa1Lab);

            return response;
        }

        public async Task<FormQa1LabDTO> GetLabourDetails(int pkRefNo)
        {
            var response = await _repoUnit.FormQa1Repository.GetLabourDetails(pkRefNo);
            return _mapper.Map<FormQa1LabDTO>(response);
        }
    }
}
