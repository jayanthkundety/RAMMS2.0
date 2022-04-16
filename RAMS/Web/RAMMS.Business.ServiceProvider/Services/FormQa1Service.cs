using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Business.ServiceProvider.Interfaces;
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

    }
}
