using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvBridgeBearing
    {
        public int Pk { get; set; }
        public string BearingType { get; set; }
        public string BrandManufacturer { get; set; }
        public double? Width { get; set; }
        public double? Length { get; set; }
        public double? Thickness { get; set; }
    }
}
