using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormF3DtlGridDTO
    {

        public int PkRefNo { get; set; }
        public string AssetId { get; set; }
        public string Ch { get; set; }
        public int? FrmCh { get; set; }
        public int? FrmChDec { get; set; }
        public string StructureCode { get; set; }
        public int ConditionRating { get; set; }
        public string Bound { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public string Description { get; set; }
       

    }
}
