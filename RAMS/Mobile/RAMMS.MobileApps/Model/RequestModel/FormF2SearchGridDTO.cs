using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO
{
    public class FormF2SearchGridDTO
    {
        public string SmartSearch { get; set; }
        public string Division { get; set; }
        public string RoadCode { get; set; }
        public int? Year { get; set; }
        public DateTime? FromInspectionDate { get; set; }
        public DateTime? ToInspectionDate { get; set; }
        public int? SecCode { get; set; }
        public string RmuCode { get; set; }
        public int? FromYear { get; set; }
        public int? ToYear { get; set; }
        public string AssertType { get; set; }
        public int? FromChKM { get; set; }
        public string FromChM { get; set; }
        public int? ToChKM { get; set; }
        public string ToChM { get; set; }
        public string Bound { get; set; }
    }
}
