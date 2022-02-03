using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvMainGt
    {
        public int Pk { get; set; }
        public string Equipment { get; set; }
        public string AssetLocation { get; set; }
        public double? AssetKmlocation { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string SerialNo { get; set; }
        public double? Capacity { get; set; }
        public double? RecommendedLifetime { get; set; }
        public DateTime? InstallationDate { get; set; }
        public double? Aging { get; set; }
    }
}
