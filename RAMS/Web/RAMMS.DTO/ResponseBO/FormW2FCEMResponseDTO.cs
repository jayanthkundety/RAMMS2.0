using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormW2FCEMResponseDTO
    {
        public int PkRefNo { get; set; }
        public int Fw2PkRefNo { get; set; }
        public DateTime? Date { get; set; }
        public string Sstatus { get; set; }
        public double? Progress { get; set; }
        public bool? IsBq { get; set; }
        public bool? IsDrawing { get; set; }
        public string Remark { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool? SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }
    }
}
