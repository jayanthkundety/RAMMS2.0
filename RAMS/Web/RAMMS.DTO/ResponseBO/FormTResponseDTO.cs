using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormTResponseDTO
    {

        public int PkRefNo { get; set; }
        public string PkRefId { get; set; }
        public string RmuCode { get; set; }
        public string DivCode { get; set; }
        public string SecCode { get; set; }
        public string SecName { get; set; }
        public string RdCode { get; set; }
        public string RdName { get; set; }
        public string ReferenceNo { get; set; }
        public string DirectionFrm { get; set; }
        public string DirectionTo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? InspectionDate { get; set; }
        public DateTime? AuditTimeFrm { get; set; }
        public DateTime? AuditTimeTo { get; set; }
        public int? PcsTotal { get; set; }
        public int? HvTotal { get; set; }
        public int? McTotal { get; set; }
        public bool? SignRcd { get; set; }
        public int UseridRcd { get; set; }
        public string UsernameRcd { get; set; }
        public string DesignationRcd { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateRcd { get; set; }
        public bool? SignHdd { get; set; }
        public int UseridHdd { get; set; }
        public string UsernameHdd { get; set; }
        public string DesignationHdd { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateHdd { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool? SubmitSts { get; set; }
        public bool? ActiveYn { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }

        public bool FormExist { get; set; }

    }
}
