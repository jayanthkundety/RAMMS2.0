using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormWDDtlResponseDTO
    {
        public int PkRefNo { get; set; }
        public int? FwdPkRefNo { get; set; }
        public string Reason { get; set; }
        public string Clause { get; set; }
        public double? ExtnPrd { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }

        public virtual FormWDResponseDTO FwdPkRefNoNavigation { get; set; }
    }
}
