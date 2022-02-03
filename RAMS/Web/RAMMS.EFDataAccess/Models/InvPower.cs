using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvPower
    {
        public int Pk { get; set; }
        public string AssetLocation { get; set; }
        public double? AssetKmlocation { get; set; }
        public string TollPlazaName { get; set; }
        public string AssetType { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }
        public string SpecificationofTimer { get; set; }
        public double? Capacity { get; set; }
        public double? Voltage { get; set; }
        public double? Ampere { get; set; }
        public double? BatteriesNo { get; set; }
        public string BatteryType { get; set; }
        public double? RecommendedLifetime { get; set; }
        public DateTime? InstallationDate { get; set; }
        public string Aging { get; set; }
        public string Purpose { get; set; }
    }
}
