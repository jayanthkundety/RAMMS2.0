using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvAbutment
    {
        public int Pk { get; set; }
        public string ProtectionType { get; set; }
        public double? Width { get; set; }
        public double? Length { get; set; }
        public double? Height { get; set; }
        public string AssetName { get; set; }
        public string SubstructureType { get; set; }
        public string FoundationType { get; set; }
        public string PierType { get; set; }
        public string PileType { get; set; }
    }
}
