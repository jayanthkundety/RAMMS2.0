using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class TrafficInfoDetail
    {
        public int Pk { get; set; }
        public int TrafficYear { get; set; }
        public int SectionPk { get; set; }
        public string TcstationId { get; set; }
        public string MainlineTollPlaza { get; set; }
        public double? Kmfrom { get; set; }
        public double? Kmto { get; set; }
        public int? Class0 { get; set; }
        public int? Class1 { get; set; }
        public int? Class2 { get; set; }
        public int? Class3 { get; set; }
        public int? Class4 { get; set; }
        public int? Class5 { get; set; }
        public int? Class6 { get; set; }
        public int? Class7 { get; set; }
        public int? TotalCount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public double? Efclass2 { get; set; }
        public double? Efclass3 { get; set; }
        public double? Efclass5 { get; set; }
        public double? TotalMesal { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
    }
}
