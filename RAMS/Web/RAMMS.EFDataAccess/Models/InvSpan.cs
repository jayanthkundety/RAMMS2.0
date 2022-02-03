using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvSpan
    {
        public int Pk { get; set; }
        public double? Length { get; set; }
        public double? TotalGirderBeamPerSpanBound { get; set; }
        public string GirderBeamType { get; set; }
        public string BrandManufacturer { get; set; }
        public double? BeamWidth { get; set; }
        public double? BeamLength { get; set; }
        public double? BeamHeight { get; set; }
    }
}
