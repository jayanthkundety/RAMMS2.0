using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;

namespace RAMMS.DTO.RequestBO
{
    public class FormQa2HeaderRequestDTO
    {
        public FormQa2HeaderRequestDTO()
        {
         
        }

        [MapTo("FqaiihPkRefNo")]
        public int No { get; set; }

        [MapTo("FqaiihRefId")]
        public string RefId { get; set; }
        [MapTo("FqaiihContNo")]
        public string ContNo { get; set; }
        [MapTo("FqaiihRoadCode")]
        public string RoadCode { get; set; }
        [MapTo("FqaiihRmu")]
        public string Rmu { get; set; }
        [MapTo("FqaiihRoadName")]
        public string RoadName { get; set; }
        [MapTo("FqaiihMonth")]
        public int? Month { get; set; }
        [MapTo("FqaiihYear")]
        public int? Year { get; set; }
        [MapTo("FqaiihSection")]
        public string Section { get; set; }
        [MapTo("FqaiihCrewSup")]
        public string CrewSup { get; set; }

        [MapTo("FqaiihCrewSupName")]
        public string CrewSupName { get; set; }

        [MapTo("FqaiihComments")]
        public string Comments { get; set; }
        [MapTo("FqaiihSignQaIni")]
        public string SignQaIni { get; set; }
        [MapTo("FqaiihUseridQaIni")]
        public int? UseridQaIni { get; set; }
        [MapTo("FqaiihUsernameQaIni")]
        public string UsernameQaIni { get; set; }
        [MapTo("FqaiihDesignationQaIni")]
        public string DesignationQaIni { get; set; }
        [MapTo("FqaiihRemarksQaIni")]
        public string RemarksQaIni { get; set; }
        [MapTo("FqaiihSignQaI")]
        public string SignQaI { get; set; }
        [MapTo("FqaiihUseridQaI")]
        public int? UseridQaI { get; set; }
        [MapTo("FqaiihUsernameQaI")]
        public string UsernameQaI { get; set; }
        [MapTo("FqaiihDesignationQaI")]
        public string DesignationQaI { get; set; }
        [MapTo("FqaiihRemarksQaI")]
        public string RemarksQaI { get; set; }
        [MapTo("FqaiihSignQaIi")]
        public string SignQaIi { get; set; }
        [MapTo("FqaiihUseridQaIi")]
        public int? UseridQaIi { get; set; }
        [MapTo("FqaiihUsernameQaIi")]
        public string UsernameQaIi { get; set; }
        [MapTo("FqaiihDesignationQaIi")]
        public string DesignationQaIi { get; set; }
        [MapTo("FqaiihRemarksQaIi")]
        public string RemarksQaIi { get; set; }
        [MapTo("FqaiihSignQaIii")]
        public string SignQaIii { get; set; }
        [MapTo("FqaiihUseridQaIii")]
        public int? UseridQaIii { get; set; }
        [MapTo("FqaiihUsernameQaIii")]
        public string UsernameQaIii { get; set; }
        [MapTo("FqaiihDesignationQaIii")]
        public string DesignationQaIii { get; set; }
        [MapTo("FqaiihRemarksQaIii")]
        public string RemarksQaIii { get; set; }
        [MapTo("FqaiihSignQaIv")]
        public string SignQaIv { get; set; }
        [MapTo("FqaiihUseridQaIv")]
        public int? UseridQaIv { get; set; }
        [MapTo("FqaiihUsernameQaIv")]
        public string UsernameQaIv { get; set; }
        [MapTo("FqaiihDesignationQaIv")]
        public string DesignationQaIv { get; set; }
        [MapTo("FqaiihRemarksQaIv")]
        public string RemarksQaIv { get; set; }
        [MapTo("FqaiihModBy")]
        public string ModBy { get; set; }
        [MapTo("FqaiihModDt")]
        public DateTime? ModDt { get; set; }
        [MapTo("FqaiihCrBy")]
        public string CrBy { get; set; }
        [MapTo("FqaiihCrDt")]
        public DateTime? CrDt { get; set; }
        [MapTo("FqaiihSubmitSts")]
        public bool SubmitSts { get; set; }
        [MapTo("FqaiihActiveYn")]
        public bool? ActiveYn { get; set; }

        [MapTo("FormN1Header")]
        public virtual ICollection<FormN1HeaderRequestDTO> FormN1Header { get; set; }
        [MapTo("FormQa2Detaill")]
        public virtual ICollection<FormQa2DtlRequestDTO> FormQa2Detail { get; set; }

        public string sortOrder { get; set; }
        public string currentFilter { get; set; }
        public string searchString { get; set; }
        public int? Page_No { get; set; }
        public int? pageSize { get; set; }
       
    }
}
