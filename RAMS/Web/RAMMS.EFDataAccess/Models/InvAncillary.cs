using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvAncillary
    {
        public int Pk { get; set; }
        public string AssetLocation { get; set; }
        public double? AssetKmlocation { get; set; }
        public string TollPlazaName { get; set; }
        public string AssetType { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }
        public double? Capacity { get; set; }
        public double? NoofSpeaker { get; set; }
        public string TypeofGate { get; set; }
        public double? TotalRemoteControl { get; set; }
        public double? TotalAccessCard { get; set; }
        public string MotorSpecification { get; set; }
        public double? RecommendedLifetime { get; set; }
        public DateTime? InstallationDate { get; set; }
        public double? Aging { get; set; }
    }
}
