using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptPavementKpilistingView
    {
        public int? Year { get; set; }
        public int? Total { get; set; }
        public int? Pdpm1aCount { get; set; }
        public int? Pdpm1bCount { get; set; }
        public int? Pdpm2aCount { get; set; }
        public int? Pdpm2bCount { get; set; }
        public int? Pdpm3count { get; set; }
        public int? Pdpm4count { get; set; }
        public int? Pdpm5aCount { get; set; }
        public int? Pdpm5bCount { get; set; }
        public int? Pdpm6count { get; set; }
        public int? Pdpm7count { get; set; }
        public string Code { get; set; }
        public string Network { get; set; }
        public string Route { get; set; }
        public string RoadNo { get; set; }
        public int MinField { get; set; }
        public int MaxField { get; set; }
        public string Pdpm1aText { get; set; }
        public string Pdpm1bText { get; set; }
        public string Pdpm2aText { get; set; }
        public string Pdpm2bText { get; set; }
        public string Pdpm3text { get; set; }
        public string Pdpm4text { get; set; }
        public string Pdpm5aText { get; set; }
        public string Pdpm5bText { get; set; }
        public string Pdpm6text { get; set; }
        public string Pdpm7text { get; set; }
        public double? Pdpm1aPercent { get; set; }
        public double? Pdpm1bPercent { get; set; }
        public double? Pdpm2aPercent { get; set; }
        public double? Pdpm2bPercent { get; set; }
        public double? Pdpm3Percent { get; set; }
        public double? Pdpm4Percent { get; set; }
        public double? Pdpm5aPercent { get; set; }
        public double? Pdpm5bPercent { get; set; }
        public double? Pdpm6Percent { get; set; }
        public double? Pdpm7Percent { get; set; }
    }
}
