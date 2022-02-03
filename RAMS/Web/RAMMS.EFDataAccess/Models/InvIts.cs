using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvIts
    {
        public int Pk { get; set; }
        public string AssetLocation { get; set; }
        public double? AssetKmlocation { get; set; }
        public string TollPlazaName { get; set; }
        public string AssetType { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Cube { get; set; }
        public string Size { get; set; }
        public string SerialNo { get; set; }
        public string SoftwarenameAndVersion { get; set; }
        public string AntivirusNameAndVersion { get; set; }
        public string Cctvtype { get; set; }
        public string CameraType { get; set; }
        public string LenseType { get; set; }
        public double? LenseSize { get; set; }
        public string PoleType { get; set; }
        public double? PoleHeight { get; set; }
        public double? QuantityofDrs { get; set; }
        public string LocationofDrs { get; set; }
        public double? RecommendedLifetime { get; set; }
        public DateTime? InstallationDate { get; set; }
        public double? Aging { get; set; }
    }
}
