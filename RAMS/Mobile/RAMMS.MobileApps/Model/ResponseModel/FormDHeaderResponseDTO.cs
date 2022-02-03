using System;
using System.Collections.Generic;

namespace RAMMS.DTO.RequestBO
{
    public class FormDHeaderResponseDTO
    {
        public FormDHeaderResponseDTO()
        {
         
        }

        
        public int No { get; set; }

        
        public int? ContNo { get; set; }  // Contract no

        
        public string Rmu { get; set; }

        
        public string RoadCode { get; set; }

        
        public string DivisionName { get; set; }

        
        public int? WeekNo { get; set; }

        
        public int? Month { get; set; }

        
        public string Day { get; set; }

        
        public int? Year { get; set; }

        
        public string SerialNo { get; set; }

        
        public string ReportedBySign { get; set; }

        
        public int? ReportedByUserId { get; set; }

        
        public string ReportedByUsername { get; set; }

        
        public string ReportedByDesignation { get; set; }

        
        public DateTime? DateReported { get; set; }

        
        public string SignVer { get; set; }

        
        public string  UseridVer { get; set; }

        
        public string UsernameVer { get; set; }

        
        public string DesignationVer { get; set; }

        
        public DateTime? DtVer { get; set; }

        
        public string SignVet { get; set; }

        
        public string UseridVet { get; set; }

        
        public string UsernameVet { get; set; }

        
        public string DesignationVet { get; set; }

        
        public DateTime? DtVet { get; set; }

        
        public string SubAuthStatus { get; set; }

        
        public DateTime? DateSubAuth { get; set; }

        
        public string RcvdAuthStatus { get; set; }

        
        public DateTime? DateRcvdAuth { get; set; }

        
        public string VetAuthStatus { get; set; }

        
        public DateTime? DateVetAuth { get; set; }


        
        public string ModifeidBy { get; set; }

        
        public DateTime? ModifiedDate { get; set; }

        
        public string CreatedBy { get; set; }

        
        public DateTime? CreatedDate { get; set; }

        
        public bool SubmitSts { get; set; }

        
        public bool? ActiveYn { get; set; }

        
        public string ReferenceID { get; set; }

        
        public string CrewUnitName { get; set; }

        
        public string SignVerSo { get; set; }

        
        public string UseridVerSo { get; set; }

        
        public string UsernameVerSo { get; set; }

        
        public string DesignationVerSo { get; set; }

        
        public DateTime? DtVerSo { get; set; }
        
        public string SignPrcdSo { get; set; }

        
        public string UseridPrcdSo { get; set; }

        
        public string UsernamePrcdSo { get; set; }

        
        public string DesignationPrcdSo { get; set; }

        
        public DateTime? DtPrcdSo { get; set; }

        
        public string SignAgrdSo { get; set; }

        
        public string UseridAgrdSo { get; set; }

        
        public string UsernameAgrdSo { get; set; }

        
        public string DesignationAgrdSo { get; set; }

        
        public DateTime? DtAgrdSo { get; set; }

        public List<FormDDetailsResponseDTO > FormDDetails { get; set; }

        public List<FormDMaterialDetailsResponseDTO> FormDMaterial { get; set; }

        public List<FormDEquipDetailsResponseDTO> FormDEquip { get; set; }

        public List<FormDLabourDetailsResponseDTO> FormDLabour { get; set; }
        public string sortOrder { get; set; }
        public string currentFilter { get; set; }
        public string searchString { get; set; }
        public int? Page_No { get; set; }
        public int? pageSize { get; set; }
       
    }
}
