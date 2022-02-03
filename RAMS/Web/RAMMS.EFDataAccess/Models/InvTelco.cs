using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvTelco
    {
        public int Pk { get; set; }
        public string AssetLocation { get; set; }
        public double? AssetKmlocation { get; set; }
        public string TollPlazaName { get; set; }
        public string AssetType { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string SerialNo { get; set; }
        public string SoftwarenameAndVersion { get; set; }
        public string Capacity { get; set; }
        public double? RecommendedLifetime { get; set; }
        public DateTime? InstallationDate { get; set; }
        public double? Aging { get; set; }
    }
}
