using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.Report
{
    public class FORMTRpt
    {
        public string RefId { get; set; }
        public string RMU { get; set; }
        public DateTime? InspectedDate { get; set; }
        public string RefNo { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
         
        public string RecName { get; set; }
        public string RecDesg { get; set; }
        public DateTime? RecDate { get; set; }
        public string HdName { get; set; }
        public string HdDesg { get; set; }
        public DateTime? HdDate { get; set; }
        public  FORMTRptDetail Details { get; set; }

    }
    public class FORMTRptDetail
    {
        public int? Day { get; set; }
        public int? TotalDay { get; set; }
        public decimal? HourlycountPerDay { get; set; }
        public string DirectionFrom { get; set; }
        public string DirectionTo { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string Description { get; set; }
        public string DescriptionPC { get; set; }
        public string DescriptionHV { get; set; }
        public string DescriptionMC { get; set; }
        public IEnumerable<FORMTRptVechicle> Vechilce { get; set; }
    }

    public class FORMTRptVechicle
    {
        public string VechicleType { get; set; }
        public string Axle { get; set; }
        public string Loading { get; set; }
        public int? Time { get; set; }
        public int? Count { get; set; }
    }
}