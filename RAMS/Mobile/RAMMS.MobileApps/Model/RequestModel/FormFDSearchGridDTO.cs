using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO
{
    public class FormFDSearchGridDTO
    {
        public int RefNo { get; set; }
        public string RefID { get; set; }
        public DateTime? InsDate { get; set; }
        public int? Year { get; set; }
        public string RMUCode { get; set; }
        public string RMUDesc { get; set; }
        public string SecCode { get; set; }
        public string SecName { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public double? RoadId { get; set; }
        public string InspectedByID { get; set; }
        public string InspectedBy { get; set; }
        public string CrewLeaderID { get; set; }
        public string CrewLeader { get; set; }
        public bool? Active { get; set; }
        public string Status { get; set; }
        public int SNo { get; set; }
    }
}
