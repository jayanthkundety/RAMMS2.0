using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.RequestBO
{
    public class AssetDDLRequestDTO
    {
        public string RMU { get; set; }
        public int SectionCode { get; set; }
        public string RdCode { get; set; }
        public string GrpCode { get; set; }
        public bool IncludeInActive { get; set; }
    }
}
