using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormW2FECMResponseDTO
    {
        public int PkRefNo { get; set; }
        public int? Fw2PkRefNo { get; set; }
        public string IwRefNo { get; set; }
        public string ProjectTitle { get; set; }
        public DateTime? DtTecm { get; set; }
        public DateTime? Dt { get; set; }
        public string Sts { get; set; }
        public string StsRemarks { get; set; }
        public bool? IsBq { get; set; }
        public bool? IsDrawing { get; set; }
        public bool? AgreedNegoLetrYn { get; set; }
        public DateTime? DtAgreedNego { get; set; }
        public double? ProgressPerc { get; set; }
        public string Remark { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
    }
}
