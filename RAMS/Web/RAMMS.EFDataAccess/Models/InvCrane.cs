using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvCrane
    {
        public int Pk { get; set; }
        public double? AssetKmlocation { get; set; }
        public string AssetNumbering { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string SerialNo { get; set; }
        public string RegistrationNo { get; set; }
        public double? Capacity { get; set; }
        public DateTime? InstallationDate { get; set; }
        public double? AgingDays { get; set; }
    }
}
