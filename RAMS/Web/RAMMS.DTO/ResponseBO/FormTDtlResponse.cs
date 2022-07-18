using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormTDtlResponseDTO
    {
        public int PkRefNo { get; set; }
        public int FmtPkRefNo { get; set; }
        public DateTime? InspectionDate { get; set; }
        public string AuditTimeFrm { get; set; }
        public string AuditTimeTo { get; set; }
        public string DirectionFrm { get; set; }
        public string DirectionTo { get; set; }
        public int? Day { get; set; }
        public int? TotalDay { get; set; }
        public int? HourlyCountPerDay { get; set; }
        public int? PcsSubTotal { get; set; }
        public int? HvSubTotal { get; set; }
        public int? McSubTotal { get; set; }
        public string Description { get; set; }
        public string DescriptionPC { get; set; }
        public string DescriptionHV { get; set; }
        public string DescriptionMC { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public List<FormTVehicleResponseDTO> Vechicles { get; set; }

    }
}
