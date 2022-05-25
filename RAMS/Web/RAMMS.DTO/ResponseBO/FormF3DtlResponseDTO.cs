using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormF3DtlResponseDTO
    {
        public int PkRefNo { get; set; }
        public int? Ff3hPkRefNo { get; set; }
        public int? RdmFrmCh { get; set; }
        public int? RdmFrmChDeci { get; set; }
        public int? RdmToCh { get; set; }
        public int? RdmToChDeci { get; set; }
        public string Code { get; set; }
        public int? ConditionI { get; set; }
        public int? ConditionIi { get; set; }
        public int? ConditionIii { get; set; }
        public string Side { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public string Description { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }

         
    }
}
