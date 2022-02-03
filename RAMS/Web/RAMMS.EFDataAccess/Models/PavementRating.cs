using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PavementRating
    {
        public int Year { get; set; }
        public double? IriGood { get; set; }
        public double? IriFair { get; set; }
        public double? IriPoor { get; set; }
        public double? IriBad { get; set; }
        public double? RutGood { get; set; }
        public double? RutFair { get; set; }
        public double? RutPoor { get; set; }
        public double? RutBad { get; set; }
        public double? MtdGood { get; set; }
        public double? MtdFair { get; set; }
        public double? MtdPoor { get; set; }
        public double? PciGood { get; set; }
        public double? PciFair { get; set; }
        public double? PciPoor { get; set; }
    }
}
