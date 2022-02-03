using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormFDDetailsDTO
    {
        public int PkRefNo { get; set; }
        public int? hPkRefNo { get; set; }
        public int? AiPkRefNo { get; set; }
        public string AiAssetGrpCode { get; set; }
        public int? AiFrmCh { get; set; }
        public string AiFrmChDeci { get; set; }
        public int? AiToCh { get; set; }
        public string AiToChDeci { get; set; }
        public string AiBound { get; set; }
        public string AiGrpType { get; set; }
        public int? Condition { get; set; }
        public string Remarks { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool? ActiveYn { get; set; }
        public decimal DBFromCHKm { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public decimal FromCHKm { get { return Convert.ToDecimal((AiFrmCh.HasValue ? AiFrmCh.Value.ToString() : "0") + "." + AiFrmChDeci); } }
        public decimal ToCHKm { get { return Convert.ToDecimal((AiToCh.HasValue ? AiToCh.Value.ToString() : "0") + "." + AiToChDeci); } }
    }
}
