using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.SearchBO
{
    public class FormHSearchDTO
    {
        public string SmartInputValue { get; set; }
        public string RoadCode { get; set; }
        public string Section { get; set; }
        public string RMU { get; set; }
        public string AssetGroupCode { get; set; }
        public int? FromChKM { get; set; }
        public int? FromChM { get; set; }
        public int? ToChKM { get; set; }
        public int? ToChM { get; set; }
        public string InspectionDate { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public int? ChinageFromKm { get; set; }
        public int? ChinageToKm { get; set; }
        public int? ChinageFromM { get; set; }
        public int? ChinageToM { get; set; }
    }
}
