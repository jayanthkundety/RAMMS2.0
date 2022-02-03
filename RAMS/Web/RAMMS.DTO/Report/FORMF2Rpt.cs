using System;
using System.Collections.Generic;

namespace RAMMS.DTO.Report
{
    public class FORMF2Rpt
    {
        public string Division { get; set; }
        public string District { get; set; }
        public string RMU { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public string InspectedByName { get; set; }
        public string InspectedByDesignation { get; set; }
        public DateTime? InspectedDate { get; set; }
        public decimal? RoadLength { get; set; }
        public IEnumerable<FORMF2RptDetail> Details { get; set; }
        public object CrewLeader { get; set; }
    }

    public class FORMF2RptDetail
    {
        public int? StartingChKm { get; set; }
        public string StartingChM { get; set; }
        public string Code { get; set; }
        public double? Condition1 { get; set; }
        public double? Condition2 { get; set; }
        public double? Condition3 { get; set; }
        public string RML { get; set; }
        public double? PostSpacing { get; set; }
        public string Remarks { get; set; }
    }
}
