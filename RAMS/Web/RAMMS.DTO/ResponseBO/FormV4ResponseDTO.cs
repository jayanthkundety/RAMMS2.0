using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormV4ResponseDTO
    {


        public int PkRefNo { get; set; }
        public int? FV3PKRefNo { get; set; }
        public int? ContNo { get; set; }
        public string FV3PKRefID { get; set; }
     
        public string Rmu { get; set; }
        public string RmuName { get; set; }
        public string Division { get; set; }
        public int? Crew { get; set; }
        public string Crewname { get; set; }
        public string SecCode { get; set; }
        public string SecName { get; set; }
        public string RefId { get; set; }
        public string ActCode { get; set; }
        public string ActName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Dt { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public decimal? TotalProduction { get; set; }
        public string Remarks { get; set; }
        public bool SignFac { get; set; }
        public int? UseridFac { get; set; }
        public string UsernameFac { get; set; }
        public string DesignationFac { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DtFac { get; set; }
        public bool SignAgr { get; set; }
        public int? UseridAgr { get; set; }
        public string UsernameAgr { get; set; }
        public string DesignationAgr { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DtAgr { get; set; }
        public bool SignVet { get; set; }
        public int? UseridVet { get; set; }
        public string UsernameVet { get; set; }
        public string DesignationVet { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DtVet { get; set; }
        public string ServiceProvider { get; set; }
        public string Verifier { get; set; }
        public string Facilitator { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }

        

      
    }
}
