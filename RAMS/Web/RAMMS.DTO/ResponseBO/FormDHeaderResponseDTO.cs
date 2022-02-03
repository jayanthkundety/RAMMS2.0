using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;

namespace RAMMS.DTO.ResponseBO
{
    public class FormDHeaderResponseDTO
    {
        public FormDHeaderResponseDTO()
        {
         
        }

        [MapTo("FdhPkRefNo")] // table primary keyC:\Users\mohan\Source\Repos\RAMMS\RAMMS.DTO\RequestBO\RoadMasterRequestDTO.cs
        public int No { get; set; }

        [MapTo("FdhContNo")]
        public int? ContNo { get; set; }  // Contract no

        [MapTo("FdhRmu")]
        public string Rmu { get; set; }

        [MapTo("FdhRoadCode")]
        public string RoadCode { get; set; }

        [MapTo("FdhDivName")]
        public string DivisionName { get; set; }

        [MapTo("FdhWeekNo")]
        public int? WeekNo { get; set; }

        [MapTo("FdhMonth")]
        public int? Month { get; set; }

        [MapTo("FdhDay")]
        public string Day { get; set; }

        [MapTo("FdhYear")]
        public int? Year { get; set; }

        [MapTo("FdhSn")]
        public string SerialNo { get; set; }

        [MapTo("FdhSignPrp")] // Reported by 
        public string ReportedBySign { get; set; }

        [MapTo("FdhUseridPrp")] //Reported by
        public int? ReportedByUserId { get; set; }

        [MapTo("FdhUsernamePrp")]
        public string ReportedByUsername { get; set; }

        [MapTo("FdhDesignationPrp")]
        public string ReportedByDesignation { get; set; }

        [MapTo("FdhDtPrp")]
        public DateTime? DateReported { get; set; }

        [MapTo("FdhSignVer")]
        public string SignVer { get; set; }

        [MapTo("FdhUseridVer")]
        public string  UseridVer { get; set; }

        [MapTo("FdhUsernameVer")]
        public string UsernameVer { get; set; }

        [MapTo("FdhDesignationVer")]
        public string DesignationVer { get; set; }

        [MapTo("FdhDtVer")]
        public DateTime? DtVer { get; set; }

        [MapTo("FdhSignVet")]
        public string SignVet { get; set; }

        [MapTo("FdhUseridVet")]
        public string UseridVet { get; set; }

        [MapTo("FdhUsernameVet")]
        public string UsernameVet { get; set; }

        [MapTo("FdhDesignationVet")]
        public string DesignationVet { get; set; }

        [MapTo("FdhDtVet")]
        public DateTime? DtVet { get; set; }

        [MapTo("FdhSubAuthSts")]
        public string SubAuthStatus { get; set; }

        [MapTo("FdhDtSubAuth")]
        public DateTime? DateSubAuth { get; set; }

        [MapTo("FdhRcvdAuthSts")]
        public string RcvdAuthStatus { get; set; }

        [MapTo("FdhDtRcvdAuth")]
        public DateTime? DateRcvdAuth { get; set; }

        [MapTo("FdhVetAuthSts")]
        public string VetAuthStatus { get; set; }

        [MapTo("FdhDtVetAuth")]
        public DateTime? DateVetAuth { get; set; }


        [MapTo("FdhModBy")]
        public string ModifeidBy { get; set; }

        [MapTo("FdhModDt")]
        public DateTime? ModifiedDate { get; set; }

        [MapTo("FdhCrBy")]
        public string CreatedBy { get; set; }

        [MapTo("FdhCrDt")]
        public DateTime? CreatedDate { get; set; }

        [MapTo("FdhSubmitSts")]
        public bool SubmitSts { get; set; }

        [MapTo("FdhActiveYn")]
        public bool? ActiveYn { get; set; }

        [MapTo("FdhRefId")]
        public string ReferenceID { get; set; }

        [MapTo("FdhCrewUnit")]
        public string CrewUnit { get; set; }

        [MapTo("FdhCrewSupName")]
        public string CrewUnitName { get; set; }

        [MapTo("FdhSignVerSo")]
        public string SignVerSo { get; set; }

        [MapTo("FdhUseridVerSo")]
        public string UseridVerSo { get; set; }

        [MapTo("FdhUsernameVerSo")]
        public string UsernameVerSo { get; set; }

        [MapTo("FdhDesignationVerSo")]
        public string DesignationVerSo { get; set; }

        [MapTo("FdhDtVerSo")]
        public DateTime? DtVerSo { get; set; }
        [MapTo("FdhSignPrcdSo")]
        public string SignPrcdSo { get; set; }

        [MapTo("FdhUseridPrcdSo")]
        public string UseridPrcdSo { get; set; }

        [MapTo("FdhUsernamePrcdSo")]
        public string UsernamePrcdSo { get; set; }

        [MapTo("FdhDesignationPrcdSo")]
        public string DesignationPrcdSo { get; set; }

        [MapTo("FdhDtPrcdSo")]
        public DateTime? DtPrcdSo { get; set; }

        [MapTo("FdhSignAgrdSo")]
        public string SignAgrdSo { get; set; }

        [MapTo("FdhUseridAgrdSo")]
        public string UseridAgrdSo { get; set; }

        [MapTo("FdhUsernameAgrdSo")]
        public string UsernameAgrdSo { get; set; }

        [MapTo("FdhDesignationAgrdSo")]
        public string DesignationAgrdSo { get; set; }

        [MapTo("FdhDtAgrdSo")]
        public DateTime? DtAgrdSo { get; set; }

        [MapTo("FdhStatus")]
        public string Status { get; set; }
        [MapTo("FdhAuditLog")]
        public string AuditLog { get; set; }

        [MapTo("FdhDate")]
        public DateTime? WeekDate { get; set; }
        public List<FormDDetailsResponseDTO > FormDDetails { get; set; }

        public List<FormDMaterialDetailsResponseDTO> FormDMaterial { get; set; }

        public List<FormDEquipDetailsResponseDTO> FormDEquip { get; set; }

        public List<FormDLabourDetailsResponseDTO> FormDLabour { get; set; }
        public string sortOrder { get; set; }
        public string currentFilter { get; set; }
        public string searchString { get; set; }
        public int? Page_No { get; set; }
        public int? pageSize { get; set; }

        public string ProcessStatus { get; set; }

    }
}
