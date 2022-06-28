using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.Report
{
    public class FORMF3Rpt
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
        public IEnumerable<FORMF3RptDetail> Details { get; set; }
        public object CrewLeader { get; set; }
    }
    public class FORMF3RptDetail
    {
 
        public int? LocationChKm { get; set; }
        public int? LocationChM { get; set; }
        public string StructCode { get; set; }
        public string Bound { get; set; }
        public int? Condition { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public string Descriptions { get; set; }
       
    }
}
