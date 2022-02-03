using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptListOfRegionsAndSections
    {
        public string Region { get; set; }
        public string Section { get; set; }
        public string Route { get; set; }
        public double KmStart { get; set; }
        public double KmEnd { get; set; }
        public string SectionRouteFrom { get; set; }
        public string SectionRouteTo { get; set; }
        public string AssetOwner { get; set; }
        public string Network { get; set; }
        public bool? IsActive { get; set; }
    }
}
