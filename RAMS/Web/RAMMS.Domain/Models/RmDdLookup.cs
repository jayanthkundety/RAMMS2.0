using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmDdLookup
    {
        public int DdlPkRefNo { get; set; }
        public string DdlType { get; set; }
        public string DdlTypeCode { get; set; }
        public string DdlTypeDesc { get; set; }
        public string DdlTypeValue { get; set; }
        public string DdlTypeRemarks { get; set; }
        public string DdlModBy { get; set; }
        public DateTime? DdlModDt { get; set; }
        public string DdlCrBy { get; set; }
        public DateTime? DdlCrDt { get; set; }
        public bool? DdlActiveYn { get; set; }
    }
}
