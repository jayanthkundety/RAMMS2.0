using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.Report
{
    public class FORMF4Rpt
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
        public IEnumerable<FORMF4RptDetail> Details { get; set; }
        public object CrewLeader { get; set; }
    }
    public class FORMF4RptDetail
    {
        public int? CentreLineChKm { get; set; }
        public string CentreLineChM { get; set; }
        public string Code { get; set; }
        public double? Condition1 { get; set; }
        public double? Condition2 { get; set; }
        public double? Condition3 { get; set; }
        public double? Length { get; set; }
        public int? NoOfCell { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public string RML { get; set; }
        public int? OverAllCondition { get; set; }
        public double? PostSpacing { get; set; }
        public string Remarks { get; set; }
        public string InletHeadWall { get; set; }
        public string OutletHeadWall { get; set; }
    }
}
