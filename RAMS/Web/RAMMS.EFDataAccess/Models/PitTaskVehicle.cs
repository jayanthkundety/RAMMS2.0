using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PitTaskVehicle
    {
        public int PitTaskPk { get; set; }
        public int RecordIndex { get; set; }
        public string VehicleName { get; set; }
        public string VehicleType { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleRegNo { get; set; }
        public string VehicleCapacity { get; set; }

        public virtual PitTask PitTaskPkNavigation { get; set; }
    }
}
