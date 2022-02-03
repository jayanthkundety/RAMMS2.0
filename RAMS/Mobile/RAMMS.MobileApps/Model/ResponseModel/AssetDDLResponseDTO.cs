using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class AssetDDLResponseDTO
    {
        public class DropDown
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public string Code { get; set; }
            public string CValue { get; set; }
            public string Group { get; set; }
            public string Item1 { get; set; }
            public string Item2 { get; set; }
            public string Item3 { get; set; }
            public int PKId { get; set; }
        }
        public List<DropDown> RMU { get; set; }
        public List<DropDown> Section { get; set; }
        public List<DropDown> RdCode { get; set; }
    }
}
