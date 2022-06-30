using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormF1DtlResponseDTO
    {
        public int PkRefNo { get; set; }
        public int Ff1hPkRefNo { get; set; }
        
        public string AssetId { get; set; }
        public int? LocCh { get; set; }
        public int? LocChDeci { get; set; }
        public string Code { get; set; }
        public int? Tier { get; set; }
        public decimal? TotalLength { get; set; }
        public decimal? TopWidth { get; set; }
        public decimal? BottomWidth { get; set; }
        public decimal? Height { get; set; }
        public int? OverallCondition { get; set; }
        public string Description { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }


    }
}
