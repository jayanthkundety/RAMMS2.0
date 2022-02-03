using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptPavementForwardWorkProgram3Years
    {
        public int? Year { get; set; }
        public string Section { get; set; }
        public double? KmFromPca { get; set; }
        public double? KmToPca { get; set; }
        public double? Length { get; set; }
        public string Bound { get; set; }
        public double? Lanes { get; set; }
        public double? WidthSurf { get; set; }
        public string DescriptionOfWork { get; set; }
    }
}
