using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormTDtlGridDTO
    {
        public int SNo { get; set; }
        public int PkRefNo { get; set; }
        public string Ch { get; set; }
        public int? FrmCh { get; set; }
        public int? FrmChDec { get; set; }
        public string StructureCode { get; set; }
        public int OverallCondition { get; set; }
        public string AssetId { get; set; }
        public double? Tier { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? BottomWidth { get; set; }
        public double? Height { get; set; }
        public string Description { get; set; }
       

    }
}
