using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvTpcanopyCeiling
    {
        public int Pk { get; set; }
        public string TollPlazaId { get; set; }
        public string CeilingType { get; set; }
        public double? Width { get; set; }
        public double? Length { get; set; }
        public double? Thickness { get; set; }
        public double? TotalArea { get; set; }
    }
}
