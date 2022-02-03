using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptListOfFeatureCommercialFacilities
    {
        public string CommercialFacilitiesId { get; set; }
        public string FeatureName { get; set; }
        public string Abbreviation { get; set; }
        public string Region { get; set; }
        public string Section { get; set; }
        public string Route { get; set; }
        public double? KmLocation { get; set; }
        public string Bound { get; set; }
        public string Lof { get; set; }
        public string Owner { get; set; }
    }
}
