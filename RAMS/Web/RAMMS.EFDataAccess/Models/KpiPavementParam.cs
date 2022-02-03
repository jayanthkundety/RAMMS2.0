using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class KpiPavementParam
    {
        public int Year { get; set; }
        public double? Pdpm1aPercent { get; set; }
        public double? Pdpm1aIri { get; set; }
        public double? Pdpm1bPercent { get; set; }
        public double? Pdpm1bIri { get; set; }
        public double? Pdpm2aRut { get; set; }
        public double? Pdpm2aPercent { get; set; }
        public double? Pdpm2bRut { get; set; }
        public double? Pdpm2bPercent { get; set; }
        public double? Pdpm3Mtd { get; set; }
        public double? Pdpm3Percent { get; set; }
        public double? Pdpm4Pci { get; set; }
        public double? Pdpm4Percent { get; set; }
        public double? Pdpm5aFwdCentralDef { get; set; }
        public double? Pdpm5aPercent { get; set; }
        public double? Pdpm5bDefDifference { get; set; }
        public double? Pdpm5bPercent { get; set; }
        public double? Pdpm6ResidualLife { get; set; }
        public double? Pdpm6Percent { get; set; }
        public double? Pdpm7Scrim { get; set; }
        public double? Pdpm7Percent { get; set; }
    }
}
