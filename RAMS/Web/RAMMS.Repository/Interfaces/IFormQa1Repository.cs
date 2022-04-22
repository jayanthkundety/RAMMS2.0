using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormQa1Repository : IRepositoryBase<RmFormQa1Hdr>
    {
        Task<List<RmFormQa1Hdr>> GetFilteredRecordList(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions);
        Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions);

        Task<RmFormQa1Hdr> FindSaveFormQa1Hdr(RmFormQa1Hdr formQa1Header, bool updateSubmit);        

        Task<int> GetFilteredEqpRecordCount(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id);

        Task<List<RmFormQa1EqVh>> GetFilteredEqpRecordList(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id);

        Task<int> GetFilteredMatRecordCount(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id);

        Task<List<RmFormQa1Mat>> GetFilteredMatRecordList(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id);

        Task<int> GetFilteredGenRecordCount(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id);

        Task<List<RmFormQa1Gen>> GetFilteredGenRecordList(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id);

        Task<RmFormQa1Gen> GetGenDetails(int pkRefNo);

        Task<RmFormQa1EqVh> GetEquipDetails(int pkRefNo);

        Task<RmFormQa1Mat> GetMatDetails(int pkRefNo);

        Task<RmFormQa1Hdr> GetFormQA1(int pkRefNo);

        Task<RmFormQa1Lab> SaveLabour(RmFormQa1Lab labour);

        Task<RmFormQa1Lab> GetLabourDetails(int pkRefNo);
        
    }
}
