using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO
{
    public class FormC1C2SearchGridDTO
    {
        public int RefNo { get; set; }
        public string RefID { get; set; }
        public int? AssetID { get; set; }
        public string AssetRefId { get; set; }
        public DateTime? InsDate { get; set; }
        public int? Year { get; set; }
        public string RMUCode { get; set; }
        public string RMUDesc { get; set; }
        public string SecCode { get; set; }
        public string SecName { get; set; }
        public string Bound { get; set; }
        public string AssetType { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public double? RoadId { get; set; }
        public decimal LocationCH { get; set; }
        public string InspectedBy { get; set; }
        public string AuditedBy { get; set; }
        public double? CDia { get; set; }
        public double? CULWidth { get; set; }
        public bool? Active { get; set; }
        public string Status { get; set; }
        public int SNo { get; set; }
    }
}
