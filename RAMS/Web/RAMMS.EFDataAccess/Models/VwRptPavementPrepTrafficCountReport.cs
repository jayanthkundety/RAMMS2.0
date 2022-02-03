using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptPavementPrepTrafficCountReport
    {
        public int Year { get; set; }
        public string TcstationId { get; set; }
        public DateTime? SurveyDate { get; set; }
        public int? Car { get; set; }
        public string Utility { get; set; }
        public string Motorcycle { get; set; }
        public int? Bus { get; set; }
        public int? Truck2Axle { get; set; }
        public int? Truck3Axle { get; set; }
        public int? Truck4Axle { get; set; }
        public string Growth { get; set; }
    }
}
