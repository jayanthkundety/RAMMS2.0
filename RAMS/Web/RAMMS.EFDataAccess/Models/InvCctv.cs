using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvCctv
    {
        public int Pk { get; set; }
        public string AssetLocation { get; set; }
        public double? AssetKmlocation { get; set; }
        public string TollPlazaName { get; set; }
        public string TollBooth { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }
        public string SoftwareNameAndVersion { get; set; }
        public string AntivirusNameAndVersion { get; set; }
        public double? Capacity { get; set; }
        public string Cctvtype { get; set; }
        public string CameraType { get; set; }
        public string CameraName { get; set; }
        public string LenseType { get; set; }
        public double? LenseSize { get; set; }
        public string PoleType { get; set; }
        public double? PoleHeight { get; set; }
        public string SystemDvrtype { get; set; }
        public string SignalCableType { get; set; }
        public double? RecommendedLifetime { get; set; }
        public DateTime? InstallationDate { get; set; }
        public double? Aging { get; set; }
    }
}
