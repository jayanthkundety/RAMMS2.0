using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptListOfFeatureInterchangesAndJunction
    {
        public string LocalityOfFeature { get; set; }
        public string FeatureName { get; set; }
        public string Abbreviation { get; set; }
        public string Region { get; set; }
        public string Section { get; set; }
        public double? KmLocation { get; set; }
        public string Bound { get; set; }
        public string Lof { get; set; }
        public string Route { get; set; }
        public string Owner { get; set; }
        public string MhaOffice { get; set; }
        public DateTime? OpeningDate { get; set; }
        public double? SpeedLimit { get; set; }
    }
}
