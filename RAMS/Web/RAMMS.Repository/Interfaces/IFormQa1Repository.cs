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

        Task<RmFormQa1Lab> GetLabourDetails(int pkRefNo);

        int? SaveMaterial(RmFormQa1Mat formQa1Mat);

        int? DeleteMaterial(int id);

        int? SaveGeneral(RmFormQa1Gen formQa1Gen);

        int? DeleteGeneral(int id);

        int? SaveEquipment(RmFormQa1EqVh formQa1EqVh);

        int? DeleteEquipment(int id);

        Task<RmFormQa1Wcq> SaveWCQ(RmFormQa1Wcq form);

        Task<RmFormQa1We> SaveWE(RmFormQa1We form);

        Task<RmFormQa1Lab> SaveLabour(RmFormQa1Lab form);

        Task<RmFormQa1Gc> SaveGC(RmFormQa1Gc form);

        Task<RmFormQa1Tes> SaveTES(RmFormQa1Tes form);

        Task<RmFormQa1Ssc> SaveSSC(RmFormQa1Ssc form);



    }
}
