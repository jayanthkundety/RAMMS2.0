using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public  class FormV5ResponseDTO
    {
       
        public int PkRefNo { get; set; }
        public int? Fv4PkRefNo { get; set; }
        public int? ContNo { get; set; }
        public string Rmu { get; set; }
        public string RmuName { get; set; }
        public int? Crew { get; set; }
        public string Crewname { get; set; }
        public string SecCode { get; set; }
        public string SecName { get; set; }
        public string Division { get; set; }
        public string RefId { get; set; }
        public string ActCode { get; set; }
        public string ActName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime? Dt { get; set; }
        public int? Year { get; set; }
        public bool SignRec { get; set; }
        public int? UseridRec { get; set; }
        public string UsernameRec { get; set; }
        public string DesignationRec { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime? DtRec { get; set; }
        public string Verifier { get; set; }
        public string Remarks { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }
        public bool FormExist { get; set; }
        
    }
}
