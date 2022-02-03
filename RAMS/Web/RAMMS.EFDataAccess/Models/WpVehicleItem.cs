using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpVehicleItem
    {
        public int WpVehiclePk { get; set; }
        public int RecordIndex { get; set; }
        public string PermitNo { get; set; }
        public string Activity { get; set; }
        public double? LaneClosureKmfrom { get; set; }
        public double? LaneClosureKmto { get; set; }
        public DateTime? VehicleFrom { get; set; }
        public string ActivatedBy { get; set; }
        public DateTime? VehicleTo { get; set; }
        public string DeactivatedBy { get; set; }

        public virtual WpVehicle WpVehiclePkNavigation { get; set; }
    }
}
