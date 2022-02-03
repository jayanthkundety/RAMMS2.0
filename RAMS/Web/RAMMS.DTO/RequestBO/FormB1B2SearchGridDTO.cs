using System;
namespace RAMMS.DTO.RequestBO
{
    public class FormB1B2SearchGridDTO
    {
        public string SmartSearch { get; set; }
        public string Division { get; set; }
        public string RoadCode { get; set; }
        public int? Year { get; set; }
        public string AssetType { get; set; }
        public DateTime? FromInspectionDate { get; set; }
        public DateTime? ToInspectionDate { get; set; }
        public int? SecCode { get; set; }
        public string RmuCode { get; set; }
        public int? FromYear { get; set; }
        public int? ToYear { get; set; }
        public int? locchFromKm { get; set; }
        public string locchFromM { get; set; }
        public int? locchToKm { get; set; }
        public string locchToM { get; set; }
    }
}
