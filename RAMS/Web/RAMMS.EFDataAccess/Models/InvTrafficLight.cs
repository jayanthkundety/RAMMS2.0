using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvTrafficLight
    {
        public int Pk { get; set; }
        public string AssetLocation { get; set; }
        public double? AssetKmlocation { get; set; }
        public string TollPlazaName { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }
        public string Size { get; set; }
        public string TypeofPole { get; set; }
        public string TypeofController { get; set; }
        public string TypeofInductionLoop { get; set; }
        public string TypeofBulb { get; set; }
        public double? NoofPole { get; set; }
        public string Kduspecification { get; set; }
        public double? RecommendedLifetime { get; set; }
        public DateTime? InstallationDate { get; set; }
        public double? Aging { get; set; }
    }
}
