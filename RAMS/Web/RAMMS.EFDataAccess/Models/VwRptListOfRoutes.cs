using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptListOfRoutes
    {
        public string Route { get; set; }
        public string RouteName { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public double? KmStart { get; set; }
        public double? KmEnd { get; set; }
        public string Bound { get; set; }
        public string AscendingDirection { get; set; }
    }
}
