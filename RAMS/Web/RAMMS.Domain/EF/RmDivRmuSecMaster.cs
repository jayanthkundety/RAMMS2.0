using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmDivRmuSecMaster
    {
        public int RdsmPkRefNo { get; set; }
        public string RdsmDivCode { get; set; }
        public string RdsmDivision { get; set; }
        public string RdsmRmuCode { get; set; }
        public string RdsmRmuName { get; set; }
        public string RdsmSectionCode { get; set; }
        public string RdsmSectionName { get; set; }
        public string RdsmModBy { get; set; }
        public DateTime? RdsmModDt { get; set; }
        public string RdsmCrBy { get; set; }
        public DateTime? RdsmCrDt { get; set; }
        public bool? RdsmActiveYn { get; set; }
    }
}
