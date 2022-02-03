using System;
namespace RAMMS.DTO.SearchBO
{
    public class RequestFormReference
    {
        public string RoadCode { get; set; }
        public string AssetGroup { get; set; }
        public int? LocationFrom { get; set; }
        public int? LocationFromDec { get; set; }
        public int? LocationTo { get; set; }
        public int? LocationToDec { get; set; }
        public FormType FormType { get; set; }
        public DateTime? DateOfInspection { get; set; }
    }
}
